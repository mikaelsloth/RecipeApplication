namespace Recipe.Presenter
{
    using Recipe.Views;
    using System.Windows.Forms;

    public interface IPresenter
    {
        event FormClosingEventHandler? Close;

        void OnClose(object? sender, FormClosingEventArgs e);
    }
}