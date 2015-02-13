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
	class Mathematics
	{
		private PopupHandler PopupHandler = new PopupHandler ();

		#region Perform Arithmetic Operation Summary

		/// <summary>
		/// Performs an arithmetic operation on an equation which has been converted to it's logical string counterpart.
		/// </summary>
		/// <param name="equation">
		/// An equation in string form. (Example: 1*1)
		/// </param>
		/// <returns>
		/// The result of the arithmetic operation as a string.
		/// </returns>
		#endregion
		internal float PerformArithmeticOperation (string equation)
		{
			float returnFloat = 0f;

			using (DataTable dt = new DataTable ())
			{

				dt.Locale = CultureInfo.InvariantCulture;

				object computation = dt.Compute(equation, null);

				if (!float.TryParse(computation.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out returnFloat))
				{

					Errors.Handler(Errors.PremadeExceptions("PerformArithmeticOperation", "computation", 0), true, true, 524288);

				}

				if (returnFloat <= 0f)
				{

					PopupHandler.CreatePopup("You cannot use a value of zero or less!", new Control (), 0, false);
					
					return 0;

				}

			}

			return returnFloat;
		}

		#region Get Final Calories Summary

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
		internal float GetFinalCalories (bool add)
		{
			
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);

			float tempFloat = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item3;

			float tempArithmetic = MainForm.AddSub_SelectedSubTab.Text.Contains("explicit", StringComparison.OrdinalIgnoreCase) ? (float)MainForm.UserProvidedServings : PerformArithmeticOperation(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", MainForm.GetArithmaticValue(true), MainForm.GetArithmeticSign, MainForm.GetArithmaticValue(false)));

			if (tempArithmetic > 0)
			{

				tempArithmetic /= FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;

			}
			else
			{

				return 0;

			}
			
			tempFloat *= tempArithmetic;
			
			float tempFloatPenalty = GetSnackingPenalty(tempFloat, add);
			
			return tempFloatPenalty > -1 ? tempFloat + tempFloatPenalty : 0;
			
		}

		#region Get EBBP (Snacking Penalty) Summary

		/// <summary>
		/// The operation for obtaining the Eating Before Bed (Snacking) Penalty.
		/// </summary>
		/// <param name="tempFloat">
		/// The float used for the calorie value before attempting to parse a penalty value from it.
		/// </param>
		/// <param name="add">
		/// Are you adding calories?
		/// </param>
		/// <returns>
		/// Returns the Eating Before Bed (Snacking) Penalty as a float.
		/// </returns>
		#endregion
		private float GetSnackingPenalty (float tempFloat, bool add)
		{
			
			var registryTuple = Storage.GetRetrievableRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);

			int hour = DateTime.Now.Hour;

			int unsafeHourThreshold = registryTuple.Item1.Hour - 8;

			string amPMDefiner = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);

			float midSnackPenalty = 0f;

			if (hour >= unsafeHourThreshold && registryTuple.Item1.Day == DateTime.Now.Day && registryTuple.Item1.ToString("tt", CultureInfo.InvariantCulture).Equals(amPMDefiner, StringComparison.OrdinalIgnoreCase))
			{

				midSnackPenalty = tempFloat / 10;

				if (midSnackPenalty < 10)
				{

					midSnackPenalty = 10;

				}
				else if (midSnackPenalty > registryTuple.Item3 / 2)
				{

					midSnackPenalty = registryTuple.Item3 / 2;

				}
				
				if (this.PopupHandler.CreatePopup(string.Format(CultureInfo.InvariantCulture, "An eating before bed {2} of {0} calories will be {1} your daily calorie count if you continue.", midSnackPenalty, !add ? "subtracted from" : "added to", !add ? "penalty" : "benefit"), null, 3, false) != DialogResult.OK)
				{

					return -1;

				}

			}

			return midSnackPenalty;
		}
	}
}