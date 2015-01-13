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
		/// <param name="userServingInputTextBox">
		/// The NumericUpDown for the "number of servings" value the user has input.
		/// </param>
		/// <param name="add">
		/// Are you adding calories?
		/// </param>
		/// <returns>
		/// Returns the calorie value after the logical operations have been finished.
		/// </returns>
		#endregion
		internal float ModifyCalories(NumericUpDown userServingInputTextBox, bool add)
		{
			
			int hour = DateTime.Now.Hour;
			int unsafeHourThreshold = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue).Item1.Hour - 8;
			string amPMDefiner = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
			
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			
			float tempFloat = FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item3 * float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item2;
			
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
		/// <param name="foodList">
		/// The food list ListBox.
		/// </param>
		/// <param name="foodNameEditBox">
		/// The edit box for the name of the food.
		/// </param>
		/// <param name="servingSizeEditBox">
		/// The NumericUpDown for the serving size value of the food.
		/// </param>
		/// <param name="caloriesPerServingEditBox">
		/// The NumericUpDown for the calories per serving value of the food.
		/// </param>
		/// <param name="definerEditBox">
		/// The edit box for the definer value of the food.
		/// </param>
		/// <param name="newItemCheckbox">
		/// The check box which defines if an item is being added or not.
		/// </param>
		#endregion
		internal void ModifyFoodItemProperty(ListBox foodList, TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox)
		{
			
			if (!Validation.EditBoxesHaveValidEntries(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, newItemCheckbox, this.PopupHandler))
			{
				
				return;
				
			}
			
			if (!newItemCheckbox.Checked)
			{
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
				
				FoodRelated.CombinedFoodList.Add(new Tuple<string, float, float, string>(foodNameEditBox.Text, float.Parse(servingSizeEditBox.Text, CultureInfo.CurrentCulture), float.Parse(caloriesPerServingEditBox.Text, CultureInfo.CurrentCulture), definerEditBox.Text));
				
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string>(null, 0f, 0f, null));
				
			}
			else
			{
				
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new Tuple<string, float, float, string>(foodNameEditBox.Text, float.Parse(servingSizeEditBox.Text, CultureInfo.InvariantCulture), float.Parse(caloriesPerServingEditBox.Text, CultureInfo.InvariantCulture), definerEditBox.Text));
				
				newItemCheckbox.Checked = false;
				
			}
			
			Functions.Refresh_foodList(foodList);
			
		}

		#region Clear Food Property Boxes Summary
		/// <summary>
		/// Clears the property settings boxes.
		/// </summary>
		/// <param name="foodNameEditBox">
		/// The edit box for the name of the food.
		/// </param>
		/// <param name="servingSizeEditBox">
		/// The NumericUpDown for the serving size value of the food.
		/// </param>
		/// <param name="caloriesPerServingEditBox">
		/// The NumericUpDown for the calories per serving value of the food.
		/// </param>
		/// <param name="definerEditBox">
		/// The edit box for the definer value of the food.
		/// </param>
		#endregion
		public static void DumpFoodPropertiesList(TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox)
		{
			
			foodNameEditBox.Clear();
			servingSizeEditBox.Value = 0;
			caloriesPerServingEditBox.Value = 0;
			definerEditBox.Clear();
			
		}

		#region
		/// <summary>
		/// Creates a pre-made seperator.
		/// </summary>
		/// <param name="Seperator">
		/// The label that will be transformed into a seperator
		/// </param>
		/// <param name="vertical">
		/// Is this a vertical seperator?
		/// </param>
		#endregion
		internal static void ModifySeperator(Label Seperator, bool vertical)
		{
			
			const BorderStyle fixed3D = BorderStyle.Fixed3D;
			
			if (vertical)
			{
				
				Seperator.AutoSize = false;
				Seperator.BorderStyle = fixed3D;
				Seperator.Width = 1;
				
			}
			else
			{
				
				Seperator.AutoSize = false;
				Seperator.BorderStyle = fixed3D;
				Seperator.Height = 2;
				
			}
			
		}

		/// <summary>
		/// Sorts the food list in one function to reduce code bloat.
		/// </summary>
		internal static void SortFoodList()
		{
			
			List<Tuple<string, float, float, string>> sortedEnum = FoodRelated.CombinedFoodList.ToList();
			sortedEnum.Sort();
			FoodRelated.CombinedFoodList = sortedEnum;
			
		}
		
	}
}


