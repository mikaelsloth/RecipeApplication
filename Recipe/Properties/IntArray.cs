namespace Recipe.Properties
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    [TypeConverter(typeof(IntArrayConverter))]
    public class IntArray : IEnumerable<int>
    {
        readonly List<int> _values;

        public IntArray() => _values = new List<int>();

        public IntArray(IEnumerable<int> values) =>  _values = new List<int>(values);

        public static implicit operator List<int>(IntArray arr) => new(arr._values);

        public static implicit operator IntArray(List<int> values) => new(values);

        public IEnumerator<int> GetEnumerator() => _values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<int>)_values).GetEnumerator();
    }
}
