#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UniversalHandlersLibrary;

#endregion

namespace WeightWatchingProgramPlus
{
	
	/// <summary>
	/// Misc general functions
	/// </summary>
	internal class Functions : IGeneralFunctions
	{
		
		/// <summary>
		/// Main initialization for the program as handled by the developer (and not, say, through windows forms or what have you).
		/// </summary>
		public void InitializeForms (IModification modification, IStorage store, IValidation valid)
		{
			
			Modification Modification = (modification as Modification);
			
			Storage Storage = (store as Storage);
			
			Validation Validation = (valid as Validation);
			
			Storage.ReadRegistry(valid);
			
			var registryTuple = Storage.GetRetrievableRegistryValues(valid);
			
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			
			string[] versionString = new string[3];
			
			if (fvi.ProductMajorPart < 10)
			{
				
				versionString [0] = string.Format(CultureInfo.InvariantCulture, "({0}", fvi.ProductMajorPart <= 5 ? "Alpha" : "Beta");
				
			}
			else if (fvi.ProductMinorPart < 100 || fvi.ProductBuildPart < 100)
			{
				
				versionString [0] = string.Format(CultureInfo.InvariantCulture, "(Release {0}", fvi.ProductMajorPart - 9);
				
			}
			
			if (fvi.ProductMajorPart > 0)
			{
				
				if (fvi.ProductMinorPart < 100)
				{
					
					versionString [1] = string.Format(CultureInfo.InvariantCulture, ".{0}", fvi.ProductMinorPart <= 10 ? "α" : fvi.ProductMinorPart <= 20 ? "β" : fvi.ProductMinorPart <= 30 ? "Δ" : fvi.ProductMinorPart <= 40 ? "ζ" : fvi.ProductMinorPart <= 50 ? "η" : fvi.ProductMinorPart <= 60 ? "Θ" : fvi.ProductMinorPart <= 70 ? "Λ" : fvi.ProductMinorPart <= 80 ? "Σ" : "Ω");
					
				}
				else if (fvi.ProductBuildPart < 100)
				{
					
					versionString [1] = string.Format(CultureInfo.InvariantCulture, "Stable Revision {0}.", fvi.ProductMinorPart - 99);
					
				}
				
				if (fvi.ProductBuildPart < 100 && fvi.ProductBuildPart > 0)
				{
					
					versionString [2] = string.Format(CultureInfo.InvariantCulture, ".{0})", fvi.ProductBuildPart <= 10 ? "α" : fvi.ProductBuildPart <= 20 ? "β" : fvi.ProductBuildPart <= 30 ? "Δ" : fvi.ProductBuildPart <= 40 ? "ζ" : fvi.ProductBuildPart <= 50 ? "η" : fvi.ProductBuildPart <= 60 ? "Θ" : fvi.ProductBuildPart <= 70 ? "Λ" : fvi.ProductBuildPart <= 80 ? "Σ" : "Ω");
					
				}
				else if (fvi.ProductMinorPart < 100)
				{
					
					versionString [0] = "Stable Build)";
					
				}
				
			}
			else
			{
				
				versionString [2] = ")";
				
			}
			
			MainForm.MainFormVersionInfoText = string.Format(CultureInfo.InstalledUICulture, "Program Version: {0}.{1}.{2} {3}{4}{5} [Rev: {6}] \t{7} Build", fvi.ProductMajorPart, fvi.ProductMinorPart, fvi.ProductBuildPart, versionString [0], versionString [1], versionString [2], fvi.ProductPrivatePart, GlobalVariables.RegistryMainValue.Contains("debug", StringComparison.OrdinalIgnoreCase) ? "Development" : "Release");
			
			RefreshFoodList(store);
			
			MainForm.FoodListGetOrSetSelected(true, MainForm.GetFoodListTopItem, true);
			
			GlobalVariables.SelectedListItem = 0;
			
			Modification.ModifyFoodPropertiesList(new[] {
				MainForm.MainFoodListItems [MainForm.GetFoodListTopItem].ToString(),
				FoodRelated.CombinedFoodList [0].Item4
			}, new[] {
				(decimal)FoodRelated.CombinedFoodList [0].Item2,
				(decimal)FoodRelated.CombinedFoodList [0].Item3
			}, new[] {
				FoodRelated.CombinedFoodList [0].Item5
			});
			
			MainForm.SetAllDecimalPointValues = registryTuple.Item6;
			
			MainForm.DecimalExample = registryTuple.Item2.ToString(CultureInfo.CurrentCulture);
			
			Validation.CheckCurrentRadioButton(modification);
			
			MainForm.ManualTimeIsInitiated = registryTuple.Item4;
			
			MainForm.ManualDateTime = registryTuple.Item1;
			
			MainForm.ChangeManualCalorieMinimumValue = (decimal)-registryTuple.Item3;
			
			MainForm.UserSetCalories = (decimal)registryTuple.Item2;
			
			MainForm.DefaultCalories = (decimal)registryTuple.Item3;
			
			MainForm.SetArithmeticSign = 0;
			
		}

		/// <summary>
		/// Clears and reloads the food table into the food listbox
		/// </summary>
		public void RefreshFoodList (IStorage store)
		{
			
			Storage Storage = (store as Storage);
			
			FoodRelated.CombinedFoodList.Clear();
			
			Storage.ReadFoodTable();
			
			MainForm.MainFoodListDataSource = null;
			
			MainForm.MainFoodListItems.Clear();
			
			MainForm.MainFoodListDataSource = new List<string> (FoodRelated.CombinedFoodList.Select(item1 => item1.Item1));
			
			Storage.WriteFoodTable();
			
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
		public void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, bool next)
		{
			foreach (var foodItem in MainForm.MainFoodListItems.OfType<string>().Where(searchResult => ((!exactSearch ? searchResult.Contains(stringToFind, StringComparison.OrdinalIgnoreCase) : searchResult.Equals(stringToFind, StringComparison.OrdinalIgnoreCase)) && !searchResult.Equals(stringToAvoid, StringComparison.OrdinalIgnoreCase) && (MainForm.MainFoodListItems.IndexOf(searchResult) > offset || (MainForm.MainFoodListItems.IndexOf(searchResult) == 0 && offset == 0 && !next) && MainForm.MainFoodListItems.IndexOf(searchResult) != -1))).Select(searchResult => MainForm.MainFoodListItems.IndexOf(searchResult)))
			{
				
				MainForm.FoodListSelected = foodItem;
				
				return;
				
			}
			
			if (next && offset > 0)
			{
				
				Find(0, stringToFind, stringToAvoid, exactSearch, false);
				
			}
			
			return;
			
		}
		
	}
}


