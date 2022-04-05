namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using System;

    public interface ICustomerFormDetailPartPresenter : IPresenter, ICustomerFormSubPart
    {
        void ModelTextChanged(object? sender, EventArgs e);

        void ModelCheckedChanged(object? sender, EventArgs e);

        void ModelTextBoxLeave(object? sender, EventArgs e);

        void SaveClicked(object? sender, EventArgs e);

        void DeleteClicked(object? sender, EventArgs e);

        void CancelClicked(object? sender, EventArgs e);

        void PrintGdprClicked(object? sender, EventArgs e);

        void RemarksClicked(object? sender, EventArgs e);

        void NewButtonClicked(object? sender, EventArgs e);

        void CustomerSelected(Customer customer);

        void SetState(DetailedPartState value, bool setFocus);

        DetailedPartState State { get; }

        event EventHandler? DataSourceChanged;

        event EventHandler? StateChanged;
    }
}
