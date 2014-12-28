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
        
        public static IList<Tuple<string, float, float, string>> CombinedFoodList { get; set; }
        #region Tuple Structure
       	//Name of food
       	//Serving Size
       	//Calories per Serving
       	//Definer
        #endregion

        public static readonly float TotalCaloriesPerDay = 2140f;

        public static float Calories { get; set; }
        
        static FoodRelated()
        {
        	
        	CombinedFoodList = new List<Tuple<string, float, float, string>>();
        	
        }
        
    }

    public static class GlobalVariables
    {
        //Misc global variables

        public static string RegistryAppendedValue { get; private set; }

        public const string WeightWatchingProgram = "Weight Watching Program+";
        
        public static string RegistryMainValue { get; set; }

        public static int SelectedListItem { get; set; }

        public static Form MainForm { get; set; }

        static GlobalVariables ()
        {
            
            RegistryAppendedValue = "SOFTWARE\\Wow6432Node\\";
            
            RegistryMainValue = WeightWatchingProgram;
            
        }
        
    }
}


