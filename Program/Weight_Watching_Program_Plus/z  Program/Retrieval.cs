#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using UniversalHandlersLibrary;
#endregion

namespace WeightWatchingProgramPlus
{
	/// <summary>
	/// Functions whose primary purpose is retrieval and reading, but who don't have a more pressing primary function
	/// </summary>
	internal class Retrieval : IRetrieval
	{
		
		private IValidation Validation;
		
		private IStorage Storage;
		
		private IMainForm MainForm;
		
		public Retrieval(IValidation valid, IStorage store, IMainForm mainForm)
		{
			
			this.Validation = valid;
			
			this.Storage = store;
			
			this.MainForm = mainForm;
			
		}
		
		public void ReadFoodTable()
		{
			
			ReadFoodTable("Files\\Text\\", "food.table");
			
		}

		public void ReadFoodTable(string directory, string file)
		{
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				
				using (StreamReader sr = new StreamReader(directory + file))
				{
					
					int number = 0;
					int position = 0;
					String line;
					
					string[] combined =  {
						"placeholder",
						"placeholder"
					};
					
					double[] tupleItemDouble =  {
						1,
						1
					};
					
					bool tupleItemBool = false;
					
					while (!string.IsNullOrEmpty((line = sr.ReadLine())))
					{
						
						if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
						{
							
							FoodRelated.CombinedFoodList.Add(new Tuple<string, double, double, string, bool>(combined[0], tupleItemDouble[0], tupleItemDouble[1], combined[1], tupleItemBool));
							
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
									
									if (!double.TryParse(line, NumberStyles.Any, CultureInfo.InvariantCulture, out tupleItemDouble[number == 1 ? 0 : 1]))
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
									
									combined[number == 0 ? 0 : 1] = line;
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
				
				throw new IOException(string.Format(CultureInfo.InvariantCulture, "{0}{1} does not exist", directory, file));
				
			}
			
		}
		
		public void ReadRegistry()
		{
			
			ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
		}

		public void ReadRegistry (string appendedRegistryValue, string registyValue)
		{
				
			if (appendedRegistryValue.Equals(GlobalVariables.RegistryAppendedValue))
			{
				
				var registryTuple = Tuple.Create(bool.Parse(GetRegistryValue("manual time")), GetRegistryValue("program version"), int.Parse(GetRegistryValue("decimal places"), CultureInfo.InvariantCulture), bool.Parse(GetRegistryValue("sync enabled")), GetRegistryValue("sync server name"));
				
				var socketTuple = Tuple.Create(GetRegistryValue("sync listen socket"), GetRegistryValue("sync send socket"));
				
				MainForm.ManualTimeIsInitiated = registryTuple.Item1;
				
				MainForm.SyncEnabled = registryTuple.Item4;
				
				MainForm.SyncIPAddress = registryTuple.Item5;
				
				MainForm.SyncListenPort = socketTuple.Item1;
					
				MainForm.SyncSendPort = socketTuple.Item2;
				
				MainForm.DecimalPlaces = (decimal)registryTuple.Item3;
				
				const string filesBackupDirectory = "Files//Backup//";
				
				if (this.Validation.ValidateBackup(appendedRegistryValue, registyValue, filesBackupDirectory, this) && GlobalVariables.CreateBackups)
				{
					
					this.Storage.Backup("Files//Text//", filesBackupDirectory, registryTuple.Item2, this.GetRegistryValue("program version"), this);
					
				}
				
			}
			else if (registyValue.Contains("Diary", StringComparison.OrdinalIgnoreCase))
			{
				//Do nothing...yet
			}
				
			Validation.CheckDateValidity(this);
		}
		
		public string GetRegistryValue(string registryIDKeyword)
		{
			
			return GetRegistryValue(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, registryIDKeyword);
			
		}
		
		private string GetRegistryValue (string appendedRegistryValue, string registryValue, string registryIDKeyword)
		{
			
			var validationVariable = this.Validation.RegistryValueDoesNotExist(appendedRegistryValue, registryValue, registryIDKeyword, this) ? this.Validation.ValidateRegistryValues(appendedRegistryValue, registryValue, registryIDKeyword, this) : GetRegistryValueFromRegistry(appendedRegistryValue, registryValue, registryIDKeyword);
			
			if(string.IsNullOrEmpty(validationVariable))
			{
				
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "GetRegistryValue: registryIDKeyword: {0} produced no results.", registryIDKeyword));
				
			}
			
			//Console.WriteLine(registryIDKeyword + ": " + validationVariable);
			
			return validationVariable;
			
		}
		
		public string ParseRegistryKeyById(string registryIDKeyword)
		{
			
			if(registryIDKeyword.Contains("calories"))
			{
				
				if(registryIDKeyword.Contains("left"))
				{
					
					return "Calories Left for the Day";
					
				}
				
				if(registryIDKeyword.Contains("default"))
				{
					
					return "Default Calories Per Day";
					
				}
			
			}
			
			if(registryIDKeyword.Contains("date", StringComparison.OrdinalIgnoreCase))
			{
						
				return "Next Reset Date";
						
			}
				
			if(registryIDKeyword.Contains("time", StringComparison.OrdinalIgnoreCase))
			{
				
				return "Manual Time";
					
			}
			
			if(registryIDKeyword.Contains("sync", StringComparison.OrdinalIgnoreCase))
			{
				
				if(registryIDKeyword.Contains("enabled", StringComparison.OrdinalIgnoreCase))
				{
						
					return "Sync";
						
				}
				
				
				if(registryIDKeyword.Contains("socket", StringComparison.OrdinalIgnoreCase))
				{
						
					if(registryIDKeyword.Contains("l", StringComparison.OrdinalIgnoreCase))
					{
							
						return "Sync Listen Socket";
							
					}
					
					
					if(registryIDKeyword.Contains("s", StringComparison.OrdinalIgnoreCase))
					{
							
						return "Sync Send Socket";
							
					}
						
				}
				
				
				if(registryIDKeyword.Contains("name", StringComparison.OrdinalIgnoreCase))
				{
					
					return "Synced Computer Name";
					
				}
				
			}
			
			if(registryIDKeyword.Contains("dec", StringComparison.OrdinalIgnoreCase))
			{
					
				return "Dec. Places";
						
			}
			
			if(registryIDKeyword.Contains("version", StringComparison.OrdinalIgnoreCase))
			{
					
				return "Last WWP+ Version";
				   	
			}
			
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ParseRegistryKeyByID: {0} This value did not produce any desirable result.", registryIDKeyword));
			
		}
		
		public string GetRegistryValueFromRegistry(string appendedRegistryValue, string registryValue, string registryIDKeyword)
		{
			
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registryValue, true))
			{
				
				return (string)tempKey.GetValue(this.ParseRegistryKeyById(registryIDKeyword));
				
			}
			
		}
		
		public string BackupVersionFileInfo(string backupDirectory)
		{
			
			string fileBackupVersion = null;
			
			if(File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}Version.old", backupDirectory)))
			{
				
				using (StreamReader sr = new StreamReader(string.Format(CultureInfo.InvariantCulture, "{0}Version.old", backupDirectory)))
				{
						
					String line;
						
					while (!string.IsNullOrEmpty((line = sr.ReadLine())))
					{
						
						fileBackupVersion = line;
							
					}
						
					sr.Close();
						
				}
				
			}
			
			return fileBackupVersion;
			
		}
		
	}
	
}




