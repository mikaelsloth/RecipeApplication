namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [DefaultEvent("Click")]
    public class ExpandButton : UserControl
    {
        private const int iconWidht = 14;
        private const int iconHeight = 6;
        private readonly EventArgs ev = new();

        private bool expanded = false;
        private int iconDrawingWidth = 2;

        public ExpandButton()
        {
            Size = new(30, 30);
        }

        public bool Expanded
        {
            get => expanded;
            set
            {
                expanded = value;
                OnExpandedChanged(ev);
            }
        }

        public int IconDrawingWidth
        {
            get => iconDrawingWidth;
            set
            {
                iconDrawingWidth = value > 5 ? 5 : value;
                Invalidate();
            }
        }

        public event EventHandler? ExpandedChanged;

        protected virtual void OnExpandedChanged(EventArgs e)
        {
            ExpandedChanged?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Fields
            Rectangle rectIcon = new((Width - iconWidht) / 2, (Height - iconHeight) / 2, iconWidht, iconHeight);
            Graphics graph = e.Graphics;

            if (!expanded)
            {
                //Draw arrow down icon
                using GraphicsPath pathupper = new();
                using Pen penupper = new(ForeColor, iconDrawingWidth);
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                pathupper.AddLine(rectIcon.X, rectIcon.Y + 5, rectIcon.X + (iconWidht / 2), rectIcon.Bottom + 5);
                pathupper.AddLine(rectIcon.X + (iconWidht / 2), rectIcon.Bottom + 5, rectIcon.Right, rectIcon.Y + 5);
                graph.DrawPath(penupper, pathupper);
                using GraphicsPath pathlower = new();
                using Pen penlower = new(ForeColor, iconDrawingWidth);
                pathlower.AddLine(rectIcon.X, rectIcon.Y - 5, rectIcon.X + (iconWidht / 2), rectIcon.Bottom - 5);
                pathlower.AddLine(rectIcon.X + (iconWidht / 2), rectIcon.Bottom - 5, rectIcon.Right, rectIcon.Y - 5);
                graph.DrawPath(penlower, pathlower);
            }
            else
            {
                //Draw arrow up icon
                using GraphicsPath pathupper = new();
                using Pen penupper = new(ForeColor, iconDrawingWidth);
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                pathupper.AddLine(rectIcon.X, rectIcon.Bottom + 5, rectIcon.X + (iconWidht / 2), rectIcon.Y + 5);
                pathupper.AddLine(rectIcon.X + (iconWidht / 2), rectIcon.Y + 5, rectIcon.Right, rectIcon.Bottom + 5);
                graph.DrawPath(penupper, pathupper);
                using GraphicsPath pathlower = new();
                using Pen penlower = new(ForeColor, iconDrawingWidth);
                pathlower.AddLine(rectIcon.X, rectIcon.Bottom - 5, rectIcon.X + (iconWidht / 2), rectIcon.Y - 5);
                pathlower.AddLine(rectIcon.X + (iconWidht / 2), rectIcon.Y - 5, rectIcon.Right, rectIcon.Bottom - 5);
                graph.DrawPath(penlower, pathlower);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            Expanded = !expanded;
            base.OnClick(e);
        }

        //[Browsable(false)]
        //private new string Text
        //{
        //    get;
        //    set;
        //}

    }
}
