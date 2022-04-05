namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using Recipe.Views;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class RecipeCardMainFormPresenter : PresenterBase<IRecipeCardMainFormView, IRecipeCardMainFormPresenter, IMainformPresenter>, IRecipeCardMainFormPresenter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IRecipeCardBrowsePartPresenter browsePart;
        private readonly IRecipeCardDetailPartPresenter detailsPart;
        private RecipeCard? recipeCard;

        public RecipeCardMainFormPresenter(IServiceProvider serviceProvider, IRecipeCardMainFormView view, IRecipeCardBrowsePartPresenter? browsePart, IRecipeCardDetailPartPresenter? detailsPart) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(RecipeCardMainFormPresenter)} class could not start due to a missing dependency");
            this.browsePart = browsePart ?? throw new ArgumentNullException(nameof(browsePart), $"The {nameof(RecipeCardMainFormPresenter)} class could not start due to a missing dependency");
            this.detailsPart = detailsPart ?? throw new ArgumentNullException(nameof(detailsPart), $"The {nameof(RecipeCardMainFormPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            LoadPartPresenters();
        }

        private void LoadPartPresenters()
        {
            if (browsePart is PresenterBase<IRecipeCardBrowsePartView, IRecipeCardBrowsePartPresenter, IRecipeCardMainFormPresenter> browsebase)
            {
                browsebase.Parent = this;
                View.LoadView(browsebase.View);
            }

            if (detailsPart is PresenterBase<IRecipeCardDetailPartView, IRecipeCardDetailPartPresenter, IRecipeCardMainFormPresenter> detailbase)
            {
                detailbase.Parent = this;
                View.LoadView(detailbase.View);
            }
        }

        public RecipeCard? RecipeCard
        {
            get => recipeCard;
            set
            {
                if (value == null) return;
                if (value.Id != (recipeCard == null ? -1 : recipeCard.Id))
                {
                    recipeCard = value;
                    bool editable = recipeCard.Date > DateTime.Now.AddDays(-7);
                    if (editable) detailsPart.SetState(RecipeCardDetailedState.Selected);
                    else detailsPart.SetState(RecipeCardDetailedState.ReadOnlySelected);
                }
            }
        }

        public void AddNew(object? sender, AddingNewEventArgs e)
        {
            //Check State is not dirty
            if (detailsPart.State == RecipeCardDetailedState.CardDirty) if (ShowDirtyMessage()) return;
            //If OK set state to new
            if (e.NewObject is null or not RecipeCard _) throw new InvalidOperationException($"The new object must be of type {typeof(RecipeCard)}");
            detailsPart.SetState(RecipeCardDetailedState.New);
        }

        private bool ShowDirtyMessage()
        {
            var result = MessageBox.Show("Der er ændringer i receptkortet der ikke er gemt.\r\n\r\nTryk Ja for at gemme. \r\n\r\nTryk Nej for at slette ændringer (som mistes derved)." +
                "\r\n\r\nTryk Annuller for at komme tilbage til receptkortet.", "Ikke gemte ændringer"
                , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                detailsPart.SaveClicked(this, EventArgs.Empty);
                return false;
            }
            else if (result == DialogResult.No)
            {
                detailsPart.CancelClicked(this, EventArgs.Empty);
                return false;
            }
            else
            {
                detailsPart.SetFocus();
                return true;
            }
        }

        //TODO
        public event EventHandler? ViewExpandedCollapsedChanged;

        public void HandleViewExpandedCollapsedChanged(object? sender, EventArgs e) =>
            //Get actual state, store it, and relay event
            ViewExpandedCollapsedChanged?.Invoke(sender, e);

        public bool BrowseViewExpandState
        {
            get;
            set;
        }
        //END TODO

        public override event FormClosingEventHandler? Close;

        public Customer GetCustomer() => Parent.SelectedCustomer ?? throw new InvalidOperationException();

        public override void OnClose(object? sender, FormClosingEventArgs e) => Close?.Invoke(sender, e);
    }
}
