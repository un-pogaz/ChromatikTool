using System;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace System.IO.Compression
{
    /// <summary>
    /// 
    /// </summary>
    static public class ZipArchiveExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="archive"></param>
        /// <param name="entryName"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        static public ZipArchiveEntry GetEntry(this ZipArchive archive, string entryName, StringComparison stringComparison)
        {
            foreach (ZipArchiveEntry item in archive.Entries)
                if (PathTool.ToLinux(item.FullName).Equals(PathTool.ToLinux(entryName), stringComparison))
                    return item;

            return null;
        }

        /// <summary>
        /// Obtiens le texte
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        static public string GetContentText(this ZipArchiveEntry entry)
        {
            using (StreamReader reader = new StreamReader(entry.Open()))
                return reader.ReadToEnd();
        }
        /// <summary>
        /// Obtiens les lignes du texte
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        static public string[] GetContentLines(this ZipArchiveEntry entry)
        {
            string[] rslt = null;

            using (StreamReader reader = new StreamReader(entry.Open()))
            {
                string line = reader.ReadLine();
                if (line != null)
                    rslt = new string[0];
                while (line != null)
                {
                    rslt = rslt.Concat(new string[] { line });
                    line = reader.ReadLine();
                }
            }

            return rslt;
        }
        /// <summary>
        /// Obtiens le contenues de l'entrée
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        static public byte[] GetContentBytes(this ZipArchiveEntry entry)
        {
            byte[] rslt = null;

            using (Stream reader = entry.Open())
            {
                int b = reader.ReadByte();
                if (b >= 0)
                    rslt = new byte[0];
                while (b >= 0)
                {
                    rslt = rslt.Concat(new byte[] { (byte)b });
                    b = reader.ReadByte();
                }
            }

            return rslt;
        }
    }
}
