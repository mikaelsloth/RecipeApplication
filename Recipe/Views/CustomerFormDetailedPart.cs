namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class CustomerFormDetailedPart : UserControl, ICustomerFormDetailPartView
    {
        ICustomerFormDetailPartPresenter? presenter;

        public CustomerFormDetailedPart()
        {
            InitializeComponent();
        }

        public ControlCollection GetAllControls => tableLayoutPanel1.Controls;

        public bool SaveButtonVisible
        {
            get => SaveButton.Visible;
            set => SaveButton.Visible = value;
        }

        public bool CancelButtonVisible
        {
            get => CancelButton.Visible;
            set => CancelButton.Visible = value;
        }

        public bool DeleteButtonVisible
        {
            get => DeleteButton.Visible;
            set => DeleteButton.Visible = value;
        }

        public bool GdprButtonVisible
        {
            get => GdprButton.Visible;
            set => GdprButton.Visible = value;
        }

        public object? BindingSourceData
        {
            get => CustomerBindingSource.DataSource;
            set => CustomerBindingSource.DataSource = value;
        }

        public ICustomerFormDetailPartPresenter Presenter
        {
            get => presenter ?? throw new ArgumentNullException(nameof(Presenter));
            set => presenter = value;
        }

        public bool RemarksButtonEnabled
        {
            get => RemarksButton.Enabled;
            set => RemarksButton.Enabled = value;
        }

        public bool RemarksButtonVisible
        {
            get => RemarksButton.Visible;
            set => RemarksButton.Visible = value;
        }

        public BindingSource BindingSource => CustomerBindingSource;

        public event EventHandler? TextboxTextChanged;

        public event EventHandler? TextboxLeave;

        public event EventHandler? GdprCheckChanged;
        
        public event EventHandler? SaveClicked;
        
        public event EventHandler? DeleteClicked;
        
        public event EventHandler? CancelClicked;
        
        public event EventHandler? PrintGdprClicked;
        
        public event EventHandler? RemarksClicked;

        public void SetError(Control control, string? errorMessage) => ErrorProvider.SetError(control, errorMessage);

        public bool SetFocus() => NameTextBox.Focus();

        #region Design Part
        #endregion

        public void TextBox_TextChanged(object? sender, EventArgs e) => TextboxTextChanged?.Invoke(sender, e);

        public void TextBox_Leave(object sender, EventArgs e) => TextboxLeave?.Invoke(sender, e);

        public void GdprCheckBox_CheckedChanged(object? sender, EventArgs e) => GdprCheckChanged?.Invoke(sender, e);

        private void SaveButton_Click(object sender, EventArgs e) => SaveClicked?.Invoke(sender, e);

        private void CancelButton_Click(object sender, EventArgs e) => CancelClicked?.Invoke(sender, e);

        private void DeleteButton_Click(object sender, EventArgs e) => DeleteClicked?.Invoke(sender, e);

        private void GdprButton_Click(object sender, EventArgs e) => PrintGdprClicked?.Invoke(sender, e);

        private void RemarksButton_Click(object sender, EventArgs e) => RemarksClicked?.Invoke(sender, e);

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Dispose(true);
    }
}
