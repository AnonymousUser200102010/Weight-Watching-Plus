using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;


namespace WeightWatchingProgramPlus
{
    /// <summary>
    /// Main Window and all tabs.
    /// </summary>
    public partial class MainForm : Form
    {
        private Storage Storage = new Storage ();
        private Functions Functions = new Functions ();
        private ErrorHandler ErrorHandler = new ErrorHandler ();

        public MainForm ()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //
            GlobalVariables.mainForm = this;
            Functions.InitializeForms(
                foodList,
                calorieRadioButton,
                timeRadioButton,
                caloriesLabel,
                Seperator1,
                Seperator2,
                howManyServingsLabel,
                searchBar,
                deleteSelectedFoodItemButton,
                clearSearchBarButton,
                foodNameEditBox,
                definerEditBox,
                servingSizeEditBox,
                caloriesPerServingEditBox,
                manualCalorieEditBox
            );
			
            //
        }
		
        //MAIN//
        protected internal virtual void SearchBarTextChanged (object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchBar.Text) && !Equals(searchBar.Text, " "))
            {
                Functions.Find(0, searchBar.Text, null, GlobalVariables.ExactSearch, foodList);
                return;
            }
            foodList.ClearSelected();
        }

        protected internal void SearchBarFocusGranted (object sender, EventArgs e)
        {
            searchBar.Clear();
            clearSearchBarButton.Enabled = true;
            Font tempfont = new Font ("Times New Roman", 10f);
            searchBar.Font = tempfont;
        }

        protected internal void ClearSearchBarButtonClicked (object sender, EventArgs e)
        {
            searchBar.Clear();
            clearSearchBarButton.Enabled = false;
            Font tempfont = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
            searchBar.Font = tempfont;
            searchBar.Text = "Click Here to Search the Food List";
            searchBar.TextAlign = HorizontalAlignment.Center;
        }

        protected internal virtual void FoodListSelectedIndexChanged (object sender, EventArgs e)
        {
            if (foodList.SelectedIndex > -1)
            {
                GlobalVariables.SelectedListItem = foodList.SelectedIndex;
                howManyServingsLabel.Text = string.Format(
                    "How many {0}s do you plan on eating?",
                    FoodRelated.definersList [foodList.SelectedIndex]
                );
                foodNameEditBox.Text = FoodRelated.foodNameList [foodList.SelectedIndex];
                foodNameEditBox.TextAlign = HorizontalAlignment.Center;
                servingSizeEditBox.Text = FoodRelated.servingSizeList [foodList.SelectedIndex].ToString();
                servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
                definerEditBox.Text = FoodRelated.definersList [foodList.SelectedIndex];
                definerEditBox.TextAlign = HorizontalAlignment.Center;
                caloriesPerServingEditBox.Text = FoodRelated.caloriesPerServingList [foodList.SelectedIndex].ToString();
                caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
            }
        }

        protected internal void FoodListLeaveFocus (object sender, EventArgs e)
        {
            if (deleteSelectedFoodItemButton.Enabled && foodList.Items.Count != 0)
            {
                deleteSelectedFoodItemButton.Enabled = false;
            }
            foodList.ClearSelected();
        }

        protected internal void FoodListEnterFocus (object sender, EventArgs e)
        {
            GlobalVariables.SelectedListItem = foodList.SelectedIndex;
            if (!deleteSelectedFoodItemButton.Enabled && foodList.Items.Count > 0)
            {
                deleteSelectedFoodItemButton.Enabled = true;
            }
        }

        protected internal void DeleteFoodItemFromTable (object sender, EventArgs e)
        {
            Functions.DumpFoodPropertiesList(
                foodNameEditBox,
                servingSizeEditBox,
                caloriesPerServingEditBox,
                definerEditBox
            );
            FoodRelated.foodNameList.Remove(GlobalVariables.SelectedListItem);
            Storage.WriteFoodTable("Text Files\\", "food.table", new string[] {
                null,
                null,
                null,
                null
            }
            );
            Functions.Refresh_foodList(foodList);
        }

        protected internal void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
        {
            string oldtext = foodNameEditBox.Text;
            Functions.FoodPropertiesSwitch(
                foodList,
                foodNameEditBox,
                servingSizeEditBox,
                caloriesPerServingEditBox,
                definerEditBox,
                newItemCheckbox
            );
            Functions.Find(0, oldtext, null, true, foodList);
        }

        protected internal void SubtractCalories (object sender, EventArgs e)
        {
            float tempcalories = Functions.ModifyCalories(userServingInputTextBox, false);
            bool safetosubtract = false;
            if (FoodRelated.Calories - tempcalories < 0)
            {
                DialogResult dialogResult = MessageBox.Show(
                                                "The amount of calories that are about to be subtracted would put you below your daily limit! Continue?",
                                                "You're overeating!",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning,
                                                MessageBoxDefaultButton.Button2
                                            );
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        safetosubtract = true;
                        break;
                }
            }
            else
            {
                safetosubtract = true;
            }
            switch (safetosubtract)
            {
                case true:
                    FoodRelated.Calories = FoodRelated.Calories - tempcalories;
                    break;
            }
            Storage.WriteRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, false);
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
            userServingInputTextBox.Value = 1;
			
        }

        protected internal void AddCalories (object sender, EventArgs e)
        {
            float tempcalories = Functions.ModifyCalories(userServingInputTextBox, true);
            if (FoodRelated.Calories + tempcalories > 2140f)
            {
                ErrorHandler.errorMessageBox(
                    "The amount of calories that you are trying to add would put you over your daily limit, and is not allowed.",
                    addCaloriesButtonMain,
                    1,
                    false
                );
            }
            else
            {
                FoodRelated.Calories = FoodRelated.Calories + tempcalories;
            }
            Storage.WriteRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, false);
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
            userServingInputTextBox.Value = 1;
        }

        protected internal void FindNextSearchItem (object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBar.Text) || Equals(searchBar.Text, " "))
            {
                return;
            }
            if (GlobalVariables.SelectedListItem >= foodList.Items.Count - 1)
            {
                Functions.Find(0, searchBar.Text, null, GlobalVariables.ExactSearch, foodList);
            }
            else
            {
                Functions.Find(
                    GlobalVariables.SelectedListItem,
                    searchBar.Text,
                    FoodRelated.foodNameList [GlobalVariables.SelectedListItem],
                    GlobalVariables.ExactSearch,
                    foodList
                );
            }
        }

        protected internal void ExactSearchCheckBoxCheckedChanged (object sender, EventArgs e)
        {
            GlobalVariables.ExactSearch = exactSearchCheckBox.Checked;
        }

        protected internal void CalorieRadioButtonCheckedChanged (object sender, EventArgs e)
        {
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
        }

        protected internal void TimeRadioButtonCheckedChanged (object sender, EventArgs e)
        {
            Validation.CheckDateValidity(
                GlobalVariables.nowDate,
                GlobalVariables.dateReset,
                Storage.CheckRegistryValues(
                    GlobalVariables.registryAppenedValue,
                    GlobalVariables.registryMainValue
                ),
                Storage
            );
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
        }

        protected internal void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
        {
            Functions.DumpFoodPropertiesList(
                foodNameEditBox,
                servingSizeEditBox,
                caloriesPerServingEditBox,
                definerEditBox
            );
            GlobalVariables.AddItem = newItemCheckbox.Checked;
            foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
        }

        protected internal void ResetCaloriesButtonClicked (object sender, EventArgs e)
        {
            manualCalorieEditBox.Value = (decimal)FoodRelated.totalCaloriesPerDay;
        }

        protected internal void ZeroOutCaloriesButtonClicked (object sender, EventArgs e)
        {
            manualCalorieEditBox.Value = 0;
        }

        protected internal void ManualSubmitButtonClicked (object sender, EventArgs e)
        {
            FoodRelated.Calories = (float)manualCalorieEditBox.Value;
            Storage.WriteRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, false);
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
        }

        protected internal void RefreshCaloriesTimeButtonClick (object sender, EventArgs e)
        {
            Validation.CheckDateValidity(
                GlobalVariables.nowDate,
                GlobalVariables.dateReset,
                Storage.CheckRegistryValues(
                    GlobalVariables.registryAppenedValue,
                    GlobalVariables.registryMainValue
                ),
                Storage
            );
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
        }
		
        //MAIN//
    }

    public static class Validation
    {
        //Functions whose primary purpose is verification and validation, but who don't have a more pressing function.
		
        public static void CheckDateValidity (DateTime dateToCompareTo, DateTime dateToCheck, bool firstProgramUse, Storage Storage)
        {
            Storage.ReadRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
            if (!Equals(DateTime.Compare(dateToCompareTo, dateToCheck), -1) || firstProgramUse)
            {
                if (Equals(
                        Registry.LocalMachine.OpenSubKey(GlobalVariables.registryAppenedValue + GlobalVariables.registryMainValue),
                        null
                    ))
                {
                    Registry.LocalMachine.CreateSubKey(GlobalVariables.registryAppenedValue + GlobalVariables.registryMainValue);
                }
                Storage.WriteRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue, true);
            }
        }

        public static void CheckCurrentRadioButton (RadioButton timeRadioButton, RadioButton calorieRadioButton, Label caloriesLabel, Functions Functions)
        {
            if (timeRadioButton.Checked)
            {
                Functions.WriteToObject(caloriesLabel, 1);
            }
            else if (calorieRadioButton.Checked)
            {
                Functions.WriteToObject(caloriesLabel, 0);
            }
        }

        public static bool Contains (this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
		
    }

    public class Functions
    {
        //		General functions
        Storage Storage = new Storage ();
        ErrorHandler ErrorHandler = new ErrorHandler ();

        public void WriteToObject (Label labelToChange, int objectNumber)
        {
            if (labelToChange == null)
            {
                throw new ArgumentNullException ("labelToChange", "WriteToObject: labelToChange");
            }
            string[] messages = {
                string.Format("Calories Left For The Day: {0}", FoodRelated.Calories),
                string.Format(
                    "Calories will reset on {0:MMMM dd} at {1:hh:mm tt}",
                    GlobalVariables.dateReset,
                    GlobalVariables.dateReset
                )
					
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
            labelToChange.Font = fontStyle [objectNumber];
            labelToChange.Text = messages [objectNumber];
            labelToChange.TextAlign = objectAlignment [objectNumber];
        }

        public void InitializeForms (ListBox foodList, RadioButton calorieRadioButton, RadioButton timeRadioButton, Label caloriesLabel, Label Seperator1,
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
            Refresh_foodList(foodList);
            Validation.CheckDateValidity(
                GlobalVariables.nowDate,
                GlobalVariables.dateReset,
                Storage.CheckRegistryValues(
                    GlobalVariables.registryAppenedValue,
                    GlobalVariables.registryMainValue
                ),
                Storage
            );
            deleteSelectedFoodItemButton.Enabled = false;
            clearSearchBarButton.Enabled = false;
            foodList.SetSelected(0, b);
            GlobalVariables.SelectedListItem = 0;
            howManyServingsLabel.Text = string.Format(
                "How many {0}s do you plan on eating?",
                FoodRelated.definersList [GlobalVariables.SelectedListItem]
            );
            foodNameEditBox.Text = FoodRelated.foodNameList [0];
            foodNameEditBox.TextAlign = center;
            servingSizeEditBox.Text = FoodRelated.servingSizeList [0].ToString();
            servingSizeEditBox.TextAlign = center;
            definerEditBox.Text = FoodRelated.definersList [0];
            definerEditBox.TextAlign = center;
            caloriesPerServingEditBox.Text = FoodRelated.caloriesPerServingList [0].ToString();
            caloriesPerServingEditBox.TextAlign = center;
            Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, new Functions ());
            manualCalorieEditBox.Value = decimal.Parse(FoodRelated.Calories.ToString());
        }

        public void DumpFoodPropertiesList (TextBox foodNameEditBox, NumericUpDown servingSizeEditBox,
                                            NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox)
        {
            foodNameEditBox.Clear();
            servingSizeEditBox.Value = 0;
            caloriesPerServingEditBox.Value = 0;
            definerEditBox.Clear();
        }

        public void Refresh_foodList (ListBox foodList)
        {
            FoodRelated.foodNameList.Clear();
            FoodRelated.servingSizeList.Clear();
            FoodRelated.caloriesPerServingList.Clear();
            FoodRelated.definersList.Clear();
            Storage.ReadFoodTable("Text Files\\", "food.table");
            foodList.DataSource = null;
            foodList.Items.Clear();
            foodList.DataSource = FoodRelated.foodNameList.Values.ToList();
            Storage.WriteFoodTable("Text Files\\", "food.table", new string[] { 
                null,
                null,
                null,
                null
            });
        }

        public  void FoodPropertiesSwitch (ListBox foodList, TextBox foodNameEditBox, NumericUpDown servingSizeEditBox,
                                           NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox)
        {
            if (string.IsNullOrWhiteSpace(foodNameEditBox.Text))
            {
                ErrorHandler.errorMessageBox("Please set a food name value!", foodNameEditBox, 0, true);
                return;
            }
            if (servingSizeEditBox.Value <= 0)
            {
                ErrorHandler.errorMessageBox("Please set a serving size value!", servingSizeEditBox, 0, true);
                return;
            }
            if (caloriesPerServingEditBox.Value <= 0)
            {
                ErrorHandler.errorMessageBox(
                    "Please set a calories per serving value!",
                    caloriesPerServingEditBox,
                    0,
                    true
                );
                return;
            }
            if (string.IsNullOrWhiteSpace(definerEditBox.Text))
            {
                ErrorHandler.errorMessageBox("Please set a definer value!", definerEditBox, 0, true);
                return;
            }
            if (!GlobalVariables.AddItem)
            {
                FoodRelated.foodNameList [GlobalVariables.SelectedListItem] = foodNameEditBox.Text;
                FoodRelated.servingSizeList [GlobalVariables.SelectedListItem] = float.Parse(servingSizeEditBox.Text);
                FoodRelated.caloriesPerServingList [GlobalVariables.SelectedListItem] = float.Parse(caloriesPerServingEditBox.Text);
                FoodRelated.definersList [GlobalVariables.SelectedListItem] = definerEditBox.Text;
                Storage.WriteFoodTable("Text Files\\", "food.table", new string[] {
                    null,
                    null,
                    null,
                    null
                }
                );
            }
            else
            {
                Storage.WriteFoodTable("Text Files\\", "food.table", new [] {
                    foodNameEditBox.Text,
                    servingSizeEditBox.Text,
                    caloriesPerServingEditBox.Text,
                    definerEditBox.Text
                }
                );
                newItemCheckbox.Checked = false;
            }
            Refresh_foodList(foodList);
        }

        public float ModifyCalories (NumericUpDown userServingInputTextBox, bool add)
        {
            int hour = DateTime.Now.Hour;
            string amPMDefiner = DateTime.Now.ToString("tt");
            Storage.ReadRegistry(GlobalVariables.registryAppenedValue, GlobalVariables.registryMainValue);
            float tempFloat = FoodRelated.caloriesPerServingList [GlobalVariables.SelectedListItem] * float.Parse(userServingInputTextBox.Text) / FoodRelated.servingSizeList [GlobalVariables.SelectedListItem];
            if (hour > 12 && hour < 4 && amPMDefiner.Equals("am", StringComparison.CurrentCultureIgnoreCase))
            {
                float midSnackPenalty = tempFloat / 10;
                if (midSnackPenalty <= 10)
                {
                    midSnackPenalty = 10;
                }
                string appliedSwitch = !add ? "applied" : "subtracted";
                DialogResult dialogResult = MessageBox.Show(
                                                string.Format(
                                                    "A midnight snacking penalty of {0} will be {1} if you continue.",
                                                    midSnackPenalty,
                                                    appliedSwitch
                                                ),
                                                "Midnight Snacking Penalty.",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Information,
                                                MessageBoxDefaultButton.Button2
                                            );
                if (dialogResult != DialogResult.OK)
                {
                    return 0;
                }
                if (!add)
                {
                    return tempFloat + midSnackPenalty;
                }
                return tempFloat - midSnackPenalty;
            }
            return tempFloat;
        }

        public void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, ListBox foodList)
        {
            for (int i = offset; i < foodList.Items.Count; i++)
            {
                if (exactSearch)
                {
                    if (foodList.Items [i].ToString().Equals(stringToFind, StringComparison.CurrentCultureIgnoreCase) &&
                        !foodList.Items [i].ToString().Equals(stringToAvoid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        foodList.SelectedIndex = i;
                        break;
                    }
                }
                else
                {
                    if (foodList.Items [i].ToString().Contains(stringToFind, StringComparison.CurrentCultureIgnoreCase) &&
                        !foodList.Items [i].ToString().Equals(stringToAvoid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        foodList.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
		
    }

    public class Storage
    {
		
        //		Functions that relate to storage

        public void ReadFoodTable (string directory, string file)
        {
            int position = 0;
            String line;
            if (!File.Exists(directory + "food table.txt"))
            {
                using (StreamReader sr = new StreamReader (directory + file))
                {
                    int number = 0;
                    while (!string.IsNullOrEmpty((line = sr.ReadLine())))
                    {
                        if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
                        {
                            position++;
                            number = 0;
                        }
                        else
                        {
                            switch (number)
                            {
                                case 0:
                                    FoodRelated.foodNameList.Add(position, line);
                                    break;
                                case 1:
                                    FoodRelated.servingSizeList.Add(position, float.Parse(line));
                                    break;
                                case 2:
                                    FoodRelated.caloriesPerServingList.Add(position, float.Parse(line));
                                    break;
                                case 3:
                                    FoodRelated.definersList.Add(position, line);
                                    break;
                            }
                            number++;
                        }
                    }
                    sr.Close();
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader (directory + "food table.txt"))
                {
                    int[] number = {
                        0,
                        0,
                        0,
                        0
                    };
                    while (!Equals((line = sr.ReadLine()), null))
                    {
                        if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
                        {
                            position++;
                        }
                        else
                        {
                            switch (position)
                            {
                                case 0:
                                    FoodRelated.foodNameList.Add(number [position], line);
                                    break;
                                case 1:
                                    FoodRelated.servingSizeList.Add(number [position], float.Parse(line));
                                    break;
                                case 2:
                                    FoodRelated.caloriesPerServingList.Add(number [position], float.Parse(line));
                                    break;
                                case 3:
                                    FoodRelated.definersList.Add(number [position], line);
                                    break;
                            }
                            number [position]++;
                        }
                    }
                    sr.Close();
                }
            }
        }

        public void WriteFoodTable (string directory, string file, string[] addString)
        {
            if (File.Exists(string.Format("{0}food.table", directory)))
            {
                File.Delete(string.Format("{0}food.table", directory));
            }
            string finalstring = null;
            const string seperator = "-------------------------------------------------------------------------\n";
            for (int i = 0; i < FoodRelated.foodNameList.Count; i++)
            {
                finalstring = finalstring + FoodRelated.foodNameList [i] + "\n";
                finalstring = finalstring + FoodRelated.servingSizeList [i] + "\n";
                finalstring = finalstring + FoodRelated.caloriesPerServingList [i] + "\n";
                finalstring = finalstring + FoodRelated.definersList [i] + "\n";
                finalstring = finalstring + seperator;
            }
            if (!string.IsNullOrWhiteSpace(addString [0]))
            {
                finalstring = string.Format("{0}{1}\n", finalstring, addString [0]);
                finalstring = string.Format("{0}{1}\n", finalstring, addString [1]);
                finalstring = string.Format("{0}{1}\n", finalstring, addString [2]);
                finalstring = string.Format("{0}{1}\n", finalstring, addString [3]);
                finalstring = string.Format("{0}{1}", finalstring, seperator);
            }
            File.WriteAllText(directory + file, finalstring);
        }

        public void ReadRegistry (string appendedRegistryValue, string registyValue)
        {
            using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
            {
                string temp = tempKey.GetValue("Calories Left for the Day").ToString();
                FoodRelated.Calories = float.Parse(temp);
                DateTime.TryParseExact(tempKey.GetValue("Last Used Date").ToString(), new [] {
                    "yyyy MMMMM dd hh:mm:ss tt"
                }, CultureInfo.InvariantCulture, DateTimeStyles.None, out GlobalVariables.dateReset);
            }
        }

        public void WriteRegistry (string appendedRegistryValue, string registyValue, bool reset)
        {
            if (!reset)
            {
                using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(
                                                 appendedRegistryValue + registyValue,
                                                 true
                                             ))
                {
                    tempKey.SetValue("Calories Left for the Day", FoodRelated.Calories.ToString());
                }
            }
            else
            {
                using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(
                                                 GlobalVariables.registryAppenedValue + GlobalVariables.registryMainValue,
                                                 true
                                             ))
                {
                    tempKey.SetValue(
                        "Calories Left for the Day",
                        FoodRelated.totalCaloriesPerDay.ToString(),
                        RegistryValueKind.String
                    );
                    tempKey.SetValue(
                        "Last Used Date",
                        GlobalVariables.nowDate.AddDays(1).ToString("yyyy MMMMM dd hh:mm:ss tt"),
                        RegistryValueKind.String
                    );
                }
            }
        }

        public bool CheckRegistryValues (string appendedRegistryValue, string registyValue)
        {
            if (!Equals(Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue), null))
            {
                return false;
            }
            Registry.LocalMachine.CreateSubKey(appendedRegistryValue + registyValue);
            using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
            {
                tempKey.SetValue(
                    "Calories Left for the Day",
                    FoodRelated.totalCaloriesPerDay.ToString(),
                    RegistryValueKind.String
                );
                tempKey.SetValue(
                    "Last Used Date",
                    GlobalVariables.nowDate.AddDays(1).ToString("yyyy MMMMM dd hh:mm:ss tt"),
                    RegistryValueKind.String
                );
            }
            return true;
        }
    }
}
