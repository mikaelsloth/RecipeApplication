namespace Recipe.Presenter
{
    using Recipe.Views;
    using System;

    public abstract class PresenterBase<TheIView, TheIPresenter, TheIPresenterParent> : PresenterBaseNoParent<TheIView, TheIPresenter>, IPresenter where TheIView : IView<TheIPresenter> where TheIPresenter : IPresenter
    {
        private TheIPresenterParent? parent;

        public virtual TheIPresenterParent Parent 
        { 
            get => parent ?? throw new InvalidOperationException(nameof(Parent)); 
            set => parent = value; 
        }

        protected PresenterBase(TheIView? view) : base(view)
        { }
    }
}