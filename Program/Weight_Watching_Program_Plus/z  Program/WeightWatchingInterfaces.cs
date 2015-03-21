/*
 * Created by SharpDevelop.
 * User: ${someguythere}
 * Date: (c)3/19/2015
 * Time: 5:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
	
	/// <summary>
	/// Interface for the Popup Handler sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IPopup
	{
		
		#region Create Popup Only Override Summary

		/// <summary>
		/// Handles the creation of popup messageboxes.
		/// </summary>
		/// <param name="message">
		/// The text that goes in the body of the messagebox.
		/// </param>
		/// <param name="popupCode">
		/// The code that defines how the popup will set itself up.
		/// </param>
		/// <returns>
		/// Returns a DialogResult variable based on the actions taken by the user with regards to the popup created herein.
		/// </returns>
		#endregion
		DialogResult CreatePopup (string message, int popupCode);

		#region Create Popup Additional Actions Override Summary

		/// <summary>
		/// Handles the creation of popup messageboxes.
		/// </summary>
		/// <param name="message">
		/// The text that goes in the body of the messagebox.
		/// </param>
		/// <param name="popupCode">
		/// The code that defines how the popup will set itself up.
		/// </param>
		/// <param name="controlItem">
		/// The items to be selected if additional actions are required.
		/// </param>
		/// <returns>
		/// Returns a DialogResult variable based on the actions taken by the user with regards to the popup created herein.
		/// </returns>
		#endregion
		DialogResult CreatePopup (string message, int popupCode, Control controlItem);
			
	}
	
	/// <summary>
	/// Interface for the General Functions sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IGeneralFunctions
	{
		
		/// <summary>
		/// Main initialization for the program as handled by the developer (and not, say, through windows forms or what have you).
		/// </summary>
		void InitializeForms (IModification modification, IStorage store, IValidation valid);
		
		/// <summary>
		/// Clears and reloads the food table into the food listbox
		/// </summary>
		void RefreshFoodList (IStorage store);
		
		#region Find Item Summary

		/// <summary>
		/// Finds an item within the food list and selects it.
		/// </summary>
		/// <param name="offset">
		/// The previous item number.
		/// </param>
		/// <param name="valueToFind">
		/// The string you'd like to find within the food list.
		/// </param>
		/// <param name="valueToAvoid">
		/// The previously found name from the food list.
		/// </param>
		/// <param name="exactSearch">
		/// Is this a search where only an exact match is allowed?
		/// </param>
		/// <param name="searchForNextValue">
		/// Was the "next" button pressed?
		/// </param>
		#endregion
		void Find (int offset, string valueToFind, string valueToAvoid, bool exactSearch, bool searchForNextValue);
		
	}
	
	/// <summary>
	/// Interface for the Mathematics sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IMathematics
	{
		
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
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		double PerformArithmeticOperation (string equation);
		
		#region Get Final Calories Summary

		/// <summary>
		/// Sub-logic handler for modification of calories. Simply to reduce code bloat and make understanding the final processes easier.
		/// </summary>
		/// <param name="add">
		/// Are you adding calories?
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <returns>
		/// Returns the calorie value after the logical operations have been finished.
		/// </returns>
		#endregion
		double GetFinalCalories (bool add, IValidation valid);
		
	}
	
	/// <summary>
	/// Interface for the Storage sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IStorage
	{
		
		#region Read Food Table Default Override Summary
		/// <summary>
		/// This function reads the food table file and parses the results into their logical values. This default override reads from the GlobalVariable values.
		/// </summary>
		#endregion
		void ReadFoodTable();
		
		#region Read Food Table Summary

		/// <summary>
		/// This function reads the food table file and parses the results into their logical values.
		/// </summary>
		/// <param name="directory">
		/// The directory where the food table is/will be created.
		/// </param>
		/// <param name="file">
		/// The name of the food table.
		/// </param>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		/// <exception cref="T:System.IO.IOException">
		/// Thrown if the provided file, in the provided directory, does not exist.
		/// </exception>
		#endregion
		void ReadFoodTable (string directory, string file);
		
		#region Write Food Table Default Override Summary
		/// <summary>
		/// Parses the logical values back into a readable format and writes them to the food table. This default override writes to the GlobalVariable values and does not add any values.
		/// </summary>
		#endregion
		void WriteFoodTable();
		
		#region Write Food Table Default Add Summary
		/// <summary>
		/// Parses the logical values back into a readable format and writes them to the food table. This default override writes to the GlobalVariable values.
		/// </summary>
		/// <param name="additionToFoodTable">
		/// A Tuple containing ONE additional food item which will be added to the food list.
		/// </param>
		#endregion
		void WriteFoodTable(Tuple<string, double, double, string, bool> additionToFoodTable);
		
		#region Write To Food Table Summary

		/// <summary>
		/// Parses the logical values back into a readable format and writes them to the food table.
		/// </summary>
		/// <param name="directory">
		/// The directory where the food table is/will be created.
		/// </param>
		/// <param name="file">
		/// The name of the food table.
		/// </param>
		/// <param name="additionToFoodTable">
		/// A Tuple containing ONE additional food item which will be added to the food list.
		/// </param>
		/// <exception cref="T:System.IO.IOException">
		/// Thrown if the provided file, in the provided directory, does not exist.
		/// </exception>
		#endregion
		void WriteFoodTable (string directory, string file, Tuple<string, double, double, string, bool> additionToFoodTable);
		
		#region Read Registry Default Override Summary
		/// <summary>
		/// Reads the registry and parses all values into their logical counterparts. Also checks to make sure the reset date is later than the current date. This override reads from the registry as dictated by the GlobalVariable values.
		/// </summary>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		void ReadRegistry(IValidation valid);
		
		#region Read Registy Summary

		/// <summary>
		/// Reads the registry and parses all values into their logical counterparts. Also checks to make sure the reset date is later than the current date.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		void ReadRegistry (string appendedRegistryValue, string registryValue, IValidation valid);
		
		#region Write Registry Default Override Summary
		/// <summary>
		/// Writes all logical values to the registry in a format it can understand. This override writes to the registry as dictated by the GlobalVariable values and does not reset any of the values.
		/// </summary>
		/// <param name="calories">
		/// Current calories left.
		/// </param>
		/// <param name="defaultCalories">
		/// Default calorie count.
		/// </param>
		/// <param name="decimalPlaces">
		/// The amount of decimal places to be used globally.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		#endregion
		void WriteRegistry(double calories, double defaultCalories, int decimalPlaces, IValidation valid);
		
		#region Write To Registry Summary

		/// <summary>
		/// Writes all logical values to the registry in a format it can understand.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="dateToApply">
		/// If resetting, this is the new reset date.
		/// </param>
		/// <param name="calories">
		/// Current calories left.
		/// </param>
		/// <param name="defaultCalories">
		/// Default calorie count.
		/// </param>
		/// <param name="decimalPlaces">
		/// The amount of decimal places to be used globally.
		/// </param>
		/// <param name="reset">
		/// reset[0] = calorie count, reset[1] = reset date.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		#endregion
		void WriteRegistry (string appendedRegistryValue, string registryValue, DateTime dateToApply, double calories, double defaultCalories, int decimalPlaces, System.Collections.Generic.IList<bool> reset, IValidation valid);
		
		#region Get Registry Values Default Override Summary
		/// <summary>
		/// Gets all registry values that can be modified and used by the program (usually all of them). This override reads from the registry as dictated by the GlobalVariable values.
		/// </summary>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <returns>
		/// Returns a Tuple(DateTime CaloriesResetTime, double Calories, double DefaultCalories, bool ManualTimeInitiated, string ProgramNumber).
		/// </returns>
		/// <example>
		/// DateTime: the time at which the calories reset.
		/// </example>
		/// <example>
		/// Double (1): the amount of calories left for the day.
		/// </example>
		/// <example>
		/// Double (2): the amount of calories that is set by default.
		/// </example>
		/// <example>
		/// Bool: if the user is using a manual time instead of an automatic one.
		/// </example>
		/// <example>
		/// String: The current program version as stored in the registry.
		/// </example>
		/// <example>
		/// Int: The currently set amount of decimal places to use globally for standardization purposes.
		/// </example>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		Tuple<DateTime, double, double, bool, string, int> GetRetrievableRegistryValues(IValidation valid);
		
		#region Get Registry Values Summary

		/// <summary>
		/// Gets all registry values that can be modified and used by the program (usually all of them).
		/// </summary>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="roundCaloriesLeftForDay">
		/// This operation is rounding the calories left for the day to the global decimal point value.
		/// </param>
		/// <returns>
		/// Returns a Tuple(DateTime CaloriesResetTime, double Calories, double DefaultCalories, bool ManualTimeInitiated, string ProgramNumber).
		/// </returns>
		/// <example>
		/// DateTime: the time at which the calories reset.
		/// </example>
		/// <example>
		/// Double (1): the amount of calories left for the day.
		/// </example>
		/// <example>
		/// Double (2): the amount of calories that is set by default.
		/// </example>
		/// <example>
		/// Bool: if the user is using a manual time instead of an automatic one.
		/// </example>
		/// <example>
		/// String: The current program version as stored in the registry.
		/// </example>
		/// <example>
		/// Int: The currently set amount of decimal places to use globally for standardization purposes.
		/// </example>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		Tuple<DateTime, double, double, bool, string, int> GetRetrievableRegistryValues(IValidation valid, bool roundCaloriesLeftForDay);
		
		#region Get Registry Values Summary

		/// <summary>
		/// Gets all registry values that can be modified and used by the program (usually all of them).
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="roundCaloriesLeftForDay">
		/// This operation is rounding the calories left for the day to the global decimal point value.
		/// </param>
		/// <returns>
		/// Returns a Tuple(DateTime CaloriesResetTime, double Calories, double DefaultCalories, bool ManualTimeInitiated, string ProgramNumber).
		/// </returns>
		/// <example>
		/// DateTime: the time at which the calories reset.
		/// </example>
		/// <example>
		/// Double (1): the amount of calories left for the day.
		/// </example>
		/// <example>
		/// Double (2): the amount of calories that is set by default.
		/// </example>
		/// <example>
		/// Bool: if the user is using a manual time instead of an automatic one.
		/// </example>
		/// <example>
		/// String: The current program version as stored in the registry.
		/// </example>
		/// <example>
		/// Int: The currently set amount of decimal places to use globally for standardization purposes.
		/// </example>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		#endregion
		Tuple<DateTime, double, double, bool, string, int> GetRetrievableRegistryValues (string appendedRegistryValue, string registryValue, IValidation valid, bool roundCaloriesLeftForDay);
		
		#region Food Tracking Diary Default Override Summary
		/// <summary>
		/// Food Tracking Function 1: Diary: Writes to file what food you've eaten and when. This override writes to the default directory and file.
		/// </summary>
		/// <param name="add">
		/// Is this operation the result of an addition operation?
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		#endregion
		void WriteFoodEaten(bool add, IValidation valid);
		
		#region Food Tracking Diary Summary

		/// <summary>
		/// Food Tracking Function 1: Diary: Writes to file what food you've eaten and when.
		/// </summary>
		/// <param name="directory">
		/// The directory where the diary file is/will be created.
		/// </param>
		/// <param name="file">
		/// The name of the diary file.
		/// </param>
		/// <param name="add">
		/// Is this operation the result of an addition operation?
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		#endregion
		void WriteFoodEaten (string directory, string file, bool add, IValidation valid);
		
	}
	
	/// <summary>
	/// Interface for the Validation sub-class so it does not need to be directly accessed.
	/// </summary>
	public interface IValidation
	{
		
		#region Check Date Summary

		/// <summary>
		/// Checks to see if the reset date is earlier or later than the checked date.
		/// </summary>
		#endregion
		void CheckDateValidity ();

		#region Check Radio Button Summary

		/// <summary>
		/// Checks to see which radio button is currently active.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// Thrown if, for some reason, the user is not checking any value, as explicitly state in this function. in the main title textbox of the program.
		/// </exception>
		#endregion
		void CheckCurrentRadioButton (IModification modification);

		#region Edit Box Validity Summary

		/// <summary>
		/// Checks to see if the food item property setting boxes have values that are within acceptable parameters.
		/// </summary>
		/// <returns>
		/// True if the name box is not an exact duplicate of another food name, all TextBoxes are not void or white space and contain valid characters, and all NumericUpDowns have values >= 0; else returns false.
		/// </returns>
		#endregion
		bool EditBoxesHaveValidEntries ();

		#region Validate Backup Summary

		/// <summary>
		/// Checks whether a backup needs to be made.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <returns>
		/// True if current version number is greater than previous or no registry value exists; else false.
		/// </returns>
		#endregion
		bool ValidateBackup (string appendedRegistryValue, string registryValue);

		#region Validate Registry Values Summary
		/// <summary>
		/// Validates registry items before passing them on if needed.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="thoroughCheck">
		/// If true, checks each registry value individually. If false, checks all of them all at once.
		/// </param>
		/// <returns>
		/// Returns a list of validated registry values to be used if needed. Some registry values do not need a pre-validation (such as strings) and as such are not included.
		/// </returns>
		/// <exception cref="T:System.Exception">
		/// Thrown if a registry value cannot be parsed.
		/// </exception>
		
		#endregion
		Tuple<DateTime, double, double, bool, int> ValidateRegistryValues (string appendedRegistryValue, string registryValue, bool thoroughCheck);
		
	}
	
	/// <summary>
	/// Interface for the Modifcation sub-class so it does not need to be directly accessed.
	/// </summary>
	public interface IModification
	{
		
		#region Modify Calories Summary

		/// <summary>
		/// Performs the main operation when modifying the user's calorie balance.
		/// </summary>
		/// <param name="sender">
		/// Who or what triggered this function.
		/// </param>
		#endregion
		void ModifyCalories (object sender);

		#region Write To Label Override Summary

		/// <summary>
		/// A lightweight modular text changer for use on a variety of objects.
		/// </summary>
		/// <param name="labelToChange">
		/// The Label whos text value will be changed (if applicible)
		/// </param>
		/// <param name="objectNumber">
		/// The number used to handle the operation.
		/// </param>
		/// <exception cref="T:System.InvalidCastException">
		/// Thrown when attempting to use an object type that is not yet supported.
		/// </exception>
		#endregion
		void WriteToObject (Label labelToChange, int objectNumber);

		#region Write To TextBox Override Summary

		/// <summary>
		/// A lightweight modular text changer for use on a variety of objects.
		/// </summary>
		/// <param name="textBoxToChange">
		/// The TextBox whos text value will be changed.
		/// </param>
		/// <param name="objectNumber">
		/// The number used to handle the operation.
		/// </param>
		/// <exception cref="T:System.InvalidCastException">
		/// Thrown when attempting to use an object type that is not yet supported.
		/// </exception>
		#endregion
		void WriteToObject (TextBox textBoxToChange, int objectNumber);

		#region Food Item Modification Summary

		/// <summary>
		/// Changes the property of an existing food item or creates a new food item.
		/// </summary>
		/// <param name="allValidEntries">
		/// All property entries are valid.
		/// </param>
		#endregion
		void ModifyFoodItemProperty (bool allValidEntries);

		#region Clear Food Property Boxes Override Summary

		/// <summary>
		/// Clears the property settings boxes.
		/// </summary>
		#endregion
		void ModifyFoodPropertiesList ();

		#region Modify Food Properties Boxes By Default Override Summary

		/// <summary>
		/// Modifies the property settings boxes.
		/// </summary>
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
		void ModifyFoodPropertiesList (string[] stringProperties, decimal[] decimalProperties, bool[] boolProperties);
		
	}
	
}
