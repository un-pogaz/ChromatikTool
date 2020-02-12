using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System
{
    public static class AttributeExtension
    {
        static public T GetFrom<T>(object value)
        {
            IEnumerable<T> attributes = value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(T), true).OfType<T>();

            return attributes.IsEmpty() ? default(T) : attributes.Last();
        }
    }
}
