namespace Recipe.Views
{
    using Recipe.Models.Db;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;

    public partial class RecipeLineAddEditView : UserControl, IRecipeLineView
    {
        //Backing fields
        private Color gridColor;
        private Color buttonBackColor;
        private RecipeLineState state = RecipeLineState.NotDefined;
        private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;
        private const AutoCompleteMode ON = AutoCompleteMode.SuggestAppend;
        private const AutoCompleteMode OFF = AutoCompleteMode.None;

        private bool textboxesInitialized = false;

        public RecipeLineAddEditView() : this(true)
        { }

        public RecipeLineAddEditView(bool addStandardItems)
        {
            gridColor = BackColor;
            buttonBackColor = BackColor;

            InitializeComponent();
            if (addStandardItems) CreateStandardLayoutPanel();
        }

        private void CreateStandardLayoutPanel() => InitializeLayoutPanel(GetStandardColumns(), true);

        private void CreateStandardLayoutPanel(RecipeLineState previous_state)
        {
            if (previous_state == RecipeLineState.EditMode) CleanUpEditMode();
            if (previous_state == RecipeLineState.AddMode) CleanUpAddMode();
            CreateStandardLayoutPanel();
        }

        private static IList<ColumnStyle> GetStandardColumns() => new List<ColumnStyle>()
            {
                new(SizeType.Percent, 75F),
                new(SizeType.Percent, 25F),
                new(SizeType.Absolute, 55F),
                new(SizeType.Absolute, 55F),
                new(SizeType.Absolute, 55F),
                new(SizeType.Absolute, 55F),
                new(SizeType.Absolute, 55F),
            };

        protected virtual void InitializeLayoutPanel(IList<ColumnStyle> columnStyles, bool addStandardTextboxes)
        {
            if (addStandardTextboxes && !textboxesInitialized) textboxesInitialized = InitializeTextBoxes();

            LayoutPanel.SuspendLayout();
            LayoutPanel.Controls.Clear();
            LayoutPanel.ColumnStyles.Clear();
            LayoutPanel.ColumnCount = columnStyles.Count;
            for (int i = 0; i < columnStyles.Count; i++)
            {
                _ = LayoutPanel.ColumnStyles.Add(columnStyles[i]);
            }

            if (addStandardTextboxes)
            {
                LayoutPanel.Controls.Add(DescTextBox, 0, 0);
                LayoutPanel.Controls.Add(UnitComboBox, 1, 0);
                LayoutPanel.Controls.Add(MorningTextBox, 2, 0);
                LayoutPanel.Controls.Add(NoonTextBox, 3, 0);
                LayoutPanel.Controls.Add(EveningTextBox, 4, 0);
                LayoutPanel.Controls.Add(NightTextBox, 5, 0);
            }

            LayoutPanel.ResumeLayout(true);
        }

        protected bool InitializeTextBoxes()
        {
            // 
            // DescTextBox
            // 
            DescTextBox = CreateTextBox(nameof(DescTextBox), "MedicinName", 0);
            // 
            // UnitTextBox
            // 
            UnitComboBox = CreateComboBox(nameof(UnitComboBox), null, "Units.UnitName", 1);
            // 
            // MorningTextBox
            // 
            MorningTextBox = CreateTextBox(nameof(MorningTextBox), "Morning", 2);
            // 
            // NoonTextBox
            // 
            NoonTextBox = CreateTextBox(nameof(NoonTextBox), "Noon", 3);
            // 
            // EveningTextBox
            // 
            EveningTextBox = CreateTextBox(nameof(EveningTextBox), "Evening", 4);
            // 
            // NightTextBox
            // 
            NightTextBox = CreateTextBox(nameof(NightTextBox), "Midnight", 5);

            return true;
        }

