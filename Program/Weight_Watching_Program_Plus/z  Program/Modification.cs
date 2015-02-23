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
		
		PopupHandler PopupHandler = new PopupHandler ();
		
		#region Modify Calories Summary
		/// <summary>
		/// Performs the main operation when modifying the user's calorie balance.
		/// </summary>
		/// <param name="sender">
		/// Who or what triggered this function.
		/// </param>
		#endregion
		internal void ModifyCalories(object sender)
		{
			
			bool senderIsSubtracting = sender.ToString().Contains("subtract", StringComparison.OrdinalIgnoreCase);
			
			bool add = !senderIsSubtracting;
			
			int errorNum = !senderIsSubtracting ? 1 : 2;
			
			string warningText = string.Format(CultureInfo.InvariantCulture, "The amount of calories that {0}", !senderIsSubtracting ? "you are trying to add would put you over your daily limit, and is not allowed." : "are about to be subtracted would put you below your daily limit! Continue?");
			
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			float tempcalories = registryTuple.Item2;
			
			Mathematics Mathematics = new Mathematics ();
			
			if (add)
			{
				
				tempcalories += Mathematics.GetFinalCalories(add);
				
			}
			else
			{
				
				tempcalories -= Mathematics.GetFinalCalories(add);
				
				if (tempcalories < -registryTuple.Item3)
				{
					
					PopupHandler.CreatePopup("You're not allowed to subtract more than the normal daily allowance from a negative calorie value.", null, 1, false);
					
					return;
					
				}
				
			}
			
			if ( ((tempcalories < 0f && !add) || (tempcalories > registryTuple.Item3 && add)) && PopupHandler.CreatePopup(warningText, null, errorNum, false) != DialogResult.Yes )
			{
				
				return;
				
			}
			
			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, DateTime.Now.AddDays(1), tempcalories, registryTuple.Item3, new [] {
				false,
				false
			});

			MainForm.UserSetCalories = (decimal)registryTuple.Item2;
			
			Validation.CheckCurrentRadioButton();
			
			Storage.WriteFoodEaten("Files\\Text\\", "Food Diary.txt", add);
			
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
		/// <exception cref="T:System.InvalidCastException">
		/// Thrown when attempting to use an object type that is not yet supported.
		/// </exception>
		#endregion
		public static void WriteToObject (Label labelToChange, TextBox textBoxToChange, int objectNumber)
		{
			
			string[] messages = {
				string.Format(CultureInfo.InvariantCulture, "Calories Left For The Day: {0}", Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item2),
				
				string.Format(CultureInfo.InvariantCulture, "Calories will reset on {0:MMMM dd} at {0:hh:mm tt}", Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1),
				"Click Here to Search the Food List"
			};
			
			var font1 = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			var font2 = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
			Font[] fontStyle = {
				font1,
				font1,
				font2
			};
			
			var middleCenter = ContentAlignment.MiddleCenter;
			ContentAlignment[] objectAlignment = {
				middleCenter,
				middleCenter,
				middleCenter
			};
			
			var center = HorizontalAlignment.Center;
			HorizontalAlignment[] horizontalObjectAlignment = {
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
				
				throw new InvalidCastException("writeToObject: All Valid Parameters:");
				
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
			
			var combinedTuple = new Tuple<string, float, float, string, bool>(MainForm.FoodNameProperty, (float)MainForm.ServingSizeProperty, (float)MainForm.CaloriesPerServingProperty, MainForm.DefinerProperty, MainForm.IsDrinkProperty);
			
			if (!MainForm.IsCreatingANewFoodItem)
			{
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
				
				FoodRelated.CombinedFoodList.Add(combinedTuple);
				
			}
			
			Storage.WriteFoodTable("Files\\Text\\", "food.table", MainForm.IsCreatingANewFoodItem ? combinedTuple : new Tuple<string, float, float, string, bool>(null, 0f, 0f, null, false));
			
			MainForm.IsCreatingANewFoodItem = false;
			
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
		/// <param name="boolProperties">
		/// Contains the bool values for all properties that require them. So far, in order, these properties are counted: IsDrinkCheckBox.
		/// </param>
		#endregion
		public static void ModifyFoodPropertiesList(bool clear, string[] stringProperties, decimal[] decimalProperties, bool[] boolProperties)
		{
			if (clear)
			{
				MainForm.FoodNameProperty = null;
				MainForm.ServingSizeProperty = 0;
				MainForm.CaloriesPerServingProperty = 0;
				MainForm.DefinerProperty = null;
				MainForm.IsDrinkProperty = false;
			}
			else
			{
				MainForm.FoodNameProperty = stringProperties[0];
				MainForm.DefinerProperty = stringProperties[1];
				MainForm.ServingSizeProperty = decimalProperties[0];
				MainForm.CaloriesPerServingProperty = decimalProperties[1];
				MainForm.IsDrinkProperty = boolProperties[0];
			}
			
		}

		#region Sort Food List Summary
		/// <summary>
		/// Sorts the food list in one function to reduce code bloat.
		/// </summary>
		#endregion
		internal static void SortFoodList()
		{
			
			var sortedEnum = FoodRelated.CombinedFoodList.ToList();
			
			sortedEnum.Sort();
			
			FoodRelated.CombinedFoodList = sortedEnum;
			
		}
		
	}
}


