namespace Recipe.Presenter
{
    using Recipe.Controller;
    using Recipe.Models.Db;
    using Recipe.Properties;
    using Recipe.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class CustomerFormHistoryPartPresenter : PresenterBase<ICustomerFormHistoryPartView, ICustomerFormHistoryPartPresenter, ICustomerMainFormPresenter>, ICustomerFormHistoryPartPresenter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly int customerHistoryPagerSpan;
        private readonly int customerHistoryGridCount;

        public CustomerFormHistoryPartPresenter(IServiceProvider serviceProvider, ICustomerFormHistoryPartView? view) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(CustomerMainFormPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            customerHistoryPagerSpan = Settings.Default.CustomerHistoryPagerSpan;
            customerHistoryGridCount = Settings.Default.CustomerHistoryGridCount;
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

        private async void SetInitialConfiguration()
        {
            await UpdateData();
            // Now we can wire the events
            View.SelectedDropDownValueChanged += CountComboBox_SelectedValueChanged;
            View.SelectedRowChanged += GridView_SelectionChanged;

            Parent.DataSetsRefreshed += DatasetsUpdated;
        }

        private void WireEvents()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntArray));
            IntArray dropdownList = (IntArray)converter.ConvertFrom(Settings.Default.HistoryDropDownList);
            View.SetNumberDropDownDataSource(dropdownList.ToList());
            View.SetNumberDropDownInitialSelected(Settings.Default.HistoryDropDownSelected);
        }

        public override event FormClosingEventHandler? Close;

        public override void OnClose(object? sender, FormClosingEventArgs e) => Close?.Invoke(sender, e);

        private async Task UpdateData(int pageIndex = 1)
        {
            object obj = serviceProvider.GetService(typeof(ICustomerHistoryController)) ?? throw new ArgumentOutOfRangeException("type", nameof(ICustomerHistoryController), "Not defined by ServiceProvider - call IT development");
            ICustomerHistoryController controller = (ICustomerHistoryController)obj;

            PagedResult<LatestCustomersView> pagedResult = await controller.GetPage(pageIndex, customerHistoryGridCount, View.GetMaxNumberOfRecords);

            View.SelectedRowChanged -= GridView_SelectionChanged;

            View.SetGridDataSource(pagedResult.Results);
            foreach (DataGridViewTextBoxColumn item in View.GetAllDataGridColumns())
            {
                item.DataPropertyName = item.Name != "FullName" ? item.Name : "Name";
            }

            View.SelectedRowChanged += GridView_SelectionChanged;

            PopulatePager(pagedResult);
        }

        private void PopulatePager(PagedResult<LatestCustomersView> pagedResult)
        {
            int startIndex, endIndex;

            IList<Page> pages = new List<Page>();

            startIndex = pagedResult.CurrentPage > 1 && pagedResult.CurrentPage + customerHistoryPagerSpan - 1 < customerHistoryPagerSpan ? pagedResult.CurrentPage : 1;
            endIndex = pagedResult.PageCount > customerHistoryPagerSpan ? customerHistoryPagerSpan : pagedResult.PageCount;
            endIndex = pagedResult.CurrentPage > customerHistoryPagerSpan % 2
                ? pagedResult.CurrentPage == 2 ? 5 : pagedResult.CurrentPage + 2
                : customerHistoryPagerSpan - pagedResult.CurrentPage + 1;

            if (endIndex - (customerHistoryPagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (customerHistoryPagerSpan - 1);
            }

            if (endIndex > pagedResult.PageCount)
            {
                endIndex = pagedResult.PageCount;
                startIndex = (endIndex - customerHistoryPagerSpan + 1) > 0 ? endIndex - customerHistoryPagerSpan + 1 : 1;
            }

            //Add the First Page Button.
            if (pagedResult.CurrentPage > 1)
            {
                pages.Add(new("<<", "1", false));
            }

            //Add the Previous Button.
            if (pagedResult.CurrentPage > 1)
            {
                pages.Add(new("<", (pagedResult.CurrentPage - 1).ToString(), false));
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new(i.ToString(), i.ToString(), i == pagedResult.CurrentPage));
            }

            //Add the Next Button.
            if (pagedResult.CurrentPage < pagedResult.PageCount)
            {
                pages.Add(new(">", (pagedResult.CurrentPage + 1).ToString(), false));
            }

            //Add the Last Button.
            if (pagedResult.CurrentPage != pagedResult.PageCount)
            {
                pages.Add(new(">>", pagedResult.PageCount.ToString(), false));
            }

            //Loop and add Buttons for Pager.
            int count = 0;
            View.ClearAllPaginationButtons();
            foreach (Page page in pages)
            {
                Button btnPage = new();
                btnPage.Size = new (45, 30);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.BackColor = SystemColors.ActiveCaption;
                btnPage.Click += PageButton_Click;
                View.AddPaginationButton(btnPage, (count * 2) + 1);
                count++;
            }
        }

        public async void PageButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btnPager) await UpdateData(int.Parse(btnPager.Name));
        }

        public async void GridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (!Parent.CheckPotentialStateConflict())
            {
                if (sender is DataGridView gv)
                {
                    if (gv.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = gv.SelectedRows[0];
                        if (row != null)
                        {
                            await Parent.HistoryGridSearch(row);
                        }
                    }
                }
            }

            View.ClearGridSelection();
        }

        public async void CountComboBox_SelectedValueChanged(object? sender, EventArgs e)
        {
            int newvalue = View.GetMaxNumberOfRecords;
            await UpdateData(1);
            Settings.Default.HistoryDropDownSelected = newvalue;
            Settings.Default.Save();
        }

        public async void DatasetsUpdated(object? sender, EventArgs e) => await UpdateData();

        private class Page
        {
            public Page(string text, string value, bool selected)
            {
                Text = text;
                Value = value;
                Selected = selected;
            }

            public string Text { get; init; }

            public string Value { get; init; }

            public bool Selected { get; init; }
        }
    }
}
