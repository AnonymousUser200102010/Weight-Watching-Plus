#region Using Directives

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using UniversalHandlersLibrary;

#endregion


namespace WeightWatchingProgramPlus
{

	/// <summary>
	/// Main Window and all tabs.
	/// </summary>
	/// 
    
	internal partial class MainForm : Form
	{
		private Storage Storage = new Storage ();

		private Functions Functions = new Functions ();

		private ErrorHandler ErrorHandler = new ErrorHandler ();

		internal MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			GlobalVariables.MainForm = this;
			Functions.InitializeForms(foodList, calorieRadioButton, timeRadioButton, caloriesLabel, Seperator1, Seperator2, howManyServingsLabel, searchBar, foodNameEditBox, definerEditBox, servingSizeEditBox, caloriesPerServingEditBox, manualCalorieEditBox);
			
			//
		}

		#region Main Forms

			#region Search Bar
	
				protected internal virtual void SearchBarTextChanged (object sender, EventArgs e)
				{
					if (!string.IsNullOrWhiteSpace(searchBar.Text))
					{
						nextSearchButton.Enabled = true;
						Functions.Find(0, searchBar.Text, null, exactSearchCheckBox.Checked, foodList);
						return;
					}
					else
					{
						nextSearchButton.Enabled = false;
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
		
				protected internal void ClearSearchBar (object sender, EventArgs e)
				{
					searchBar.Clear();
					clearSearchBarButton.Enabled = false;
					Font tempfont = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
					searchBar.Font = tempfont;
					searchBar.Text = "Click Here to Search the Food List";
					searchBar.TextAlign = HorizontalAlignment.Center;
					nextSearchButton.Enabled = false;
				}
				
				protected internal void FindNextSearchItem (object sender, EventArgs e)
				{
					if (string.IsNullOrWhiteSpace(searchBar.Text))
					{
						if (nextSearchButton.Enabled)
						{
							nextSearchButton.Enabled = false;
						}
						return;
					}
					if (GlobalVariables.SelectedListItem >= foodList.Items.Count - 1)
					{
						Functions.Find(0, searchBar.Text, null, exactSearchCheckBox.Checked, foodList);
					}
					else
					{
						Functions.Find(GlobalVariables.SelectedListItem, searchBar.Text, FoodRelated.FoodNameList [GlobalVariables.SelectedListItem], exactSearchCheckBox.Checked, foodList);
					}
				}
	
			#endregion
	
			#region Food Listbox
	
				protected internal virtual void FoodListSelectedIndexChanged (object sender, EventArgs e)
				{
					if (foodList.SelectedIndex > -1)
					{
						GlobalVariables.SelectedListItem = foodList.SelectedIndex;
						howManyServingsLabel.Text = string.Format(CultureInfo.InvariantCulture, "How many {0}s do you plan on eating?", FoodRelated.DefinersList [foodList.SelectedIndex]);
						foodNameEditBox.Text = FoodRelated.FoodNameList [foodList.SelectedIndex];
						foodNameEditBox.TextAlign = HorizontalAlignment.Center;
						servingSizeEditBox.Text = FoodRelated.ServingSizeList [foodList.SelectedIndex].ToString(CultureInfo.CurrentCulture);
						servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
						definerEditBox.Text = FoodRelated.DefinersList [foodList.SelectedIndex];
						definerEditBox.TextAlign = HorizontalAlignment.Center;
						caloriesPerServingEditBox.Text = FoodRelated.CaloriesPerServingList [foodList.SelectedIndex].ToString(CultureInfo.CurrentCulture);
						caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
					}
				}
		
				protected internal void FoodListFocusChanged (object sender, EventArgs e)
				{
					if (foodList.Focused)
					{
						GlobalVariables.SelectedListItem = foodList.SelectedIndex;
						if (!deleteSelectedFoodItemButton.Enabled && foodList.Items.Count > 0)
						{
							deleteSelectedFoodItemButton.Enabled = true;
						}
					}
					else
					{
						if (deleteSelectedFoodItemButton.Enabled && foodList.Items.Count <= 0)
						{
							deleteSelectedFoodItemButton.Enabled = false;
						}
						foodList.ClearSelected();
					}
				}
		
				protected internal void DeleteFoodItemFromTable (object sender, EventArgs e)
				{
					Functions.DumpFoodPropertiesList(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
					FoodRelated.FoodNameList.Remove(GlobalVariables.SelectedListItem);
					Storage.WriteFoodTable("Files\\Text\\", "food.table", new string[] {
						null,
						null,
						null,
						null
					});
					Functions.Refresh_foodList(foodList);
				}
	
			#endregion
			
			#region Calorie List & Related
	
				protected internal void CalorieRadioButtonCheckedChanged (object sender, EventArgs e)
				{
					Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
				}
		
				protected internal void TimeRadioButtonCheckedChanged (object sender, EventArgs e)
				{
					Validation.CheckDateValidity(GlobalVariables.NowDate, GlobalVariables.DateReset, Storage.CheckRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue), Storage);
					Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
				}
				
				protected internal void RefreshCaloriesTimeButtonClick (object sender, EventArgs e)
				{
					Validation.CheckDateValidity(GlobalVariables.NowDate, GlobalVariables.DateReset, Storage.CheckRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue), Storage);
					Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
				}
			#endregion
			
			protected internal void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
			{
				string oldtext = foodNameEditBox.Text;
				Functions.FoodPropertiesSwitch(foodList, foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, newItemCheckbox);
				Functions.Find(0, oldtext, null, true, foodList);
			}

		#endregion
		
		#region Edit Your Calories Manual Forms
		
			protected internal void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
			{
				Functions.DumpFoodPropertiesList(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
				foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
			}
	
			protected internal void ResetCaloriesButtonClicked (object sender, EventArgs e)
			{
				manualCalorieEditBox.Value = (decimal)FoodRelated.TotalCaloriesPerDay;
			}
	
			protected internal void ZeroOutCaloriesButtonClicked (object sender, EventArgs e)
			{
				manualCalorieEditBox.Value = 0;
			}
	
			protected internal void ManualSubmitButtonClicked (object sender, EventArgs e)
			{
				FoodRelated.Calories = (float)manualCalorieEditBox.Value;
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, false);
				Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
			}
		
		#endregion

		protected internal void ModifyCalories (object sender, EventArgs e)
		{
			bool add = true;
			int errorNum = 1;
			string warningText = "The amount of calories that you are trying to add would put you over your daily limit, and is not allowed.";
			if (sender.ToString().Contains("subtract", StringComparison.InvariantCultureIgnoreCase))
			{
				add = false;
				errorNum = 2;
				warningText = "The amount of calories that are about to be subtracted would put you below your daily limit! Continue?";
			} 
			float tempcalories = 0f;
			if (add)
			{
				tempcalories = FoodRelated.Calories + Functions.ModifyCalories(userServingInputTextBox, add);
			}
			else
			{
				tempcalories = FoodRelated.Calories - Functions.ModifyCalories(userServingInputTextBox, add);
			}
			bool safeToModify = false;
			if (tempcalories < 0f && !add || tempcalories > FoodRelated.TotalCaloriesPerDay && add)
			{
				DialogResult dialogResult = ErrorHandler.ErrorMessageBox(warningText, null, errorNum, false);
				if (!add && dialogResult == DialogResult.Yes)
				{
					safeToModify = true;
				}
			}
			else
			{
				safeToModify = true;
			}
			if (safeToModify)
			{
				tempcalories = Functions.ModifyCalories(userServingInputTextBox, add);
				if (add)
				{
					FoodRelated.Calories += tempcalories;
				}
				else
				{
					FoodRelated.Calories -= tempcalories;
				}
				Storage.WriteFoodEaten("Files\\Text\\", "Food Diary.txt", WriteToFileCheckBox, RecordFoodCheckBox, userServingInputTextBox, add);
				if (FoodRelated.Calories >= 0)
				{
					manualCalorieEditBox.Value = (decimal)FoodRelated.Calories;
				}
			}
			Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, false);
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, Functions);
		}
	}

	internal static class Validation
	{
		//Functions whose primary purpose is verification and validation, but who don't have a more pressing function.
		
		internal static void CheckDateValidity (DateTime dateToCompareTo, DateTime dateToCheck, bool firstProgramUse, Storage Storage)
		{
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			if (!Equals(DateTime.Compare(dateToCompareTo, dateToCheck), -1) || firstProgramUse)
			{
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, true);
			}
		}

		internal static void CheckCurrentRadioButton (RadioButton timeRadioButton, RadioButton calorieRadioButton, Label caloriesLabel, Functions Functions)
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
	}

	internal class Functions
	{
		//		General functions
        
		Storage Storage = new Storage ();

		ErrorHandler ErrorHandler = new ErrorHandler ();

		public void WriteToObject (Label labelToChange, int objectNumber)
		{
			if (Equals(labelToChange, null))
			{
				Errors.Handler(new ArgumentNullException ("labelToChange", "WriteToObject: labelToChange"), true);
			}
			string[] messages = {
				string.Format(CultureInfo.InvariantCulture, "Calories Left For The Day: {0}", FoodRelated.Calories),
				string.Format(CultureInfo.InvariantCulture, "Calories will reset on {0:MMMM dd} at {1:hh:mm tt}", GlobalVariables.DateReset, GlobalVariables.DateReset)
					
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

		internal void InitializeForms (ListBox foodList, RadioButton calorieRadioButton, RadioButton timeRadioButton, Label caloriesLabel, Label Seperator1, Label Seperator2, Label howManyServingsLabel, TextBox searchBar, TextBox foodNameEditBox, TextBox definerEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, NumericUpDown manualCalorieEditBox)
		{
			const BorderStyle fixed3D = BorderStyle.Fixed3D;
			const HorizontalAlignment center = HorizontalAlignment.Center;
			const bool b = true;
			GlobalVariables.MainForm.Text = GlobalVariables.RegistryMainValue;
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
			Validation.CheckDateValidity(GlobalVariables.NowDate, GlobalVariables.DateReset, this.Storage.CheckRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue), this.Storage);
			foodList.SetSelected(0, b);
			GlobalVariables.SelectedListItem = 0;
			howManyServingsLabel.Text = string.Format(CultureInfo.InvariantCulture, "How many {0}s do you plan on eating?", FoodRelated.DefinersList [GlobalVariables.SelectedListItem]);
			foodNameEditBox.Text = FoodRelated.FoodNameList [0];
			foodNameEditBox.TextAlign = center;
			servingSizeEditBox.Text = FoodRelated.ServingSizeList [0].ToString(CultureInfo.InvariantCulture);
			servingSizeEditBox.TextAlign = center;
			definerEditBox.Text = FoodRelated.DefinersList [0];
			definerEditBox.TextAlign = center;
			caloriesPerServingEditBox.Text = FoodRelated.CaloriesPerServingList [0].ToString(CultureInfo.InvariantCulture);
			caloriesPerServingEditBox.TextAlign = center;
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel, this);
			if (FoodRelated.Calories >= 0)
			{
				manualCalorieEditBox.Value = decimal.Parse(FoodRelated.Calories.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
			}
			else
			{
				manualCalorieEditBox.Value = 0;
			}
		}

		internal void DumpFoodPropertiesList (TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox)
		{
			foodNameEditBox.Clear();
			servingSizeEditBox.Value = 0;
			caloriesPerServingEditBox.Value = 0;
			definerEditBox.Clear();
		}

		internal void Refresh_foodList (ListBox foodList)
		{
			FoodRelated.FoodNameList.Clear();
			FoodRelated.ServingSizeList.Clear();
			FoodRelated.CaloriesPerServingList.Clear();
			FoodRelated.DefinersList.Clear();
			this.Storage.ReadFoodTable("Files\\Text\\", "food.table");
			foodList.DataSource = null;
			foodList.Items.Clear();
			foodList.DataSource = FoodRelated.FoodNameList.Values.ToList();
			this.Storage.WriteFoodTable("Files\\Text\\", "food.table", new string[] { 
				null,
				null,
				null,
				null
			});
		}

		internal  void FoodPropertiesSwitch (ListBox foodList, TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox)
		{
			if (string.IsNullOrWhiteSpace(foodNameEditBox.Text))
			{
				this.ErrorHandler.ErrorMessageBox("Please set a food name value!", foodNameEditBox, 0, true);
				return;
			}
			if (servingSizeEditBox.Value <= 0)
			{
				this.ErrorHandler.ErrorMessageBox("Please set a serving size value!", servingSizeEditBox, 0, true);
				return;
			}
			if (caloriesPerServingEditBox.Value <= 0)
			{
				this.ErrorHandler.ErrorMessageBox("Please set a calories per serving value!", caloriesPerServingEditBox, 0, true);
				return;
			}
			if (string.IsNullOrWhiteSpace(definerEditBox.Text))
			{
				this.ErrorHandler.ErrorMessageBox("Please set a definer value!", definerEditBox, 0, true);
				return;
			}
			if (!newItemCheckbox.Checked)
			{
				FoodRelated.FoodNameList [GlobalVariables.SelectedListItem] = foodNameEditBox.Text;
				FoodRelated.ServingSizeList [GlobalVariables.SelectedListItem] = float.Parse(servingSizeEditBox.Text, CultureInfo.CurrentCulture);
				FoodRelated.CaloriesPerServingList [GlobalVariables.SelectedListItem] = float.Parse(caloriesPerServingEditBox.Text, CultureInfo.CurrentCulture);
				FoodRelated.DefinersList [GlobalVariables.SelectedListItem] = definerEditBox.Text;
				this.Storage.WriteFoodTable("Files\\Text\\", "food.table", new string[] {
					null,
					null,
					null,
					null
				});
			}
			else
			{
				this.Storage.WriteFoodTable("Files\\Text\\", "food.table", new [] {
					foodNameEditBox.Text,
					servingSizeEditBox.Text,
					caloriesPerServingEditBox.Text,
					definerEditBox.Text
				});
				newItemCheckbox.Checked = false;
			}
			Refresh_foodList(foodList);
		}

		internal float ModifyCalories (NumericUpDown userServingInputTextBox, bool add)
		{
			int hour = DateTime.Now.Hour;
			string amPMDefiner = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
			this.Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			float tempFloat = FoodRelated.CaloriesPerServingList [GlobalVariables.SelectedListItem] * float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.ServingSizeList [GlobalVariables.SelectedListItem];
			if (hour > 12 && hour < 4 && amPMDefiner.Equals("am", StringComparison.CurrentCultureIgnoreCase))
			{
				float midSnackPenalty = tempFloat / 10;
				if (midSnackPenalty <= 10)
				{
					midSnackPenalty = 10;
				}
				string appliedSwitch = !add ? "applied" : "subtracted";
				DialogResult dialogResult = MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "A midnight snacking penalty of {0} will be {1} if you continue.", midSnackPenalty, appliedSwitch), "Midnight Snacking Penalty.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
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

		internal void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, ListBox foodList)
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

	internal class Storage
	{
		
		//		Functions that relate to storage
		
		internal void ReadFoodTable (string directory, string file)
		{
			int position = 0;
			String line;
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
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
									FoodRelated.FoodNameList.Add(position, line);
									break;
								case 1:
									FoodRelated.ServingSizeList.Add(position, float.Parse(line, CultureInfo.CurrentCulture));
									break;
								case 2:
									FoodRelated.CaloriesPerServingList.Add(position, float.Parse(line, CultureInfo.CurrentCulture));
									break;
								case 3:
									FoodRelated.DefinersList.Add(position, line);
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
				Errors.Handler(new IOException (string.Format("{0}{1} does not exist", directory, file)), true);
			}
		}

		internal void WriteFoodTable (string directory, string file, string[] addString)
		{
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				File.Delete(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file));
			}
			else
			{
				Errors.Handler(new IOException (string.Format("{0}{1} does not exist", directory, file)), true);
			}
			string finalstring = null;
			const string seperator = "-------------------------------------------------------------------------\n";
			for (int i = 0; i < FoodRelated.FoodNameList.Count; i++)
			{
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}", FoodRelated.FoodNameList [i], FoodRelated.ServingSizeList [i], FoodRelated.CaloriesPerServingList [i], FoodRelated.DefinersList [i], seperator);
			}
			if (!string.IsNullOrWhiteSpace(addString [0]) && !string.IsNullOrWhiteSpace(addString [1]) && !string.IsNullOrWhiteSpace(addString [2]) && !string.IsNullOrWhiteSpace(addString [3]))
			{
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}", addString [0], addString [1], addString [2], addString [3], seperator);
			}
			File.WriteAllText(directory + file, finalstring);
		}

		internal void ReadRegistry (string appendedRegistryValue, string registyValue)
		{
			DateTime tempdate = new DateTime ();
			if (Equals(Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue), null))
			{
				Registry.LocalMachine.CreateSubKey(appendedRegistryValue + registyValue);
			}
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				if (appendedRegistryValue.Equals(GlobalVariables.RegistryAppendedValue))
				{
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Calories Left for the Day")))
					{
						tempKey.SetValue("Calories Left for the Day", FoodRelated.TotalCaloriesPerDay);
					}
					FoodRelated.Calories = float.Parse(tempKey.GetValue("Calories Left for the Day").ToString(), CultureInfo.InvariantCulture);
					if (string.IsNullOrWhiteSpace((string)tempKey.GetValue("Last Used Date")))
					{
						tempKey.SetValue("Last Used Date", GlobalVariables.NowDate);
					}
					DateTime.TryParseExact(tempKey.GetValue("Last Used Date").ToString(), new [] {
							"yyyy MMMMM dd hh:mm:ss tt"
						}, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempdate);
					GlobalVariables.DateReset = tempdate;
				}
				else if (registyValue.Contains("Diary", StringComparison.InvariantCultureIgnoreCase))
				{
            		
				}
			}
		}

		internal void WriteRegistry (string appendedRegistryValue, string registyValue, bool reset)
		{
			float caloriesToUpdate = FoodRelated.Calories;
			if (reset)
			{
				caloriesToUpdate = FoodRelated.TotalCaloriesPerDay;
			}
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				tempKey.SetValue("Calories Left for the Day", caloriesToUpdate, RegistryValueKind.String);
				if(reset)
				{
					tempKey.SetValue("Last Used Date", GlobalVariables.NowDate.AddDays(1).ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture), RegistryValueKind.String);	
				}
			}
		}

		internal bool CheckRegistryValues (string appendedRegistryValue, string registyValue)
		{
			if (!Equals(Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue), null))
			{
				return false;
			}
			Registry.LocalMachine.CreateSubKey(appendedRegistryValue + registyValue);
			using (RegistryKey tempKey = Registry.LocalMachine.OpenSubKey(appendedRegistryValue + registyValue, true))
			{
				tempKey.SetValue("Calories Left for the Day", FoodRelated.TotalCaloriesPerDay.ToString(CultureInfo.CurrentCulture), RegistryValueKind.String);
				tempKey.SetValue("Last Used Date", GlobalVariables.NowDate.AddDays(1).ToString("yyyy MMMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture), RegistryValueKind.String);
			}
			return true;
		}

		internal void WriteFoodEaten (string directory, string file, CheckBox WriteToFile, CheckBox record, NumericUpDown userServingInputTextBox, bool add)
		{
        	
			string finalstring = null;
			const string seperator = "-";
            
			if (record.Checked)
			{
				if (WriteToFile.Checked)
				{
					DateTime Now = DateTime.Now;
	        		
					float tempserval = float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.ServingSizeList [GlobalVariables.SelectedListItem];
	        		
					float temptolval = FoodRelated.ServingSizeList [GlobalVariables.SelectedListItem] * tempserval;
	        		
					if (Math.Floor(temptolval) <= 0)
					{
						temptolval = (float)Math.Round(temptolval, 1);
					}
					else
					{
						temptolval = (float)Math.Floor(temptolval);
					}
	        		
					finalstring += "At " + Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture) + " (on: " + Now.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
					
					if (add)
					{
						finalstring += ") You added back to your calorie count: ";
					}
					else
					{
						finalstring += ") You ate: ";
					}
	        		
					finalstring += userServingInputTextBox.Value + " " + FoodRelated.DefinersList [GlobalVariables.SelectedListItem];
	        		
					if (userServingInputTextBox.Value > 1)
					{
						finalstring += "s";
					}
	        		
					float tempcalval = FoodRelated.CaloriesPerServingList [GlobalVariables.SelectedListItem] * float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.ServingSizeList [GlobalVariables.SelectedListItem];
	        		
					if(FoodRelated.Calories >= 0)
					{
						finalstring += string.Format(" (of: '{0}').\nWhich is {1} servings, or {2} calories of '{0}'. You had {3} calories left for the day.\n{4}\n", FoodRelated.FoodNameList[GlobalVariables.SelectedListItem], tempserval, Math.Round(tempcalval, 4, MidpointRounding.AwayFromZero), FoodRelated.Calories, seperator);
					}
					else
					{
						finalstring += string.Format(" (of: '{0}').\nWhich is {1} servings, or {2} calories of '{0}'. You had -{3} calories left for the day.\n{4}\n", FoodRelated.FoodNameList[GlobalVariables.SelectedListItem], tempserval, Math.Round(tempcalval, 4, MidpointRounding.AwayFromZero), Math.Abs(FoodRelated.Calories), seperator);;
					}
					
					if (File.Exists(directory + file))
					{
						using (StreamReader sr = new StreamReader (directory + file))
						{
							string line;
							while (!string.IsNullOrEmpty((line = sr.ReadLine())))
							{
								if (line.Contains(seperator, StringComparison.CurrentCultureIgnoreCase))
								{
									finalstring += seperator + "\n";
								}
								else
								{
									finalstring += line + "\n";
								}
							}
							sr.Close();
						}
						File.Delete(directory + file);
					}
					File.WriteAllText(directory + file, finalstring);
				}
	        	
				//ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue + "\\Diary");
	        	
				userServingInputTextBox.Value = 1;
			}
		}
	}
}
