namespace Recipe.Presenter
{
    using System;
    using System.Windows.Forms;

    public interface ICustomerFormSearchPartPresenter : IPresenter, ICustomerFormSubPart
    {
        void ResetButtonClick(object? sender, EventArgs e);

        void NewButtonClick(object? sender, EventArgs e);

        void SearchTextChanged(object? sender, EventArgs e);

        void SearchBoxEnter(object? sender, EventArgs e);

        void SearchTextboxEnterKeyPressed(object? sender, PreviewKeyDownEventArgs e);

        void DatasetsUpdated(object? sender, EventArgs e);

        void SetState(SearchPartState state, bool setfocus);

        SearchPartState State { get; }
    }
}
