#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using UniversalHandlersLibrary;

#endregion


namespace WeightWatchingProgramPlus
{

	/// <summary>
	/// Main Window and all tabs.
	/// </summary>
	/// 
    
	internal partial class MainForm : Form
	{
		private Modification Modification = new Modification ();

		private PopupHandler PopupHandler = new PopupHandler ();
		
		private Validation Validation = new Validation ();
		
		private Functions Functions = new Functions ();

		internal MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			GlobalVariables.MainForm = this;
			
			GlobalVariables.ExactResetDateTimePicker = exactResetDatetimePicker;
			
			Functions.InitializeForms(foodList, calorieRadioButton, timeRadioButton, caloriesLabel, Seperator5, howManyServingsLabel, foodNameEditBox, definerEditBox, servingSizeEditBox, caloriesPerServingEditBox, manualCalorieEditBox, defaultCaloriesNumericUpDown, exactResetDatetimePicker);
			
			//
		}

		#region Main Tab

		#region Search Bar

		protected internal virtual void SearchBarTextChanged (object sender, EventArgs e)
		{
				
			if (!string.IsNullOrWhiteSpace(searchBar.Text) && !searchBar.Text.Equals("Click Here to Search the Food List", StringComparison.CurrentCultureIgnoreCase))
			{
					
				if (!nextSearchButton.Enabled)
				{
						
					nextSearchButton.Enabled = true;
						
				}
					
				Functions.Find(0, searchBar.Text, null, exactSearchCheckBox.Checked, foodList, false);
					
				return;
					
			}
			if (nextSearchButton.Enabled)
			{
					
				nextSearchButton.Enabled = false;
					
			}
				
			foodList.ClearSelected();
				
		}

		protected internal void SearchBarFocusGranted (object sender, EventArgs e)
		{
				
			searchBar.Clear();
				
			clearSearchBarButton.Enabled = true;
				
			Font tempfont = new Font ("Times New Roman", 10f);
				
			searchBar.Font = tempfont;
				
		}

		protected internal void ClearSearchBar (object sender, EventArgs e)
		{
				
			if (!nextSearchButton.Focused)
			{
					
				searchBar.Clear();
					
				clearSearchBarButton.Enabled = false;
					
				Modification.WriteToObject(null, searchBar, 2);
					
				nextSearchButton.Enabled = false;
					
			}
				
		}

		protected internal void FindNextSearchItem (object sender, EventArgs e)
		{
				
			if (string.IsNullOrWhiteSpace(searchBar.Text) || searchBar.Text.Equals("Click Here to Search the Food List", StringComparison.CurrentCultureIgnoreCase))
			{
					
				if (nextSearchButton.Enabled)
				{
						
					nextSearchButton.Enabled = false;
						
				}
					
				return;
					
			}
				
			Functions.Find(GlobalVariables.SelectedListItem, searchBar.Text, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, exactSearchCheckBox.Checked, foodList, true);
				
		}

		#endregion

		#region Food Listbox

