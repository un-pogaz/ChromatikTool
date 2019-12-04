using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
    /// <summary>
    /// Static extension class for <see cref="JObject"/>
    /// </summary>
    static public class JObjectCreate
    {
        /// <summary>
        /// Load a JSON from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public JObject Object(string path)
        {
            return ObjectJSON(File.ReadAllText(path));
        }
        /// <summary>
        /// Load a JSON from file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        static public JObject Object(string path, JsonSerializerSettings setting)
        {
            return ObjectJSON(File.ReadAllText(path), setting);
        }
        /// <summary>
        /// Load a JSON from a <see cref="string"/>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        static public JObject ObjectJSON(string json)
        {
            return ObjectJSON(json, JObjectWrite.Settings);
        }
        /// <summary>
        /// Load a JSON from a <see cref="string"/>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        static public JObject ObjectJSON(string json, JsonSerializerSettings setting)
        {
            return JsonConvert.DeserializeObject<JObject>(json, setting);
        }
    }
}
