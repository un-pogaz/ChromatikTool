using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Get the value of a Field or Property with the specified <see cref="BindingFlags"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Invokes the void constructor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public object InvokeConstructor(this Type type)
        {
            return type.InvokeConstructor(new object[0]);
        }
        /// <summary>
        /// Invokes the constructor corresponding to the specified parameter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        static public object InvokeConstructor(this Type type, params object[] parameters)
        {
            if (type == null)
                return null;

            return type.GetConstructors().InvokeConstructor(parameters);
        }
        /// <summary>
        /// Invokes the constructor corresponding to the specified parameter
        /// </summary>
        /// <param name="constructors"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        static public object InvokeConstructor(this ConstructorInfo[] constructors, params object[] parameters)
        {
            if (constructors == null)
                return null;

            if (parameters == null)
                parameters = new object[0];

            Type[] parametersType = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                parametersType[i] = parameters[i].GetType();

            foreach (var item in constructors)
            {
                ParameterInfo[] param = item.GetParameters();
                if (param.Length == parametersType.Length)
                {
                    bool valide = true;
                    for (int i = 0; i < parametersType.Length; i++)
                        if (param[i].ParameterType != parametersType[i])
                        {
                            valide = false;
                            break;
                        }

                    if (valide)
                        return item.Invoke(parameters);
                }
            }

            return null;
        }
        /// <summary>
        /// Determines if the type is an inheritance from a other.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        static public bool IsInheritanceOf(this Type type, Type parent)
        {
            if (type == null || parent == null)
                return false;
            else if (type.BaseType == null)
                return false;
            else if (type == parent)
                return true;
            else if (type.BaseType == parent)
                return true;
            else
                return type.BaseType.IsInheritanceOf(parent);

        }

        /// <summary>
        /// Get the type specified to the namespace and the name.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <param name="fullnamespace"></param>
        /// <returns></returns>
        static public Type GetType(this Assembly assembly, string name, string fullnamespace)
        {
            if (assembly == null)
                return null;

            return assembly.GetType(fullnamespace + "." + name, false, true);
        }


        static public T[] GetAttributesFrom<T>(object value)
        {
            IEnumerable<T> attributes = value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(T), true).OfType<T>();

            return attributes.ToArray();
        }
        static public T GetAttributeLastFrom<T>(object value)
        {
            T[] tbl = GetAttributesFrom<T>(value);
            return tbl.IsEmpty() ? default(T) : tbl.Last();
        }

        /// <summary>
        /// Get the Stream of the specified Resource in the assembly associated to the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public IO.Stream GetManifestResourceStream(this Type type, params string[] name)
        {
            string fullName = name.Join(".");
            if (type.GetManifestResourceNames().Contains(fullName))
            { }
            else
            {
                foreach (var item in type.GetManifestResourceNames())
                    if (item.Equals(fullName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        fullName = item;
                        break;
                    }
            }

            return type.Assembly.GetManifestResourceStream(name.Join("."));
        }
        /// <summary>
        /// Get the strint of the specified Resource in the assembly associated to the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public string GetManifestResourceString(this Type type, params string[] name)
        {
            return type.GetManifestResourceString(Encoding.UTF8, name);
        }
        /// <summary>
        /// Get the strint of the specified Resource in the assembly associated to the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static public string GetManifestResourceString(this Type type, Encoding encoding, params string[] name)
        {
            IO.Stream stream = type.GetManifestResourceStream(name);
            if (stream != null)
                using (IO.StreamReader reader = new IO.StreamReader(stream, encoding, true, 2048))
                    return reader.ReadToEnd();

            return null;
        }

        /// <summary>
        /// Get the nmaes of all Resource in the assembly associated to the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public string[] GetManifestResourceNames(this Type type)
        {
            Assembly assembly = type.Assembly;
            return assembly.GetManifestResourceNames();
        }
        /// <summary>
        /// Get the Info of the specified Resource in the assembly associated to the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public ManifestResourceInfo GetManifestResourceInfo(this Type type, params string[] name)
        {
            return type.Assembly.GetManifestResourceInfo(name.Join("."));
        }
        /// <summary>
        /// Get the name of the Assembly of the <see cref="Type"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public string GetAssemblyName(this Type type)
        {
            return type.Assembly.GetName().Name;
        }


    }
}
