using System;
using System.Linq;
using System.Text;
using System.IO;
using System.MimeType;

namespace System.Base64
{
    static public class Base64file
    {
        static public string FromFile(string filePath) { return FromFile(filePath, true); }

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

        static public void Create(string filePath, string base64)
        {
            // One line
            string b64 = base64.Replace(Environment.NewLine, string.Empty).Trim();

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
