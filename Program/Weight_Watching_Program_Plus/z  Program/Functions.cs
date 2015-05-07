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
		
		public void InitializeForms (IModification modification, IStorage store, IValidation valid)
		{
			
			Modification Modification = (modification as Modification);
			
			Storage Storage = (store as Storage);
			
			Validation Validation = (valid as Validation);
			
			Storage.ReadRegistry(valid);
			
			var registryTuple = Storage.GetRetrievableRegistryValues(valid);
			
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			
			string[] versionString = {
				
				string.Format(CultureInfo.InvariantCulture, "{0}, ", fvi.ProductMajorPart <= 5 ? string.Format(CultureInfo.InvariantCulture, "Alpha {0}", fvi.ProductMajorPart) : fvi.ProductMajorPart <= 10 ? string.Format(CultureInfo.InvariantCulture, "Beta {0}", fvi.ProductMajorPart - 5) : string.Format(CultureInfo.InvariantCulture, "Release {0}", fvi.ProductMajorPart - 9)),
				
				fvi.ProductMinorPart < 100 ? string.Format(CultureInfo.InvariantCulture, "{0} Minor, ", FindSymbol(fvi.ProductMinorPart)) : string.Format(CultureInfo.InvariantCulture, "Stable Minor {0}, ", fvi.ProductMinorPart - 99),
				
				fvi.ProductBuildPart < 100 ? string.Format(CultureInfo.InvariantCulture, "{0} Build, ", FindSymbol(fvi.ProductBuildPart)) : string.Format(CultureInfo.InvariantCulture, "Stable Build {0}", fvi.ProductBuildPart - 99),
				
			};
			
			MainForm.MainFormVersionInfoText = string.Format(CultureInfo.InstalledUICulture, "Program Version: {0}.{1}.{2} ({3}{4}{5}) [Rev: {6}]", fvi.ProductMajorPart, fvi.ProductMinorPart, fvi.ProductBuildPart, versionString [0], versionString [1], versionString [2], fvi.ProductPrivatePart);
			
			MainForm.MainFormBuildInfoText = string.Format(CultureInfo.InstalledUICulture, "{0} Build", GlobalVariables.RegistryMainValue.Contains("debug", StringComparison.OrdinalIgnoreCase) ? "Development" : "Release");
			
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
			
			Validation.CheckCurrentRadioButton(modification);
			
			MainForm.ManualTimeIsInitiated = registryTuple.Item4;
			
			MainForm.ManualDateTime = registryTuple.Item1;
			
			MainForm.UserSetCalories = (decimal)registryTuple.Item2;
			
			MainForm.DefaultCalories = (decimal)registryTuple.Item3;
			
			MainForm.SetArithmeticSign = 0;
			
		}
		
		private static string FindSymbol(int numberToCheck)
		{
			
			return numberToCheck <= 5 ? "α" : numberToCheck <= 10 ? "β" : numberToCheck <= 20 ? "Δ" : numberToCheck <= 30 ? "ζ" : numberToCheck <= 40 ? "η" : numberToCheck <= 50 ? "Θ" : numberToCheck <= 60 ? "Λ" : numberToCheck <= 70 ? "Σ" : "Ω";
			
		}
		
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

		public void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, bool next, IPopup popUp)
		{
			
			PopupHandler PopupHandler = (popUp as PopupHandler);
			
			foreach (var foodItem in MainForm.MainFoodListItems.OfType<string>().Where(searchResult => ((!exactSearch ? searchResult.Contains(stringToFind, StringComparison.OrdinalIgnoreCase) : searchResult.Equals(stringToFind, StringComparison.OrdinalIgnoreCase)) && !searchResult.Equals(stringToAvoid, StringComparison.OrdinalIgnoreCase) && (MainForm.MainFoodListItems.IndexOf(searchResult) > offset || (MainForm.MainFoodListItems.IndexOf(searchResult) == 0 && offset == 0 && !next) && MainForm.MainFoodListItems.IndexOf(searchResult) != -1))).Select(searchResult => MainForm.MainFoodListItems.IndexOf(searchResult)))
			{
				
				MainForm.FoodListSelected = foodItem;
				
				return;
				
			}
			
			if (next && offset > 0)
			{
				
				Find(0, stringToFind, stringToAvoid, exactSearch, false, popUp);
				
			}
			else if (!exactSearch)
			{
				
				PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The term {0} could not be found in the food database!", stringToFind), 8);	
				
			}
			
			return;
			
		}
		
	}
}


