namespace Recipe.Properties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    public class IntArrayConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type) => type == typeof(string);

        public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type) => type == typeof(string);

        public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value is not IntArray arr) throw new ArgumentOutOfRangeException(nameof(value), "object must be of type IntArray");
            StringBuilder sb = new();
            foreach (int i in arr)
                _ = sb.Append(i).Append(';');
            return sb.ToString(0, Math.Max(0, sb.Length - 1));
        }

        public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            List<int> arr = new();
            if (data is string s)
            {
                foreach (string txt in s.Split(';'))
                    arr.Add(int.Parse(txt));
            }

            return new IntArray(arr);
        }
    }
}
