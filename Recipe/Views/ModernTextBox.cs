namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxItem(typeof(ModernTextBox))]
    [DefaultEvent(nameof(TextChanged))]
    [DefaultBindingProperty(nameof(Text))]
    //[Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    //[Designer(typeof(ModernTextBoxDesigner))]
    [Description("Enables the user to enter text, and provides multiline editing and password character masking with additional design options.")]
    public class ModernTextBox : UserControl
    {
        private readonly TextBox textBox;
        private const int TopBottomPadding = 3;
        private const int LeftRightPadding = 5;

        private int initialHeight;
        private Color borderColor = SystemColors.InactiveBorder;
        private int borderSize = 0;
        private bool underlinedStyle = false;
        private Color borderFocusColor = SystemColors.ActiveBorder;
        private int borderRadius = 0;
        private ExtendedBorderStyle borderStyle = ExtendedBorderStyle.None;

        bool calculating_Size = false;

        public ModernTextBox()
        {
            SuspendLayout();
            textBox = CreateTextBox();
            initialHeight = textBox.Height + (2 * TopBottomPadding);

            Controls.Add(textBox);
            MinimumSize = new(0, initialHeight);
            Padding = new(LeftRightPadding, TopBottomPadding, LeftRightPadding, TopBottomPadding);
            Size = new(textBox.Width + (2 * LeftRightPadding), textBox.Height + (2 * TopBottomPadding));
            BackColor = SystemColors.Window;
        }

        #region New Functionality

        #region Private helper methods ->

        private TextBox CreateTextBox()
        {
            TextBox tb = new()
            {
                //AutoSize = false,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Text = "",
                Margin = new(0),
                BackColor = BackColor,
                ForeColor = ForeColor,
                Location = new(LeftRightPadding, TopBottomPadding),
            };
            tb.TextChanged += TextBox_TextChanged;
            tb.Click += TextBox_Click;
            tb.MouseEnter += TextBox_MouseEnter;
            tb.MouseLeave += TextBox_MouseLeave;
            tb.KeyPress += TextBox_KeyPress;
            tb.Enter += TextBox_Enter;
            tb.Leave += TextBox_Leave;
            tb.PreviewKeyDown += TextBox_PreviewKeyDown;
            return tb;
        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if (Multiline)
            {
                pathTxt = textBox.ClientRectangle.GetRoundedPath(borderRadius - borderSize);
                textBox.Region = new(pathTxt);
            }
            else
            {
                pathTxt = textBox.ClientRectangle.GetRoundedPath(borderSize * 2);
                textBox.Region = new(pathTxt);
            }

            pathTxt.Dispose();
        }

        private void CalculateMarginAndLocation()
        {
            int padding_offset = (int)BorderStyle > 3 ? (int)Math.Floor(borderRadius / 3F) + borderSize : 0; // When ExtendedStyles.DrawnBorder is used we need to make space for rounded border drawing
            int w_offset = (Width - (Padding.Left + Padding.Right) - textBox.Width) / 2;
            int h_offset = (Height - (Padding.Top + Padding.Bottom) - textBox.Height) / 2;

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
                    Size = new(Width + (2 * (Padding.Left - (LeftRightPadding + padding_offset))), textBox.Height + (2 * (TopBottomPadding + padding_offset)));
                    calculating_Size = false;
                }
            }

            Padding = new(LeftRightPadding + padding_offset, TopBottomPadding + padding_offset, LeftRightPadding + padding_offset, TopBottomPadding + padding_offset);
            textBox.Margin = new(w_offset, h_offset, w_offset, h_offset);
            textBox.Location = new(Padding.Left + textBox.Margin.Left, Padding.Top + textBox.Margin.Top);
        }

        #endregion

        #region Overrided methods ->

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            textBox.Font = Font;
            initialHeight = textBox.Height + Padding.Top + Padding.Bottom;
            MinimumSize = new(0, initialHeight);
            CalculateMarginAndLocation();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            textBox.Size = new(Width - (Padding.Left + Padding.Right), Height - (Padding.Top + Padding.Bottom));
            if (!calculating_Size) CalculateMarginAndLocation();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (DesignMode) CalculateMarginAndLocation();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if ((int)BorderStyle > 2)
            {
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
                    if (borderRadius > 15) SetTextBoxRoundedRegion();//Set the rounded region of TextBox component
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;
                    if (Focused || textBox.Focused) penBorder.Color = borderFocusColor;

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
                    if (Focused || textBox.Focused) penBorder.Color = borderFocusColor;

                    if (underlinedStyle) //Line Style
                        graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                    else //Normal Style
                        graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
                }
            }
        }

        #endregion

        #region Public properties ->

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

        [Category("Appearance")]
        [Description("The border color of the component.")]
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
        [DefaultValue(0)]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                if (value >= 0)
                {
                    borderSize = value;
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

        [Category("Appearance")]
        [Description("The border radius of the component.")]
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

        #endregion

        #endregion

        #region Existing Functionality

        #region Overrided methods ->

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            textBox.ForeColor = ForeColor;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            textBox.BackColor = BackColor;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            textBox.Enabled = Enabled;
        }

        #endregion

        #region Public properties ->

        [Category("Behavior")]
        [DefaultValue((char)0)]
        [Localizable(true)]
        [Description("Indicates the character to display for password input for single-line edit controls.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public char PasswordChar
        {
            get => textBox.PasswordChar;
            set => textBox.PasswordChar = value;
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MergableProperty(false)]
        [Localizable(true)]
        [Description("The lines of text in a multiline edit, as an array of String values.")]
        [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string[] Lines
        {
            get => textBox.Lines;
            set => textBox.Lines = value;
        }

        [Category("Behavior")]
        [DefaultValue(32767)]
        [Localizable(true)]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        public virtual int MaxLength
        {
            get => textBox.MaxLength;
            set => textBox.MaxLength = value;
        }

        [Category("Behavior")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates if the text in the edit control has been modified by the user.")]
        public bool Modified
        {
            get => textBox.Modified;
            set => textBox.Modified = value;
        }

        [Category("Appearance")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The currently selected text.")]
        public virtual string SelectedText
        {
            get => textBox.SelectedText;
            set => textBox.SelectedText = value;
        }

        [Category("Appearance")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The length of the currently selected text.")]
        public virtual int SelectionLength
        {
            get => textBox.SelectionLength;
            set => textBox.SelectionLength = value;
        }

        [Category("Appearance")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The beginning of the currently selected text.")]
        public int SelectionStart
        {
            get => textBox.SelectionStart;
            set => textBox.SelectionStart = value;
        }

        [Category("Behavior")]
        [Localizable(true)]
        [DefaultValue(true)]
        [Description("Indicates if lines are automatically word-wrapped for multiline edit controls.")]
        public bool WordWrap
        {
            get => textBox.WordWrap;
            set => textBox.WordWrap = value;
        }

        public override string ToString()
        {
            string s = base.ToString();

            string txt = Text;
            if (txt.Length > 40)
            {
                txt = txt.Substring(0, 40) + "...";
            }

            return s + ", Text: " + txt.ToString();
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        public bool ReadOnly
        {
            get => textBox.ReadOnly;
            set => textBox.ReadOnly = value;
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        public bool HideSelection
        {
            get => textBox.HideSelection;
            set => textBox.HideSelection = value;
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Indicates if tab characters are accepted as input for multiline edit controls.")]
        public bool AcceptsTab
        {
            get => textBox.AcceptsTab;
            set => textBox.AcceptsTab = value;
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Indicates if return characters are accepted as input for multiline edit controls.")]
        public bool AcceptsReturn
        {
            get => textBox.AcceptsReturn;
            set => textBox.AcceptsReturn = value;
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Indicates whether shortcuts defined for the control are enabled.")]
        public virtual bool ShortcutsEnabled
        {
            get => textBox.ShortcutsEnabled;
            set => textBox.ShortcutsEnabled = value;
        }

        [Category("Appearance")]
        [Localizable(true)]
        [DefaultValue(ScrollBars.None)]
        [Description("Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
        public ScrollBars ScrollBars
        {
            get => textBox.ScrollBars;
            set => textBox.ScrollBars = value;
        }

        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        [Category("Behavior")]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get => textBox.UseSystemPasswordChar;
            set => textBox.UseSystemPasswordChar = value;
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(true)]
        [Description("Controls whether the text of the edit control can span more than one line.")]
        [RefreshProperties(RefreshProperties.All)]
        public bool Multiline
        {
            get => textBox.Multiline;
            set => textBox.Multiline = value;
        }

        [Category("Behavior")]
        [DefaultValue(CharacterCasing.Normal)]
        [Description("Indicates if all characters should be left alone or converted to uppercase or lowercase.")]
        public CharacterCasing CharacterCasing
        {
            get => textBox.CharacterCasing;
            set => textBox.CharacterCasing = value;
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue(HorizontalAlignment.Left)]
        [Description("Indicates how the text should be aligned for edit controls.")]
        public HorizontalAlignment TextAlign
        {
            get => textBox.TextAlign;
            set => textBox.TextAlign = value;
        }

        [Category("Appearance")]
        [Localizable(true)]
        [DefaultValue("")]
        [Description("Specifies the PlaceholderText of the TextBox control. The PlaceholderText is displayed in the control when the Text property is null or empty and can be used to guide the user what input is expected by the control.")]
        public string PlaceholderText
        {
            get => textBox.PlaceholderText;
            set => textBox.PlaceholderText = value;
        }

        [DefaultValue(AutoCompleteMode.None)]
        [Description("Indicates the text completion behavior of the text box.")]
        [TypeConverter(typeof(TextBoxAutoCompleteSourceConverter))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => textBox.AutoCompleteMode;
            set => textBox.AutoCompleteMode = value;
        }

        [DefaultValue(AutoCompleteSource.None)]
        [Description("The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource
        {
            get => textBox.AutoCompleteSource;
            set => textBox.AutoCompleteSource = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => textBox.AutoCompleteCustomSource;
            set => textBox.AutoCompleteCustomSource = value;
        }

        #endregion

        #region Public Methods ->

        [Browsable(false)]
        public virtual int TextLength => textBox.TextLength;

        public void AppendText(string text) => textBox.AppendText(text);

        public void Clear() => textBox.Clear();

        public void ClearUndo() => textBox.ClearUndo();

        public void Copy() => textBox.Copy();

        public void Cut() => textBox.Cut();

        public void Paste() => textBox.Paste();

        public virtual char GetCharFromPosition(Point pt) => textBox.GetCharFromPosition(pt);

        public virtual int GetCharIndexFromPosition(Point pt) => textBox.GetCharIndexFromPosition(pt);

        public virtual int GetLineFromCharIndex(int index) => textBox.GetLineFromCharIndex(index);

        public virtual Point GetPositionFromCharIndex(int index) => textBox.GetPositionFromCharIndex(index);

        public int GetFirstCharIndexFromLine(int lineNumber) => textBox.GetFirstCharIndexFromLine(lineNumber);

        public int GetFirstCharIndexOfCurrentLine() => textBox.GetFirstCharIndexOfCurrentLine();

        public void ScrollToCaret() => textBox.ScrollToCaret();

        public void DeselectAll() => textBox.DeselectAll();

        public void Select(int start, int length) => textBox.Select(start, length);

        public void SelectAll() => textBox.SelectAll();

        public void Paste(string text) => textBox.Paste(text);

        [Category("Layout")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The preferred height of this control.")]
        [Obsolete("For better accuracy and/or wrapping use GetPreferredSize instead.")]
        public int PreferredHeight => textBox.PreferredHeight;

        public void Undo() => textBox.Undo();

        [Category("Behavior")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates if the edit control can undo the previous action.")]
        public bool CanUndo => textBox.CanUndo;

        #endregion

        #endregion

        #region Events and EventHandlers ->

        private void TextBox_TextChanged(object? sender, EventArgs e) => OnTextChanged(e);

        protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

        [Browsable(true)]
        public new event EventHandler? TextChanged;

        private void TextBox_Click(object? sender, EventArgs e) => OnClick(e);

        private void TextBox_MouseEnter(object? sender, EventArgs e) => OnMouseEnter(e);

        private void TextBox_MouseLeave(object? sender, EventArgs e) => OnMouseLeave(e);

        private void TextBox_KeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

        private void TextBox_Enter(object? sender, EventArgs e) => OnEnter(e);

        private void TextBox_Leave(object? sender, EventArgs e) => OnLeave(e);

        private void TextBox_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

        #endregion
    }
}
