using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Globalization;
using System.IO;


namespace Weight_Watching_Program_Plus
{
	//Total_Calories_Per_Day = 2140;
	
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
			Functions.InitializeForms(foodList,calorieRadioButton,caloriesLabel,Seperator1,Seperator2,howManyServingsLabel,searchBar,deleteSelectedFoodItemButton,
			                          clearSearchBarButton,foodNameEditBox,definerEditBox,servingSizeEditBox,caloriesPerServingEditBox, manualCalorieEditBox);
			//
		}

		void SearchBarTextChanged (object sender, EventArgs e)
		{
			if (searchBar.Text != "" && searchBar.Text != " " && searchBar.Text != null) {
				Functions.find (0, searchBar.Text, null, GlobalVariables.exactSearch, foodList);
			} else {
				foodList.ClearSelected ();
			}
		}

		void searchBarFocusGranted (object sender, EventArgs e)
		{
			searchBar.Clear ();
			clearSearchBarButton.Enabled = true;
			Font tempfont = new Font ("Times New Roman", 10f);
			searchBar.Font = tempfont;
		}

		void clearSearchBarButtonClicked (object sender, EventArgs e)
		{
			searchBar.Clear ();
			clearSearchBarButton.Enabled = false;
			Font tempfont = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
			searchBar.Font = tempfont;
			searchBar.Text = "Click Here to Search the Food List";
			searchBar.TextAlign = HorizontalAlignment.Center;
		}

		void foodListSelectedIndexChanged (object sender, EventArgs e)
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

		void foodListLeaveFocus (object sender, EventArgs e)
		{
			if (deleteSelectedFoodItemButton.Enabled == true && foodList.Items.Count <= 0) {
				deleteSelectedFoodItemButton.Enabled = false;
			}
			foodList.ClearSelected ();
		}

		void foodListEnterFocus (object sender, EventArgs e)
		{
			GlobalVariables.selectedListItem = foodList.SelectedIndex;
			if (deleteSelectedFoodItemButton.Enabled == false && foodList.Items.Count > 0) {
				deleteSelectedFoodItemButton.Enabled = true;
			}
		}

		void deleteFoodItemFromTable (object sender, EventArgs e)
		{
			Functions.dumpFoodPropertiesList (foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
			GlobalVariables.foodNameList.Remove (GlobalVariables.selectedListItem);
			GlobalVariables.ignoreList [GlobalVariables.selectedListItem] = true;
			Storage.writeFoodTable ("Text Files\\", "food table.txt", new string[4] {
				null,
				null,
				null,
				null
			}
			);
			Functions.refresh_foodList (foodList);
		}

		void setFoodPropertiesButtonClicked (object sender, EventArgs e)
		{
			string oldtext = foodNameEditBox.Text;
			Functions.foodPropertiesSwitch (foodList, foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, newItemCheckbox);
			Functions.find (0, oldtext, null, true, foodList);
		}

		void subtractCalories (object sender, EventArgs e)
		{
			float tempcalories = Functions.modifyCalories (userServingInputTextBox);
			bool safetosubtract = false;
			if (GlobalVariables.calories - tempcalories < 0) {
				DialogResult dialogResult = MessageBox.Show ("The amount of calories that are about to be subtracted would put you below your daily limit! Continue?", "You're overeating!", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes) {
					safetosubtract = true;
				}
			} else {
				safetosubtract = true;
			}
			if (safetosubtract == true) {
				GlobalVariables.calories = GlobalVariables.calories - tempcalories;
			}
			Storage.writeRegistry (GlobalVariables.regappend, "Weight Watching Program+");
			Font tempfont = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			caloriesLabel.Font = tempfont;
			caloriesLabel.Text = "Calories Left For The Day: " + GlobalVariables.calories.ToString ();
			caloriesLabel.TextAlign = ContentAlignment.MiddleCenter;
			
		}

		void addCalories (object sender, EventArgs e)
		{
			float tempcalories = Functions.modifyCalories (userServingInputTextBox);
			if (GlobalVariables.calories + tempcalories > 2140f) {
				DialogResult dialogResult = MessageBox.Show ("That amount would put you over your daily allowance, and isn't allowed.", "You're overeating!");
			} else {
				GlobalVariables.calories = GlobalVariables.calories + tempcalories;
			}
			Storage.writeRegistry (GlobalVariables.regappend, "Weight Watching Program+");
			Font tempfont = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			caloriesLabel.Font = tempfont;
			caloriesLabel.Text = "Calories Left For The Day: " + GlobalVariables.calories.ToString ();
			caloriesLabel.TextAlign = ContentAlignment.MiddleCenter;
		}

		void findNextSearchItem (object sender, EventArgs e)
		{
			if (searchBar.Text != "" && searchBar.Text != " " && searchBar.Text != null) {
				if (GlobalVariables.selectedListItem >= foodList.Items.Count - 1) {
					Functions.find (0, searchBar.Text, null, GlobalVariables.exactSearch, foodList);
				} else {
					Functions.find (GlobalVariables.selectedListItem, searchBar.Text, GlobalVariables.foodNameList [GlobalVariables.selectedListItem], GlobalVariables.exactSearch, foodList);
				}
			}
		}

		void ExactSearchCheckBoxCheckedChanged (object sender, EventArgs e)
		{
			GlobalVariables.exactSearch = exactSearchCheckBox.Checked;
		}

		void CalorieRadioButtonCheckedChanged (object sender, EventArgs e)
		{
			Font tempfont = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			caloriesLabel.Font = tempfont;
			caloriesLabel.Text = "Calories Left For The Day: " + GlobalVariables.calories.ToString ();
			caloriesLabel.TextAlign = ContentAlignment.MiddleCenter;
		}

		void TimeRadioButtonCheckedChanged (object sender, EventArgs e)
		{
			Font tempfont = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			caloriesLabel.Font = tempfont;
			caloriesLabel.Text = "Calories will reset on " + GlobalVariables.dateReset.ToString ("MMMM dd") + " at " + GlobalVariables.dateReset.ToString ("hh:mm tt");
			caloriesLabel.TextAlign = ContentAlignment.MiddleCenter;
		}

		void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
		{
			Functions.dumpFoodPropertiesList (foodNameEditBox,servingSizeEditBox,caloriesPerServingEditBox,definerEditBox);
			if (newItemCheckbox.Checked == true) {
				GlobalVariables.addItem = true;
				foodPropertiesButton.Text = "Add this new food item";
			} else {
				GlobalVariables.addItem = false;
				foodPropertiesButton.Text = "Set Food Item Properties";
			}
		}
		
		void resetCaloriesButtonClicked (object sender, EventArgs e)
		{
			manualCalorieEditBox.Value = 2140;
		}
		void zeroOutCaloriesButtonClicked (object sender, EventArgs e)
		{
			manualCalorieEditBox.Value = 0;
		}
		void manualSubmitButtonClicked (object sender, EventArgs e)
		{
			GlobalVariables.calories = (float)manualCalorieEditBox.Value;
			Storage.writeRegistry(GlobalVariables.regappend,"Weight Watching Program+");
			if(calorieRadioButton.Checked == true){
				caloriesLabel.Text = "Calories Left For The Day: " + GlobalVariables.calories.ToString ();
			}
		}
	}

	public static class GlobalVariables
	{
		public static Dictionary<int, string> foodNameList = new Dictionary<int, string> ();
		public static Dictionary<int, float> servingSizeList = new Dictionary<int, float> ();
		public static Dictionary<int, float> caloriesPerServingList = new Dictionary<int, float> ();
		public static Dictionary<int, string> definersList = new Dictionary<int, string> ();
		public static Dictionary<int, bool> ignoreList = new Dictionary<int, bool> ();
		public static int numentries;
		public static int selectedListItem;
		
		public static bool exactSearch = false;
		public static bool addItem = false;
		
		public static DateTime dateReset;
		
		public static float calories;
		
		public static readonly string regappend = "SOFTWARE\\Wow6432Node\\";
	}

	public static class Functions
	{
		
		//		General functions
		
		public static void InitializeForms(ListBox foodList, RadioButton calorieRadioButton, Label caloriesLabel, Label Seperator1, 
		                                   Label Seperator2, Label howManyServingsLabel, TextBox searchBar, Button deleteSelectedFoodItemButton,
		                                  Button clearSearchBarButton, TextBox foodNameEditBox, TextBox definerEditBox, NumericUpDown servingSizeEditBox,
		                                 NumericUpDown caloriesPerServingEditBox, NumericUpDown manualCalorieEditBox){
			calorieRadioButton.Checked = true;
			caloriesLabel.BorderStyle = BorderStyle.Fixed3D;
			Seperator1.AutoSize = false;
			Seperator1.BorderStyle = BorderStyle.Fixed3D;
			Seperator1.Width = 1;
			searchBar.TextAlign = HorizontalAlignment.Center;
			Seperator2.AutoSize = false;
			Seperator2.BorderStyle = BorderStyle.Fixed3D;
			Seperator2.Height = 2;
			searchBar.TextAlign = HorizontalAlignment.Center;
			Functions.refresh_foodList (foodList);
			deleteSelectedFoodItemButton.Enabled = false;
			clearSearchBarButton.Enabled = false;
			foodList.SetSelected (0, true);
			GlobalVariables.selectedListItem = 0;
			howManyServingsLabel.Text = "How many " + GlobalVariables.definersList [GlobalVariables.selectedListItem] + "s do you plan on eating?";
			foodNameEditBox.Text = GlobalVariables.foodNameList [0];
			foodNameEditBox.TextAlign = HorizontalAlignment.Center;
			servingSizeEditBox.Text = GlobalVariables.servingSizeList [0].ToString ();
			servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
			definerEditBox.Text = GlobalVariables.definersList [0];
			definerEditBox.TextAlign = HorizontalAlignment.Center;
			caloriesPerServingEditBox.Text = GlobalVariables.caloriesPerServingList [0].ToString ();
			caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
			Storage.readRegistry (GlobalVariables.regappend, "Weight Watching Program+");
			Font tempfont = new Font ("Microsoft Sans Serif", 12f, FontStyle.Bold);
			caloriesLabel.Font = tempfont;
			caloriesLabel.Text = "Calories Left For The Day: " + GlobalVariables.calories.ToString ();
			caloriesLabel.TextAlign = ContentAlignment.MiddleCenter;
			manualCalorieEditBox.Value = decimal.Parse(GlobalVariables.calories.ToString());
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
			GlobalVariables.numentries = 0;
			GlobalVariables.ignoreList.Clear ();
			GlobalVariables.foodNameList.Clear ();
			GlobalVariables.servingSizeList.Clear ();
			GlobalVariables.caloriesPerServingList.Clear ();
			GlobalVariables.definersList.Clear ();
			Storage.readFoodTable ("Text Files\\", "food table.txt");
			foodList.Items.Clear ();
			for (int i = 0; i < GlobalVariables.numentries; i++) {
				foodList.Items.Add (GlobalVariables.foodNameList [i]);
			}
		}

		public static void foodPropertiesSwitch (ListBox foodList, TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, 
		                                        NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox)
		{
			if (GlobalVariables.addItem == false) {
				GlobalVariables.foodNameList [GlobalVariables.selectedListItem] = foodNameEditBox.Text;
				GlobalVariables.servingSizeList [GlobalVariables.selectedListItem] = float.Parse (servingSizeEditBox.Text);
				GlobalVariables.caloriesPerServingList [GlobalVariables.selectedListItem] = float.Parse (caloriesPerServingEditBox.Text);
				GlobalVariables.definersList [GlobalVariables.selectedListItem] = definerEditBox.Text;
				Storage.writeFoodTable ("Text Files\\", "food table.txt", new string[4] {
					null,
					null,
					null,
					null
				}
				);
			} else {
				Storage.writeFoodTable ("Text Files\\", "food table.txt", new string[4] { 
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
			float tempcalories = 0f;
			Storage.readRegistry (GlobalVariables.regappend, "Weight Watching Program+");
			tempcalories = GlobalVariables.caloriesPerServingList [GlobalVariables.selectedListItem] * float.Parse (userServingInputTextBox.Text) / GlobalVariables.servingSizeList [GlobalVariables.selectedListItem];
			return tempcalories;
		}

		public static void find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, ListBox foodList)
		{
			for (int i = offset; i < foodList.Items.Count; i++) {
				if (exactSearch == true) {
					if (foodList.Items [i].ToString ().Equals (stringToFind, StringComparison.CurrentCultureIgnoreCase) == true &&
					   !foodList.Items [i].ToString ().Equals (stringToAvoid, StringComparison.CurrentCultureIgnoreCase)) {
						foodList.SelectedIndex = i;
						break;
					}
				} else {
					if (foodList.Items [i].ToString ().Contains (stringToFind, StringComparison.CurrentCultureIgnoreCase) == true &&
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

	public class Storage
	{
		
		//		Functions that relate to storage
		
		public static void readFoodTable (string directory, string file)
		{
			using (StreamReader sr = new StreamReader (directory + file)) {
				String line;
				int position = 0;
				int[] number = new int[4] {
					0,
					0,
					0,
					0
				};
				while ((line = sr.ReadLine ()) != null) {
					if (Functions.Contains (line, "-", StringComparison.CurrentCultureIgnoreCase)) {
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
				GlobalVariables.numentries = number [0];
				sr.Close ();
			}
			for (int i = 0; i <= GlobalVariables.numentries; i++) {
				GlobalVariables.ignoreList.Add (i, false);
			}
		}

		public static void writeFoodTable (string directory, string file, string[] addString)
		{
			string finalstring = "";
			for (int i = 0; i < GlobalVariables.numentries; i++) {
				if (GlobalVariables.ignoreList [i] == false) {
					finalstring = finalstring + GlobalVariables.foodNameList [i] + "\n";
				}
			}
			if (addString [0] != null && addString [0] != "") {
				finalstring = finalstring + addString [0] + "\n";
			}
			finalstring = finalstring + "-------------------------------------------------------------------------" + "\n";
			for (int i = 0; i < GlobalVariables.numentries; i++) {
				if (GlobalVariables.ignoreList [i] == false) {
					finalstring = finalstring + GlobalVariables.servingSizeList [i] + "\n";
				}
			}
			if (addString [1] != null && addString [1] != "") {
				finalstring = finalstring + addString [1] + "\n";
			}
			finalstring = finalstring + "-------------------------------------------------------------------------" + "\n";
			for (int i = 0; i < GlobalVariables.numentries; i++) {
				if (GlobalVariables.ignoreList [i] == false) {
					finalstring = finalstring + GlobalVariables.caloriesPerServingList [i] + "\n";
				}
			}
			if (addString [2] != null && addString [2] != "") {
				finalstring = finalstring + addString [2] + "\n";
			}
			finalstring = finalstring + "-------------------------------------------------------------------------" + "\n";
			for (int i = 0; i < GlobalVariables.numentries; i++) {
				if (GlobalVariables.ignoreList [i] == false) {
					finalstring = finalstring + GlobalVariables.definersList [i] + "\n";
				}
			}
			if (addString [3] != null && addString [3] != "") {
				finalstring = finalstring + addString [3] + "\n";
			}
			finalstring = finalstring + "-------------------------------------------------------------------------" + "\n";
			File.WriteAllText (directory + file, finalstring);
		}

		public static void readRegistry (string appendedRegistryValue, string registyValue)
		{
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (appendedRegistryValue + registyValue, true)) {
				string temp = tempKey.GetValue ("Calories Left for the Day").ToString ();
				GlobalVariables.calories = float.Parse (temp);
				DateTime.TryParseExact (tempKey.GetValue ("Last Used Date").ToString (), new string[] { "yyyy MMMMM dd hh:mm:ss tt" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out GlobalVariables.dateReset);
			}
		}

		public static void writeRegistry (string appendedRegistryValue, string registyValue)
		{
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey (appendedRegistryValue + registyValue, true)) {
				tempKey.SetValue ("Calories Left for the Day", GlobalVariables.calories.ToString ());
			}
		}

		
	}
}
