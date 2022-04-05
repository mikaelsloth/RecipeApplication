namespace Recipe.Presenter
{
    using Recipe.Models.Db;
    using Recipe.Views;
    using System;
    using System.Windows.Forms;

    public class RecipeCardDetailPartPresenter : PresenterBase<IRecipeCardDetailPartView, IRecipeCardDetailPartPresenter, IRecipeCardMainFormPresenter>, IRecipeCardDetailPartPresenter
    {
        private readonly IServiceProvider serviceProvider;
        private RecipeLine? recipeLine; 

        public RecipeCardDetailPartPresenter(IServiceProvider serviceProvider, IRecipeCardDetailPartView? view) : base(view)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), $"The {nameof(RecipeCardBrowsePartPresenter)} class could not start due to a missing dependency");
            View.Presenter = this;
            WireEvents();
        }

        private void WireEvents()
        {
            Close += View.Close_Clicked;
            View.AddLineRequest += AddClicked;
            View.DeleteLineRequest += DeleteClicked;
            View.EditLineRequest += EditClicked;
            View.PrintRecipe += PrintRecipeClicked;
            View.RemarkChanged += RemarksChanged;
        }

        private void RemarksChanged(object? sender, EventArgs e)
        { 
            if (State is RecipeCardDetailedState.ReadOnlySelected) throw new InvalidOperationException("Data cannot change in read only view");
            SetState(RecipeCardDetailedState.CardDirty); 
        }
 
        private void PrintRecipeClicked(object? sender, EventArgs e) => throw new NotImplementedException();
        
        private void EditClicked(object? sender, EventArgs e)
        {
            if (State is RecipeCardDetailedState.ReadOnlySelected) throw new InvalidOperationException("Data cannot change in read only view");
            if (State is RecipeCardDetailedState.New or RecipeCardDetailedState.LineDirty) if (ShowAlreadyEditingWarningOnEdit()) return;
            if (sender is not RecipeLineView rlv) throw new InvalidOperationException("Cannot identify sending object");
            recipeLine = rlv.DataSource as RecipeLine;
            SetState(RecipeCardDetailedState.Edit);
        }

        private bool ShowAlreadyEditingWarningOnEdit() => throw new NotImplementedException(); // return false if autosave is chosen, true if continue to edit

        private bool ShowAlreadyEditingWarningOnDelete() => throw new NotImplementedException(); // return false if autosave is chosen, true if continue to edit

        private void DeleteClicked(object? sender, EventArgs e)
        {
            if (State is RecipeCardDetailedState.ReadOnlySelected) throw new InvalidOperationException("Data cannot change in read only view");
            if (State is RecipeCardDetailedState.New or RecipeCardDetailedState.LineDirty) if (ShowAlreadyEditingWarningOnEdit()) return;
            if (sender is not RecipeLineView rlv) throw new InvalidOperationException("Cannot identify sending object");
            recipeLine = rlv.DataSource as RecipeLine;
        }

        private void AddClicked(object? sender, EventArgs e) => throw new NotImplementedException();

        public void CancelClicked(object? sender, EventArgs e) => throw new NotImplementedException();

        public void SaveClicked(object? sender, EventArgs e) => throw new NotImplementedException();

        public override IRecipeCardMainFormPresenter Parent
        {
            get => base.Parent;
            set
            {
                base.Parent = value;
                SetInitialConfiguration();
            }
        }
        

        private void SetInitialConfiguration() => Parent.Close += OnClose;

        public RecipeCardDetailedState State { get; private set; } = RecipeCardDetailedState.NotDefined;

        public override event FormClosingEventHandler? Close;

        public override void OnClose(object? sender, FormClosingEventArgs e) => Close?.Invoke(sender, e);
                
        public void SetFocus() => throw new NotImplementedException();
        
        public void SetState(RecipeCardDetailedState state)
        {
            if (state != State)
            {
                switch (state)
                {
                    case RecipeCardDetailedState.Blank:
                        // no card selected
                        break;
                    case RecipeCardDetailedState.New:
                        // goto add mode
                        break;
                    case RecipeCardDetailedState.Selected:
                        // display an editable card
                        break;
                    case RecipeCardDetailedState.ReadOnlySelected:
                        // display a non-editable card
                        break;
                    case RecipeCardDetailedState.Edit:
                        // goto edit mode
                        break;
                    case RecipeCardDetailedState.CardDirty:
                        // Edits have been made to remarks
                        break;
                    case RecipeCardDetailedState.LineDirty:
                        // Edits have been made to remarks
                        break;
                    case RecipeCardDetailedState.NotDefined:
                    default:
                        throw new InvalidOperationException("This state is invalid to set!");
                }

                State = state;
            }
        }
    }
}