        private ModernComboBox CreateComboBox(string name, object? dataSource, object? tagName, int tabIndex)
        {
            ModernComboBox combo = new()
            {
                DropDownStyle = ComboBoxStyle.DropDown,
                Dock = DockStyle.Fill,
                Font = new("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                Name = name,
                TabIndex = tabIndex,
                Text = "",
                DataSource = dataSource,
                ValueMember = "Id",
                DisplayMember = "UnitName",
                BackColor = BackColor,
                ForeColor = ForeColor,
                Tag = tagName,
            };
            combo.TextChanged += TextBox_Changed;
            return combo;
        }

        private ModernButton CreateButton(string name, Image darkImage, Image lightImage, EventHandler del)
        {
            ModernButton button = new()
            {
                BackColor = ButtonBackColor,
                FlatStyle = FlatStyle.Flat,
                ForeColor = ForeColor,
                Dock = DockStyle.Right,
                Name = name,
                Size = new(50, 50),
                UseVisualStyleBackColor = true,
            };
            button.FlatAppearance.BorderSize = 0;
            button.Image = button.BackColor.GetBrightness() switch
            {
                > 0.8F => darkImage,
                _ => lightImage,
            };
            button.Click += del;
            return button;
        }

        private ModernTextBox CreateTextBox(string name, object? tagName, int tabIndex)
        {
            ModernTextBox textBox = new()
            {
                BackColor = BackColor,
                Dock = DockStyle.Fill,
                Font = new("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new(3),
                Name = name,
                Padding = new(2, 6, 2, 6),
                Tag = tagName,
                TabIndex = tabIndex,
                ForeColor = ForeColor
            };
            textBox.TextChanged += TextBox_Changed;
            return textBox;
        }

        protected virtual void TextBox_Changed(object? sender, EventArgs e) => State = RecipeLineState.EditMode;

        private void Save_Clicked(object? sender, EventArgs e) => OnSaveClicked(e);

        protected virtual void OnSaveClicked(EventArgs e) => SaveClicked?.Invoke(this, e);

        public event EventHandler? SaveClicked;

        private void Cancel_Clicked(object? sender, EventArgs e) => OnCancelClicked(e);

        protected virtual void OnCancelClicked(EventArgs e) => CancelClicked?.Invoke(this, e);

        public event EventHandler? CancelClicked;

        private void Add_Clicked(object? sender, EventArgs e) => OnAddClicked(e);

        protected virtual void OnAddClicked(EventArgs e) => AddClicked?.Invoke(this, e);

        public event EventHandler? AddClicked;

        private void SetGridColorAndPaint()
        {
            if (gridColor != BackColor) LayoutPanel.CellPaint += LayoutPanel_CellPaint;
            else LayoutPanel.CellPaint -= LayoutPanel_CellPaint;
            Invalidate();
        }

        private void SetButtonColorAndPaint()
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernButton bt) bt.BackColor = ButtonBackColor;
            }
        }

