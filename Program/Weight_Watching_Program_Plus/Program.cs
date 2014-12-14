using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Weight_Watching_Program_Plus
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	
	public static class GlobalVariables
	{
		public static Dictionary<int, string> foodNameList = new Dictionary<int, string> ();
		public static Dictionary<int, float> servingSizeList = new Dictionary<int, float> ();
		public static Dictionary<int, float> caloriesPerServingList = new Dictionary<int, float> ();
		public static Dictionary<int, string> definersList = new Dictionary<int, string> ();
		
		public static int selectedListItem;
		
		public static bool exactSearch;
		public static bool addItem;
		
		public static readonly DateTime nowDate = DateTime.Now;
		public static DateTime dateReset;
		
		public static float calories;
		public static readonly float totalCaloriesPerDay = 2140f;
		
		public static readonly string registryAppenedValue = "SOFTWARE\\Wow6432Node\\";

		const string weightWatchingProgram = "Weight Watching Program+";

		public static string registryMainValue = weightWatchingProgram;
		
		public static Form mainForm;
	}

	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		
		[STAThread]
		private static void Main (string[] args)
		{
			#if DEBUG
			GlobalVariables.registryMainValue = string.Format ("{0}~debug", GlobalVariables.registryMainValue);
			#endif
			
			const string seperator = "-------------------------------------------------------------------------\n";
			const string explaination = "The food.table file is a converted food list.txt file. This new file has greater readability and reliability than the old method, which required iterating through a full list before continuing to another. Obviously this has the potential to break and cause problems.\nThe new format is like so:\n" + seperator + "Name of food\nServing size of food\nCalories per serving of food\ndefiner of food\n" + seperator;
			
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			if (!Directory.Exists ("Text Files")) {
				Directory.CreateDirectory ("Text Files");
			}
			const string textFilesfoodtableExplainationtxt = "Text Files\\food.table explaination.txt";
			if (!File.Exists (textFilesfoodtableExplainationtxt)) {
				File.WriteAllText (textFilesfoodtableExplainationtxt, explaination);
			}
			const string textFilesfoodtable = "Text Files\\food.table";
			const string textFilesfoodTabletxt = "Text Files\\food table.txt";
			const string textFilesfoodbku = "Text Files\\food.bku";
			if (!File.Exists (textFilesfoodtable)) {
				if (File.Exists (textFilesfoodTabletxt)) {
					File.WriteAllText (textFilesfoodtable, null);
				} else {
					File.WriteAllText ("Error.Program.cs.dmp", "You need a food.table! Put a food.table file in the text files folder!");
					return;
				}
			} else {
				if (!File.Exists (textFilesfoodbku)) {
					File.Copy (textFilesfoodtable, textFilesfoodbku);
				} else {
					File.Delete (textFilesfoodbku);
					File.Copy (textFilesfoodtable, textFilesfoodbku);
				}
			}
			Storage.readRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
			Functions.checkDateValidity (GlobalVariables.nowDate, GlobalVariables.dateReset, Storage.checkRegistryValues (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue));
			Application.Run (new MainForm ());
		}
	}
}
