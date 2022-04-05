namespace Recipe.Views
{
    using Recipe.Presenter;

    public interface ICustomerMainFormView : IView<ICustomerMainFormPresenter>
    {
        void LoadView<P>(IView<P> view, int position);
    }
}
