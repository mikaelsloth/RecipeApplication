namespace Recipe.Views
{
    using Recipe.Models.Db;
    using Recipe.Presenter;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface ICustomerFormHistoryPartView : IView<ICustomerFormHistoryPartPresenter>
    {
        void SetGridDataSource(IList<LatestCustomersView> datasource);

        void SetNumberDropDownDataSource(IList<int> datasource);

        int GetMaxNumberOfRecords { get; }

        void AddPaginationButton(Button button, int position);

        void ClearAllPaginationButtons();

        event EventHandler? SelectedRowChanged;

        event EventHandler? SelectedDropDownValueChanged;

        void SetNumberDropDownInitialSelected(int historyDropDownSelected);

        DataGridViewColumnCollection GetAllDataGridColumns();

        void ClearGridSelection();
    }
}
