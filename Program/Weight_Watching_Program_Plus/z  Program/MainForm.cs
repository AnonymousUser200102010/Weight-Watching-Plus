#region Using Directives

using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UniversalHandlersLibrary;

#endregion


namespace WeightWatchingProgramPlus
{

	/// <summary>
	/// Main Window and all tabs.
	/// </summary>
	///
	internal partial class MainForm : Form, IMainForm
	{
		
		private IMainForm mainForm;
		
		private IGeneralFunctions Functions = new Functions ();
		
		private IPopup PopupHandler = new PopupHandler ();
		
		private IValidation Validation;
		
		private IStorage Storage;
		
		private IRetrieval Retrieval;
		
		private IMathematics Mathematics;
		
		private IModification Modification;
		
		private INetOps NetworkOps;
		
		private ToolTip[] toolTips = {
			new ToolTip()
		};

		internal MainForm ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			
			this.mainForm = (this as IMainForm);
			
			this.Storage = new Storage (this.PopupHandler, this.mainForm);
			
			this.Validation = new Validation (this.Storage, this.PopupHandler, this.mainForm);
			
			this.Retrieval = new Retrieval (Validation as Validation, this.Storage, this.mainForm);
			
			this.NetworkOps = new NetworkOps(this.Retrieval, this.mainForm);
			
			this.Mathematics = new Mathematics (this.PopupHandler, this.Retrieval, this.mainForm);
			
			this.Modification = new Modification (this.PopupHandler, this.Validation, this.Storage, this.Mathematics, this.Functions, this.Retrieval, this.mainForm);

			this.Functions.InitializeForms(this.Modification, this.Storage, this.Validation, this.Retrieval, this.NetworkOps, this.mainForm);
			
			
			
			//RegistryKey rHive = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "TESTMACHINE", RegistryView.Registry64);
			
		}

		#region Properties
		
		public void setMainFormState(bool paused)
		{
			
			this.Enabled = !paused;
			
		}

		public void MainFormVersionInfoText(string value)
		{
			
			this.productVersionInfoBar.Text = value;
			
		}
		
		public void MainFormBuildInfoText(string value)
		{
			
			this.productBuildInfoBar.Text = value;
			
		}
		
		public string MainFormTitle
		{
			
			get { return this.Text; }
			
			set { this.Text = value; }
				
		}
		
		public bool SyncEnabled
		{
			
			get { return this.syncEnabledToolStripMenuItem.Checked; }
			
			set { this.syncEnabledToolStripMenuItem.Checked = value; }
			
		}
		
		public string SyncIPAddress
		{
			
			get { return this.SyncPlaceToAccess.Text; }
			
			set { this.SyncPlaceToAccess.Text = value; }
			
		}
		
		public string SyncSendPort
		{
			
			get { return this.syncSendPort.Text; }
			
			set { this.syncSendPort.Text = value; }
			
		}
		
		public string SyncListenPort
		{
			
			get { return this.syncListenPort.Text; }
			
			set { this.syncListenPort.Text = value; }
			
		}
		
		public void SetSyncConnectionItems()
		{
			
			string[] serverConnectionMessages = {
				"not listening",
				"starting the listening phase",
				"listening",
				"recieving data"
			};
			
			string[] clientConnectionMessages = {
				"not sending data",
				"connecting to server",
				"connected to server",
				"transfering data",
			};
			
			Image[] connectionStateImage = {
				Image.FromFile("Files\\Assets\\network-disconnected.ico"),
				Image.FromFile("Files\\Assets\\network-connection-pending.ico"),
				Image.FromFile("Files\\Assets\\network-connected.ico"),
				Image.FromFile("Files\\Assets\\network-initiating-transfer.gif")
			};
			
			int serverConnectionStatus = this.NetworkOps.ServerConnectionStatus;
			
			int clientConnectionStatus = this.NetworkOps.ClientConnectionStatus;
			
			if(serverConnectionStatus > 0 || clientConnectionStatus > 0)
			{
				
				this.toolTips[0].SetToolTip(this.syncImage, string.Format(CultureInfo.CurrentCulture, "You're currently {0}{1}{2}.", serverConnectionMessages[serverConnectionStatus], " and ", clientConnectionMessages[clientConnectionStatus]));
				
			}
			else
			{
				
				this.toolTips[0].SetToolTip(this.syncImage, "You're no longer syncing.");
				
			}
			
			this.syncImage.BackgroundImage = serverConnectionStatus >= clientConnectionStatus ? connectionStateImage[serverConnectionStatus] : connectionStateImage[clientConnectionStatus];
			
		}
		
