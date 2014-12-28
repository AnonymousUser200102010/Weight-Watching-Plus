#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UniversalHandlersLibrary;

#endregion

namespace WeightWatchingProgramPlus
{

	internal sealed class Program
	{
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// 
		
		[STAThread]
		private static void Main (string[] args)
		{
			try
			{
				
				#if DEBUG
				
					GlobalVariables.RegistryMainValue = string.Format(CultureInfo.InvariantCulture, "{0}~debug", GlobalVariables.RegistryMainValue);
				
				#endif
				
				Application.EnableVisualStyles();
				
				Application.SetCompatibleTextRenderingDefault(false);
				
				if (!Directory.Exists("Files\\"))
				{
					Directory.CreateDirectory("Files\\");
				}
				
				if (!Directory.Exists("Files\\Text\\"))
				{
					Directory.CreateDirectory("Files\\Text\\");
				}
				
				bool explain = true;
				
				foreach (string s in args)
				{
					
					if (s.Equals("-clearcache", StringComparison.CurrentCultureIgnoreCase))
					{
						
						if (File.Exists("Files\\Text\\Messages.txt"))
						{
							
							File.Delete("Files\\Text\\Messages.txt");
							
						}
						
						if (File.Exists("Files\\Text\\food.table explaination.txt"))
						{
							
							File.Delete("Files\\Text\\food.table explaination.txt");
							
						}
						
						if (File.Exists("Files\\Text\\Food Diary.txt"))
						{
							
							File.Delete("Files\\Text\\Food Diary.txt");
							
						}
						
					}
					else if (s.Equals("-noexplaination", StringComparison.CurrentCultureIgnoreCase))
					{
						
						explain = false;
						
					}
					else
					{
						
						Errors.Handler(new ArgumentException (s + " is not a valid argument! The program cannot parse it and therefore cannot continue."), true, 524288, true);
						
					}
					
				}
				
				findFoodTable("Files\\Text\\food.table", "Files\\Text\\food.bku", "Files\\Text\\food.table explaination.txt", explain);

				Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
				
			}
			catch (Exception e)
			{
				
				Errors.Handler(e, true, 524288, true);
				
			}
			finally
			{
				
				Application.Run(new MainForm ());
				
			}
		}

		public static void findFoodTable (string textFilesfoodtable, string textFilesfoodbku, string textFilesfoodtableExplainationtxt, bool explain)
		{
			
			if (!File.Exists(textFilesfoodtable))
			{
				
				if (!File.Exists(textFilesfoodbku))
				{
					
					Errors.Handler(new ArgumentNullException (textFilesfoodtable, "program.cs: You need a food.table! Put a food.table file in the Files\\Text\\ folder!"), true, 524288, true);
					
					return;
					
				}
				
				File.Copy(textFilesfoodbku, textFilesfoodtable);
				
				File.SetAttributes(textFilesfoodtable, FileAttributes.Compressed);
				
			}
			else
			{
				
				if (!File.Exists(textFilesfoodbku))
				{
					
					File.Copy(textFilesfoodtable, textFilesfoodbku);
					
				}
				else
				{
					
					File.Delete(textFilesfoodbku);
					
					File.Copy(textFilesfoodtable, textFilesfoodbku);
					
				}
				
				File.SetAttributes(textFilesfoodbku, FileAttributes.Archive | FileAttributes.Temporary | FileAttributes.Compressed);
				
			}
			const string seperator = "-------------------------------------------------------------------------\n";
			
			var dosanddonts = string.Format(CultureInfo.InvariantCulture, "{0}\n", "You're allowed to use all normal characters, upper and lower case; numbers and letters\nYou're also allowed to use all special characters. A word of warning, however, the program seems to have trouble processing them in conjunction with other characters, so it's best to steer clear of them if you can.");
			
			var explaination = string.Format(CultureInfo.InvariantCulture, "The food.table file is a converted food list.txt file. This new file has greater readability and reliability than the old method, which required iterating through a full list before continuing to another. Obviously this has the potential to break and cause problems.\nThe new format is like so:\n{0}\nName of food\nServing size of food\nCalories per serving of food\ndefiner of food\n{0}\n{1}\n", seperator, dosanddonts);
			
			if (!File.Exists(textFilesfoodtableExplainationtxt) && explain)
			{
				
				File.WriteAllText(textFilesfoodtableExplainationtxt, explaination);
				
				File.SetAttributes(textFilesfoodtableExplainationtxt, FileAttributes.Compressed | FileAttributes.Temporary);
				
			}
			
		}
	}
}
