namespace Recipe.Views
{
    using Recipe.Presenter;
    using System;
    using System.Windows.Forms;

    public partial class RecipeCardMainForm : Form, IRecipeCardMainFormView
    {
        private IRecipeCardMainFormPresenter? presenter = null;

        public RecipeCardMainForm()
        {
            InitializeComponent();
        }

        public IRecipeCardMainFormPresenter Presenter 
        { 
            get => presenter ?? throw new ArgumentNullException(nameof(presenter));
            set => presenter = value; 
        }

        public void Close_Clicked(object? sender, FormClosingEventArgs e) => Close();

        public void LoadView<P>(IView<P> view)
        {
            if (view is UserControl userControl)
            {
                userControl.Dock = DockStyle.Top;
                Controls.Add(userControl);
            }
        }
    }
}
