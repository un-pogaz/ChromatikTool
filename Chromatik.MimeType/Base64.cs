using System;
using System.Linq;
using System.Text;
using System.IO;
using System.MimeType;

namespace System.Base64
{
    /// <summary>
    /// Static class for parse file in base 64
    /// </summary>
    static public class Base64file
    {
        /// <summary>
        /// Get Base64 from a file, in web format
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static public string FromFile(string filePath) { return FromFile(filePath, true); }
        /// <summary>
        /// Get Base64 from a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="WebFormat"></param>
        /// <returns></returns>
        static public string FromFile(string filePath, bool WebFormat)
        {
            if (File.Exists(filePath))
            {
                string b64 = Convert.ToBase64String(File.ReadAllBytes(filePath));
                if (WebFormat)
                    return "data:" + MimeTypes.GetMimeType(Path.GetExtension(filePath))[0] + ";base64," + b64;
                else
                    return b64;
            }
            else
                return null;
        }

        /// <summary>
        /// Create file from a Base64
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="base64"></param>
        static public void Create(string filePath, string base64)
        {
            // One line
            string b64 = base64.RegexBoucle("(\r|\n)", string.Empty).Trim();

            // Suppresion des metadonées base64url
            while (b64.Contains(","))
                b64 = b64.Remove(0, b64.IndexOf(',') + 1).Trim();

            // Rajout des compléments manquant
            while (b64.Length % 4 != 0)
                b64 += "=";

            // Remplacement des caractére de subtitution base64url et ecriture du fichier 
            File.WriteAllBytes(filePath, Convert.FromBase64String(b64));
        }
    }
}
