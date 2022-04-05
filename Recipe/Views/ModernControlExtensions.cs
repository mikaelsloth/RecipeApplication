namespace Recipe.Views
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    internal static class ModernControlExtensions
    {
        public static GraphicsPath GetRoundedPath(this Rectangle rect, float radius)
        {
            GraphicsPath path = new();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void ControlRegionAndBorder(this Control control, float radius, Graphics graph, Color borderColor)
        {
            using GraphicsPath roundPath = control.ClientRectangle.GetRoundedPath(radius);
            using Pen penBorder = new(borderColor, 1);
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            control.Region = new(roundPath);
            graph.DrawPath(penBorder, roundPath);
        }

        public static GraphicsPath GetFigurePath(this CheckBox checkBox)
        {
            int arcSize = checkBox.Height - 1;
            Rectangle leftArc = new(0, 0, arcSize, arcSize);
            Rectangle rightArc = new(checkBox.Width - arcSize - 2, 0, arcSize, arcSize);

            GraphicsPath path = new();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(rightArc, 270, 180);
            path.CloseFigure();

            return path;
        }

        public static GraphicsPath GetRoundedPath(this RectangleF rect, float radius)
        {
            GraphicsPath path = new();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
