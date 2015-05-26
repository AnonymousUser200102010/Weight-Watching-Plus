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
		
		public void InitializeForms (IModification modification, IStorage store, IValidation valid, IRetrieval retrieve, INetOps netOps, IMainForm mainForm)
		{
			
			Modification Modification = (modification as Modification);
			
			Storage Storage = (store as Storage);
			
			Validation Validation = (valid as Validation);
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			NetworkOps NetworkOps = (netOps as NetworkOps);
			
			MainForm MainForm = (mainForm as MainForm);
			
			MainForm.SetSyncConnectionItems();
			
			if(!Validation.RegistryValueDoesNotExist(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, "sync enabled", retrieve) || !bool.Parse(Retrieval.GetRegistryValue("sync enabled")))
			{
				
				NetworkOps.StartListen(int.Parse(MainForm.SyncListenPort, CultureInfo.InvariantCulture));
				
			}
			
			Retrieval.ReadRegistry();
			
			var registryTuple = Tuple.Create(DateTime.Parse(Retrieval.GetRegistryValue("reset date"), CultureInfo.InvariantCulture), decimal.Parse(Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture), decimal.Parse(Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture), bool.Parse(Retrieval.GetRegistryValue("manual time enabled")), int.Parse(Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture));
			
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			
			string[] versionString = {
				
				string.Format(CultureInfo.InvariantCulture, "{0}, ", fvi.ProductMajorPart <= 5 ? string.Format(CultureInfo.InvariantCulture, "Alpha {0}", fvi.ProductMajorPart) : fvi.ProductMajorPart <= 10 ? string.Format(CultureInfo.InvariantCulture, "Beta {0}", fvi.ProductMajorPart - 5) : string.Format(CultureInfo.InvariantCulture, "Release {0}", fvi.ProductMajorPart - 9)),
				
				fvi.ProductMinorPart < 100 ? string.Format(CultureInfo.InvariantCulture, "{0} Minor, ", FindSymbol(fvi.ProductMinorPart)) : string.Format(CultureInfo.InvariantCulture, "Stable Minor {0}, ", fvi.ProductMinorPart - 99),
				
				fvi.ProductBuildPart < 100 ? string.Format(CultureInfo.InvariantCulture, "{0} Build", FindSymbol(fvi.ProductBuildPart)) : string.Format(CultureInfo.InvariantCulture, "Stable Build {0}", fvi.ProductBuildPart - 99),
				
			};
			
			MainForm.MainFormVersionInfoText(string.Format(CultureInfo.InstalledUICulture, "Program Version: {0}.{1}.{2} ({3}{4}{5}) [Rev: {6}]", fvi.ProductMajorPart, fvi.ProductMinorPart, fvi.ProductBuildPart, versionString [0], versionString [1], versionString [2], fvi.ProductPrivatePart));
			
			MainForm.MainFormBuildInfoText(string.Format(CultureInfo.InstalledUICulture, "{0} Build", GlobalVariables.RegistryMainValue.Contains("de", StringComparison.OrdinalIgnoreCase) ? "Development" : "Release"));
			
			RefreshFoodList(store, retrieve, mainForm);
			
			MainForm.FoodListSelectedIndex(true, MainForm.GetFoodListTopItem, true);
			
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
			
			MainForm.SetAllDecimalPointValues(registryTuple.Item5);
			
			Validation.CheckCurrentRadioButton(modification);
			
			MainForm.ManualTimeIsInitiated = registryTuple.Item4;
			
			MainForm.ManualDateTime = registryTuple.Item1;
			
			MainForm.UserSetCalories(registryTuple.Item2);
			
			MainForm.DefaultCalories = registryTuple.Item3;
			
			MainForm.SetArithmeticSign = 0;
			
		}
		
		private static string FindSymbol(int numberToCheck)
		{
			
			return numberToCheck <= 5 ? "α" : numberToCheck <= 10 ? "β" : numberToCheck <= 20 ? "Δ" : numberToCheck <= 30 ? "ζ" : numberToCheck <= 40 ? "η" : numberToCheck <= 50 ? "Θ" : numberToCheck <= 60 ? "Λ" : numberToCheck <= 70 ? "Σ" : "Ω";
			
		}
		
		public void RefreshFoodList (IStorage store, IRetrieval retrieve, IMainForm mainForm)
		{
			
			MainForm MainForm = (mainForm as MainForm);
			
			Storage Storage = (store as Storage);
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			FoodRelated.CombinedFoodList.Clear();
			
			Retrieval.ReadFoodTable();
			
			MainForm.MainFoodListDataSource(null);
			
			MainForm.MainFoodListItems.Clear();
			
			MainForm.MainFoodListDataSource(new List<string> (FoodRelated.CombinedFoodList.Select(item1 => item1.Item1)));
			
			Storage.WriteFoodTable();
			
		}

		public void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, bool next, IPopup popUp, IMainForm mainForm)
		{
			
			PopupHandler PopupHandler = (popUp as PopupHandler);
			
			MainForm MainForm = (mainForm as MainForm);
			
			foreach (var foodItem in MainForm.MainFoodListItems.OfType<string>().Where(searchResult => ((!exactSearch ? searchResult.Contains(stringToFind, StringComparison.OrdinalIgnoreCase) : searchResult.Equals(stringToFind, StringComparison.OrdinalIgnoreCase)) && !searchResult.Equals(stringToAvoid, StringComparison.OrdinalIgnoreCase) && (MainForm.MainFoodListItems.IndexOf(searchResult) > offset || (MainForm.MainFoodListItems.IndexOf(searchResult) == 0 && offset == 0 && !next) && MainForm.MainFoodListItems.IndexOf(searchResult) != -1))).Select(searchResult => MainForm.MainFoodListItems.IndexOf(searchResult)))
			{
				
				MainForm.FoodListSelectedIndex(true , foodItem, true);
				
				return;
				
			}
			
			if (next && offset > 0)
			{
				
				Find(0, stringToFind, stringToAvoid, exactSearch, false, popUp, mainForm);
				
			}
			else if (!exactSearch)
			{
				
				PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The term {0} could not be found in the food database!", stringToFind), 8);	
				
			}
			
			return;
			
		}
		
	}
}


