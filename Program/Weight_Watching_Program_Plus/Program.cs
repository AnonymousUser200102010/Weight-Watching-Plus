using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Management;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
		public static Dictionary<int, bool> ignoreList = new Dictionary<int, bool> ();
		
		public static int numentries;
		public static int selectedListItem;
		
		public static bool exactSearch = false;
		public static bool addItem = false;
		public static bool allowLastActionInAnEvent = false;
		
		public static DateTime nowDate = DateTime.Now;
		public static DateTime dateReset;
		
		public static float calories;
		public static float totalCaloriesPerDay = 2140f;
		
		public static readonly string registryAppenedValue = "SOFTWARE\\Wow6432Node\\";
		public static string registryMainValue = "Weight Watching Program+";
		
		public static Boolean isDebugMode = false;
		
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
			GlobalVariables.registryMainValue = GlobalVariables.registryMainValue + "~debug";
			#endif
			
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			if (!Directory.Exists ("Text Files")) {
				Directory.CreateDirectory ("Text Files");
			}
			if (File.Exists ("Text Files\\food table.txt")) {
				if (!File.Exists ("Text Files\\food table.bku")) {
					File.Copy ("Text Files\\food table.txt", "Text Files\\food table.bku");
				} else {
					File.Delete ("Text Files\\food table.bku");
					File.Copy ("Text Files\\food table.txt", "Text Files\\food table.bku");
				}
				Storage.readRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
				Functions.checkDateValidity (GlobalVariables.nowDate, GlobalVariables.dateReset, Storage.checkRegistryValues (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue));
				Application.Run (new MainForm ());
			} else {
				File.WriteAllText ("Error.Program.cs.dmp", "You need a food table.txt! Put a food table.txt in the text files folder!");
			}
		}
	}
}
