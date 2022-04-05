namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using System;

    public interface IMainformPresenter : IPresenter
    {
        void StartApplication();

        Customer? SelectedCustomer { get; set; }

        void LoadDefaultForm(object? sender, EventArgs e);

        void ShowSettings(object? sender, EventArgs e);

        void ShowRecipes(object? sender, EventArgs e);

        void ShowMedicine(object? sender, EventArgs e);

        void ShowAbout(object? sender, EventArgs e);

        void ShowCustomers(object? sender, EventArgs e);
    }
}
