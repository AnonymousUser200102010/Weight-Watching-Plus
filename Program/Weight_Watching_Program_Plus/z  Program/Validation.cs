#region Using Directives

using System;
using System.Linq;
using System.Text.RegularExpressions;
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

		#region Check Date Summary
		/// <summary>
		/// Checks to see if the reset date is earlier or later than the checked date.
		/// </summary>
		#endregion
		internal static void CheckDateValidity()
		{
			
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			if (DateTime.Compare(registryTuple.Item1, DateTime.Now) <= 0 || Registry.LocalMachine.OpenSubKey(GlobalVariables.RegistryAppendedValue + GlobalVariables.RegistryMainValue) == null)
			{
				
				var manualTimeIsInitiated = MainForm.ManualTimeIsInitiated;
				
				DateTime tempDate = manualTimeIsInitiated ? new DateTime(registryTuple.Item1.Year, registryTuple.Item1.Month, DateTime.Now.Day + 1, registryTuple.Item1.Hour, registryTuple.Item1.Minute, registryTuple.Item1.Second, registryTuple.Item1.Millisecond, DateTimeKind.Local) : DateTime.Now.AddDays(1);
				
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, tempDate, registryTuple.Item2, registryTuple.Item3, new[] {
					true,
					true
				});
				
			}
		}

		#region Check Radio Button Summary
		/// <summary>
		/// Checks to see which radio button is currently active.
		/// </summary>
		/// <param name="caloriesLabel">
		/// The label at the top of the "Main" tab.
		/// </param>
		#endregion
		internal static void CheckCurrentRadioButton(Label caloriesLabel)
		{
			
			if (MainForm.UserCheckingTime)
			{
				
				Modification.WriteToObject(caloriesLabel, null, 1);
				
			}
			else if (MainForm.UserCheckingCalories)
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
		/// <returns>
		/// True if the name box is not an exact duplicate of another food name, all TextBoxes are not void or white space and contain valid characters, and all NumericUpDowns have values >= 0; else returns false.
		/// </returns>
		#endregion
		internal static bool EditBoxesHaveValidEntries()
		{
			PopupHandler PopupHandler = new PopupHandler ();
			
			if (HasInvalidCharacters(MainForm.FoodNameProperty) || HasInvalidCharacters(MainForm.DefinerProperty))
			{
				
				PopupHandler.CreatePopup("Properties cannot contain special characters!", MainForm.ReturnPropertyControl(0), 4, true);
				
				return false;
				
			}
			
			if (AlreadyExists(MainForm.FoodNameProperty))
			{
				
				PopupHandler.CreatePopup("Your food name cannot be the exact same as another food item!", MainForm.ReturnPropertyControl(0), 4, true);
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(MainForm.FoodNameProperty))
			{
				
				PopupHandler.CreatePopup("Please set a food name value!", MainForm.ReturnPropertyControl(0), 0, true);
				
				return false;
				
			}
			
			if (MainForm.ServingSizeProperty <= 0)
			{
				
				PopupHandler.CreatePopup("Please set a serving size value!", MainForm.ReturnPropertyControl(1), 0, true);
				
				return false;
				
			}
			
			if (MainForm.CaloriesPerServingProperty <= 0)
			{
				
				PopupHandler.CreatePopup("Please set a calories per serving value!", MainForm.ReturnPropertyControl(2), 0, true);
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(MainForm.DefinerProperty))
			{
				
				PopupHandler.CreatePopup("Please set a definer value!", MainForm.ReturnPropertyControl(3), 0, true);
				
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
		/// <returns>
		/// Checks the supplied string against all names in the food list database and if it equals one or more, returns true. Otherwise false.
		/// </returns>
		#endregion
		private static bool AlreadyExists(string text)
		{
			
			for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
			{
				Tuple<string, float, float, string, bool> t = FoodRelated.CombinedFoodList[i];
				
				if (text.Equals(t.Item1, StringComparison.OrdinalIgnoreCase) && (i != GlobalVariables.SelectedListItem || MainForm.IsCreatingANewFoodItem))
				{
					
					return true;
					
				}
				
			}
			return false;
		}
		
		private static bool HasInvalidCharacters(string text)
		{
			
			Regex RgxUrl = new Regex("[^a-zA-Z0-9 ()%]");
			
			return RgxUrl.IsMatch(text);
			
		}
		
	}
}


