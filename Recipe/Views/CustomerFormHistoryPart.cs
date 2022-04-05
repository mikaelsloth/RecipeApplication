namespace Recipe.Views
{
    using Recipe.Models.Db;
    using Recipe.Presenter;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class CustomerFormHistoryPart : UserControl, ICustomerFormHistoryPartView
    {
        private ICustomerFormHistoryPartPresenter? presenter = null;

        public CustomerFormHistoryPart()
        {
            InitializeComponent();
            HistoryGridView.AutoGenerateColumns = false;
        }

        public ICustomerFormHistoryPartPresenter Presenter
        {
            get => presenter ?? throw new InvalidOperationException();
            set => presenter = value;
        }

        public void ClearAllPaginationButtons() => PagerLayoutPanel.Controls.Clear();

        public void AddPaginationButton(Button button, int position)
        {
            if (position is not 1 and not 3 and not 5 and not 7 and not 9 and not 11 and not 13) throw new ArgumentOutOfRangeException(nameof(position));

            button.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            PagerLayoutPanel.Controls.Add(button, position, 0);
        }

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Dispose(true);

        public int GetMaxNumberOfRecords => (int)HistoryCountComboBox.SelectedItem;

        public void SetGridDataSource(IList<LatestCustomersView> datasource)
        {
            HistoryGridView.SelectionChanged -= HistoryGridView_SelectionChanged;
            HistoryGridView.DataSource = null;
            BindingSource.DataSource = null;
            BindingSource.DataSource = datasource;
            HistoryGridView.DataSource = BindingSource;
            HistoryGridView.ClearSelection();
            HistoryGridView.SelectionChanged += HistoryGridView_SelectionChanged;
        }

        public void SetNumberDropDownDataSource(IList<int> datasource) => HistoryCountComboBox.DataSource = datasource;

        public event EventHandler? SelectedDropDownValueChanged;

        public event EventHandler? SelectedRowChanged;

        private void HistoryCountComboBox_SelectedValueChanged(object? sender, EventArgs e) => SelectedDropDownValueChanged?.Invoke(sender, e);

        private void HistoryGridView_SelectionChanged(object? sender, EventArgs e) => SelectedRowChanged?.Invoke(sender, e);

        public void SetNumberDropDownInitialSelected(int historyDropDownSelected) => HistoryCountComboBox.SelectedItem = historyDropDownSelected;

        public DataGridViewColumnCollection GetAllDataGridColumns() => HistoryGridView.Columns;

        public void ClearGridSelection()
        {
            HistoryGridView.SelectionChanged -= HistoryGridView_SelectionChanged;
            HistoryGridView.ClearSelection();
            HistoryGridView.SelectionChanged += HistoryGridView_SelectionChanged;
        }
    }
}

