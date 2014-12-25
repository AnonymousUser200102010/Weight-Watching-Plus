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
		private static void Main ()
		{
			try
			{
				#if DEBUG
				GlobalVariables.RegistryMainValue = string.Format(CultureInfo.InvariantCulture, "{0}~debug", GlobalVariables.RegistryMainValue);
				#endif
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				if (!Directory.Exists("Files"))
				{
					Directory.CreateDirectory("Files");
				}
				if (!Directory.Exists("Files\\Text\\"))
				{
					Directory.CreateDirectory("Files\\Text\\");
				}
				findFoodTable("Files\\Text\\food.table", "Files\\Text\\food table.txt", "Files\\Text\\food.bku", "Files\\Text\\food.table explaination.txt");
	            
				Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
				Validation.CheckDateValidity(GlobalVariables.NowDate, GlobalVariables.DateReset, Storage.CheckRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue));
				Application.Run(new MainForm ());
			}
			catch (Exception e)
			{
				Errors.Handler(e, true);
			}
		}

		public static void findFoodTable (string textFilesfoodtable, string textFilesfoodTabletxt, string textFilesfoodbku, string textFilesfoodtableExplainationtxt)
		{
			if (!File.Exists(textFilesfoodtable))
			{
				if (!File.Exists(textFilesfoodTabletxt))
				{
					Errors.Handler(new ArgumentNullException (textFilesfoodtable, "program.cs: You need a food.table! Put a food.table file in the Files\\Text\\ folder!"), true);
					return;
				}
				File.WriteAllText(textFilesfoodtable, null);
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
			}
			const string seperator = "-------------------------------------------------------------------------\n";
			var explaination = string.Format(CultureInfo.InvariantCulture, "The food.table file is a converted food list.txt file. This new file has greater readability and reliability than the old method, which required iterating through a full list before continuing to another. Obviously this has the potential to break and cause problems.\nThe new format is like so:\n{0}\nName of food\nServing size of food\nCalories per serving of food\ndefiner of food\n{0}", seperator);
			if (!File.Exists(textFilesfoodtableExplainationtxt))
			{
				File.WriteAllText(textFilesfoodtableExplainationtxt, explaination);
			}
		}
	}
}
