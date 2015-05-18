#region Using Directives

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
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
	internal class Validation : IValidation
	{
		
		private IPopup PopupHandler;
		private IStorage Storage;
		
		public Validation (Storage store, PopupHandler pU)
		{
			
			this.Storage = store;
			this.PopupHandler = pU;
			
		}

		public void CheckDateValidity ()
		{
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this, false);
			
			if (DateTime.Compare(registryTuple.Item1, DateTime.Now) <= 0 || Registry.LocalMachine.OpenSubKey(GlobalVariables.RegistryAppendedValue + GlobalVariables.RegistryMainValue) == null)
			{
				
				var manualTimeIsInitiated = MainForm.ManualTimeIsInitiated;
				
				DateTime tempDate = manualTimeIsInitiated ? new DateTime (registryTuple.Item1.Year, registryTuple.Item1.Month, DateTime.Now.Day + 1, registryTuple.Item1.Hour, registryTuple.Item1.Minute, registryTuple.Item1.Second, registryTuple.Item1.Millisecond, DateTimeKind.Local) : DateTime.Now.AddDays(1);
				
				this.Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, tempDate, registryTuple.Item2, registryTuple.Item3, registryTuple.Item6, new[] {
					true,
					true
				}, this);
				
			}
		}

		public void CheckCurrentRadioButton (IModification modification)
		{
			
			Modification Modification = (modification as Modification);
			
			Label caloriesLabel = (Label)MainForm.ReturnPropertyControl(4);
			
			if (MainForm.UserCheckingTime)
			{
				
				Modification.WriteToObject(caloriesLabel, 1);
				
			}
			else if (MainForm.UserCheckingCalories)
			{
				
				Modification.WriteToObject(caloriesLabel, 0);
				
			}
			else
			{
				
				throw new InvalidOperationException ("CheckCurrentRadioButton: operation invalid; perameters cannot be parsed into a logical operation.");
				
			}
				
		}

		public bool EditBoxesHaveValidEntries ()
		{
			
			if (HasInvalidCharacters(MainForm.FoodNameProperty) || HasInvalidCharacters(MainForm.DefinerProperty))
			{
				
				this.PopupHandler.CreatePopup("Properties cannot contain special characters!", 4, MainForm.ReturnPropertyControl(0));
				
				return false;
				
			}
			
			if (AlreadyExists(MainForm.FoodNameProperty))
			{
				
				this.PopupHandler.CreatePopup("Your food name cannot be the exact same as another food item!", 4, MainForm.ReturnPropertyControl(0));
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(MainForm.FoodNameProperty))
			{
				
				this.PopupHandler.CreatePopup("Please set a food name value!", 0, MainForm.ReturnPropertyControl(0));
				
				return false;
				
			}
			
			if (MainForm.ServingSizeProperty <= 0)
			{
				
				this.PopupHandler.CreatePopup("Please set a serving size value!", 0, MainForm.ReturnPropertyControl(1));
				
				return false;
				
			}
			
			if (MainForm.CaloriesPerServingProperty <= 0)
			{
				
				this.PopupHandler.CreatePopup("Please set a calories per serving value!", 0, MainForm.ReturnPropertyControl(2));
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(MainForm.DefinerProperty))
			{
				
				this.PopupHandler.CreatePopup("Please set a definer value!", 0, MainForm.ReturnPropertyControl(3));
				
				return false;
				
			}
			
			return true;
			
		}

		private static bool AlreadyExists (string text)
		{
			
			return (FoodRelated.CombinedFoodList.Where(item => !item.Item1.Equals(FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item1, StringComparison.OrdinalIgnoreCase)).Select(item => item.Item1)).Any(s => text.Contains(s, StringComparison.OrdinalIgnoreCase));
			
		}

		private static bool HasInvalidCharacters (string text)
		{
			
			Regex validCharacters = new Regex ("[^a-zA-Z0-9 ()%]");
			
			return validCharacters.IsMatch(text);
			
		}
		
		public bool PortIsValid ()
		{
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this, false);
			
			if(!HasOnlyNumbers(MainForm.SyncComputerSocket))
			{
				
				MainForm.SyncComputerSocket = registryTuple.Item7.Item3.ToString(CultureInfo.CurrentCulture);
				
				PopupHandler.CreatePopup("The port field requires numbers.", 4);
				
				return false;
				
			}
			
			if(string.IsNullOrWhiteSpace(MainForm.SyncComputerSocket) || int.Parse(MainForm.SyncComputerSocket) <= IPEndPoint.MinPort || int.Parse(MainForm.SyncComputerSocket) >= IPEndPoint.MaxPort)
			{
				
				MainForm.SyncComputerSocket = registryTuple.Item7.Item3.ToString(CultureInfo.CurrentCulture);
				
				PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "Port must be within {0} and {1} and cannot be an empty space.\nTo change single digits, simply highlight the digit and change it that way, instead of using the backspace key.", IPEndPoint.MinPort + 1, IPEndPoint.MaxPort - 1), 4);
				
				return false;
				
			}
			
			return true;
			
		}
		
		/// <summary>
		/// Checks the given text for any character beside a number.
		/// </summary>
		/// <param name="text">
		/// Text to check
		/// </param>
		/// <returns>
		/// True if it only contains numbers; otherwise, false.
		/// </returns>
		private static bool HasOnlyNumbers (string text)
		{
			
			Regex validCharacters = new Regex ("[^0-9]");
			
			return !validCharacters.IsMatch(text);
			
		}

		public bool ValidateBackup (string appendedRegistryValue, string registryValue)
		{
			
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			
			var versionNumber = this.Storage.GetRetrievableRegistryValues(this).Item5;
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				if (string.IsNullOrWhiteSpace(versionNumber) || !string.Equals(versionNumber, fvi.FileVersion, StringComparison.OrdinalIgnoreCase))
				{
							
					tempKey.SetValue("Last WWP+ Version", fvi.FileVersion);
							
					return true;
							
				}
				
			}
			
			return false;
			
		}

		public Tuple<DateTime, double, double, bool, int, bool, int> ValidateRegistryValues (string appendedRegistryValue, string registryValue, bool thoroughCheck)
		{
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
			
				double[] tempDouble = {
					0f,
					0f
				};
				
				bool[] tempBool = {
					false,
					false
				};
				
				int[] tempInt = {
					0,
					0
				};
				
				DateTime tempDate = new DateTime ();
				
				if (thoroughCheck)
				{
						
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Default Calories Per Day")))
					{
							
						tempKey.SetValue("Default Calories Per Day", "2140");
							
					}
					
					
					if (!double.TryParse(tempKey.GetValue("Default Calories Per Day").ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out tempDouble [1]))
					{
							
						throw Errors.PremadeExceptions("Registry", "Default Calories Per Day", 0);
							
					}
						
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Calories Left for the Day")))
					{
							
						tempKey.SetValue("Calories Left for the Day", "0");
							
					}
					
					
					if (!double.TryParse(tempKey.GetValue("Calories Left for the Day").ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out tempDouble [0]))
					{
							
						throw Errors.PremadeExceptions("Registry", "Calories Left for the Day", 0);
							
					}
						
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Manual Time")))
					{
							
						tempKey.SetValue("Manual Time", tempBool[0].ToString());
							
					}
					
					
					if (!bool.TryParse((string)tempKey.GetValue("Manual Time"), out tempBool[0]))
					{
							
						throw Errors.PremadeExceptions("Registry", "Manual Time", 0);
							
					}
						
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Next Reset Date")))
					{
							
						tempKey.SetValue("Next Reset Date", DateTime.Now.ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture));
							
					}
					
					
					if (!DateTime.TryParseExact(tempKey.GetValue("Next Reset Date").ToString(), new[] {
						"yyyy MMMMM dd hh:mm:ss tt"
					}, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
					{
							
						throw Errors.PremadeExceptions("Registry", "Reset Date", 0);
							
					}
					
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Dec. Places")))
					{
							
						tempKey.SetValue("Dec. Places", "4");
							
					}
					
					if (!int.TryParse(tempKey.GetValue("Dec. Places").ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt [0]))
					{
							
						throw Errors.PremadeExceptions("Registry", "Decimal Places", 0);
							
					}
					
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Sync")))
					{
							
						tempKey.SetValue("Sync", tempBool[1].ToString());
							
					}
					
					if (!bool.TryParse((string)tempKey.GetValue("Sync"), out tempBool[1]))
					{
							
						throw Errors.PremadeExceptions("Registry", "Sync", 0);
							
					}
					
					if (!int.TryParse(tempKey.GetValue("Sync Socket").ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt [1]))
					{
							
						throw Errors.PremadeExceptions("Registry", "Sync Socket", 0);
							
					}
					
				}
				else
				{
					
					if (!DateTime.TryParseExact(tempKey.GetValue("Next Reset Date").ToString(), new[] {
						"yyyy MMMMM dd hh:mm:ss tt"
					}, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate) ||
					    !double.TryParse(tempKey.GetValue("Calories Left for the Day").ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out tempDouble [0]) ||
					    !double.TryParse(tempKey.GetValue("Default Calories Per Day").ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out tempDouble [1]) ||
					    !bool.TryParse((string)tempKey.GetValue("Manual Time"), out tempBool[0]) ||
					    !bool.TryParse((string)tempKey.GetValue("Sync"), out tempBool[1]) || 
					    !int.TryParse(tempKey.GetValue("Dec. Places").ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt[0]) || 
					    !int.TryParse(tempKey.GetValue("Sync Socket").ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt [1]))
					{
						
						throw Errors.PremadeExceptions("Registry", "ValidateRegistryValues", 0);
						
					}
					
				}
				
				return Tuple.Create(tempDate, tempDouble [0], tempDouble [1], tempBool[0], tempInt[0], tempBool[1], tempInt[1]);
				
			}
			
		}
		
	}
}


