#region Using Directives

using System;
using System.Collections.Generic;
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
		private Modification Modification = new Modification();
		private PopupHandler PopupHandler = new PopupHandler ();

		internal MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			GlobalVariables.MainForm = this;
			Functions.InitializeForms(foodList, calorieRadioButton, timeRadioButton, caloriesLabel, Seperator1, Seperator2, seperator3, Seperator4, Seperator5, howManyServingsLabel,  foodNameEditBox, definerEditBox, servingSizeEditBox, caloriesPerServingEditBox, manualCalorieEditBox, AmPmComboBox);
			
			//
		}

		#region Main Forms

			#region Search Bar
	
				protected internal virtual void SearchBarTextChanged (object sender, EventArgs e)
				{
					if (!string.IsNullOrWhiteSpace(searchBar.Text))
					{
						if (!nextSearchButton.Enabled)
						{
							nextSearchButton.Enabled = true;
						}
						Functions.Find(0, searchBar.Text, null, exactSearchCheckBox.Checked, foodList);
						return;
					}
					if (nextSearchButton.Enabled)
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
					if(!nextSearchButton.Focused)
					{
						searchBar.Clear();
						clearSearchBarButton.Enabled = false;
						Font tempfont = new Font ("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
						searchBar.Font = tempfont;
						searchBar.Text = "Click Here to Search the Food List";
						searchBar.TextAlign = HorizontalAlignment.Center;
						nextSearchButton.Enabled = false;
					}
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
						Functions.Find(GlobalVariables.SelectedListItem, searchBar.Text, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, exactSearchCheckBox.Checked, foodList);
					}
				}
	
			#endregion
	
			#region Food Listbox
	
				protected internal virtual void FoodListSelectedIndexChanged (object sender, EventArgs e)
				{
					if (foodList.SelectedIndex > -1)
					{
						if(newItemCheckbox.Checked)
						{
							newItemCheckbox.Checked = false;
						}
						GlobalVariables.SelectedListItem = foodList.SelectedIndex;
						howManyServingsLabel.Text = string.Format(CultureInfo.InvariantCulture, "How many {0}s do you plan on eating?", FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item4);
						foodNameEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item1;
						foodNameEditBox.TextAlign = HorizontalAlignment.Center;
						servingSizeEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item2.ToString(CultureInfo.CurrentCulture);
						servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
						definerEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item4;
						definerEditBox.TextAlign = HorizontalAlignment.Center;
						caloriesPerServingEditBox.Text = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item3.ToString(CultureInfo.CurrentCulture);
						caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
					}
				}
		
				protected internal void FoodListFocusChanged (object sender, EventArgs e)
				{
					if(newItemCheckbox.Checked)
					{
						newItemCheckbox.Checked = false;
					}
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
					Modification.DumpFoodPropertiesList(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
					FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
					Storage.WriteFoodTable("Files\\Text\\", "food.table", new string[] {
						null,
						null,
						null,
						null
					});
					Functions.Refresh_foodList(foodList);
					foodList.SetSelected(GlobalVariables.SelectedListItem, true);
				}
	
			#endregion
			
			protected internal void TimeOrCaloriesChangedWithoutEvent (object sender, EventArgs e)
			{
				if(refreshCaloriesTimeButton.Focused)
				{
					int i = 0;
					while(i <= 3)
					{
						Validation.CheckDateValidity(GlobalVariables.NowDate, GlobalVariables.DateReset, Storage.CheckRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue));
						i++;
					}
				}
				else
				{
					Validation.CheckDateValidity(GlobalVariables.NowDate, GlobalVariables.DateReset, Storage.CheckRegistryValues(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue));
				}
				Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
			}
			
			protected internal void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
			{
				string oldtext = foodNameEditBox.Text;
				Modification.FoodPropertiesSwitch(foodList, foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, newItemCheckbox);
				Functions.Find(0, oldtext, null, true, foodList);
			}
			
			protected internal void ModifyCalories (object sender, EventArgs e)
		{
			bool add = true;
			int errorNum = 1;
			string warningText = "The amount of calories that you are trying to add would put you over your daily limit, and is not allowed.";
			if (sender.ToString().Contains("subtract", StringComparison.OrdinalIgnoreCase))
			{
				add = false;
				errorNum = 2;
				warningText = "The amount of calories that are about to be subtracted would put you below your daily limit! Continue?";
			} 
			float tempcalories = FoodRelated.Calories;
			if (add)
			{
				tempcalories += Modification.ModifyCalories(userServingInputTextBox, add);
			}
			else
			{
				tempcalories -= Modification.ModifyCalories(userServingInputTextBox, add);
			}
			bool safeToModify = false;
			if (tempcalories < 0f && !add || tempcalories > FoodRelated.TotalCaloriesPerDay && add)
			{
				DialogResult dialogResult = PopupHandler.ErrorMessageBox(warningText, null, errorNum, false);
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
				tempcalories = Modification.ModifyCalories(userServingInputTextBox, add);
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
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
		}

		#endregion
		
		#region Edit Your Calories Manual Forms
		
			protected internal void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
			{
				Modification.DumpFoodPropertiesList(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox);
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
				Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
			}
		
		#endregion
	}

	static class Validation
	{
		//Functions whose primary purpose is verification and validation, but who don't have a more pressing primary function.
		
		internal static void CheckDateValidity (DateTime dateToCompareTo, DateTime dateToCheck, bool firstProgramUse)
		{
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			if (!Equals(DateTime.Compare(dateToCompareTo, dateToCheck), -1) || firstProgramUse)
			{
				Storage.WriteRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue, true);
			}
		}

		internal static void CheckCurrentRadioButton (RadioButton timeRadioButton, RadioButton calorieRadioButton, Label caloriesLabel)
		{
			if (timeRadioButton.Checked)
			{
				Modification.WriteToObject(caloriesLabel, 1);
			}
			else if (calorieRadioButton.Checked)
			{
				Modification.WriteToObject(caloriesLabel, 0);
			}
		}
		
		internal static bool CheckEditBoxesHaveValidEntries(TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, PopupHandler PopupHandler)
		{
			if(AlreadyExists(foodNameEditBox.Text))
			{
				PopupHandler.ErrorMessageBox("Your food name cannot be the exact same as another food item!", foodNameEditBox, 4, true);
				return false;
			}
			else if (string.IsNullOrWhiteSpace(foodNameEditBox.Text))
			{
				PopupHandler.ErrorMessageBox("Please set a food name value!", foodNameEditBox, 0, true);
				return false;
			} 
			else if (servingSizeEditBox.Value <= 0)
			{
				PopupHandler.ErrorMessageBox("Please set a serving size value!", servingSizeEditBox, 0, true);
				return false;
			}
			else if (caloriesPerServingEditBox.Value <= 0)
			{
				PopupHandler.ErrorMessageBox("Please set a calories per serving value!", caloriesPerServingEditBox, 0, true);
				return false;
			}
			else if (string.IsNullOrWhiteSpace(definerEditBox.Text))
			{
				PopupHandler.ErrorMessageBox("Please set a definer value!", definerEditBox, 0, true);
				return false;
			}
			
			return true;
		}
		
		private static bool AlreadyExists(string text)
		{
			foreach(Tuple<string, float, float, string> t in FoodRelated.CombinedFoodList)
			{
				if(text.Equals(t.Item1, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}
	
	internal class Modification
	{
		//Functions whose primary purpose is modification and creation, but who don't have a more pressing primary function.
		
		PopupHandler PopupHandler = new PopupHandler ();
		
		internal float ModifyCalories (NumericUpDown userServingInputTextBox, bool add)
		{
			int hour = DateTime.Now.Hour;
			string amPMDefiner = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
			Storage.ReadRegistry(GlobalVariables.RegistryAppendedValue, GlobalVariables.RegistryMainValue);
			float tempFloat = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item3 * float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
			if (hour > 12 && hour < 4 && amPMDefiner.Equals("am", StringComparison.CurrentCultureIgnoreCase))
			{
				float midSnackPenalty = tempFloat / 10;
				if (midSnackPenalty <= 10)
				{
					midSnackPenalty = 10;
				}
				string appliedSwitch = !add ? "applied" : "subtracted";
				if (this.PopupHandler.ErrorMessageBox(string.Format(CultureInfo.InvariantCulture, "A midnight snacking penalty of {0} will be {1} if you continue.", midSnackPenalty, appliedSwitch), null, 3, false) != DialogResult.OK)
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
		
		public static void WriteToObject (Label labelToChange, int objectNumber)
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
		
		internal void FoodPropertiesSwitch (ListBox foodList, TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox, CheckBox newItemCheckbox)
		{
			if(!Validation.CheckEditBoxesHaveValidEntries(foodNameEditBox, servingSizeEditBox, caloriesPerServingEditBox, definerEditBox, this.PopupHandler))
			{
				return;
			}
			if (!newItemCheckbox.Checked)
			{
				FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
				FoodRelated.CombinedFoodList.Insert(GlobalVariables.SelectedListItem, new Tuple<string, float, float, string>(foodNameEditBox.Text, float.Parse(servingSizeEditBox.Text, CultureInfo.CurrentCulture), float.Parse(caloriesPerServingEditBox.Text, CultureInfo.CurrentCulture), definerEditBox.Text));
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new string[] {
					null,
					null,
					null,
					null
				});
			}
			else
			{
				Storage.WriteFoodTable("Files\\Text\\", "food.table", new [] {
					foodNameEditBox.Text,
					servingSizeEditBox.Text,
					caloriesPerServingEditBox.Text,
					definerEditBox.Text
				});
				newItemCheckbox.Checked = false;
			}
			Functions.Refresh_foodList(foodList);
		}
		
		internal static void DumpFoodPropertiesList (TextBox foodNameEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, TextBox definerEditBox)
		{
			foodNameEditBox.Clear();
			servingSizeEditBox.Value = 0;
			caloriesPerServingEditBox.Value = 0;
			definerEditBox.Clear();
		}
		
		internal static void ModifySeperator(Label Seperator, bool vertical)
		{
			const BorderStyle fixed3D = BorderStyle.Fixed3D;
			if(vertical)
			{
				Seperator.AutoSize = false;
				Seperator.BorderStyle = fixed3D;
				Seperator.Width = 1;
			}
			else
			{
				Seperator.AutoSize = false;
				Seperator.BorderStyle = fixed3D;
				Seperator.Height = 2;
			}
		}
	}

	static class Storage
	{
		
		//		Functions that relate to storage
		
		internal static void ReadFoodTable (string directory, string file)
		{
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				using (StreamReader sr = new StreamReader (directory + file))
				{
					int number = 0;
					int position = 0;
					String line;
					string[] combined = new string[4];
					while (!string.IsNullOrEmpty((line = sr.ReadLine())))
					{
						if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
						{
							FoodRelated.CombinedFoodList.Add(new Tuple<string, float, float, string>(combined[0], float.Parse(combined[1], CultureInfo.InvariantCulture), float.Parse(combined[2], CultureInfo.InvariantCulture), combined[3]));
							position++;
							number = 0;
						}
						else
						{
							combined[number] = line;
							number++;
						}
					}
					sr.Close();
				}
				List<Tuple<string, float, float, string>> sortedEnum = FoodRelated.CombinedFoodList.ToList();
				sortedEnum.Sort();
				FoodRelated.CombinedFoodList = sortedEnum;
			}
			else
			{
				Errors.Handler(new IOException (string.Format(CultureInfo.InvariantCulture, "{0}{1} does not exist", directory, file)), true);
			}
		}

		internal static void WriteFoodTable (string directory, string file, string[] addString)
		{
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file)))
			{
				File.Delete(string.Format(CultureInfo.InvariantCulture, "{0}{1}", directory, file));
			}
			else
			{
				Errors.Handler(new IOException (string.Format(CultureInfo.CurrentCulture, "{0}{1} does not exist", directory, file)), true);
			}
			string finalstring = null;
			const string seperator = "-------------------------------------------------------------------------\n";
			for (int i = 0; i < FoodRelated.CombinedFoodList.Count; i++)
			{
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}", FoodRelated.CombinedFoodList [i].Item1, FoodRelated.CombinedFoodList [i].Item2, FoodRelated.CombinedFoodList [i].Item3, FoodRelated.CombinedFoodList [i].Item4, seperator);
			}
			if (!string.IsNullOrWhiteSpace(addString [0]) && !string.IsNullOrWhiteSpace(addString [1]) && !string.IsNullOrWhiteSpace(addString [2]) && !string.IsNullOrWhiteSpace(addString [3]))
			{
				finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}", addString [0], addString [1], addString [2], addString [3], seperator);
			}
			File.WriteAllText(directory + file, finalstring);
		}

		internal static void ReadRegistry (string appendedRegistryValue, string registyValue)
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
					if(!DateTime.TryParseExact(tempKey.GetValue("Last Used Date").ToString(), new [] {
							"yyyy MMMMM dd hh:mm:ss tt"
					                          }, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempdate))
					{
						Errors.Handler(new FormatException("Last Used Date: Registry: Arguement failed to produce a valid result."), true);
					}
					GlobalVariables.DateReset = tempdate;
				}
				else if (registyValue.Contains("Diary", StringComparison.InvariantCultureIgnoreCase))
				{
            		//Do nothing...yet
				}
			}
		}

		internal static void WriteRegistry (string appendedRegistryValue, string registyValue, bool reset)
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

		internal static bool CheckRegistryValues (string appendedRegistryValue, string registyValue)
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

		internal static void WriteFoodEaten (string directory, string file, CheckBox WriteToFile, CheckBox record, NumericUpDown userServingInputTextBox, bool add)
		{
        	
			string finalstring = null;
			const string seperator = "-";
            
			if (record.Checked)
			{
				if (WriteToFile.Checked)
				{
					DateTime Now = DateTime.Now;
	        		
					float tempserval = float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
	        		
					float temptolval = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2 * tempserval;
	        		
					if (Math.Floor(temptolval) <= 0)
					{
						temptolval = (float)Math.Round(temptolval, 1);
					}
					else
					{
						temptolval = (float)Math.Floor(temptolval);
					}
	        		
					finalstring += string.Format(CultureInfo.CurrentCulture, "At {0} (on: {1}", Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture), Now.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture));
					
					if (add)
					{
						finalstring += ") You added back to your calorie count: ";
					}
					else
					{
						finalstring += ") You ate: ";
					}
	        		
					finalstring += string.Format(CultureInfo.CurrentCulture, "{0} {1}", userServingInputTextBox.Value, FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item4);
	        		
					if (userServingInputTextBox.Value > 1)
					{
						finalstring += "s";
					}
	        		
					float tempcalval = FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item3 * float.Parse(userServingInputTextBox.Text, CultureInfo.CurrentCulture) / FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item2;
					
					finalstring += string.Format(CultureInfo.CurrentCulture, " (of: '{0}').\nWhich is {1} servings, or {2} calories of '{0}'. ", FoodRelated.CombinedFoodList[GlobalVariables.SelectedListItem].Item1, tempserval, Math.Round(tempcalval, 4, MidpointRounding.AwayFromZero));
	        		
					if(FoodRelated.Calories >= 0)
					{
						finalstring += string.Format(CultureInfo.CurrentCulture, "You had {0} calories left for the day.\n{1}\n", FoodRelated.Calories, seperator);
					}
					else
					{
						finalstring += string.Format(CultureInfo.CurrentCulture, "You had {0} calories left for the day.\n{1}\n", Math.Abs(FoodRelated.Calories), seperator);
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
									finalstring += string.Format(CultureInfo.CurrentCulture, "{0}\n", seperator);
								}
								else
								{
									finalstring += string.Format(CultureInfo.CurrentCulture, "{0}\n", line);
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
	
	static class Functions
	{
		//		General functions
		
		internal static void InitializeForms (ListBox foodList, RadioButton calorieRadioButton, RadioButton timeRadioButton, Label caloriesLabel, Label Seperator1, Label Seperator2, Label Seperator3, Label Seperator4, Label Seperator5, Label howManyServingsLabel, TextBox foodNameEditBox, TextBox definerEditBox, NumericUpDown servingSizeEditBox, NumericUpDown caloriesPerServingEditBox, NumericUpDown manualCalorieEditBox, ComboBox AmPmComboBox)
		{
			const HorizontalAlignment center = HorizontalAlignment.Center;
			
			GlobalVariables.MainForm.Text = GlobalVariables.RegistryMainValue;
			
			Modification.ModifySeperator(Seperator1, true);
			Modification.ModifySeperator(Seperator2, false);
			Modification.ModifySeperator(Seperator3, false);
			Modification.ModifySeperator(Seperator4, true);
			Modification.ModifySeperator(Seperator5, false);
			
			AmPmComboBox.SelectedItem = 1;
			
			Refresh_foodList(foodList);
			
			foodList.SetSelected(foodList.TopIndex, true);
			
			GlobalVariables.SelectedListItem = 0;
			
			howManyServingsLabel.Text = string.Format(CultureInfo.InvariantCulture, "How many {0}s do you plan on eating?", FoodRelated.CombinedFoodList 
			                                          [GlobalVariables.SelectedListItem].Item4);
			
			foodNameEditBox.Text = foodList.Items[foodList.TopIndex].ToString();
			foodNameEditBox.TextAlign = center;
			
			servingSizeEditBox.Text = FoodRelated.CombinedFoodList [0]. Item2.ToString(CultureInfo.InvariantCulture);
			servingSizeEditBox.TextAlign = center;
			
			definerEditBox.Text = FoodRelated.CombinedFoodList [0].Item4;
			definerEditBox.TextAlign = center;
			
			caloriesPerServingEditBox.Text = FoodRelated.CombinedFoodList[0].Item3.ToString(CultureInfo.InvariantCulture);
			caloriesPerServingEditBox.TextAlign = center;
			
			Validation.CheckCurrentRadioButton(timeRadioButton, calorieRadioButton, caloriesLabel);
			
			if (FoodRelated.Calories >= 0)
			{
				manualCalorieEditBox.Value = decimal.Parse(FoodRelated.Calories.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
			}
			else
			{
				manualCalorieEditBox.Value = 0;
			}
		}

		internal static void Refresh_foodList (ListBox foodList)
		{
			FoodRelated.CombinedFoodList.Clear();
			Storage.ReadFoodTable("Files\\Text\\", "food.table");
			foodList.DataSource = null;
			foodList.Items.Clear();
			foreach(Tuple<string, float, float, string> name in FoodRelated.CombinedFoodList)
			{
				foodList.Items.Add(name.Item1);
			}
			Storage.WriteFoodTable("Files\\Text\\", "food.table", new string[] { 
				null,
				null,
				null,
				null
			});
		}

		internal static void Find (int offset, string stringToFind, string stringToAvoid, bool exactSearch, ListBox foodList)
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
}
