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
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main (string[] args)
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			if (!Directory.Exists ("Text Files")) {
				Directory.CreateDirectory ("Text Files");
			}
			if (File.Exists ("Text Files\\food table.txt")) {
				Logic.logicMain ();
				if (!File.Exists ("Text Files\\food table.bku")) {
					File.Copy ("Text Files\\food table.txt", "Text Files\\food table.bku");
				} else {
					File.Delete ("Text Files\\food table.bku");
					File.Copy ("Text Files\\food table.txt", "Text Files\\food table.bku");
				}
				Application.Run (new MainForm ());
			} else {
				File.WriteAllText ("Error.Program.cs.dmp", "You need a food table.txt! Put a food table.txt in the text files folder!");
			}
		}

		public static class Logic
		{
			
			private static readonly DateTime Date = DateTime.Now;
			private static DateTime Rollover_Date = new DateTime ();

			public static void logicMain ()
			{
				bool First_Program_Use = false;
				string regappend = "SOFTWARE\\Wow6432Node\\";
				string reg = "Weight Watching Program+";
				float Total_Calories_Per_Day = 2140f;
				if (Registry.LocalMachine.OpenSubKey (regappend + reg) == null) {
					Registry.LocalMachine.CreateSubKey (regappend + reg);
					using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (regappend + reg, true)) {
						tempKey.SetValue ("Calories Left for the Day", Total_Calories_Per_Day.ToString (), RegistryValueKind.String);
						tempKey.SetValue ("Last Used Date", Date.AddDays (1).ToString ("yyyy MMMMM dd hh:mm:ss tt"), RegistryValueKind.String);
						First_Program_Use = true;
					}
				}
				DateTime.TryParseExact (getRolloverDate (Date, reg, regappend), new string[] { "yyyy MMMMM dd hh:mm:ss tt" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out Rollover_Date);
				if (DateTime.Compare (Date, Rollover_Date) == 1 || First_Program_Use == true) {
					if (Registry.LocalMachine.OpenSubKey (regappend + reg) == null) {
						Registry.LocalMachine.CreateSubKey (regappend + reg);
					}
					using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (regappend + reg, true)) {
						tempKey.SetValue ("Calories Left for the Day", Total_Calories_Per_Day.ToString (), RegistryValueKind.String);
						tempKey.SetValue ("Last Used Date", Date.AddDays (1).ToString ("yyyy MMMMM dd hh:mm:ss tt"), RegistryValueKind.String);
					}
				}
			}

			private static string getRolloverDate (DateTime Date, string reg, string regappend)
			{
				using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (regappend + reg, true)) {
					string temp = tempKey.GetValue ("Last Used Date").ToString ();
					return temp;
				}
			}
		}
	}
}
