using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
    /// <summary>
    /// Static class for write a JSON file
    /// </summary>
    static public class JObjectWrite
    {
        /// <summary>
        /// Default stting used by the <see cref="JObjectWrite"/>
        /// </summary>
        static public JsonSerializerSettings DefaultSettings { get; } = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            StringEscapeHandling = StringEscapeHandling.Default,
            FloatFormatHandling = FloatFormatHandling.String,
        };
        /// <summary>
        /// Setting used by the <see cref="JObjectWrite"/>
        /// </summary>
        static public JsonSerializerSettings Settings { get; } = DefaultSettings;

        /// <summary>
        /// Write a JSON file from a <see cref="JObject"/>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="jobject"></param>
        static public void Object(string path, JObject jobject)
        {
            Object(path, jobject, Settings);
        }
        /// <summary>
        /// Write a JSON file from a <see cref="JObject"/>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="jobject"></param>
        /// <param name="settings"></param>
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
