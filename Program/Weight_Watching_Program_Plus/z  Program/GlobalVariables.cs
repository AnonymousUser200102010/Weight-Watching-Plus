using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
    /// <summary>
    /// Variables directly linked to food or the creation of the food table.
    /// </summary>
    /// 
    public static class FoodRelated
    {
        #region Food list Summary
        /// <summary>
        /// Food items list.
        /// </summary>
        /// <example>
        /// <item1>
        /// Item 1 = name of food
        /// </item1>
        /// </example>
        /// <example>
        /// <item2>
        /// Item 2 = serving size of the food item.
        /// </item2>
        /// </example>
        /// <example>
        /// <item3>
        /// Item 3 = calories per serving of the food item
        /// </item3>
        /// </example>
        /// <example>
        /// <item4>
        /// Item 4 = definer fod the food item.
        /// </item4>
        /// <item5>
        /// Item 5 = drink bool.
        /// </item5>
        /// </example>
        #endregion
        public static IList<Tuple<string, float, float, string, bool>> CombinedFoodList { get; set; }
        
        static FoodRelated()
        {
        	
        	CombinedFoodList = new List<Tuple<string, float, float, string, bool>>();
        	
        }
        
    }

    /// <summary>
    /// Misc global variables.
    /// </summary>
    public static class GlobalVariables
    {

    	/// <summary>
    	/// Registry value that comes first after LOCAL_MACHINE
    	/// </summary>
        public static string RegistryAppendedValue { get; private set; }
        
        /// <summary>
        /// Name of the program as the user sees it.
        /// </summary>
        public const string WeightWatchingProgram = "Weight Watching Program+";
        
        /// <summary>
        /// Registry value that is added after the appended value.
        /// </summary>
        public static string RegistryMainValue { get; set; }

        /// <summary>
        /// The stored item from the food list ListBox that has been determined to be current.
        /// </summary>
        public static int SelectedListItem { get; set; }
        
        /// <summary>
        /// Determines if backups should be made for this program at or after runtime.
        /// </summary>
        public static bool CreateBackups { get; set; }
        
        /// <summary>
        /// Determines if the program should debug regardless of the build type.
        /// </summary>
        public static bool Debug { get; set; }
        

        static GlobalVariables ()
        {
            
            RegistryAppendedValue = "SOFTWARE\\Wow6432Node\\";
            
            RegistryMainValue = WeightWatchingProgram;
            
            CreateBackups = true;
            
            Debug = false;
            
        }
        
    }
}


