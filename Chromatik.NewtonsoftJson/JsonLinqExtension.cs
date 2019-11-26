using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Newtonsoft.Json.Linq
{
    static public class JsonLinqExtension
    {
        static public IEnumerable<JProperty> Properties(this JProperty jcontainer)
        {
            if (jcontainer.Value.Type == JTokenType.Object)
                return ((JObject)jcontainer.Value).Properties();
            else
                return new JProperty[0];
        }
        static public JProperty Property(this JProperty jcontainer, string name)
        {
            if (jcontainer.Value.Type == JTokenType.Object)
                return ((JObject)jcontainer.Value).Property(name);
            else
                return null;
        }
        static public JProperty Property(this JProperty jcontainer, string name, StringComparison comparison)
        {
            if (jcontainer.Value.Type == JTokenType.Object)
                return ((JObject)jcontainer.Value).Property(name, comparison);
            else
                return null;
        }

        static public JProperty AddProperty(this JProperty jproperty, string name)
        {
            return jproperty.AddProperty(name, new JObject());
        }
        static public JProperty AddProperty(this JProperty jproperty, string name, JValue value)
        {
            return jproperty.AddProperty(name, (value as JToken));
        }
        static public JProperty AddProperty(this JProperty jproperty, string name, JArray array)
        {
            return jproperty.AddProperty(name, (array as JToken));
        }
        static public JProperty AddProperty(this JProperty jproperty, string name, JProperty property)
        {
            return jproperty.AddProperty(name, (property as JToken));
        }
        static public JProperty AddProperty(this JProperty jproperty, string name, JObject JObject)
        {
            return jproperty.AddProperty(name, (JObject as JToken));
        }
        static public JProperty AddProperty(this JProperty jproperty, string name, JToken token)
        {
            (jproperty.Value as JObject).Add(name, token);
            return jproperty.Value.Last as JProperty;
        }


        static public JProperty AddProperty(this JObject jobject, string name)
        {
            return jobject.AddProperty(name, new JObject());
        }
        static public JProperty AddProperty(this JObject jobject, string name, JToken token)
        {
            jobject.Add(name, token);
            return jobject.Last as JProperty;
        }
    }
}
