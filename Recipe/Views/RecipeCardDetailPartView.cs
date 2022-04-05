namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;

    public partial class RecipeCardDetailPartView : UserControl, IRecipeCardDetailPartView
    {
        private IRecipeCardDetailPartPresenter? presenter;
        private bool readOnly = false;

        public RecipeCardDetailPartView()
        {
            InitializeComponent();
        }

        public IRecipeCardDetailPartPresenter Presenter
        {
            get => presenter ?? throw new ArgumentNullException(nameof(Presenter));
            set => presenter = value;
        }

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Dispose(true);

        private void PrintButton_Click(object? sender, EventArgs e) => OnPrintRecipe(e);

        protected virtual void OnPrintRecipe(EventArgs e) => PrintRecipe?.Invoke(this, e);

        public event EventHandler? PrintRecipe;

        private void RemarksButton_RemarksChanged(object? sender, EventArgs e) => OnRemarkChanged(e);

        protected virtual void OnRemarkChanged(EventArgs e) => RemarkChanged?.Invoke(this, e);

        public event EventHandler? RemarkChanged;

        public virtual object? DataSource
        {
            get => RecipeCardBinding.DataSource;
            set => RecipeCardBinding.DataSource = value;
        }

        public virtual ControlCollection GetGridControlCollection(MedicinTypes types) => GetGridForMedicinType(types).Controls;

        private RecipeLinesGrid GetGridForMedicinType(MedicinTypes types) => types switch
        {
            MedicinTypes.Dråber => LiquidLinesGrid,
            MedicinTypes.Vitamin => PillsLinesGrid,
            _ => throw new ArgumentOutOfRangeException(nameof(types), types, "An invalied enumeration value has been specified"),
        };

        public virtual bool ReadOnly 
        { 
            get => readOnly;
            set
            {
                readOnly = value;
                SetReadOnlyStatus();
            }
        }

        private void SetReadOnlyStatus() => throw new NotImplementedException();

        private void AddPillsButton_Click(object? sender, EventArgs e) => OnAddLineRequest(sender, e);

        protected virtual void OnAddLineRequest(object? sender, EventArgs e) => AddLineRequest?.Invoke(this, e);

        public event EventHandler? AddLineRequest;

        private void AddLiquidButton_Click(object? sender, EventArgs e) => OnAddLineRequest(sender, e);

        private void LiquidLinesGrid_LineDeleteRequested(object? sender, EventArgs e) => OnDeleteLineRequest(sender, e);

        protected virtual void OnDeleteLineRequest(object? sender, EventArgs e) => DeleteLineRequest?.Invoke(sender, e);

        public event EventHandler? DeleteLineRequest;

        private void LiquidLinesGrid_LineEditRequested(object? sender, EventArgs e) => OnEditLineRequest(sender, e);

        protected virtual void OnEditLineRequest(object? sender, EventArgs e) => EditLineRequest?.Invoke(sender, e);

        public event EventHandler? EditLineRequest;

        private void PillsLinesGrid_LineDeleteRequested(object? sender, EventArgs e) => OnDeleteLineRequest(sender, e);

        private void PillsLinesGrid_LineEditRequested(object? sender, EventArgs e) => OnEditLineRequest(sender, e);

        public virtual int AddLine(RecipeLineView recipeLine,MedicinTypes types) => GetGridForMedicinType(types).Add(recipeLine);

        public virtual void DeleteLine(RecipeLineView recipeLine, MedicinTypes types) => GetGridForMedicinType(types).Remove(recipeLine);
    }
}
