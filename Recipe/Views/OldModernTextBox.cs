namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [DefaultEvent("TextChanged")]
    [DefaultBindingProperty("Text")]
    public partial class OldModernTextBox : UserControl
    {
        private Color borderColor = SystemColors.InactiveBorder;
        private int borderSize = 0;
        private bool underlinedStyle = false;
        private Color borderFocusColor = SystemColors.ActiveBorder;
        private int borderRadius = 0;

        public OldModernTextBox()
        {
            InitializeComponent();
        }

        #region -> Private methods

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if (Multiline)
            {
                pathTxt = textBox1.ClientRectangle.GetRoundedPath(borderRadius - borderSize);
                textBox1.Region = new(pathTxt);
            }
            else
            {
                pathTxt = textBox1.ClientRectangle.GetRoundedPath(borderSize * 2);
                textBox1.Region = new(pathTxt);
            }

            pathTxt.Dispose();
        }

        private void UpdateControlHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new(0, txtHeight);
                textBox1.Multiline = false;

                Height = textBox1.Height + Padding.Top + Padding.Bottom;
            }
        }
        #endregion

        #region -> Properties
        //Properties

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
                    Invalidate();
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

        [Category("Behavior")]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get => textBox1.UseSystemPasswordChar;
            set => textBox1.UseSystemPasswordChar = value;
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(true)]
        [Description("Controls whether the text of the edit control can span more than one line.")]
        [RefreshProperties(RefreshProperties.All)]
        public bool Multiline
        {
            get => textBox1.Multiline;
            set => textBox1.Multiline = value;
        }

        [DefaultValue(typeof(Color), "0xF0F0F0")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }

        [DefaultValue(typeof(Color), "0x000000")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (DesignMode)
                    UpdateControlHeight();
            }
        }

        [Category("Behavior")]
        [DefaultValue(CharacterCasing.Normal)]
        [Description("Indicates if all characters should be left alone or converted to uppercase or lowercase.")]
        public CharacterCasing CharacterCasing
        {
            get => textBox1.CharacterCasing;
            set => textBox1.CharacterCasing = value;
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue(HorizontalAlignment.Left)]
        [Description("Indicates how the text should be aligned for edit controls.")]
        public HorizontalAlignment TextAlign
        {
            get => textBox1.TextAlign;
            set => textBox1.TextAlign = value;            
        }

        [Category("Appearance")]
        [Localizable(true)]
        [DefaultValue("")]
        [Description("Specifies the PlaceholderText of the TextBox control. The PlaceholderText is displayed in the control when the Text property is null or empty and can be used to guide the user what input is expected by the control.")]
        public string PlaceholderText
        {
            get => textBox1.PlaceholderText;
            set => textBox1.PlaceholderText = value;
        }

        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
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
                    Invalidate();//Redraw control
                }
            }
        }

        [DefaultValue(AutoCompleteMode.None)]
        [Description("Indicates the text completion behavior of the text box.")]
        [TypeConverter(typeof(TextBoxAutoCompleteSourceConverter))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => textBox1.AutoCompleteMode;
            set => textBox1.AutoCompleteMode = value;
        }

        [DefaultValue(AutoCompleteSource.None)]
        [Description("The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource
        {
            get => textBox1.AutoCompleteSource;
            set => textBox1.AutoCompleteSource = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => textBox1.AutoCompleteCustomSource;
            set => textBox1.AutoCompleteCustomSource = value;
        }
        #endregion

        #region -> Overridden methods

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (DesignMode)
                UpdateControlHeight();
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }
        
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
                if (borderRadius > 15) SetTextBoxRoundedRegion();//Set the rounded region of TextBox component
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                penBorder.Alignment = PenAlignment.Center;
                if (Focused || textBox1.Focused) penBorder.Color = borderFocusColor;

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
                if (Focused || textBox1.Focused) penBorder.Color = borderFocusColor;

                if (underlinedStyle) //Line Style
                    graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                else //Normal Style
                    graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            textBox1.Enabled = Enabled;
            base.OnEnabledChanged(e);
        }

        #endregion

        //TextBox-> TextChanged event
        private void TextBox1_TextChanged(object? sender, EventArgs e) => OnTextChanged(e);

        protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

        [Browsable(true)]
        public new event EventHandler? TextChanged;

        private void TextBox1_Click(object? sender, EventArgs e) => OnClick(e);
        
        private void TextBox1_MouseEnter(object? sender, EventArgs e) => OnMouseEnter(e);
        
        private void TextBox1_MouseLeave(object? sender, EventArgs e) => OnMouseLeave(e);
        
        private void TextBox1_KeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

        private void TextBox1_Enter(object? sender, EventArgs e) => OnEnter(e);

        private void TextBox1_Leave(object? sender, EventArgs e) => OnLeave(e);

        private void TextBox1_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);
    }
}
