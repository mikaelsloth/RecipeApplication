namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class RoundedForm : Form
    {
        //Fields
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = SystemColors.ActiveBorder;
        private bool useFormBound;

        //Constructor
        public RoundedForm() : this(null, null)
        { }

        public RoundedForm(Control? dataowner, string? initialText)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Padding = new(borderSize);
            PanelTitleBar.BackColor = borderColor;
            BackColor = borderColor;
            DataOwner = dataowner;
            Text = initialText;
        }

        [Category("Appearance")]
        [Description("The border radius of the component.")]
        [DefaultValue(0)]
        public int BorderRadius 
        { 
            get => borderRadius;
            set
            {
                borderRadius = value;
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
                borderSize = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "0xF0F0F0")]
        public override Color BackColor 
        { 
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                borderColor = value;
                PanelTitleBar.BackColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Localizable(true)]
        [Bindable(true)]
        [Description("The text associated with the form.")]
        public override string? Text 
        {
            get => TextBox == null ? base.Text : TextBox.Text;
            set
            {
                if (TextBox == null) base.Text = value;
                else TextBox.Text = value ?? string.Empty;
            }
        }

        [Category("Appearance")]
        [Description("Indicate whether the form border should blend in with the colors of the background.")]
        [DefaultValue(false)]
        public bool UseFormBoundColors
        {
            get => useFormBound;
            set
            {
                useFormBound = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Control? DataOwner { get; set; }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void RoundedForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- Minimize borderless form from taskbar
                return cp;
            }
        }

        private void FormRegionAndBorder(Form form, float radius, Graphics graph, Color borderColor, float borderSize)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                using GraphicsPath roundPath = form.ClientRectangle.GetRoundedPath(radius);
                using Pen penBorder = new(borderColor, borderSize);
                using Matrix transform = new();
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                form.Region = new(roundPath);
                if (borderSize >= 1)
                {
                    Rectangle rect = form.ClientRectangle;
                    float scaleX = 1.0F - ((borderSize + 1) / rect.Width);
                    float scaleY = 1.0F - ((borderSize + 1) / rect.Height);

                    transform.Scale(scaleX, scaleY);
                    transform.Translate(borderSize / 1.6F, borderSize / 1.6F);

                    graph.Transform = transform;
                    graph.DrawPath(penBorder, roundPath);
                }
            }
        }

        private void DrawPath(Rectangle rect, Graphics graph, Color color)
        {
            using GraphicsPath roundPath = rect.GetRoundedPath(borderRadius);
            using Pen penBorder = new(color, 3);
            graph.DrawPath(penBorder, roundPath);
        }

        private struct FormBoundsColors
        {
            public Color TopLeftColor;
            public Color TopRightColor;
            public Color BottomLeftColor;
            public Color BottomRightColor;
        }

        private FormBoundsColors GetFormBoundsColors()
        {
            FormBoundsColors fbColor = new();
            using Bitmap bmp = new(1, 1);
            using Graphics graph = Graphics.FromImage(bmp);
            Rectangle rectBmp = new(0, 0, 1, 1);

            //Top Left
            rectBmp.X = Bounds.X - 1;
            rectBmp.Y = Bounds.Y;
            graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
            fbColor.TopLeftColor = bmp.GetPixel(0, 0);

            //Top Right
            rectBmp.X = Bounds.Right;
            rectBmp.Y = Bounds.Y;
            graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
            fbColor.TopRightColor = bmp.GetPixel(0, 0);

            //Bottom Left
            rectBmp.X = Bounds.X;
            rectBmp.Y = Bounds.Bottom;
            graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
            fbColor.BottomLeftColor = bmp.GetPixel(0, 0);

            //Bottom Right
            rectBmp.X = Bounds.Right;
            rectBmp.Y = Bounds.Bottom;
            graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
            fbColor.BottomRightColor = bmp.GetPixel(0, 0);

            return fbColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //-> SMOOTH OUTER BORDER
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectForm = ClientRectangle;
            int mWidht = rectForm.Width / 2;
            int mHeight = rectForm.Height / 2;
            FormBoundsColors fbColors = useFormBound ? GetFormBoundsColors() : 
                new() { TopLeftColor = borderColor, BottomLeftColor = borderColor, BottomRightColor = borderColor, TopRightColor = borderColor };

            //Top Left
            DrawPath(rectForm, e.Graphics, fbColors.TopLeftColor);            

            //Top Right
            Rectangle rectTopRight = new(mWidht, rectForm.Y, mWidht, mHeight);
            DrawPath(rectTopRight, e.Graphics, fbColors.TopRightColor);

            //Bottom Left
            Rectangle rectBottomLeft = new(rectForm.X, rectForm.X + mHeight, mWidht, mHeight);
            DrawPath(rectBottomLeft, e.Graphics, fbColors.BottomLeftColor);

            //Bottom Right
            Rectangle rectBottomRight = new(mWidht, rectForm.Y + mHeight, mWidht, mHeight);
            DrawPath(rectBottomRight, e.Graphics, fbColors.BottomRightColor);
        }

        //-> SET ROUNDED REGION AND BORDER
        private void RoundedForm_Paint(object? sender, PaintEventArgs e) => FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);

        private void PanelContainer_Paint(object? sender, PaintEventArgs e) => PanelContainer.ControlRegionAndBorder(borderRadius - (borderSize / 2), e.Graphics, borderColor);

        private void RoundedForm_ResizeEnd(object? sender, EventArgs e) => Invalidate();

        private void RoundedForm_SizeChanged(object? sender, EventArgs e) => Invalidate();

        private void RoundedForm_Activated(object? sender, EventArgs e) => Invalidate();

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            if (DataOwner is RemarksButton remBtn) remBtn.Expanded = false;            
            Close();
        }
    }
}
