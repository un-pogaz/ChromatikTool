using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace System.IO.Compression
{
    static public class ZipArchiveExtension
    {
        static public ZipArchiveEntry GetEntry(this ZipArchive archive, string entryName, StringComparison stringComparison)
        {
            foreach (ZipArchiveEntry item in archive.Entries)
                if (PathTool.ToLinux(item.FullName).Equals(PathTool.ToLinux(entryName), stringComparison))
                    return item;

            return null;
        }

        static public string GetContentText(this ZipArchiveEntry entry)
        {
            using (StreamReader reader = new StreamReader(entry.Open()))
                return reader.ReadToEnd();
        }
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
                    rslt = rslt.Append(new string[] { line });
                    line = reader.ReadLine();
                }
            }

            return rslt;
        }
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
                    rslt = rslt.Append(new byte[] { (byte)b });
                    b = reader.ReadByte();
                }
            }

            return rslt;
        }
    }
}
