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
	static class Storage
	{
		
		#region Read Food Table Summary

		/// <summary>
		/// This function reads the food table file and parses the results into their logical values.
		/// </summary>
		/// <param name="directory">
		/// The directory where the food table is/will be created.
		/// </param>
		/// <param name="file">
		/// The name of the food table.
		/// </param>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		/// <exception cref="T:System.IO.IOException">
		/// Thrown if the provided file, in the provided directory, does not exist.
		/// </exception>
		#endregion
		internal static void ReadFoodTable (string directory, string file)
		{
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				using (StreamReader sr = new StreamReader (directory + file))
				{
					
					int number = 0;
					int position = 0;
					String line;
					string[] combined = new string[2];
					float[] tupleItemFloat = new float[2];
					bool tupleItemBool = false;
					
					while (!string.IsNullOrEmpty((line = sr.ReadLine())))
					{
						
						if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
						{
							
							FoodRelated.CombinedFoodList.Add(new Tuple<string, float, float, string, bool> (combined [0], tupleItemFloat [0], tupleItemFloat [1], combined [1], tupleItemBool));
							
							#if DEBUG
							
							//Messages.Handler(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n\n", combined [0], tupleItemFloat [0], tupleItemFloat [1], combined [1], tupleItemBool), "Weight Watching +", true, 102400);
							
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
									
									if (!float.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out tupleItemFloat [number == 1 ? 0 : 1]))
									{
										
										throw Errors.PremadeExceptions("ReadFoodTable", "tupleItemFloat", 0);
										
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
				
				Modification.SortFoodList();
				
			}
			else
			{
				
				throw new IOException (string.Format(CultureInfo.InvariantCulture, "{0}{1} does not exist", directory, file));
				
			}
			
		}

		#region Write To Food Table Summary

		/// <summary>
		/// Parses the logical values back into a readable format and writes them to the food table.
		/// </summary>
		/// <param name="directory">
		/// The directory where the food table is/will be created.
		/// </param>
		/// <param name="file">
		/// The name of the food table.
		/// </param>
		/// <param name="additionToFoodTable">
		/// A Tuple containing ONE additional food item which will be added to the food list.
		/// </param>
		/// <exception cref="T:System.IO.IOException">
		/// Thrown if the provided file, in the provided directory, does not exist.
		/// </exception>
		#endregion
		internal static void WriteFoodTable (string directory, string file, Tuple<string, float, float, string, bool> additionToFoodTable)
		{
			
			Modification.SortFoodList();
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				File.Delete(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file));
				
			}
			else
			{
				
				throw new IOException (string.Format(CultureInfo.CurrentCulture, "{0}{1} does not exist", directory, file));
				
			}
			
			string finalstring = null;
			
			const string seperator = "-------------------------------------------------------------------------\n";
			
			for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
			{
				
				Tuple<string, float, float, string, bool> foodTuple = FoodRelated.CombinedFoodList [i];
				
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n{5}", foodTuple.Item1, foodTuple.Item2, foodTuple.Item3, foodTuple.Item4, foodTuple.Item5, seperator);
				
			}
			
			if (!string.IsNullOrWhiteSpace(additionToFoodTable.Item1) && additionToFoodTable.Item2 > 0 && additionToFoodTable.Item3 > 0 && !string.IsNullOrWhiteSpace(additionToFoodTable.Item4))
			{
				
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n{5}", additionToFoodTable.Item1, additionToFoodTable.Item2, additionToFoodTable.Item3, additionToFoodTable.Item4, additionToFoodTable.Item5, seperator);
				
			}
			else if (GlobalVariables.Debug)
			{
				
				Messages.Handler(string.Format(CultureInfo.InvariantCulture, "additionToFoodTable: one or more of your addons contained an invalid entry: \nItem 1: '{0}'\n Item 2: '{1}'\nItem 3: '{2}'\nItem 4: '{3}'\nItem 5: '{4}'\n", additionToFoodTable.Item1, additionToFoodTable.Item2, additionToFoodTable.Item3, additionToFoodTable.Item4, additionToFoodTable.Item5), "Weight Watching +", true, 102400);
				
			}
			
			File.WriteAllText(directory + file, finalstring);
			File.SetAttributes(directory + file, FileAttributes.Compressed);
			
		}

		#region Read Registy Summary

		/// <summary>
		/// Reads the registry and parses all values into their logical counterparts. Also checks to make sure the reset date is later than the current date.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registyValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		internal static void ReadRegistry (string appendedRegistryValue, string registyValue)
		{
			
			if (Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue) == null)
			{
				Registry.LocalMachine.CreateSubKey(appendedRegistryValue + registyValue);
			}
				
				if (appendedRegistryValue.Equals(GlobalVariables.RegistryAppendedValue))
				{
					
					Validation.ValidateRegistryValues(appendedRegistryValue, registyValue, true);
					
					MainForm.ManualTimeIsInitiated = GetRetrievableRegistryValues(appendedRegistryValue, registyValue).Item4;
					
					if(Validation.ValidateBackup(appendedRegistryValue, registyValue) && GlobalVariables.CreateBackups)
					{
						
						Backup("Files//Text//", "Files//Backup//");
						
					}
					
				}
				else if (registyValue.Contains("Diary", StringComparison.OrdinalIgnoreCase))
				{
					//Do nothing...yet
				}
				
				Validation.CheckDateValidity();
		}

		#region Write To Registry Summary

		/// <summary>
		/// Writes all logical values to the registry in a format it can understand.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registyValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="date">
		/// If resetting, this is the new reset date.
		/// </param>
		/// <param name="calories">
		/// Current calories left.
		/// </param>
		/// <param name="defaultCalories">
		/// Default calorie count.
		/// </param>
		/// <param name="reset">
		/// reset[0] = calorie count, reset[1] = reset date.
		/// </param>
		#endregion
		internal static void WriteRegistry (string appendedRegistryValue, string registyValue, DateTime date, float calories, float defaultCalories, System.Collections.Generic.IList<bool> reset)
		{
			
			PopupHandler PopupHandler = new PopupHandler ();
			
			float tempFloat = calories;
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				
				var registryTuple = GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
				
				if (reset [0])
				{
					
					if (calories > 0)
					{
					
						tempFloat += registryTuple.Item3;
						
					}
					else if (calories < 0)
					{
						
						tempFloat = registryTuple.Item3 + calories;
						
					}
					else
					{
						
						tempFloat = registryTuple.Item3;
						
					}
					
					if (tempFloat > (registryTuple.Item3 * 2))
					{
							
						tempFloat = (registryTuple.Item3 * 2);
							
							
						PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The program cannot allow you to have more than double your daily allowance. As a result, your calories have only been set to {0}.", tempFloat), null, 6, false);
							
					}
					else if (tempFloat < 1200f)
					{
							
						tempFloat = 1200f;
							
						PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The program cannot allow you to have less than 1200 calories per day, as that is considered the lowest point before starving. As a result, your calories have only been set to {0}.", 1200), null, 6, false);
					}
					
				}
				
				if (reset [1])
				{
					
					tempKey.SetValue("Next Reset Date", date.ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture), RegistryValueKind.String);
					
					MainForm.ManualDateTime = date;
					
				}
				
				tempKey.SetValue("Calories Left for the Day", tempFloat, RegistryValueKind.String);
				tempKey.SetValue("Default Calories Per Day", defaultCalories.ToString(CultureInfo.CurrentCulture));
				tempKey.SetValue("Manual Time", MainForm.ManualTimeIsInitiated.ToString());
				
			}
		}

		#region Get Registry Values Summary

		/// <summary>
		/// Gets all registry values that can be modified and used by the program (usually all of them).
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <returns>
		/// Returns a Tuple(DateTime CaloriesResetTime, float Calories, float DefaultCalories, bool ManualTimeInitiated, string ProgramNumber).
		/// </returns>
		/// <example>
		/// DateTime: the time at which the calories reset.
		/// </example>
		/// <example>
		/// Float (1): the amount of calories left for the day.
		/// </example>
		/// <example>
		/// Float (2): the amount of calories that is set by default.
		/// </example>
		/// <example>
		/// Bool: if the user is using a manual time instead of an automatic one.
		/// </example>
		/// <example>
		/// string: The current program version as stored in the registry.
		/// </example>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		internal static Tuple<DateTime, float, float, bool, string> GetRetrievableRegistryValues (string appendedRegistryValue, string registryValue)
		{
			
			string tempString = null;
			
			var validationTuple = Validation.ValidateRegistryValues(appendedRegistryValue, registryValue, GlobalVariables.Debug);
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				tempString = (string)tempKey.GetValue("Last WWP+ Version");
				
			}
			
			return Tuple.Create(validationTuple.Item1, validationTuple.Item2, validationTuple.Item3, validationTuple.Item4, tempString);
			
		}

		#region Food Tracking Diary Summary

		/// <summary>
		/// Food Tracking Function 1: Diary: Writes to file what food you've eaten and when.
		/// </summary>
		/// <param name="directory">
		/// The directory where the diary file is/will be created.
		/// </param>
		/// <param name="file">
		/// The name of the diary file.
		/// </param>
		/// <param name="add">
		/// Is this operation the result of an addition operation?
		/// </param>
		#endregion
		internal static void WriteFoodEaten (string directory, string file, bool add)
		{
			string finalstring = null;
			const string seperator = "-";
			
			if (MainForm.DiaryIsBeingUsed)
			{
				
				if (MainForm.UserIsWritingDiaryToFile)
				{
					
					DateTime Now = DateTime.Now;
					
					float tempserval = (float)MainForm.UserProvidedServings / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
					
					float temptolval = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2 * tempserval;
					
					temptolval = Math.Floor(temptolval) <= 0 ? (float)Math.Round(temptolval, 1) : (float)Math.Floor(temptolval);
					
					finalstring += string.Format(CultureInfo.CurrentCulture, "At {0} (on: {1}): You {2} {3} {4}", Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture), Now.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture), add ? "added back to your calorie count" : MainForm.IsDrinkProperty ? "drank" : "ate", MainForm.UserProvidedServings, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4);
					
					if (MainForm.UserProvidedServings > 1)
					{
						
						finalstring += "s";
						
					}
					
					float tempcalval = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item3 * (float)MainForm.UserProvidedServings / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
					
					finalstring += string.Format(CultureInfo.CurrentCulture, " of '{0}'.\nWhich is {1} servings, or {2} calories of '{0}'. ", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, tempserval, Math.Round(tempcalval, 4, MidpointRounding.AwayFromZero));
					
					var registryTuple = GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
					
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
		
		#region Store Backup Summary
		/// <summary>
		/// Backs up all files in the "original directory"
		/// </summary>
		/// <param name="originalDirectory">
		/// The directory which you want to copy the files from.
		/// </param>
		/// <param name="backupDirectory">
		/// The directory to copy the files to.
		/// </param>
		#endregion
		private static void Backup(string originalDirectory, string backupDirectory)
		{
			
			string[] passoverDocumentKeywords = {
				"readme",
				"diary"
			};
			
			if(!Directory.Exists(backupDirectory))
			{
				
				Directory.CreateDirectory(backupDirectory);
				
			}
			else
			{
				
				foreach(string file in ( Directory.GetFiles(backupDirectory).Where(file => !string.IsNullOrWhiteSpace(file))) )
				{
					
					File.Delete(file);
					
				}
				
			}
			
			foreach(string file in Directory.GetFiles(originalDirectory).Where(file => !passoverDocumentKeywords.Any((string s) => file.Contains(s, StringComparison.OrdinalIgnoreCase))))
			{
				
				File.Copy(file, string.Format(CultureInfo.InvariantCulture, "{0}{1}", backupDirectory, Path.GetFileName(file)));
				
			}
			
		}
		
	}
}


