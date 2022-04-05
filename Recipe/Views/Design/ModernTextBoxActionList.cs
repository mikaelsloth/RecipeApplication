namespace Recipe.Views.Design
{
    using System.ComponentModel;
    using System.ComponentModel.Design;

    public class ModernTextBoxActionList : DesignerActionList
    {
        public ModernTextBoxActionList(ModernTextBoxDesigner designer) : base(designer.Component)
        {
        }

        public bool Multiline
        {
            get => ((ModernTextBox)Component).Multiline;
            set => TypeDescriptor.GetProperties(Component)["Multiline"].SetValue(Component, value);
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new();
            _ = items.Add(new DesignerActionPropertyItem("Multiline", "MultiLine", "Properties", "Puts the TextBox in a mode where it can display multiple lines of text"));
            return items;
        }
    }
}