namespace Recipe.Presenter
{
    using Recipe.Controller;
    using Recipe.Models.Db;
    using Recipe.Views;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class CustomerMainFormPresenter : PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter, IMainformPresenter>, ICustomerMainFormPresenter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ICustomerFormSearchPartPresenter searchPart;
        private readonly ICustomerFormDetailPartPresenter detailPart;
        private readonly ICustomerFormHistoryPartPresenter historyPart;
        private Customer? selectedCustomer;

        public CustomerMainFormPresenter(IServiceProvider serviceProvider,
                                         ICustomerMainFormView view,
                                         ICustomerFormSearchPartPresenter? searchPart,
                                         ICustomerFormDetailPartPresenter? detailPart,
                                         ICustomerFormHistoryPartPresenter? historyPart) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(CustomerMainFormPresenter)} class could not start due to a missing dependency");
            this.searchPart = searchPart ?? throw new ArgumentNullException(nameof(searchPart), $"The {nameof(CustomerMainFormPresenter)} class could not start due to a missing dependency");
            this.detailPart = detailPart ?? throw new ArgumentNullException(nameof(detailPart), $"The {nameof(CustomerMainFormPresenter)} class could not start due to a missing dependency");
            this.historyPart = historyPart ?? throw new ArgumentNullException(nameof(historyPart), $"The {nameof(CustomerMainFormPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            LoadPartPresenters();
        }

        private void LoadPartPresenters()
        {
            if (searchPart is PresenterBase<ICustomerFormSearchPartView, ICustomerFormSearchPartPresenter, ICustomerMainFormPresenter> searchbase)
            {
                searchbase.Parent = this;
                View.LoadView(searchbase.View, 1);
            }

            if (detailPart is PresenterBase<ICustomerFormDetailPartView, ICustomerFormDetailPartPresenter, ICustomerMainFormPresenter> detailbase)
            {
                detailbase.Parent = this;
                View.LoadView(detailbase.View, 3);
            }

            if (historyPart is PresenterBase<ICustomerFormHistoryPartView, ICustomerFormHistoryPartPresenter, ICustomerMainFormPresenter> historybase) {
                historybase.Parent = this;
                View.LoadView(historybase.View, 5);
            }
        }

        public void StateChanged(object? sender, EventArgs e)
        {
             if (detailPart.State is DetailedPartState.New or DetailedPartState.Selected or DetailedPartState.Dirty) searchPart.SetState(SearchPartState.Blank, false);
        }

        public override event FormClosingEventHandler? Close;

        public event EventHandler? NewButtonClicked;

        public event EventHandler? DataSetsRefreshed;

        public Customer? SelectedCustomer 
        { 
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                Parent.SelectedCustomer = value;
            }
        }

        #region Searching methods

        public virtual async Task SearchTextboxSearch(TextBox sender)
        {
            string text = sender.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                ICustomerController controller = serviceProvider.GetServiceType<ICustomerController>();

                Customer? customer = sender.Name switch
                {
                    "NameTextBox" => await controller.GetFromNameSearch(text),
                    "PhoneTextBox" => await controller.GetFromPhoneSearch(text),
                    _ => throw new ArgumentOutOfRangeException(nameof(sender), sender.Name, "This input textbox is not valid - contact IT development")
                };
                ProcessCustomerResult(customer);
            }
        }

        public async Task HistoryGridSearch(DataGridViewRow row)
        {
            int id = Convert.ToInt32(row.Cells["Id"].Value);
            ICustomerController controller = serviceProvider.GetServiceType<ICustomerController>();
            Customer? customer = await controller.Get(id);
            ProcessCustomerResult(customer);
        }

        private void ProcessCustomerResult(Customer? customer)
        {
            // if no results exist 
            if (customer == null)
            {
                ShowNoResultWarning();
                return;
            }

            SelectedCustomer = customer;
            detailPart.CustomerSelected(customer);
        }

        public virtual bool CheckPotentialStateConflict()
        {
            if (detailPart.State == DetailedPartState.Dirty) return ShowIsDirtyWarning();
            detailPart.SetState(DetailedPartState.Blank, false);
            return false;
        }

        private void ShowNoResultWarning()
        {
            var result = MessageBox.Show("Der blev ikke fundet nogen kunder ud fra søge data.\r\n\r\nØnsker du at oprette en ny kunde?\r\n\r\nTryk Ja for oprette en ny kunde. \r\n\r\nTryk Nej for at starte en ny søgning."
                , "Ingen søge resultater", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                NewButtonClick();
            }
            else
            {
                detailPart.SetState(DetailedPartState.Blank, false);
                searchPart.SetState(SearchPartState.Blank, true);
            }
        }

        private bool ShowIsDirtyWarning()
        {
            var result = MessageBox.Show("Der er ændringer i den valgte kunde der ikke er gemt.\r\n\r\nTryk Ja for at gemme. \r\n\r\nTryk Nej for at slette ændringer (som mistes derved)." +
                "\r\n\r\nTryk Annuller for at komme tilbage til den valgte kunde.", "Ikke gemte ændringer"
                , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                detailPart.SaveClicked(this, new());
                return false;
            }
            else if (result == DialogResult.No)
            {
                detailPart.CancelClicked(this, new());
                return false;
            }
            else
            {
                detailPart.SetFocus();
                return true;
            }
        }

        #endregion

        #region CRUD operation methods

        public virtual void NewButtonClick()
        {
            if (CheckPotentialStateConflict() == false)
            {
                //searchPart.SetState(SearchPartState.Blank, false);
                NewButtonClicked?.Invoke(this, new());
            }
        }

        #endregion

        public override void OnClose(object? sender, FormClosingEventArgs e) => Close?.Invoke(this, e);

        public void DataSetsRefresh(object? sender, EventArgs e) => DataSetsRefreshed?.Invoke(sender, e);
    }
}
