using System;
using System.Collections.Generic;
using System.Text;

namespace System.Reflection
{
    /// <summary>
    /// Static class to extend <see cref="Type"/>
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Get the value of a the Field
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static public object GetValueOf(this object obj, string fieldName)
        {
            return obj.GetValueOf(fieldName, false);
        }

        /// <summary>
        /// Get the value of a the Field, includ the Private and Protected
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="incluedPrivate">True for includ the Private and Protected</param>
        /// <returns></returns>
        static public object GetValueOf(this object obj, string fieldName, bool incluedPrivate)
        {
            if (incluedPrivate)
                return obj.GetValueOf(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            else
                return obj.GetValueOf(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
        }

        /// <summary>
        /// Get the value of a the Field with the specified <see cref="BindingFlags"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        static public object GetValueOf(this object obj, string fieldName, BindingFlags bindingFlags)
        {
            if (obj == null)
                return null;

            try
            {
                FieldInfo field = obj.GetType().GetField(fieldName, bindingFlags);

                if (field != null && (field.IsPublic || field.IsPrivate && bindingFlags.HasFlag(BindingFlags.NonPublic)))
                    return field.GetValue(obj);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the <see cref="FieldInfo"/>, includ the Private and Protected
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="incluedPrivate">True for includ the Private and Protected</param>
        /// <returns></returns>
        static public FieldInfo GetField(this Type type, string name, bool incluedPrivate)
        {
            if (incluedPrivate)
                return type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            else
                return type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
        }
    }
}
