namespace Recipe.Views
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ModernToggleButton : CheckBox
    {
        //Fields
        private Color backColorOnPosition = SystemColors.Highlight;
        private Color toggleColorOnPosition = SystemColors.Window;
        private Color backColorOffPosition = SystemColors.ControlLight;
        private Color toggleColorOffPosition = SystemColors.Window;
        private bool solidStyle = true;

        //Constructor
        public ModernToggleButton()
        {
            MinimumSize = new(45, 22);
        }

        //Properties
        [Category("Appearance")]
        [Description("The background color of the control when in On position.")]
        [DefaultValue(typeof(Color), "0x3399FF")]
        public Color BackColorOnPosition
        {
            get => backColorOnPosition;
            set
            {
                backColorOnPosition = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The color of the toggle part of the control when in On position.")]
        [DefaultValue(typeof(Color), "0xFFFFFF")]
        public Color ToggleColorOnPosition
        {
            get => toggleColorOnPosition;
            set
            {
                toggleColorOnPosition = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The background color of the control when in Off position.")]
        [DefaultValue(typeof(Color), "0xE3E3E3")]
        public Color BackColorOffPosition
        {
            get => backColorOffPosition;
            set
            {
                backColorOffPosition = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The color of the toggle part of the control when in Off position.")]
        [DefaultValue(typeof(Color), "0xFFFFFF")]
        public Color ToggleColorOffPosition
        {
            get => toggleColorOffPosition;
            set
            {
                toggleColorOffPosition = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get => base.Text;
            set { return; }
        }

        [Category("Appearance")]
        [Description("Whether the background colors for the control fills all the drawn surface or only the border")]
        [DefaultValue(true)]
        public bool SolidStyle
        {
            get => solidStyle;
            set
            {
                solidStyle = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int toggleSize = Height - 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(Parent.BackColor);

            if (Checked) //ON
            {
                //Draw the control surface
                if (solidStyle)
                {
                    using SolidBrush onbckBrush = new(backColorOnPosition);
                    e.Graphics.FillPath(onbckBrush, this.GetFigurePath());
                }
                else
                {
                    using Pen onbckPen = new(backColorOnPosition, 2);
                    e.Graphics.DrawPath(onbckPen, this.GetFigurePath());
                }
                //Draw the toggle
                Rectangle toggleRect = new(Width - Height + 1, 2, toggleSize, toggleSize);
                using SolidBrush toggleBrush = new(toggleColorOnPosition);
                e.Graphics.FillEllipse(toggleBrush, toggleRect);
            }
            else //OFF
            {
                //Draw the control surface
                    if (solidStyle)
                    {
                        using SolidBrush offbckBrush = new(backColorOffPosition);
                        e.Graphics.FillPath(offbckBrush, this.GetFigurePath());
                    }
                else
                {
                    using Pen offbckPen = new(backColorOffPosition, 2);
                    e.Graphics.DrawPath(offbckPen, this.GetFigurePath());
                }
                //Draw the toggle
                Rectangle toggleRect = new(2, 2, toggleSize, toggleSize);
                using SolidBrush toggleBrush = new(toggleColorOffPosition);
                e.Graphics.FillEllipse(toggleBrush, toggleRect);
            }
        }
    }
}