        private void LayoutPanel_CellPaint(object? sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column < 6)
            {
                using Pen pen = new(gridColor, 0.5F);
                e.Graphics.DrawRectangle(pen, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 0.5F);
            }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox tb) tb.ForeColor = ForeColor;
                if (item is ModernComboBox cb) cb.ForeColor = ForeColor;
            }

            base.OnForeColorChanged(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox tb) tb.BackColor = BackColor;
                if (item is ModernComboBox cb) cb.BackColor = BackColor;
            }

            base.OnBackColorChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox tb) tb.Font = Font;
                if (item is ModernComboBox cb) cb.Font = Font;
            }

            base.OnFontChanged(e);
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(RecipeLineState.NotDefined)]
        public virtual RecipeLineState State
        {
            get => state;
            set
            {
                RecipeLineState previous_state = state;
                state = value;
                if (state != previous_state)
                    ShowStateButtons(state, previous_state);
            }
        }

        [DefaultValue(typeof(Color), "0xF0F0F0")]
        public virtual Color GridColor
        {
            get => gridColor;
            set
            {
                Color oldcolor = gridColor;
                gridColor = value;
                if (oldcolor != gridColor)
                    SetGridColorAndPaint();
            }
        }

        [DefaultValue(typeof(Color), "0xF0F0F0")]
        public virtual Color ButtonBackColor
        {
            get => buttonBackColor;
            set
            {
                Color oldcolor = buttonBackColor;
                buttonBackColor = value;
                if (oldcolor != buttonBackColor)
                    SetButtonColorAndPaint();
            }
        }

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public object? DataSource
        {
            get => RecipeLineBindingSource.DataSource;
            set
            {
                if (DataSource != null) UnBindData();
                RecipeLineBindingSource.DataSource = value;

                if (DataSource is not null and RecipeLine line)
                {
                    BindData();
                    State = line.ValueEquals(RecipeLine.Empty) ? RecipeLineState.AddMode : RecipeLineState.EditMode;
                }
            }
        }

        public string DataMember
        {
            get => RecipeLineBindingSource.DataMember;
            set => RecipeLineBindingSource.DataMember = value;
        }

        public AutoCompleteSource AutoCompleteSource
        {
            get => autoCompleteSource;
            set
            {
                AutoCompleteSource old = autoCompleteSource;
                autoCompleteSource = value switch
                {
                    AutoCompleteSource.CustomSource => AutoCompleteSource.CustomSource,
                    _ => AutoCompleteSource.None,
                };
                if (old != value) SetAutoComplete();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => DescTextBox.AutoCompleteCustomSource;
            set => DescTextBox.AutoCompleteCustomSource = value;
        }

        [Category("Data")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint),
           AttributeProvider(typeof(IListSource))]
        public object? UnitsDataSource
        {
            get => UnitComboBox?.DataSource;
            set
            {
                if (value == null)
                {
                    UnitComboBox.AutoCompleteMode = AutoCompleteMode.None;
                    UnitComboBox.AutoCompleteSource = AutoCompleteSource.None;
                }

                if (value is not null and not (IList or IListSource)) throw new ArgumentException("Complex DataBinding accepts as a data source either an IList or an IListSource.", nameof(value));
                UnitComboBox.DataSource = value;

                if (value is not null and IList<Unit> units)
                {
                    UnitComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    UnitComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    AutoCompleteStringCollection results = new();
                    foreach (var item in units)
                    {
                        _ = results.Add(item.UnitName);
                    }

                    UnitComboBox.AutoCompleteCustomSource = results;
                }
            }
        }

        [Browsable(false)]
        public AutoCompleteMode AutoCompleteMode { get; private set; } = OFF;

        [Browsable(false)]
        public BindingSource BindingSource => RecipeLineBindingSource;

        [Browsable(false)]
        public ControlCollection GetAllControls => LayoutPanel.Controls;

        private void BindData()
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox textBox && textBox.Tag != null) _ = textBox.DataBindings.Add("Text", RecipeLineBindingSource, textBox.Tag.ToString());
                if (item is ModernComboBox comboBox && comboBox.Tag != null) _ = comboBox.DataBindings.Add("Text", RecipeLineBindingSource, comboBox.Tag.ToString());
            }
        }

        private void UnBindData()
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox textBox) textBox.DataBindings.Clear();
            }
        }

        private void ShowStateButtons(RecipeLineState state, RecipeLineState previous_state)
        {
            switch (state)
            {
                case RecipeLineState.EditMode:
                    CreateEditModeLayoutPanel(previous_state);
                    break;
                case RecipeLineState.AddMode:
                    CreateAddModeLayoutPanel(previous_state);
                    break;
                case RecipeLineState.NotDefined:
                default:
                    CreateStandardLayoutPanel(previous_state);
                    break;
            }
        }

        private void CreateAddModeLayoutPanel(RecipeLineState previous_state)
        {
            if (previous_state == RecipeLineState.EditMode) CleanUpEditMode();
            InitializeLayoutPanel(GetAddModeColumns(), true);
            AddButton = CreateButton(nameof(AddButton), Properties.Resources.Add_thin_24x_Black, Properties.Resources.Add_thin_24x_White, Add_Clicked);
            LayoutPanel.Controls.Add(AddButton, 7, 0);
        }

        private void CleanUpEditMode()
        {
            LayoutPanel.Controls.Remove(CancelButton);
            LayoutPanel.Controls.Remove(SaveButton);
            CancelButton.Click -= Cancel_Clicked;
            SaveButton.Click -= Save_Clicked;
            CancelButton = null;
            SaveButton = null;
        }

        private void CreateEditModeLayoutPanel(RecipeLineState previous_state)
        {
            if (previous_state == RecipeLineState.AddMode) CleanUpAddMode();
            InitializeLayoutPanel(GetEditModeColumns(), true);
            CancelButton = CreateButton(nameof(CancelButton), Properties.Resources.Cancel_Black_24x, Properties.Resources.Cancel_White_24x, Cancel_Clicked);
            SaveButton = CreateButton(nameof(SaveButton), Properties.Resources.Save_Black_24x, Properties.Resources.Save_White_24x, Save_Clicked);
            LayoutPanel.Controls.Add(CancelButton, 7, 0);
            LayoutPanel.Controls.Add(SaveButton, 8, 0);
        }

        private void CleanUpAddMode()
        {
            LayoutPanel.Controls.Remove(AddButton);
            AddButton.Click -= Add_Clicked;
            AddButton = null;
        }

        private static IList<ColumnStyle> GetAddModeColumns()
        {
            var list = GetStandardColumns();
            list.Add(new(SizeType.Absolute, 55F));
            return list;
        }

        private static IList<ColumnStyle> GetEditModeColumns()
        {
            var list = GetStandardColumns();
            list.Add(new(SizeType.Absolute, 55F));
            list.Add(new(SizeType.Absolute, 55F));
            return list;
        }

        private void SetAutoComplete()
        {
            AutoCompleteMode = autoCompleteSource == AutoCompleteSource.None ? OFF : ON;

            DescTextBox.AutoCompleteMode = AutoCompleteMode;
            DescTextBox.AutoCompleteSource = autoCompleteSource;
        }
    }
}