		protected internal virtual void FoodListSelectedIndexChanged (object sender, EventArgs e)
		{
				
			if (foodList.SelectedIndex > -1)
			{
					
				if (newItemCheckbox.Checked)
				{
						
					newItemCheckbox.Checked = false;
						
				}
					
				GlobalVariables.SelectedListItem = foodList.SelectedIndex;
					
				howManyServingsLabel.Text = string.Format(CultureInfo.CurrentCulture, "How many {0}s do you plan on {1}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4.Contains("fluid", StringComparison.CurrentCultureIgnoreCase) ? "drinking" : "eating");
					
				foodNameEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item1;
					
				foodNameEditBox.TextAlign = HorizontalAlignment.Center;
					
				servingSizeEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item2.ToString(CultureInfo.CurrentCulture);
					
				servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
					
				definerEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item4;
					
				definerEditBox.TextAlign = HorizontalAlignment.Center;
					
				caloriesPerServingEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item3.ToString(CultureInfo.CurrentCulture);
					
				caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		protected internal void FoodListFocusChanged (object sender, EventArgs e)
		{
				
			if (newItemCheckbox.Checked)
			{
					
				newItemCheckbox.Checked = false;
					
			}
				
			if (foodList.Focused)
			{
					
				GlobalVariables.SelectedListItem = foodList.SelectedIndex;
					
				if (!deleteSelectedFoodItemButton.Enabled && foodList.Items.Count > 0)
				{
						
					deleteSelectedFoodItemButton.Enabled = true;
						
				}
					
			}
			else
			{
					
				if (deleteSelectedFoodItemButton.Enabled && foodList.Items.Count <= 0)
				{
						
					deleteSelectedFoodItemButton.Enabled = false;
						
				}
					
				foodList.ClearSelected();
					
			}
				
		}

		protected internal void DeleteFoodItemFromTable (object sender, EventArgs e)
		{
			
			if (PopupHandler.CreatePopup("Are you sure you want to delete " + FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1 + "?", foodList, 5, false) == DialogResult.Yes)
			{
					
				Modification.DumpFoodPropertiesList(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
					
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string> (null, 0f, 0f, null));
					
				Functions.Refresh_foodList(foodList);
					
				foodList.SetSelected(GlobalVariables.SelectedListItem, true);
					
			}
				
		}

		#endregion

		#region Edit Your Calories Manual Tab

		protected internal void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
		{
				
			Modification.DumpFoodPropertiesList(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
				
			foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
				
		}

		protected internal void ResetCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = (decimal)Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3;
				
		}

		protected internal void ZeroOutCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = 0;
				
		}

		protected internal void ManualSubmitButtonClicked (object sender, EventArgs e)
		{

			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now.AddDays(1), (float)manualCalorieEditBox.Value, Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3, new [] {
				false,
				false
			});
				
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
				
		}

		void ChangedTabManualAddSub (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2 >= 0 ? (decimal)Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2 : 0;
				
		}

		#endregion

		protected internal void TimeOrCaloriesChangedWithoutEvent (object sender, EventArgs e)
		{
				
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
				
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
				
		}

		protected internal void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
		{
				
			string oldtext = foodNameEditBox.Text;
				
			Modification.ModifyFoodItemProperty(foodList, foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, newItemCheckbox);
				
			Functions.Find(0, oldtext, null, true, foodList, false);
				
		}

		protected internal void ModifyCalories (object sender, EventArgs e)
		{
			bool add = true;
			int errorNum = 1;
			string warningText = "The amount of calories that you are trying to add would put you over your daily limit, and is not allowed.";
			
			if (sender.ToString().Contains("subtract", StringComparison.OrdinalIgnoreCase))
			{
				
				add = false;
				errorNum = 2;
				warningText = "The amount of calories that are about to be subtracted would put you below your daily limit! Continue?";
				
			} 
			
			float tempcalories = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2;
			
			if (add)
			{
				
				tempcalories += Modification.ModifyCalories(userServingInputTextBox, add);
				
			}
			else
			{
				
				tempcalories -= Modification.ModifyCalories(userServingInputTextBox, add);
				
			}
			
			bool safeToModify = false;
			
			if (tempcalories < 0f && !add || tempcalories > Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3 && add)
			{
				
				if (PopupHandler.CreatePopup(warningText, null, errorNum, false) == DialogResult.Yes)
				{
					
					safeToModify = true;
					
				}
				
			}
			else
			{
				
				safeToModify = true;
				
			}
			
			if (safeToModify)
			{
				
				Storage.WriteFoodEaten("Files\\Text\\", "Food Diary.txt", WriteToFileCheckBox, RecordFoodCheckBox, userServingInputTextBox, add);
				
				if (Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2 >= 0)
				{
					
					manualCalorieEditBox.Value = (decimal)Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2;
					
				}
				
			}
			
			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now.AddDays(1), tempcalories,  Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3, new [] {
				false,
				false
			});
			
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
			
		}

		#endregion

		#region Additional Options Tab

		void ResetCaloriesSpecificCheckBoxCheckStatusChanged (object sender, EventArgs e)
		{
			
			exactResetDatetimePicker.Enabled = resetCaloriesManualCheckBox.Checked;
			
			Validation.ManualTimeEngaged = resetCaloriesManualCheckBox.Checked;
			
		}

