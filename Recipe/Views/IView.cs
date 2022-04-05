namespace Recipe.Views
{
    using System.Windows.Forms;

    public interface IView<P>
    {
        P Presenter { get; set; }

        void Close_Clicked(object? sender, FormClosingEventArgs e);
    }
}
