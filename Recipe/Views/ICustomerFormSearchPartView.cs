namespace Recipe.Views
{
    using System;
    using System.Windows.Forms;
    using Recipe.Presenter;

    public interface ICustomerFormSearchPartView : IView<ICustomerFormSearchPartPresenter>
    {
        string NameText { get; set; }
        
        string PhoneText { get; set; }
        
        bool ResetVisibility { get; set; }

        bool SetFocus();

        void SetAutoCompleteOnNameTextBox(AutoCompleteStringCollection autoCompleteStrings);
        
        void SetAutoCompleteOnPhoneTextBox(AutoCompleteStringCollection autoCompleteStrings);

        event EventHandler? Reset;

        event EventHandler? New;

        event PreviewKeyDownEventHandler? NameKeyPreview;

        event PreviewKeyDownEventHandler? PhoneKeyPreview;

        event EventHandler? NameEnter;

        event EventHandler? PhoneEnter;

        event EventHandler? NameTextChanged;

        event EventHandler? PhoneTextChanged;
    }
}