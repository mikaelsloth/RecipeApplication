namespace Recipe.Presenter
{

    using EntityFramework.Exceptions.Common;
    using Recipe.BusinessRules;
    using Recipe.Controller;
    using Recipe.Models.Db;
    using Recipe.Printing;
    using Recipe.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class CustomerFormDetailPartPresenter : PresenterBase<ICustomerFormDetailPartView, ICustomerFormDetailPartPresenter, ICustomerMainFormPresenter>, ICustomerFormDetailPartPresenter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IValidateEntities<Customer> validator;
        private DetailedPartState previous_state = DetailedPartState.NotDefined;

        private Customer? orgCustomer = null;

        private bool errors_present;
        //private bool updatingBindings;

        public DetailedPartState State { get; private set; } = DetailedPartState.NotDefined;

        public CustomerFormDetailPartPresenter(IServiceProvider serviceProvider, IValidateEntities<Customer> validator, ICustomerFormDetailPartView? view) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(CustomerFormDetailPartPresenter)} class could not start due to a missing dependency");
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator), $"The {nameof(CustomerFormDetailPartPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            WireEvents();
        }

        private void WireEvents()
        {
            View.TextboxLeave += ModelTextBoxLeave;
            View.TextboxTextChanged += ModelTextChanged;
            View.GdprCheckChanged += ModelCheckedChanged;
            View.CancelClicked += CancelClicked;
            View.DeleteClicked += DeleteClicked;
            View.PrintGdprClicked += PrintGdprClicked;
            View.RemarksClicked += RemarksClicked;
            View.SaveClicked += SaveClicked;
            Close += View.Close_Clicked;
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
            StateChanged += Parent.StateChanged;
            DataSourceChanged += Parent.DataSetsRefresh;
            Parent.NewButtonClicked += NewButtonClicked;

            SetState(DetailedPartState.Blank, false);
        }

        public event EventHandler? DataSourceChanged;

        public event EventHandler? StateChanged;

        #region Closing the View
        public virtual void OnDataSourceChanged() => DataSourceChanged?.Invoke(this, new());

        public override event FormClosingEventHandler? Close;

        public override void OnClose(object? sender, FormClosingEventArgs e)
        {
            if (State == DetailedPartState.Dirty)
            {
                var result = ShowIsDirtyOnCloseWarning();
                switch (result)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        SetFocus();
                        break;
                    case DialogResult.Yes:
                        object? obj = View.BindingSourceData;
                        if (obj == null) break;
                        Customer customer = (Customer)obj;
                        if (!ValidateCustomer(customer, false))
                        {
                            e.Cancel = true;
                            DisplayValidationErrorPreventedShutdown();
                        }

                        SaveClicked(this, new());
                        break;
                    default:
                        break;
                }
            }

            if (!e.Cancel) Close?.Invoke(sender, e);
        }

        private static void DisplayValidationErrorPreventedShutdown() => MessageBox.Show("Ændringerne kunne ikke gemmes p.g.a. validerings fejl." +
                "\r\n\r\nNedlukningen afbrydes derfor", "Nedlukning afbrudt", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        private static DialogResult ShowIsDirtyOnCloseWarning() => MessageBox.Show("Der er ændringer i den valgte kunde der ikke er gemt." +
                "\r\n\r\nTryk Ja for at gemme ændringerne og derefter fortsætte nedlukningen." +
                "\r\n\r\nTryk Nej for at slette ændringer (som mistes derved) og fortsætte nedlukningen." +
                "\r\n\r\nTryk Annuller for at fortryde nedlukningen."
                , "Ikke gemte ændringer ved nedlukning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        #endregion

        #region State Management method calls

        public void SetFocus() => View.SetFocus();

        public void ModelTextChanged(object? sender, EventArgs e) => SetState(DetailedPartState.Dirty, false);

        public void ModelCheckedChanged(object? sender, EventArgs e)
        {
            ChangeCheckBoxChecked(sender);

            SetState(DetailedPartState.Dirty, false);
        }

        private void ChangeCheckBoxChecked(object? sender)
        {
            if (State is DetailedPartState.Dirty or DetailedPartState.New or DetailedPartState.Selected)
                if (sender is CheckBox checkbox) View.GdprButtonVisible = !checkbox.Checked;
        }

        public void ModelTextBoxLeave(object? sender, EventArgs e)
        {
            // if in dirty state, then check if still dirty, otherwise ignore
            if (State == DetailedPartState.Dirty)
            {
                if (sender is not null) ((TextBox)sender).DataBindings[0].WriteValue();
                object? obj = View.BindingSourceData ?? throw new InvalidOperationException("ModelTextBoxLeave: customer is null");
                Customer customer = (Customer)obj;
                if (orgCustomer == null) throw new InvalidOperationException("ModelTextBoxLeave: orgcustomer is null");
                if (customer.ValueEquals(orgCustomer)) SetState(previous_state, false);
            }
        }

        public void SetState(DetailedPartState value, bool setFocus = false)
        {
            //if (!updatingBindings)
            //{
            if (value == DetailedPartState.NotDefined) throw new InvalidOperationException("You cannot set this state on a running form");
            DetailedPartState oldstate = State;

            if (value != oldstate)
            {
                State = value;
                previous_state = oldstate;
                OnStateChanged(State, setFocus);
            }
            //}
        }

        private void OnStateChanged(DetailedPartState value, bool setFocus)
        {
            switch (value)
            {
                case DetailedPartState.Blank:
                    if (orgCustomer != null)
                    {
                        UnBindDataFromDetailSection();
                        orgCustomer = null;
                    }

                    foreach (Control item in View.GetAllControls)
                    {
                        _ = item switch
                        {
                            TextBox t => SetAddressTextBoxState(t, false),
                            CheckBox c => SetAddressCheckBoxState(c, false),
                            Button b => SetAddressButtonState(b, false),
                            _ => false
                        };
                    }

                    StateChanged?.Invoke(this, new());
                    View.RemarksButtonEnabled = false;
                    View.RemarksButtonVisible = true;
                    if (setFocus) _ = View.SetFocus();
                    break;
                case DetailedPartState.New:
                    if (previous_state != DetailedPartState.Dirty) SetDetailSectionInEditMode();
                    StateChanged?.Invoke(this, new());
                    View.RemarksButtonEnabled = true;
                    View.SaveButtonVisible = false;
                    View.CancelButtonVisible = false;
                    _ = View.SetFocus();
                    break;
                case DetailedPartState.Selected:
                    if (previous_state != DetailedPartState.Dirty) SetDetailSectionInEditMode();
                    StateChanged?.Invoke(this, new());
                    View.RemarksButtonEnabled = true;
                    View.DeleteButtonVisible = true;
                    View.SaveButtonVisible = false;
                    View.CancelButtonVisible = false;
                    _ = View.SetFocus();
                    break;
                case DetailedPartState.Dirty:
                    StateChanged?.Invoke(this, new());
                    View.SaveButtonVisible = true;
                    View.CancelButtonVisible = true;
                    if (setFocus) _ = View.SetFocus();
                    break;
                default:
                    break;
            }
        }

        private void SetDetailSectionInEditMode()
        {
            foreach (Control item in View.GetAllControls)
            {
                _ = item switch
                {
                    TextBox t => SetAddressTextBoxState(t, true),
                    CheckBox c => SetAddressCheckBoxState(c, true),
                    _ => false
                };
            }
        }

        private bool SetAddressTextBoxState(TextBox control, bool enabled)
        {
            if (!enabled)
            {
                control.TextChanged -= View.TextBox_TextChanged;
                control.Text = "";
                control.TextChanged += View.TextBox_TextChanged;
            }

            control.Enabled = enabled;
            return true;
        }

        private bool SetAddressCheckBoxState(CheckBox control, bool enabled)
        {
            if (!enabled)
            {
                control.CheckedChanged -= View.GdprCheckBox_CheckedChanged;
                control.Checked = false;
                control.CheckedChanged -= View.GdprCheckBox_CheckedChanged;
            }

            control.Enabled = enabled;
            return true;
        }

        private static bool SetAddressButtonState(Button control, bool enabled)
        {
            control.Visible = enabled;
            return true;
        }

        #endregion

        #region Command methods

        public async void SaveClicked(object? sender, EventArgs e)
        {
            object obj = View.BindingSourceData ?? throw new InvalidOperationException("Did not get a valid object from the binding source - please contact IT");
            object objController = serviceProvider.GetService(typeof(ICustomerController)) ?? throw new ArgumentOutOfRangeException("type", nameof(ICustomerController), "Not defined by ServiceProvider - call IT development");

            ICustomerController controller = (ICustomerController)objController;
            Customer customer = (Customer)obj;

            if (!ValidateCustomer(customer, true)) return;

            await SaveCustomer(customer, controller);
        }

        private bool ValidateCustomer(Customer customer, bool displayErrors = true)
        {
            if (errors_present) ReleaseAllErrors();

            DomainResult validationResult = customer.Validate(validator);
            if (!validationResult.Success)
            {
                errors_present = true;
                if (displayErrors) DisplayErrorMessage(validationResult.Message);
            }

            return validationResult.Success;
        }

        private async Task SaveCustomer(Customer customer, ICustomerController controller)
        {
            try
            {
                switch (previous_state)
                {
                    case DetailedPartState.New:
                        _ = await controller.Add(customer);
                        break;
                    case DetailedPartState.Selected:
                        await controller.Modify(customer);
                        break;
                    default:
                        throw new InvalidOperationException($"StateConfusion on {nameof(SaveCustomer)} operation. Previous state was : {previous_state}");
                }

                Parent.SelectedCustomer = customer;
                OnDataSourceChanged();
                SetState(previous_state, false);
            }
            catch (UniqueConstraintException)
            {
                DisplayErrorMessage();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        private void ReleaseAllErrors()
        {
            foreach (var item in View.GetAllControls.OfType<TextBox>())
            {
                View.SetError(item, null);
            };
        }

        private void DisplayErrorMessage(IList<ArgumentException>? exceptions)
        {
            if (exceptions == null) return;
            for (int i = 0; i < exceptions.Count; i++)
            {
                var textbox = View.GetAllControls.OfType<TextBox>().FirstOrDefault(t => t.Tag.ToString() == exceptions[i].ParamName);
                if (textbox != null) View.SetError(textbox, exceptions[i].Message);
            }

            _ = View.SetFocus();
        }

        private void DisplayErrorMessage()
        {
            var result = MessageBox.Show("Der eksisterer allerede en kunde med disse data.\r\n\r\nØnsker du at rette indtastningerne?\r\n\r\nTryk Ja for gå tilbage for at rette i oplysninger. \r\n\r\nTryk Nej for at slette alle ændringerne."
                , "Duplet af kunde data", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes) _ = View.SetFocus();
            else
            {
                CancelClicked(null, new());
            }
        }

        private async Task DeleteCustomer(Customer customer, ICustomerController controller)
        {
            try
            {
                await controller.Delete(customer.Id);
                SetState(DetailedPartState.Blank);
                OnDataSourceChanged();
            }
            catch { throw; }
        }

        public async void DeleteClicked(object? sender, EventArgs e)
        {
            object obj = View.BindingSourceData ?? throw new InvalidOperationException("Did not get a valid object from the binding source - please contact IT");
            object objController = serviceProvider.GetService(typeof(ICustomerController)) ?? throw new ArgumentOutOfRangeException("type", nameof(ICustomerController), "Not defined by ServiceProvider - call IT development");

            ICustomerController controller = (ICustomerController)objController;
            Customer customer = (Customer)obj;

            await DeleteCustomer(customer, controller);
            OnDataSourceChanged();
        }

        public void CancelClicked(object? sender, EventArgs e)
        {
            UnBindDataFromDetailSection();
            if (orgCustomer == null) throw new InvalidOperationException();
            BindDataToDetailSection(orgCustomer.Clone());
            SetState(previous_state, false);
        }

        public void RemarksClicked(object? sender, EventArgs e) => throw new NotImplementedException();

        public void NewButtonClicked(object? sender, EventArgs e)
        {
            SetState(DetailedPartState.New, true);
            orgCustomer = new();
            BindDataToDetailSection(new());
        }

        public void CustomerSelected(Customer customer)
        {
            if (orgCustomer == null || !orgCustomer.ValueEquals(customer))
            {
                SetState(DetailedPartState.Selected);
                orgCustomer = customer.Clone();
                BindDataToDetailSection(customer);
            }
        }

        private void BindDataToDetailSection(Customer customer)
        {
            if (View.BindingSourceData != null) UnBindDataFromDetailSection();
            View.BindingSourceData = customer;

            foreach (Control item in View.GetAllControls)
            {
                _ = item switch
                {
                    TextBox t => SetAddressTextBoxData(t, View.BindingSource),
                    CheckBox c => SetAddressCheckBoxData(c, View.BindingSource),
                    _ => false
                };
            }
        }

        private void UnBindDataFromDetailSection()
        {
            foreach (Control item in View.GetAllControls)
            {
                _ = item switch
                {
                    TextBox t => ClearTextBoxBindings(t),
                    CheckBox c => ClearCheckBoxBindings(c),
                    _ => false
                };
            }

            View.BindingSourceData = null;
        }

        private static bool ClearCheckBoxBindings(CheckBox c)
        {
            c.DataBindings.Clear();
            return true;
        }

        private static bool ClearTextBoxBindings(TextBox t)
        {
            t.DataBindings.Clear();
            return true;
        }

        private bool SetAddressTextBoxData(TextBox control, BindingSource binding)
        {
            control.TextChanged -= View.TextBox_TextChanged;
            _ = control.DataBindings.Add("Text", binding, control.Tag.ToString());
            control.TextChanged += View.TextBox_TextChanged;
            return true;
        }

        private bool SetAddressCheckBoxData(CheckBox control, BindingSource binding)
        {
            control.CheckedChanged -= View.GdprCheckBox_CheckedChanged;
            _ = control.DataBindings.Add("Checked", binding, control.Tag.ToString());
            ChangeCheckBoxChecked(control);
            control.CheckedChanged += View.GdprCheckBox_CheckedChanged;
            return true;
        }

        #endregion

        #region Print a document

        public async void PrintGdprClicked(object? sender, EventArgs e)
        {
            PrintRichEditForm rtf = new();
            GdprDocument print = new(serviceProvider, rtf);
            await print.LoadData();
            print.PrintMode = PrintMode.Preview;
            print.CreateRenderer();
            print.Print();
        }

        #endregion
    }
}
