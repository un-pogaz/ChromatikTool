using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{

    static public class JObjectWrite
    {
        /// <summary>
        /// 
        /// </summary>
        static public JsonSerializerSettings DefaultSettings { get; } = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            StringEscapeHandling = StringEscapeHandling.Default,
            FloatFormatHandling = FloatFormatHandling.String,
        };
        /// <summary>
        /// 
        /// </summary>
        static public JsonSerializerSettings Settings { get; } = DefaultSettings;

        static public void Object(string path, JObject jobject)
        {
            Object(path, jobject, Settings);
        }

        static public void Object(string path, JObject jobject, JsonSerializerSettings settings)
        {
            using (StreamWriter tw = new StreamWriter(path))
            {
                using (JsonTextWriter writer = new JsonTextWriter(tw))
                {
                    writer.AutoCompleteOnClose = true;
                    if (settings.Formatting == Formatting.Indented)
                    {
                        writer.Formatting = Formatting.Indented;
                        writer.IndentChar = '\t';
                        writer.Indentation = 1;
                    }
                    else
                        writer.Formatting = Formatting.None;
                    writer.QuoteName = true;
                    writer.StringEscapeHandling = settings.StringEscapeHandling;
                    jobject.CreateWriter();
                    using (JsonReader reader = jobject.CreateReader())
                        writer.WriteToken(reader);
                }
            }
        }
    }
}
