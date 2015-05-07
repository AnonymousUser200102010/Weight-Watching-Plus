#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using UniversalHandlersLibrary;

#endregion

namespace WeightWatchingProgramPlus
{
	
	/// <summary>
	/// Functions that relate to storage
	/// </summary>
	internal class Storage : IStorage
	{
		
		IPopup PopupHandler;
		
		public Storage(PopupHandler pU)
		{
			
			this.PopupHandler = pU;
			
		}
		
		public void ReadFoodTable()
		{
			
			ReadFoodTable("Files\\Text\\", "food.table");
			
		}

		public void ReadFoodTable (string directory, string file)
		{

			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				using (StreamReader sr = new StreamReader (directory + file))
				{
					
					int number = 0;
					int position = 0;
					String line;
					string[] combined = new string[2];
					double[] tupleItemDouble = new double[2];
					bool tupleItemBool = false;
					
					while (!string.IsNullOrEmpty((line = sr.ReadLine())))
					{
						
						if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
						{
							
							FoodRelated.CombinedFoodList.Add(new Tuple<string, double, double, string, bool> (combined [0], tupleItemDouble [0], tupleItemDouble [1], combined [1], tupleItemBool));
							
							#if DEBUG
							
							//Messages.Handler(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n\n", combined [0], tupleItemDouble [0], tupleItemDouble [1], combined [1], tupleItemBool), "Weight Watching +", true, 102400);
							
							#endif
							
							position++;
							
							number = 0;
							
						}
						else
						{
							
							switch (number)
							{
									
								case 1:
								case 2:
									
									if (!double.TryParse(line, NumberStyles.Any, CultureInfo.InvariantCulture, out tupleItemDouble [number == 1 ? 0 : 1]))
									{
										
										throw Errors.PremadeExceptions("ReadFoodTable", "tupleItemDouble", 0);
										
									}
									
									break;
									
								case 4:
									
									if (!bool.TryParse(line, out tupleItemBool))
									{
										
										throw Errors.PremadeExceptions("ReadFoodTable", "tupleItemBool", 0);
										
									}
									
									break;
									
								default:
									
									combined [number == 0 ? 0 : 1] = line;
									
									break;
									
							}
							
							number++;
							
						}
						
					}
					
					sr.Close();
					
				}
				
			}
			else
			{
				
				throw new IOException (string.Format(CultureInfo.InvariantCulture, "{0}{1} does not exist", directory, file));
				
			}
			
		}

