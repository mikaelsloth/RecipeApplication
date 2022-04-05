namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;
    using static System.Windows.Forms.Control;

    public interface ICustomerFormDetailPartView : IView<ICustomerFormDetailPartPresenter>
    {
        bool SetFocus();

        event EventHandler? TextboxTextChanged;

        event EventHandler? TextboxLeave;

        event EventHandler? GdprCheckChanged;

        event EventHandler? SaveClicked;

        event EventHandler? DeleteClicked;

        event EventHandler? CancelClicked;

        event EventHandler? PrintGdprClicked;

        event EventHandler? RemarksClicked;

        void TextBox_TextChanged(object? sender, EventArgs e);

        void TextBox_Leave(object sender, EventArgs e);

        void GdprCheckBox_CheckedChanged(object? sender, EventArgs e);

        ControlCollection GetAllControls { get; }

        bool SaveButtonVisible { get; set; }

        bool CancelButtonVisible { get; set; }

        bool DeleteButtonVisible { get; set; }

        bool RemarksButtonEnabled { get; set; }

        bool RemarksButtonVisible { get; set; }

        bool GdprButtonVisible { get; set; }

        object? BindingSourceData { get; set; }

        BindingSource BindingSource { get; }

        void SetError(Control control, string? errorMessage);
    }
}
