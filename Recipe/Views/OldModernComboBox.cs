namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DefaultEvent("SelectedIndexChanged")]
    //[Designer("System.Windows.Forms.Design.ComboBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public partial class OldModernComboBox : UserControl
    {
        private class BetterComboBox : ComboBox
        {
            private const int SWP_NOSIZE = 0x1;
            private const int WM_CTLCOLORLISTBOX = 0x0134;
            private int dropDownHeightOffset = 0;

            internal void SetDropDownHeightOffset(int offset) => dropDownHeightOffset = offset;

            [DllImport("user32.dll")]
            static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_CTLCOLORLISTBOX)
                {
                    // Get the current combo position and size
                    Rectangle comboRect = RectangleToScreen(ClientRectangle);

                    int topOfDropDown = comboRect.Bottom + dropDownHeightOffset;
                    int leftOfDropDown = comboRect.Left;

                    // Postioning/sizing the drop-down
                    //SetWindowPos(HWND hWnd,
                    //      HWND hWndInsertAfter,
                    //      int X,
                    //      int Y,
                    //      int cx,
                    //      int cy,
                    //      UINT uFlags);
                    //when using the SWP_NOSIZE flag, cx and cy params are ignored
                    _ = SetWindowPos(m.LParam, IntPtr.Zero, leftOfDropDown, topOfDropDown, 0, 0, SWP_NOSIZE);                    
                }

                base.WndProc(ref m);
            }
        }

        //Fields
        private const string TestString = "j^";
        private const int LeftRightPadding = 4;
        private const int TopBottomPadding = 2;
        private int initialHeight;

        private Color backColor = SystemColors.Window;
        private Color iconColor = SystemColors.Highlight;
        private Color listTextColor = SystemColors.GrayText;
        private Color borderColor = SystemColors.InactiveBorder;
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color borderFocusColor = SystemColors.ActiveBorder;
        private bool underlinedStyle = false;

        private readonly BetterComboBox cmbList;
        private readonly Button btnIcon;

        //Events
        [Browsable(true)]
        public event EventHandler? SelectedIndexChanged; //Default event

        [Browsable(true)]
        public new event EventHandler? TextChanged;

        public OldModernComboBox()
        {
            initialHeight = CalculateMinimumHeight();
            cmbList = new();
            btnIcon = new();
            SuspendLayout();
            // 
            // cmbList
            //
            cmbList.DrawMode = DrawMode.OwnerDrawVariable;
            cmbList.BackColor = ListBackColor;
            cmbList.ForeColor = listTextColor;
            cmbList.FormattingEnabled = true;
            cmbList.Size = new(170, initialHeight);
            cmbList.FlatStyle = FlatStyle.Flat;
            cmbList.Dock = DockStyle.Fill;
            cmbList.DrawItem += ComboBox_DrawItem;
            cmbList.MeasureItem += ComboBox_MeasureItem;
            cmbList.SelectedIndexChanged += ComboBox_SelectedIndexChanged;//Default event
            cmbList.TextChanged += ComboBox_TextChanged;//Refresh text
            cmbList.TabIndex = 0;
            // 
            // btnIcon
            // 
            btnIcon.Dock = DockStyle.Right;
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.BackColor = backColor;
            btnIcon.Size = new(cmbList.Height, cmbList.Height);
            //btnIcon.Cursor = Cursors.Hand;
            btnIcon.Click += Icon_Click;//Open dropdown list
            btnIcon.Paint += Icon_Paint;//Draw icon
            btnIcon.TabStop = false;
            // 
            // ModernComboBox
            // 
            Controls.Add(btnIcon);
            Controls.Add(cmbList);
            MinimumSize = new(0, cmbList.Height + (2 * TopBottomPadding));
            Size = new(cmbList.Width + (2 * LeftRightPadding), cmbList.Height + (2 * TopBottomPadding));
            ForeColor = SystemColors.ControlText;
            Padding = new(LeftRightPadding, TopBottomPadding, LeftRightPadding, TopBottomPadding);
            AdjustDimensions();
            ResumeLayout(false);
        }

        private int CalculateMinimumHeight() => TextRenderer.MeasureText(TestString, Font, new Size(short.MaxValue, (int)(FontHeight * 1.25)), TextFormatFlags.SingleLine).Height;

        private void ComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemWidth = DropDownWidth;
            e.ItemHeight = Math.Max(initialHeight, Height - (Padding.Top + Padding.Bottom));
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            string text = e.Index < 0 ? string.Empty : Items[e.Index]?.ToString() ?? string.Empty;
            e.DrawBackground();
            e.DrawFocusRectangle();

            using SolidBrush backBrush = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? new(SystemColors.Highlight) :
                (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit ? new(e.BackColor) : new(ListBackColor);
            using SolidBrush textBrush = new(e.ForeColor);
            using StringFormat format = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds, format);
        }

        //Private methods
        private void AdjustDimensions()
        {
            int padding_offset = (int)Math.Floor(borderRadius / 3F) + borderSize;
            int calculated_height = cmbList.Height + (2 * (TopBottomPadding + padding_offset));
            Size = new(Width + (2 * (Padding.Left - (TopBottomPadding + padding_offset))), calculated_height);
            btnIcon.Size = new(btnIcon.Size.Width, cmbList.Height);
            Padding = new(LeftRightPadding + padding_offset, TopBottomPadding + padding_offset, LeftRightPadding + padding_offset, TopBottomPadding + padding_offset);
            cmbList.SetDropDownHeightOffset((Height - cmbList.Height) / 2);
        }

        //Properties
        //-> Appearance
        [DefaultValue(typeof(Color), "0x000000")]
        public override Color BackColor
        {
            get => backColor;
            set
            {
                backColor = value;
                btnIcon.BackColor = backColor;
                cmbList.BackColor = backColor;
                base.BackColor = borderColor;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Whether the Underlined style should be applied to the control.")]
        [DefaultValue(false)]
        public bool UnderlinedStyle
        {
            get => underlinedStyle;
            set
            {
                underlinedStyle = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The color of the controls dropdown arrow.")]
        [DefaultValue(typeof(Color), "0x3399FF")]
        public Color IconColor
        {
            get => iconColor;
            set
            {
                iconColor = value;
                btnIcon.Invalidate();//Redraw icon
            }
        }

        [Category("Appearance")]
        [Description("The color of the dropdown area.")]
        [DefaultValue(typeof(Color), "0xE3E3E3")]
        public Color ListBackColor { get; set; } = SystemColors.ControlLight;

        [Category("Appearance")]
        [Description("The color of the text.")]
        [DefaultValue(typeof(Color), "0x6D6D6D")]
        public Color ListTextColor
        {
            get => listTextColor;
            set
            {
                listTextColor = value;
                cmbList.ForeColor = listTextColor;
                cmbList.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The color of the border.")]
        [DefaultValue(typeof(Color), "0xF4F7FC")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The border size of the component.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(0)]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                AdjustDimensions();
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The border radius of the component.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(0)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    AdjustDimensions();
                    Invalidate();//Redraw control
                }
            }
        }

        [Category("Appearance")]
        [Description("The border color of the component when focused.")]
        [DefaultValue(typeof(Color), "0xB4B4B4")]
        public Color BorderFocusColor
        {
            get => borderFocusColor;
            set
            {
                borderFocusColor = value;
                Invalidate();
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                cmbList.ForeColor = value;
            }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                initialHeight = CalculateMinimumHeight();
                cmbList.Font = value;//Optional

                if (DesignMode) AdjustDimensions();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get => cmbList.Text;
            set => cmbList.Text = value;
        }

        [Category("Appearance")]
        [DefaultValue(ComboBoxStyle.DropDown)]
        [Description("Controls the appearance and functionality of the combo box.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ComboBoxStyle DropDownStyle
        {
            get => cmbList.DropDownStyle;
            set
            {
                if (cmbList.DropDownStyle != ComboBoxStyle.Simple)
                    cmbList.DropDownStyle = value;
            }
        }

        [Category("Behavior")]
        [Description("The width, in pixels, of the drop-down box in a combo box.")]
        public int DropDownWidth
        {
            get => cmbList.DropDownWidth;
            set => cmbList.DropDownWidth = value;
        }

        //Event methods

        protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

        //-> Default event
        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e) => SelectedIndexChanged?.Invoke(sender, e);//Refresh text

        //-> Items actions
        private void Icon_Click(object? sender, EventArgs e)
        {
            //Open dropdown list
            cmbList.Select();
            cmbList.DroppedDown = true;
        }

        //Refresh text
        private void ComboBox_TextChanged(object? sender, EventArgs e) => OnTextChanged(e);

        //-> Draw icon
        private void Icon_Paint(object? sender, PaintEventArgs e)
        {
            //Fields
            int iconWidht = 14;
            int iconHeight = 6;
            Rectangle rectIcon = new((btnIcon.Width - iconWidht) / 2, (btnIcon.Height - iconHeight) / 2, iconWidht, iconHeight);
            Graphics graph = e.Graphics;

            //Draw arrow down icon
            using GraphicsPath path = new();
            using Pen pen = new(iconColor, 2);
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            path.AddLine(rectIcon.X, rectIcon.Y, rectIcon.X + (iconWidht / 2), rectIcon.Bottom);
            path.AddLine(rectIcon.X + (iconWidht / 2), rectIcon.Bottom, rectIcon.Right, rectIcon.Y);
            graph.DrawPath(pen, path);
        }

        private void Surface_MouseEnter(object? sender, EventArgs e) => OnMouseEnter(e);

        private void Surface_MouseLeave(object? sender, EventArgs e) => OnMouseLeave(e);

        //Overridden methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (borderRadius > 1)//Rounded TextBox
            {
                //-Fields
                var rectBorderSmooth = ClientRectangle;
                Rectangle rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using GraphicsPath pathBorderSmooth = rectBorderSmooth.GetRoundedPath(borderRadius);
                using GraphicsPath pathBorder = rectBorder.GetRoundedPath(borderRadius - borderSize);
                using Pen penBorderSmooth = new(Parent.BackColor, smoothSize);
                using Pen penBorder = new(borderColor, borderSize);
                //-Drawing
                Region = new(pathBorderSmooth);//Set the rounded region of UserControl
                //if (borderRadius > 15) SetLabelRoundedRegion();//Set the rounded region of TextBox component
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                penBorder.Alignment = PenAlignment.Center;
                if (Focused || cmbList.Focused) penBorder.Color = borderFocusColor;

                if (underlinedStyle) //Line Style
                {
                    //Draw border smoothing
                    graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                    //Draw border
                    graph.SmoothingMode = SmoothingMode.None;
                    graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                }
                else //Normal Style
                {
                    //Draw border smoothing
                    graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                    //Draw border
                    graph.DrawPath(penBorder, pathBorder);
                }
            }
            else //Square/Normal TextBox
            {
                //Draw border
                using Pen penBorder = new(borderColor, borderSize);
                Region = new(ClientRectangle);
                penBorder.Alignment = PenAlignment.Inset;
                if (Focused || cmbList.Focused) penBorder.Color = borderFocusColor;

                if (underlinedStyle) //Line Style
                    graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                else //Normal Style
                    graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
            }
        }

        //Properties
        //-> Data
        [Category("Data")]
        [Description("The items in the combo box.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        public ComboBox.ObjectCollection Items => cmbList.Items;

        [Category("Data")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint),
             AttributeProvider(typeof(IListSource))]
        [Description("Indicates the list that this control will use to get its items.")]
        public object? DataSource
        {
            get => cmbList.DataSource;
            set => cmbList.DataSource = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("The autocomplete custom source, which is a custom StringCollection used when the AutoCompleteSource is CustomSource.")]
        [Localizable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => cmbList.AutoCompleteCustomSource;
            set => cmbList.AutoCompleteCustomSource = value;
        }

        [Browsable(true)]
        [DefaultValue(AutoCompleteSource.None)]
        [Description("The source of complete strings used for automatic completion.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource
        {
            get => cmbList.AutoCompleteSource;
            set => cmbList.AutoCompleteSource = value;
        }

        [Browsable(true)]
        [DefaultValue(AutoCompleteMode.None)]
        [Description("Indicates the text completion behavior of the combo box.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => cmbList.AutoCompleteMode;
            set => cmbList.AutoCompleteMode = value;
        }

        [Bindable(true)]
        [Browsable(false)]
        [Description("The currently selected item in the combo box, or null.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get => cmbList.SelectedItem;
            set => cmbList.SelectedItem = value;
        }

        [Browsable(false)]
        [Description("The index of the currently selected item of the combo box.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => cmbList.SelectedIndex;
            set => cmbList.SelectedIndex = value;
        }

        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get => cmbList.DisplayMember;
            set => cmbList.DisplayMember = value;
        }

        [Category("Data")]
        [Description("Indicates the property to use as the actual value for the items in the control.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get => cmbList.ValueMember;
            set => cmbList.ValueMember = value;
        }
    }
}
