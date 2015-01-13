#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
		#endregion
		internal static void ReadFoodTable(string directory, string file)
		{
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				using (StreamReader sr = new StreamReader(directory + file))
				{
					int number = 0;
					int position = 0;
					String line;
					string[] combined = new string[4];
					
					while (!string.IsNullOrEmpty((line = sr.ReadLine())))
					{
						
						if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
						{
							
							float[] tupleItemFloat =  {
								0.0f,
								0.0f
							};
							
							if (!float.TryParse(combined[1], NumberStyles.Float, CultureInfo.InvariantCulture, out tupleItemFloat[0]) || !float.TryParse(combined[2], NumberStyles.Float, CultureInfo.InvariantCulture, out tupleItemFloat[1]))
							{
								
								Errors.Handler(Errors.premadeExceptions("ReadFoodTable", "tupleItemFloat", 0), true, true, 524288);
								
							}
							
							FoodRelated.CombinedFoodList.Add(new Tuple<string, float, float, string>(combined[0], tupleItemFloat[0], tupleItemFloat[1], combined[3]));
							position++;
							
							number = 0;
							
						}
						else
						{
							
							combined[number] = line;
							number++;
							
						}
						
					}
					
					sr.Close();
					
				}
				
				Modification.SortFoodList();
				
			}
			else
			{
				
				Errors.Handler(new IOException(string.Format(CultureInfo.InvariantCulture, "{0}{1} does not exist", directory, file)), true, true, 524288);
				
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
		#endregion
		internal static void WriteFoodTable(string directory, string file, Tuple<string, float, float, string> additionToFoodTable)
		{
			
			Modification.SortFoodList();
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				File.Delete(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file));
				
			}
			else
			{
				
				Errors.Handler(new IOException(string.Format(CultureInfo.CurrentCulture, "{0}{1} does not exist", directory, file)), true, true, 524288);
				
			}
			
			string finalstring = null;
			const string seperator = "-------------------------------------------------------------------------\n";
			
			for (int i = 0, FoodRelatedCombinedFoodListCount = FoodRelated.CombinedFoodList.Count; i < FoodRelatedCombinedFoodListCount; i++)
			{
				
				Tuple<string, float, float, string> foodTuple = FoodRelated.CombinedFoodList[i];
				
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}", foodTuple.Item1, foodTuple.Item2, foodTuple.Item3, foodTuple.Item4, seperator);
				
			}
			
			if (!string.IsNullOrWhiteSpace(additionToFoodTable.Item1) && additionToFoodTable.Item2 > 0 && additionToFoodTable.Item3 > 0 && !string.IsNullOrWhiteSpace(additionToFoodTable.Item4))
			{
				
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}", additionToFoodTable.Item1, additionToFoodTable.Item2, additionToFoodTable.Item3, additionToFoodTable.Item4, seperator);
				
			}
			else
			{
				if (string.IsNullOrWhiteSpace(additionToFoodTable.Item1) || additionToFoodTable.Item2 <= 0 || additionToFoodTable.Item3 <= 0 || string.IsNullOrWhiteSpace(additionToFoodTable.Item4))
				{
					
					Messages.Handler("additionToFoodTable: one or more of your addons contained an invalid entry. Was this intended?", "Weight Watching +", true, 102400);
					
				}
				
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
		#endregion
		internal static void ReadRegistry(string appendedRegistryValue, string registyValue)
		{
			
			Validation Validation = new Validation ();
			
			if (Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue) == null)
			{
				Registry.LocalMachine.CreateSubKey(appendedRegistryValue + registyValue);
			}
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				
				if (appendedRegistryValue.Equals(GlobalVariables.RegistryAppendedValue))
				{
					
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Calories Left for the Day")))
					{
						
						tempKey.SetValue("Calories Left for the Day", Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3);
						
					}
					
					float tempfloat = 0f;
					
					if (!float.TryParse(tempKey.GetValue("Calories Left for the Day").ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out tempfloat))
					{
						
						Errors.Handler(Errors.premadeExceptions("Registry", "Calories Left for the Day", 0), true, true, 524288);
						
					}
					tempfloat = 0f;
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Default Calories Per Day")))
					{
						
						tempKey.SetValue("Default Calories Per Day", "2140");
						
					}
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Last Used Date")))
					{
						
						tempKey.SetValue("Last Used Date", DateTime.Now.ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture));
						
					}
					
					Validation.CheckDateValidity(GetRetrievableRegistryValues(appendedRegistryValue, registyValue).Item1);
					
				}
				else if (registyValue.Contains("Diary", StringComparison.OrdinalIgnoreCase))
				{
					//Do nothing...yet
				}
			}
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
		internal static void WriteRegistry(string appendedRegistryValue, string registyValue, DateTime date, float calories, float defaultCalories, System.Collections.Generic.IList<bool> reset)
		{
			
			float tempFloat = calories;
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				
				if (reset[0])
				{
					
					if (calories > 0)
					{
						
						tempFloat += Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3;
						
					}
					else if (calories < 0)
					{
						
						tempFloat = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3 + calories;
						
					}
					else
					{
						
						tempFloat = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3;
						
					}
					
				}
				
				if (reset[1])
				{
					
					tempKey.SetValue("Last Used Date", date.ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture), RegistryValueKind.String);
					
				}
				
				tempKey.SetValue("Calories Left for the Day", tempFloat, RegistryValueKind.String);
				tempKey.SetValue("Default Calories Per Day", defaultCalories.ToString(CultureInfo.CurrentCulture));
				
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
		/// Returns a Tuple(DateTime CaloriesResetTime, Float Calories, Float DefaultCalories).
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
		#endregion
		internal static Tuple<DateTime, float, float> GetRetrievableRegistryValues(string appendedRegistryValue, string registryValue)
		{
			DateTime tempDate = new DateTime();
			float[] tempFloat = {
				0f,
				0f
			};
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				if (!DateTime.TryParseExact(tempKey.GetValue("Last Used Date").ToString(), new[] {"yyyy MMMMM dd hh:mm:ss tt"}, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
				{
					
					Errors.Handler(Errors.premadeExceptions("Registry", "Last Used Date", 0), true, true, 524288);
					
				}
				
				if (!float.TryParse(tempKey.GetValue("Calories Left for the Day").ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out tempFloat[0]))
				{
					
					Errors.Handler(Errors.premadeExceptions("Registry", "Calories Left for the Day", 0), true, true, 524288);
					
				}
				
				if (!float.TryParse(tempKey.GetValue("Default Calories Per Day").ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out tempFloat[1]))
				{
					
					Errors.Handler(Errors.premadeExceptions("Registry", "Default Calories Per Day", 0), true, true, 524288);
					
				}
			}
			return Tuple.Create(tempDate, tempFloat[0], tempFloat[1]);
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
		/// <param name="WriteToFile">
		/// Is this operation writing the diary values to a file?
		/// </param>
		/// <param name="record">
		/// Is this operation recording at all?
		/// </param>
		/// <param name="userServingInputTextBox">
		/// The NumericUpDown for the "number of servings" value the user has input.
		/// </param>
		/// <param name="add">
		/// Is this operation the result of an addition operation?
		/// </param>
		#endregion
		internal static void WriteFoodEaten(string directory, string file, CheckBox WriteToFile, CheckBox record, NumericUpDown userServingInputTextBox, bool add)
		{
			string finalstring = null;
			const string seperator = "-";
			
			if (record.Checked)
			{
				
				if (WriteToFile.Checked)
				{
					
					DateTime Now = DateTime.Now;
					
					float tempserval = float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item2;
					
					float temptolval = FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item2 * tempserval;
					
					temptolval = Math.Floor(temptolval) <= 0 ? (float)Math.Round(temptolval, 1) : (float)Math.Floor(temptolval);
					
					finalstring += string.Format(CultureInfo.CurrentCulture, "At {0} (on: {1}): You {2}: {3} {4}", Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture), Now.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture), add ? "added back to your calorie count" : FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item4.Contains("fluid", StringComparison.CurrentCulture) ? "drank" : "ate", userServingInputTextBox.Value, FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item4);
					
					if (userServingInputTextBox.Value > 1)
					{
						
						finalstring += "s";
						
					}
					
					float tempcalval = FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item3 * float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item2;
					
					finalstring += string.Format(CultureInfo.CurrentCulture, " (of: '{0}').\nWhich is {1} servings, or {2} calories of '{0}'. ", FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item1, tempserval, Math.Round(tempcalval, 4, MidpointRounding.AwayFromZero));
					
					finalstring += Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2 >= 0 ? string.Format(CultureInfo.CurrentCulture, "You had {0} calories left for the day.\n{1}\n", Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2, seperator) : string.Format(CultureInfo.CurrentCulture, "You had {0} calories left for the day.\n{1}\n", Math.Abs(Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2), seperator);
					
					if (File.Exists(directory + file))
					{
						
						using (StreamReader sr = new StreamReader(directory + file))
						{
							
							string line;
							
							while (!string.IsNullOrEmpty((line = sr.ReadLine())))
							{
								
								finalstring += line.Contains(seperator, StringComparison.CurrentCultureIgnoreCase) ? string.Format(CultureInfo.CurrentCulture, "{0}\n", seperator) : string.Format(CultureInfo.CurrentCulture, "{0}\n", line);
								
							}
							
							sr.Close();
						}
						
						File.Delete(directory + file);
					}
					
					File.WriteAllText(directory + file, finalstring);
					
					File.SetAttributes(directory + file, FileAttributes.Compressed | FileAttributes.Temporary);
				}
				
				//ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue + "\\Diary");
				
				userServingInputTextBox.Value = 1;
				
			}
			
		}
		
	}
}


