using System;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{

	/// <summary>
	/// Interface for the MainForm class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IMainForm
	{
		
		string CustomUser { get; set; }

		/// <summary>
		/// Enables or disables the mainform.
		/// </summary>
		/// <param name="paused">
		/// If true, disables the mainform; else it enables it.
		/// </param>
		void MainFormState (bool paused);

		/// <summary>
		/// Sets the text of the "version" information bar located at the bottom of the program.
		/// </summary>
		/// <param name="value">
		/// The text to set.
		/// </param>
		void MainFormVersionInfoText (string value);

		/// <summary>
		/// Sets the text of the "build" information bar located at the bottom of the program.
		/// </summary>
		/// <example>
		/// Release/Development "Build"
		/// </example>
		/// <param name="value">
		/// The text to set.
		/// </param>
		void MainFormBuildInfoText (string value);

		/// <summary>
		/// Gets or Sets the title of the application after launch.
		/// </summary>
		string MainFormTitle { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="WeightWatchingProgramPlus.IMainForm"/> sync enabled.
		/// </summary>
		/// <value><c>true</c> if sync is enabled; otherwise, <c>false</c>.</value>
		bool SyncEnabled { get; set; }

		string SyncIPAddress { get; set; }

		string SyncSendPort { get; set; }

		string SyncListenPort { get; set; }

		void SetSyncConnectionItems ();

		/// <summary>
		/// Gets or Sets the value in the #Servings NumericUpDown.
		/// </summary>
		decimal UserProvidedServings { get; set; }

		/// <summary>
		/// Gets or Sets whether the manual time is being used or not.
		/// </summary>
		bool ManualTimeIsInitiated { get; set; }

		/// <summary>
		/// Gets or Sets the Manual Reset Time.
		/// </summary>
		DateTime ManualDateTime { get; set; }

		/// <summary>
		/// Gets or Sets the main Food List ListBox's DataSource.
		/// </summary>
		void MainFoodListDataSource (object value);

		/// <summary>
		/// Gets the main Food List ListBox's items.
		/// </summary>
		ListBox.ObjectCollection MainFoodListItems { get; }

		/// <summary>
		/// Gets or Sets the selection for the specified item in the main Food List ListBox.
		/// </summary>
		/// <param name="setIndex">
		/// Is this operation setting the selected value for the main Food List ListBox as well?
		/// </param>
		/// <param name="index">
		/// The zero-based index of the item that determines whether it is selected.
		/// </param>
		/// <param name="value">
		/// True to select the specified item; otherwise false.
		/// </param>
		/// <returns>
		/// Returns the currently selected item.
		/// </returns>
		int FoodListSelectedIndex (bool setIndex, int index, bool value);

		/// <summary>
		/// Gets or Sets the uppermost item in the main Food List ListBox.
		/// </summary>
		int FoodListTopItem { get; }

		/// <summary>
		/// Gets or Sets the label which the user sees asking how many servings they are eating.
		/// </summary>
		void NumberOfServingsLabel (string value);

		/// <summary>
		/// Gets or Sets the "Name" property box which shows the name of the currently selected food item.
		/// </summary>
		string FoodNameProperty { get; set; }

		/// <summary>
		/// Gets or Sets the "Definer" property box which shows the definer of the currently selected food item.
		/// </summary>
		string DefinerProperty { get; set; }

		/// <summary>
		/// Gets or Sets the "Serving Size" property box which shows the serving size of the currently selected food item.
		/// </summary>
		decimal ServingSizeProperty { get; set; }

		/// <summary>
		/// Gets or Sets the "Calories Per Serving" property box which shows how many calories are in each serving of the currently selected food item.
		/// </summary>
		decimal CaloriesPerServingProperty { get; set; }

		/// <summary>
		/// Gets or Sets the "Is Drink" checkbox button which indicates what food items are drinks and which are food.
		/// </summary>
		bool IsDrinkProperty { get; set; }

		/// <summary>
		/// Gets or Sets if the user is checking the time.
		/// </summary>
		bool UserCheckingTime { get; }

		/// <summary>
		/// Gets or Sets if the user is checking their current calorie balance.
		/// </summary>
		bool UserCheckingCalories { get; }

		/// <summary>
		/// Gets or Sets the amount of calories in the NumericUpDown in the "manual" tab of the "main" tab.
		/// </summary>
		void UserSetCalories (decimal value);

		/// <summary>
		/// Gets or Sets the amount of default calories to be applied wherever applicable.
		/// </summary>
		decimal DefaultCalories { get; set; }

		/// <summary>
		/// The NumericUpDown which shows, and sets, the decimal places to be used globally.
		/// </summary>
		decimal DecimalPlaces { get; set; }

		/// <summary>
		/// Sets the decimal example text box.
		/// </summary>
		void DecimalExample (string value);

		/// <summary>
		/// Gets or Sets the value for the "new item" checkbox.
		/// </summary>
		bool IsCreatingNewFoodItem { get; set; }

		/// <summary>
		/// Gets the property control box of your choice based on the contolID provided. This is to be used as a LAST RESTORT when ALL ELSE FAILS!
		/// </summary>
		/// <param name="controlId">
		/// The ID of the control item you wish to use.
		/// </param>
		/// <returns>
		/// Returns a property control value based on the following IDs: (ID 0): Food Name, (ID 1): Serving Size, (ID 2): Calories Per Serving, (ID 3): Definer. (ID 4): Calories Label.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Thrown if the ID provided does not correlate (is less or more) to any of the controls that have been explicitly exposed.
		/// </exception>
		Control ReturnPropertyControl (int controlId);

		/// <summary>
		/// Gets or Sets whether the user has chosen to write the diary portion of Food Tracking to a file.
		/// </summary>
		bool UserIsWritingDiaryToFile { get; }

		/// <summary>
		/// Gets or Sets whether the user has chosen to use the diary portion of Food Tracking.
		/// </summary>
		bool DiaryIsBeingUsed { get; }

		/// <summary>
		/// Gets the "sign" for the arithmetic operation in the "arithmetic" sub-tab.
		/// </summary>
		string GetArithmeticSign { get; }

		/// <summary>
		/// Sets the "sign" for the arithmetic operation in the "arithmetic" sub-tab.
		/// </summary>
		void SetArithmeticSign (int value);

		TabPage AddSubSelectedSubTab { get; set; }

		/// <summary>
		/// Gets the value from one of the NumericUpDowns in the "arithmetic" subtab.
		/// </summary>
		/// <param name="left">
		/// Are you getting the value of the left NumericUpDown?
		/// </param>
		/// <returns>
		/// The decimal value of the NumericUpDown as determined by <paramref name="left"></paramref>.
		/// </returns>
		decimal ArithmeticValue (bool left);

		void SetAllDecimalPointValues (int value);

		decimal ServingSizeMinimumValue { get; }

		decimal ManualCaloriesMinimumValue { get; }

		double GlobalMinimumValue { get; set; }
		
	}

	/// <summary>
	/// Interface for the Popup Handler sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IPopup
	{
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
		DialogResult CreatePopup (string message, int popupCode);

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
		/// <param name="modification">
		/// A preinitialized instance of IModification. It is not recommended to pass a new initialization of IModification.
		/// </param>
		/// <param name="store">
		/// A preinitialized instance of IStorage. It is not recommended to pass a new initialization of IStorage.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		/// <param name="netOps">
		/// A preinitialized instance of INetOps. It is not recommended to pass a new initialization of INetOps.
		/// </param>
		/// <param name="mainForm">
		/// A preinitialized instance of IMainForm. It is not recommended to pass a new initialization of IMainForm.
		/// </param>
		void InitializeForms (IModification modification, IStorage store, IValidation valid, IRetrieval retrieve, INetOps netOps, IMainForm mainForm);

		/// <summary>
		/// Clears and reloads the food table into the food listbox
		/// </summary>
		/// <param name="store">
		/// A preinitialized instance of IStorage. It is not recommended to pass a new initialization of IStorage.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void RefreshFoodList (IStorage store, IRetrieval retrieve, IMainForm mainForm);

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
		/// <param name="popup">
		/// A preinitialized instance of IPopup. It is not recommended to pass a new initialization of IPopup.
		/// </param>
		void Find (int offset, string valueToFind, string valueToAvoid, bool exactSearch, bool searchForNextValue, IPopup popup, IMainForm mainForm);
		
	}

	/// <summary>
	/// Interface for the Mathematics sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IMathematics
	{
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
		double PerformArithmeticOperation (string equation);

		/// <summary>
		/// Performs an arithmetic operation on an equation which has been converted to it's logical string counterpart.
		/// </summary>
		/// <param name="equation">
		/// An equation in string form. (Example: 1*1)
		/// </param>
		/// <param name="allowPopupWindows">
		/// This operation allows the creation of and interaction with popup dialogs.
		/// </param>
		/// <returns>
		/// The result of the arithmetic operation as a string.
		/// </returns>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		double PerformArithmeticOperation (string equation, bool allowPopupWindows);

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
		double GetFinalCalories (bool add, IValidation valid);
		
	}

	/// <summary>
	/// Interface for the Retrieval sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IRetrieval
	{

		/// <summary>
		/// This function reads the food table file and parses the results into their logical values. This default override reads from the GlobalVariable values.
		/// </summary>
		void ReadFoodTable ();

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
		void ReadFoodTable (string directory, string file);

		/// <summary>
		/// Reads the registry and parses all values into their logical counterparts. Also checks to make sure the reset date is later than the current date. This override reads from the registry as dictated by the GlobalVariable values.
		/// </summary>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		void ReadRegistry ();

		/// <summary>
		/// Reads the registry and parses all values into their logical counterparts. Also checks to make sure the reset date is later than the current date.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		void ReadRegistry (string appendedRegistryValue, string registryValue);

		/// <summary>
		/// Gets the registry value requested through the keyword provided.
		/// </summary>
		/// <param name="registryIdKeyword">
		/// The keyword you wish to use to choose the registry item you wish to pull.
		/// </param>
		/// <returns>
		/// Returns the most closely related registry value to the keyword provided.
		/// </returns>
		/// <exception cref="T:System.Exception">
		/// Thrown if a local value that requires a parsable registry value cannot parse it.
		/// </exception>
		/// <exception cref="T:System.ArgumentException">
		/// Thrown if the registry keyword fails to produce results.
		/// </exception>
		string GetRegistryValue (string registryIdKeyword);

		string ParseRegistryKeyById (string registryIdKeyword);

		string GetRegistryValueFromRegistry (string appendedRegistryValue, string registryValue, string registryIdKeyword);

		/// <summary>
		/// Initiates the backup of all files without any "passover keywords".
		/// </summary>
		/// <param name="backupDirectory">
		/// The directory where the backup will reside.
		/// </param>
		string BackupVersionFileInfo (string backupDirectory);
		
	}

	/// <summary>
	/// Interface for the Storage sub-class so it doesn't need to be accessed directly.
	/// </summary>
	public interface IStorage
	{
		/// <summary>
		/// Parses the logical values back into a readable format and writes them to the food table. This default override writes to the GlobalVariable values and does not add any values.
		/// </summary>
		void WriteFoodTable ();

		/// <summary>
		/// Parses the logical values back into a readable format and writes them to the food table. This default override writes to the GlobalVariable values.
		/// </summary>
		/// <param name="additionToFoodTable">
		/// A Tuple containing ONE additional food item which will be added to the food list.
		/// </param>
		void WriteFoodTable (Tuple<string, double, double, string, bool> additionToFoodTable);

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
		void WriteFoodTable (string directory, string file, Tuple<string, double, double, string, bool> additionToFoodTable);

		/// <summary>
		/// Writes the currently supplied object to the registry as a string.
		/// </summary>
		/// <param name="objectToSave">
		/// The value provided, in object form.
		/// </param>
		/// <param name="registryIdKeyword">
		/// The keyword you wish to use to choose the registry title you wish to write to.
		/// </param>
		/// <param name="reset">
		/// Allows an action to be performed, where applicable, that "resets" or modifies the value before it is written to the registry.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void WriteRegistry (string objectToSave, string registryIdKeyword, bool reset, IValidation valid, IRetrieval retrieve);

		/// <summary>
		/// Writes the currently supplied object to the registry as a string.
		/// </summary>
		/// <param name="registryIdKeyword">
		/// The keyword you wish to use to choose the registry title you wish to write to.
		/// </param>
		/// <param name="reset">
		/// Allows an action to be performed, where applicable, that "resets" or modifies the value before it is written to the registry.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void WriteRegistry (string registryIdKeyword, bool reset, IValidation valid, IRetrieval retrieve);

		/// <summary>
		/// Writes the currently supplied values to the registry.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="objectToSave">
		/// The value provided, in object form.
		/// </param>
		/// <param name="registryIdKeyword">
		/// The keyword you wish to use to choose the registry title you wish to write to.
		/// </param>
		/// <param name="reset">
		/// Reset the "remaining" calories value.
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void WriteRegistry (string appendedRegistryValue, string registryValue, string objectToSave, string registryIdKeyword, bool reset, IValidation valid, IRetrieval retrieve);

		/// <summary>
		/// Food Tracking Function 1: Diary: Writes to file what food you've eaten and when. This override writes to the default directory and file.
		/// </summary>
		/// <param name="add">
		/// Is this operation the result of an addition operation?
		/// </param>
		/// <param name="valid">
		/// A preinitialized instance of IValidation. It is not recommended to pass a new initialization of IValidation.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void WriteFoodEaten (bool add, IValidation valid, IRetrieval retrieve);

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
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void WriteFoodEaten (string directory, string file, bool add, IValidation valid, IRetrieval retrieve);

		/// <summary>
		/// Initiates the backup of all files without any "passover keywords".
		/// </summary>
		/// <param name="originalDirectory">
		/// The original directory where files will be copied from.
		/// </param>
		/// <param name="backupDirectory">
		/// The directory where the backup will reside.
		/// </param>
		/// <param name="oldVersion">
		/// The old version of the program.
		/// </param>
		/// <param name="newVersion">
		/// The current version of the program.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void Backup (string originalDirectory, string backupDirectory, string oldVersion, string newVersion, IRetrieval retrieve);
		
	}

	/// <summary>
	/// Interface for the Validation sub-class so it does not need to be directly accessed.
	/// </summary>
	public interface IValidation
	{
		/// <summary>
		/// Checks to see if the reset date is earlier or later than the checked date.
		/// </summary>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		void CheckDateValidity (IRetrieval retrieve);

		/// <summary>
		/// Checks to see which radio button is currently active.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// Thrown if, for some reason, the user is not checking any value, as explicitly state in this function. in the main title textbox of the program.
		/// </exception>
		void CheckCurrentRadioButton (IModification modification);

		/// <summary>
		/// Checks to see if the food item property setting boxes have values that are within acceptable parameters.
		/// </summary>
		/// <returns>
		/// True if the name box is not an exact duplicate of another food name, all TextBoxes are not void or white space and contain valid characters, and all NumericUpDowns have values >= 0; else returns false.
		/// </returns>
		bool EditBoxesHaveValidEntries ();

		/// <summary>
		/// Checks whether a backup needs to be made.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="backupDirectory">
		/// The directory where the backup will reside.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		/// <returns>
		/// True if current version number is greater than previous, no registry value exists or has a value, or the backed up version file contains a different version number than the program; else false.
		/// </returns>
		bool ValidateBackup (string appendedRegistryValue, string registryValue, string backupDirectory, IRetrieval retrieve);

		/// <summary>
		/// Checks the existence of a registry value, under the assumption the value will NOT exist.
		/// </summary>
		/// <returns><c>true</c>, if the registry value being searched for does not exist, <c>false</c> otherwise.</returns>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="registryIdKeyword">
		/// The keyword that most closely relates to the registry item you're seeking.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of <c>IRetrieval</c>. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		bool RegistryValueDoesNotExist (string appendedRegistryValue, string registryValue, string registryIdKeyword, IRetrieval retrieve);

		/// <summary>
		/// Validates registry items before passing one on if needed. The one to be passed on would be the one which contains the registry keyword provided.
		/// </summary>
		/// <param name="appendedRegistryValue">
		/// Registry value that comes first after LOCAL_MACHINE
		/// </param>
		/// <param name="registryValue">
		/// Registry value that is added after the appended value.
		/// </param>
		/// <param name="registryIdKeyword">
		/// The keyword that most closely relates to the registry item you're seeking. If none, enter "null" as the parameter and null will be returned.
		/// </param>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		/// <returns>
		/// Returns the requested registry item, if applicable.
		/// </returns>
		/// <exception cref="T:System.Exception">
		/// Thrown if a registry value cannot be parsed.
		/// </exception>
		string ValidateRegistryValues (string appendedRegistryValue, string registryValue, string registryIdKeyword, IRetrieval retrieve);

		/// <summary>
		/// Checks if the sync port is valid.
		/// </summary>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		/// <param name="listenPort">
		/// Are you checking the listening port?
		/// </param>
		/// <returns>
		/// True if sync port is valid; else, false.
		///</returns>
		bool PortIsValid (IRetrieval retrieve, bool listenPort);

		/// <summary>
		/// Checks if the sync IP address is valid.
		/// </summary>
		/// <param name="retrieve">
		/// A preinitialized instance of IRetrieval. It is not recommended to pass a new initialization of IRetrieval.
		/// </param>
		/// <returns>
		/// True if IP Address is valid; else, false. 
		/// </returns>
		bool IPAddressIsValid (IRetrieval retrieve);
		
	}

	/// <summary>
	/// Interface for the Modifcation sub-class so it does not need to be directly accessed.
	/// </summary>
	public interface IModification
	{
		/// <summary>
		/// Performs the main operation when modifying the user's calorie balance.
		/// </summary>
		/// <param name="sender">
		/// Who or what triggered this function.
		/// </param>
		void ModifyCalories (object sender);

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
		void WriteToObject (Label labelToChange, int objectNumber);

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
		void WriteToObject (TextBox textBoxToChange, int objectNumber);

		/// <summary>
		/// Changes the property of an existing food item or creates a new food item.
		/// </summary>
		/// <param name="allValidEntries">
		/// All property entries are valid.
		/// </param>
		void ModifyFoodItemProperty (bool allValidEntries);

		/// <summary>
		/// Clears the property settings boxes.
		/// </summary>
		void ModifyFoodPropertiesList ();

		/// <summary>
		/// Modifies the property settings boxes.
		/// </summary>
		/// <param name="wordProperties">
		/// Contains the strings for all properties that require them. So far, in order, these properties are counted: FoodName, Definer
		/// </param>
		/// <param name="numberProperties">
		/// Contains the decimal values for all properties that require them. So far, in order, these properties are counted: ServingSize, CaloriesPerServing.
		/// </param>
		/// <param name="conditionProperties">
		/// Contains the bool values for all properties that require them. So far, in order, these properties are counted: IsDrinkCheckBox.
		/// </param>
		void ModifyFoodPropertiesList (string[] wordProperties, decimal[] numberProperties, bool[] conditionProperties);
		
	}

	/// <summary>
	/// Interface for the NetworkOps sub-class so it does not need to be directly accessed.
	/// </summary>
	public interface INetOps
	{
		/// <summary>
		/// Gets or sets the server connection status.
		/// </summary>
		/// <value>
		/// The server connection status.
		/// </value>
		int ServerConnectionStatus { get; set; }

		/// <summary>
		/// Gets or sets the client connection status.
		/// </summary>
		/// <value>
		/// The client connection status.
		/// </value>
		int ClientConnectionStatus { get; set; }

		/// <summary>
		/// Initializes server.
		/// </summary>
		/// <param name="port">
		/// The port chosen that the server will bind to when listening for connections.
		/// </param>
		void StartListen (int port);

		/// <summary>
		/// Starts the process of sending a file or other information.
		/// </summary>
		/// <param name="ipAddress">
		/// The IP address of the host server.
		/// </param>
		/// <param name="port">
		/// The port of the host server.
		/// </param>
		/// <param name="message">
		/// The "message" that indicates the nature of this transaction.
		/// </param>
		/// <example>
		/// "send" would indicate a file is being SENT. "uptime" would indicate the server (you) is being requested for the uptime of the program. And so on. 
		/// Mostly, you'll only need to worry about using simple phrases or words, and any of the more complicated or convoluted commands are handled code-side.
		/// </example>
		/// <param name="additionalInfo">
		/// Additional information generally refering to the directory the client wishes the server to save the file under. 
		/// However, this can also refer to any number of variables currently supported for sending operations. 
		/// View the source code for more information.
		/// </param>
		/// <param name="fileName">
		/// If this is currently a file I/O operation, this field refers to the name of the file being sent/requested. Otherwise the field will be ignored by the server.
		/// </param>
		/// <param name="pathToFile">
		/// If this is currently a file I/O operation, this field refers to the actual file's path. 
		/// This reference will be used to grab and stream the desired file information to the server.
		/// </param>
		void StartSend (string ipAddress, int port, string message, string additionalInfo, string fileName, string pathToFile);
		
	}
	
}
