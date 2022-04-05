namespace Recipe.Views
{
    using Recipe.Presenter;

    public interface IRecipeCardMainFormView : IView<IRecipeCardMainFormPresenter>
    {
        void LoadView<P>(IView<P> view);
    }
}