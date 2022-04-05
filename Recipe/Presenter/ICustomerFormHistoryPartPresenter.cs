namespace Recipe.Presenter
{
    using System;

    public interface ICustomerFormHistoryPartPresenter : IPresenter
    {
        void PageButton_Click(object? sender, EventArgs e);

        void GridView_SelectionChanged(object? sender, EventArgs e);

        void CountComboBox_SelectedValueChanged(object? sender, EventArgs e);

        void DatasetsUpdated(object? sender, EventArgs e);
    }
}
