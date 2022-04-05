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
    [DefaultProperty(nameof(Items))]
    [DefaultBindingProperty(nameof(Text))]
    [Description("Displays an editable text box with a drop-down list of permitted values.")]
    public class ModernComboBox : UserControl
    {
        /// <summary>
        /// Subclassing the original ComboBox to control position of dropdown list
        /// </summary>
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
        private ExtendedBorderStyle borderStyle = ExtendedBorderStyle.None;

        private readonly BetterComboBox cmbList;
        private readonly Button btnIcon;

        bool calculating_Size = false;

        public ModernComboBox()
        {
            SuspendLayout();
            cmbList = CreateComboBox();
            btnIcon = CreateIcon();
            initialHeight = cmbList.Height + (2 * TopBottomPadding);

            Controls.Add(btnIcon);
            Controls.Add(cmbList);
            MinimumSize = new(0, initialHeight);
            Size = new(cmbList.Width + (2 * LeftRightPadding), initialHeight);
            ForeColor = SystemColors.ControlText;
            Padding = new(LeftRightPadding, TopBottomPadding, LeftRightPadding, TopBottomPadding);
            CalculateMarginAndLocation();
            ResumeLayout(false);
        }

        private Button CreateIcon()
        {
            Button btn = new()
            {
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                Size = new(cmbList.Height, cmbList.Height),
                //Cursor = Cursors.Hand,
                TabStop = false,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += Icon_Click;//Open dropdown list
            btn.Paint += Icon_Paint;//Draw icon
            return btn;
        }

        private BetterComboBox CreateComboBox()
        {
            BetterComboBox cmb = new()
            {
                DrawMode = DrawMode.OwnerDrawVariable,
                BackColor = BackColor,
                ForeColor = ForeColor,
                Margin = new(0),
                FormattingEnabled = true,
                FlatStyle = FlatStyle.Flat,
                Location = new(LeftRightPadding, TopBottomPadding),
                TabIndex = 0,
            };
            cmb.DrawItem += ComboBox_DrawItem;
            cmb.MeasureItem += ComboBox_MeasureItem;
            cmb.SelectedIndexChanged += ComboBox_SelectedIndexChanged;//Default event
            cmb.TextChanged += ComboBox_TextChanged;//Refresh text
            return cmb;
        }

        private void ComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemWidth = DropDownWidth;
            e.ItemHeight = Height - (Padding.Top + Padding.Bottom) - (cmbList.Margin.Bottom > 0 ? 2 : 0);
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

        private void CalculateMarginAndLocation()
        {
            int padding_offset = (int)BorderStyle > 3 ? (int)Math.Floor(borderRadius / 3F) + borderSize : 0; // When ExtendedStyles.DrawnBorder is used we need to make space for rounded border drawing
            int w_offset = (Width - (Padding.Left + Padding.Right) - cmbList.Width) / 2;
            int h_offset = (Height - (Padding.Top + Padding.Bottom) - cmbList.Height) / 2;

            // Only calculate if ExtendedStyles.DrawnBorder is used
            if (padding_offset > 0)
            {
                int compare = padding_offset.CompareTo(h_offset);
                int ch_offset = compare switch
                {
                    >= 0 => 0,
                    < 0 => h_offset - padding_offset,
                };
                padding_offset = compare switch
                {
                    > 0 => padding_offset - h_offset,
                    <= 0 => 0,
                };
                h_offset = ch_offset;

                // Do we need to resize?
                if (padding_offset > 0)
                {
                    calculating_Size = true;
                    Size = new(Width + (2 * (Padding.Left - (LeftRightPadding + padding_offset))), cmbList.Height + (2 * (TopBottomPadding + padding_offset)));
                    calculating_Size = false;
                }
            }

            Padding = new(LeftRightPadding + padding_offset, TopBottomPadding + padding_offset, LeftRightPadding + padding_offset, TopBottomPadding + padding_offset);
            cmbList.Margin = new(w_offset, h_offset, w_offset, h_offset);
            cmbList.Location = new(Padding.Left + cmbList.Margin.Left, Padding.Top + cmbList.Margin.Top);
            cmbList.SetDropDownHeightOffset((Height - cmbList.Height) / 2);
            btnIcon.Size = new(btnIcon.Size.Width, Height - (Padding.Top + Padding.Bottom));
        }

        #region Properties ->

        [Category("Appearance")]
        [DefaultValue(ExtendedBorderStyle.DrawnBorder)]
        [Description("Indicates whether the edit control should have a border.")]
        public new ExtendedBorderStyle BorderStyle
        {
            get => borderStyle;
            set
            {
                ExtendedBorderStyle old = borderStyle;
                borderStyle = value;
                if (old != borderStyle)
                {
                    switch (value)
                    {
                        case ExtendedBorderStyle.None:
                        case ExtendedBorderStyle.FixedSingle:
                        case ExtendedBorderStyle.Fixed3D:
                            underlinedStyle = false;
                            base.BorderStyle = (BorderStyle)(int)value;
                            break;
                        case ExtendedBorderStyle.DrawnBorder:
                            underlinedStyle = false;
                            break;
                        case ExtendedBorderStyle.UnderLined:
                            underlinedStyle = true;
                            break;
                        default:
                            break;
                    }

                    Invalidate();
                }
            }
        }

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
                if (cmbList.DroppedDown) cmbList.Invalidate();
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
                CalculateMarginAndLocation();
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
                    CalculateMarginAndLocation();
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

        #endregion

        #region Overridden Methods ->

        //Event methods

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            cmbList.Font = Font;
            initialHeight = cmbList.Height + Padding.Top + Padding.Bottom;
            MinimumSize = new(0, initialHeight);
            CalculateMarginAndLocation();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            cmbList.Size = new(Width - (Padding.Left + Padding.Right), Height - (Padding.Top + Padding.Bottom));
            if (!calculating_Size) CalculateMarginAndLocation();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            cmbList.ForeColor = ForeColor;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (DesignMode) CalculateMarginAndLocation();
        }

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
        private void ComboBox_TextChanged(object? sender, EventArgs e) =>
            OnTextChanged(e);

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if ((int)BorderStyle > 2)
            {

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
        }

        #endregion

        #region Events ->

        [Browsable(true)]
        public event EventHandler? SelectedIndexChanged; //Default event

        [Browsable(true)]
        public new event EventHandler? TextChanged;

        #endregion
    }
}
