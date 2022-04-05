namespace Recipe
{
    using Recipe.Controller;
    using Recipe.Printing;
    using System;
    using System.Windows.Forms;

    public partial class TestForm : Form
    {
        private readonly IServiceProvider serviceProvider;
        private readonly PrintRichEditForm rtf;
        private GdprDocument? print;

        public TestForm(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InitializeComponent();
            rtf = new();
        }

        private async void Button1_Click(object? sender, EventArgs e)
        {
            //else
            //{
            //    rtf.LoadRTFContent(com.RftData);
            //}
            //PrintRichEditForm form = new();

            //print = new GdprDocument(serviceProvider, rtf);
            //await print.LoadData();
            //print.PrintMode = PrintMode.Preview;
            //modernBindingNavigator1.AddStandardItems();

            object? obj = serviceProvider.GetService(typeof(IRecipeLineController));
            if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(IRecipeLineController), "Not defined by ServiceProvider - call IT development");
            IRecipeLineController instance = (IRecipeLineController)obj;
        }

        private void Line_Enter(object? sender, EventArgs e) => MessageBox.Show("YES");

        private void Button2_Click(object sender, EventArgs e)
        {
            //if (print != null)
            //print.Print();
            //print = null;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //var obj = serviceProvider.GetService(typeof(ICommonEntitiesController));
            //if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(ICommonEntitiesController), "Not defined by ServiceProvider - call IT development");
            //var instance = (ICommonEntitiesController)obj;

            //CommonEntity? com = await instance.Get();

            //if (com == null)
            //{
            //    return;
            //}
            //com.RftData = rtf.PersistRTFContent();
            //await instance.Modify(com);
            //if (print != null)
            //    print.CreateRenderer();
            
        }
    }
}
