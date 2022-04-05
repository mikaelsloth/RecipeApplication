namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ModernLabel : Label
    {
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color borderColor = SystemColors.InactiveBorder;

        public ModernLabel()
        {
            AutoSize = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (borderRadius > Height)
                borderRadius = Height;
        }

        //Properties
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
        [Description("The border radius of the component.")]
        [DefaultValue(0)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value > Height ? Height : value;
                Invalidate();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded label
            {
                using GraphicsPath pathSurface = rectSurface.GetRoundedPath(borderRadius);
                using GraphicsPath pathBorder = rectBorder.GetRoundedPath(borderRadius - borderSize);
                using Pen penSurface = new(Parent.BackColor, smoothSize);
                using Pen penBorder = new(borderColor, borderSize);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //Label surface
                Region = new(pathSurface);
                //Draw surface border for HD result
                e.Graphics.DrawPath(penSurface, pathSurface);

                //Label border                    
                if (borderSize >= 1)
                    //Draw control border
                    e.Graphics.DrawPath(penBorder, pathBorder);
            }
            else //Normal label
            {
                e.Graphics.SmoothingMode = SmoothingMode.None;
                //Label surface
                Region = new(rectSurface);
                //Label border
                if (borderSize >= 1)
                {
                    using Pen penBorder = new(borderColor, borderSize);
                    penBorder.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += Container_BackColorChanged;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Parent.BackColorChanged -= Container_BackColorChanged;
            base.OnHandleDestroyed(e);
        }

        private void Container_BackColorChanged(object? sender, EventArgs e) => Invalidate();
    }
}
