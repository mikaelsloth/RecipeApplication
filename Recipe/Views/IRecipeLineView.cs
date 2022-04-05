namespace Recipe.Views
{
    using System.Drawing;
    using System.Windows.Forms;
    using static System.Windows.Forms.Control;

    public interface IRecipeLineView
    {
        BindingSource BindingSource { get; }
        
        object? DataSource { get; set; }
        
        ControlCollection GetAllControls { get; }
        
        Color GridColor { get; set; }
    }
}