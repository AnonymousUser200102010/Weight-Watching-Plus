
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox isDrinkCheckBox;
		private System.Windows.Forms.NumericUpDown userServingInputTextBox;
		private System.Windows.Forms.TabControl AddSub_SubTabControl;
		private System.Windows.Forms.TabPage addSub_SubTabManual;
		private System.Windows.Forms.TabPage addSub_SubTabArithmetic;
		private System.Windows.Forms.NumericUpDown arithmeticNumericUpDown_Left;
		private System.Windows.Forms.ComboBox arithmeticSignComboBox;
		private System.Windows.Forms.NumericUpDown arithmeticNumericUpDown_Right;

		
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.exactSearchCheckBox = new System.Windows.Forms.CheckBox();
			this.nextSearchButton = new System.Windows.Forms.Button();
			this.deleteSelectedFoodItemButton = new System.Windows.Forms.Button();
			this.clearSearchBarButton = new System.Windows.Forms.Button();
			this.searchLabel = new System.Windows.Forms.Label();
			this.searchBar = new System.Windows.Forms.TextBox();
			this.foodList = new System.Windows.Forms.ListBox();
			this.editFoodPropertiesGroupBox = new System.Windows.Forms.GroupBox();
			this.isDrinkCheckBox = new System.Windows.Forms.CheckBox();
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
			this.AddSub_SubTabControl = new System.Windows.Forms.TabControl();
			this.addSub_SubTabArithmetic = new System.Windows.Forms.TabPage();
			this.arithmeticNumericUpDown_Right = new System.Windows.Forms.NumericUpDown();
			this.arithmeticSignComboBox = new System.Windows.Forms.ComboBox();
			this.arithmeticNumericUpDown_Left = new System.Windows.Forms.NumericUpDown();
			this.addSub_SubTabManual = new System.Windows.Forms.TabPage();
			this.userServingInputTextBox = new System.Windows.Forms.NumericUpDown();
			this.WriteToFileCheckBox = new System.Windows.Forms.CheckBox();
			this.RecordFoodCheckBox = new System.Windows.Forms.CheckBox();
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
			this.caloriesLabel = new System.Windows.Forms.Label();
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
			this.groupBox1.SuspendLayout();
			this.editFoodPropertiesGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.caloriesPerServingEditBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.servingSizeEditBox)).BeginInit();
			this.MainSubTabControl.SuspendLayout();
			this.AddSubButton.SuspendLayout();
			this.AddSub_SubTabControl.SuspendLayout();
			this.addSub_SubTabArithmetic.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.arithmeticNumericUpDown_Right)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.arithmeticNumericUpDown_Left)).BeginInit();
			this.addSub_SubTabManual.SuspendLayout();
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
			this.planTab.BackColor = System.Drawing.SystemColors.Control;
			this.planTab.Controls.Add(this.planningFoodTree);
			this.planTab.Controls.Add(this.label1);
			this.planTab.Location = new System.Drawing.Point(4, 28);
			this.planTab.Margin = new System.Windows.Forms.Padding(4);
			this.planTab.Name = "planTab";
			this.planTab.Padding = new System.Windows.Forms.Padding(4);
			this.planTab.Size = new System.Drawing.Size(1062, 568);
			this.planTab.TabIndex = 1;
			this.planTab.Text = "Plan a Meal";
			// 
			// planningFoodTree
			// 
			this.planningFoodTree.Location = new System.Drawing.Point(0, 0);
			this.planningFoodTree.Margin = new System.Windows.Forms.Padding(4);
			this.planningFoodTree.Name = "planningFoodTree";
			this.planningFoodTree.Size = new System.Drawing.Size(479, 564);
			this.planningFoodTree.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Bookman Old Style", 24F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(478, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(584, 564);
			this.label1.TabIndex = 0;
			this.label1.Text = "UNFINISHED";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FoodListPage
			// 
			this.FoodListPage.BackColor = System.Drawing.SystemColors.Control;
			this.FoodListPage.Controls.Add(this.groupBox1);
			this.FoodListPage.Controls.Add(this.editFoodPropertiesGroupBox);
			this.FoodListPage.Controls.Add(this.MainSubTabControl);
			this.FoodListPage.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold);
			this.FoodListPage.Location = new System.Drawing.Point(4, 28);
			this.FoodListPage.Margin = new System.Windows.Forms.Padding(4);
			this.FoodListPage.Name = "FoodListPage";
			this.FoodListPage.Padding = new System.Windows.Forms.Padding(4);
			this.FoodListPage.Size = new System.Drawing.Size(1062, 568);
			this.FoodListPage.TabIndex = 0;
			this.FoodListPage.Text = "Main";
			this.FoodListPage.ToolTipText = "The main menu of the program containing a food list and editing service, //TBD//";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.exactSearchCheckBox);
			this.groupBox1.Controls.Add(this.nextSearchButton);
			this.groupBox1.Controls.Add(this.deleteSelectedFoodItemButton);
			this.groupBox1.Controls.Add(this.clearSearchBarButton);
			this.groupBox1.Controls.Add(this.searchLabel);
			this.groupBox1.Controls.Add(this.searchBar);
			this.groupBox1.Controls.Add(this.foodList);
			this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(581, 281);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Food List";
			// 
			// exactSearchCheckBox
			// 
			this.exactSearchCheckBox.Font = new System.Drawing.Font("Times New Roman", 8.25F);
			this.exactSearchCheckBox.Location = new System.Drawing.Point(8, 245);
			this.exactSearchCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.exactSearchCheckBox.Name = "exactSearchCheckBox";
			this.exactSearchCheckBox.Size = new System.Drawing.Size(76, 25);
			this.exactSearchCheckBox.TabIndex = 5;
			this.exactSearchCheckBox.Text = "Exact";
			this.exactSearchCheckBox.UseVisualStyleBackColor = true;
			this.exactSearchCheckBox.Click += new System.EventHandler(this.FoodListFocusChanged);
			// 
			// nextSearchButton
			// 
			this.nextSearchButton.Enabled = false;
			this.nextSearchButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.nextSearchButton.Location = new System.Drawing.Point(421, 245);
			this.nextSearchButton.Margin = new System.Windows.Forms.Padding(4);
			this.nextSearchButton.Name = "nextSearchButton";
			this.nextSearchButton.Size = new System.Drawing.Size(72, 25);
			this.nextSearchButton.TabIndex = 3;
			this.nextSearchButton.Text = "Next";
			this.nextSearchButton.UseVisualStyleBackColor = true;
			this.nextSearchButton.Click += new System.EventHandler(this.FindNextSearchItem);
			// 
			// deleteSelectedFoodItemButton
			// 
			this.deleteSelectedFoodItemButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.deleteSelectedFoodItemButton.Location = new System.Drawing.Point(23, 214);
			this.deleteSelectedFoodItemButton.Margin = new System.Windows.Forms.Padding(4);
			this.deleteSelectedFoodItemButton.Name = "deleteSelectedFoodItemButton";
			this.deleteSelectedFoodItemButton.Size = new System.Drawing.Size(551, 28);
			this.deleteSelectedFoodItemButton.TabIndex = 1;
			this.deleteSelectedFoodItemButton.Text = "Delete Selected Food Item";
			this.deleteSelectedFoodItemButton.UseVisualStyleBackColor = true;
			this.deleteSelectedFoodItemButton.Click += new System.EventHandler(this.DeleteFoodItemFromTable);
			// 
			// clearSearchBarButton
			// 
			this.clearSearchBarButton.Enabled = false;
			this.clearSearchBarButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.clearSearchBarButton.Location = new System.Drawing.Point(501, 246);
			this.clearSearchBarButton.Margin = new System.Windows.Forms.Padding(4);
			this.clearSearchBarButton.Name = "clearSearchBarButton";
			this.clearSearchBarButton.Size = new System.Drawing.Size(72, 25);
			this.clearSearchBarButton.TabIndex = 4;
			this.clearSearchBarButton.Text = "Clear";
			this.clearSearchBarButton.UseVisualStyleBackColor = true;
			this.clearSearchBarButton.Click += new System.EventHandler(this.ClearSearchBar);
			// 
			// searchLabel
			// 
			this.searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
			this.searchLabel.Location = new System.Drawing.Point(80, 246);
			this.searchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.searchLabel.Name = "searchLabel";
			this.searchLabel.Size = new System.Drawing.Size(77, 25);
			this.searchLabel.TabIndex = 3;
			this.searchLabel.Text = "Search";
			// 
			// searchBar
			// 
			this.searchBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
			this.searchBar.Location = new System.Drawing.Point(152, 246);
			this.searchBar.Margin = new System.Windows.Forms.Padding(4);
			this.searchBar.Name = "searchBar";
			this.searchBar.Size = new System.Drawing.Size(260, 23);
			this.searchBar.TabIndex = 2;
			this.searchBar.Text = "Click Here to Search the Food List";
			this.searchBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.searchBar.TextChanged += new System.EventHandler(this.SearchBarTextChanged);
			this.searchBar.Enter += new System.EventHandler(this.SearchBarFocusGranted);
			this.searchBar.Leave += new System.EventHandler(this.ClearSearchBar);
			// 
			// foodList
			// 
			this.foodList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.foodList.FormattingEnabled = true;
			this.foodList.ItemHeight = 20;
			this.foodList.Location = new System.Drawing.Point(22, 36);
			this.foodList.Margin = new System.Windows.Forms.Padding(4);
			this.foodList.Name = "foodList";
			this.foodList.Size = new System.Drawing.Size(551, 164);
			this.foodList.Sorted = true;
			this.foodList.TabIndex = 0;
			this.foodList.SelectedIndexChanged += new System.EventHandler(this.FoodListSelectedIndexChanged);
			this.foodList.Enter += new System.EventHandler(this.FoodListFocusChanged);
			this.foodList.Leave += new System.EventHandler(this.FoodListFocusChanged);
			// 
			// editFoodPropertiesGroupBox
			// 
			this.editFoodPropertiesGroupBox.Controls.Add(this.isDrinkCheckBox);
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
			this.editFoodPropertiesGroupBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
			this.editFoodPropertiesGroupBox.Location = new System.Drawing.Point(582, 0);
			this.editFoodPropertiesGroupBox.Margin = new System.Windows.Forms.Padding(4);
			this.editFoodPropertiesGroupBox.Name = "editFoodPropertiesGroupBox";
			this.editFoodPropertiesGroupBox.Padding = new System.Windows.Forms.Padding(4);
			this.editFoodPropertiesGroupBox.Size = new System.Drawing.Size(476, 281);
			this.editFoodPropertiesGroupBox.TabIndex = 22;
			this.editFoodPropertiesGroupBox.TabStop = false;
			this.editFoodPropertiesGroupBox.Text = "Properties";
			// 
			// isDrinkCheckBox
			// 
			this.isDrinkCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.isDrinkCheckBox.Location = new System.Drawing.Point(378, 183);
			this.isDrinkCheckBox.Name = "isDrinkCheckBox";
			this.isDrinkCheckBox.Size = new System.Drawing.Size(91, 26);
			this.isDrinkCheckBox.TabIndex = 10;
			this.isDrinkCheckBox.Text = "Is Drink";
			this.isDrinkCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.isDrinkCheckBox.UseVisualStyleBackColor = true;
			// 
			// newItemCheckbox
			// 
			this.newItemCheckbox.Font = new System.Drawing.Font("Times New Roman", 8.25F);
			this.newItemCheckbox.Location = new System.Drawing.Point(400, 218);
			this.newItemCheckbox.Margin = new System.Windows.Forms.Padding(4);
			this.newItemCheckbox.Name = "newItemCheckbox";
			this.newItemCheckbox.Size = new System.Drawing.Size(69, 50);
			this.newItemCheckbox.TabIndex = 12;
			this.newItemCheckbox.Text = "New Item";
			this.newItemCheckbox.UseVisualStyleBackColor = true;
			this.newItemCheckbox.CheckedChanged += new System.EventHandler(this.NewItemCheckboxCheckedChanged);
			// 
			// caloriesPerServingEditBox
			// 
			this.caloriesPerServingEditBox.DecimalPlaces = 2;
			this.caloriesPerServingEditBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
			this.caloriesPerServingEditBox.Location = new System.Drawing.Point(261, 117);
			this.caloriesPerServingEditBox.Margin = new System.Windows.Forms.Padding(4);
			this.caloriesPerServingEditBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.caloriesPerServingEditBox.Name = "caloriesPerServingEditBox";
			this.caloriesPerServingEditBox.Size = new System.Drawing.Size(208, 30);
			this.caloriesPerServingEditBox.TabIndex = 8;
			// 
			// servingSizeEditBox
			// 
			this.servingSizeEditBox.DecimalPlaces = 2;
			this.servingSizeEditBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
			this.servingSizeEditBox.Location = new System.Drawing.Point(13, 118);
			this.servingSizeEditBox.Margin = new System.Windows.Forms.Padding(4);
			this.servingSizeEditBox.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.servingSizeEditBox.Name = "servingSizeEditBox";
			this.servingSizeEditBox.Size = new System.Drawing.Size(224, 30);
			this.servingSizeEditBox.TabIndex = 7;
			// 
			// foodPropertiesButton
			// 
			this.foodPropertiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.foodPropertiesButton.Location = new System.Drawing.Point(13, 218);
			this.foodPropertiesButton.Margin = new System.Windows.Forms.Padding(4);
			this.foodPropertiesButton.Name = "foodPropertiesButton";
			this.foodPropertiesButton.Size = new System.Drawing.Size(379, 53);
			this.foodPropertiesButton.TabIndex = 11;
			this.foodPropertiesButton.Text = "Set Food Item Properties";
			this.foodPropertiesButton.UseVisualStyleBackColor = true;
			this.foodPropertiesButton.Click += new System.EventHandler(this.SetFoodPropertiesButtonClicked);
			// 
			// definerLabel
			// 
			this.definerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			this.definerLabel.Location = new System.Drawing.Point(150, 152);
			this.definerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.definerLabel.Name = "definerLabel";
			this.definerLabel.Size = new System.Drawing.Size(87, 31);
			this.definerLabel.TabIndex = 11;
			this.definerLabel.Text = "Definer";
			this.definerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// definerEditBox
			// 
			this.definerEditBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.definerEditBox.Location = new System.Drawing.Point(13, 183);
			this.definerEditBox.Margin = new System.Windows.Forms.Padding(4);
			this.definerEditBox.Name = "definerEditBox";
			this.definerEditBox.Size = new System.Drawing.Size(358, 26);
			this.definerEditBox.TabIndex = 9;
			// 
			// caloriesPerServingLabel
			// 
			this.caloriesPerServingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			this.caloriesPerServingLabel.Location = new System.Drawing.Point(261, 82);
			this.caloriesPerServingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.caloriesPerServingLabel.Name = "caloriesPerServingLabel";
			this.caloriesPerServingLabel.Size = new System.Drawing.Size(208, 31);
			this.caloriesPerServingLabel.TabIndex = 9;
			this.caloriesPerServingLabel.Text = "Calories Per Serving";
			this.caloriesPerServingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// servingSizeLabel
			// 
			this.servingSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			this.servingSizeLabel.Location = new System.Drawing.Point(55, 84);
			this.servingSizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.servingSizeLabel.Name = "servingSizeLabel";
			this.servingSizeLabel.Size = new System.Drawing.Size(139, 31);
			this.servingSizeLabel.TabIndex = 8;
			this.servingSizeLabel.Text = " Serving Size";
			this.servingSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodNameLabel
			// 
			this.foodNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			this.foodNameLabel.Location = new System.Drawing.Point(217, 20);
			this.foodNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.foodNameLabel.Name = "foodNameLabel";
			this.foodNameLabel.Size = new System.Drawing.Size(73, 25);
			this.foodNameLabel.TabIndex = 7;
			this.foodNameLabel.Text = "Name";
			this.foodNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// foodNameEditBox
			// 
			this.foodNameEditBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.foodNameEditBox.Location = new System.Drawing.Point(13, 52);
			this.foodNameEditBox.Margin = new System.Windows.Forms.Padding(4);
			this.foodNameEditBox.Name = "foodNameEditBox";
			this.foodNameEditBox.Size = new System.Drawing.Size(455, 26);
			this.foodNameEditBox.TabIndex = 6;
			// 
			// MainSubTabControl
			// 
			this.MainSubTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.MainSubTabControl.Controls.Add(this.AddSubButton);
			this.MainSubTabControl.Controls.Add(this.ManualButton);
			this.MainSubTabControl.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.MainSubTabControl.Location = new System.Drawing.Point(0, 289);
			this.MainSubTabControl.Margin = new System.Windows.Forms.Padding(4);
			this.MainSubTabControl.Name = "MainSubTabControl";
			this.MainSubTabControl.SelectedIndex = 0;
			this.MainSubTabControl.Size = new System.Drawing.Size(1062, 279);
			this.MainSubTabControl.TabIndex = 13;
			this.MainSubTabControl.SelectedIndexChanged += new System.EventHandler(this.ChangedTabManualAddSub);
			// 
			// AddSubButton
			// 
			this.AddSubButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AddSubButton.Controls.Add(this.AddSub_SubTabControl);
			this.AddSubButton.Controls.Add(this.WriteToFileCheckBox);
			this.AddSubButton.Controls.Add(this.RecordFoodCheckBox);
			this.AddSubButton.Controls.Add(this.howManyServingsLabel);
			this.AddSubButton.Controls.Add(this.addCaloriesButtonMain);
			this.AddSubButton.Controls.Add(this.subtractCaloriesButton);
			this.AddSubButton.Location = new System.Drawing.Point(4, 31);
			this.AddSubButton.Margin = new System.Windows.Forms.Padding(4);
			this.AddSubButton.Name = "AddSubButton";
			this.AddSubButton.Padding = new System.Windows.Forms.Padding(4);
			this.AddSubButton.Size = new System.Drawing.Size(1054, 244);
			this.AddSubButton.TabIndex = 0;
			this.AddSubButton.Text = "Add/Subtract";
			this.AddSubButton.UseVisualStyleBackColor = true;
			// 
			// AddSub_SubTabControl
			// 
			this.AddSub_SubTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.AddSub_SubTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.AddSub_SubTabControl.Controls.Add(this.addSub_SubTabArithmetic);
			this.AddSub_SubTabControl.Controls.Add(this.addSub_SubTabManual);
			this.AddSub_SubTabControl.Location = new System.Drawing.Point(8, 48);
			this.AddSub_SubTabControl.Multiline = true;
			this.AddSub_SubTabControl.Name = "AddSub_SubTabControl";
			this.AddSub_SubTabControl.SelectedIndex = 0;
			this.AddSub_SubTabControl.Size = new System.Drawing.Size(889, 90);
			this.AddSub_SubTabControl.TabIndex = 0;
			// 
			// addSub_SubTabArithmetic
			// 
			this.addSub_SubTabArithmetic.BackColor = System.Drawing.SystemColors.Control;
			this.addSub_SubTabArithmetic.Controls.Add(this.arithmeticNumericUpDown_Right);
			this.addSub_SubTabArithmetic.Controls.Add(this.arithmeticSignComboBox);
			this.addSub_SubTabArithmetic.Controls.Add(this.arithmeticNumericUpDown_Left);
			this.addSub_SubTabArithmetic.Location = new System.Drawing.Point(68, 4);
			this.addSub_SubTabArithmetic.Name = "addSub_SubTabArithmetic";
			this.addSub_SubTabArithmetic.Padding = new System.Windows.Forms.Padding(3);
			this.addSub_SubTabArithmetic.Size = new System.Drawing.Size(817, 82);
			this.addSub_SubTabArithmetic.TabIndex = 0;
			this.addSub_SubTabArithmetic.Text = "Arithmetic";
			// 
			// arithmeticNumericUpDown_Right
			// 
			this.arithmeticNumericUpDown_Right.DecimalPlaces = 1;
			this.arithmeticNumericUpDown_Right.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold);
			this.arithmeticNumericUpDown_Right.Location = new System.Drawing.Point(718, 11);
			this.arithmeticNumericUpDown_Right.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.arithmeticNumericUpDown_Right.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			131072});
			this.arithmeticNumericUpDown_Right.Name = "arithmeticNumericUpDown_Right";
			this.arithmeticNumericUpDown_Right.Size = new System.Drawing.Size(90, 65);
			this.arithmeticNumericUpDown_Right.TabIndex = 2;
			this.arithmeticNumericUpDown_Right.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.arithmeticNumericUpDown_Right.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// arithmeticSignComboBox
			// 
			this.arithmeticSignComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.arithmeticSignComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.arithmeticSignComboBox.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold);
			this.arithmeticSignComboBox.FormattingEnabled = true;
			this.arithmeticSignComboBox.Items.AddRange(new object[] {
			"/",
			"*",
			"-",
			"+"});
			this.arithmeticSignComboBox.Location = new System.Drawing.Point(648, 11);
			this.arithmeticSignComboBox.MaxDropDownItems = 4;
			this.arithmeticSignComboBox.Name = "arithmeticSignComboBox";
			this.arithmeticSignComboBox.Size = new System.Drawing.Size(64, 65);
			this.arithmeticSignComboBox.TabIndex = 1;
			// 
			// arithmeticNumericUpDown_Left
			// 
			this.arithmeticNumericUpDown_Left.DecimalPlaces = 9;
			this.arithmeticNumericUpDown_Left.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.arithmeticNumericUpDown_Left.Location = new System.Drawing.Point(6, 11);
			this.arithmeticNumericUpDown_Left.Maximum = new decimal(new int[] {
			0,
			902409669,
			54,
			0});
			this.arithmeticNumericUpDown_Left.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			589824});
			this.arithmeticNumericUpDown_Left.Name = "arithmeticNumericUpDown_Left";
			this.arithmeticNumericUpDown_Left.Size = new System.Drawing.Size(636, 65);
			this.arithmeticNumericUpDown_Left.TabIndex = 0;
			this.arithmeticNumericUpDown_Left.Tag = "";
			this.arithmeticNumericUpDown_Left.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.arithmeticNumericUpDown_Left.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// addSub_SubTabManual
			// 
			this.addSub_SubTabManual.BackColor = System.Drawing.SystemColors.Control;
			this.addSub_SubTabManual.Controls.Add(this.userServingInputTextBox);
			this.addSub_SubTabManual.Location = new System.Drawing.Point(68, 4);
			this.addSub_SubTabManual.Name = "addSub_SubTabManual";
			this.addSub_SubTabManual.Padding = new System.Windows.Forms.Padding(3);
			this.addSub_SubTabManual.Size = new System.Drawing.Size(817, 82);
			this.addSub_SubTabManual.TabIndex = 1;
			this.addSub_SubTabManual.Tag = "";
			this.addSub_SubTabManual.Text = "Explicit";
			// 
			// userServingInputTextBox
			// 
			this.userServingInputTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.userServingInputTextBox.DecimalPlaces = 9;
			this.userServingInputTextBox.Font = new System.Drawing.Font("Times New Roman", 28.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.userServingInputTextBox.Location = new System.Drawing.Point(7, 12);
			this.userServingInputTextBox.Margin = new System.Windows.Forms.Padding(4);
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
			this.userServingInputTextBox.Size = new System.Drawing.Size(803, 63);
			this.userServingInputTextBox.TabIndex = 31;
			this.userServingInputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.userServingInputTextBox.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// WriteToFileCheckBox
			// 
			this.WriteToFileCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.WriteToFileCheckBox.Checked = true;
			this.WriteToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.WriteToFileCheckBox.Enabled = false;
			this.WriteToFileCheckBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
			this.WriteToFileCheckBox.Location = new System.Drawing.Point(907, 102);
			this.WriteToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.WriteToFileCheckBox.Name = "WriteToFileCheckBox";
			this.WriteToFileCheckBox.Size = new System.Drawing.Size(128, 32);
			this.WriteToFileCheckBox.TabIndex = 26;
			this.WriteToFileCheckBox.Text = "Write to File?";
			this.WriteToFileCheckBox.UseVisualStyleBackColor = true;
			// 
			// RecordFoodCheckBox
			// 
			this.RecordFoodCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.RecordFoodCheckBox.Checked = true;
			this.RecordFoodCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.RecordFoodCheckBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
			this.RecordFoodCheckBox.Location = new System.Drawing.Point(907, 52);
			this.RecordFoodCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.RecordFoodCheckBox.Name = "RecordFoodCheckBox";
			this.RecordFoodCheckBox.Size = new System.Drawing.Size(129, 31);
			this.RecordFoodCheckBox.TabIndex = 25;
			this.RecordFoodCheckBox.Text = "Record?";
			this.RecordFoodCheckBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.RecordFoodCheckBox.UseVisualStyleBackColor = true;
			// 
			// howManyServingsLabel
			// 
			this.howManyServingsLabel.BackColor = System.Drawing.SystemColors.Control;
			this.howManyServingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
			this.howManyServingsLabel.Location = new System.Drawing.Point(8, 4);
			this.howManyServingsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.howManyServingsLabel.Name = "howManyServingsLabel";
			this.howManyServingsLabel.Size = new System.Drawing.Size(1027, 41);
			this.howManyServingsLabel.TabIndex = 29;
			this.howManyServingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// addCaloriesButtonMain
			// 
			this.addCaloriesButtonMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.addCaloriesButtonMain.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.addCaloriesButtonMain.Location = new System.Drawing.Point(8, 194);
			this.addCaloriesButtonMain.Margin = new System.Windows.Forms.Padding(4);
			this.addCaloriesButtonMain.Name = "addCaloriesButtonMain";
			this.addCaloriesButtonMain.Size = new System.Drawing.Size(1027, 41);
			this.addCaloriesButtonMain.TabIndex = 28;
			this.addCaloriesButtonMain.Text = "Add selected item\'s calories to your daily calorie allowance";
			this.addCaloriesButtonMain.UseVisualStyleBackColor = true;
			this.addCaloriesButtonMain.Click += new System.EventHandler(this.ModifyCalories);
			// 
			// subtractCaloriesButton
			// 
			this.subtractCaloriesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.subtractCaloriesButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.subtractCaloriesButton.Location = new System.Drawing.Point(8, 142);
			this.subtractCaloriesButton.Margin = new System.Windows.Forms.Padding(4);
			this.subtractCaloriesButton.Name = "subtractCaloriesButton";
			this.subtractCaloriesButton.Size = new System.Drawing.Size(1027, 44);
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
			this.ManualButton.Location = new System.Drawing.Point(4, 31);
			this.ManualButton.Margin = new System.Windows.Forms.Padding(4);
			this.ManualButton.Name = "ManualButton";
			this.ManualButton.Padding = new System.Windows.Forms.Padding(4);
			this.ManualButton.Size = new System.Drawing.Size(1054, 244);
			this.ManualButton.TabIndex = 1;
			this.ManualButton.Text = "Manual";
			this.ManualButton.UseVisualStyleBackColor = true;
			// 
			// manualZeroOutButton
			// 
			this.manualZeroOutButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
			this.manualZeroOutButton.Location = new System.Drawing.Point(535, 138);
			this.manualZeroOutButton.Margin = new System.Windows.Forms.Padding(4);
			this.manualZeroOutButton.Name = "manualZeroOutButton";
			this.manualZeroOutButton.Size = new System.Drawing.Size(503, 49);
			this.manualZeroOutButton.TabIndex = 9;
			this.manualZeroOutButton.Text = "Zero Out Calories";
			this.manualZeroOutButton.UseVisualStyleBackColor = true;
			this.manualZeroOutButton.Click += new System.EventHandler(this.ZeroOutCaloriesButtonClicked);
			// 
			// manualResetButton
			// 
			this.manualResetButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
			this.manualResetButton.Location = new System.Drawing.Point(8, 138);
			this.manualResetButton.Margin = new System.Windows.Forms.Padding(4);
			this.manualResetButton.Name = "manualResetButton";
			this.manualResetButton.Size = new System.Drawing.Size(519, 49);
			this.manualResetButton.TabIndex = 8;
			this.manualResetButton.Text = "Reset Calories To Default";
			this.manualResetButton.UseVisualStyleBackColor = true;
			this.manualResetButton.Click += new System.EventHandler(this.ResetCaloriesButtonClicked);
			// 
			// manualSubmitButton
			// 
			this.manualSubmitButton.Font = new System.Drawing.Font("Bookman Old Style", 18F, System.Drawing.FontStyle.Bold);
			this.manualSubmitButton.Location = new System.Drawing.Point(8, 194);
			this.manualSubmitButton.Margin = new System.Windows.Forms.Padding(4);
			this.manualSubmitButton.Name = "manualSubmitButton";
			this.manualSubmitButton.Size = new System.Drawing.Size(1029, 41);
			this.manualSubmitButton.TabIndex = 7;
			this.manualSubmitButton.Text = "Submit";
			this.manualSubmitButton.UseVisualStyleBackColor = true;
			this.manualSubmitButton.Click += new System.EventHandler(this.ManualSubmitButtonClicked);
			// 
			// ManualFoodLabel
			// 
			this.ManualFoodLabel.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold);
			this.ManualFoodLabel.Location = new System.Drawing.Point(8, 4);
			this.ManualFoodLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ManualFoodLabel.Name = "ManualFoodLabel";
			this.ManualFoodLabel.Size = new System.Drawing.Size(1029, 59);
			this.ManualFoodLabel.TabIndex = 6;
			this.ManualFoodLabel.Text = "Enter the amount of calories you want to set your daily allowance to in the box b" +
	"elow";
			this.ManualFoodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// manualCalorieEditBox
			// 
			this.manualCalorieEditBox.DecimalPlaces = 4;
			this.manualCalorieEditBox.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold);
			this.manualCalorieEditBox.Increment = new decimal(new int[] {
			1,
			0,
			0,
			262144});
			this.manualCalorieEditBox.Location = new System.Drawing.Point(18, 66);
			this.manualCalorieEditBox.Margin = new System.Windows.Forms.Padding(4);
			this.manualCalorieEditBox.Maximum = new decimal(new int[] {
			0,
			434162106,
			542,
			0});
			this.manualCalorieEditBox.Name = "manualCalorieEditBox";
			this.manualCalorieEditBox.Size = new System.Drawing.Size(1019, 63);
			this.manualCalorieEditBox.TabIndex = 5;
			this.manualCalorieEditBox.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			// 
			// refreshCaloriesTimeButton
			// 
			this.refreshCaloriesTimeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refreshCaloriesTimeButton.BackgroundImage")));
			this.refreshCaloriesTimeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.refreshCaloriesTimeButton.Location = new System.Drawing.Point(1014, 19);
			this.refreshCaloriesTimeButton.Margin = new System.Windows.Forms.Padding(4);
			this.refreshCaloriesTimeButton.Name = "refreshCaloriesTimeButton";
			this.refreshCaloriesTimeButton.Size = new System.Drawing.Size(43, 33);
			this.refreshCaloriesTimeButton.TabIndex = 2;
			this.refreshCaloriesTimeButton.UseVisualStyleBackColor = true;
			this.refreshCaloriesTimeButton.Click += new System.EventHandler(this.TimeOrCaloriesChangedWithoutEvent);
			// 
			// timeRadioButton
			// 
			this.timeRadioButton.Font = new System.Drawing.Font("Times New Roman", 8.25F);
			this.timeRadioButton.Location = new System.Drawing.Point(11, 38);
			this.timeRadioButton.Margin = new System.Windows.Forms.Padding(4);
			this.timeRadioButton.Name = "timeRadioButton";
			this.timeRadioButton.Size = new System.Drawing.Size(84, 21);
			this.timeRadioButton.TabIndex = 1;
			this.timeRadioButton.TabStop = true;
			this.timeRadioButton.Text = "Time";
			this.timeRadioButton.UseVisualStyleBackColor = true;
			this.timeRadioButton.CheckedChanged += new System.EventHandler(this.TimeOrCaloriesChangedWithoutEvent);
			// 
			// calorieRadioButton
			// 
			this.calorieRadioButton.Checked = true;
			this.calorieRadioButton.Font = new System.Drawing.Font("Times New Roman", 8.25F);
			this.calorieRadioButton.Location = new System.Drawing.Point(11, 13);
			this.calorieRadioButton.Margin = new System.Windows.Forms.Padding(4);
			this.calorieRadioButton.Name = "calorieRadioButton";
			this.calorieRadioButton.Size = new System.Drawing.Size(84, 21);
			this.calorieRadioButton.TabIndex = 0;
			this.calorieRadioButton.TabStop = true;
			this.calorieRadioButton.Text = "Calories";
			this.calorieRadioButton.UseVisualStyleBackColor = true;
			this.calorieRadioButton.CheckedChanged += new System.EventHandler(this.TimeOrCaloriesChangedWithoutEvent);
			// 
			// caloriesLabel
			// 
			this.caloriesLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.caloriesLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.caloriesLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.caloriesLabel.Location = new System.Drawing.Point(103, 13);
			this.caloriesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.caloriesLabel.Name = "caloriesLabel";
			this.caloriesLabel.Size = new System.Drawing.Size(903, 46);
			this.caloriesLabel.TabIndex = 100;
			this.caloriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabsMenu
			// 
			this.tabsMenu.Controls.Add(this.FoodListPage);
			this.tabsMenu.Controls.Add(this.planTab);
			this.tabsMenu.Controls.Add(this.additionalOptionsTab);
			this.tabsMenu.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold);
			this.tabsMenu.HotTrack = true;
			this.tabsMenu.Location = new System.Drawing.Point(4, 63);
			this.tabsMenu.Margin = new System.Windows.Forms.Padding(4);
			this.tabsMenu.Multiline = true;
			this.tabsMenu.Name = "tabsMenu";
			this.tabsMenu.SelectedIndex = 0;
			this.tabsMenu.ShowToolTips = true;
			this.tabsMenu.Size = new System.Drawing.Size(1070, 600);
			this.tabsMenu.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tabsMenu.TabIndex = 3;
			// 
			// additionalOptionsTab
			// 
			this.additionalOptionsTab.BackColor = System.Drawing.SystemColors.Control;
			this.additionalOptionsTab.Controls.Add(this.defaultCaloriesGroupBox);
			this.additionalOptionsTab.Controls.Add(this.resetCaloriesSpecificGroupBox);
			this.additionalOptionsTab.Controls.Add(this.label3);
			this.additionalOptionsTab.Controls.Add(this.Seperator5);
			this.additionalOptionsTab.Location = new System.Drawing.Point(4, 28);
			this.additionalOptionsTab.Margin = new System.Windows.Forms.Padding(4);
			this.additionalOptionsTab.Name = "additionalOptionsTab";
			this.additionalOptionsTab.Padding = new System.Windows.Forms.Padding(4);
			this.additionalOptionsTab.Size = new System.Drawing.Size(1062, 568);
			this.additionalOptionsTab.TabIndex = 3;
			this.additionalOptionsTab.Text = "Additional Options";
			// 
			// defaultCaloriesGroupBox
			// 
			this.defaultCaloriesGroupBox.Controls.Add(this.defaultCaloriesSetButton);
			this.defaultCaloriesGroupBox.Controls.Add(this.defaultCaloriesNumericUpDown);
			this.defaultCaloriesGroupBox.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Bold);
			this.defaultCaloriesGroupBox.Location = new System.Drawing.Point(504, 7);
			this.defaultCaloriesGroupBox.Margin = new System.Windows.Forms.Padding(4);
			this.defaultCaloriesGroupBox.Name = "defaultCaloriesGroupBox";
			this.defaultCaloriesGroupBox.Padding = new System.Windows.Forms.Padding(4);
			this.defaultCaloriesGroupBox.Size = new System.Drawing.Size(192, 59);
			this.defaultCaloriesGroupBox.TabIndex = 38;
			this.defaultCaloriesGroupBox.TabStop = false;
			this.defaultCaloriesGroupBox.Text = "Default Calories";
			// 
			// defaultCaloriesSetButton
			// 
			this.defaultCaloriesSetButton.Location = new System.Drawing.Point(113, 22);
			this.defaultCaloriesSetButton.Margin = new System.Windows.Forms.Padding(4);
			this.defaultCaloriesSetButton.Name = "defaultCaloriesSetButton";
			this.defaultCaloriesSetButton.Size = new System.Drawing.Size(65, 26);
			this.defaultCaloriesSetButton.TabIndex = 1;
			this.defaultCaloriesSetButton.Text = "Set";
			this.defaultCaloriesSetButton.UseVisualStyleBackColor = true;
			this.defaultCaloriesSetButton.Click += new System.EventHandler(this.DefaultCaloriesSetButtonClick);
			// 
			// defaultCaloriesNumericUpDown
			// 
			this.defaultCaloriesNumericUpDown.DecimalPlaces = 2;
			this.defaultCaloriesNumericUpDown.Location = new System.Drawing.Point(8, 22);
			this.defaultCaloriesNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
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
			this.defaultCaloriesNumericUpDown.Size = new System.Drawing.Size(97, 25);
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
			this.resetCaloriesSpecificGroupBox.Location = new System.Drawing.Point(8, 7);
			this.resetCaloriesSpecificGroupBox.Margin = new System.Windows.Forms.Padding(4);
			this.resetCaloriesSpecificGroupBox.Name = "resetCaloriesSpecificGroupBox";
			this.resetCaloriesSpecificGroupBox.Padding = new System.Windows.Forms.Padding(4);
			this.resetCaloriesSpecificGroupBox.Size = new System.Drawing.Size(504, 59);
			this.resetCaloriesSpecificGroupBox.TabIndex = 37;
			this.resetCaloriesSpecificGroupBox.TabStop = false;
			this.resetCaloriesSpecificGroupBox.Text = "Reset Calories At a Specific Time";
			// 
			// exactResetDatetimePicker
			// 
			this.exactResetDatetimePicker.CalendarFont = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exactResetDatetimePicker.CustomFormat = "MMMMM dd yyyy hh:mm:ss tt";
			this.exactResetDatetimePicker.Enabled = false;
			this.exactResetDatetimePicker.Font = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exactResetDatetimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.exactResetDatetimePicker.Location = new System.Drawing.Point(128, 20);
			this.exactResetDatetimePicker.Margin = new System.Windows.Forms.Padding(4);
			this.exactResetDatetimePicker.Name = "exactResetDatetimePicker";
			this.exactResetDatetimePicker.Size = new System.Drawing.Size(359, 27);
			this.exactResetDatetimePicker.TabIndex = 36;
			this.exactResetDatetimePicker.ValueChanged += new System.EventHandler(this.ExactResetDatetimePickerValueChanged);
			// 
			// resetCaloriesManualCheckBox
			// 
			this.resetCaloriesManualCheckBox.Location = new System.Drawing.Point(20, 21);
			this.resetCaloriesManualCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.resetCaloriesManualCheckBox.Name = "resetCaloriesManualCheckBox";
			this.resetCaloriesManualCheckBox.Size = new System.Drawing.Size(113, 30);
			this.resetCaloriesManualCheckBox.TabIndex = 0;
			this.resetCaloriesManualCheckBox.Text = "Enabled";
			this.resetCaloriesManualCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.resetCaloriesManualCheckBox.UseVisualStyleBackColor = true;
			this.resetCaloriesManualCheckBox.CheckedChanged += new System.EventHandler(this.ResetCaloriesSpecificCheckBoxCheckStatusChanged);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Bookman Old Style", 24F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(0, 70);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(1060, 494);
			this.label3.TabIndex = 35;
			this.label3.Text = "UNFINISHED";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Seperator5
			// 
			this.Seperator5.Location = new System.Drawing.Point(8, 4);
			this.Seperator5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Seperator5.Name = "Seperator5";
			this.Seperator5.Size = new System.Drawing.Size(1044, 10);
			this.Seperator5.TabIndex = 33;
			this.Seperator5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(1077, 667);
			this.Controls.Add(this.tabsMenu);
			this.Controls.Add(this.calorieRadioButton);
			this.Controls.Add(this.caloriesLabel);
			this.Controls.Add(this.refreshCaloriesTimeButton);
			this.Controls.Add(this.timeRadioButton);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Weight Watching Program";
			this.planTab.ResumeLayout(false);
			this.FoodListPage.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.editFoodPropertiesGroupBox.ResumeLayout(false);
			this.editFoodPropertiesGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.caloriesPerServingEditBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.servingSizeEditBox)).EndInit();
			this.MainSubTabControl.ResumeLayout(false);
			this.AddSubButton.ResumeLayout(false);
			this.AddSub_SubTabControl.ResumeLayout(false);
			this.addSub_SubTabArithmetic.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.arithmeticNumericUpDown_Right)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.arithmeticNumericUpDown_Left)).EndInit();
			this.addSub_SubTabManual.ResumeLayout(false);
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
