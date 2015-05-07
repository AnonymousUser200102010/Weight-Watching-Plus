#region Using Directives

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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
		
		private static bool Explain = true;
		private static bool MultiTask = false;

		[STAThread]
		private static void Main (string[] args)
		{
			
			Application.ThreadException += Application_ThreadException;
			
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
				
			IOHandler.CreateFolderTree(null);
			
			ArgumentHandler(args);
				
			findTextFiles("Files\\Text\\food.table", "Files\\Text\\food.bku", "Files\\Text\\README.txt", Explain);
			
			#if DEBUG
				
			GlobalVariables.RegistryMainValue += "~debug";
			
			GlobalVariables.Debug = true;
				
			#endif
			
			BackEnd.SetupConsole(GlobalVariables.Debug);
				
			Application.EnableVisualStyles();
				
			Application.SetCompatibleTextRenderingDefault(false);
			
			if(Process.GetProcessesByName (Path.GetFileNameWithoutExtension (Application.ProductName)).Count() <= 1 || MultiTask)
			{
				
				Application.Run(new MainForm ());
				
			}
			else
			{
				
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "{0} is already running!", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileDescription), "Application Failed to Launch");
				
				Environment.Exit(0);
				
			}
			
		}

		#region Find Files Summary
		/// <summary>
		/// Looks for any and all text files and verifies if they exist.
		/// </summary>
		/// <param name="textFilesfoodtable">
		/// Path to the food.table text file.
		/// </param>
		/// <param name="textFilesfoodbku">
		/// Path to the food.bku (food.table backup) text file.
		/// </param>
		/// <param name="textFilesfoodtableExplainationtxt">
		/// Path to the explaination file which should include all usage warnings.
		/// </param>
		/// <param name="explain">
		/// Toggles the creation of the explaination file on and off.
		/// </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// Thrown if the file, as provided in the textFilesfoodtable variable, doesn't exist.
		/// </exception>
		
		#endregion
		private static void findTextFiles (string textFilesfoodtable, string textFilesfoodbku, string textFilesfoodtableExplainationtxt, bool explain)
		{
			
			if (!File.Exists(textFilesfoodtable))
			{
				
				if (!File.Exists(textFilesfoodbku))
				{
					
					throw new ArgumentNullException (textFilesfoodtable, "program.cs: You need a food.table! Put a food.table file in the Files\\Text\\ folder!");
					
				}
				
				File.Copy(textFilesfoodbku, textFilesfoodtable);
				
				File.SetAttributes(textFilesfoodtable, FileAttributes.Compressed);
				
			}
			else
			{
				
				if (File.Exists(textFilesfoodbku))
				{
					
					File.Delete(textFilesfoodbku);
					
				}
				
				File.Copy(textFilesfoodtable, textFilesfoodbku);
				
				File.SetAttributes(textFilesfoodbku, FileAttributes.Archive | FileAttributes.Temporary | FileAttributes.Compressed);
				
			}
			
			const string seperator = "-------------------------------------------------------------------------\n";
			
			string dosanddonts = string.Format(CultureInfo.InvariantCulture, "{0}\n", "You're allowed to use all normal characters, upper and lower case; numbers and letters\nYou're forbidden from using special characters by design. Using them caused too much trouble and so they are disallowed for the time being.");
			
			string explaination = string.Format(CultureInfo.InvariantCulture, "The format for the food.table is like so:\n{0}Name of food\nServing size of food\nCalories per serving of food\ndefiner of food\n{0}\n{1}\n", seperator, dosanddonts);
			
			if (!File.Exists(textFilesfoodtableExplainationtxt) && explain)
			{
				
				File.WriteAllText(textFilesfoodtableExplainationtxt, explaination);
				
				File.SetAttributes(textFilesfoodtableExplainationtxt, FileAttributes.Compressed | FileAttributes.Temporary);
				
			}
			
		}

		private static void Application_ThreadException (object sender, ThreadExceptionEventArgs e)
		{
			
			MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
			
			Errors.Handler(e.Exception, true, true, 524288);
			
		}

		private static void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
		{
			
			MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
			
			Errors.Handler((e.ExceptionObject as Exception), true, true, 524288);
			
		}

		/// <summary>
		/// Parses and activates any arguments provided to the program at runtime.
		/// </summary>
		/// <param name="args">
		/// An array of string values containing any arguments provided with the program (or as added by you).
		/// </param>
		/// <exception cref="T:System.ArgumentException">
		/// Thrown if ANY arguement is input that has not been explicitly listed and/or handled in the function.
		/// </exception>
		private static void ArgumentHandler (string[] args)
		{
			
			foreach (string s in args)
			{
					
				if (s.Equals("-clearcache", StringComparison.CurrentCultureIgnoreCase))
				{
					
					if (File.Exists("Files\\Text\\Messages.txt"))
					{
						
						File.Delete("Files\\Text\\Messages.txt");
							
					}
						
					if (File.Exists("Files\\Text\\README.txt"))
					{
							
						File.Delete("Files\\Text\\README.txt");
							
					}
						
					if (File.Exists("Files\\Text\\Food Diary.txt"))
					{
							
						File.Delete("Files\\Text\\Food Diary.txt");
							
					}
						
				}
				else if (s.Equals("-noexplanation", StringComparison.CurrentCultureIgnoreCase))
				{
						
					Explain = false;
						
				}
				else if (s.Equals("-nobackup", StringComparison.CurrentCultureIgnoreCase))
				{
						
					GlobalVariables.CreateBackups = false;
						
				}
				else if (s.Equals("-debugrelease", StringComparison.CurrentCultureIgnoreCase))
				{
					
					GlobalVariables.Debug = true;
					
				}
				else if (s.Equals("-allowmulti", StringComparison.CurrentCultureIgnoreCase))
				{
					
					MultiTask = true;
					
				}
				else
				{
						
					throw new ArgumentException (string.Format(CultureInfo.InvariantCulture, "{0} is not a valid argument! The program cannot parse it and therefore cannot continue.", s));
					
				}
					
			}
			
		}
		
	}
}
