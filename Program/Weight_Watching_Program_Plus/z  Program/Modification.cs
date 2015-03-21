#region Using Directives

using System;
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
	internal class Modification : IModification
	{
		
		IPopup PopupHandler;
			
		IValidation Validation;
			
		IStorage Storage;
			
		IMathematics Mathematics;
			
		IGeneralFunctions Functions;
		
		public Modification(PopupHandler pH, Validation validation, Storage storage, Mathematics math, Functions _func)
		{
			
			this.PopupHandler = pH;
			this.Validation = validation;
			this.Storage = storage;
			this.Mathematics = math;
			this.Functions = _func;
			
		}

		public void ModifyCalories (object sender)
		{
			
			bool senderIsSubtracting = sender.ToString().Contains("subtract", StringComparison.OrdinalIgnoreCase);
			
			bool add = !senderIsSubtracting;
			
			int errorNum = !senderIsSubtracting ? 1 : 2;
			
			string warningText = string.Format(CultureInfo.InvariantCulture, "The amount of calories that {0}", !senderIsSubtracting ? "you are trying to add would put you over your daily limit, and is not allowed." : "are about to be subtracted would put you below your daily limit! Continue?");
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation, false);
			
			double tempcalories = registryTuple.Item2;
			
			if (add)
			{
				
				tempcalories += this.Mathematics.GetFinalCalories(add, this.Validation);
				
			}
			else
			{
				
				tempcalories -= this.Mathematics.GetFinalCalories(add, this.Validation);
				
				if (tempcalories < -registryTuple.Item3)
				{
					
					this.PopupHandler.CreatePopup("You're not allowed to subtract more than the normal daily allowance from a negative calorie value.", 1);
					
					return;
					
				}
				
			}
			
			string decimalPlacesLiteral = null;
			
			for (int i = 1; i < registryTuple.Item6; i++)
			{
				
				decimalPlacesLiteral += "0";
				
				#if DEBUG
				
				//Console.WriteLine(decimalPlacesLiteral);
				
				#endif
				
			}
			
			if(GlobalVariables.Debug)
			{
				
				Console.WriteLine(decimalPlacesLiteral);
				
				//Console.WriteLine(string.Format(CultureInfo.CurrentCulture, ".{0}1", decimalPlacesLiteral));
				
				//Console.WriteLine(this.Mathematics.PerformArithmeticOperation(string.Format(CultureInfo.CurrentCulture, "{0}+.{1}1", registryTuple.Item3, decimalPlacesLiteral)));
				
				Console.WriteLine(double.Parse(string.Format(CultureInfo.CurrentCulture, "{0}.{1}1", registryTuple.Item3, decimalPlacesLiteral), CultureInfo.CurrentCulture));
				
			}
			
			if (((tempcalories < 0f && !add) || (tempcalories >= double.Parse(string.Format(CultureInfo.CurrentCulture, "{0}.{1}1", registryTuple.Item3, decimalPlacesLiteral), CultureInfo.InvariantCulture) && add)) && this.PopupHandler.CreatePopup(warningText, errorNum) != DialogResult.Yes)
			{
				
				return;
				
			}
			
			this.Storage.WriteRegistry(tempcalories, registryTuple.Item3, registryTuple.Item6, this.Validation);
			
			registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation);

			MainForm.UserSetCalories = (decimal)registryTuple.Item2;
			
			this.Validation.CheckCurrentRadioButton(this);
			
			MainForm.DecimalExample = registryTuple.Item2.ToString(CultureInfo.CurrentCulture);
			
			this.Storage.WriteFoodEaten(add, this.Validation);
			
		}

		public void WriteToObject (Label labelToChange, int objectNumber)
		{
			
			WriteToObject(labelToChange, null, objectNumber);
			
		}

		public void WriteToObject (TextBox textBoxToChange, int objectNumber)
		{
			
			WriteToObject(null, textBoxToChange, objectNumber);
			
		}

		private void WriteToObject (Label labelToChange, TextBox textBoxToChange, int objectNumber)
		{
			
			var registryTuple = this.Storage.GetRetrievableRegistryValues(this.Validation);
			
			string[] messages = {
				string.Format(CultureInfo.InvariantCulture, "Calories Left For The Day: {0}", registryTuple.Item2),
				
				string.Format(CultureInfo.InvariantCulture, "Calories will reset on {0:MMMM dd} at {0:hh:mm tt}", registryTuple.Item1),
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
			
			if (labelToChange != null)
			{
				
				labelToChange.Font = fontStyle [objectNumber];
			
				labelToChange.Text = messages [objectNumber];
			
				labelToChange.TextAlign = objectAlignment [objectNumber];
				
			}
			else if (textBoxToChange != null)
			{
				
				textBoxToChange.Font = fontStyle [objectNumber];
				
				textBoxToChange.Text = messages [objectNumber];
				
				textBoxToChange.TextAlign = horizontalObjectAlignment [objectNumber];
				
			}
			else
			{
				
				throw new InvalidCastException ("writeToObject: All Valid Parameters:");
				
			}
			
		}

		public void ModifyFoodItemProperty (bool allValidEntries)
		{
			
			if (!allValidEntries)
			{
				
				return;
				
			}
			
			var combinedTuple = new Tuple<string, double, double, string, bool> (MainForm.FoodNameProperty, (double)MainForm.ServingSizeProperty, (double)MainForm.CaloriesPerServingProperty, MainForm.DefinerProperty, MainForm.IsDrinkProperty);
			
			if (!MainForm.IsCreatingANewFoodItem)
			{
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
				
				FoodRelated.CombinedFoodList.Add(combinedTuple);
				
			}
			
			this.Storage.WriteFoodTable(MainForm.IsCreatingANewFoodItem ? combinedTuple : new Tuple<string, double, double, string, bool> (null, 0f, 0f, null, false));
			
			MainForm.IsCreatingANewFoodItem = false;
			
			this.Functions.RefreshFoodList(this.Storage);
			
		}

		public void ModifyFoodPropertiesList ()
		{
			
			ModifyFoodPropertiesList(null, null, null, true);
			
		}

		public void ModifyFoodPropertiesList (string[] stringProperties, decimal[] decimalProperties, bool[] boolProperties)
		{
			
			ModifyFoodPropertiesList(stringProperties, decimalProperties, boolProperties, false);
			
		}

		private static void ModifyFoodPropertiesList (string[] stringProperties, decimal[] decimalProperties, bool[] boolProperties, bool clear)
		{
			if (clear)
			{
				MainForm.FoodNameProperty = null;
				MainForm.ServingSizeProperty = MainForm.ServingSizeMinimumValue;
				MainForm.CaloriesPerServingProperty = MainForm.CaloriesPerServingMinimumValue;
				MainForm.DefinerProperty = null;
				MainForm.IsDrinkProperty = false;
			}
			else
			{
				MainForm.FoodNameProperty = stringProperties [0];
				MainForm.DefinerProperty = stringProperties [1];
				MainForm.ServingSizeProperty = decimalProperties [0];
				MainForm.CaloriesPerServingProperty = decimalProperties [1];
				MainForm.IsDrinkProperty = boolProperties [0];
			}
			
		}
		
	}
}


