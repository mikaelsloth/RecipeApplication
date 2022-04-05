namespace Recipe.Presenter
{
    using Recipe.Controller;
    using Recipe.Models.Db;
    using Recipe.Views;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class CustomerFormSearchPartPresenter : PresenterBase<ICustomerFormSearchPartView, ICustomerFormSearchPartPresenter, ICustomerMainFormPresenter>, ICustomerFormSearchPartPresenter
    {
        private readonly IServiceProvider serviceProvider;

        public SearchPartState State { get; private set; } = SearchPartState.NotDefined;

        public override event FormClosingEventHandler? Close;

        public CustomerFormSearchPartPresenter(IServiceProvider serviceProvider, ICustomerFormSearchPartView view) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(CustomerMainFormPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            WireEvents();
        }

        public override ICustomerMainFormPresenter Parent
        {
            get => base.Parent;
            set
            {
                base.Parent = value;
                SetInitialConfiguration();
            }
        }

        private void SetInitialConfiguration()
        {
            Parent.DataSetsRefreshed += DatasetsUpdated;
            UpdateDataSets();
            SetState(SearchPartState.Blank, true);
        }

        private void WireEvents()
        {
            View.NameEnter += SearchBoxEnter;
            View.NameKeyPreview += SearchTextboxEnterKeyPressed;
            View.NameTextChanged += SearchTextChanged;
            View.New += NewButtonClick;
            View.PhoneEnter += SearchBoxEnter;
            View.PhoneKeyPreview += SearchTextboxEnterKeyPressed;
            View.PhoneTextChanged += SearchTextChanged;
            View.Reset += ResetButtonClick;
            Close += View.Close_Clicked;
        }

        public void SetState(SearchPartState state, bool setfocus = false)
        {
            SearchPartState oldstate = State;
            if (state != oldstate)
            {
                switch (state)
                {
                    case SearchPartState.NotDefined:
                        throw new InvalidOperationException();
                    case SearchPartState.Blank:
                        View.NameText = string.Empty;
                        View.PhoneText = string.Empty;
                        View.ResetVisibility = false;
                        if (setfocus) _ = View.SetFocus();
                        break;
                    case SearchPartState.Searching:
                        View.ResetVisibility = true;
                        break;
                    default:
                        break;
                }

                State = state;  
            }
        }

        public void NewButtonClick(object? sender, EventArgs e) => Parent.NewButtonClick();

        public override void OnClose(object? sender, FormClosingEventArgs e) => Close?.Invoke(sender, e);

        public void ResetButtonClick(object? sender, EventArgs e) => SetState(SearchPartState.Blank, true);

        public void SearchBoxEnter(object? sender, EventArgs e) => Parent.CheckPotentialStateConflict();

        public async void SearchTextboxEnterKeyPressed(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab || e.KeyData == Keys.Shift | e.Shift)
            {
                e.IsInputKey = false;
                if (Parent == null) throw new InvalidOperationException();
                if (sender is not TextBox textBox) throw new InvalidOperationException();
                await Parent.SearchTextboxSearch(textBox);
            }
        }

        public void SearchTextChanged(object? sender, EventArgs e) => SetState(SearchPartState.Searching);

        public void DatasetsUpdated(object? sender, EventArgs e) => UpdateDataSets();

        private async void UpdateDataSets()
        {
            var result1 = await UpdateNameList(); ;
            View.SetAutoCompleteOnNameTextBox(result1);
            var result2 = await UpdatePhoneList();
            View.SetAutoCompleteOnPhoneTextBox(result2);
        }

        private async Task<AutoCompleteStringCollection> UpdateNameList()
        {
            ICustomerNameSearchController controller = serviceProvider.GetServiceType<ICustomerNameSearchController>();

            return await GetAutoCompleteData(controller);
        }

        private async Task<AutoCompleteStringCollection> UpdatePhoneList()
        {
            ICustomerPhoneSearchController controller = serviceProvider.GetServiceType<ICustomerPhoneSearchController>();

            return await GetAutoCompleteData(controller);
        }

        private static async Task<AutoCompleteStringCollection> GetAutoCompleteData<T>(IAutoCompleteList<T> controller) where T : IAutoCompleteTextView
        {
            AutoCompleteStringCollection results = new();
            var list = await controller.GetAll();
            foreach (var item in list)
            {
                _ = results.Add(item.AutoCompleteText);
            }

            return results;
        }

        public void SetFocus() => View.SetFocus();
    }
}
