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
        static private BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
        
        /// <summary>
        /// Get the value of a Field or Property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static public object GetValueOf(this object obj, string fieldName) { return obj.ExistedValueOf(fieldName, bindingFlags).Value; }
        /// <summary>
        /// Get the value of a Field or Property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static public T GetValueOf<T>(this object obj, string fieldName) { return obj.ExistedValueOf(fieldName, bindingFlags).CastValueOrDefault<T>(fieldName); }
        
        static public object GetValueOf(this object obj, string fieldName, BindingFlags bindingFlags) { return obj.ExistedValueOf(fieldName, bindingFlags).Value; }
        /// <summary>
        /// Get the value of a Field or Property with the specified <see cref="BindingFlags"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        static public T GetValueOf<T>(this object obj, string fieldName, BindingFlags bindingFlags) { return obj.ExistedValueOf(fieldName, bindingFlags).CastValueOrDefault<T>(fieldName); }

        static private KeyValuePair<bool, object> ExistedValueOf(this object obj, string fieldName, BindingFlags bindingFlags)
        {
            if (obj == null)
                return new KeyValuePair<bool, object>(false, null);

            try
            {
                FieldInfo field = obj.GetType().GetField(fieldName, bindingFlags);

                if (field != null)
                    return new KeyValuePair<bool, object>(true, field.GetValue(obj));
                else
                {
                    PropertyInfo propertie = obj.GetType().GetProperty(fieldName, bindingFlags);

                    object r = propertie.GetValue(obj);
                    if (propertie != null)
                        return new KeyValuePair<bool, object>(true, propertie.GetValue(obj));

                    return new KeyValuePair<bool, object>(false, null);
                }
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, object>(false, null);
            }
        }
        
        static private T CastValueOrDefault<T>(this KeyValuePair<bool, object> existedObj, string fieldName)
        {
            Type tType = typeof(T);

            if (tType.IsEnum)
            {
                if (existedObj.Key)
                {
                    if (existedObj.Value is T)
                        return (T)existedObj.Value;
                }
                else
                    return default(T);
            }
            else
            {
                if (existedObj.Key)
                {
                    if (existedObj.Value == null)
                        return default(T);
                    else if (existedObj.Value is T)
                        return (T)existedObj.Value;
                }
                else
                    return default(T);
            }

            throw new InvalidCastException("The value of the requested Field '"+ fieldName + "' is not of the Type called.");
        }
    }
}
