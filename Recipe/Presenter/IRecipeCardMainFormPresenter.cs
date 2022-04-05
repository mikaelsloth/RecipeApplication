namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using System.ComponentModel;

    public interface IRecipeCardMainFormPresenter : IPresenter
    {
        RecipeCard? RecipeCard { get; set; }

        void AddNew(object? sender, AddingNewEventArgs e);
        Customer GetCustomer();
    }
}