namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using System;

    public interface IRecipeCardDetailPartPresenter : IPresenter
    {
        RecipeCardDetailedState State { get; }

        void SetState(RecipeCardDetailedState state);
        
        void SaveClicked(object? sender, EventArgs e);
        
        void CancelClicked(object? sender, EventArgs e);
        
        void SetFocus();
    }
}
