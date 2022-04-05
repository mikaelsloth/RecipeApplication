namespace Recipe.Views
{
    using System;
    using System.Windows.Forms;

    [Obsolete("Replace with Modern Remarks Button")]
    public class PropertyButton : Button
    {
        private bool databool = false;

        public bool DataBool
        {
            get => databool; 
            set 
            {
                bool oldvalue = databool;
                if (oldvalue != value)
                {
                    databool = value;
                    OnDataBoolChanged();
                }
            }
        }

        public virtual void OnDataBoolChanged() => DataBoolChanged?.Invoke(this, new());

        public event EventHandler? DataBoolChanged;
    }
}
