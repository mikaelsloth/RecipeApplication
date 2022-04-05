namespace Recipe.Views
{
    using Recipe.Models.Db;
    using Recipe.Presenter;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class RecipeCardBrowsePartView : UserControl, IRecipeCardBrowsePartView
    {
        private IRecipeCardBrowsePartPresenter? presenter;

        public IRecipeCardBrowsePartPresenter Presenter
        { 
            get => presenter ?? throw new ArgumentNullException(nameof(Presenter));
            set => presenter = value; 
        }

        public RecipeCardBrowsePartView()
        {
            InitializeComponent();
            InitializeNavigatorControls();
            InitializeBindingSourceNavigator();
        }

        #region Initializers

        private static ModernButton GetButton(string name) => new()
        {
            BackColor = SystemColors.Window,
            BorderColor = Color.Red,
            BorderRadius = 6,
            BorderSize = 1,
            FlatStyle = FlatStyle.Flat,
            ForeColor = SystemColors.Window,
            Name = name,
            Size = new(50, 28),
            Text = "",
            UseVisualStyleBackColor = false,
        };

        private static ModernTextBox GetTextBox(string name) => new()
        {
            BackColor = SystemColors.Window,
            BorderColor = Color.Red,
            BorderFocusColor = SystemColors.ActiveBorder,
            BorderRadius = 6,
            BorderSize = 1,
            Margin = new(0),
            Multiline = false,
            Name = name,
            Padding = new(3),
            Size = new(50, 28),
            Text = "",
            TextAlign = HorizontalAlignment.Center,
            BorderStyle = ExtendedBorderStyle.None,
        };

        private static ModernLabel GetLabel(string name, string text) => new()
        {
            BackColor = SystemColors.Window,
            BorderColor = Color.Red,
            BorderRadius = 6,
            BorderSize = 1,
            Name = name,
            Size = new(50, 28),
            Text = text,
            TextAlign = ContentAlignment.MiddleCenter,
        };

        private void InitializeNavigatorControls()
        {
            // 
            // GoToNextButton
            // 
            GoToNextButton = GetButton("GoToNextButton");
            GoToNextButton.FlatAppearance.BorderSize = 0;
            GoToNextButton.Image = GoToNextButton.Parent.BackColor.GetBrightness() >= 0.8F
                ? Properties.Resources.Next_16x16_Black
                : Properties.Resources.Next_16x16_White;
            // 
            // GotoFirstButton
            // 
            GotoFirstButton = GetButton("GotoFirstButton");
            GotoFirstButton.FlatAppearance.BorderSize = 0;
            GotoFirstButton.Image = GotoFirstButton.Parent.BackColor.GetBrightness() >= 0.8F
                ? Properties.Resources.GoToFirstRow_16x16_Black
                : Properties.Resources.GoToFirstRow_16x16_White;
            // 
            // GoToLastButton
            // 
            GoToLastButton = GetButton("GoToLastButton");
            GoToLastButton.FlatAppearance.BorderSize = 0;
            GoToLastButton.Image = GoToLastButton.Parent.BackColor.GetBrightness() >= 0.8F
                ? Properties.Resources.GoToLastRow_16x16_Black
                : Properties.Resources.GoToLastRow_16x16_White;
            // 
            // GoToPreviousButton
            // 
            GoToPreviousButton = GetButton("GoToPreviousButton");
            GoToPreviousButton.FlatAppearance.BorderSize = 0;
            GoToPreviousButton.Image = GoToPreviousButton.Parent.BackColor.GetBrightness() >= 0.8F
                ? Properties.Resources.Previous_16x16_Black
                : Properties.Resources.Previous_16x16_White;
            // 
            // AddNewButton
            // 
            AddNewButton = GetButton("AddNewButton");
            AddNewButton.FlatAppearance.BorderSize = 0;
            AddNewButton.Image = AddNewButton.Parent.BackColor.GetBrightness() >= 0.8F
                ? Properties.Resources.Add_thin_16x16_Black
                : Properties.Resources.Add_thin_16x16_White;
            // 
            // DeleteButton
            // 
            DeleteButton = GetButton("DeleteButton");
            DeleteButton.FlatAppearance.BorderSize = 0;
            DeleteButton.Image = DeleteButton.Parent.BackColor.GetBrightness() >= 0.8F
                ? Properties.Resources.Remove_thin_16x_16x_Black
                : Properties.Resources.Remove_thin_16x_16x_White;
            //
            // PositionTextBox
            //
            PositionTextBox = GetTextBox("PositionTextBox");
            //
            // CountTextLabel
            //
            CountTextLabel = GetLabel("CountTextLabel", "af");
            CountTextLabel.Font = new Font(Font.FontFamily, 9, GraphicsUnit.Point);
            //
            // CountLabel
            //
            CountLabel = GetLabel("CountLabel", "");
        }

        private void InitializeBindingSourceNavigator()
        {
            BrowseBindingNavigator.BindingSource = BrowseBindingSource;
            // Adding controls
            BrowseBindingNavigator.MoveFirstItem = GotoFirstButton;
            BrowseBindingNavigator.MoveNextItem = GoToNextButton;
            BrowseBindingNavigator.MovePreviousItem = GoToPreviousButton;
            BrowseBindingNavigator.MoveLastItem = GoToLastButton;
            BrowseBindingNavigator.AddNewItem = AddNewButton;
            BrowseBindingNavigator.DeleteItem = DeleteButton;
            BrowseBindingNavigator.CountItem = CountLabel;
            BrowseBindingNavigator.PositionTextItem = CountTextLabel;
            BrowseBindingNavigator.PositionItem = PositionTextBox;
        }

        #endregion

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Dispose(true);

        public void SetDataSource(IList<RecipeCard> recipes) => BrowseBindingSource.DataSource = recipes;

        public event EventHandler? PositionChanged;

        public event AddingNewEventHandler? AddingNew;

        private void BrowseBindingSource_PositionChanged(object sender, EventArgs e) => PositionChanged?.Invoke(sender, e);

        private void BrowseBindingSource_AddingNew(object sender, AddingNewEventArgs e) => AddingNew?.Invoke(sender, e);

        public void SetCustomerName(string name) => CustomerNameLabel.Text = name;

        public void SetRecipeCardDate(DateTime date) => DateModernLabel.Text = date.ToLongDateString();

        #region Design Features

        public event EventHandler? ExpandedChanged;

        public bool Expanded
        {
            get => ExpandButton.Expanded;
            set => ExpandButton.Expanded = value;
        }

        protected virtual void OnExpandedChanged(EventArgs e)
        {
            if(Expanded)
            {
                Height = 160;
                BrowseBindingNavigator.Visible = true;
            }
            else
            {
                Height = 95;
                BrowseBindingNavigator.Visible = false;
            }

            ExpandedChanged?.Invoke(this, e);
        }

        private void ExpandButton_Click(object sender, EventArgs e) => OnExpandedChanged(e);

        #endregion
    }
}
