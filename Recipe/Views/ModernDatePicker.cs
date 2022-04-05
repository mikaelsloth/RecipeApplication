namespace Recipe.Views
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.ComponentModel;

    public class ModernDatePicker : DateTimePicker
    {
        //Fields
        //-> Appearance
        private Color skinColor = SystemColors.Window;
        private Color textColor = SystemColors.ControlText;
        private Color borderColor = SystemColors.InactiveBorder;
        private int borderSize = 0;
        private int borderRadius = 0;

        //-> Other Values
        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.calendarWhite;
        private Rectangle iconButtonArea;
        private const int calendarIconWidth = 34;
        private const int arrowIconWidth = 17;

        //Constructor
        public ModernDatePicker()
        {
            SetStyle(ControlStyles.UserPaint, true);
            MinimumSize = new(0, 35);
            Font = new(Font.Name, 9.5F);            
        }

        //Properties
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                skinColor = value;
                calendarIcon = skinColor.GetBrightness() >= 0.8F ? Properties.Resources.calendarDark : Properties.Resources.calendarWhite;
                base.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                textColor = value;
                base.ForeColor = value;
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

        //Overridden methods
        protected override void OnDropDown(EventArgs eventargs)
        {
            base.OnDropDown(eventargs);
            droppedDown = true;
        }

        protected override void OnCloseUp(EventArgs eventargs)
        {
            base.OnCloseUp(eventargs);
            droppedDown = false;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int smoothSize = 2;
            if (borderSize > 0) smoothSize = borderSize;

            using Graphics graphics = CreateGraphics();
            using Pen penBorder = new(borderColor, borderSize);
            using Pen penSurface = new(Parent.BackColor, smoothSize);
            using SolidBrush skinBrush = new(skinColor);
            using SolidBrush openIconBrush = new(Color.FromArgb(50, 64, 64, 64));
            using SolidBrush textBrush = new(textColor);
            using StringFormat textFormat = new();
            Rectangle clientArea = new(0, 0, Width, Height);
            Rectangle iconArea = new(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height);
            penBorder.Alignment = PenAlignment.Inset;
            textFormat.LineAlignment = StringAlignment.Center;

            if (borderRadius > 2) //Rounded control
            {
                Rectangle rectBorder = Rectangle.Inflate(clientArea, -borderSize, -borderSize);

                using GraphicsPath pathSurface = clientArea.GetRoundedPath(borderRadius);
                using GraphicsPath pathBorder = rectBorder.GetRoundedPath(borderRadius - borderSize);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                //Draw surface
                Region = new(pathSurface);
                graphics.FillPath(skinBrush, pathSurface);
                //Draw surface border for HD result
                graphics.DrawPath(penSurface, pathSurface);
                //Draw text
                graphics.DrawString("   " + Text, Font, textBrush, clientArea, textFormat);
                //Draw open calendar icon highlight
                if (droppedDown) graphics.FillRectangle(openIconBrush, iconArea);
                //Draw border 
                if (borderSize >= 1) graphics.DrawPath(penBorder, pathBorder);
                //Draw icon
                graphics.DrawImage(calendarIcon, Width - calendarIcon.Width - 9, (Height - calendarIcon.Height) / 2);
            }
            else //Normal control
            {
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //Drawsurface
                graphics.FillRectangle(skinBrush, clientArea);
                //Draw text
                graphics.DrawString(" " + Text, Font, textBrush, clientArea, textFormat);
                //Draw open calendar icon highlight
                if (droppedDown) graphics.FillRectangle(openIconBrush, iconArea);
                //Draw border
                if (borderSize >= 1) graphics.DrawRectangle(penBorder, clientArea.X, clientArea.Y, clientArea.Width, clientArea.Height);
                //Draw icon
                graphics.DrawImage(calendarIcon, Width - calendarIcon.Width - 9, (Height - calendarIcon.Height) / 2);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            int iconWidth = GetIconButtonWidth();
            iconButtonArea = new(Width - iconWidth, 0, iconWidth, Height);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = iconButtonArea.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
        }

        //Private methods
        private int GetIconButtonWidth()
        {
            int textWidh = TextRenderer.MeasureText(Text, Font).Width;
            return textWidh <= Width - (calendarIconWidth + 20) ? calendarIconWidth : arrowIconWidth;
        }
    }
}
