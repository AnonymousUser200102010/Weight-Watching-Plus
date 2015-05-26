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
	/// Functions whose primary purpose is storage and writing, but who don't have a more pressing primary function.
	/// </summary>
	internal class Storage : IStorage
	{
		
		private IPopup PopupHandler;
		
		private IMainForm MainForm;
		
		public Storage(IPopup pU, IMainForm mainForm)
		{
			
			this.PopupHandler = pU;
			
			this.MainForm = mainForm;
			
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
			
			string fileToUseLiteral = string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file);
			
			if (File.Exists(fileToUseLiteral))
			{
				
				File.Delete(fileToUseLiteral);
				
			}
			else
			{
				
				throw new IOException (string.Format(CultureInfo.CurrentCulture, "{0}{1} does not exist", directory, file));
				
			}
			
			const string seperator = "-------------------------------------------------------------------------\n";
			
			using (StreamWriter outfile = new StreamWriter(fileToUseLiteral))
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
					
					Messages.Handler(string.Format(CultureInfo.InvariantCulture, "additionToFoodTable: one or more of your addons contained an invalid entry: \nItem 1: '{0}'\n Item 2: '{1}'\nItem 3: '{2}'\nItem 4: '{3}'\nItem 5: '{4}'\n", additionToFoodTable.Item1, additionToFoodTable.Item2, additionToFoodTable.Item3, additionToFoodTable.Item4, additionToFoodTable.Item5), "Weight Watching +", true, 25000);
					
				}
				
			}
			
			File.SetAttributes(directory + file, FileAttributes.Compressed);
			
		}
		
		#region WriteRegistry Overrides
		
		public void WriteRegistry(string registryIDKeyword, bool reset, IValidation valid, IRetrieval retrieve)
		{
			
			string objectToSave = null;
			
			if(registryIDKeyword.Contains("time", StringComparison.OrdinalIgnoreCase))
			{
				
				objectToSave = MainForm.ManualTimeIsInitiated.ToString();
					
			}
			else if(registryIDKeyword.Contains("sync", StringComparison.OrdinalIgnoreCase))
			{
				
				if(registryIDKeyword.Contains("enabled", StringComparison.OrdinalIgnoreCase))
				{
						
					objectToSave = MainForm.SyncEnabled.ToString();
						
				}
				else if(registryIDKeyword.Contains("socket", StringComparison.OrdinalIgnoreCase))
				{
						
					if(registryIDKeyword.Contains("l", StringComparison.OrdinalIgnoreCase))
					{
							
						objectToSave = MainForm.SyncListenPort;
							
					}
					else if(registryIDKeyword.Contains("s", StringComparison.OrdinalIgnoreCase))
					{
							
						objectToSave = MainForm.SyncSendPort;
							
					}
						
				}
				else if(registryIDKeyword.Contains("name", StringComparison.OrdinalIgnoreCase))
				{
					
					objectToSave = MainForm.SyncIPAddress;
					
				}
				else
				{
					
					throw new ArgumentException("WriteRegistry: registryIDKeyword: This value did not produce any desireable result.");
					
				}
				
			}
			else
			{
				
				throw new ArgumentException("WriteRegistry: registryIDKeyword: This value did not produce any desireable result.");
				
			}
			
			WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, objectToSave, registryIDKeyword, reset, valid, retrieve);
			
		}
		
		public void WriteRegistry(string objectToSave, string registryIDKeyword, bool reset, IValidation valid, IRetrieval retrieve)
		{
			
			WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, objectToSave, registryIDKeyword, reset, valid, retrieve);
			
		}
		
		public void WriteRegistry (string appendedRegistryValue, string registyValue, string objectToSave, string registryIDKeyword, bool reset, IValidation valid, IRetrieval retrieve)
		{
			
			Validation Validation = (valid as Validation);
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			var registryNameLiteral = Retrieval.ParseRegistryKeyById(registryIDKeyword);
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				
				if(registryIDKeyword.Contains("calories"))
				{
					
					if(registryIDKeyword.Contains("left"))
					{
						
						var registryDefaultCalories = double.Parse(Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture);
						
						var calories = double.Parse(objectToSave, CultureInfo.CurrentCulture);
						
						double tempDouble = calories;
						
						if (reset)
						{
							
							if (calories > 0)
							{
							
								tempDouble += registryDefaultCalories;
								
							}
							else if (calories < 0)
							{
								
								tempDouble = registryDefaultCalories + calories;
								
							}
							else
							{
								
								tempDouble = registryDefaultCalories;
								
							}
							
							if (tempDouble > (registryDefaultCalories * 2))
							{
									
								tempDouble = (registryDefaultCalories * 2);
									
									
								this.PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The program cannot allow you to have more than double your daily allowance. As a result, your calories have only been set to {0}.", tempDouble), 6);
									
							}
							else if (tempDouble < 1200f)
							{
									
								tempDouble = 1200f;
									
								this.PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "The program cannot allow you to have less than 1200 calories per day, as that is considered the lowest point before starving. As a result, your calories have only been set to {0}.", 1200), 6);
							}
							
						}
						
						tempKey.SetValue(registryNameLiteral, tempDouble.ToString(CultureInfo.CurrentCulture));
						
					}
					else if(registryIDKeyword.Contains("default"))
					{
						
						tempKey.SetValue(registryNameLiteral, objectToSave);
						
					}
					
				}
				else if(registryIDKeyword.Contains("date", StringComparison.OrdinalIgnoreCase))
				{
					
					DateTime date = DateTime.Parse(objectToSave, CultureInfo.CurrentCulture);
						
					tempKey.SetValue(registryNameLiteral, date.ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture));
						
					MainForm.ManualDateTime = date;
						
				}	
				else if(registryIDKeyword.Contains("time", StringComparison.OrdinalIgnoreCase))
				{
					
					tempKey.SetValue(registryNameLiteral, MainForm.ManualTimeIsInitiated.ToString());
						
				}
				else if(registryIDKeyword.Contains("sync", StringComparison.OrdinalIgnoreCase))
				{
					
					if(registryIDKeyword.Contains("enabled", StringComparison.OrdinalIgnoreCase))
					{
							
						tempKey.SetValue(registryNameLiteral, MainForm.SyncEnabled);
							
					}
					else if(registryIDKeyword.Contains("socket", StringComparison.OrdinalIgnoreCase))
					{
							
						if(registryIDKeyword.Contains("l", StringComparison.OrdinalIgnoreCase))
						{
								
							tempKey.SetValue(registryNameLiteral, MainForm.SyncListenPort);
								
						}
						else if(registryIDKeyword.Contains("s", StringComparison.OrdinalIgnoreCase))
						{
								
							tempKey.SetValue(registryNameLiteral, MainForm.SyncSendPort);
								
						}
							
					}
					else if(registryIDKeyword.Contains("name", StringComparison.OrdinalIgnoreCase))
					{
						
						tempKey.SetValue(registryNameLiteral, MainForm.SyncIPAddress);
						
					}
					
				}
				else if(registryIDKeyword.Contains("dec", StringComparison.OrdinalIgnoreCase))
				{
					
					tempKey.SetValue(registryNameLiteral, objectToSave);
						
				}
				
			}
			
		}
		
		#endregion

		public void WriteFoodEaten(bool add, IValidation valid, IRetrieval retrieve)
		{
			
			WriteFoodEaten("Files\\Text\\", "Food Diary.txt", add, valid, retrieve);
			
		}

		public void WriteFoodEaten (string directory, string file, bool add, IValidation valid, IRetrieval retrieve)
		{
			
			Validation Validation = (valid as Validation);
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
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

					finalstring += string.Format(CultureInfo.CurrentCulture, "You had {0} calories left for the day.\n{1}\n", Retrieval.GetRegistryValue("calories left"), seperator);
					
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

		public void Backup (string originalDirectory, string backupDirectory, string oldVersion, string newVersion, IRetrieval retrieve)
		{
			
			string[] passoverDocumentKeywords = {
				"readme",
				"diary"
			};
			
			Retrieval Retrieval = (retrieve as Retrieval);
			
			string backupVersionFileVersion = Retrieval.BackupVersionFileInfo(backupDirectory);
			
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
			
			File.WriteAllText(string.Format(CultureInfo.CurrentCulture, "{0}Backup.log", backupDirectory), string.Format(CultureInfo.CurrentCulture, "Files backed up on {0} at {1}.\nFrom version {2} to version {3}.", DateTime.Now.ToString("MMMMM dd yyyy", CultureInfo.CurrentCulture), DateTime.Now.ToString("hh:mm tt", CultureInfo.CurrentCulture), !string.Equals(oldVersion, newVersion, StringComparison.OrdinalIgnoreCase) ? oldVersion : backupVersionFileVersion, newVersion));
			
			File.WriteAllText(string.Format(CultureInfo.CurrentCulture, "{0}Version.old", backupDirectory), string.Format(CultureInfo.CurrentCulture, "{0}", newVersion));
			
		}
		
	}
}


