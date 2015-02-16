#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UniversalHandlersLibrary;

#endregion

namespace WeightWatchingProgramPlus
{
	
	internal static class SafeNativeMethods
	{
		
		#if DEBUG
		
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocConsole ();
		
		#endif
		
	}

	internal sealed class Program
	{
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// 
		
		private static bool explain = true;
		
		[STAThread]
		private static void Main (string[] args)
		{
			
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			
			try
			{
				
				#if DEBUG
				
				GlobalVariables.RegistryMainValue = string.Format(CultureInfo.InvariantCulture, "{0}~debug", GlobalVariables.RegistryMainValue);
				
				SafeNativeMethods.AllocConsole();
				
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
				
				ArgumentHandler(args);
				
				findTextFiles("Files\\Text\\food.table", "Files\\Text\\food.bku", "Files\\Text\\food.table explaination.txt", explain);
				
			}
			catch (Exception e)
			{
				
				Errors.Handler(e, true, true, 524288);
				
			}
			finally
			{
				
				Application.Run(new MainForm ());
				
			}
			
		}

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

		private static void findTextFiles (string textFilesfoodtable, string textFilesfoodbku, string textFilesfoodtableExplainationtxt, bool explain)
		{
			
			if (!File.Exists(textFilesfoodtable))
			{
				
				if (!File.Exists(textFilesfoodbku))
				{
					
					Errors.Handler(new ArgumentNullException (textFilesfoodtable, "program.cs: You need a food.table! Put a food.table file in the Files\\Text\\ folder!"), true, true, 524288);
					
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
			
			var dosanddonts = string.Format(CultureInfo.InvariantCulture, "{0}\n", "You're allowed to use all normal characters, upper and lower case; numbers and letters\nYou're forbidden from using special characters by design. Using them caused too much trouble and so they are disallowed for the time being.");
			
			var explaination = string.Format(CultureInfo.InvariantCulture, "The food.table file is a converted food list.txt file. This new file has greater readability and reliability than the old method, which required iterating through a full list before continuing to another. Obviously this has the potential to break and cause problems.\nThe new format is like so:\n{0}\nName of food\nServing size of food\nCalories per serving of food\ndefiner of food\n{0}\n{1}\n", seperator, dosanddonts);
			
			if (!File.Exists(textFilesfoodtableExplainationtxt) && explain)
			{
				
				File.WriteAllText(textFilesfoodtableExplainationtxt, explaination);
				
				File.SetAttributes(textFilesfoodtableExplainationtxt, FileAttributes.Compressed | FileAttributes.Temporary);
				
			}
			
		}

		private static void Application_ThreadException (object sender, ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
			// here you can log the exception ...
			Errors.Handler(e.Exception, true, true, 524288);
		}

		private static void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
			// here you can log the exception ...
			Errors.Handler((e.ExceptionObject as Exception), true, true, 524288);
		}
		
		private static void ArgumentHandler(string[] args)
		{
			
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
					else if (s.Equals("-nobackup", StringComparison.CurrentCultureIgnoreCase))
					{
						
						GlobalVariables.CreateBackups = false;
						
					}
					else
					{
						
						Errors.Handler(new ArgumentException (s + " is not a valid argument! The program cannot parse it and therefore cannot continue."), true, true, 524288);
						
					}
					
				}
			
		}
		
	}
}
