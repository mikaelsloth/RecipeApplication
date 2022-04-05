namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;

    public interface IMainFormView : IView<IMainformPresenter>
    {
        void StartApplication();

        event FormClosingEventHandler FormClosing;

        event EventHandler? Load;

        event EventHandler? CustomerButtonClick;

        event EventHandler? RecipesButtonClick;

        event EventHandler? MedicineButtonClick;

        event EventHandler? SettingsButtonClick;

        event EventHandler? AboutButtonClick;

        bool CustomerButtonChecked { get; set; }

        bool RecipesButtonChecked { get; set; }

        bool MedicinButtonChecked { get; set; }

        public void LoadSubView<P>(IView<P> view);
    }
}
