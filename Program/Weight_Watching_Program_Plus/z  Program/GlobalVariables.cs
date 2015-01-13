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
        /// </example>
        #endregion
        public static IList<Tuple<string, float, float, string>> CombinedFoodList { get; set; }
        
        static FoodRelated()
        {
        	
        	CombinedFoodList = new List<Tuple<string, float, float, string>>();
        	
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
        /// Program form.
        /// </summary>
        public static Form MainForm { get; set; }
        
        /// <summary>
        /// Stored "additional settings" DateTimePicker.
        /// </summary>
        public static DateTimePicker ExactResetDateTimePicker { get; set; }

        static GlobalVariables ()
        {
            
            RegistryAppendedValue = "SOFTWARE\\Wow6432Node\\";
            
            RegistryMainValue = WeightWatchingProgram;
            
        }
        
    }
}


