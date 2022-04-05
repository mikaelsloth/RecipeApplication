namespace Recipe.Presenter
{
    using Recipe.Controller;
    using Recipe.Models.Db;
    using Recipe.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class RecipeCardBrowsePartPresenter : PresenterBase<IRecipeCardBrowsePartView, IRecipeCardBrowsePartPresenter, IRecipeCardMainFormPresenter>, IRecipeCardBrowsePartPresenter
    {
        private readonly IServiceProvider serviceProvider;

        public RecipeCardBrowsePartPresenter(IServiceProvider serviceProvider, IRecipeCardBrowsePartView? view) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(CustomerFormDetailPartPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            WireEvents();
        }

        private void WireEvents() => View.ExpandedChanged += View_ExpandedCollapsedChanged;

        public override IRecipeCardMainFormPresenter Parent
        {
            get => base.Parent;
            set
            {
                base.Parent = value;
                SetInitialConfiguration();
            }
        }

        private async void SetInitialConfiguration()
        {
            View.SetCustomerName(Parent.GetCustomer().Name);
            //Get RecipeCards
            IList<RecipeCard> data = await GetRecipeCards();
            View.SetDataSource(data);
            if (data.Count > 0)
                View.SetRecipeCardDate(data[0].Date);
            // Now we can wire the events            
            View.AddingNew += BindingSource_AddingNew;
            View.PositionChanged += BindingSource_PositionChanged;
            AddingNew += Parent.AddNew;
            Parent.Close += OnClose;
        }

        private async Task<IList<RecipeCard>> GetRecipeCards()
        {
            IRecipeCardController controller = serviceProvider.GetServiceType<IRecipeCardController>();
            return await controller.GetByCustomer(Parent.GetCustomer().Id);
        }

        public event AddingNewEventHandler? AddingNew;

        public event EventHandler? ViewExpandedCollapsedChanged;

        public override event FormClosingEventHandler? Close;

        public void BindingSource_AddingNew(object? sender, AddingNewEventArgs e)
        {
            if (e.NewObject is null || e.NewObject is not RecipeCard rc)
            {
                e.NewObject = new RecipeCard()
                {
                    CustomerId = Parent.GetCustomer().Id,
                    Date = DateTime.Today
                };
            }
            else
            {
                rc.CustomerId = Parent.GetCustomer().Id;
                rc.Date = DateTime.Today;
            }

            AddingNew?.Invoke(sender, e);
        }

        public void BindingSource_PositionChanged(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                if (sender is BindingSource bs)
                {
                    Parent.RecipeCard = (RecipeCard)bs.Current;
                }
            }
        }

        public override void OnClose(object? sender, FormClosingEventArgs e) => Close?.Invoke(sender, e);

        public void View_ExpandedCollapsedChanged(object? sender, EventArgs e) => ViewExpandedCollapsedChanged?.Invoke(sender, e);
    }
}
