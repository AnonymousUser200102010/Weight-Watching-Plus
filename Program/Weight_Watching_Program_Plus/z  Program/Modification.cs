#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using UniversalHandlersLibrary;
#endregion

namespace WeightWatchingProgramPlus
{
	
	/// <summary>
	/// Functions whose primary purpose is modification and creation, but who don't have a more pressing primary function.
	/// </summary>
	internal class Modification
	{

		private PopupHandler PopupHandler = new PopupHandler ();

		#region Modify Calories Summary
		/// <summary>
		/// Sub-logic handler for modification of calories. Simply to reduce code bloat and make understanding the final processes easier.
		/// </summary>
		/// <param name="add">
		/// Are you adding calories?
		/// </param>
		/// <returns>
		/// Returns the calorie value after the logical operations have been finished.
		/// </returns>
		#endregion
		internal float ModifyCalories(bool add)
		{
			
			int hour = DateTime.Now.Hour;
			int unsafeHourThreshold = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1.Hour - 8;
			string amPMDefiner = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
			
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			float tempFloat = FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item3 * (float)MainForm.UserProvidedServings / FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item2;
			
			if (hour >= unsafeHourThreshold && Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1.Day == DateTime.Now.Day && Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1.ToString("tt", CultureInfo.InvariantCulture).Equals(amPMDefiner, StringComparison.OrdinalIgnoreCase))
			{
				
				float midSnackPenalty = tempFloat / 10;
				
				if (midSnackPenalty < 10)
				{
					
					midSnackPenalty = 10;
					
				}
				else if (midSnackPenalty > Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3 / 2)
				{
					
					midSnackPenalty = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item3 / 2;
						
				}
				
				if (this.PopupHandler.CreatePopup(string.Format(CultureInfo.InvariantCulture, "An eating before bed {2} of {0} calories will be {1} your daily calorie count if you continue.", midSnackPenalty, !add ? "subtracted from" : "added to", !add ? "penalty" : "benefit"), null, 3, false) != DialogResult.OK)
				{
					
					return 0;
					
				}
				
				return tempFloat + midSnackPenalty;
				
			}
			
			return tempFloat;
			
		}

		#region Write To Object Summary
		/// <summary>
		/// A lightweight modular text changer for use on a variety of objects.
		/// </summary>
		/// <param name="labelToChange">
		/// The Label whos text value will be changed (if applicible)
		/// </param>
		/// <param name="textBoxToChange">
		/// The TextBox whos text value will be changed (if applicible)
		/// </param>
		/// <param name="objectNumber">
		/// The number used to handle the operation.
		/// </param>
		#endregion
		public static void WriteToObject(Label labelToChange, TextBox textBoxToChange, int objectNumber)
		{
			
			string[] messages =  {
				string.Format(CultureInfo.InvariantCulture, "Calories Left For The Day: {0}", Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2),
				string.Format(CultureInfo.InvariantCulture, "Calories will reset on {0:MMMM dd} at {0:hh:mm tt}", Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1),
				"Click Here to Search the Food List"
			};
			
			var font1 = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
			var font2 = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
			Font[] fontStyle =  {
				font1,
				font1,
				font2
			};
			
			var middleCenter = ContentAlignment.MiddleCenter;
			ContentAlignment[] objectAlignment =  {
				middleCenter,
				middleCenter,
				middleCenter
			};
			
			var center = HorizontalAlignment.Center;
			HorizontalAlignment[] horizontalObjectAlignment =
			{
				center,
				center,
				center
			};
			
			if(labelToChange != null)
			{
				
				labelToChange.Font = fontStyle[objectNumber];
			
				labelToChange.Text = messages[objectNumber];
			
				labelToChange.TextAlign = objectAlignment[objectNumber];
				
			}
			else if (textBoxToChange != null)
			{
				
				textBoxToChange.Font = fontStyle[objectNumber];
				
				textBoxToChange.Text = messages[objectNumber];
				
				textBoxToChange.TextAlign = horizontalObjectAlignment[objectNumber];
				
			}
			else
			{
				
				Errors.Handler(new NullReferenceException("writeToObject: All Valid Parameters:"), true, true, 524288);
				
			}
		}

		#region Food Item Modification Summary
		/// <summary>
		/// Changes the property of an existing food item or creates a new food item.
		/// </summary>
		/// <param name="AllValidEntries">
		/// All property entries are valid.
		/// </param>
		#endregion
		internal static void ModifyFoodItemProperty(bool AllValidEntries)
		{
			
			if (!AllValidEntries)
			{
				
				return;
				
			}
			
			if (!MainForm.IsCreatingANewFoodItem)
			{
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
				
				FoodRelated.CombinedFoodList.Add(new Tuple<string, float, float, string, bool>(MainForm.FoodNameProperty, (float)MainForm.ServingSizeProperty, (float)MainForm.CaloriesPerServingProperty, MainForm.DefinerProperty, MainForm.IsDrinkProperty));
				
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string, bool>(null, 0f, 0f, null, false));
				
			}
			else
			{
				
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string, bool>(MainForm.FoodNameProperty, (float)MainForm.ServingSizeProperty, (float)MainForm.CaloriesPerServingProperty, MainForm.DefinerProperty, MainForm.IsDrinkProperty));
				
				MainForm.IsCreatingANewFoodItem = false;
				
			}
			
			Functions.Refresh_foodList();
			
		}

		#region Clear Food Property Boxes Summary
		/// <summary>
		/// Clears the property settings boxes.
		/// </summary>
		/// <param name="clear">
		/// Clears all of the property boxes.
		/// </param>
		/// <param name="stringProperties">
		/// Contains the strings for all properties that require them. So far, in order, these properties are counted: FoodName, Definer
		/// </param>
		/// <param name="decimalProperties">
		/// Contains the decimal values for all properties that require them. So far, in order, these properties are counted: ServingSize, CaloriesPerServing.
		/// </param>
		#endregion
		public static void ModifyFoodPropertiesList(bool clear, string[] stringProperties, decimal[] decimalProperties)
		{
			if (clear)
			{
				MainForm.FoodNameProperty = null;
				MainForm.ServingSizeProperty = 0;
				MainForm.CaloriesPerServingProperty = 0;
				MainForm.DefinerProperty = null;
			}
			else
			{
				MainForm.FoodNameProperty = stringProperties[0];
				MainForm.DefinerProperty = stringProperties[1];
				MainForm.ServingSizeProperty = decimalProperties[0];
				MainForm.CaloriesPerServingProperty = decimalProperties[1];
			}
			
		}

		/// <summary>
		/// Sorts the food list in one function to reduce code bloat.
		/// </summary>
		internal static void SortFoodList()
		{
			
			List<Tuple<string, float, float, string, bool>> sortedEnum = FoodRelated.CombinedFoodList.ToList();
			
			sortedEnum.Sort();
			
			FoodRelated.CombinedFoodList = sortedEnum;
			
		}
		
	}
}