		public decimal UserProvidedServings
		{
				
			get
			{
				
				IMathematics math = (this.Mathematics as Mathematics);
				
				return AddSubSelectedSubTab.Text.Contains("explicit", StringComparison.OrdinalIgnoreCase) ? this.userServingInputTextBox.Value : (decimal)(math.PerformArithmeticOperation(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", this.GetArithmeticValue(true), this.GetArithmeticSign, this.GetArithmeticValue(false))));
				
			}
				
			set
			{ 
				
				this.userServingInputTextBox.Value = value;
				
				this.arithmeticNumericUpDown_Left.Value = value;
				
				this.arithmeticNumericUpDown_Right.Value = value;
			
			}
				
		}

		public bool ManualTimeIsInitiated
		{
				
			get{ return this.resetCaloriesManualCheckBox.Checked; }
				
			set{ this.resetCaloriesManualCheckBox.Checked = value; }
				
		}

		public DateTime ManualDateTime
		{
				
			get { return this.exactResetDatetimePicker.Value; }
				
			set { this.exactResetDatetimePicker.Value = value; }
				
		}

		public void MainFoodListDataSource(object value)
		{
				
			this.foodList.DataSource = value;
				
		}

		public ListBox.ObjectCollection MainFoodListItems
		{
				
			get { return this.foodList.Items; }
				
		}

		public int FoodListSelectedIndex (bool setIndex, int index, bool value)
		{
					
			if (setIndex)
			{
						
				this.foodList.SetSelected(index, value);
				
				GlobalVariables.SelectedListItem = index;
				
				#if DEBUG
				
				Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Food : {0}\nGlobal Vars SI: {1}\nFood Name: {2}", this.foodList.SelectedIndex, GlobalVariables.SelectedListItem, FoodRelated.CombinedFoodList [this.foodList.SelectedIndex].Item1));
				
				#endif
						
			}

			return this.foodList.SelectedIndex;			
					
		}

		public int GetFoodListTopItem
		{
				
			get { return this.foodList.TopIndex; }
				
		}

		public string NumberOfServingsLabel
		{
				
			set { this.howManyServingsLabel.Text = value; }
				
		}

		public string FoodNameProperty
		{
				
			get { return this.foodNameEditBox.Text; }
				
			set
			{ 
					
				this.foodNameEditBox.Text = value;
				
				this.foodNameEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		public string DefinerProperty
		{
				
			get { return this.definerEditBox.Text; }
				
			set
			{ 
					
				this.definerEditBox.Text = value; 
				
				this.definerEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		public decimal ServingSizeProperty
		{
				
			get { return this.servingSizeEditBox.Value; }
				
			set
			{
					
				this.servingSizeEditBox.Value = value; 
				
				this.servingSizeEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		public decimal CaloriesPerServingProperty
		{
				
			get { return this.caloriesPerServingEditBox.Value; }
				
			set
			{
					
				this.caloriesPerServingEditBox.Value = value; 
				
				this.caloriesPerServingEditBox.TextAlign = HorizontalAlignment.Center;
					
			}
				
		}

		public bool IsDrinkProperty
		{
				
			get { return this.isDrinkCheckBox.Checked; }
				
			set { this.isDrinkCheckBox.Checked = value; }
				
		}

		public bool UserCheckingTime
		{
				
			get { return this.timeRadioButton.Checked; }
				
		}

		public bool UserCheckingCalories
		{
				
			get { return this.calorieRadioButton.Checked; }
				
		}

		public void UserSetCalories(decimal value)
		{
				
			this.manualCalorieEditBox.Value = value;
				
		}

		public decimal DefaultCalories
		{
				
			get { return this.defaultCaloriesNumericUpDown.Value; }
			
			set { this.defaultCaloriesNumericUpDown.Value = value; }
				
		}

		public decimal DecimalPlaces
		{
			
			get { return this.decimalPlacesNumericUpDown.Value; }
			
			set { this.decimalPlacesNumericUpDown.Value = value; }
			
		}

		public void DecimalExample(string value)
		{
			
			this.exampleNumDecPlaceTextBox.Text = value;
			
		}

		public bool IsCreatingNewFoodItem
		{
				
			get { return this.newItemCheckbox.Checked; }
				
			set { this.newItemCheckbox.Checked = value; }
				
		}
		
		public Control ReturnPropertyControl (int controlID)
		{
				
			switch (controlID)
			{
						
				case 0:
					return this.foodNameEditBox;
						
				case 1:
					return this.servingSizeEditBox;
						
				case 2:
					return this.caloriesPerServingEditBox;
						
				case 3:
					return this.definerEditBox;
						
				case 4:
					return this.caloriesLabel;
						
			}
				
			throw new ArgumentOutOfRangeException("controlID", controlID, string.Format(CultureInfo.CurrentCulture, "Value must be between {0} and {1}", 0, 4));
				
		}

		public bool UserIsWritingDiaryToFile
		{
				
			get { return this.WriteToFileCheckBox.Checked; }
				
		}
		
		public bool DiaryIsBeingUsed
		{
				
			get { return this.RecordFoodCheckBox.Checked; }
				
			private set { this.RecordFoodCheckBox.Checked = value; }
				
		}
		
		public string GetArithmeticSign
		{
			
			get { return this.arithmeticSignComboBox.SelectedItem.ToString(); }
			
		}

		public int SetArithmeticSign
		{
			
			set { this.arithmeticSignComboBox.SelectedIndex = value; }
			
		}

		
		public TabPage AddSubSelectedSubTab
		{
			
			get { return this.AddSub_SubTabControl.SelectedTab; }
			
			set { this.AddSub_SubTabControl.SelectedTab = value; }
			
		}

		public decimal GetArithmeticValue (bool left)
		{
			
			return left ? this.arithmeticNumericUpDown_Left.Value : this.arithmeticNumericUpDown_Right.Value;
			
		}
		
		public void SetAllDecimalPointValues(int value)
		{
			
			this.arithmeticNumericUpDown_Left.DecimalPlaces = value;
			
			this.defaultCaloriesNumericUpDown.DecimalPlaces = value;
			
			this.userServingInputTextBox.DecimalPlaces = value;
			
			this.caloriesPerServingEditBox.DecimalPlaces = value;
			
			this.manualCalorieEditBox.DecimalPlaces = value;
			
			double possibleMinNumber = 0.0000000000;
			
			possibleMinNumber = double.Parse(string.Format(CultureInfo.InvariantCulture, "{0}1", possibleMinNumber.ToString(string.Format(CultureInfo.InvariantCulture, "F{0}", (value - 1)), CultureInfo.InvariantCulture)), CultureInfo.CurrentCulture);
			
			this.GlobalMinimumValue = possibleMinNumber;
			
			if(value <= 4)
			{
				this.servingSizeEditBox.DecimalPlaces = value;
				this.servingSizeEditBox.Minimum = (decimal)possibleMinNumber;
			}
			else if (value > 0)
			{
				
				this.servingSizeEditBox.DecimalPlaces = 4;
				this.servingSizeEditBox.Minimum = (decimal)(.0001);
				
			}
			else
			{
				
				throw new InvalidCastException("Value for SetAllDecimalPointValues is out of bounds!");
				
			}
			
		}
		
		
		public decimal ServingSizeMinimumValue
		{
			
			get { return this.servingSizeEditBox.Minimum; }
			
		}
		
		public decimal ManualCaloriesMinimumValue
		{
			
			get { return this.manualCalorieEditBox.Minimum; }
			
		}
		
		public double GlobalMinimumValue
		{
			
			set
			{
				
				this.arithmeticNumericUpDown_Left.Minimum = (decimal)value;

				this.defaultCaloriesNumericUpDown.Minimum = (decimal)value;
				
				this.userServingInputTextBox.Minimum = (decimal)value;
				
				this.caloriesPerServingEditBox.Minimum = (decimal)value;

				this.manualCalorieEditBox.Minimum = -(decimal)(double.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture) + value);
				
			}
			
			
			get { return (double)this.defaultCaloriesNumericUpDown.Minimum; }
			
		}

		#endregion

		#region Main Tab
		
		private void MainTabSelectedIndexChanged(object sender, EventArgs e)
		{
			
			var registryTuple = Tuple.Create(decimal.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture), decimal.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture), int.Parse(this.Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture));
			
			exampleNumDecPlaceTextBox.Text = registryTuple.Item1.ToString(string.Format(CultureInfo.CurrentCulture, "F{0}", decimalPlacesNumericUpDown.Value), CultureInfo.CurrentCulture);
			
			decimalPlacesNumericUpDown.Value = registryTuple.Item3;
			
			defaultCaloriesNumericUpDown.Value = (decimal)registryTuple.Item2;
			
		}

		#region Search Bar

		private void SearchBarTextChanged (object sender, EventArgs e)
		{
				
			if (!string.IsNullOrWhiteSpace(searchBar.Text) && !searchBar.Text.Equals("Click Here to Search the Food List", StringComparison.CurrentCultureIgnoreCase))
			{
					
				if (!nextSearchButton.Enabled)
				{
						
					nextSearchButton.Enabled = true;
						
				}
					
				Functions.Find(0, searchBar.Text, null, exactSearchCheckBox.Checked, false, this.PopupHandler, mainForm);
					
				return;
					
			}
			
			if (nextSearchButton.Enabled)
			{
					
				nextSearchButton.Enabled = false;
					
			}
				
			foodList.ClearSelected();
				
		}

		private void SearchBarFocusGranted (object sender, EventArgs e)
		{
				
			searchBar.Clear();
				
			clearSearchBarButton.Enabled = true;
				
			searchBar.Font = new Font ("Times New Roman", 10f);
				
		}

		private void ClearSearchBar (object sender, EventArgs e)
		{
				
			if (!nextSearchButton.Focused)
			{
					
				searchBar.Clear();
					
				clearSearchBarButton.Enabled = false;
					
				this.Modification.WriteToObject(searchBar, 2);
					
				nextSearchButton.Enabled = false;
				
				foodList.SetSelected(GlobalVariables.SelectedListItem, true);
					
			}
				
		}

		private void FindNextSearchItem (object sender, EventArgs e)
		{
				
			if (string.IsNullOrWhiteSpace(searchBar.Text) || searchBar.Text.Equals("Click Here to Search the Food List", StringComparison.CurrentCultureIgnoreCase))
			{
					
				if (nextSearchButton.Enabled)
				{
						
					nextSearchButton.Enabled = false;
						
				}
					
				return;
					
			}
				
			this.Functions.Find(GlobalVariables.SelectedListItem, searchBar.Text, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, exactSearchCheckBox.Checked, true, this.PopupHandler, mainForm);
				
		}

		#endregion

		#region Food Listbox

		private void FoodListSelectedIndexChanged (object sender, EventArgs e)
		{
				
			if (foodList.SelectedIndex > -1)
			{
					
				if (newItemCheckbox.Checked)
				{
						
					newItemCheckbox.Checked = false;
						
				}
				
				GlobalVariables.SelectedListItem = foodList.SelectedIndex;
					
				howManyServingsLabel.Text = string.Format(CultureInfo.CurrentCulture, "How many {0}s of {1} do you plan on {2}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item4, FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1, FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item5 ? "drinking" : "eating");
				
				subtractCaloriesButton.Text = string.Format(CultureInfo.CurrentCulture, "Subtract {0}'s calories from your daily calorie allowance", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1);
				
				addCaloriesButtonMain.Text = string.Format(CultureInfo.CurrentCulture, "Add {0}'s calories to your daily calorie allowance", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1);
					
				FoodNameProperty = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item1;
					
				ServingSizeProperty = (decimal)FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item2;
					
				CaloriesPerServingProperty = (decimal)FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item3;
				
				DefinerProperty = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item4;
				
				isDrinkCheckBox.Checked = FoodRelated.CombinedFoodList [foodList.SelectedIndex].Item5;
				
				#if DEBUG
				
				Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Food : {0}\nGlobal Vars SI: {1}\nFood Name: {2}", this.foodList.SelectedIndex, GlobalVariables.SelectedListItem, FoodRelated.CombinedFoodList [this.foodList.SelectedIndex].Item1));
				
				#endif
					
			}
				
		}

		private void FoodListFocusChanged (object sender, EventArgs e)
		{
				
			if (newItemCheckbox.Checked)
			{
					
				newItemCheckbox.Checked = false;
					
			}
				
			if (foodList.Focused && foodList.SelectedIndex > -1)
			{
				
				GlobalVariables.SelectedListItem = foodList.SelectedIndex;
					
			}
			else
			{
					
				foodList.ClearSelected();
					
			}
				
		}

		private void DeleteFoodItemFromTable (object sender, EventArgs e)
		{
			
			if(foodList.Items.Count > 1)
			{
				
				if (this.PopupHandler.CreatePopup(string.Format(CultureInfo.CurrentCulture, "Are you sure you want to delete {0}?", FoodRelated.CombinedFoodList [GlobalVariables.SelectedListItem].Item1), 5) == DialogResult.Yes)
				{
					
					int previouslySelectedIndex = GlobalVariables.SelectedListItem;
					
					this.Modification.ModifyFoodPropertiesList();
					
					FoodRelated.CombinedFoodList.RemoveAt(GlobalVariables.SelectedListItem);
						
					this.Storage.WriteFoodTable();
						
					Functions.RefreshFoodList(this.Storage, this.Retrieval, mainForm);
					
					Console.WriteLine(FoodRelated.CombinedFoodList [previouslySelectedIndex < foodList.Items.Count ? previouslySelectedIndex : (foodList.Items.Count - 1)].Item1);
						
					foodList.SetSelected(previouslySelectedIndex < foodList.Items.Count ? previouslySelectedIndex : (foodList.Items.Count - 1), true);
					
				}
				
			}
			else
			{
				
				this.PopupHandler.CreatePopup("You cannot delete the last food item on your list!", 4);
				
			}
			
		}

		#endregion

		#region Edit Your Calories Tab

		private void NewItemCheckboxCheckedChanged (object sender, EventArgs e)
		{
			
			if (newItemCheckbox.Checked)
			{
				
				this.Modification.ModifyFoodPropertiesList();
				
			}
			else
			{
				
				foodList.SetSelected(GlobalVariables.SelectedListItem, true);
				
			}
				
			foodPropertiesButton.Text = newItemCheckbox.Checked ? "Add this new food item" : "Set Food Item Properties";
				
		}

		private void ResetCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = decimal.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture);
				
		}

		private void ZeroOutCaloriesButtonClicked (object sender, EventArgs e)
		{
				
			manualCalorieEditBox.Value = 0;
				
		}

		private void ManualSubmitButtonClicked (object sender, EventArgs e)
		{

			var registryTuple = Tuple.Create(double.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture), int.Parse(this.Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture));
			
			this.Storage.WriteRegistry(manualCalorieEditBox.Value.ToString(CultureInfo.CurrentCulture), "calories left", false, this.Validation, this.Retrieval);
				
			this.Validation.CheckCurrentRadioButton(this.Modification);
				
		}

		private void ChangedTabManualAddSub (object sender, EventArgs e)
		{
			
			manualCalorieEditBox.Value = decimal.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture);
				
		}

		#endregion

		private void TimeOrCaloriesChangedWithoutEvent (object sender, EventArgs e)
		{
				
			this.Retrieval.ReadRegistry();
				
			this.Validation.CheckCurrentRadioButton(this.Modification);
				
		}

		private void SetFoodPropertiesButtonClicked (object sender, EventArgs e)
		{
				
			string oldtext = foodNameEditBox.Text;
				
			this.Modification.ModifyFoodItemProperty(Validation.EditBoxesHaveValidEntries());
				
			Functions.Find(0, oldtext, null, true, false, this.PopupHandler, mainForm);
				
		}

		private void ModifyCalories (object sender, EventArgs e)
		{
			
			this.Modification.ModifyCalories(sender);
			
		}

		#endregion

		#region Additional Options Tab

		private void ResetCaloriesSpecificCheckBoxCheckStatusChanged (object sender, EventArgs e)
		{
			
			exactResetDatetimePicker.Enabled = resetCaloriesManualCheckBox.Checked;
			
			var registryTuple = Tuple.Create(double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture), double.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture), int.Parse(this.Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture));
			
			this.Storage.WriteRegistry("manual time", false, this.Validation, this.Retrieval);
			
		}

