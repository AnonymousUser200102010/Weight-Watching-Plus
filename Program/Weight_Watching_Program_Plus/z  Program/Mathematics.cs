#region Using Directives

using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using UniversalHandlersLibrary;

#endregion

namespace WeightWatchingProgramPlus
{

	/// <summary>
	/// Functions whose primary purpose is parsing mathematical operations and performing arithmetic, but who don't have a more pressing primary function.
	/// </summary>
	internal class Mathematics : IMathematics
	{
		
		private IPopup PopupHandler;

		private IRetrieval Retrieval;

		private IMainForm MainForm;

		public Mathematics (IPopup pU, IRetrieval retrieve, IMainForm mainForm)
		{

			this.PopupHandler = pU;
			
			this.Retrieval = retrieve;
			
			this.MainForm = mainForm;
			
		}

		public double PerformArithmeticOperation (string equation)
		{
			
			return PerformArithmeticOperation(equation, false);
			
		}

		public double PerformArithmeticOperation (string equation, bool allowPopups)
		{
			
			double returnDouble = 0f;

			using (DataTable dt = new DataTable ())
			{

				dt.Locale = CultureInfo.InvariantCulture;

				object computation = dt.Compute(equation, null);

				if (!double.TryParse(computation.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out returnDouble))
				{

					throw Errors.PremadeExceptions("PerformArithmeticOperation", "computation", 0);

				}

				if (returnDouble <= 0f && allowPopups)
				{
					
					this.PopupHandler.CreatePopup("You cannot use arithmetic values which create an operation whose final value is zero or less!", 0);
					
					return 0;

				}

			}

			return returnDouble;
		}

		public double GetFinalCalories (bool add, IValidation valid)
		{
			
			this.Retrieval.ReadRegistry();

			var userProvidedServings = MainForm.AddSubSelectedSubTab.Text.Contains("explicit", StringComparison.OrdinalIgnoreCase) ? MainForm.UserProvidedServings : (decimal)(PerformArithmeticOperation(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", MainForm.ArithmeticValue(true), MainForm.GetArithmeticSign, MainForm.ArithmeticValue(false)), true));
			
			double userServings = (FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item3 * ((double)userProvidedServings / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2));
			
			if (userServings < (double)MainForm.ManualCaloriesMinimumValue)
			{
				
				return 0;
				
			}
			
			double tempDoublePenalty = GetSnackingPenalty(userServings, add);
			
			return tempDoublePenalty > -1 ? userServings + tempDoublePenalty : 0;
			
		}

		#region Get EBBP (Snacking Penalty) Summary

		/// <summary>
		/// The operation for obtaining the Eating Before Bed (Snacking) Penalty.
		/// </summary>
		/// <param name="tempDouble">
		/// The double used for the calorie value before attempting to parse a penalty value from it.
		/// </param>
		/// <param name="add">
		/// Are you adding calories?
		/// </param>
		/// <returns>
		/// Returns the Eating Before Bed (Snacking) Penalty as a double.
		/// </returns>
		#endregion
		private double GetSnackingPenalty (double tempDouble, bool add)
		{
			
			var registryTuple = Tuple.Create(DateTime.Parse(this.Retrieval.GetRegistryValue("reset date"), CultureInfo.InvariantCulture), double.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture));

			int hour = DateTime.Now.Hour;

			int unsafeHourThreshold = registryTuple.Item1.Hour - 8;

			string amPMDefiner = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);

			double midSnackPenalty = 0f;

			if (hour >= unsafeHourThreshold && registryTuple.Item1.Day == DateTime.Now.Day && registryTuple.Item1.ToString("tt", CultureInfo.InvariantCulture).Equals(amPMDefiner, StringComparison.OrdinalIgnoreCase))
			{

				midSnackPenalty = tempDouble / 10;

				if (midSnackPenalty < 10)
				{

					midSnackPenalty = 10;

				}
				else if (midSnackPenalty > registryTuple.Item2 / 2)
				{

					midSnackPenalty = registryTuple.Item2 / 2;

				}
				
				if (this.PopupHandler.CreatePopup(string.Format(CultureInfo.InvariantCulture, "An eating before bed {2} of {0} calories will be {1} your daily calorie count if you continue.", midSnackPenalty, !add ? "subtracted from" : "added to", !add ? "penalty" : "benefit"), 3) != DialogResult.OK)
				{

					return -1;

				}

			}

			return midSnackPenalty;
		}
	}
}