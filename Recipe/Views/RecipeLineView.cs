namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ComplexBindingProperties("DataSource", "DataMember")]
    public partial class RecipeLineView : UserControl, IRecipeLineView
    {
        //Backing fields
        private Color gridColor;
        private Color buttonBackColor;
        private bool showDeleteButton = false;
        private bool showEditButtons = false;

        private bool textboxesInitialized = false;
        private bool twoButtonsNeeded = false;
        private bool readOnly = false;

        public RecipeLineView(bool addStandardItems)
        {
            gridColor = BackColor;
            buttonBackColor = BackColor;
            InitializeComponent();
            if (addStandardItems) ResetLayoutPanel();
        }

        public RecipeLineView() : this(true)
        { }

        private ModernTextBox CreateTextBox(string name, object? tagName, int tabIndex)
        {
            ModernTextBox textBox = new()
            {
                BackColor = BackColor,
                CharacterCasing = CharacterCasing.Upper,
                Dock = DockStyle.Fill,
                Font = new("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new(1),
                Name = name,
                Padding = new(2, 6, 2, 6),
                Tag = tagName,
                TabIndex = tabIndex,
            };
            textBox.Enter += TextBox_Enter;
            return textBox;
        }

        private void TextBox_Enter(object? sender, EventArgs e) => OnEnter(e);

        protected virtual void InitializeLayoutPanel(ColumnStyle[] columnStyles, bool addStandardTextboxes)
        {
            if (addStandardTextboxes && !textboxesInitialized) textboxesInitialized = InitializeTextBoxes();

            LayoutPanel.SuspendLayout();
            LayoutPanel.Controls.Clear();
            LayoutPanel.ColumnStyles.Clear();
            LayoutPanel.ColumnCount = columnStyles.Length;
            for (int i = 0; i < columnStyles.Length; i++)
            {
                _ = LayoutPanel.ColumnStyles.Add(columnStyles[i]);
            }

            if (addStandardTextboxes)
            {
                LayoutPanel.Controls.Add(DescTextBox, 0, 0);
                LayoutPanel.Controls.Add(UnitTextBox, 1, 0);
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
            UnitTextBox = CreateTextBox(nameof(UnitTextBox), "Units.UnitName", 1);
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

        private void LayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
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
            }

            base.OnForeColorChanged(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox tb) tb.BackColor = BackColor;
            }

            base.OnBackColorChanged(e);
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

        [DefaultValue(false)]
        public virtual bool ShowDeleteButton
        {
            get => showDeleteButton;
            set
            {
                showDeleteButton = value;
                if (value) ShowDeleteButtonInternal();
                else RemoveDeleteButton();
            }
        }

        [DefaultValue(false)]
        public virtual bool ShowEditButton
        {
            get => showEditButtons;
            set
            {
                showEditButtons = value;
                if (value) ShowEditButtonInternal();
                else RemoveEditButton();
            }
        }

        private void RemoveDeleteButton()
        {
            if (twoButtonsNeeded)
            {
                ResetLayoutPanel();
                if (showEditButtons) LayoutPanel.Controls.Add(EditButton, 6, 0);
                twoButtonsNeeded = false;
            }
            else LayoutPanel.Controls.Remove(DeleteButton);
            DeleteButton.Click -= Delete_Clicked;
            DeleteButton = null;
        }

        private void RemoveEditButton()
        {
            if (twoButtonsNeeded)
            {
                ResetLayoutPanel();
                if (showDeleteButton) LayoutPanel.Controls.Add(DeleteButton, 6, 0);
                twoButtonsNeeded = false;
            }
            else LayoutPanel.Controls.Remove(EditButton);
            EditButton.Click -= Edit_Clicked;
            EditButton = null;
        }

        private void ResetLayoutPanel()
        {
            ColumnStyle[] columnStyles = new ColumnStyle[]
            {
                new(SizeType.Percent, 100F),
                new(SizeType.Absolute, 80F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
            };
            InitializeLayoutPanel(columnStyles, true);
        }

        private void ShowDeleteButtonInternal()
        {
            if (TwoButtonsNeeded()) CreateTwoButtons();
            else
            {
                DeleteButton = CreateButton(nameof(DeleteButton), Properties.Resources.Remove_thin_24x_Black, Properties.Resources.Remove_thin_24x_White, Delete_Clicked);
                LayoutPanel.Controls.Add(DeleteButton, 6, 0);
            }
        }

        private void ShowEditButtonInternal()
        {
            if (TwoButtonsNeeded()) CreateTwoButtons();
            else
            {
                EditButton = CreateButton(nameof(EditButton), Properties.Resources.Edit_Black_24x, Properties.Resources.Edit_White_24x, Edit_Clicked);
                LayoutPanel.Controls.Add(EditButton, 6, 0);
            }
        }

        private bool TwoButtonsNeeded()
        {
            bool result = showDeleteButton && showEditButtons;
            twoButtonsNeeded = result;
            return result;
        }

        private void CreateTwoButtons()
        {
            ColumnStyle[] columnStyles = new ColumnStyle[]
            {
                new(SizeType.Percent, 100F),
                new(SizeType.Absolute, 80F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F),
                new(SizeType.Absolute, 35F)
            };
            InitializeLayoutPanel(columnStyles, true);
            DeleteButton = CreateButton(nameof(DeleteButton), Properties.Resources.Remove_thin_24x_Black, Properties.Resources.Remove_thin_24x_White, Delete_Clicked);
            EditButton = CreateButton(nameof(EditButton), Properties.Resources.Edit_Black_24x, Properties.Resources.Edit_White_24x, Edit_Clicked);
            LayoutPanel.Controls.Add(EditButton, 6, 0);
            LayoutPanel.Controls.Add(DeleteButton, 7, 0);
        }

        private ModernButton CreateButton(string name, Image darkImage, Image lightImage, EventHandler del)
        {
            ModernButton button = new()
            {
                BackColor = ButtonBackColor,
                FlatStyle = FlatStyle.Flat,
                ForeColor = SystemColors.ControlText,
                Dock = DockStyle.Right,
                Name = name,
                Size = new(35, 35),
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

        private void Edit_Clicked(object? sender, EventArgs e) => OnEditClicked(e);

        protected virtual void OnEditClicked(EventArgs e) => EditClicked?.Invoke(this, e);

        public event EventHandler? EditClicked;

        private void Delete_Clicked(object? sender, EventArgs e) => OnDeleteClicked(e);

        protected virtual void OnDeleteClicked(EventArgs e) => DeleteClicked?.Invoke(this, e);

        public event EventHandler? DeleteClicked;

        public object? DataSource
        {
            get => RecipeLineBindingSource.DataSource;
            set
            {
                if (DataSource != null) UnBindData();
                RecipeLineBindingSource.DataSource = value;
                if (DataSource != null) BindData();
            }
        }

        private void BindData()
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox textBox && textBox.Tag != null) _ = textBox.DataBindings.Add("Text", RecipeLineBindingSource, textBox.Tag.ToString());
            }

            _ = DataBindings.Add(nameof(Index), RecipeLineBindingSource, "Position");
        }

        private void UnBindData()
        {
            foreach (Control item in LayoutPanel.Controls)
            {
                if (item is ModernTextBox textBox) textBox.DataBindings.Clear();
            }

            DataBindings.Clear();
        }

        public string DataMember
        {
            get => RecipeLineBindingSource.DataMember;
            set => RecipeLineBindingSource.DataMember = value;
        }

        [Browsable(false)]
        public BindingSource BindingSource => RecipeLineBindingSource;

        [Browsable(false)]
        public ControlCollection GetAllControls => LayoutPanel.Controls;

        [Bindable(true)]
        [Browsable(false)]
        public int Index { get; set; } = 0;

        public bool ReadOnly 
        {
            get => readOnly;
            set
            {
                readOnly = value;
                foreach (Control item in LayoutPanel.Controls)
                {
                    item.Enabled = !readOnly;
                }
            }
        }
    }
}