		private void ExactResetDatetimePickerValueChanged (object sender, EventArgs e)
		{
			
			var registryTuple = Tuple.Create(DateTime.Parse(Retrieval.GetRegistryValue("reset date"), CultureInfo.InvariantCulture), double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture), double.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture), int.Parse(this.Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture));
			
			if (resetCaloriesManualCheckBox.Checked && exactResetDatetimePicker.Value != registryTuple.Item1)
			{
				
				Storage.WriteRegistry(exactResetDatetimePicker.Value.ToString(), "reset date", true, this.Validation, this.Retrieval);
				
			}
			
		}

		private void DefaultCaloriesSetButtonClick (object sender, EventArgs e)
		{
			
			var registryTuple = Tuple.Create(double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture), int.Parse(this.Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture));
			
			this.Storage.WriteRegistry(defaultCaloriesNumericUpDown.Value.ToString(CultureInfo.CurrentCulture), "default calories", false, this.Validation, this.Retrieval);
			
		}

		private void LicenseInfoButtonClick (object sender, EventArgs e)
		{
			
			this.PopupHandler.CreatePopup(string.Format(CultureInfo.InstalledUICulture, "This program was released under {0}. You may obtain more information on this license from either the license file that was (supposed to be) included with this program, or from any of the sources at the project's GitHub page <https://github.com/AnonymousUser200102010/Weight-Watching-Plus>.", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright), 7);
			 
		}
		
		private void SetDecimalPlacesValueButtonClick(object sender, EventArgs e)
		{
			
			var registryTuple = Tuple.Create(double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture), double.Parse(this.Retrieval.GetRegistryValue("default calories"), CultureInfo.InvariantCulture));
			
			this.Storage.WriteRegistry(decimalPlacesNumericUpDown.Value.ToString(CultureInfo.CurrentCulture), "decimal places", false, this.Validation, this.Retrieval);
			
			int registryDecimalPlace = int.Parse(this.Retrieval.GetRegistryValue("decimal places"), CultureInfo.InvariantCulture);
			
			this.SetAllDecimalPointValues(registryDecimalPlace);
			
			TimeOrCaloriesChangedWithoutEvent(sender, e);
			
		}
		
		private void DecimalPlacesNumericUpDownValueChanged(object sender, EventArgs e)
		{
			
			exampleNumDecPlaceTextBox.Text = double.Parse(this.Retrieval.GetRegistryValue("calories left"), CultureInfo.InvariantCulture).ToString(string.Format(CultureInfo.CurrentCulture, "F{0}", decimalPlacesNumericUpDown.Value), CultureInfo.CurrentCulture);
			
		}
		#endregion
		
		#region Menu Items
		
		private void SyncStatusChanged(object sender, EventArgs e)
		{
			
			string senderText = sender.ToString();
			
			bool @checked = syncEnabledToolStripMenuItem.Checked;
			
			serverInfoToolStripMenuItem.Enabled = @checked;
				
			syncBehaviorToolStripItem.Enabled = @checked;
			
			if(@checked && this.NetworkOps.ServerConnectionStatus == 0)
			{
				
				this.NetworkOps.StartListen(int.Parse(syncListenPort.Text, CultureInfo.InvariantCulture));
				
			}
			else if (!@checked && this.NetworkOps.ServerConnectionStatus > 0)
			{
				
				this.NetworkOps.ServerConnectionStatus = 0;
				
			}
			
			if(this.Validation.PortIsValid(this.Retrieval, senderText.Contains("listen")))
			{
			
				if(string.Equals(senderText.Contains("listen") ? this.Retrieval.GetRegistryValue("sync listen socket") : this.Retrieval.GetRegistryValue("sync send socket"), senderText.Contains("listen") ? syncListenPort.Text : syncSendPort.Text))
				{
					
					this.Storage.WriteRegistry(senderText.Contains("listen") ? "sync listen socket" : "sync send socket", false, this.Validation, this.Retrieval);
					
				}
				
			}
			
			if(this.Validation.IPAddressIsValid(this.Retrieval))
			{
			
				this.Storage.WriteRegistry("sync server name", false, this.Validation, this.Retrieval);
				
			}
			
			this.Storage.WriteRegistry("sync enabled", false, this.Validation, this.Retrieval);
			
		}
		#endregion
		
		void SyncImageMouseHover(object sender, EventArgs e)
		{
			
			SetSyncConnectionItems();
			
		}
		void SyncImageMouseLeave(object sender, EventArgs e)
		{
			
			toolTips[0].Dispose();
			
			toolTips[0] = new ToolTip();
			
		}
		
	}
	
	

}
