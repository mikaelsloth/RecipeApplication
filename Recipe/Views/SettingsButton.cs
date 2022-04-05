namespace Recipe.Views
{
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class SettingsButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new();
            grPath.AddEllipse(1, 1, ClientSize.Width - 4, ClientSize.Height - 4);
            Region = new(grPath);
            base.OnPaint(e);
        }
    }
}
