namespace Recipe.Presenter
{
    using Recipe.Views;
    using System;
    using System.Windows.Forms;

    public abstract class PresenterBaseNoParent<TheIView, TheIPresenter> : IPresenter where TheIView : IView<TheIPresenter> where TheIPresenter : IPresenter
    {

        public abstract event FormClosingEventHandler? Close;

        public abstract void OnClose(object? sender, FormClosingEventArgs e);

        public TheIView View { get; }

        protected PresenterBaseNoParent(TheIView? view) => View = view ?? throw new ArgumentNullException(nameof(view), "The PresenterBase class could not start due to a missing dependency");
    }
}
