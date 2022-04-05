namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form, IMainFormView
    {
        private IMainformPresenter? presenter;

        public event EventHandler? CustomerButtonClick;

        public event EventHandler? MedicineButtonClick;

        public event EventHandler? AboutButtonClick;

        public event EventHandler? RecipesButtonClick;

        public event EventHandler? SettingsButtonClick;

        public MainForm()
        {
            InitializeComponent();
        }

        // implements IView.StartApplication
        public void StartApplication() => Application.Run(this);

        public IMainformPresenter Presenter
        {
            get => presenter ?? throw new InvalidOperationException();
            set => presenter = (IMainformPresenter)value;
        }

        public bool CustomerButtonChecked 
        { 
            get => CustomersButton.Checked; 
            set => CustomersButton.Checked = value; 
        }

        public bool RecipesButtonChecked 
        { 
            get => RecipesButton.Checked; 
            set => RecipesButton.Checked = value; 
        }

        public bool MedicinButtonChecked 
        { 
            get => MedicinsButton.Checked;
            set => MedicinsButton.Checked = value; 
        }

        public void LoadSubView<P>(IView<P> subview)
        {            
            Control c = tableLayoutPanel1.GetControlFromPosition(1, 0);
            if (c != null )
            {
                tableLayoutPanel1.Controls.Remove(c);
            }

            if (subview is Form subform)
            {
                subform.Dock = DockStyle.Fill;
                subform.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
                subform.TopLevel = false;
                tableLayoutPanel1.Controls.Add(subform, 1, 0);
                subform.Show();
            }
            else
            {
                throw new ArgumentException("The provided IView is not a Form", nameof(subview));
            }
        }

        private void CustomersButton_Clicked(object? sender, EventArgs e) => CustomerButtonClick?.Invoke(sender, e);

        private void RecipesButton_Clicked(object? sender, EventArgs e) => RecipesButtonClick?.Invoke(sender, e);

        private void MedicinsButton_Clicked(object? sender, EventArgs e) => MedicineButtonClick?.Invoke(sender, e);

        private void SettingsButton_Click(object? sender, EventArgs e) => SettingsButtonClick?.Invoke(sender, e);

        private void AboutLabel_Click(object? sender, EventArgs e) => AboutButtonClick?.Invoke(sender, e);

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Close();
    }
}
