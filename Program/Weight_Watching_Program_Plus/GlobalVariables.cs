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
		
        public static Dictionary<int, string> foodNameList { get; set; }

        public static Dictionary<int, float> servingSizeList { get; set; }

        public static Dictionary<int, float> caloriesPerServingList { get; set; }

        public static Dictionary<int, string> definersList { get; set; }

        public static float totalCaloriesPerDay { get; private set; }

        public static float Calories { get; set; }

        static FoodRelated ()
        {
            foodNameList = new Dictionary<int, string> ();
            servingSizeList = new Dictionary<int, float> ();
            caloriesPerServingList = new Dictionary<int, float> ();
            definersList = new Dictionary<int, string> ();
            totalCaloriesPerDay = 2140f;
            //Do this for all your properties
        }
    }

    public static class GlobalVariables
    {
        //Misc global variables
		
        public static DateTime nowDate { get; private set; }

        public static DateTime dateReset;

        public static string registryAppenedValue { get; private set; }

        public const string weightWatchingProgram = "Weight Watching Program+";
        public static string registryMainValue = weightWatchingProgram;

        public static int SelectedListItem { get; set; }

        public static bool ExactSearch { get; set; }

        public static bool AddItem { get; set; }

        public static Form mainForm { get; set; }

        static GlobalVariables ()
        {
            nowDate = DateTime.Now;
            registryAppenedValue = "SOFTWARE\\Wow6432Node\\";
        }
    }
}


