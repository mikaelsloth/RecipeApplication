namespace Recipe.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultBindingProperty("Remarks")]
    public partial class RemarksButton : UserControl
    {
        private bool _expanded = false;
        private string? remarks;
        private Form? form;
        private readonly EventArgs ev = EventArgs.Empty;

        public RemarksButton()
        {
            InitializeComponent();
        }

        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text 
        { 
            get => Button.Text; 
            set => Button.Text = value; 
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue(HorizontalAlignment.Left)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Indicates where the popup control should be positioned to this control.")]
        public CallOutFormPosition CallOutPosition { get; set; } = CallOutFormPosition.TopRight;

        [Category("Appearance")]
        [Description("The text associated with the popup control.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string? Remarks 
        { 
            get => remarks;
            set
            {
                remarks = value;
                OnRemarksChanged(ev);
            }
        }

        [Browsable(true)]
        public event EventHandler? RemarksChanged;

        [Browsable(true)]
        public event EventHandler? ExpandedChanged;

        [Browsable(false)]
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnExpandedChanged(ev);
                }
            }
        }

        protected virtual void OnRemarksChanged(EventArgs e)
        {
            Button.Font = string.IsNullOrEmpty(remarks) ? new Font(Font, FontStyle.Regular) : new Font(Font, FontStyle.Bold);
            RemarksChanged?.Invoke(this, e);
        }

        protected virtual void OnExpandedChanged(EventArgs e)
        {
            switch (_expanded)
            {
                case false:
                    if (form is not null)
                    {
                        Remarks = form.Text;
                        form.Close();
                    }

                    break;
                case true:
                    form?.Close();
                    form = new RoundedForm(this, Remarks)
                    {
                        StartPosition = FormStartPosition.Manual,
                        BorderRadius = 25,
                        BorderSize=3,
                        BackColor = SystemColors.ActiveBorder
                    };
                    Point start = CallOutPosition switch
                    {
                        CallOutFormPosition.TopLeft => new(Left - (form.Width + 2), Top - (form.Height + 2)),
                        CallOutFormPosition.BottomLeft => new(Left - (form.Width + 2), Top + Height + 2),
                        CallOutFormPosition.BottomRight => new(Right + 2, Top + Height + 2),
                        CallOutFormPosition.TopMiddle => new(Right - (Width / 2) - (form.Width / 2), Top - (form.Height + 2)),
                        CallOutFormPosition.BottomMiddle => new(Right - (Width / 2) - (form.Width / 2), Top + Height + 2),
                        CallOutFormPosition.LeftMiddle => new(Left - (form.Width + 2), Top + (Height / 2) - (form.Height / 2)),
                        CallOutFormPosition.RightMiddle => new(Right + 2, Top + (Height / 2) - (form.Height / 2)),
                        _ => new(Right + 2, Top - (form.Height + 2)),
                    };
                    form.Location = Parent.PointToScreen(start);                    
                    form.Show();
                    break;
            }

            ExpandedChanged?.Invoke(this, e);
        }

        private void Button_Click(object? sender, EventArgs e) => Expanded = !_expanded;
    }
}
