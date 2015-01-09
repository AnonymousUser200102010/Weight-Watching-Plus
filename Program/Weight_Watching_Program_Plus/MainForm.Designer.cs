﻿
namespace WeightWatchingProgramPlus
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabPage FoodListPage;
		private System.Windows.Forms.TabPage planTab;
		private System.Windows.Forms.TabControl tabsMenu;
		private System.Windows.Forms.ListBox foodList;
		private System.Windows.Forms.Label foodListLabel;
		private System.Windows.Forms.Label searchLabel;
		private System.Windows.Forms.TextBox searchBar;
		private System.Windows.Forms.Label definerLabel;
		private System.Windows.Forms.TextBox definerEditBox;
		private System.Windows.Forms.Label caloriesPerServingLabel;
		private System.Windows.Forms.Label servingSizeLabel;
		private System.Windows.Forms.Label foodNameLabel;
		private System.Windows.Forms.TextBox foodNameEditBox;
		private System.Windows.Forms.Button foodPropertiesButton;
		private System.Windows.Forms.Button clearSearchBarButton;
		private System.Windows.Forms.Label caloriesLabel;
		private System.Windows.Forms.Button deleteSelectedFoodItemButton;
		private System.Windows.Forms.NumericUpDown servingSizeEditBox;
		private System.Windows.Forms.Button addCaloriesButtonMain;
		private System.Windows.Forms.Button subtractCaloriesButton;
		private System.Windows.Forms.Label howManyServingsLabel;
		private System.Windows.Forms.NumericUpDown caloriesPerServingEditBox;
		private System.Windows.Forms.NumericUpDown userServingInputTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label ManualFoodLabel;
		private System.Windows.Forms.Button nextSearchButton;
		private System.Windows.Forms.CheckBox exactSearchCheckBox;
		private System.Windows.Forms.RadioButton timeRadioButton;
		private System.Windows.Forms.RadioButton calorieRadioButton;
		private System.Windows.Forms.CheckBox newItemCheckbox;
		private System.Windows.Forms.NumericUpDown manualCalorieEditBox;
		private System.Windows.Forms.Button manualZeroOutButton;
		private System.Windows.Forms.Button manualResetButton;
		private System.Windows.Forms.Button manualSubmitButton;
		private System.Windows.Forms.Button refreshCaloriesTimeButton;
		private System.Windows.Forms.TreeView planningFoodTree;
		private System.Windows.Forms.CheckBox RecordFoodCheckBox;
		private System.Windows.Forms.CheckBox WriteToFileCheckBox;
		private System.Windows.Forms.TabPage additionalOptionsTab;
		private System.Windows.Forms.CheckBox resetCaloriesManualCheckBox;
		private System.Windows.Forms.Label Seperator5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker exactResetDatetimePicker;
		private System.Windows.Forms.TabControl MainSubTabControl;
		private System.Windows.Forms.TabPage AddSubButton;
		private System.Windows.Forms.TabPage ManualButton;
		private System.Windows.Forms.GroupBox editFoodPropertiesGroupBox;
		private System.Windows.Forms.GroupBox resetCaloriesSpecificGroupBox;
		private System.Windows.Forms.GroupBox defaultCaloriesGroupBox;
		private System.Windows.Forms.Button defaultCaloriesSetButton;
		private System.Windows.Forms.NumericUpDown defaultCaloriesNumericUpDown;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.planTab = new System.Windows.Forms.TabPage();
			this.planningFoodTree = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.FoodListPage = new System.Windows.Forms.TabPage();
			this.editFoodPropertiesGroupBox = new System.Windows.Forms.GroupBox();
			this.newItemCheckbox = new System.Windows.Forms.CheckBox();
			this.caloriesPerServingEditBox = new System.Windows.Forms.NumericUpDown();
			this.servingSizeEditBox = new System.Windows.Forms.NumericUpDown();
			this.foodPropertiesButton = new System.Windows.Forms.Button();
			this.definerLabel = new System.Windows.Forms.Label();
			this.definerEditBox = new System.Windows.Forms.TextBox();
			this.caloriesPerServingLabel = new System.Windows.Forms.Label();
			this.servingSizeLabel = new System.Windows.Forms.Label();
			this.foodNameLabel = new System.Windows.Forms.Label();
			this.foodNameEditBox = new System.Windows.Forms.TextBox();
			this.MainSubTabControl = new System.Windows.Forms.TabControl();
			this.AddSubButton = new System.Windows.Forms.TabPage();
			this.WriteToFileCheckBox = new System.Windows.Forms.CheckBox();
			this.RecordFoodCheckBox = new System.Windows.Forms.CheckBox();
			this.userServingInputTextBox = new System.Windows.Forms.NumericUpDown();
			this.howManyServingsLabel = new System.Windows.Forms.Label();
			this.addCaloriesButtonMain = new System.Windows.Forms.Button();
			this.subtractCaloriesButton = new System.Windows.Forms.Button();
			this.ManualButton = new System.Windows.Forms.TabPage();
			this.manualZeroOutButton = new System.Windows.Forms.Button();
			this.manualResetButton = new System.Windows.Forms.Button();
			this.manualSubmitButton = new System.Windows.Forms.Button();
			this.ManualFoodLabel = new System.Windows.Forms.Label();
			this.manualCalorieEditBox = new System.Windows.Forms.NumericUpDown();
			this.refreshCaloriesTimeButton = new System.Windows.Forms.Button();
			this.timeRadioButton = new System.Windows.Forms.RadioButton();
			this.calorieRadioButton = new System.Windows.Forms.RadioButton();
			this.exactSearchCheckBox = new System.Windows.Forms.CheckBox();
			this.nextSearchButton = new System.Windows.Forms.Button();
			this.deleteSelectedFoodItemButton = new System.Windows.Forms.Button();
			this.caloriesLabel = new System.Windows.Forms.Label();
			this.clearSearchBarButton = new System.Windows.Forms.Button();
			this.searchLabel = new System.Windows.Forms.Label();
			this.searchBar = new System.Windows.Forms.TextBox();
			this.foodListLabel = new System.Windows.Forms.Label();
			this.foodList = new System.Windows.Forms.ListBox();
			this.tabsMenu = new System.Windows.Forms.TabControl();
			this.additionalOptionsTab = new System.Windows.Forms.TabPage();
			this.defaultCaloriesGroupBox = new System.Windows.Forms.GroupBox();
			this.defaultCaloriesSetButton = new System.Windows.Forms.Button();
			this.defaultCaloriesNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.resetCaloriesSpecificGroupBox = new System.Windows.Forms.GroupBox();
			this.exactResetDatetimePicker = new System.Windows.Forms.DateTimePicker();
			this.resetCaloriesManualCheckBox = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Seperator5 = new System.Windows.Forms.Label();
			this.planTab.SuspendLayout();
			this.FoodListPage.SuspendLayout();
			this.editFoodPropertiesGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.caloriesPerServingEditBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.servingSizeEditBox)).BeginInit();
			this.MainSubTabControl.SuspendLayout();
			this.AddSubButton.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.userServingInputTextBox)).BeginInit();
			this.ManualButton.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.manualCalorieEditBox)).BeginInit();
			this.tabsMenu.SuspendLayout();
			this.additionalOptionsTab.SuspendLayout();
			this.defaultCaloriesGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.defaultCaloriesNumericUpDown)).BeginInit();
			this.resetCaloriesSpecificGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// planTab
			// 
			this.planTab.Controls.Add(this.planningFoodTree);
			this.planTab.Controls.Add(this.label1);
			this.planTab.Location = new System.Drawing.Point(4, 28);
			this.planTab.Name = "planTab";
			this.planTab.Padding = new System.Windows.Forms.Padding(3);
			this.planTab.Size = new System.Drawing.Size(795, 507);
			this.planTab.TabIndex = 1;
			this.planTab.Text = "Plan a Meal";
			this.planTab.UseVisualStyleBackColor = true;
			// 
			// planningFoodTree
			// 
			this.planningFoodTree.Location = new System.Drawing.Point(0, 0);
			this.planningFoodTree.Name = "planningFoodTree";
			this.planningFoodTree.Size = new System.Drawing.Size(360, 507);
			this.planningFoodTree.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Bookman Old Style", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(359, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(436, 504);
			this.label1.TabIndex = 0;
			this.label1.Text = "UNFINISHED";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FoodListPage
			// 
			this.FoodListPage.BackColor = System.Drawing.SystemColors.Control;
			this.FoodListPage.Controls.Add(this.editFoodPropertiesGroupBox);
			this.FoodListPage.Controls.Add(this.MainSubTabControl);
			this.FoodListPage.Controls.Add(this.refreshCaloriesTimeButton);
			this.FoodListPage.Controls.Add(this.timeRadioButton);
			this.FoodListPage.Controls.Add(this.calorieRadioButton);
			this.FoodListPage.Controls.Add(this.exactSearchCheckBox);
			this.FoodListPage.Controls.Add(this.nextSearchButton);
			this.FoodListPage.Controls.Add(this.deleteSelectedFoodItemButton);
			this.FoodListPage.Controls.Add(this.caloriesLabel);
			this.FoodListPage.Controls.Add(this.clearSearchBarButton);
			this.FoodListPage.Controls.Add(this.searchLabel);
			this.FoodListPage.Controls.Add(this.searchBar);
			this.FoodListPage.Controls.Add(this.foodListLabel);
			this.FoodListPage.Controls.Add(this.foodList);
			this.FoodListPage.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FoodListPage.Location = new System.Drawing.Point(4, 28);
			this.FoodListPage.Name = "FoodListPage";
			this.FoodListPage.Padding = new System.Windows.Forms.Padding(3);
			this.FoodListPage.Size = new System.Drawing.Size(795, 507);
			this.FoodListPage.TabIndex = 0;
			this.FoodListPage.Text = "Main";
			this.FoodListPage.ToolTipText = "The main menu of the program containing a food list and editing service, //TBD//";
			// 
			// editFoodPropertiesGroupBox
			// 
			this.editFoodPropertiesGroupBox.Controls.Add(this.newItemCheckbox);
			this.editFoodPropertiesGroupBox.Controls.Add(this.caloriesPerServingEditBox);
			this.editFoodPropertiesGroupBox.Controls.Add(this.servingSizeEditBox);
			this.editFoodPropertiesGroupBox.Controls.Add(this.foodPropertiesButton);
			this.editFoodPropertiesGroupBox.Controls.Add(this.definerLabel);
			this.editFoodPropertiesGroupBox.Controls.Add(this.definerEditBox);
			this.editFoodPropertiesGroupBox.Controls.Add(this.caloriesPerServingLabel);
			this.editFoodPropertiesGroupBox.Controls.Add(this.servingSizeLabel);
			this.editFoodPropertiesGroupBox.Controls.Add(this.foodNameLabel);
			this.editFoodPropertiesGroupBox.Controls.Add(this.foodNameEditBox);
			this.editFoodPropertiesGroupBox.Location = new System.Drawing.Point(437, 41);
			this.editFoodPropertiesGroupBox.Name = "editFoodPropertiesGroupBox";
			this.editFoodPropertiesGroupBox.Size = new System.Drawing.Size(357, 233);
			this.editFoodPropertiesGroupBox.TabIndex = 22;
			this.editFoodPropertiesGroupBox.TabStop = false;
			// 
			// newItemCheckbox
			// 
			this.newItemCheckbox.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.newItemCheckbox.Location = new System.Drawing.Point(300, 177);
			this.newItemCheckbox.Name = "newItemCheckbox";
			this.newItemCheckbox.Size = new System.Drawing.Size(52, 41);
			this.newItemCheckbox.TabIndex = 12;
			this.newItemCheckbox.Text = "New Item";
			this.newItemCheckbox.UseVisualStyleBackColor = true;
			this.newItemCheckbox.CheckedChanged += new System.EventHandler(this.NewItemCheckboxCheckedChanged);
			// 
			// caloriesPerServingEditBox
			// 
			this.caloriesPerServingEditBox.DecimalPlaces = 2;
			this.caloriesPerServingEditBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.caloriesPerServingEditBox.Location = new System.Drawing.Point(196, 95);
			this.caloriesPerServingEditBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.caloriesPerServingEditBox.Name = "caloriesPerServingEditBox";
			this.caloriesPerServingEditBox.Size = new System.Drawing.Size(156, 26);
			this.caloriesPerServingEditBox.TabIndex = 9;
			// 
			// servingSizeEditBox
			// 
			this.servingSizeEditBox.DecimalPlaces = 2;
			this.servingSizeEditBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.servingSizeEditBox.Location = new System.Drawing.Point(10, 96);
			this.servingSizeEditBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.servingSizeEditBox.Name = "servingSizeEditBox";
			this.servingSizeEditBox.Size = new System.Drawing.Size(168, 26);
			this.servingSizeEditBox.TabIndex = 8;
			// 
			// foodPropertiesButton
			// 
			this.foodPropertiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodPropertiesButton.Location = new System.Drawing.Point(10, 177);
			this.foodPropertiesButton.Name = "foodPropertiesButton";
			this.foodPropertiesButton.Size = new System.Drawing.Size(284, 43);
			this.foodPropertiesButton.TabIndex = 11;
			this.foodPropertiesButton.Text = "Set Food Item Properties";
			this.foodPropertiesButton.UseVisualStyleBackColor = true;
			this.foodPropertiesButton.Click += new System.EventHandler(this.SetFoodPropertiesButtonClicked);
			// 
			// definerLabel
			// 
			this.definerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.definerLabel.Location = new System.Drawing.Point(153, 121);
			this.definerLabel.Name = "definerLabel";
			this.definerLabel.Size = new System.Drawing.Size(65, 25);
			this.definerLabel.TabIndex = 11;
			this.definerLabel.Text = "Definer";
			this.definerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// definerEditBox
			// 
			this.definerEditBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.definerEditBox.Location = new System.Drawing.Point(10, 149);
			this.definerEditBox.Name = "definerEditBox";
			this.definerEditBox.Size = new System.Drawing.Size(342, 22);
			this.definerEditBox.TabIndex = 10;
			// 
			// caloriesPerServingLabel
			// 
			this.caloriesPerServingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.caloriesPerServingLabel.Location = new System.Drawing.Point(196, 67);
			this.caloriesPerServingLabel.Name = "caloriesPerServingLabel";
			this.caloriesPerServingLabel.Size = new System.Drawing.Size(156, 25);
			this.caloriesPerServingLabel.TabIndex = 9;
			this.caloriesPerServingLabel.Text = "Calories Per Serving";
			this.caloriesPerServingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// servingSizeLabel
			// 
			this.servingSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.servingSizeLabel.Location = new System.Drawing.Point(41, 68);
			this.servingSizeLabel.Name = "servingSizeLabel";
			this.servingSizeLabel.Size = new System.Drawing.Size(104, 25);
			this.servingSizeLabel.TabIndex = 8;
			this.servingSizeLabel.Text = " Serving Size";
			this.servingSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodNameLabel
			// 
			this.foodNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodNameLabel.Location = new System.Drawing.Point(163, 16);
			this.foodNameLabel.Name = "foodNameLabel";
			this.foodNameLabel.Size = new System.Drawing.Size(55, 20);
			this.foodNameLabel.TabIndex = 7;
			this.foodNameLabel.Text = "Name";
			this.foodNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodNameEditBox
			// 
			this.foodNameEditBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodNameEditBox.Location = new System.Drawing.Point(10, 42);
			this.foodNameEditBox.Name = "foodNameEditBox";
			this.foodNameEditBox.Size = new System.Drawing.Size(342, 22);
			this.foodNameEditBox.TabIndex = 7;
			// 
			// MainSubTabControl
			// 
			this.MainSubTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.MainSubTabControl.Controls.Add(this.AddSubButton);
			this.MainSubTabControl.Controls.Add(this.ManualButton);
			this.MainSubTabControl.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainSubTabControl.Location = new System.Drawing.Point(0, 283);
			this.MainSubTabControl.Name = "MainSubTabControl";
			this.MainSubTabControl.SelectedIndex = 0;
			this.MainSubTabControl.Size = new System.Drawing.Size(792, 228);
			this.MainSubTabControl.TabIndex = 21;
			this.MainSubTabControl.SelectedIndexChanged += new System.EventHandler(this.ChangedTabManualAddSub);
			// 
			// AddSubButton
			// 
			this.AddSubButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AddSubButton.Controls.Add(this.WriteToFileCheckBox);
			this.AddSubButton.Controls.Add(this.RecordFoodCheckBox);
			this.AddSubButton.Controls.Add(this.userServingInputTextBox);
			this.AddSubButton.Controls.Add(this.howManyServingsLabel);
			this.AddSubButton.Controls.Add(this.addCaloriesButtonMain);
			this.AddSubButton.Controls.Add(this.subtractCaloriesButton);
			this.AddSubButton.Location = new System.Drawing.Point(4, 27);
			this.AddSubButton.Name = "AddSubButton";
			this.AddSubButton.Padding = new System.Windows.Forms.Padding(3);
			this.AddSubButton.Size = new System.Drawing.Size(784, 197);
			this.AddSubButton.TabIndex = 0;
			this.AddSubButton.Text = "Add/Subtract";
			this.AddSubButton.UseVisualStyleBackColor = true;
			// 
			// WriteToFileCheckBox
			// 
			this.WriteToFileCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.WriteToFileCheckBox.Checked = true;
			this.WriteToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.WriteToFileCheckBox.Enabled = false;
			this.WriteToFileCheckBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WriteToFileCheckBox.Location = new System.Drawing.Point(679, 70);
			this.WriteToFileCheckBox.Name = "WriteToFileCheckBox";
			this.WriteToFileCheckBox.Size = new System.Drawing.Size(96, 26);
			this.WriteToFileCheckBox.TabIndex = 26;
			this.WriteToFileCheckBox.Text = "Write to File?";
			this.WriteToFileCheckBox.UseVisualStyleBackColor = true;
			// 
			// RecordFoodCheckBox
			// 
			this.RecordFoodCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.RecordFoodCheckBox.Checked = true;
			this.RecordFoodCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.RecordFoodCheckBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RecordFoodCheckBox.Location = new System.Drawing.Point(679, 39);
			this.RecordFoodCheckBox.Name = "RecordFoodCheckBox";
			this.RecordFoodCheckBox.Size = new System.Drawing.Size(97, 25);
			this.RecordFoodCheckBox.TabIndex = 25;
			this.RecordFoodCheckBox.Text = "Record?";
			this.RecordFoodCheckBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.RecordFoodCheckBox.UseVisualStyleBackColor = true;
			// 
			// userServingInputTextBox
			// 
			this.userServingInputTextBox.DecimalPlaces = 9;
			this.userServingInputTextBox.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.userServingInputTextBox.Location = new System.Drawing.Point(98, 50);
			this.userServingInputTextBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.userServingInputTextBox.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			655360});
			this.userServingInputTextBox.Name = "userServingInputTextBox";
			this.userServingInputTextBox.Size = new System.Drawing.Size(575, 35);
			this.userServingInputTextBox.TabIndex = 24;
			this.userServingInputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.userServingInputTextBox.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// howManyServingsLabel
			// 
			this.howManyServingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.howManyServingsLabel.Location = new System.Drawing.Point(98, 3);
			this.howManyServingsLabel.Name = "howManyServingsLabel";
			this.howManyServingsLabel.Size = new System.Drawing.Size(575, 33);
			this.howManyServingsLabel.TabIndex = 29;
			this.howManyServingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// addCaloriesButtonMain
			// 
			this.addCaloriesButtonMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.addCaloriesButtonMain.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.addCaloriesButtonMain.Location = new System.Drawing.Point(6, 158);
			this.addCaloriesButtonMain.Name = "addCaloriesButtonMain";
			this.addCaloriesButtonMain.Size = new System.Drawing.Size(770, 33);
			this.addCaloriesButtonMain.TabIndex = 28;
			this.addCaloriesButtonMain.Text = "Add selected item\'s calories to your daily calorie allowance";
			this.addCaloriesButtonMain.UseVisualStyleBackColor = true;
			this.addCaloriesButtonMain.Click += new System.EventHandler(this.ModifyCalories);
			// 
			// subtractCaloriesButton
			// 
			this.subtractCaloriesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.subtractCaloriesButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.subtractCaloriesButton.Location = new System.Drawing.Point(6, 107);
			this.subtractCaloriesButton.Name = "subtractCaloriesButton";
			this.subtractCaloriesButton.Size = new System.Drawing.Size(770, 36);
			this.subtractCaloriesButton.TabIndex = 27;
			this.subtractCaloriesButton.Text = "Subtract selected food item from your daily calorie allowance";
			this.subtractCaloriesButton.UseVisualStyleBackColor = true;
			this.subtractCaloriesButton.Click += new System.EventHandler(this.ModifyCalories);
			// 
			// ManualButton
			// 
			this.ManualButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ManualButton.Controls.Add(this.manualZeroOutButton);
			this.ManualButton.Controls.Add(this.manualResetButton);
			this.ManualButton.Controls.Add(this.manualSubmitButton);
			this.ManualButton.Controls.Add(this.ManualFoodLabel);
			this.ManualButton.Controls.Add(this.manualCalorieEditBox);
			this.ManualButton.Location = new System.Drawing.Point(4, 27);
			this.ManualButton.Name = "ManualButton";
			this.ManualButton.Padding = new System.Windows.Forms.Padding(3);
			this.ManualButton.Size = new System.Drawing.Size(784, 197);
			this.ManualButton.TabIndex = 1;
			this.ManualButton.Text = "Manual";
			this.ManualButton.UseVisualStyleBackColor = true;
			// 
			// manualZeroOutButton
			// 
			this.manualZeroOutButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.manualZeroOutButton.Location = new System.Drawing.Point(401, 112);
			this.manualZeroOutButton.Name = "manualZeroOutButton";
			this.manualZeroOutButton.Size = new System.Drawing.Size(377, 40);
			this.manualZeroOutButton.TabIndex = 9;
			this.manualZeroOutButton.Text = "Zero Out Calories";
			this.manualZeroOutButton.UseVisualStyleBackColor = true;
			this.manualZeroOutButton.Click += new System.EventHandler(this.ZeroOutCaloriesButtonClicked);
			// 
			// manualResetButton
			// 
			this.manualResetButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.manualResetButton.Location = new System.Drawing.Point(6, 112);
			this.manualResetButton.Name = "manualResetButton";
			this.manualResetButton.Size = new System.Drawing.Size(389, 40);
			this.manualResetButton.TabIndex = 8;
			this.manualResetButton.Text = "Reset Calories To Default";
			this.manualResetButton.UseVisualStyleBackColor = true;
			this.manualResetButton.Click += new System.EventHandler(this.ResetCaloriesButtonClicked);
			// 
			// manualSubmitButton
			// 
			this.manualSubmitButton.Font = new System.Drawing.Font("Bookman Old Style", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.manualSubmitButton.Location = new System.Drawing.Point(6, 158);
			this.manualSubmitButton.Name = "manualSubmitButton";
			this.manualSubmitButton.Size = new System.Drawing.Size(772, 33);
			this.manualSubmitButton.TabIndex = 7;
			this.manualSubmitButton.Text = "Submit";
			this.manualSubmitButton.UseVisualStyleBackColor = true;
			this.manualSubmitButton.Click += new System.EventHandler(this.ManualSubmitButtonClicked);
			// 
			// ManualFoodLabel
			// 
			this.ManualFoodLabel.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ManualFoodLabel.Location = new System.Drawing.Point(6, 3);
			this.ManualFoodLabel.Name = "ManualFoodLabel";
			this.ManualFoodLabel.Size = new System.Drawing.Size(772, 48);
			this.ManualFoodLabel.TabIndex = 6;
			this.ManualFoodLabel.Text = "Enter the amount of calories you want to set your daily allowance to in the box b" +
	"elow";
			this.ManualFoodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// manualCalorieEditBox
			// 
			this.manualCalorieEditBox.DecimalPlaces = 3;
			this.manualCalorieEditBox.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.manualCalorieEditBox.Increment = new decimal(new int[] {
			1,
			0,
			0,
			196608});
			this.manualCalorieEditBox.Location = new System.Drawing.Point(6, 54);
			this.manualCalorieEditBox.Maximum = new decimal(new int[] {
			0,
			434162106,
			542,
			0});
			this.manualCalorieEditBox.Name = "manualCalorieEditBox";
			this.manualCalorieEditBox.Size = new System.Drawing.Size(772, 52);
			this.manualCalorieEditBox.TabIndex = 5;
			this.manualCalorieEditBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// refreshCaloriesTimeButton
			// 
			this.refreshCaloriesTimeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refreshCaloriesTimeButton.BackgroundImage")));
			this.refreshCaloriesTimeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.refreshCaloriesTimeButton.Location = new System.Drawing.Point(756, 10);
			this.refreshCaloriesTimeButton.Name = "refreshCaloriesTimeButton";
			this.refreshCaloriesTimeButton.Size = new System.Drawing.Size(32, 27);
			this.refreshCaloriesTimeButton.TabIndex = 20;
			this.refreshCaloriesTimeButton.UseVisualStyleBackColor = true;
			this.refreshCaloriesTimeButton.Click += new System.EventHandler(this.TimeOrCaloriesChangedWithoutEvent);
			// 
			// timeRadioButton
			// 
			this.timeRadioButton.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeRadioButton.Location = new System.Drawing.Point(4, 25);
			this.timeRadioButton.Name = "timeRadioButton";
			this.timeRadioButton.Size = new System.Drawing.Size(63, 17);
			this.timeRadioButton.TabIndex = 19;
			this.timeRadioButton.TabStop = true;
			this.timeRadioButton.Text = "Time";
			this.timeRadioButton.UseVisualStyleBackColor = true;
			this.timeRadioButton.CheckedChanged += new System.EventHandler(this.TimeOrCaloriesChangedWithoutEvent);
			// 
			// calorieRadioButton
			// 
			this.calorieRadioButton.Checked = true;
			this.calorieRadioButton.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.calorieRadioButton.Location = new System.Drawing.Point(4, 5);
			this.calorieRadioButton.Name = "calorieRadioButton";
			this.calorieRadioButton.Size = new System.Drawing.Size(63, 17);
			this.calorieRadioButton.TabIndex = 18;
			this.calorieRadioButton.TabStop = true;
			this.calorieRadioButton.Text = "Calories";
			this.calorieRadioButton.UseVisualStyleBackColor = true;
			this.calorieRadioButton.CheckedChanged += new System.EventHandler(this.TimeOrCaloriesChangedWithoutEvent);
			// 
			// exactSearchCheckBox
			// 
			this.exactSearchCheckBox.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exactSearchCheckBox.Location = new System.Drawing.Point(3, 240);
			this.exactSearchCheckBox.Name = "exactSearchCheckBox";
			this.exactSearchCheckBox.Size = new System.Drawing.Size(57, 20);
			this.exactSearchCheckBox.TabIndex = 5;
			this.exactSearchCheckBox.Text = "Exact";
			this.exactSearchCheckBox.UseVisualStyleBackColor = true;
			this.exactSearchCheckBox.Click += new System.EventHandler(this.FoodListFocusChanged);
			// 
			// nextSearchButton
			// 
			this.nextSearchButton.Enabled = false;
			this.nextSearchButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nextSearchButton.Location = new System.Drawing.Point(313, 240);
			this.nextSearchButton.Name = "nextSearchButton";
			this.nextSearchButton.Size = new System.Drawing.Size(54, 20);
			this.nextSearchButton.TabIndex = 4;
			this.nextSearchButton.Text = "Next";
			this.nextSearchButton.UseVisualStyleBackColor = true;
			this.nextSearchButton.Click += new System.EventHandler(this.FindNextSearchItem);
			// 
			// deleteSelectedFoodItemButton
			// 
			this.deleteSelectedFoodItemButton.Enabled = false;
			this.deleteSelectedFoodItemButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.deleteSelectedFoodItemButton.Location = new System.Drawing.Point(14, 215);
			this.deleteSelectedFoodItemButton.Name = "deleteSelectedFoodItemButton";
			this.deleteSelectedFoodItemButton.Size = new System.Drawing.Size(413, 23);
			this.deleteSelectedFoodItemButton.TabIndex = 1;
			this.deleteSelectedFoodItemButton.Text = "Delete Selected Food Item";
			this.deleteSelectedFoodItemButton.UseVisualStyleBackColor = true;
			this.deleteSelectedFoodItemButton.Click += new System.EventHandler(this.DeleteFoodItemFromTable);
			// 
			// caloriesLabel
			// 
			this.caloriesLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.caloriesLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.caloriesLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.caloriesLabel.Location = new System.Drawing.Point(73, 5);
			this.caloriesLabel.Name = "caloriesLabel";
			this.caloriesLabel.Size = new System.Drawing.Size(677, 37);
			this.caloriesLabel.TabIndex = 0;
			this.caloriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// clearSearchBarButton
			// 
			this.clearSearchBarButton.Enabled = false;
			this.clearSearchBarButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.clearSearchBarButton.Location = new System.Drawing.Point(373, 241);
			this.clearSearchBarButton.Name = "clearSearchBarButton";
			this.clearSearchBarButton.Size = new System.Drawing.Size(54, 20);
			this.clearSearchBarButton.TabIndex = 6;
			this.clearSearchBarButton.Text = "Clear";
			this.clearSearchBarButton.UseVisualStyleBackColor = true;
			this.clearSearchBarButton.Click += new System.EventHandler(this.ClearSearchBar);
			// 
			// searchLabel
			// 
			this.searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.searchLabel.Location = new System.Drawing.Point(57, 241);
			this.searchLabel.Name = "searchLabel";
			this.searchLabel.Size = new System.Drawing.Size(58, 20);
			this.searchLabel.TabIndex = 3;
			this.searchLabel.Text = "Search";
			// 
			// searchBar
			// 
			this.searchBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.searchBar.Location = new System.Drawing.Point(111, 241);
			this.searchBar.Name = "searchBar";
			this.searchBar.Size = new System.Drawing.Size(196, 20);
			this.searchBar.TabIndex = 3;
			this.searchBar.Text = "Click Here to Search the Food List";
			this.searchBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.searchBar.TextChanged += new System.EventHandler(this.SearchBarTextChanged);
			this.searchBar.Enter += new System.EventHandler(this.SearchBarFocusGranted);
			this.searchBar.Leave += new System.EventHandler(this.ClearSearchBar);
			// 
			// foodListLabel
			// 
			this.foodListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodListLabel.Location = new System.Drawing.Point(168, 57);
			this.foodListLabel.Name = "foodListLabel";
			this.foodListLabel.Size = new System.Drawing.Size(102, 20);
			this.foodListLabel.TabIndex = 1;
			this.foodListLabel.Text = "Food Items";
			this.foodListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodList
			// 
			this.foodList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodList.FormattingEnabled = true;
			this.foodList.ItemHeight = 16;
			this.foodList.Location = new System.Drawing.Point(13, 80);
			this.foodList.Name = "foodList";
			this.foodList.Size = new System.Drawing.Size(414, 132);
			this.foodList.TabIndex = 0;
			this.foodList.SelectedIndexChanged += new System.EventHandler(this.FoodListSelectedIndexChanged);
			this.foodList.Enter += new System.EventHandler(this.FoodListFocusChanged);
			this.foodList.Leave += new System.EventHandler(this.FoodListFocusChanged);
			// 
			// tabsMenu
			// 
			this.tabsMenu.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabsMenu.Controls.Add(this.FoodListPage);
			this.tabsMenu.Controls.Add(this.planTab);
			this.tabsMenu.Controls.Add(this.additionalOptionsTab);
			this.tabsMenu.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabsMenu.HotTrack = true;
			this.tabsMenu.Location = new System.Drawing.Point(2, 0);
			this.tabsMenu.Multiline = true;
			this.tabsMenu.Name = "tabsMenu";
			this.tabsMenu.SelectedIndex = 0;
			this.tabsMenu.ShowToolTips = true;
			this.tabsMenu.Size = new System.Drawing.Size(803, 539);
			this.tabsMenu.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tabsMenu.TabIndex = 30;
			// 
			// additionalOptionsTab
			// 
			this.additionalOptionsTab.Controls.Add(this.defaultCaloriesGroupBox);
			this.additionalOptionsTab.Controls.Add(this.resetCaloriesSpecificGroupBox);
			this.additionalOptionsTab.Controls.Add(this.label3);
			this.additionalOptionsTab.Controls.Add(this.Seperator5);
			this.additionalOptionsTab.Location = new System.Drawing.Point(4, 28);
			this.additionalOptionsTab.Name = "additionalOptionsTab";
			this.additionalOptionsTab.Padding = new System.Windows.Forms.Padding(3);
			this.additionalOptionsTab.Size = new System.Drawing.Size(795, 507);
			this.additionalOptionsTab.TabIndex = 3;
			this.additionalOptionsTab.Text = "Additional Options";
			this.additionalOptionsTab.UseVisualStyleBackColor = true;
			// 
			// defaultCaloriesGroupBox
			// 
			this.defaultCaloriesGroupBox.Controls.Add(this.defaultCaloriesSetButton);
			this.defaultCaloriesGroupBox.Controls.Add(this.defaultCaloriesNumericUpDown);
			this.defaultCaloriesGroupBox.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.defaultCaloriesGroupBox.Location = new System.Drawing.Point(558, 6);
			this.defaultCaloriesGroupBox.Name = "defaultCaloriesGroupBox";
			this.defaultCaloriesGroupBox.Size = new System.Drawing.Size(144, 48);
			this.defaultCaloriesGroupBox.TabIndex = 38;
			this.defaultCaloriesGroupBox.TabStop = false;
			this.defaultCaloriesGroupBox.Text = "Default Calories";
			// 
			// defaultCaloriesSetButton
			// 
			this.defaultCaloriesSetButton.Location = new System.Drawing.Point(85, 18);
			this.defaultCaloriesSetButton.Name = "defaultCaloriesSetButton";
			this.defaultCaloriesSetButton.Size = new System.Drawing.Size(49, 21);
			this.defaultCaloriesSetButton.TabIndex = 1;
			this.defaultCaloriesSetButton.Text = "Set";
			this.defaultCaloriesSetButton.UseVisualStyleBackColor = true;
			this.defaultCaloriesSetButton.Click += new System.EventHandler(this.DefaultCaloriesSetButtonClick);
			// 
			// defaultCaloriesNumericUpDown
			// 
			this.defaultCaloriesNumericUpDown.DecimalPlaces = 2;
			this.defaultCaloriesNumericUpDown.Location = new System.Drawing.Point(6, 18);
			this.defaultCaloriesNumericUpDown.Maximum = new decimal(new int[] {
			999999,
			0,
			0,
			0});
			this.defaultCaloriesNumericUpDown.Minimum = new decimal(new int[] {
			1200,
			0,
			0,
			0});
			this.defaultCaloriesNumericUpDown.Name = "defaultCaloriesNumericUpDown";
			this.defaultCaloriesNumericUpDown.Size = new System.Drawing.Size(73, 22);
			this.defaultCaloriesNumericUpDown.TabIndex = 0;
			this.defaultCaloriesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.defaultCaloriesNumericUpDown.Value = new decimal(new int[] {
			1200,
			0,
			0,
			0});
			// 
			// resetCaloriesSpecificGroupBox
			// 
			this.resetCaloriesSpecificGroupBox.Controls.Add(this.exactResetDatetimePicker);
			this.resetCaloriesSpecificGroupBox.Controls.Add(this.resetCaloriesManualCheckBox);
			this.resetCaloriesSpecificGroupBox.Location = new System.Drawing.Point(6, 6);
			this.resetCaloriesSpecificGroupBox.Name = "resetCaloriesSpecificGroupBox";
			this.resetCaloriesSpecificGroupBox.Size = new System.Drawing.Size(547, 48);
			this.resetCaloriesSpecificGroupBox.TabIndex = 37;
			this.resetCaloriesSpecificGroupBox.TabStop = false;
			// 
			// exactResetDatetimePicker
			// 
			this.exactResetDatetimePicker.CustomFormat = "MMMMM dd yyyy hh:mm:ss tt";
			this.exactResetDatetimePicker.Enabled = false;
			this.exactResetDatetimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.exactResetDatetimePicker.Location = new System.Drawing.Point(271, 18);
			this.exactResetDatetimePicker.Name = "exactResetDatetimePicker";
			this.exactResetDatetimePicker.Size = new System.Drawing.Size(270, 23);
			this.exactResetDatetimePicker.TabIndex = 36;
			this.exactResetDatetimePicker.ValueChanged += new System.EventHandler(this.ExactResetDatetimePickerValueChanged);
			// 
			// resetCaloriesManualCheckBox
			// 
			this.resetCaloriesManualCheckBox.Location = new System.Drawing.Point(15, 17);
			this.resetCaloriesManualCheckBox.Name = "resetCaloriesManualCheckBox";
			this.resetCaloriesManualCheckBox.Size = new System.Drawing.Size(259, 24);
			this.resetCaloriesManualCheckBox.TabIndex = 0;
			this.resetCaloriesManualCheckBox.Text = "Reset Calories At a Specific Time";
			this.resetCaloriesManualCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.resetCaloriesManualCheckBox.UseVisualStyleBackColor = true;
			this.resetCaloriesManualCheckBox.CheckedChanged += new System.EventHandler(this.ResetCaloriesSpecificCheckBoxCheckStatusChanged);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Bookman Old Style", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(795, 418);
			this.label3.TabIndex = 35;
			this.label3.Text = "UNFINISHED";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Seperator5
			// 
			this.Seperator5.Location = new System.Drawing.Point(6, 3);
			this.Seperator5.Name = "Seperator5";
			this.Seperator5.Size = new System.Drawing.Size(783, 8);
			this.Seperator5.TabIndex = 33;
			this.Seperator5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(808, 542);
			this.Controls.Add(this.tabsMenu);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Weight Watching Program";
			this.planTab.ResumeLayout(false);
			this.FoodListPage.ResumeLayout(false);
			this.FoodListPage.PerformLayout();
			this.editFoodPropertiesGroupBox.ResumeLayout(false);
			this.editFoodPropertiesGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.caloriesPerServingEditBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.servingSizeEditBox)).EndInit();
			this.MainSubTabControl.ResumeLayout(false);
			this.AddSubButton.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.userServingInputTextBox)).EndInit();
			this.ManualButton.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.manualCalorieEditBox)).EndInit();
			this.tabsMenu.ResumeLayout(false);
			this.additionalOptionsTab.ResumeLayout(false);
			this.defaultCaloriesGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.defaultCaloriesNumericUpDown)).EndInit();
			this.resetCaloriesSpecificGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
