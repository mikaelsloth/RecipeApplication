namespace Recipe.Views
{
    using Recipe.Models.Db;
    using Recipe.Presenter;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IRecipeCardBrowsePartView : IView<IRecipeCardBrowsePartPresenter>
    {
        event AddingNewEventHandler? AddingNew;
        
        event EventHandler? ExpandedChanged;
        
        event EventHandler? PositionChanged;

        bool Expanded { get; set; }
        
        void SetCustomerName(string name);
        
        void SetDataSource(IList<RecipeCard> recipes);
        
        void SetRecipeCardDate(DateTime date);
    }
}