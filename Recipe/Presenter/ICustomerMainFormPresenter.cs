namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public interface ICustomerMainFormPresenter : IPresenter
    {
        void StateChanged(object? sender, EventArgs e);

        Task SearchTextboxSearch(TextBox sender);

        Task HistoryGridSearch(DataGridViewRow row);

        void NewButtonClick();

        event EventHandler? NewButtonClicked;

        bool CheckPotentialStateConflict();

        void DataSetsRefresh(object? sender, EventArgs e);

        event EventHandler? DataSetsRefreshed;

        Customer? SelectedCustomer { get; set; }
    }
}
