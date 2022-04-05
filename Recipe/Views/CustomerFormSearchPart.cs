namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;

    public partial class CustomerFormSearchPart : UserControl, ICustomerFormSearchPartView
    {
        private ICustomerFormSearchPartPresenter? presenter;
        public CustomerFormSearchPart()
        {
            InitializeComponent();
        }

        private void ResetButton_Click(object sender, EventArgs e) => Reset?.Invoke(sender, e);

        public event EventHandler? Reset;

        private void NewButton_Click(object sender, EventArgs e) => New?.Invoke(sender, e);

        public event EventHandler? New;

        private void NameTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => NameKeyPreview?.Invoke(sender, e);

        public event PreviewKeyDownEventHandler? NameKeyPreview;

        private void PhoneTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => PhoneKeyPreview?.Invoke(sender, e);

        public event PreviewKeyDownEventHandler? PhoneKeyPreview;

        private void NameTextBox_Enter(object sender, EventArgs e) => NameEnter?.Invoke(sender, e);

        public event EventHandler? NameEnter;

        private void PhoneTextBox_Enter(object sender, EventArgs e) => PhoneEnter?.Invoke(sender, e);

        public event EventHandler? PhoneEnter;

        private void NameTextBox_TextChanged(object sender, EventArgs e) => NameTextChanged?.Invoke(sender, e);

        public event EventHandler? NameTextChanged;

        private void PhoneTextBox_TextChanged(object sender, EventArgs e) => PhoneTextChanged?.Invoke(sender, e);

        public event EventHandler? PhoneTextChanged;

        public void SetAutoCompleteOnNameTextBox(AutoCompleteStringCollection autoCompleteStrings) => SetAutoCompleteStrings(NameTextBox, autoCompleteStrings);

        public void SetAutoCompleteOnPhoneTextBox(AutoCompleteStringCollection autoCompleteStrings) => SetAutoCompleteStrings(PhoneTextBox, autoCompleteStrings);

        private static void SetAutoCompleteStrings(TextBox textBox, AutoCompleteStringCollection autoCompleteStrings)
        {
            textBox.AutoCompleteMode = AutoCompleteMode.None;
            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = autoCompleteStrings;
        }

        public string NameText
        {
            get => NameTextBox.Text;
            set => NameTextBox.Text = value;
        }

        public string PhoneText
        {
            get => PhoneTextBox.Text;
            set => PhoneTextBox.Text = value;
        }

        public bool ResetVisibility
        {
            get => ResetButton.Visible;
            set => ResetButton.Visible = value;
        }

        public bool SetFocus() => NameTextBox.Focus();

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Dispose(true);

        public ICustomerFormSearchPartPresenter Presenter 
        { 
            get => presenter ?? throw new InvalidOperationException();
            set => presenter = value; 
        }
    }
}