		void ExactResetDatetimePickerValueChanged (object sender, EventArgs e)
		{
			if (resetCaloriesManualCheckBox.Checked && exactResetDatetimePicker.Value != Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1)
			{
				
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, exactResetDatetimePicker.Value, Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2, Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3, new [] {
					false,
					true
				});
				
			}
			
		}

		void DefaultCaloriesSetButtonClick (object sender, EventArgs e)
		{
			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now, Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2, (float)defaultCaloriesNumericUpDown.Value, new [] {
				false,
				false
			});
		}

		#endregion
	}

	/// <summary>
	/// Misc general functions
	/// </summary>
	class Functions
	{
		
		internal static void InitializeForms (ListBox foodList, RadioButton calorieRadioButton, RadioButton timeRadioButton, Label caloriesLabel, Label Seperator5, Label howManyServingsLabel, TextBox foodNameEditBox, TextBox definerEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, NumericUpDown manualCalorieEditBox, NumericUpDown defaultCaloriesNumericUpDown, DateTimePicker exactResetDatetimePicker)
		{
			const HorizontalAlignment center = HorizontalAlignment.Center;
			
			GlobalVariables.MainForm.Text = GlobalVariables.RegistryMainValue;
			
			Modification.ModifySeperator(Seperator5, false);
			
			Refresh_foodList(foodList);
			
			foodList.SetSelected(foodList.TopIndex, true);
			
			GlobalVariables.SelectedListItem = 0;
			
			howManyServingsLabel.Text = string.Format(CultureInfo.CurrentCulture, "How many {0}s do you plan on {1}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4.Contains("fluid", StringComparison.CurrentCultureIgnoreCase) ? "drinking" : "eating");
			
			foodNameEditBox.Text = foodList.Items [foodList.TopIndex].ToString();
			foodNameEditBox.TextAlign = center;
			
			servingSizeEditBox.Text = FoodRelated.CombinedFoodList [0].Item2.ToString(CultureInfo.InvariantCulture);
			servingSizeEditBox.TextAlign = center;
			
			definerEditBox.Text = FoodRelated.CombinedFoodList [0].Item4;
			definerEditBox.TextAlign = center;
			
			caloriesPerServingEditBox.Text = FoodRelated.CombinedFoodList [0].Item3.ToString(CultureInfo.InvariantCulture);
			caloriesPerServingEditBox.TextAlign = center;
			
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
			
			exactResetDatetimePicker.Value = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1;
			
			manualCalorieEditBox.Value = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2 >= 0 ? decimal.Parse(Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture) : 0;
			
			defaultCaloriesNumericUpDown.Value = (decimal)Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3;
		}

		public static void Refresh_foodList (ListBox foodList)
		{
			FoodRelated.CombinedFoodList.Clear();
			
			Storage.ReadFoodTable("Files\\Text\\", "food.table");
			
			foodList.DataSource = null;
			
			foodList.Items.Clear();
			
			List<string> Item1 = new List<string> ();
			
			for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
			{
				Tuple<string, float, float, string> name = FoodRelated.CombinedFoodList [i];
				
				Item1.Add(name.Item1);
				
			}
			
			Item1.Sort();
			
			foodList.DataSource = Item1;
			
			Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string> (null, 0f, 0f, null));
		}

		#region Find Item Summary
		/// <summary>
		/// Finds an item within the food list and selects it.
		/// </summary>
		/// <param name="offset">
		/// The previous item number.
		/// </param>
		/// <param name="stringToFind">
		/// The string you'd like to find within the food list.
		/// </param>
		/// <param name="stringToAvoid">
		/// The previously found name from the food list.
		/// </param>
		/// <param name="exactSearch">
		/// Is this a search where only an exact match is allowed?
		/// </param>
		/// <param name="foodList">
		/// The food list.
		/// </param>
		/// <param name="next">
		/// Was the next button pressed?
		/// </param>
		#endregion
		internal void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, ListBox foodList, bool next)
		{
			for (int i = offset; i <= foodList.Items.Count; i++)
			{
				
				if (i < foodList.Items.Count)
				{
					if (exactSearch)
					{
						
						if (foodList.Items [i].ToString().Equals(stringToFind, StringComparison.CurrentCultureIgnoreCase) &&
						    !foodList.Items [i].ToString().Equals(stringToAvoid, StringComparison.CurrentCultureIgnoreCase))
						{
							
							foodList.SelectedIndex = i;
							
							return;
							
						}
						
					}
					else
					{
					
						if (foodList.Items [i].ToString().Contains(stringToFind, StringComparison.CurrentCultureIgnoreCase) &&
						    !foodList.Items [i].ToString().Equals(stringToAvoid, StringComparison.CurrentCultureIgnoreCase))
						{
							
							foodList.SelectedIndex = i;
							
							return;
							
						}
						
					}
					
				}
				else if (i >= foodList.Items.Count && !exactSearch && next)
				{
					
					Find(0, stringToFind, null, exactSearch, foodList, next);
					
					return;
				}
				
			}
			return;
		}
	}
}
