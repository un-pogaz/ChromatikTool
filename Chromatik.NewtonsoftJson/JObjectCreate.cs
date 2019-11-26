using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
    static public class JObjectCreate
    {
        static public JObject Object(string path)
        {
            return ObjectJSON(File.ReadAllText(path));
        }
        static public JObject Object(string path,JsonSerializerSettings setting)
        {
            return ObjectJSON(File.ReadAllText(path), setting);
        }
        static public JObject ObjectJSON(string json)
        {
            return ObjectJSON(json, JObjectWrite.Settings);
        }
        static public JObject ObjectJSON(string json, JsonSerializerSettings setting)
        {
            return JsonConvert.DeserializeObject<JObject>(json, setting);
        }
    }
}
