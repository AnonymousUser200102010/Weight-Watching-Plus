#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
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

		private Functions Functions = new Functions ();

		private static MainForm mainForm = new MainForm ();

		internal MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			
			mainForm = (this as MainForm);

			Functions.InitializeForms();
			
			arithmeticSignComboBox.SelectedIndex = 0;
			
			//
		}

		#region delegates

		/// <summary>
		/// Gets or Sets the title of the application after launch.
		/// </summary>
		public static string MainFormTitle
		{
				
			set { mainForm.Text = value; }
				
		}

		/// <summary>
		/// Gets or Sets the value in the #Servings NumericUpDown.
		/// </summary>
		public static decimal UserProvidedServings
		{
				
			get{ return mainForm.userServingInputTextBox.Value; }
				
			set{ mainForm.userServingInputTextBox.Value = value; }
				
		}

		/// <summary>
		/// Gets or Sets whether the manual time is being used or not.
		/// </summary>
		public static bool ManualTimeIsInitiated
		{
				
			get{ return mainForm.resetCaloriesManualCheckBox.Checked; }
				
			set{ mainForm.resetCaloriesManualCheckBox.Checked = value; }
				
		}

		/// <summary>
		/// Gets or Sets the Manual Reset Time.
		/// </summary>
		public static DateTime ManualDateTime
		{
				
			get { return mainForm.exactResetDatetimePicker.Value; }
				
			set { mainForm.exactResetDatetimePicker.Value = value; }
				
		}

		/// <summary>
		/// Gets or Sets the main Food List ListBox's DataSource.
		/// </summary>
		public static object MainFoodListDataSource
		{
				
			set { mainForm.foodList.DataSource = value; }
				
		}

		/// <summary>
		/// Gets the main Food List ListBox's items.
		/// </summary>
		public static ListBox.ObjectCollection MainFoodListItems
		{
				
			get { return mainForm.foodList.Items; }
				
		}

		/// <summary>
		/// Gets or Sets the SelectedIndex for the main Food List ListBox
		/// </summary>
		public static int FoodListSelected
		{
				
			set { mainForm.foodList.SelectedIndex = value; }
				
		}

		#region FoodListGet/Select Summary

		/// <summary>
		/// Gets or Sets the selection for the specified item in the main Food List ListBox.
		/// </summary>
		/// <param name="set">
		/// Is this operation setting the selected value for the main Food List ListBox as well?
		/// </param>
		/// <param name="index">
		/// The zero-based index of the item that determines whether it is selected.
		/// </param>
		/// <param name="value">
		/// True to select the specified item; otherwise false.
		/// </param>
		/// <returns>
		/// Returns a bool based on the GetSelected function for the main Food List ListBox.
		/// </returns>
		#endregion
			public static bool FoodListGetOrSetSelected (bool set, int index, bool value)
		{
					
			if (set)
			{
						
				mainForm.foodList.SetSelected(index, value);
						
			}
						
			return mainForm.foodList.GetSelected(index);			
					
		}

		/// <summary>
		/// Gets or Sets the uppermost item in the main Food List ListBox.
		/// </summary>
		public static int GetFoodListTopItem
		{
				
			get { return mainForm.foodList.TopIndex; }
				
		}

		/// <summary>
		/// Gets or Sets the label which the user sees asking how many servings they are eating.
		/// </summary>
		public static string NumberOfServingsLabel
		{
				
			set { mainForm.howManyServingsLabel.Text = value; }
				
		}

		/// <summary>
		/// Gets or Sets the "Name" property box which shows the name of the currently selected food item.
		/// </summary>
		public static string FoodNameProperty
		{
				
			get { return mainForm.foodNameEditBox.Text; }
				
			set
			{ 
					
				mainForm.foodNameEditBox.Text = value;
				mainForm.foodNameEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		/// <summary>
		/// Gets or Sets the "Definer" property box which shows the definer of the currently selected food item.
		/// </summary>
		public static string DefinerProperty
		{
				
			get { return mainForm.definerEditBox.Text; }
				
			set
			{ 
					
				mainForm.definerEditBox.Text = value; 
				mainForm.definerEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		/// <summary>
		/// Gets or Sets the "Serving Size" property box which shows the serving size of the currently selected food item.
		/// </summary>
		public static decimal ServingSizeProperty
		{
				
			get { return mainForm.servingSizeEditBox.Value; }
				
			set
			{
					
				mainForm.servingSizeEditBox.Value = value; 
				mainForm.servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		/// <summary>
		/// Gets or Sets the "Calories Per Serving" property box which shows how many calories are in each serving of the currently selected food item.
		/// </summary>
		public static decimal CaloriesPerServingProperty
		{
				
			get { return mainForm.caloriesPerServingEditBox.Value; }
				
			set
			{
					
				mainForm.caloriesPerServingEditBox.Value = value; 
				mainForm.caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		public static bool IsDrinkProperty
		{
				
			get { return mainForm.isDrinkCheckBox.Checked; }
				
			set { mainForm.isDrinkCheckBox.Checked = value; }
				
		}

		/// <summary>
		/// Gets or Sets if the user is checking the time.
		/// </summary>
		public static bool UserCheckingTime
		{
				
			get { return mainForm.timeRadioButton.Checked; }
				
		}

		/// <summary>
		/// Gets or Sets if the user is checking their current calorie balance.
		/// </summary>
		public static bool UserCheckingCalories
		{
				
			get { return mainForm.calorieRadioButton.Checked; }
				
		}

		/// <summary>
		/// Gets or Sets the amount of calories in the NumericUpDown in the "manual" tab of the "main" tab.
		/// </summary>
		public static decimal UserSetCalories
		{
				
			set { mainForm.manualCalorieEditBox.Value = value; }
				
		}

		/// <summary>
		/// Gets or Sets the amount of default calories to be applied wherever applicable.
		/// </summary>
		public static decimal DefaultCalories
		{
				
			set { mainForm.defaultCaloriesNumericUpDown.Value = value; }
				
		}

		/// <summary>
		/// Gets or Sets the value for the "new item" checkbox.
		/// </summary>
		public static bool IsCreatingANewFoodItem
		{
				
			get { return mainForm.newItemCheckbox.Checked; }
				
			set { mainForm.newItemCheckbox.Checked = value; }
				
		}

		/// <summary>
		/// Gets the property control box of your choice based on the contolID provided. This is to be used as a LAST RESTORT when ALL ELSE FAILS!
		/// </summary>
		/// <param name="controlID">
		/// The ID of the control item you wish to use.
		/// </param>
		/// <returns>
		/// Returns a property control value based on the following IDs: (ID 0): Food Name, (ID 1): Serving Size, (ID 2): Calories Per Serving, (ID 3): Definer. (ID 4): Calories Label.
		/// </returns>
		public static Control ReturnPropertyControl (int controlID)
		{
				
			switch (controlID)
			{
						
				case 0:
					return mainForm.foodNameEditBox;
						
				case 1:
					return mainForm.servingSizeEditBox;
						
				case 2:
					return mainForm.caloriesPerServingEditBox;
						
				case 3:
					return mainForm.definerEditBox;
						
				case 4:
					return mainForm.caloriesLabel;
						
				default:
					Errors.Handler(new ArgumentOutOfRangeException ("controlID", controlID, "Value must be between " + 0 + " and " + 4), true, true, 524288);
					break;
						
			}
				
			return null;
				
		}

		/// <summary>
		/// Gets or Sets whether the user has chosen to write the diary portion of Food Tracking to a file.
		/// </summary>
		public static bool UserIsWritingDiaryToFile
		{
				
			get { return mainForm.WriteToFileCheckBox.Checked; }
				
		}

		/// <summary>
		/// Gets or Sets whether the user has chosen to use the diary portion of Food Tracking.
		/// </summary>
		public static bool DiaryIsBeingUsed
		{
				
			get { return mainForm.RecordFoodCheckBox.Checked; }
				
			private set { mainForm.RecordFoodCheckBox.Checked = value; }
				
		}
		
		/// <summary>
		/// Sets the minimum value for the manual calorie NumericUpDown in the "manual" sub-tab.
		/// </summary>
		public static decimal ChangeManualCalorieMinimumValue
		{
			
			set { mainForm.manualCalorieEditBox.Minimum = value; }
			
		}
		
		/// <summary>
		/// Gets the "sign" for the arithmetic operation in the "arithmetic" sub-tab.
		/// </summary>
		public static string GetArithmeticSign
		{
			
			get { return mainForm.arithmeticSignComboBox.SelectedItem.ToString(); }
			
		}
		
		/// <summary>
		/// Sets the "sign" for the arithmetic operation in the "arithmetic" sub-tab.
		/// </summary>
		public static int SetArithmeticSign
		{
			
			set { mainForm.arithmeticSignComboBox.SelectedIndex = value; }
			
		}

		
		public static TabPage AddSub_SelectedSubTab
		{
			
			get { return mainForm.AddSub_SubTabControl.SelectedTab; }
			
			set { mainForm.AddSub_SubTabControl.SelectedTab = value; }
			
		}
		
		/// <summary>
		/// Gets the value from one of the NumericUpDowns in the "arithmetic" subtab.
		/// </summary>
		/// <param name="left">
		/// Are you getting the value of the left NumericUpDown?
		/// </param>
		/// <returns>
		/// The decimal value of the NumericUpDown as determined by <paramref name="left"></paramref>.
		/// </returns>
		public static decimal GetArithmaticValue(bool left)
		{
			
			return left ? mainForm.arithmeticNumericUpDown_Left.Value : mainForm.arithmeticNumericUpDown_Right.Value;
			
		}

		#endregion

		#region Main Tab

		#region Search Bar

		private void SearchBarTextChanged (object sender, EventArgs e)
		{
				
			if (!string.IsNullOrWhiteSpace(searchBar.Text) && !searchBar.Text.Equals("Click Here to Search the Food List", StringComparison.CurrentCultureIgnoreCase))
			{
					
				if (!nextSearchButton.Enabled)
				{
						
					nextSearchButton.Enabled = true;
						
				}
					
				Functions.Find(0, searchBar.Text, null, exactSearchCheckBox.Checked, false);
					
				return;
					
			}
			if (nextSearchButton.Enabled)
			{
					
				nextSearchButton.Enabled = false;
					
			}
				
			foodList.ClearSelected();
				
		}

		private void SearchBarFocusGranted (object sender, EventArgs e)
		{
				
			searchBar.Clear();
				
			clearSearchBarButton.Enabled = true;
				
			Font tempfont = new Font ("Times New Roman", 10f);
				
			searchBar.Font = tempfont;
				
		}

		private void ClearSearchBar (object sender, EventArgs e)
		{
				
			if (!nextSearchButton.Focused)
			{
					
				searchBar.Clear();
					
				clearSearchBarButton.Enabled = false;
					
				Modification.WriteToObject(null, searchBar, 2);
					
				nextSearchButton.Enabled = false;
					
			}
				
		}

		private void FindNextSearchItem (object sender, EventArgs e)
		{
				
			if (string.IsNullOrWhiteSpace(searchBar.Text) || searchBar.Text.Equals("Click Here to Search the Food List", StringComparison.CurrentCultureIgnoreCase))
			{
					
				if (nextSearchButton.Enabled)
				{
						
					nextSearchButton.Enabled = false;
						
				}
					
				return;
					
			}
				
			Functions.Find(GlobalVariables.SelectedListItem, searchBar.Text, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, exactSearchCheckBox.Checked, true);
				
		}

		#endregion

		#region Food Listbox

		private void FoodListSelectedIndexChanged (object sender, EventArgs e)
		{
				
			if (foodList.SelectedIndex > -1)
			{
					
				if (newItemCheckbox.Checked)
				{
						
					newItemCheckbox.Checked = false;
						
				}
					
				GlobalVariables.SelectedListItem = foodList.SelectedIndex;
				
				isDrinkCheckBox.Checked = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item5;
					
				howManyServingsLabel.Text = string.Format(CultureInfo.CurrentCulture, "How many {0}s do you plan on {1}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, isDrinkCheckBox.Checked ? "drinking" : "eating");
					
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

		private void FoodListFocusChanged (object sender, EventArgs e)
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

		private void DeleteFoodItemFromTable (object sender, EventArgs e)
		{
			
			if (PopupHandler.CreatePopup("Are you sure you want to delete " + FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1 + "?", foodList, 5, false) == DialogResult.Yes)
			{
					
				Modification.ModifyFoodPropertiesList(true, new string[] {
					null,
					null
				}, new decimal[] {
					0,
					0
				}, new [] {
					false
				});
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
					
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string, bool> (null, 0f, 0f, null, false));
					
				Functions.Refresh_foodList();
					
				foodList.SetSelected(GlobalVariables.SelectedListItem, true);
					
			}
				
		}

		#endregion

		#region Edit Your Calories Tab

		private void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
		{
			if (newItemCheckbox.Checked)
			{
				
				Modification.ModifyFoodPropertiesList(true, new string[] {
					null,
					null
				}, new decimal[] {
					0,
					0
				}, new [] {
					false
				});
				
			}
			else
			{
				
				FoodListSelectedIndexChanged(sender, e);
				
			}
				
			foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
				
		}

		private void ResetCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = (decimal)Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3;
				
		}

		private void ZeroOutCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = 0;
				
		}

		private void ManualSubmitButtonClicked (object sender, EventArgs e)
		{

			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now.AddDays(1), (float)manualCalorieEditBox.Value, Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3, new [] {
				false,
				false
			});
				
			Validation.CheckCurrentRadioButton();
				
		}

		void ChangedTabManualAddSub (object sender, EventArgs e)
		{
				
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			manualCalorieEditBox.Value = (decimal)registryTuple.Item2;
				
		}

		#endregion

		private void TimeOrCaloriesChangedWithoutEvent (object sender, EventArgs e)
		{
				
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
				
			Validation.CheckCurrentRadioButton();
				
		}

		private void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
		{
				
			string oldtext = foodNameEditBox.Text;
				
			Modification.ModifyFoodItemProperty(Validation.EditBoxesHaveValidEntries());
				
			Functions.Find(0, oldtext, null, true, false);
				
		}

		private void ModifyCalories (object sender, EventArgs e)
		{
			
			Modification.ModifyCalories(sender);
			
		}

		#endregion

		#region Additional Options Tab

		void ResetCaloriesSpecificCheckBoxCheckStatusChanged (object sender, EventArgs e)
		{
			
			exactResetDatetimePicker.Enabled = resetCaloriesManualCheckBox.Checked;
			
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now, registryTuple.Item2, registryTuple.Item3, new List<bool> {
				
					false,
				false});
			
		}

		void ExactResetDatetimePickerValueChanged (object sender, EventArgs e)
		{
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			if (resetCaloriesManualCheckBox.Checked && exactResetDatetimePicker.Value != registryTuple.Item1)
			{
				
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, exactResetDatetimePicker.Value, registryTuple.Item2, registryTuple.Item3, new [] {
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
		
		internal static void InitializeForms ()
		{
			
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			MainForm.MainFormTitle = GlobalVariables.RegistryMainValue;
			
			Refresh_foodList();
			
			MainForm.FoodListGetOrSetSelected(true, MainForm.GetFoodListTopItem, true);
			
			GlobalVariables.SelectedListItem = 0;
			
			MainForm.NumberOfServingsLabel = string.Format(CultureInfo.CurrentCulture, "How many {0}s do you plan on {1}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item5 ? "drinking" : "eating");
			
			
			Modification.ModifyFoodPropertiesList(false, new []{MainForm.MainFoodListItems [MainForm.GetFoodListTopItem].ToString(), FoodRelated.CombinedFoodList [0].Item4}, new []{(decimal)FoodRelated.CombinedFoodList [0].Item2, (decimal)FoodRelated.CombinedFoodList [0].Item3}, new []{FoodRelated.CombinedFoodList[0].Item5});
			
			Validation.CheckCurrentRadioButton();
			
			MainForm.ManualTimeIsInitiated = registryTuple.Item4;
			
			MainForm.ManualDateTime = registryTuple.Item1;
			
			MainForm.ChangeManualCalorieMinimumValue = (decimal)-registryTuple.Item3;
			
			MainForm.UserSetCalories = (decimal)registryTuple.Item2;
			
			MainForm.DefaultCalories = (decimal)registryTuple.Item3;
			
			MainForm.SetArithmeticSign = 0;
			
		}

		public static void Refresh_foodList ()
		{
			FoodRelated.CombinedFoodList.Clear();
			
			Storage.ReadFoodTable("Files\\Text\\", "food.table");
			
			MainForm.MainFoodListDataSource = null;
			
			MainForm.MainFoodListItems.Clear();
			
			List<string> Item1 = new List<string> ();
			
			for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
			{
				Tuple<string, float, float, string, bool> name = FoodRelated.CombinedFoodList [i];
				
				Item1.Add(name.Item1);
				
			}
			
			Item1.Sort();
			
			MainForm.MainFoodListDataSource = Item1;
			
			Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string, bool> (null, 0f, 0f, null, false));
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
		/// <param name="next">
		/// Was the "next" button pressed?
		/// </param>
		#endregion
		internal void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, bool next)
		{
			for (int i = offset; i <= MainForm.MainFoodListItems.Count; i++)
			{
				
				if (i < MainForm.MainFoodListItems.Count)
				{
					if (exactSearch)
					{
						
						if (MainForm.MainFoodListItems [i].ToString().Equals(stringToFind, StringComparison.CurrentCultureIgnoreCase) && !MainForm.MainFoodListItems [i].ToString().Equals(stringToAvoid, StringComparison.CurrentCultureIgnoreCase))
						{
							
							MainForm.FoodListSelected = i;
							
							return;
							
						}
						
					}
					else
					{
					
						if (MainForm.MainFoodListItems [i].ToString().Contains(stringToFind, StringComparison.CurrentCultureIgnoreCase) && !MainForm.MainFoodListItems [i].ToString().Equals(stringToAvoid, StringComparison.CurrentCultureIgnoreCase))
						{
							
							MainForm.FoodListSelected = i;
							
							return;
							
						}
						
					}
					
				}
				
			}
			
			if(next && offset > 0)
			{
				
				Find(0, stringToFind, stringToAvoid, exactSearch, next);
				
			}
			
			return;
		}
		
	}
	
	
}
