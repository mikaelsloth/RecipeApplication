#nullable disable

namespace Recipe.Views
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    internal class TextBoxAutoCompleteSourceConverter : EnumConverter
    {
        public TextBoxAutoCompleteSourceConverter(Type type) : base(type)
        {
        }

        /// <summary>
        ///  Gets a collection of standard values for the data type this validator is
        ///  designed for.
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            StandardValuesCollection values = base.GetStandardValues(context);
            ArrayList list = new();
            int count = values.Count;
            for (int i = 0; i < count; i++)
            {
                string currentItemText = values[i].ToString();
                if (!currentItemText.Equals("ListItems")) _ = list.Add(values[i]);
            }

            return new StandardValuesCollection(list);
        }
    }
}
