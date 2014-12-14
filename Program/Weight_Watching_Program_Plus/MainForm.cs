using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Weight_Watching_Program_Plus
{
	/// <summary>
	/// Main Window and all tabs.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent ();
			
			//
			GlobalVariables.mainForm = this;
			Functions.InitializeForms (foodList, calorieRadioButton, timeRadioButton, caloriesLabel, Seperator1, Seperator2, howManyServingsLabel, searchBar, deleteSelectedFoodItemButton,
				clearSearchBarButton, foodNameEditBox, definerEditBox, servingSizeEditBox, caloriesPerServingEditBox, manualCalorieEditBox);
			
			//
		}
		
		//MAIN//
		protected internal virtual void SearchBarTextChanged (object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty (searchBar.Text) && searchBar.Text != " ") {
				Functions.find (0, searchBar.Text, null, GlobalVariables.exactSearch, foodList);
			} else {
				foodList.ClearSelected ();
			}
		}
		
		protected internal void searchBarFocusGranted (object sender, EventArgs e)
		{
			searchBar.Clear ();
			clearSearchBarButton.Enabled = true;
			Font tempfont = new Font ("Times New Roman", 10f);
			searchBar.Font = tempfont;
		}
		
		protected internal void clearSearchBarButtonClicked (object sender, EventArgs e)
		{
			searchBar.Clear ();
			clearSearchBarButton.Enabled = false;
			Font tempfont = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
			searchBar.Font = tempfont;
			searchBar.Text = "Click Here to Search the Food List";
			searchBar.TextAlign = HorizontalAlignment.Center;
		}
		
		protected internal virtual void foodListSelectedIndexChanged (object sender, EventArgs e)
		{
			if (foodList.SelectedIndex > -1) {
				GlobalVariables.selectedListItem = foodList.SelectedIndex;
				howManyServingsLabel.Text = "How many " + GlobalVariables.definersList [foodList.SelectedIndex] + "s do you plan on eating?";
				foodNameEditBox.Text = GlobalVariables.foodNameList [foodList.SelectedIndex];
				foodNameEditBox.TextAlign = HorizontalAlignment.Center;
				servingSizeEditBox.Text = GlobalVariables.servingSizeList [foodList.SelectedIndex].ToString ();
				servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
				definerEditBox.Text = GlobalVariables.definersList [foodList.SelectedIndex];
				definerEditBox.TextAlign = HorizontalAlignment.Center;
				caloriesPerServingEditBox.Text = GlobalVariables.caloriesPerServingList [foodList.SelectedIndex].ToString ();
				caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
			}
		}
		
		protected internal void foodListLeaveFocus (object sender, EventArgs e)
		{
			if (deleteSelectedFoodItemButton.Enabled == true && foodList.Items.Count <= 0) {
				deleteSelectedFoodItemButton.Enabled = false;
			}
			foodList.ClearSelected ();
		}
		
		protected internal void foodListEnterFocus (object sender, EventArgs e)
		{
			GlobalVariables.selectedListItem = foodList.SelectedIndex;
			if (deleteSelectedFoodItemButton.Enabled == false && foodList.Items.Count > 0) {
				deleteSelectedFoodItemButton.Enabled = true;
			}
		}
		
		protected internal void deleteFoodItemFromTable (object sender, EventArgs e)
		{
			Functions.dumpFoodPropertiesList (foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
			GlobalVariables.foodNameList.Remove (GlobalVariables.selectedListItem);
			Storage.writeFoodTable ("Text Files\\", "food.table", new string[] {
				null,
				null,
				null,
				null
			}
			);
			Functions.refresh_foodList (foodList);
		}
		
		protected internal void setFoodPropertiesButtonClicked (object sender, EventArgs e)
		{
			string oldtext = foodNameEditBox.Text;
			Functions.foodPropertiesSwitch (foodList, foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, newItemCheckbox);
			Functions.find (0, oldtext, null, true, foodList);
		}
		
		protected internal void subtractCalories (object sender, EventArgs e)
		{
			float tempcalories = Functions.modifyCalories (userServingInputTextBox);
			bool safetosubtract = false;
			if (GlobalVariables.calories - tempcalories < 0) {
				DialogResult dialogResult = MessageBox.Show ("The amount of calories that are about to be subtracted would put you below your daily limit! Continue?", "You're overeating!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				switch (dialogResult) {
				case DialogResult.Yes:
					safetosubtract = true;
					break;
				}
			} else {
				safetosubtract = true;
			}
			switch (safetosubtract) {
			case true:
				GlobalVariables.calories = GlobalVariables.calories - tempcalories;
				break;
			}
			Storage.writeRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, false);
			Functions.checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
			userServingInputTextBox.Value = 1;
			
		}
		
		protected internal void addCalories (object sender, EventArgs e)
		{
			float tempcalories = Functions.modifyCalories (userServingInputTextBox);
			if (GlobalVariables.calories + tempcalories > 2140f) {
				DialogResult dialogResult = MessageBox.Show ("The amount of calories that you are trying to add would put you over your daily limit, and is not allowed.", "You're overeating.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			} else {
				GlobalVariables.calories = GlobalVariables.calories + tempcalories;
			}
			Storage.writeRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, false);
			Functions.checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
			userServingInputTextBox.Value = 1;
		}
		
		protected internal void findNextSearchItem (object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty (searchBar.Text) || searchBar.Text == " ") {
				return;
			}
			if (GlobalVariables.selectedListItem >= foodList.Items.Count - 1) {
				Functions.find (0, searchBar.Text, null, GlobalVariables.exactSearch, foodList);
			} else {
				Functions.find (GlobalVariables.selectedListItem, searchBar.Text, GlobalVariables.foodNameList [GlobalVariables.selectedListItem], GlobalVariables.exactSearch, foodList);
			}
		}
		
		protected internal void ExactSearchCheckBoxCheckedChanged (object sender, EventArgs e)
		{
			GlobalVariables.exactSearch = exactSearchCheckBox.Checked;
		}
		
		protected internal void CalorieRadioButtonCheckedChanged (object sender, EventArgs e)
		{
			Functions.checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
		}
		
		protected internal void TimeRadioButtonCheckedChanged (object sender, EventArgs e)
		{
			Functions.checkDateValidity (GlobalVariables.nowDate, GlobalVariables.dateReset, Storage.checkRegistryValues (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue));
			Functions.checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
		}
		
		protected internal void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
		{
			Functions.dumpFoodPropertiesList (foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
			GlobalVariables.addItem = newItemCheckbox.Checked;
			foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
		}
		
		protected internal void resetCaloriesButtonClicked (object sender, EventArgs e)
		{
			manualCalorieEditBox.Value = (decimal)GlobalVariables.totalCaloriesPerDay;
		}
		
		protected internal void zeroOutCaloriesButtonClicked (object sender, EventArgs e)
		{
			manualCalorieEditBox.Value = 0;
		}
		
		protected internal void manualSubmitButtonClicked (object sender, EventArgs e)
		{
			GlobalVariables.calories = (float)manualCalorieEditBox.Value;
			Storage.writeRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, false);
			Functions.checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
		}
		
		protected internal void RefreshCaloriesTimeButtonClick (object sender, EventArgs e)
		{
			Functions.checkDateValidity (GlobalVariables.nowDate, GlobalVariables.dateReset, Storage.checkRegistryValues (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue));
			Functions.checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
		}
		
		//MAIN//
	}

	public static class Functions
	{
		//		General functions
		
		public static void checkDateValidity (DateTime dateToCompareTo, DateTime dateToCheck, bool firstProgramUse)
		{
			const bool b = true;
			Storage.readRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
			if (!Equals (DateTime.Compare (dateToCompareTo, dateToCheck), -1) || firstProgramUse == b) {
				if (Equals (Registry.LocalMachine.OpenSubKey (GlobalVariables.registryAppenedValue + GlobalVariables.registryMainValue), null)) {
					Registry.LocalMachine.CreateSubKey (GlobalVariables.registryAppenedValue + GlobalVariables.registryMainValue);
				}
				Storage.writeRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, b);
			}
		}

		public static void checkCurrentRadioButton (RadioButton timeRadioButton, RadioButton calorieRadioButton, Label caloriesLabel)
		{
			const bool b = true;
			if (Equals (timeRadioButton.Checked, b)) {
				writeToObject (caloriesLabel, 1);
			} else if (Equals (calorieRadioButton.Checked, b)) {
				writeToObject (caloriesLabel, 0);
			}
		}
		
		static void writeToObject (Label labelToChange, int objectNumber)
		{
			string[] messages = {
				string.Format ("Calories Left For The Day: {0}", GlobalVariables.calories),
				string.Format ("Calories will reset on {0:MMMM dd} at {1:hh:mm tt}", GlobalVariables.dateReset, GlobalVariables.dateReset)
					
			};
			var font = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			Font[] fontStyle = {
				font,
				font
					
			};
			var middleCenter = ContentAlignment.MiddleCenter;
			ContentAlignment[] objectAlignment = {
				middleCenter,
				middleCenter
					
			};
			if (Equals (labelToChange, null))
				return;
			labelToChange.Font = fontStyle [objectNumber];
			labelToChange.Text = messages [objectNumber];
			labelToChange.TextAlign = objectAlignment [objectNumber];
		}
		
		public static void InitializeForms (ListBox foodList, RadioButton calorieRadioButton, RadioButton timeRadioButton, Label caloriesLabel, Label Seperator1,
		                                    Label Seperator2, Label howManyServingsLabel, TextBox searchBar, Button deleteSelectedFoodItemButton,
		                                    Button clearSearchBarButton, TextBox foodNameEditBox, TextBox definerEditBox, NumericUpDown servingSizeEditBox,
		                                    NumericUpDown caloriesPerServingEditBox, NumericUpDown manualCalorieEditBox)
		{
			
			const BorderStyle fixed3D = BorderStyle.Fixed3D;
			const HorizontalAlignment center = HorizontalAlignment.Center;
			const bool b = true;
			GlobalVariables.mainForm.Text = GlobalVariables.registryMainValue;
			calorieRadioButton.Checked = b;
			caloriesLabel.BorderStyle = fixed3D;
			Seperator1.AutoSize = false;
			Seperator1.BorderStyle = fixed3D;
			Seperator1.Width = 1;
			searchBar.TextAlign = center;
			Seperator2.AutoSize = false;
			Seperator2.BorderStyle = fixed3D;
			Seperator2.Height = 2;
			searchBar.TextAlign = center;
			refresh_foodList (foodList);
			checkDateValidity (GlobalVariables.nowDate, GlobalVariables.dateReset, Storage.checkRegistryValues (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue));
			deleteSelectedFoodItemButton.Enabled = false;
			clearSearchBarButton.Enabled = false;
			foodList.SetSelected (0, b);
			GlobalVariables.selectedListItem = 0;
			howManyServingsLabel.Text = string.Format ("How many {0}s do you plan on eating?", GlobalVariables.definersList [GlobalVariables.selectedListItem]);
			foodNameEditBox.Text = GlobalVariables.foodNameList [0];
			foodNameEditBox.TextAlign = center;
			servingSizeEditBox.Text = GlobalVariables.servingSizeList [0].ToString ();
			servingSizeEditBox.TextAlign = center;
			definerEditBox.Text = GlobalVariables.definersList [0];
			definerEditBox.TextAlign = center;
			caloriesPerServingEditBox.Text = GlobalVariables.caloriesPerServingList [0].ToString ();
			caloriesPerServingEditBox.TextAlign = center;
			checkCurrentRadioButton (timeRadioButton, calorieRadioButton, caloriesLabel);
			manualCalorieEditBox.Value = decimal.Parse (GlobalVariables.calories.ToString ());
		}

		public static void dumpFoodPropertiesList (TextBox foodNameEditBox, NumericUpDown servingSizeEditBox,
		                                           NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox)
		{
			foodNameEditBox.Clear ();
			servingSizeEditBox.Value = 0;
			caloriesPerServingEditBox.Value = 0;
			definerEditBox.Clear ();
		}

		public static void refresh_foodList (ListBox foodList)
		{
			GlobalVariables.foodNameList.Clear ();
			GlobalVariables.servingSizeList.Clear ();
			GlobalVariables.caloriesPerServingList.Clear ();
			GlobalVariables.definersList.Clear ();
			Storage.readFoodTable ("Text Files\\", "food.table");
			foodList.DataSource = null;
			foodList.Items.Clear ();
			List<string> foodNameList;
			foodNameList = Enumerable.ToList (GlobalVariables.foodNameList.Values);
			foodList.DataSource = foodNameList;
		}

		public static void foodPropertiesSwitch (ListBox foodList, TextBox foodNameEditBox, NumericUpDown servingSizeEditBox,
		                                         NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox)
		{
			if (Equals (GlobalVariables.addItem, false)) {
				GlobalVariables.foodNameList [GlobalVariables.selectedListItem] = foodNameEditBox.Text;
				GlobalVariables.servingSizeList [GlobalVariables.selectedListItem] = float.Parse (servingSizeEditBox.Text);
				GlobalVariables.caloriesPerServingList [GlobalVariables.selectedListItem] = float.Parse (caloriesPerServingEditBox.Text);
				GlobalVariables.definersList [GlobalVariables.selectedListItem] = definerEditBox.Text;
				Storage.writeFoodTable ("Text Files\\", "food.table", new string[4] {
					null,
					null,
					null,
					null
				}
				);
			} else {
				Storage.writeFoodTable ("Text Files\\", "food.table", new string[4] {
					foodNameEditBox.Text,
					servingSizeEditBox.Text,
					caloriesPerServingEditBox.Text,
					definerEditBox.Text
				}
				);
				newItemCheckbox.Checked = false;
			}
			refresh_foodList (foodList);
		}

		public static float modifyCalories (NumericUpDown userServingInputTextBox)
		{
			Storage.readRegistry (GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
			return GlobalVariables.caloriesPerServingList [GlobalVariables.selectedListItem] * float.Parse (userServingInputTextBox.Text) / GlobalVariables.servingSizeList [GlobalVariables.selectedListItem];
		}

		public static void find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, ListBox foodList)
		{
			for (int i = offset; i < foodList.Items.Count; i++) {
				const bool b = true;
				if (Equals (exactSearch, b)) {
					if (Equals (foodList.Items [i].ToString ().Equals (stringToFind, StringComparison.CurrentCultureIgnoreCase), b) &&
					    !foodList.Items [i].ToString ().Equals (stringToAvoid, StringComparison.CurrentCultureIgnoreCase)) {
						foodList.SelectedIndex = i;
						break;
					}
				} else {
					if (Equals (foodList.Items [i].ToString ().Contains (stringToFind, StringComparison.CurrentCultureIgnoreCase), b) &&
					    !foodList.Items [i].ToString ().Equals (stringToAvoid, StringComparison.CurrentCultureIgnoreCase)) {
						foodList.SelectedIndex = i;
						break;
					}
				}
			}
		}

		public static bool Contains (this string source, string toCheck, StringComparison comp)
		{
			return source.IndexOf (toCheck, comp) >= 0;
		}
	}

	public static class Storage
	{
		
		//		Functions that relate to storage
		
		public static void readFoodTable (string directory, string file)
		{
			int position = 0;
			String line;
			if (!File.Exists (directory + "food table.txt")) {
				using (StreamReader sr = new StreamReader (directory + file)) {
					int number = 0;
					while (!string.IsNullOrEmpty ((line = sr.ReadLine ()))) {
						if (line.Contains ("-", StringComparison.CurrentCultureIgnoreCase)) {
							position++;
							number = 0;
						} else {
							switch (number) {
							case 0:
								GlobalVariables.foodNameList.Add (position, line);
								break;
							case 1:
								GlobalVariables.servingSizeList.Add (position, float.Parse (line));
								break;
							case 2:
								GlobalVariables.caloriesPerServingList.Add (position, float.Parse (line));
								break;
							case 3:
								GlobalVariables.definersList.Add (position, line);
								break;
							}
							number++;
						}
					}
					sr.Close ();
				}
			} else {
				using (StreamReader sr = new StreamReader (directory + "food table.txt")) {
					int[] number = {
						0,
						0,
						0,
						0
					};
					while (!Equals ((line = sr.ReadLine ()), null)) {
						if (line.Contains ("-", StringComparison.CurrentCultureIgnoreCase)) {
							position++;
						} else {
							switch (position) {
							case 0:
								GlobalVariables.foodNameList.Add (number [position], line);
								break;
							case 1:
								GlobalVariables.servingSizeList.Add (number [position], float.Parse (line));
								break;
							case 2:
								GlobalVariables.caloriesPerServingList.Add (number [position], float.Parse (line));
								break;
							case 3:
								GlobalVariables.definersList.Add (number [position], line);
								break;
							}
							number [position]++;
						}
					}
					sr.Close ();
				}
				File.Delete (directory + "food table.txt");
			}
		}

		public static void writeFoodTable (string directory, string file, string[] addString)
		{
			string finalstring = null;
			const string seperator = "-------------------------------------------------------------------------\n";
			for (int i = 0; i < GlobalVariables.foodNameList.Count; i++) {
				finalstring = finalstring + GlobalVariables.foodNameList [i] + "\n";
				finalstring = finalstring + GlobalVariables.servingSizeList [i] + "\n";
				finalstring = finalstring + GlobalVariables.caloriesPerServingList [i] + "\n";
				finalstring = finalstring + GlobalVariables.definersList [i] + "\n";
				finalstring = finalstring + seperator;
			}
			if (!string.IsNullOrEmpty (addString [0])) {
				finalstring = string.Format ("{0}{1}\n", finalstring, addString [0]);
				finalstring = string.Format ("{0}{1}\n", finalstring, addString [1]);
				finalstring = string.Format ("{0}{1}\n", finalstring, addString [2]);
				finalstring = string.Format ("{0}{1}\n", finalstring, addString [3]);
				finalstring = string.Format ("{0}{1}", finalstring, seperator);
			}
			File.WriteAllText (directory + file, finalstring);
		}

		public static void readRegistry (string appendedRegistryValue, string registyValue)
		{
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (appendedRegistryValue + registyValue, true)) {
				string temp = tempKey.GetValue ("Calories Left for the Day").ToString ();
				GlobalVariables.calories = float.Parse (temp);
				DateTime.TryParseExact (tempKey.GetValue ("Last Used Date").ToString (), new [] { "yyyy MMMMM dd hh:mm:ss tt" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out GlobalVariables.dateReset);
			}
		}

		public static void writeRegistry (string appendedRegistryValue, string registyValue, bool reset)
		{
			const bool b = true;
			if (Equals (reset, false)) {
				using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (appendedRegistryValue + registyValue, b)) {
					tempKey.SetValue ("Calories Left for the Day", GlobalVariables.calories.ToString ());
				}
			} else {
				using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (GlobalVariables.registryAppenedValue + GlobalVariables.registryMainValue, b)) {
					tempKey.SetValue ("Calories Left for the Day", GlobalVariables.totalCaloriesPerDay.ToString (), RegistryValueKind.String);
					tempKey.SetValue ("Last Used Date", GlobalVariables.nowDate.AddDays (1).ToString ("yyyy MMMMM dd hh:mm:ss tt"), RegistryValueKind.String);
				}
			}
		}

		public static bool checkRegistryValues (string appendedRegistryValue, string registyValue)
		{
			if (!Equals (Registry.LocalMachine.OpenSubKey (appendedRegistryValue + registyValue), null))
				return false;
			Registry.LocalMachine.CreateSubKey (appendedRegistryValue + registyValue);
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (appendedRegistryValue + registyValue, true)) {
				tempKey.SetValue ("Calories Left for the Day", GlobalVariables.totalCaloriesPerDay.ToString (), RegistryValueKind.String);
				tempKey.SetValue ("Last Used Date", GlobalVariables.nowDate.AddDays (1).ToString ("yyyy MMMMM dd hh:mm:ss tt"), RegistryValueKind.String);
			}
			return true;
		}
	}
}
