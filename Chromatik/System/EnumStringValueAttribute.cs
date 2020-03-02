using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System
{
    /// <summary>
    /// An attribute containing an arbitrary string value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumStringValueAttribute : Attribute
    {
        /// <summary>
        /// Arbitrary string value.
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// Set a arbitrary string value.
        /// </summary>
        /// <param name="value"></param>
        public EnumStringValueAttribute(string value) { Value = value; }
        /// <summary>
        /// Get the string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string GetFrom(object value)
        {
            EnumStringValueAttribute rslt = Reflection.TypeExtension.GetAttributeLastFrom<EnumStringValueAttribute>(value);
            
            return rslt == null ? null : rslt.Value;
        }
    }
}
