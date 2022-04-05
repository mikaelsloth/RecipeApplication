namespace Recipe.Presenter
{
    using System;
    using System.ComponentModel;

    public interface IRecipeCardBrowsePartPresenter : IPresenter
    {
        event AddingNewEventHandler? AddingNew;

        void BindingSource_AddingNew(object? sender, AddingNewEventArgs e);
        
        void BindingSource_PositionChanged(object? sender, EventArgs e);
        
        void View_ExpandedCollapsedChanged(object? sender, EventArgs e);
    }
}