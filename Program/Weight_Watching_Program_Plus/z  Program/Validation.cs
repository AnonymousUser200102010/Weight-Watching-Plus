#region Using Directives

using System;
using System.Collections.Generic;
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

		private IMainForm MainForm;

		public Validation (IStorage store, IPopup pU, IMainForm mainForm)
		{
			
			this.Storage = store;
			
			this.PopupHandler = pU;
			
			this.MainForm = mainForm;
			
		}

		public void CheckDateValidity (IRetrieval retrieve)
		{
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			var registryTuple = Tuple.Create(DateTime.Parse(Retrieval.GetRegistryValue("reset date"), CultureInfo.InvariantCulture), Retrieval.GetRegistryValue("calories left"));
			
			if (DateTime.Compare(registryTuple.Item1, DateTime.Now) <= 0 || Registry.LocalMachine.OpenSubKey(GlobalVariables.RegistryAppendedValue + GlobalVariables.RegistryMainValue) == null)
			{
				
				var manualTimeIsInitiated = MainForm.ManualTimeIsInitiated;
				
				DateTime tempDate = manualTimeIsInitiated ? new DateTime (registryTuple.Item1.Year >= DateTime.Now.Year ? registryTuple.Item1.Year : DateTime.Now.Year, registryTuple.Item1.Month >= DateTime.Now.Month ? registryTuple.Item1.Month : DateTime.Now.Month, DateTime.Now.Day + 1, registryTuple.Item1.Hour, registryTuple.Item1.Minute, registryTuple.Item1.Second, registryTuple.Item1.Millisecond, DateTimeKind.Local) : DateTime.Now.AddDays(1);
				
				this.Storage.WriteRegistry(registryTuple.Item2, "calories left", true, this, Retrieval);
				
				this.Storage.WriteRegistry(tempDate.ToString(), "reset date", true, this, Retrieval);
				
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
			
			return (FoodRelated.CombinedFoodList.Where(item => !item.Item1.Equals(FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, StringComparison.OrdinalIgnoreCase)).Select(item => item.Item1)).Any(s => text.Contains(s, StringComparison.OrdinalIgnoreCase));
			
		}

		private static bool HasInvalidCharacters (string text)
		{
			
			Regex validCharacters = new Regex ("[^a-zA-Z0-9 ()%]");
			
			return validCharacters.IsMatch(text);
			
		}

		public bool PortIsValid (IRetrieval retrieve, bool listenPort)
		{
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			var syncSocketVar = listenPort ? Retrieval.GetRegistryValue("sync listen socket") : Retrieval.GetRegistryValue("sync send socket");
			
			if (!HasOnlyNumbers(listenPort ? MainForm.SyncListenPort : MainForm.SyncSendPort, null))
			{
				
				if (listenPort)
				{
					
					MainForm.SyncListenPort = syncSocketVar;
					
				}
				else
				{
					
					MainForm.SyncSendPort = syncSocketVar;
					
				}
				
				PopupHandler.CreatePopup("The port field can contain only numbers.", 4);
				
				return false;
				
			}
			
			if (string.IsNullOrWhiteSpace(listenPort ? MainForm.SyncListenPort : MainForm.SyncSendPort) || int.Parse(listenPort ? MainForm.SyncListenPort : MainForm.SyncSendPort, CultureInfo.InvariantCulture) <= IPEndPoint.MinPort || int.Parse(listenPort ? MainForm.SyncListenPort : MainForm.SyncSendPort, CultureInfo.InvariantCulture) >= IPEndPoint.MaxPort)
			{
				
				if (listenPort)
				{
					
					MainForm.SyncListenPort = syncSocketVar;
					
				}
				else
				{
					
					MainForm.SyncSendPort = syncSocketVar;
					
				}
				
				PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The port must be within {0} and {1} and cannot be an empty space.\nTo change single digits, simply highlight the digit and change it that way, instead of using the backspace key.", IPEndPoint.MinPort + 1, IPEndPoint.MaxPort - 1), 4);
				
				return false;
				
			}
			
			return true;
			
		}

		public bool IPAddressIsValid (IRetrieval retrieve)
		{
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			var syncIPAVar = Retrieval.GetRegistryValue("sync server name");
			
			if (!HasOnlyNumbers(MainForm.SyncIPAddress, "."))
			{
				
				MainForm.SyncIPAddress = syncIPAVar;
				
				PopupHandler.CreatePopup("The IP Address can contain only numbers and periods.", 4);
				
				return false;
				
			}
			
			int[] count = {
				0,
				0
			};
			
			if (MainForm.SyncIPAddress.Length < 15)
			{
				
				foreach (char character in MainForm.SyncIPAddress.Substring(0))
				{
					
					string characterString = character.ToString();
					
					if (count [0] > 3 || count [1] > 3)
					{
							
						MainForm.SyncIPAddress = syncIPAVar;
					
						PopupHandler.CreatePopup("The IP Address' format is wrong, the normal format is xxx.xxx.xxx.xxx " + count [0] + count [1], 4);
							
						return false;
							
					}
					
					if (characterString.Equals(".", StringComparison.OrdinalIgnoreCase))
					{
						
						count [0] = 0;
						
						count [1]++;
						
					}
					else
					{
						
						count [0]++;
						
					}
					
				}
				
			}
			else
			{
				
				MainForm.SyncIPAddress = syncIPAVar;
					
				PopupHandler.CreatePopup("The IP Address is too long. It can be up to 15 characters including periods.", 4);
							
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
		/// <param name="whitelistedCharacters">
		/// Any additional characters that have been whitelisted for a special purpose.
		/// </param>
		/// <returns>
		/// True if it only contains numbers; otherwise, false.
		/// </returns>
		private static bool HasOnlyNumbers (string text, string whitelistedCharacters)
		{
			
			Regex validCharacters = new Regex (string.Format(CultureInfo.InvariantCulture, "[^0-9 {0}]", whitelistedCharacters));
			
			return !validCharacters.IsMatch(text);
			
		}

		public bool ValidateBackup (string appendedRegistryValue, string registryValue, string backupDirectory, IRetrieval retrieve)
		{
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			
			string fileBackupVersion = Retrieval.BackupVersionFileInfo(backupDirectory);
			
			var versionNumber = Retrieval.GetRegistryValue("program version");
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				if (string.IsNullOrWhiteSpace(versionNumber) || string.Equals(versionNumber, "0.0.0.0", StringComparison.OrdinalIgnoreCase) || !string.Equals(versionNumber, fvi.FileVersion, StringComparison.OrdinalIgnoreCase))
				{
							
					tempKey.SetValue("Last WWP+ Version", fvi.FileVersion);
							
					return true;
							
				}
				
				if (!string.Equals(fileBackupVersion, fvi.FileVersion, StringComparison.OrdinalIgnoreCase))
				{
					
					return true;
					
				}
				
			}
			
			return false;
			
		}

		public bool RegistryValueDoesNotExist (string appendedRegistryValue, string registryValue, string registryIDKeyword, IRetrieval retrieve)
		{
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			var parsedRegistryKey = Retrieval.ParseRegistryKeyById(registryIDKeyword);
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				return string.IsNullOrWhiteSpace((string)tempKey.GetValue(parsedRegistryKey));
				
			}
			
		}

		public string ValidateRegistryValues (string appendedRegistryValue, string registryValue, string registryIDKeyword, IRetrieval retrieve)
		{
			
			#region variables
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			double[] tempDouble = {
				0,
				2140
			};
				
			bool[] tempBool = {
				false,
				false
			};
				
			int[] tempInt = {
				4,
				5050,
				5051
			};
			
			DateTime[] tempDate = {
				DateTime.Now
			};
			
			string[] tempString = {
				"0.0.0.0",
				"127.0.0.1",
				"USER"
			};
			
			//Name, Type, Array Position.
			var list = new List<Tuple<string, string, int>> ();
			
			list.Add(Tuple.Create("Next Reset Date", "datetime", 0));
			list.Add(Tuple.Create("Calories Left for the Day", "double", 0));
			list.Add(Tuple.Create("Default Calories Per Day", "double", 1));
			list.Add(Tuple.Create("Manual Time", "bool", 0));
			list.Add(Tuple.Create("Sync", "bool", 1));
			list.Add(Tuple.Create("Dec. Places", "int", 0));
			list.Add(Tuple.Create("Sync Listen Socket", "int", 1));
			list.Add(Tuple.Create("Sync Send Socket", "int", 2));
			list.Add(Tuple.Create("Last WWP+ Version", "string", 0));
			list.Add(Tuple.Create("Synced Computer Name", "string", 1));
			list.Add(Tuple.Create("Custom User Name", "string", 2));
			
			var registryValueList = list;
			#endregion
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				foreach (Tuple<string, string, int> registryValueTuple in registryValueList)
				{
					
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue(registryValueTuple.Item1)))
					{
						
						if (registryValueTuple.Item2.Equals("double", StringComparison.OrdinalIgnoreCase))
						{
							
							tempKey.SetValue(registryValueTuple.Item1, tempDouble [registryValueTuple.Item3].ToString(CultureInfo.CurrentCulture));
							
						}
						else if (registryValueTuple.Item2.Equals("bool", StringComparison.OrdinalIgnoreCase))
						{
							
							tempKey.SetValue(registryValueTuple.Item1, tempBool [registryValueTuple.Item3].ToString(CultureInfo.CurrentCulture));
							
						}
						else if (registryValueTuple.Item2.Equals("datetime", StringComparison.OrdinalIgnoreCase))
						{
							
							tempKey.SetValue(registryValueTuple.Item1, tempDate [registryValueTuple.Item3].ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.CurrentCulture));
							
						}
						else if (registryValueTuple.Item2.Equals("int", StringComparison.OrdinalIgnoreCase))
						{
							
							tempKey.SetValue(registryValueTuple.Item1, tempInt [registryValueTuple.Item3].ToString(CultureInfo.CurrentCulture));
							
						}
						else if (registryValueTuple.Item2.Equals("string", StringComparison.OrdinalIgnoreCase))
						{
							
							tempKey.SetValue(registryValueTuple.Item1, tempString [registryValueTuple.Item3]);
							
						}
								
					}
					
					string registryValueLiteral = (string)tempKey.GetValue(registryValueTuple.Item1);
					
					bool failedParse = false;
					
					if (registryValueTuple.Item2.Equals("double", StringComparison.OrdinalIgnoreCase))
					{
						
						failedParse |= !double.TryParse(registryValueLiteral, NumberStyles.Number, CultureInfo.InvariantCulture, out tempDouble [registryValueTuple.Item3]);
						
					}
					else if (registryValueTuple.Item2.Equals("bool", StringComparison.OrdinalIgnoreCase))
					{
						
						failedParse |= !bool.TryParse(registryValueLiteral, out tempBool [registryValueTuple.Item3]);
							
					}
					else if (registryValueTuple.Item2.Equals("datetime", StringComparison.OrdinalIgnoreCase))
					{
						
						failedParse |= !DateTime.TryParseExact(registryValueLiteral, new[] {
							"yyyy MMMMM dd hh:mm:ss tt"
						}, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate [registryValueTuple.Item3]);
						
					}
					else if (registryValueTuple.Item2.Equals("int", StringComparison.OrdinalIgnoreCase))
					{
						
						failedParse |= !int.TryParse(registryValueLiteral, NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt [registryValueTuple.Item3]);
							
					}
					else if (!registryValueTuple.Item2.Equals("string", StringComparison.OrdinalIgnoreCase))
					{
						
						throw Errors.PremadeExceptions("ValidateRegistry: Type", registryValueTuple.Item2, 0);
						
					}
					
					if (failedParse)
					{
						
						throw Errors.PremadeExceptions("ValidateRegistry", registryValueTuple.Item1, 0);
						
					}
					
				}
				
			}
			
			foreach (Tuple<string, string, int> currentItem in registryValueList.Where(value => value.Item1.Equals(Retrieval.ParseRegistryKeyById(registryIDKeyword), StringComparison.OrdinalIgnoreCase)))
			{
				
				if (currentItem.Item2.Equals("double", StringComparison.OrdinalIgnoreCase))
				{
								
					return tempDouble [currentItem.Item3].ToString(CultureInfo.InvariantCulture);
								
				}
				
				if (currentItem.Item2.Equals("bool", StringComparison.OrdinalIgnoreCase))
				{
								
					return tempBool [currentItem.Item3].ToString();
								
				}
				
				if (currentItem.Item2.Equals("datetime", StringComparison.OrdinalIgnoreCase))
				{
								
					return tempDate [currentItem.Item3].ToString();
								
				}
				
				if (currentItem.Item2.Equals("int", StringComparison.OrdinalIgnoreCase))
				{
								
					return tempInt [currentItem.Item3].ToString(CultureInfo.InvariantCulture);
					
				}
				
			}
				
			return null;
			
		}
		
	}
}


