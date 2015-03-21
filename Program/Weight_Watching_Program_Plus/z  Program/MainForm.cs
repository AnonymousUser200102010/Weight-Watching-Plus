#region Using Directives

using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
		
		private IGeneralFunctions Functions = new Functions ();
		
		private IPopup PopupHandler = new PopupHandler ();
		
		private IValidation Validation;
		
		private IStorage Storage;
		
		private IMathematics Mathematics;
		
		private IModification Modification;

		private static MainForm mainForm;

		internal MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			
			this.Storage = new Storage ((this.PopupHandler as PopupHandler));
			
			this.Validation = new Validation ((this.Storage as Storage), (this.PopupHandler as PopupHandler));
			
			this.Mathematics = new Mathematics ((this.Storage as Storage), (this.PopupHandler as PopupHandler));
			
			this.Modification = new Modification ((this.PopupHandler as PopupHandler), (this.Validation as Validation), (this.Storage as Storage), (this.Mathematics as Mathematics), (this.Functions as Functions));
			mainForm = (this as MainForm);

			this.Functions.InitializeForms(this.Modification, this.Storage, this.Validation);
			
			//
		}

		#region properties

		public static string MainFormVersionInfoText
		{
			
			set { mainForm.productVersionInfoBar.Text = value; }
			
		}

		/// <summary>
		/// Gets or Sets the title of the application after launch.
		/// </summary>
		public static string MainFormTitle
		{
			
			get { return mainForm.Text; }
			
			set { mainForm.Text = value; }
				
		}

		/// <summary>
		/// Gets or Sets the value in the #Servings NumericUpDown.
		/// </summary>
		public static decimal UserProvidedServings
		{
				
			get
			{
				
				IMathematics math = (mainForm.Mathematics as Mathematics);
				
				return AddSub_SelectedSubTab.Text.Contains("explicit", StringComparison.OrdinalIgnoreCase) ? mainForm.userServingInputTextBox.Value : (decimal)(math.PerformArithmeticOperation(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", MainForm.GetArithmaticValue(true), MainForm.GetArithmeticSign, MainForm.GetArithmaticValue(false))));
				
			}
				
			set
			{ 
				
				mainForm.userServingInputTextBox.Value = value;
				
				mainForm.arithmeticNumericUpDown_Left.Value = value;
				
				mainForm.arithmeticNumericUpDown_Right.Value = value;
			
			}
				
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
				
			set
			{ 
				
				mainForm.foodList.SelectedIndex = value;
				
				GlobalVariables.SelectedListItem = value;
				
				mainForm.howManyServingsLabel.Text = string.Format(CultureInfo.CurrentCulture, "How many {0}s do you plan on {1}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item5 ? "drinking" : "eating");
				
				#if DEBUG
				
				Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Food : {0}\nGlobal Vars SI: {1}\nFood Name: {2}", mainForm.foodList.SelectedIndex, GlobalVariables.SelectedListItem, FoodRelated.CombinedFoodList [mainForm.foodList.SelectedIndex].Item1));
				
				#endif
				
			}
				
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

		/// <summary>
		/// Gets or Sets the "Is Drink" checkbox button which indicates what food items are drinks and which are food.
		/// </summary>
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
		/// The NumericUpDown which shows, and sets, the decimal places to be used globally.
		/// </summary>
		public static decimal DecimalPlaces
		{
			
			get { return mainForm.decimalPlacesNumericUpDown.Value; }
			
			set { mainForm.decimalPlacesNumericUpDown.Value = value; }
			
		}
		
		/// <summary>
		/// Sets the decimal example text box.
		/// </summary>
		public static string DecimalExample
		{
			
			set { mainForm.exampleNumDecPlaceTextBox.Text = value; }
			
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
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Thrown if the ID provided does not correlate (is less or more) to any of the controls that have been explicitly exposed.
		/// </exception>
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
						
			}
				
			throw new ArgumentOutOfRangeException ("controlID", controlID, "Value must be between " + 0 + " and " + 4);
				
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
		public static decimal GetArithmaticValue (bool left)
		{
			
			return left ? mainForm.arithmeticNumericUpDown_Left.Value : mainForm.arithmeticNumericUpDown_Right.Value;
			
		}
		
		public static int SetAllDecimalPointValues
		{
			
			set 
			{ 
				
				string decimalPlacesLiteral = ".";
				
				for (int i = 1; i < value; i++)
				{
					
					decimalPlacesLiteral += "0";
					
				}
				
				decimalPlacesLiteral += "1";
				
				double decimalPlacesDouble = 0;
				
				if(!double.TryParse(decimalPlacesLiteral, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalPlacesDouble))
				{
					
					throw Errors.PremadeExceptions("SetAllDecimalPointValues", "decimalPlacesLiteral", 0);
					
				}
			
				mainForm.arithmeticNumericUpDown_Left.DecimalPlaces = value;
				mainForm.arithmeticNumericUpDown_Left.Minimum = (decimal)decimalPlacesDouble;
				
				mainForm.defaultCaloriesNumericUpDown.DecimalPlaces = value;
				mainForm.defaultCaloriesNumericUpDown.Minimum = (decimal)decimalPlacesDouble;
				
				mainForm.userServingInputTextBox.DecimalPlaces = value;
				mainForm.userServingInputTextBox.Minimum = (decimal)decimalPlacesDouble;
				
				mainForm.caloriesPerServingEditBox.DecimalPlaces = value;
				mainForm.caloriesPerServingEditBox.Minimum = (decimal)decimalPlacesDouble;
				
				mainForm.manualCalorieEditBox.DecimalPlaces = value;
				mainForm.manualCalorieEditBox.Minimum = -(decimal.Parse(string.Format(CultureInfo.InvariantCulture, "{0}{1}", mainForm.Storage.GetRetrievableRegistryValues(mainForm.Validation).Item3, decimalPlacesLiteral), CultureInfo.CurrentCulture));
				
				if(value <= 4)
				{
					mainForm.servingSizeEditBox.DecimalPlaces = value;
					mainForm.servingSizeEditBox.Minimum = (decimal)decimalPlacesDouble;
				}
				else if (value > 0)
				{
					
					mainForm.servingSizeEditBox.DecimalPlaces = 4;
					mainForm.servingSizeEditBox.Minimum = (decimal)(.0001);
					
				}
				else
				{
					
					throw new InvalidCastException("Value for SetAllDecimalPointValues is out of bounds!");
					
				}
				
			}
			
		}
		
		
		public static decimal ServingSizeMinimumValue
		{
			
			get { return mainForm.servingSizeEditBox.Minimum; }
			
			set { mainForm.servingSizeEditBox.Value = value; }
			
		}
		
		public static decimal CaloriesPerServingMinimumValue
		{
			
			get { return mainForm.caloriesPerServingEditBox.Minimum; }
			
			set { mainForm.caloriesPerServingEditBox.Value = value; }
			
		}

		#endregion

		#region Main Tab
		
		void MainTabSelectedIndexChanged(object sender, EventArgs e)
		{
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			exampleNumDecPlaceTextBox.Text = Math.Round(registryTuple.Item2, registryTuple.Item6).ToString(CultureInfo.CurrentCulture);
			
			decimalPlacesNumericUpDown.Value = registryTuple.Item6;
			
			defaultCaloriesNumericUpDown.Value = (decimal)registryTuple.Item3;
			
		}

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
				
			searchBar.Font = new Font ("Times New Roman", 10f);
				
		}

		private void ClearSearchBar (object sender, EventArgs e)
		{
				
			if (!nextSearchButton.Focused)
			{
					
				searchBar.Clear();
					
				clearSearchBarButton.Enabled = false;
					
				this.Modification.WriteToObject(searchBar, 2);
					
				nextSearchButton.Enabled = false;
				
				foodList.SetSelected(GlobalVariables.SelectedListItem, true);
					
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
				
			this.Functions.Find(GlobalVariables.SelectedListItem, searchBar.Text, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, exactSearchCheckBox.Checked, true);
				
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
					
				howManyServingsLabel.Text = string.Format(CultureInfo.CurrentCulture, "How many {0}s do you plan on {1}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item5 ? "drinking" : "eating");
					
				FoodNameProperty = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item1;
					
				ServingSizeProperty = (decimal)FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item2;
					
				CaloriesPerServingProperty = (decimal)FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item3;
				
				DefinerProperty = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item4;
				
				isDrinkCheckBox.Checked = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item5;
				
				#if DEBUG
				
				Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Food : {0}\nGlobal Vars SI: {1}\nFood Name: {2}", mainForm.foodList.SelectedIndex, GlobalVariables.SelectedListItem, FoodRelated.CombinedFoodList [mainForm.foodList.SelectedIndex].Item1));
				
				#endif
					
			}
				
		}

		private void FoodListFocusChanged (object sender, EventArgs e)
		{
				
			if (newItemCheckbox.Checked)
			{
					
				newItemCheckbox.Checked = false;
					
			}
				
			if (foodList.Focused && foodList.SelectedIndex > -1)
			{
				
				GlobalVariables.SelectedListItem = foodList.SelectedIndex;
					
			}
			else
			{
					
				foodList.ClearSelected();
					
			}
				
		}

		private void DeleteFoodItemFromTable (object sender, EventArgs e)
		{
			
			if (this.PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "Are you sure you want to delete {0}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1), 5) == DialogResult.Yes)
			{
				
				int previouslySelectedIndex = GlobalVariables.SelectedListItem;
				
				this.Modification.ModifyFoodPropertiesList();
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
					
				this.Storage.WriteFoodTable();
					
				Functions.RefreshFoodList(this.Storage);
				
				Console.WriteLine(FoodRelated.CombinedFoodList [previouslySelectedIndex].Item1);
					
				foodList.SetSelected(previouslySelectedIndex, true);
			}
				
		}

		#endregion

		#region Edit Your Calories Tab

		private void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
		{
			
			if (newItemCheckbox.Checked)
			{
				
				this.Modification.ModifyFoodPropertiesList();
				
			}
			else
			{
				
				foodList.SetSelected(GlobalVariables.SelectedListItem, true);
				
			}
				
			foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
				
		}

		private void ResetCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = (decimal)this.Storage.GetRetrievableRegistryValues(this.Validation).Item3;
				
		}

		private void ZeroOutCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = 0;
				
		}

		private void ManualSubmitButtonClicked (object sender, EventArgs e)
		{

			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation);
			
			this.Storage.WriteRegistry((double)manualCalorieEditBox.Value, registryTuple.Item3, registryTuple.Item6, this.Validation);
				
			this.Validation.CheckCurrentRadioButton(this.Modification);
				
		}

		private void ChangedTabManualAddSub (object sender, EventArgs e)
		{
				
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation);
			
			manualCalorieEditBox.Value = (decimal)registryTuple.Item2;
				
		}

		#endregion

		private void TimeOrCaloriesChangedWithoutEvent (object sender, EventArgs e)
		{
				
			this.Storage.ReadRegistry(this.Validation);
				
			this.Validation.CheckCurrentRadioButton(this.Modification);
				
		}

		private void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
		{
				
			string oldtext = foodNameEditBox.Text;
				
			this.Modification.ModifyFoodItemProperty(Validation.EditBoxesHaveValidEntries());
				
			Functions.Find(0, oldtext, null, true, false);
				
		}

		private void ModifyCalories (object sender, EventArgs e)
		{
			
			this.Modification.ModifyCalories(sender);
			
		}

		#endregion

		#region Additional Options Tab

		void ResetCaloriesSpecificCheckBoxCheckStatusChanged (object sender, EventArgs e)
		{
			
			exactResetDatetimePicker.Enabled = resetCaloriesManualCheckBox.Checked;
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			this.Storage.WriteRegistry(registryTuple.Item2, registryTuple.Item3, registryTuple.Item6, this.Validation);
			
		}

		void ExactResetDatetimePickerValueChanged (object sender, EventArgs e)
		{
			
			var registryTuple = Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			if (resetCaloriesManualCheckBox.Checked && exactResetDatetimePicker.Value != registryTuple.Item1)
			{
				
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, exactResetDatetimePicker.Value, registryTuple.Item2, registryTuple.Item3, registryTuple.Item6, new [] {
					false,
					true
				}, this.Validation);
				
			}
			
		}

		void DefaultCaloriesSetButtonClick (object sender, EventArgs e)
		{
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			this.Storage.WriteRegistry(registryTuple.Item2, (double)defaultCaloriesNumericUpDown.Value, registryTuple.Item6, this.Validation);
			
		}

		void LicenseInfoButtonClick (object sender, EventArgs e)
		{
			
			this.PopupHandler.CreatePopup(string.Format(CultureInfo.InstalledUICulture, "This program was released under {0}. You may obtain more information on this license from either the license file that was (supposed to be) included with this program, or from any of the sources at the project's GitHub page <https://github.com/AnonymousUser200102010/Weight-Watching-Plus>.", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright), 7);
			 
		}
		
		void SetDecimalPlacesValueButtonClick(object sender, EventArgs e)
		{
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			this.Storage.WriteRegistry(registryTuple.Item2, registryTuple.Item3, (int)this.decimalPlacesNumericUpDown.Value, this.Validation);
			
			registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation);
			
			MainForm.SetAllDecimalPointValues = registryTuple.Item6;
			
			//TimeOrCaloriesChangedWithoutEvent(sender, e);
			
		}
		
		void DecimalPlacesNumericUpDownValueChanged(object sender, EventArgs e)
		{
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			exampleNumDecPlaceTextBox.Text = Math.Round(registryTuple.Item2, (int)decimalPlacesNumericUpDown.Value).ToString(CultureInfo.CurrentCulture);
			
		}
		#endregion
	}

}
