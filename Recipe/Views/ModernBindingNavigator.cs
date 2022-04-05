#nullable disable

namespace Recipe.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    [DefaultProperty(nameof(BindingSource))]
    [DefaultEvent(nameof(RefreshItems))]
    public partial class ModernBindingNavigator : UserControl
    {
        private int _sectionSeparator = 10;
        private int _controlSeparator = 5;
        private bool _showTextPanel = false;
        private const int _tablePanelMinimumHeight = 30;

        private const string MoveFirstItemName = "BindingNavigatorMoveFirstItem";
        private const string MovePreviousItemName = "BindingNavigatorMovePreviousItem";
        private const string MoveNextItemName = "BindingNavigatorMoveNextItem";
        private const string MoveLastItemName = "BindingNavigatorMoveLastItem";
        private const string PositionItemName = "BindingNavigatorPositionItem";
        private const string PositionTextItemName = "BindingNavigatorPositionTextItem";
        private const string CountItemName = "BindingNavigatorCountItem";
        private const string AddNewItemName = "BindingNavigatorAddNewItem";
        private const string DeleteItemName = "BindingNavigatorDeleteItem";

        private readonly Dictionary<int, string> _positions = new();

        private void UpdateTableLayout()
        {
            float _percentage = _showTextPanel ? 11.11111F : 12.50F;

            TableLayoutPanel.Controls.Clear();
            _positions.Clear();
            TableLayoutPanel.ColumnStyles.Clear();
            TableLayoutPanel.ColumnCount = _showTextPanel ? 17 : 15;
            int j = 0;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, MoveFirstItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _controlSeparator));
            j++;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, MovePreviousItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _sectionSeparator));
            j++;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, MoveNextItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _controlSeparator));
            j++;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, MoveLastItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _sectionSeparator));
            j++;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, PositionItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _controlSeparator));
            j++;

            if (_showTextPanel)
            {
                _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
                _positions.Add(2 * j, PositionTextItemName);
                _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _controlSeparator));
                j++;
            }

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, CountItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _sectionSeparator));
            j++;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, AddNewItemName);
            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _controlSeparator));
            j++;

            _ = TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _percentage));
            _positions.Add(2 * j, DeleteItemName);

            for (int i = 0; i < TableLayoutPanel.ColumnCount; i++)
            {
                if (i % 2 == 1) continue;
                Panel panel = new()
                {
                    Dock = DockStyle.Fill,
                    Name = _positions[i],
                };
                if (panel.Name == PositionTextItemName) panel.Font = new("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
                TableLayoutPanel.Controls.Add(panel, i, 0);
            }
        }

        [Category("Appearance")]
        [Description("The size of the space between sections of buttons on the component.")]
        [DefaultValue(10)]
        public int SectionSeparatorWidth
        {
            get => _sectionSeparator;
            set => _sectionSeparator = value < 0 ? 0 : value;
        }

        [Category("Appearance")]
        [Description("The size of the space between individual buttons in a section of buttons on the component.")]
        [DefaultValue(5)]
        public int ControlSeparatorWidth
        {
            get => _controlSeparator;
            set => _controlSeparator = value < 0 ? 0 : value;
        }

        [Category("Appearance")]
        [Description("Whether to show the formatting in a separate label on the component.")]
        [DefaultValue(false)]
        public bool ShowTextPanel
        {
            get => _showTextPanel;
            set
            {
                bool _oldvalue = _showTextPanel;
                _showTextPanel = value;
                if (_oldvalue != _showTextPanel && !_initializing) UpdateTableLayout();
            }
        }

        // Private members related to main functionality

        private BindingSource _bindingSource;

        private ModernButton _moveFirstItem;
        private ModernButton _movePreviousItem;
        private ModernButton _moveNextItem;
        private ModernButton _moveLastItem;
        private ModernButton _addNewItem;
        private ModernButton _deleteItem;
        private ModernTextBox _positionItem;
        private ModernLabel _countItem;
        private ModernLabel _hiddenItem;

        private string _countItemFormat = "";

        private EventHandler _onRefreshItems;

        private bool _initializing;

        private bool _addNewItemUserEnabled = true;
        private bool _deleteItemUserEnabled = true;

        #region => Constructors

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ModernBindingNavigator() : this(false)
        {
        }

        /// <summary>
        ///  Creates a BindingNavigator strip containing standard items, bound to the specified BindingSource.
        /// </summary>
        public ModernBindingNavigator(BindingSource bindingSource) : this(true)
        {
            BindingSource = bindingSource;
        }

        /// <summary>
        ///  Creates an empty BindingNavigator tool strip, and adds the strip to the specified container.
        ///  Call AddStandardItems() to add standard tool strip items.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ModernBindingNavigator(IContainer container) : this(false)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.Add(this);
        }

        /// <summary>
        ///  Creates a BindingNavigator strip, optionally containing a set of standard tool strip items.
        /// </summary>
        public ModernBindingNavigator(bool addStandardItems)
        {
            BeginInit();
            InitializeComponent();
            UpdateControlHeight();
            UpdateTableLayout();

            if (addStandardItems) AddStandardItems();
            EndInit();
        }

        #endregion

        #region Main API and functionlity

        /// <summary>
        ///  ISupportInitialize support. Disables updates to tool strip items during initialization.
        /// </summary>
        public void BeginInit() => _initializing = true;

        /// <summary>
        ///  ISupportInitialize support. Enables updates to tool strip items after initialization.
        /// </summary>
        public void EndInit()
        {
            _initializing = false;
            RefreshItemsInternal();
        }

        public virtual void AddStandardItems()
        {
            //
            // Create items
            //
            MoveFirstItem = new();
            MovePreviousItem = new();
            MoveNextItem = new();
            MoveLastItem = new();
            PositionItem = new();
            PositionTextItem = new();
            CountItem = new();
            AddNewItem = new();
            DeleteItem = new();
            //
            // Set up strings
            //
            // Default to lowercase for null name, because C# dev is more likely to create controls programmatically than
            // vb dev.
            char ch = string.IsNullOrEmpty(Name) || char.IsLower(Name[0]) ? 'b' : 'B';

            MoveFirstItem.Name = ch + "indingNavigatorMoveFirstItem";
            MovePreviousItem.Name = ch + "indingNavigatorMovePreviousItem";
            MoveNextItem.Name = ch + "indingNavigatorMoveNextItem";
            MoveLastItem.Name = ch + "indingNavigatorMoveLastItem";
            PositionItem.Name = ch + "indingNavigatorPositionItem";
            PositionTextItem.Name = ch + "indingNavigatorPositionTextItem";
            CountItem.Name = ch + "indingNavigatorCountItem";
            AddNewItem.Name = ch + "indingNavigatorAddNewItem";
            DeleteItem.Name = ch + "indingNavigatorDeleteItem";

            MoveFirstItem.Text = "";
            MovePreviousItem.Text = "";
            MoveNextItem.Text = "";
            MoveLastItem.Text = "";
            AddNewItem.Text = "";
            DeleteItem.Text = "";
            //
            // Set up images
            //
            MoveFirstItem.Image = Properties.Resources.icons8_first_25;
            MovePreviousItem.Image = Properties.Resources.icons8_previous_24;
            MoveNextItem.Image = Properties.Resources.icons8_next_24;
            MoveLastItem.Image = Properties.Resources.icons8_last_25;
            AddNewItem.Image = Properties.Resources.icons8_add_new_26;
            DeleteItem.Image = Properties.Resources.icons8_reduce_26;
            ////
            //// Set other random properties
            ////
            //PositionItem.AutoSize = false;
            //PositionItem.Width = 28;
        }

        [Browsable(false)]
        public BindingSource BindingSource
        {
            get => _bindingSource;
            set => WireUpBindingSource(ref _bindingSource, value);
        }

        [Browsable(false)]
        public ModernLabel PositionTextItem
        {
            get
            {
                if (_hiddenItem is not null && _hiddenItem.IsDisposed) _hiddenItem = null;
                return _hiddenItem;
            }
            set => WireUpLabel(ref _hiddenItem, value, PositionTextItemName);
        }

        /// <summary>
        ///  The ToolStripItem that triggers the 'Move first' action, or null.
        /// </summary>
        [Browsable(false)]
        public ModernButton MoveFirstItem
        {
            get
            {
                if (_moveFirstItem is not null && _moveFirstItem.IsDisposed) _moveFirstItem = null;
                return _moveFirstItem;
            }
            set => WireUpButton(ref _moveFirstItem, value, new EventHandler(OnMoveFirst), MoveFirstItemName);
        }

        /// <summary>
        ///  The ToolStripItem that triggers the 'Move previous' action, or null.
        /// </summary>
        [Browsable(false)]
        public ModernButton MovePreviousItem
        {
            get
            {
                if (_movePreviousItem is not null && _movePreviousItem.IsDisposed) _movePreviousItem = null;
                return _movePreviousItem;
            }
            set => WireUpButton(ref _movePreviousItem, value, new EventHandler(OnMovePrevious), MovePreviousItemName);
        }

        /// <summary>
        ///  The ToolStripItem that triggers the 'Move next' action, or null.
        /// </summary>
        [Browsable(false)]
        public ModernButton MoveNextItem
        {
            get
            {
                if (_moveNextItem is not null && _moveNextItem.IsDisposed) _moveNextItem = null;
                return _moveNextItem;
            }
            set => WireUpButton(ref _moveNextItem, value, new EventHandler(OnMoveNext), MoveNextItemName);
        }

        /// <summary>
        ///  The ToolStripItem that triggers the 'Move last' action, or null.
        /// </summary>
        [Browsable(false)]
        public ModernButton MoveLastItem
        {
            get
            {
                if (_moveLastItem is not null && _moveLastItem.IsDisposed) _moveLastItem = null;
                return _moveLastItem;
            }
            set => WireUpButton(ref _moveLastItem, value, new EventHandler(OnMoveLast), MoveLastItemName);
        }

        /// <summary>
        ///  The ToolStripItem that triggers the 'Add new' action, or null.
        /// </summary>
        [Browsable(false)]
        public ModernButton AddNewItem
        {
            get
            {
                if (_addNewItem is not null && _addNewItem.IsDisposed) _addNewItem = null;
                return _addNewItem;
            }
            set
            {
                if (_addNewItem != value && value is not null)
                {
                    value.EnabledChanged += new EventHandler(OnAddNewItemEnabledChanged);
                    _addNewItemUserEnabled = value.Enabled;
                }

                WireUpButton(ref _addNewItem, value, new EventHandler(OnAddNew), AddNewItemName);
            }
        }

        /// <summary>
        ///  The ToolStripItem that triggers the 'Delete' action, or null.
        /// </summary>
        [Browsable(false)]
        public ModernButton DeleteItem
        {
            get
            {
                if (_deleteItem is not null && _deleteItem.IsDisposed) _deleteItem = null;
                return _deleteItem;
            }
            set
            {
                if (_deleteItem != value && value is not null)
                {
                    value.EnabledChanged += new EventHandler(OnDeleteItemEnabledChanged);
                    _deleteItemUserEnabled = value.Enabled;
                }

                WireUpButton(ref _deleteItem, value, new EventHandler(OnDelete), DeleteItemName);
            }
        }

        /// <summary>
        ///  The ToolStripItem that displays the current position, or null.
        /// </summary>
        [Browsable(false)]
        public ModernTextBox PositionItem
        {
            get
            {
                if (_positionItem is not null && _positionItem.IsDisposed) _positionItem = null;
                return _positionItem;
            }
            set => WireUpTextBox(ref _positionItem, value, new KeyEventHandler(OnPositionKey), new EventHandler(OnPositionLostFocus), PositionItemName);
        }

        /// <summary>
        ///  The ToolStripItem that displays the total number of items, or null.
        /// </summary>
        [Browsable(false)]
        public ModernLabel CountItem
        {
            get
            {
                if (_countItem is not null && _countItem.IsDisposed) _countItem = null;
                return _countItem;
            }
            set => WireUpLabel(ref _countItem, value, CountItemName);
        }

        /// <summary>
        ///  Formatting to apply to count displayed in the CountItem tool strip item.
        /// </summary>
        public string CountItemFormat
        {
            get => _countItemFormat;
            set
            {
                if (_countItemFormat != value)
                {
                    _countItemFormat = value;
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Event raised when the state of the tool strip items needs to be
        ///  refreshed to reflect the current state of the data.
        /// </summary>
        [Browsable(true)]
        public event EventHandler RefreshItems
        {
            add => _onRefreshItems += value;
            remove => _onRefreshItems -= value;
        }

        /// <summary>
        ///  Refreshes the state of the standard items to reflect the current state of the data.
        /// </summary>
        protected virtual void RefreshItemsCore()
        {
            int count, position;
            bool allowNew, allowRemove;

            // Get state info from the binding source (if any)
            if (_bindingSource is null)
            {
                count = 0;
                position = 0;
                allowNew = false;
                allowRemove = false;
            }
            else
            {
                count = _bindingSource.Count;
                position = _bindingSource.Position + 1;
                allowNew = (_bindingSource as IBindingList).AllowNew;
                allowRemove = (_bindingSource as IBindingList).AllowRemove;
            }

            // Enable or disable items (except when in design mode)
            if (!DesignMode)
            {
                if (MoveFirstItem is not null) _moveFirstItem.Enabled = position > 1;

                if (MovePreviousItem is not null) _movePreviousItem.Enabled = position > 1;

                if (MoveNextItem is not null) _moveNextItem.Enabled = position < count;

                if (MoveLastItem is not null) _moveLastItem.Enabled = position < count;

                if (AddNewItem is not null)
                {
                    EventHandler handler = new(OnAddNewItemEnabledChanged);
                    _addNewItem.EnabledChanged -= handler;
                    _addNewItem.Enabled = _addNewItemUserEnabled && allowNew;
                    _addNewItem.EnabledChanged += handler;
                }

                if (DeleteItem is not null)
                {
                    EventHandler handler = new(OnDeleteItemEnabledChanged);
                    _deleteItem.EnabledChanged -= handler;
                    _deleteItem.Enabled = _deleteItemUserEnabled && allowRemove && count > 0;
                    _deleteItem.EnabledChanged += handler;
                }

                if (PositionItem is not null) _positionItem.Enabled = position > 0 && count > 0;

                if (CountItem is not null) _countItem.Enabled = count > 0;
            }

            // Update current position indicator
            if (_positionItem is not null) _positionItem.Text = position.ToString(CultureInfo.CurrentCulture);

            // Update record count indicator
            if (_countItem is not null) _countItem.Text = DesignMode ? CountItemFormat : string.Format(CultureInfo.CurrentCulture, CountItemFormat, count);
        }

        /// <summary>
        ///  Called when the state of the tool strip items needs to be refreshed to reflect the current state of the data.
        ///  Calls <see cref="RefreshItemsCore"/> to refresh the state of the standard items, then raises the RefreshItems event.
        /// </summary>
        protected virtual void OnRefreshItems()
        {
            // Refresh all the standard items
            RefreshItemsCore();

            // Raise the public event
            _onRefreshItems?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///  Accept new row position entered into PositionItem.
        /// </summary>
        private void AcceptNewPosition()
        {
            // If no position item or binding source, do nothing
            if (_positionItem is null || _bindingSource is null) return;

            // Default to old position, in case new position turns out to be garbage
            int newPosition = _bindingSource.Position;

            try
            {
                // Read new position from item text (and subtract one!)
                newPosition = Convert.ToInt32(_positionItem.Text, CultureInfo.CurrentCulture) - 1;
            }
            catch (FormatException)
            {
                // Ignore bad user input
            }
            catch (OverflowException)
            {
                // Ignore bad user input
            }

            // If user has managed to enter a valid number, that is not the same as the current position, try
            // navigating to that position. Let the BindingSource validate the new position to keep it in range.
            if (newPosition != _bindingSource.Position) _bindingSource.Position = newPosition;

            // Update state of all items to reflect final position. If the user entered a bad position,
            // this will effectively reset the Position item back to showing the current position.
            RefreshItemsInternal();
        }

        /// <summary>
        ///  Cancel new row position entered into PositionItem.
        /// Just refresh state of all items to reflect current position
        ///    (causing position item's new value to get blasted away)
        ///</summary>
        private void CancelNewPosition() => RefreshItemsInternal();

        /// <summary>
        ///  Navigates to first item in BindingSource's list when the MoveFirstItem is clicked.
        /// </summary>
        private void OnMoveFirst(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_bindingSource is not null)
                {
                    _bindingSource.MoveFirst();
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Navigates to previous item in BindingSource's list when the MovePreviousItem is clicked.
        /// </summary>
        private void OnMovePrevious(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_bindingSource is not null)
                {
                    _bindingSource.MovePrevious();
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Navigates to next item in BindingSource's list when the MoveNextItem is clicked.
        /// </summary>
        private void OnMoveNext(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_bindingSource is not null)
                {
                    _bindingSource.MoveNext();
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Navigates to last item in BindingSource's list when the MoveLastItem is clicked.
        /// </summary>
        private void OnMoveLast(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_bindingSource is not null)
                {
                    _bindingSource.MoveLast();
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Adds new item to BindingSource's list when the AddNewItem is clicked.
        /// </summary>
        private void OnAddNew(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_bindingSource is not null)
                {
                    _ = _bindingSource.AddNew();
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Deletes current item from BindingSource's list when the DeleteItem is clicked.
        /// </summary>
        private void OnDelete(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_bindingSource is not null)
                {
                    _bindingSource.RemoveCurrent();
                    RefreshItemsInternal();
                }
            }
        }

        /// <summary>
        ///  Navigates to specific item in BindingSource's list when a value is entered into the PositionItem.
        /// </summary>
        private void OnPositionKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    AcceptNewPosition();
                    break;
                case Keys.Escape:
                    CancelNewPosition();
                    break;
            }
        }

        /// <summary>
        ///  Navigates to specific item in BindingSource's list when a value is entered into the PositionItem.
        /// </summary>
        private void OnPositionLostFocus(object sender, EventArgs e) => AcceptNewPosition();

        /// <summary>
        ///  Refresh tool strip items when something changes in the BindingSource.
        /// </summary>
        private void OnBindingSourceStateChanged(object sender, EventArgs e) => RefreshItemsInternal();

        /// <summary>
        ///  Refresh tool strip items when the BindingSource is disposed.
        /// </summary>
        private void OnBindingSourceDisposed(object sender, EventArgs e) => BindingSource = null;

        /// <summary>
        ///  Refresh tool strip items when something changes in the BindingSource's list.
        /// </summary>
        private void OnBindingSourceListChanged(object sender, ListChangedEventArgs e) => RefreshItemsInternal();

        /// <summary>
        ///  Refresh the state of the items when the state of the data changes.
        /// </summary>
        private void RefreshItemsInternal()
        {
            // Block all updates during initialization
            if (_initializing) return;

            // Call method that updates the items (overridable)
            OnRefreshItems();
        }

        private void OnAddNewItemEnabledChanged(object sender, EventArgs e)
        {
            if (AddNewItem is not null) _addNewItemUserEnabled = _addNewItem.Enabled;
        }

        private void OnDeleteItemEnabledChanged(object sender, EventArgs e)
        {
            if (DeleteItem is not null) _deleteItemUserEnabled = _deleteItem.Enabled;
        }

        /// <summary>
        ///  Wire up some member variable to the specified button item, hooking events
        ///  on the new button and unhooking them from the previous button, if required.
        /// </summary>
        private void WireUpButton(ref ModernButton oldButton, ModernButton newButton, EventHandler clickHandler, string panelName)
        {
            if (oldButton == newButton) return;

            if (oldButton is not null) oldButton.Click -= clickHandler;

            if (newButton is not null) newButton.Click += clickHandler;

            oldButton = newButton;
            AddToPanel(oldButton, panelName);
            RefreshItemsInternal();
        }

        private void AddToPanel(Control control, string panelname)
        {
            Panel panel = TableLayoutPanel.Controls.OfType<Panel>().Where(c => c.Name == panelname).First();
            if (panel != null)
            {
                panel.Controls.Clear();
                control.Location = new((panel.Width - control.Width) / 2, (panel.Height - control.Height) / 2);
                panel.Controls.Add(control);
            }
        }

        /// <summary>
        ///  Wire up some member variable to the specified text box item, hooking events
        ///  on the new text box and unhooking them from the previous text box, if required.
        /// </summary>
        private void WireUpTextBox(ref ModernTextBox oldTextBox, ModernTextBox newTextBox, KeyEventHandler keyUpHandler, EventHandler lostFocusHandler, string panelName)
        {
            if (oldTextBox == newTextBox) return;

            if (oldTextBox is not null)
            {
                oldTextBox.KeyUp -= keyUpHandler;
                oldTextBox.LostFocus -= lostFocusHandler;
            }

            if (newTextBox is not null)
            {
                newTextBox.KeyUp += keyUpHandler;
                newTextBox.LostFocus += lostFocusHandler;
            }

            oldTextBox = newTextBox;
            AddToPanel(oldTextBox, panelName);
            RefreshItemsInternal();
        }

        /// <summary>
        ///  Wire up some member variable to the specified label item, hooking events
        ///  on the new label and unhooking them from the previous label, if required.
        /// </summary>
        private void WireUpLabel(ref ModernLabel oldLabel, ModernLabel newLabel, string panelName)
        {
            if (oldLabel == newLabel) return;

            oldLabel = newLabel;
            AddToPanel(oldLabel, panelName);
            RefreshItemsInternal();
        }

        /// <summary>
        ///  Wire up some member variable to the specified binding source, hooking events
        ///  on the new binding source and unhooking them from the previous one, if required.
        /// </summary>
        private void WireUpBindingSource(ref BindingSource oldBindingSource, BindingSource newBindingSource)
        {
            if (oldBindingSource == newBindingSource) return;

            if (oldBindingSource is not null)
            {
                oldBindingSource.PositionChanged -= new EventHandler(OnBindingSourceStateChanged);
                oldBindingSource.CurrentChanged -= new EventHandler(OnBindingSourceStateChanged);
                oldBindingSource.CurrentItemChanged -= new EventHandler(OnBindingSourceStateChanged);
                oldBindingSource.DataSourceChanged -= new EventHandler(OnBindingSourceStateChanged);
                oldBindingSource.DataMemberChanged -= new EventHandler(OnBindingSourceStateChanged);
                oldBindingSource.ListChanged -= new ListChangedEventHandler(OnBindingSourceListChanged);
                oldBindingSource.Disposed -= new EventHandler(OnBindingSourceDisposed);
            }

            if (newBindingSource is not null)
            {
                newBindingSource.PositionChanged += new EventHandler(OnBindingSourceStateChanged);
                newBindingSource.CurrentChanged += new EventHandler(OnBindingSourceStateChanged);
                newBindingSource.CurrentItemChanged += new EventHandler(OnBindingSourceStateChanged);
                newBindingSource.DataSourceChanged += new EventHandler(OnBindingSourceStateChanged);
                newBindingSource.DataMemberChanged += new EventHandler(OnBindingSourceStateChanged);
                newBindingSource.ListChanged += new ListChangedEventHandler(OnBindingSourceListChanged);
                newBindingSource.Disposed += new EventHandler(OnBindingSourceDisposed);
            }

            oldBindingSource = newBindingSource;
            RefreshItemsInternal();
        }

        #endregion

        #region Formatting and display

        private Color borderColor = SystemColors.InactiveBorder;
        private int borderSize = 2;
        private Color borderFocusColor = SystemColors.ActiveBorder;
        private int borderRadius = 0;

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            pathTxt = TableLayoutPanel.ClientRectangle.GetRoundedPath(borderSize * 2);
            TableLayoutPanel.Region = new(pathTxt);
            pathTxt.Dispose();
        }

        private void UpdateControlHeight()
        {
            int txtHeight = TextRenderer.MeasureText("Text", Font).Height + 1;

            TableLayoutPanel.MinimumSize = new(0, Math.Max(txtHeight, _tablePanelMinimumHeight));
            Height = TableLayoutPanel.Height + Padding.Top + Padding.Bottom;
        }

        [Category("Appearance")]
        [Description("The color of the border of the component.")]
        [DefaultValue(typeof(Color), "0xF4F7FC")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The border size of the component.")]
        [DefaultValue(2)]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                if (value >= 1)
                {
                    borderSize = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("The border color of the component when focused.")]
        [DefaultValue(typeof(Color), "0xB4B4B4")]
        public Color BorderFocusColor
        {
            get => borderFocusColor;
            set
            {
                borderFocusColor = value;
                Invalidate();
            }
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                TableLayoutPanel.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                TableLayoutPanel.ForeColor = value;
            }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                TableLayoutPanel.Font = value;
                if (DesignMode)
                    UpdateControlHeight();
            }
        }

        [Category("Appearance")]
        [Description("The border radius of the component.")]
        [DefaultValue(0)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    Invalidate();//Redraw control
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Changes is being done internally
            if (DesignMode)
                UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (borderRadius > 1)//Rounded TextBox
            {
                //-Fields
                var rectBorderSmooth = ClientRectangle;
                Rectangle rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using GraphicsPath pathBorderSmooth = rectBorderSmooth.GetRoundedPath(borderRadius);
                using GraphicsPath pathBorder = rectBorder.GetRoundedPath(borderRadius - borderSize);
                using Pen penBorderSmooth = new(Parent.BackColor, smoothSize);
                using Pen penBorder = new(borderColor, borderSize);
                //-Drawing
                Region = new(pathBorderSmooth);//Set the rounded region of UserControl
                if (borderRadius > 15) SetTextBoxRoundedRegion();//Set the rounded region of TextBox component
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                penBorder.Alignment = PenAlignment.Center;
                if (Focused) penBorder.Color = borderFocusColor;

                //Draw border smoothing
                graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                //Draw border
                graph.DrawPath(penBorder, pathBorder);
            }
            else //Square/Normal TextBox
            {
                //Draw border
                using Pen penBorder = new(borderColor, borderSize);
                Region = new(ClientRectangle);
                penBorder.Alignment = PenAlignment.Inset;
                if (Focused) penBorder.Color = borderFocusColor;

                graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
            }
        }

        #endregion
    }
}
