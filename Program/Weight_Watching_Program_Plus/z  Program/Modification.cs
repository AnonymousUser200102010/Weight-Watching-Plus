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
		
		private IPopup PopupHandler;

		private IValidation Validation;

		private IStorage Storage;

		private IMathematics Mathematics;

		private IGeneralFunctions Functions;

		private IRetrieval Retrieval;

		private IMainForm MainForm;

		public Modification (IPopup pH, IValidation validation, IStorage storage, IMathematics math, IGeneralFunctions _func, IRetrieval retrieve, IMainForm mainForm)
		{
			
			this.PopupHandler = pH;
			
			this.Validation = validation;
			
			this.Storage = storage;
			
			this.Mathematics = math;
			
			this.Functions = _func;
			
			this.Retrieval = retrieve;
			
			this.MainForm = mainForm;
			
		}

		public void ModifyCalories (object sender)
		{
			
			bool senderIsSubtracting = sender.ToString().Contains("subtract", StringComparison.OrdinalIgnoreCase);
			
			bool add = !senderIsSubtracting;
			
			int errorNum = !senderIsSubtracting ? 1 : 2;
			
			string warningText = string.Format(CultureInfo.InvariantCulture, "The amount of calories that {0}", !senderIsSubtracting ? "you are trying to add would put you over your daily limit, and is not allowed." : "are about to be subtracted would put you below your daily limit! Continue?");
			
			var registryCaloriesLeft = double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture);
			
			double tempcalories = registryCaloriesLeft;
			
			if (add)
			{
				
				tempcalories += this.Mathematics.GetFinalCalories(add, this.Validation);
				
			}
			else
			{
				
				tempcalories -= this.Mathematics.GetFinalCalories(add, this.Validation);
				
				if (tempcalories < -(double)MainForm.DefaultCalories)
				{
					
					this.PopupHandler.CreatePopup("You're not allowed to subtract more than the normal daily allowance from a negative calorie value.", 1);
					
					return;
					
				}
				
			}
			
			if (GlobalVariables.Debug)
			{
				
				Console.WriteLine(((double)MainForm.DefaultCalories + (double)MainForm.GlobalMinimumValue));
				
			}
			
			if (((tempcalories < 0f && !add) || (tempcalories >= ((double)MainForm.DefaultCalories + (double)MainForm.GlobalMinimumValue) && add)) && this.PopupHandler.CreatePopup(warningText, errorNum) != DialogResult.Yes)
			{
				
				return;
				
			}
			
			this.Storage.WriteRegistry(tempcalories.ToString(CultureInfo.CurrentCulture), "calories left", false, this.Validation, this.Retrieval);
			
			registryCaloriesLeft = double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture);

			MainForm.UserSetCalories((decimal)registryCaloriesLeft);
			
			this.Validation.CheckCurrentRadioButton(this);
			
			this.Storage.WriteFoodEaten(add, this.Validation, this.Retrieval);
			
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
			
			var registryTuple = Tuple.Create(DateTime.Parse(this.Retrieval.GetRegistryValue("reset date"), CultureInfo.InvariantCulture), this.Retrieval.GetRegistryValue("calories left"));
			
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
			
			if (!MainForm.IsCreatingNewFoodItem)
			{
				
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
				
				FoodRelated.CombinedFoodList.Add(combinedTuple);
				
			}
			
			this.Storage.WriteFoodTable(MainForm.IsCreatingNewFoodItem ? combinedTuple : new Tuple<string, double, double, string, bool> (null, 0f, 0f, null, false));
			
			MainForm.IsCreatingNewFoodItem = false;
			
			this.Functions.RefreshFoodList(this.Storage, this.Retrieval, this.MainForm);
			
		}

		public void ModifyFoodPropertiesList ()
		{
			
			ModifyFoodPropertiesList(null, null, null, true);
			
		}

		public void ModifyFoodPropertiesList (string[] stringProperties, decimal[] decimalProperties, bool[] boolProperties)
		{
			
			ModifyFoodPropertiesList(stringProperties, decimalProperties, boolProperties, false);
			
		}

		private void ModifyFoodPropertiesList (string[] stringProperties, decimal[] decimalProperties, bool[] boolProperties, bool clear)
		{
			
			if (clear)
			{
				
				this.MainForm.FoodNameProperty = null;
				
				this.MainForm.ServingSizeProperty = this.MainForm.ServingSizeMinimumValue;
				
				this.MainForm.CaloriesPerServingProperty = (decimal)this.MainForm.GlobalMinimumValue;
				
				this.MainForm.DefinerProperty = null;
				
				this.MainForm.IsDrinkProperty = false;
				
			}
			else
			{
				
				this.MainForm.FoodNameProperty = stringProperties [0];
				
				this.MainForm.DefinerProperty = stringProperties [1];
				
				this.MainForm.ServingSizeProperty = decimalProperties [0];
				
				this.MainForm.CaloriesPerServingProperty = decimalProperties [1];
				
				this.MainForm.IsDrinkProperty = boolProperties [0];
				
			}
			
		}
		
	}
	
}


