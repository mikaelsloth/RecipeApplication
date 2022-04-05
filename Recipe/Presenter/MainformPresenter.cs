namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using Recipe.Views;
    using System;
    using System.Windows.Forms;

    public class MainformPresenter : PresenterBaseNoParent<IMainFormView, IMainformPresenter>, IMainformPresenter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IPresenter[] childPresenters;

        public MainformPresenter(IServiceProvider? serviceProvider, IMainFormView? view, ICustomerMainFormPresenter? customerpresenter) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), "The MainformPresenter class could not start due to a missing dependency");
            childPresenters = new IPresenter[2];
            childPresenters[0] = customerpresenter ?? throw new ArgumentNullException(nameof(serviceProvider), "The MainformPresenter class could not start due to a missing dependency");
            if (customerpresenter is PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter, IMainformPresenter> presenter) presenter.Parent = this;
            View.Presenter = this;
            WireEvents();
        }

        public override event FormClosingEventHandler? Close;

        private void WireEvents()
        {
            View.Load += LoadDefaultForm;
            View.FormClosing += OnClose;
            View.CustomerButtonClick += ShowCustomers;
            View.AboutButtonClick += ShowAbout;
            View.MedicineButtonClick += ShowMedicine;
            View.RecipesButtonClick += ShowRecipes;
            View.SettingsButtonClick += ShowSettings;
            Close += childPresenters[0].OnClose;
        }

        public void StartApplication() => View.StartApplication();

        public virtual Customer? SelectedCustomer { get; set; } = null;

        public void LoadDefaultForm(object? sender, EventArgs e) => ShowCustomers(sender, e);

        public override void OnClose(object? sender, FormClosingEventArgs e)
        {
            Close?.Invoke(sender, e);
            if (e.Cancel) Stop();
        }

        public void ShowAbout(object? sender, EventArgs e)
        {
            //var obj = serviceProvider.GetService(typeof(AboutForm));
            //if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(AboutForm), "Not defined by ServiceProvider - call IT development");
            //AboutForm resolved = (AboutForm)obj;
            //resolved.Show();
        }

        public void ShowCustomers(object? sender, EventArgs e)
        {
            if (childPresenters[0] is PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter, IMainformPresenter> presenter) View.LoadSubView(presenter.View);
            View.CustomerButtonChecked = true;
            View.MedicinButtonChecked = false;
            View.RecipesButtonChecked = false;
            //{
            //    var obj = serviceProvider.GetService(typeof(PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter>)) ?? throw new ArgumentOutOfRangeException("type", nameof(PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter>), "Not defined by ServiceProvider - call IT development");
            //    PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter> resolved = (PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter>)obj;
            //    resolved.Parent = this;
            //    childPresenters[0] = (ICustomerMainFormPresenter)resolved;
            //    View.LoadSubView(resolved.View);
            //}
        }

        public void ShowMedicine(object? sender, EventArgs e)
        {
            View.CustomerButtonChecked = false;
            View.MedicinButtonChecked = true;
            View.RecipesButtonChecked = false;
            //var obj = serviceProvider.GetService(typeof(MedicineMainForm));
            //if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(MedicineMainForm), "Not defined by ServiceProvider - call IT development");
            //MedicineMainForm resolved = (MedicineMainForm)obj;
            //parent.LoadSubForm(resolved);
        }

        public void ShowRecipes(object? sender, EventArgs e)
        {
            if (SelectedCustomer == null) ShowMissingCustomerMessage();
            View.CustomerButtonChecked = false;
            View.MedicinButtonChecked = false;
            View.RecipesButtonChecked = true;
            //var obj = serviceProvider.GetService(typeof(RecipesMainForm));
            //if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(RecipesMainForm), "Not defined by ServiceProvider - call IT development");
            //RecipesMainForm resolved = (RecipesMainForm)obj;
            //parent.LoadSubForm(resolved);
        }

        private void ShowMissingCustomerMessage() => throw new NotImplementedException();

        public void ShowSettings(object? sender, EventArgs e)
        {
            //var obj = serviceProvider.GetService(typeof(SettingsForm));
            //if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(SettingsForm), "Not defined by ServiceProvider - call IT development");
            //SettingsForm resolved = (SettingsForm)obj;
            //resolved.Show();
        }

        private static void Stop() => Application.Exit();
    }
}
