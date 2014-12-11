﻿
namespace Weight_Watching_Program_Plus
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
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label howManyServingsLabel;
		private System.Windows.Forms.TabPage manualTab;
		private System.Windows.Forms.NumericUpDown caloriesPerServingEditBox;
		private System.Windows.Forms.NumericUpDown userServingInputTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label Seperator1;
		private System.Windows.Forms.Label Seperator2;
		private System.Windows.Forms.Button nextSearchButton;
		private System.Windows.Forms.CheckBox exactSearchCheckBox;
		private System.Windows.Forms.RadioButton timeRadioButton;
		private System.Windows.Forms.RadioButton calorieRadioButton;
		private System.Windows.Forms.CheckBox newItemCheckbox;
		private System.Windows.Forms.NumericUpDown manualCalorieEditBox;
		private System.Windows.Forms.Button manualZeroOutButton;
		private System.Windows.Forms.Button manualResetButton;
		private System.Windows.Forms.Button manualSubmitButton;
		
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
			this.manualTab = new System.Windows.Forms.TabPage();
			this.manualZeroOutButton = new System.Windows.Forms.Button();
			this.manualResetButton = new System.Windows.Forms.Button();
			this.manualSubmitButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.manualCalorieEditBox = new System.Windows.Forms.NumericUpDown();
			this.planTab = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.FoodListPage = new System.Windows.Forms.TabPage();
			this.newItemCheckbox = new System.Windows.Forms.CheckBox();
			this.timeRadioButton = new System.Windows.Forms.RadioButton();
			this.calorieRadioButton = new System.Windows.Forms.RadioButton();
			this.exactSearchCheckBox = new System.Windows.Forms.CheckBox();
			this.nextSearchButton = new System.Windows.Forms.Button();
			this.Seperator2 = new System.Windows.Forms.Label();
			this.Seperator1 = new System.Windows.Forms.Label();
			this.userServingInputTextBox = new System.Windows.Forms.NumericUpDown();
			this.caloriesPerServingEditBox = new System.Windows.Forms.NumericUpDown();
			this.howManyServingsLabel = new System.Windows.Forms.Label();
			this.servingSizeEditBox = new System.Windows.Forms.NumericUpDown();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.deleteSelectedFoodItemButton = new System.Windows.Forms.Button();
			this.caloriesLabel = new System.Windows.Forms.Label();
			this.clearSearchBarButton = new System.Windows.Forms.Button();
			this.foodPropertiesButton = new System.Windows.Forms.Button();
			this.definerLabel = new System.Windows.Forms.Label();
			this.definerEditBox = new System.Windows.Forms.TextBox();
			this.caloriesPerServingLabel = new System.Windows.Forms.Label();
			this.servingSizeLabel = new System.Windows.Forms.Label();
			this.foodNameLabel = new System.Windows.Forms.Label();
			this.foodNameEditBox = new System.Windows.Forms.TextBox();
			this.searchLabel = new System.Windows.Forms.Label();
			this.searchBar = new System.Windows.Forms.TextBox();
			this.foodListLabel = new System.Windows.Forms.Label();
			this.foodList = new System.Windows.Forms.ListBox();
			this.tabsMenu = new System.Windows.Forms.TabControl();
			this.manualTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.manualCalorieEditBox)).BeginInit();
			this.planTab.SuspendLayout();
			this.FoodListPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.userServingInputTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.caloriesPerServingEditBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.servingSizeEditBox)).BeginInit();
			this.tabsMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// manualTab
			// 
			this.manualTab.AccessibleRole = System.Windows.Forms.AccessibleRole.WhiteSpace;
			this.manualTab.CausesValidation = false;
			this.manualTab.Controls.Add(this.manualZeroOutButton);
			this.manualTab.Controls.Add(this.manualResetButton);
			this.manualTab.Controls.Add(this.manualSubmitButton);
			this.manualTab.Controls.Add(this.label2);
			this.manualTab.Controls.Add(this.manualCalorieEditBox);
			this.manualTab.Location = new System.Drawing.Point(4, 28);
			this.manualTab.Name = "manualTab";
			this.manualTab.Padding = new System.Windows.Forms.Padding(3);
			this.manualTab.Size = new System.Drawing.Size(795, 507);
			this.manualTab.TabIndex = 2;
			this.manualTab.Text = "Edit Your Daily Calorie Allowance";
			this.manualTab.UseVisualStyleBackColor = true;
			// 
			// manualZeroOutButton
			// 
			this.manualZeroOutButton.Location = new System.Drawing.Point(387, 251);
			this.manualZeroOutButton.Name = "manualZeroOutButton";
			this.manualZeroOutButton.Size = new System.Drawing.Size(147, 40);
			this.manualZeroOutButton.TabIndex = 4;
			this.manualZeroOutButton.Text = "Zero Out Calories";
			this.manualZeroOutButton.UseVisualStyleBackColor = true;
			this.manualZeroOutButton.Click += new System.EventHandler(this.zeroOutCaloriesButtonClicked);
			// 
			// manualResetButton
			// 
			this.manualResetButton.Location = new System.Drawing.Point(246, 251);
			this.manualResetButton.Name = "manualResetButton";
			this.manualResetButton.Size = new System.Drawing.Size(142, 40);
			this.manualResetButton.TabIndex = 3;
			this.manualResetButton.Text = "Reset Calories To Default";
			this.manualResetButton.UseVisualStyleBackColor = true;
			this.manualResetButton.Click += new System.EventHandler(this.resetCaloriesButtonClicked);
			// 
			// manualSubmitButton
			// 
			this.manualSubmitButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.manualSubmitButton.Location = new System.Drawing.Point(246, 291);
			this.manualSubmitButton.Name = "manualSubmitButton";
			this.manualSubmitButton.Size = new System.Drawing.Size(288, 42);
			this.manualSubmitButton.TabIndex = 2;
			this.manualSubmitButton.Text = "Submit";
			this.manualSubmitButton.UseVisualStyleBackColor = true;
			this.manualSubmitButton.Click += new System.EventHandler(this.manualSubmitButtonClicked);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Bookman Old Style", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(246, 150);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(288, 44);
			this.label2.TabIndex = 1;
			this.label2.Text = "CALORIES";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.manualCalorieEditBox.Location = new System.Drawing.Point(246, 197);
			this.manualCalorieEditBox.Maximum = new decimal(new int[] {
			0,
			434162106,
			542,
			0});
			this.manualCalorieEditBox.Name = "manualCalorieEditBox";
			this.manualCalorieEditBox.Size = new System.Drawing.Size(288, 52);
			this.manualCalorieEditBox.TabIndex = 0;
			this.manualCalorieEditBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// planTab
			// 
			this.planTab.Controls.Add(this.label1);
			this.planTab.Location = new System.Drawing.Point(4, 28);
			this.planTab.Name = "planTab";
			this.planTab.Padding = new System.Windows.Forms.Padding(3);
			this.planTab.Size = new System.Drawing.Size(795, 507);
			this.planTab.TabIndex = 1;
			this.planTab.Text = "Plan a Meal";
			this.planTab.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Bookman Old Style", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(795, 504);
			this.label1.TabIndex = 0;
			this.label1.Text = "UNFINISHED";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FoodListPage
			// 
			this.FoodListPage.BackColor = System.Drawing.SystemColors.Control;
			this.FoodListPage.Controls.Add(this.newItemCheckbox);
			this.FoodListPage.Controls.Add(this.timeRadioButton);
			this.FoodListPage.Controls.Add(this.calorieRadioButton);
			this.FoodListPage.Controls.Add(this.exactSearchCheckBox);
			this.FoodListPage.Controls.Add(this.nextSearchButton);
			this.FoodListPage.Controls.Add(this.Seperator2);
			this.FoodListPage.Controls.Add(this.Seperator1);
			this.FoodListPage.Controls.Add(this.userServingInputTextBox);
			this.FoodListPage.Controls.Add(this.caloriesPerServingEditBox);
			this.FoodListPage.Controls.Add(this.howManyServingsLabel);
			this.FoodListPage.Controls.Add(this.servingSizeEditBox);
			this.FoodListPage.Controls.Add(this.button3);
			this.FoodListPage.Controls.Add(this.button2);
			this.FoodListPage.Controls.Add(this.deleteSelectedFoodItemButton);
			this.FoodListPage.Controls.Add(this.caloriesLabel);
			this.FoodListPage.Controls.Add(this.clearSearchBarButton);
			this.FoodListPage.Controls.Add(this.foodPropertiesButton);
			this.FoodListPage.Controls.Add(this.definerLabel);
			this.FoodListPage.Controls.Add(this.definerEditBox);
			this.FoodListPage.Controls.Add(this.caloriesPerServingLabel);
			this.FoodListPage.Controls.Add(this.servingSizeLabel);
			this.FoodListPage.Controls.Add(this.foodNameLabel);
			this.FoodListPage.Controls.Add(this.foodNameEditBox);
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
			this.FoodListPage.Text = "Food List";
			this.FoodListPage.ToolTipText = "The main menu of the program containing a food list and editing service, //TBD//";
			// 
			// newItemCheckbox
			// 
			this.newItemCheckbox.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.newItemCheckbox.Location = new System.Drawing.Point(737, 218);
			this.newItemCheckbox.Name = "newItemCheckbox";
			this.newItemCheckbox.Size = new System.Drawing.Size(52, 41);
			this.newItemCheckbox.TabIndex = 34;
			this.newItemCheckbox.Text = "New Item";
			this.newItemCheckbox.UseVisualStyleBackColor = true;
			this.newItemCheckbox.CheckedChanged += new System.EventHandler(this.NewItemCheckboxCheckedChanged);
			// 
			// timeRadioButton
			// 
			this.timeRadioButton.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeRadioButton.Location = new System.Drawing.Point(4, 25);
			this.timeRadioButton.Name = "timeRadioButton";
			this.timeRadioButton.Size = new System.Drawing.Size(63, 17);
			this.timeRadioButton.TabIndex = 33;
			this.timeRadioButton.TabStop = true;
			this.timeRadioButton.Text = "Time";
			this.timeRadioButton.UseVisualStyleBackColor = true;
			this.timeRadioButton.CheckedChanged += new System.EventHandler(this.TimeRadioButtonCheckedChanged);
			// 
			// calorieRadioButton
			// 
			this.calorieRadioButton.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.calorieRadioButton.Location = new System.Drawing.Point(4, 5);
			this.calorieRadioButton.Name = "calorieRadioButton";
			this.calorieRadioButton.Size = new System.Drawing.Size(63, 17);
			this.calorieRadioButton.TabIndex = 32;
			this.calorieRadioButton.TabStop = true;
			this.calorieRadioButton.Text = "Calories";
			this.calorieRadioButton.UseVisualStyleBackColor = true;
			this.calorieRadioButton.CheckedChanged += new System.EventHandler(this.CalorieRadioButtonCheckedChanged);
			// 
			// exactSearchCheckBox
			// 
			this.exactSearchCheckBox.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exactSearchCheckBox.Location = new System.Drawing.Point(3, 240);
			this.exactSearchCheckBox.Name = "exactSearchCheckBox";
			this.exactSearchCheckBox.Size = new System.Drawing.Size(57, 20);
			this.exactSearchCheckBox.TabIndex = 31;
			this.exactSearchCheckBox.Text = "Exact";
			this.exactSearchCheckBox.UseVisualStyleBackColor = true;
			this.exactSearchCheckBox.CheckedChanged += new System.EventHandler(this.ExactSearchCheckBoxCheckedChanged);
			// 
			// nextSearchButton
			// 
			this.nextSearchButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nextSearchButton.Location = new System.Drawing.Point(313, 240);
			this.nextSearchButton.Name = "nextSearchButton";
			this.nextSearchButton.Size = new System.Drawing.Size(54, 20);
			this.nextSearchButton.TabIndex = 30;
			this.nextSearchButton.Text = "Next";
			this.nextSearchButton.UseVisualStyleBackColor = true;
			this.nextSearchButton.Click += new System.EventHandler(this.findNextSearchItem);
			// 
			// Seperator2
			// 
			this.Seperator2.Location = new System.Drawing.Point(3, 272);
			this.Seperator2.Name = "Seperator2";
			this.Seperator2.Size = new System.Drawing.Size(785, 8);
			this.Seperator2.TabIndex = 29;
			this.Seperator2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Seperator1
			// 
			this.Seperator1.Location = new System.Drawing.Point(433, 42);
			this.Seperator1.Name = "Seperator1";
			this.Seperator1.Size = new System.Drawing.Size(10, 230);
			this.Seperator1.TabIndex = 28;
			// 
			// userServingInputTextBox
			// 
			this.userServingInputTextBox.DecimalPlaces = 9;
			this.userServingInputTextBox.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.userServingInputTextBox.Increment = new decimal(new int[] {
			1,
			0,
			0,
			196608});
			this.userServingInputTextBox.Location = new System.Drawing.Point(111, 329);
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
			this.userServingInputTextBox.TabIndex = 27;
			this.userServingInputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.userServingInputTextBox.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// caloriesPerServingEditBox
			// 
			this.caloriesPerServingEditBox.DecimalPlaces = 2;
			this.caloriesPerServingEditBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.caloriesPerServingEditBox.Location = new System.Drawing.Point(633, 136);
			this.caloriesPerServingEditBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.caloriesPerServingEditBox.Name = "caloriesPerServingEditBox";
			this.caloriesPerServingEditBox.Size = new System.Drawing.Size(156, 26);
			this.caloriesPerServingEditBox.TabIndex = 26;
			// 
			// howManyServingsLabel
			// 
			this.howManyServingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.howManyServingsLabel.Location = new System.Drawing.Point(111, 276);
			this.howManyServingsLabel.Name = "howManyServingsLabel";
			this.howManyServingsLabel.Size = new System.Drawing.Size(575, 33);
			this.howManyServingsLabel.TabIndex = 23;
			this.howManyServingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// servingSizeEditBox
			// 
			this.servingSizeEditBox.DecimalPlaces = 2;
			this.servingSizeEditBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.servingSizeEditBox.Location = new System.Drawing.Point(447, 137);
			this.servingSizeEditBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.servingSizeEditBox.Name = "servingSizeEditBox";
			this.servingSizeEditBox.Size = new System.Drawing.Size(168, 26);
			this.servingSizeEditBox.TabIndex = 20;
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button3.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.button3.Location = new System.Drawing.Point(13, 461);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(776, 43);
			this.button3.TabIndex = 18;
			this.button3.Text = "Add selected item\'s calories to your daily calorie allowance";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.addCalories);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button2.Location = new System.Drawing.Point(13, 394);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(776, 43);
			this.button2.TabIndex = 17;
			this.button2.Text = "Subtract selected food item from your daily calorie allowance";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.subtractCalories);
			// 
			// deleteSelectedFoodItemButton
			// 
			this.deleteSelectedFoodItemButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.deleteSelectedFoodItemButton.Location = new System.Drawing.Point(14, 215);
			this.deleteSelectedFoodItemButton.Name = "deleteSelectedFoodItemButton";
			this.deleteSelectedFoodItemButton.Size = new System.Drawing.Size(413, 23);
			this.deleteSelectedFoodItemButton.TabIndex = 16;
			this.deleteSelectedFoodItemButton.Text = "Delete Selected Food Item";
			this.deleteSelectedFoodItemButton.UseVisualStyleBackColor = true;
			this.deleteSelectedFoodItemButton.Click += new System.EventHandler(this.deleteFoodItemFromTable);
			// 
			// caloriesLabel
			// 
			this.caloriesLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.caloriesLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.caloriesLabel.Location = new System.Drawing.Point(73, 5);
			this.caloriesLabel.Name = "caloriesLabel";
			this.caloriesLabel.Size = new System.Drawing.Size(701, 37);
			this.caloriesLabel.TabIndex = 15;
			this.caloriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// clearSearchBarButton
			// 
			this.clearSearchBarButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.clearSearchBarButton.Location = new System.Drawing.Point(373, 241);
			this.clearSearchBarButton.Name = "clearSearchBarButton";
			this.clearSearchBarButton.Size = new System.Drawing.Size(54, 20);
			this.clearSearchBarButton.TabIndex = 14;
			this.clearSearchBarButton.Text = "Clear";
			this.clearSearchBarButton.UseVisualStyleBackColor = true;
			this.clearSearchBarButton.Click += new System.EventHandler(this.clearSearchBarButtonClicked);
			// 
			// foodPropertiesButton
			// 
			this.foodPropertiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodPropertiesButton.Location = new System.Drawing.Point(447, 218);
			this.foodPropertiesButton.Name = "foodPropertiesButton";
			this.foodPropertiesButton.Size = new System.Drawing.Size(284, 43);
			this.foodPropertiesButton.TabIndex = 13;
			this.foodPropertiesButton.Text = "Set Food Item Properties";
			this.foodPropertiesButton.UseVisualStyleBackColor = true;
			this.foodPropertiesButton.Click += new System.EventHandler(this.setFoodPropertiesButtonClicked);
			// 
			// definerLabel
			// 
			this.definerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.definerLabel.Location = new System.Drawing.Point(590, 162);
			this.definerLabel.Name = "definerLabel";
			this.definerLabel.Size = new System.Drawing.Size(65, 25);
			this.definerLabel.TabIndex = 11;
			this.definerLabel.Text = "Definer";
			this.definerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// definerEditBox
			// 
			this.definerEditBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.definerEditBox.Location = new System.Drawing.Point(447, 190);
			this.definerEditBox.Name = "definerEditBox";
			this.definerEditBox.Size = new System.Drawing.Size(342, 22);
			this.definerEditBox.TabIndex = 10;
			// 
			// caloriesPerServingLabel
			// 
			this.caloriesPerServingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.caloriesPerServingLabel.Location = new System.Drawing.Point(633, 108);
			this.caloriesPerServingLabel.Name = "caloriesPerServingLabel";
			this.caloriesPerServingLabel.Size = new System.Drawing.Size(156, 25);
			this.caloriesPerServingLabel.TabIndex = 9;
			this.caloriesPerServingLabel.Text = "Calories Per Serving";
			this.caloriesPerServingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// servingSizeLabel
			// 
			this.servingSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.servingSizeLabel.Location = new System.Drawing.Point(478, 109);
			this.servingSizeLabel.Name = "servingSizeLabel";
			this.servingSizeLabel.Size = new System.Drawing.Size(104, 25);
			this.servingSizeLabel.TabIndex = 8;
			this.servingSizeLabel.Text = " Serving Size";
			this.servingSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodNameLabel
			// 
			this.foodNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodNameLabel.Location = new System.Drawing.Point(600, 52);
			this.foodNameLabel.Name = "foodNameLabel";
			this.foodNameLabel.Size = new System.Drawing.Size(55, 25);
			this.foodNameLabel.TabIndex = 7;
			this.foodNameLabel.Text = "Name";
			this.foodNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodNameEditBox
			// 
			this.foodNameEditBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.foodNameEditBox.Location = new System.Drawing.Point(447, 83);
			this.foodNameEditBox.Name = "foodNameEditBox";
			this.foodNameEditBox.Size = new System.Drawing.Size(342, 22);
			this.foodNameEditBox.TabIndex = 4;
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
			this.searchBar.TabIndex = 2;
			this.searchBar.Text = "Click Here to Search the Food List";
			this.searchBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.searchBar.TextChanged += new System.EventHandler(this.SearchBarTextChanged);
			this.searchBar.Enter += new System.EventHandler(this.searchBarFocusGranted);
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
			this.foodList.SelectedIndexChanged += new System.EventHandler(this.foodListSelectedIndexChanged);
			this.foodList.Enter += new System.EventHandler(this.foodListEnterFocus);
			this.foodList.Leave += new System.EventHandler(this.foodListLeaveFocus);
			// 
			// tabsMenu
			// 
			this.tabsMenu.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabsMenu.Controls.Add(this.FoodListPage);
			this.tabsMenu.Controls.Add(this.planTab);
			this.tabsMenu.Controls.Add(this.manualTab);
			this.tabsMenu.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabsMenu.HotTrack = true;
			this.tabsMenu.Location = new System.Drawing.Point(2, 0);
			this.tabsMenu.Multiline = true;
			this.tabsMenu.Name = "tabsMenu";
			this.tabsMenu.SelectedIndex = 0;
			this.tabsMenu.ShowToolTips = true;
			this.tabsMenu.Size = new System.Drawing.Size(803, 539);
			this.tabsMenu.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tabsMenu.TabIndex = 0;
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
			this.manualTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.manualCalorieEditBox)).EndInit();
			this.planTab.ResumeLayout(false);
			this.FoodListPage.ResumeLayout(false);
			this.FoodListPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.userServingInputTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.caloriesPerServingEditBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.servingSizeEditBox)).EndInit();
			this.tabsMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
