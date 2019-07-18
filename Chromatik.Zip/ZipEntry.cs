using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Ionic.Zip;
using Ionic.Zlib;

namespace Chromatik.Zip
{
    /// <summary>
    /// Represents a entry in a ZipFile.
    /// </summary>
    [ComVisible(true)]
    public class ZipEntry
    {
        /// <summary>
        /// Base <see cref="Ionic.Zip.ZipEntry"/>
        /// </summary>
        protected Ionic.Zip.ZipEntry zipEntry { get; }

        /// <summary>
        /// Create a <see cref="ZipEntry"/>
        /// </summary>
        /// <param name="entry"></param>
        internal ZipEntry(Ionic.Zip.ZipEntry entry)
        {
            zipEntry = entry;
            /////////////////

            zipEntry.ProvisionalAlternateEncoding = Encoding.UTF8;
            zipEntry.UseUnicodeAsNecessary = true;

            if (zipEntry.CompressionLevel != CompressionLevel.BestCompression && zipEntry.CompressionLevel != CompressionLevel.None)
            {
                if (zipEntry.CompressionRatio == 0)
                    zipEntry.CompressionLevel = CompressionLevel.None;
                else
                    zipEntry.CompressionLevel = CompressionLevel.BestCompression;
            }
        }

        /// <summary>
        /// The name of the file contained in the ZipEntry.
        /// </summary>
        public string FileName {
            get { return zipEntry.FileName; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("FileName");
                zipEntry.FileName = ZipFile.ToEntryFormat(value);
            }
        }

        /// <summary>
        /// Provides a string representation of the instance.
        /// </summary>
        /// <returns></returns>
        new public string ToString() { return FileName; }

        /// <summary>
        /// Last Access time for the file represented by the entry.
        /// </summary>
        public DateTime AccessedTime { get { return zipEntry.AccessedTime; } set { zipEntry.AccessedTime = value; } }
        /// <summary>
        /// The file creation time for the file represented by the entry.
        /// </summary>
        public DateTime CreationTime { get { return zipEntry.CreationTime; } set { zipEntry.CreationTime = value; } }
        /// <summary>
        /// The time and date at which the file indicated by the ZipEntry was last modified.
        /// </summary>
        public DateTime LastModified { get { return zipEntry.LastModified; } set { zipEntry.LastModified = value; } }
        /// <summary>
        /// Last Modified time for the file represented by the entry.
        /// </summary>
        public DateTime ModifiedTime { get { return zipEntry.ModifiedTime; } set { zipEntry.ModifiedTime = value; } }
        /// <summary>
        /// The file attributes for the entry.
        /// </summary>
        public FileAttributes Attributes { get { return zipEntry.Attributes; } set { zipEntry.Attributes = value; } }
        /// <summary>
        /// The comment attached to the ZipEntry.
        /// </summary>
        public string Comment { get { return zipEntry.Comment; } set { zipEntry.Comment = value; } }
        /// <summary>
        /// The compressed size of the file, in bytes, within the zip archive.
        /// </summary>
        public long CompressedSize { get { return zipEntry.CompressedSize; } }
        /// <summary>
        /// The size of the file, in bytes, before compression, or after extraction.
        /// </summary>
        public long UncompressedSize { get { return zipEntry.UncompressedSize; } }
        /// <summary>
        /// The ratio of compressed size to uncompressed size of the ZipEntry.
        /// </summary>
        public double CompressionRatio { get { return zipEntry.CompressionRatio; } }

        /// <summary>
        /// Indicates whether the entry was included in the most recent save.
        /// </summary>
        public bool IncludedInMostRecentSave { get { return zipEntry.IncludedInMostRecentSave; } }
        /// <summary>
        /// Sets the NTFS Creation, Access, and Modified times for the given entry.
        /// </summary>
        /// <param name="created">the creation time of the entry.</param>
        /// <param name="accessed">the last access time of the entry.</param>
        /// <param name="modified">the last modified time of the entry.</param>
        public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified) { zipEntry.SetEntryTimes(created, accessed, modified); }
        
        /// <summary>
        /// Sets if the entry is compresed when saving the zip archive.
        /// </summary>
        public bool IsUncompressed
        {
            get { return (zipEntry.CompressionLevel == CompressionLevel.None); }
            set
            {
                if (value)
                    zipEntry.CompressionLevel = CompressionLevel.None;
                else
                    zipEntry.CompressionLevel = CompressionLevel.BestCompression;
            }
        }

        /// <summary>
        /// Extract the entry to the filesystem, at the specified location.
        /// </summary>
        /// <param name="destinationFileName">the specified location exracted file.</param>
        public void Extract(string destinationFileName)
        {
            Extract(destinationFileName, false);
        }
        /// <summary>
        /// Extract the entry to the filesystem, at the specified location.
        /// </summary>
        /// <param name="destinationFileName">the specified location exracted file.</param>
        /// <param name="overwrite">true to replace an existing file with the same name as the destination; otherwise, false.</param>
        public void Extract(string destinationFileName, bool overwrite)
        {
            FileMode fm = FileMode.CreateNew;
            if (overwrite)
                fm = FileMode.Create;

            string dir = Path.GetDirectoryName(Path.GetFullPath(destinationFileName));
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (FileStream fileStream = new FileStream(destinationFileName, fm, FileAccess.ReadWrite, FileShare.Read))
            {
                using (Stream stream = GetStream())
                    stream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// Get a clone stream of the entry content.
        /// </summary>
        /// <returns></returns>
        public Stream GetStream()
        {
            MemoryStream loadMemory = new MemoryStream();
            zipEntry.Extract(loadMemory);
            loadMemory.Position = 0;
            MemoryStream stream = new MemoryStream();
            loadMemory.CopyTo(stream);
            stream.Position = 0;
            loadMemory.Position = 0;
            return stream;
        }
        /// <summary>
        /// Get the content text of the entry.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            using (StreamReader reader = new StreamReader(GetStream()))
                return reader.ReadToEnd();
        }
        /// <summary>
        /// Get the content line of the entry.
        /// </summary>
        /// <returns></returns>
        public string[] GetLines()
        {
            string[] rslt = null;
            using (StreamReader reader = new StreamReader(GetStream()))
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
        /// Get the content <see cref="byte"/>[] of the entry.
        /// </summary>
        /// <returns></returns>
        public byte[] GetByte()
        {
            using (MemoryStream memory = (MemoryStream)GetStream())
                return memory.ToArray();
        }

        
        ///// <summary>
        ///// The stream that provides content for the ZipEntry.
        ///// </summary>
        //public Stream InputStream { get { return zipEntry.InputStream; } set { zipEntry.InputStream = value; } }

        ///// <summary>
        ///// Extracts the entry to the specified stream.
        ///// </summary>
        ///// <param name="stream">the stream to which the entry should be extracted.</param>
        //public void Extract(Stream stream) { zipEntry.Extract(stream); }

    }
}
