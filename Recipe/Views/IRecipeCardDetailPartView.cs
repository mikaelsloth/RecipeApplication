namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;

    public interface IRecipeCardDetailPartView : IView<IRecipeCardDetailPartPresenter>
    {
        object? DataSource { get; set; }
       
        bool ReadOnly { get; set; }

        event EventHandler? PrintRecipe;
        
        event EventHandler? RemarkChanged;
        
        event EventHandler? AddLineRequest;
        
        event EventHandler? DeleteLineRequest;
        
        event EventHandler? EditLineRequest;

        int AddLine(RecipeLineView recipeLine, MedicinTypes types);
        
        void DeleteLine(RecipeLineView recipeLine, MedicinTypes types);
        
        Control.ControlCollection GetGridControlCollection(MedicinTypes types);
    }
}
