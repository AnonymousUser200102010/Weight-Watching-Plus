#region Using Directives

using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using UniversalHandlersLibrary;
#endregion

namespace WeightWatchingProgramPlus
{
	/// <summary>
	/// Functions whose primary purpose is verification and validation, but who don't have a more pressing primary function.
	/// </summary>
	class Validation
	{

		internal bool ManualTimeEngaged;

		#region Check Date Summary
		/// <summary>
		/// Checks to see if the reset date is earlier or later than the checked date.
		/// </summary>
		/// <param name="dateToCheck">
		/// Checked date.
		/// </param>
		#endregion
		internal void CheckDateValidity(DateTime dateToCheck)
		{
			
			if (DateTime.Compare(!ManualTimeEngaged ? dateToCheck : GlobalVariables.ExactResetDateTimePicker.Value, DateTime.Now) <= 0 || Registry.LocalMachine.OpenSubKey(GlobalVariables.RegistryAppendedValue + GlobalVariables.RegistryMainValue) == null)
			{
				
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, !ManualTimeEngaged ? DateTime.Now.AddDays(1) : GlobalVariables.ExactResetDateTimePicker.Value.AddDays(1), Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2, Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3, new[] {
					true,
					true
				});
				
				GlobalVariables.ExactResetDateTimePicker.Value = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1;
				
			}
		}

		#region Check Radio Button Summary
		/// <summary>
		/// Checks to see which radio button is currently active.
		/// </summary>
		/// <param name="timeRadioButton">
		/// The radio button labeled "Time" on the program form.
		/// </param>
		/// <param name="calorieRadioButton">
		/// The radio button labeled "Calories" on the program form.
		/// </param>
		/// <param name="caloriesLabel">
		/// The label at the top of the "Main" tab.
		/// </param>
		#endregion
		internal static void CheckCurrentRadioButton(RadioButton timeRadioButton, RadioButton calorieRadioButton, Label caloriesLabel)
		{
			
			if (timeRadioButton.Checked)
			{
				
				Modification.WriteToObject(caloriesLabel, null, 1);
				
			}
			else if (calorieRadioButton.Checked)
			{
				
				Modification.WriteToObject(caloriesLabel, null, 0);
				
			}
			else
			{
				
				Errors.Handler(new InvalidOperationException("CheckCurrentRadioButton: operation invalid; perameters cannot be parsed into a logical operation."), true, true, 524288);
				
			}
				
		}

		#region Edit Box Validity Summary
		/// <summary>
		/// Checks to see if the food item property setting boxes have values that are within acceptable parameters.
		/// </summary>
		/// <param name="foodNameEditBox">
		/// The edit box for the name of the food.
		/// </param>
		/// <param name="servingSizeEditBox">
		/// The NumericUpDown for the serving size value of the food.
		/// </param>
		/// <param name="caloriesPerServingEditBox">
		/// The NumericUpDown for the calories per serving value of the food.
		/// </param>
		/// <param name="definerEditBox">
		/// The edit box for the definer value of the food.
		/// </param>
		/// <param name="newItemCheckbox">
		/// The check box which defines if an item is being added or not.
		/// </param>
		/// <param name="PopupHandler">
		/// Popuphandler.cs
		/// </param>
		/// <returns>
		/// True if the name box is not an exact duplicate of another food name, all TextBoxes are not void or white space, and all NumericUpDowns have values >= 0, else returns false.
		/// </returns>
		#endregion
		internal static bool EditBoxesHaveValidEntries(TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox, PopupHandler PopupHandler)
		{
			
			if (AlreadyExists(foodNameEditBox.Text, newItemCheckbox))
			{
				
				PopupHandler.CreatePopup("Your food name cannot be the exact same as another food item!", foodNameEditBox, 4, true);
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(foodNameEditBox.Text))
			{
				
				PopupHandler.CreatePopup("Please set a food name value!", foodNameEditBox, 0, true);
				
				return false;
				
			}
			
			if (servingSizeEditBox.Value <= 0)
			{
				
				PopupHandler.CreatePopup("Please set a serving size value!", servingSizeEditBox, 0, true);
				
				return false;
				
			}
			
			if (caloriesPerServingEditBox.Value <= 0)
			{
				
				PopupHandler.CreatePopup("Please set a calories per serving value!", caloriesPerServingEditBox, 0, true);
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(definerEditBox.Text))
			{
				
				PopupHandler.CreatePopup("Please set a definer value!", definerEditBox, 0, true);
				
				return false;
				
			}
			
			return true;
			
		}

		#region Check Exist Status Summary
		/// <summary>
		/// Checks to see if the string that has been supplied is equal to any string in the food list names database.
		/// </summary>
		/// <param name="text">
		/// String to check.
		/// </param>
		/// <param name="newItemCheckBox">
		/// The check box which defines if an item is being added or not.
		/// </param>
		/// <returns>
		/// Checks the supplied string against all names in the food list database and if it equals one or more, returns true. Otherwise false.
		/// </returns>
		#endregion
		private static bool AlreadyExists(string text, CheckBox newItemCheckBox)
		{
			
			for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
			{
				Tuple<string, float, float, string> t = FoodRelated.CombinedFoodList[i];
				
				if (text.Equals(t.Item1, StringComparison.OrdinalIgnoreCase))
				{
					
					if (i != GlobalVariables.SelectedListItem || newItemCheckBox.Checked)
					{
						
						return true;
						
					}
					
				}
			}
			return false;
		}
		
	}
}


