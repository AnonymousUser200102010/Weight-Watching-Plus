using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
    /// <summary>
    /// Global Variables.
    /// </summary>
    /// 
	
    public static class FoodRelated
    {
        //Variabled directly linked to food or the creation of the food table.
		
        public static Dictionary<int, string> FoodNameList { get; set; }

        public static Dictionary<int, float> ServingSizeList { get; set; }

        public static Dictionary<int, float> CaloriesPerServingList { get; set; }

        public static Dictionary<int, string> DefinersList { get; set; }

        public static readonly float TotalCaloriesPerDay = 2140f;

        public static float Calories { get; set; }

        static FoodRelated ()
        {
            FoodNameList = new Dictionary<int, string> ();
            ServingSizeList = new Dictionary<int, float> ();
            CaloriesPerServingList = new Dictionary<int, float> ();
            DefinersList = new Dictionary<int, string> ();
        }
    }

    public static class GlobalVariables
    {
        //Misc global variables
		
        public static DateTime NowDate { get; private set; }

        public static DateTime DateReset { get; set; }

        public static string RegistryAppendedValue { get; private set; }

        public const string WeightWatchingProgram = "Weight Watching Program+";
        
        public static string RegistryMainValue { get; set; }

        public static int SelectedListItem { get; set; }

        public static Form MainForm { get; set; }

        static GlobalVariables ()
        {
            NowDate = DateTime.Now;
            RegistryAppendedValue = "SOFTWARE\\Wow6432Node\\";
            RegistryMainValue = WeightWatchingProgram;
        }
    }
}


