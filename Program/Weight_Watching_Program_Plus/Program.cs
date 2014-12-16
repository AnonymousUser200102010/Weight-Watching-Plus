using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// 
		
		[STAThread]
		static void Main ()
		{
			Storage Storage = new Storage();
			Functions Functions = new Functions ();
        	ErrorHandler ErrorHandler = new ErrorHandler ();
			
			#if DEBUG
			GlobalVariables.registryMainValue = string.Format("{0}~debug", GlobalVariables.registryMainValue);
			#endif
			
			const string seperator = "-------------------------------------------------------------------------\n";
			var explaination = string.Format("The food.table file is a converted food list.txt file. This new file has greater readability and reliability than the old method, which required iterating through a full list before continuing to another. Obviously this has the potential to break and cause problems.\nThe new format is like so:\n-------------------------------------------------------------------------\nName of food\nServing size of food\nCalories per serving of food\ndefiner of food\n{0}", seperator);
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (!Directory.Exists("Text Files"))
			{
				Directory.CreateDirectory("Text Files");
			}
			const string textFilesfoodtableExplainationtxt = "Text Files\\food.table explaination.txt";
			if (!File.Exists(textFilesfoodtableExplainationtxt))
			{
				File.WriteAllText(textFilesfoodtableExplainationtxt, explaination);
			}
			const string textFilesfoodtable = "Text Files\\food.table";
			const string textFilesfoodTabletxt = "Text Files\\food table.txt";
			const string textFilesfoodbku = "Text Files\\food.bku";
			if (!File.Exists(textFilesfoodtable))
			{
				if (!File.Exists(textFilesfoodTabletxt))
				{
					File.WriteAllText("Error.Program.cs.dmp", "You need a food.table! Put a food.table file in the text files folder!");
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
			Storage.ReadRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
			Validation.CheckDateValidity(GlobalVariables.nowDate, GlobalVariables.dateReset, Storage.CheckRegistryValues(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue), Storage);
			Application.Run(new MainForm ());
		}
	}
}
