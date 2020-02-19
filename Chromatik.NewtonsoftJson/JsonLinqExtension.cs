using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Newtonsoft.Json.Linq
{
    /// <summary>
    /// Static class extension for <see cref="JObject"/>
    /// </summary>
    static public class JsonLinqExtension
    {
        /// <summary>
        /// Get the value of the propertie in a <see cref="JObject"/>
        /// </summary>
        /// <param name="jcontainer"></param>
        /// <returns>A <see cref="JProperty"/> or null</returns>
        static public JObject ValueObjet(this JProperty jcontainer)
        {
            if (jcontainer.Value.Type == JTokenType.Object)
                return ((JObject)jcontainer.Value);
            else
                return null;
        }
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="JProperty"/>
        /// of this object's properties.</summary>
        /// <param name="jcontainer"></param>
        /// <returns></returns>
        static public IEnumerable<JProperty> Properties(this JProperty jcontainer)
        {
            JObject jobject = jcontainer.ValueObjet();
            if (jobject != null)
                return jobject.Properties();
            else
                return new JProperty[0];
        }
        /// <summary>
        /// Gets a <see cref="JProperty"/> with the specified name.
        /// </summary>
        /// <param name="jcontainer"></param>
        /// <param name="name">The property name</param>
        /// <returns>A <see cref="JProperty"/> matched with the specified name or null.</returns>
        static public JProperty Property(this JProperty jcontainer, string name)
        {
            JObject jobject = jcontainer.ValueObjet();
            if (jobject != null)
                return jobject.Property(name);
            else
                return null;
        }
        /// <summary>
        /// Gets the <see cref="JProperty"/> with the specified name. The exact name
        /// will be searched for first and if no matching property is found then the <see cref="StringComparison"/>
        /// will be used to match a property.</summary>
        /// <param name="jcontainer"></param>
        /// <param name="name">The property name</param>
        /// <param name="comparison">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns>A <see cref="JProperty"/> matched with the specified name or null.</returns>
        static public JProperty Property(this JProperty jcontainer, string name, StringComparison comparison)
        {
            JObject jobject = jcontainer.ValueObjet();
            if (jobject != null)
                return jobject.Property(name, comparison);
            else
                return null;
        }

        /// <summary>
        /// Add a <see cref="JProperty"/> and return it
        /// </summary>
        /// <param name="jproperty"></param>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>The added property</returns>
        static public JProperty AddProperty(this JProperty jproperty, string propertyName)
        {
            return jproperty.ValueObjet().AddProperty(propertyName);
        }
        /// <summary>
        /// Add a <see cref="JProperty"/> and return it
        /// </summary>
        /// <param name="jproperty"></param>
        /// <param name="propertyName"></param>
        /// <param name="value">The value</param>
        /// <returns>The added property</returns>
        static public JProperty AddProperty(this JProperty jproperty, string propertyName, JToken value)
        {
            return jproperty.ValueObjet().AddProperty(propertyName, value);
        }
        /// <summary>
        /// Add a <see cref="JProperty"/> and return it
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>The added property</returns>
        static public JProperty AddProperty(this JObject jobject, string propertyName)
        {
            return jobject.AddProperty(propertyName, JValue.CreateNull());
        }
        /// <summary>
        /// Add a <see cref="JProperty"/> and return it
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="value">The value</param>
        /// <returns>The added property</returns>
        static public JProperty AddProperty(this JObject jobject, string propertyName, JToken value)
        {
            jobject.Add(propertyName, value);
            return (JProperty)jobject.Last;
        }

        /// <summary>
        /// Remove the property with the specified name.
        /// </summary>
        /// <param name="jproperty"></param>
        /// <param name="name">Name of the property</param>
        /// <returns>true if item was successfully removed; otherwise, false.</returns>
        static public bool RemoveProperty(this JProperty jproperty, string name)
        {
            return jproperty.ValueObjet().Remove(name);
        }
    }
}