		public void WriteFoodTable()
		{
			
			WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, double, double, string, bool>(null, 0, 0, null, false));
			
		}
		
		public void WriteFoodTable(Tuple<string, double, double, string, bool> additionToFoodTable)
		{
			
			WriteFoodTable("Files\\Text\\", "food.table", additionToFoodTable);
			
		}

		public void WriteFoodTable (string directory, string file, Tuple<string, double, double, string, bool> additionToFoodTable)
		{
			
			FoodRelated.CombinedFoodList.Sort();
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				File.Delete(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file));
				
			}
			else
			{
				
				throw new IOException (string.Format(CultureInfo.CurrentCulture, "{0}{1} does not exist", directory, file));
				
			}
			
			const string seperator = "-------------------------------------------------------------------------\n";
			
			using (StreamWriter outfile = new StreamWriter(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
				{
					
					outfile.Write(string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n{5}", FoodRelated.CombinedFoodList [i].Item1, FoodRelated.CombinedFoodList [i].Item2, FoodRelated.CombinedFoodList [i].Item3, FoodRelated.CombinedFoodList [i].Item4, FoodRelated.CombinedFoodList [i].Item5, seperator));
					
				}
				
				if (!string.IsNullOrWhiteSpace(additionToFoodTable.Item1) && additionToFoodTable.Item2 > 0 && additionToFoodTable.Item3 > 0 && !string.IsNullOrWhiteSpace(additionToFoodTable.Item4))
				{
					
					outfile.Write(string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n{5}", additionToFoodTable.Item1, additionToFoodTable.Item2, additionToFoodTable.Item3, additionToFoodTable.Item4, additionToFoodTable.Item5, seperator));
					
				}
				else if (GlobalVariables.Debug)
				{
					
					Messages.Handler(string.Format(CultureInfo.InvariantCulture, "additionToFoodTable: one or more of your addons contained an invalid entry: \nItem 1: '{0}'\n Item 2: '{1}'\nItem 3: '{2}'\nItem 4: '{3}'\nItem 5: '{4}'\n", additionToFoodTable.Item1, additionToFoodTable.Item2, additionToFoodTable.Item3, additionToFoodTable.Item4, additionToFoodTable.Item5), "Weight Watching +", true, 102400);
					
				}
				
			}
			
			File.SetAttributes(directory + file, FileAttributes.Compressed);
			
		}

		public void ReadRegistry(IValidation valid)
		{
			
			ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, valid);
			
		}

		public void ReadRegistry (string appendedRegistryValue, string registyValue, IValidation valid)
		{
			
			Validation Validation = (valid as Validation);
			
			if (Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue) == null)
			{
				Registry.LocalMachine.CreateSubKey(appendedRegistryValue + registyValue);
			}
				
			if (appendedRegistryValue.Equals(GlobalVariables.RegistryAppendedValue))
			{
				
				Validation.ValidateRegistryValues(appendedRegistryValue, registyValue, true);
				
				var registryTuple = GetRetrievableRegistryValues(valid);
				
				MainForm.ManualTimeIsInitiated = registryTuple.Item4;
				
				MainForm.DecimalPlaces = (decimal)registryTuple.Item6;
				
				if (Validation.ValidateBackup(appendedRegistryValue, registyValue) && GlobalVariables.CreateBackups)
				{
					
					Backup("Files//Text//", "Files//Backup//", registryTuple.Item5, GetRetrievableRegistryValues(valid).Item5);
					
				}
				
			}
			else if (registyValue.Contains("Diary", StringComparison.OrdinalIgnoreCase))
			{
				//Do nothing...yet
			}
				
			Validation.CheckDateValidity();
		}

		public void WriteRegistry(double calories, double defaultCalories, int decimalPlaces, IValidation valid)
		{
			
			WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now, calories, defaultCalories, decimalPlaces, new []{ false, false }, valid);
			
		}

		public void WriteRegistry (string appendedRegistryValue, string registyValue, DateTime date, double calories, double defaultCalories, int decimalPlaces, System.Collections.Generic.IList<bool> reset, IValidation valid)
		{
			
			Validation Validation = (valid as Validation);
			
			double tempDouble = calories;
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				
				var registryTuple = GetRetrievableRegistryValues(Validation, false);
				
				if (reset [0])
				{
					
					if (calories > 0)
					{
					
						tempDouble += registryTuple.Item3;
						
					}
					else if (calories < 0)
					{
						
						tempDouble = registryTuple.Item3 + calories;
						
					}
					else
					{
						
						tempDouble = registryTuple.Item3;
						
					}
					
					if (tempDouble > (registryTuple.Item3 * 2))
					{
							
						tempDouble = (registryTuple.Item3 * 2);
							
							
						this.PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The program cannot allow you to have more than double your daily allowance. As a result, your calories have only been set to {0}.", tempDouble), 6);
							
					}
					else if (tempDouble < 1200f)
					{
							
						tempDouble = 1200f;
							
						this.PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The program cannot allow you to have less than 1200 calories per day, as that is considered the lowest point before starving. As a result, your calories have only been set to {0}.", 1200), 6);
					}
					
				}
				
				if (reset [1])
				{
					
					tempKey.SetValue("Next Reset Date", date.ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture));
					
					MainForm.ManualDateTime = date;
					
				}
				
				tempKey.SetValue("Calories Left for the Day", tempDouble.ToString(CultureInfo.CurrentCulture));
				tempKey.SetValue("Default Calories Per Day", defaultCalories.ToString(CultureInfo.CurrentCulture));
				tempKey.SetValue("Manual Time", MainForm.ManualTimeIsInitiated.ToString());
				tempKey.SetValue("Dec. Places", decimalPlaces.ToString(CultureInfo.CurrentCulture));
				
			}
		}

		public Tuple<DateTime, double, double, bool, string, int> GetRetrievableRegistryValues(IValidation valid)
		{
			
			return GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, valid, true);
			
		}
		
		public Tuple<DateTime, double, double, bool, string, int> GetRetrievableRegistryValues(IValidation valid, bool roundCaloriesLeftForDay)
		{
			
			return GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, valid, roundCaloriesLeftForDay);
			
		}

		public Tuple<DateTime, double, double, bool, string, int> GetRetrievableRegistryValues (string appendedRegistryValue, string registryValue, IValidation valid, bool roundCaloriesLeftForDay)
		{
			
			Validation Validation = (valid as Validation);
			
			string tempString = null;
			
			var validationTuple = Validation.ValidateRegistryValues(appendedRegistryValue, registryValue, GlobalVariables.Debug);
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				tempString = (string)tempKey.GetValue("Last WWP+ Version");
				
			}
			
			return Tuple.Create(validationTuple.Item1, roundCaloriesLeftForDay ? (double)Math.Round(validationTuple.Item2, validationTuple.Item5) : validationTuple.Item2, (double)Math.Round(validationTuple.Item3, validationTuple.Item5), validationTuple.Item4, tempString, validationTuple.Item5);
			
		}

		public void WriteFoodEaten(bool add, IValidation valid)
		{
			
			WriteFoodEaten("Files\\Text\\", "Food Diary.txt", add, valid);
			
		}

		public void WriteFoodEaten (string directory, string file, bool add, IValidation valid)
		{
			
			Validation Validation = (valid as Validation);
			
			string finalstring = null;
			const string seperator = "-";
			
			if (MainForm.DiaryIsBeingUsed)
			{
				
				if (MainForm.UserIsWritingDiaryToFile)
				{
					
					DateTime Now = DateTime.Now;
					
					double tempserval = (double)MainForm.UserProvidedServings / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
					
					double temptolval = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2 * tempserval;
					
					temptolval = Math.Floor(temptolval) <= 0 ? (double)Math.Round(temptolval, 1) : (double)Math.Floor(temptolval);
					
					finalstring += string.Format(CultureInfo.CurrentCulture, "At {0} (on: {1}): You {2} {3} {4}", Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture), Now.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture), add ? "added back to your calorie count" : MainForm.IsDrinkProperty ? "drank" : "ate", MainForm.UserProvidedServings, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4);
					
					if (MainForm.UserProvidedServings > 1)
					{
						
						finalstring += "s";
						
					}
					
					double tempcalval = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item3 * (double)MainForm.UserProvidedServings / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
					
					finalstring += string.Format(CultureInfo.CurrentCulture, " of '{0}'.\nWhich is {1} servings, or {2} calories of '{0}'. ", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, tempserval, Math.Round(tempcalval, 4, MidpointRounding.AwayFromZero));
					
					var registryTuple = GetRetrievableRegistryValues(Validation);
					
					finalstring += string.Format(CultureInfo.CurrentCulture, "You had {0} calories left for the day.\n{1}\n", registryTuple.Item2, seperator);
					
					if (File.Exists(directory + file))
					{
						
						using (StreamReader sr = new StreamReader (directory + file))
						{
							
							string line;
							
							while (!string.IsNullOrEmpty((line = sr.ReadLine())))
							{
								
								finalstring += string.Format(CultureInfo.CurrentCulture, "{0}\n", line);
								
							}
							
							sr.Close();
						}
						
						File.Delete(directory + file);
					}
					
					File.WriteAllText(directory + file, finalstring);
					
					File.SetAttributes(directory + file, FileAttributes.Compressed | FileAttributes.Temporary);
				}
				
				//ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue + "\\Diary");
				
				MainForm.UserProvidedServings = 1;
				
			}
			
		}

		private static void Backup (string originalDirectory, string backupDirectory, string oldVersion, string newVersion)
		{
			
			string[] passoverDocumentKeywords = {
				"readme",
				"diary"
			};
			
			if (!Directory.Exists(backupDirectory))
			{
				
				Directory.CreateDirectory(backupDirectory);
				
			}
			else
			{
				
				foreach (string file in ( Directory.GetFiles(backupDirectory).Where(file => !string.IsNullOrWhiteSpace(file))))
				{
					
					File.Delete(file);
					
				}
				
			}
			
			foreach (string file in Directory.GetFiles(originalDirectory).Where(file => !passoverDocumentKeywords.Any((string s) => file.Contains(s, StringComparison.OrdinalIgnoreCase))))
			{
				
				File.Copy(file, string.Format(CultureInfo.InvariantCulture, "{0}{1}", backupDirectory, Path.GetFileName(file)));
				
			}
			
			File.WriteAllText(string.Format(CultureInfo.CurrentCulture, "{0}Backup.log", backupDirectory), string.Format(CultureInfo.CurrentCulture, "Files backed up on {0} at {1}.\nFrom version {2} to version {3}.", DateTime.Now.ToString("MMMMM dd yyyy", CultureInfo.CurrentCulture), DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.CurrentCulture), oldVersion, newVersion));
			
		}
		
	}
}


