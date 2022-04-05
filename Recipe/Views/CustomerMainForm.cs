namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class CustomerMainForm : Form, ICustomerMainFormView
    {
        private ICustomerMainFormPresenter? presenter = null;

        public ICustomerMainFormPresenter Presenter
        {
            get => presenter ?? throw new InvalidOperationException();
            set => presenter = value;
        }

        public CustomerMainForm()
        {
            InitializeComponent();
        }

        #region General Design Functionality

        private void MainLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 | e.Row == 2 | e.Row == 4)
            {
                using Brush brush = Brushes.Red;
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }
            else
            {
                using Brush brush = Brushes.White;
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }
        }

        public void LoadView<P>(IView<P> view, int position)
        {
            if (position is not 1 and not 3 and not 5) throw new ArgumentOutOfRangeException(nameof(position));
            Control c = MainLayoutPanel.GetControlFromPosition(0, position);
            if (c != null)
                MainLayoutPanel.Controls.Remove(c);

            if (view is UserControl userControl)
            {
                userControl.Dock = DockStyle.Fill;
                userControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
                MainLayoutPanel.Controls.Add(userControl, 0, position);
            }
        }

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Close();

        #endregion
    }
}
