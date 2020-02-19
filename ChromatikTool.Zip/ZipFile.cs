using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Chromatik.Zip
{
    /// <summary>
    /// The ZipFile type represents a zip archive file. This class reads and
    /// writes zip files, as definedin the format for zip described by PKWare.
    /// </summary>
    public partial class ZipFile : IEnumerable<ZipEntry>, IEnumerable, IDisposable
    {
        /// <summary>
        /// Base <see cref="Ionic.Zip.ZipFile"/>
        /// </summary>
        protected Ionic.Zip.ZipFile zipFile;

        /// <summary>
        /// Create a new empty <see cref="ZipFile"/>.
        /// </summary>
        public ZipFile() : this(new Ionic.Zip.ZipFile(UTF8SansBomEncoding.Default))
        { }

        static private Ionic.Zip.ZipFile GetZipFile(Stream stream)
        {
            try
            {
                return Ionic.Zip.ZipFile.Read(stream);
            }
            catch (Ionic.Zip.ZipException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Ionic.Zlib.ZlibException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Load a <see cref="ZipFile"/> from a  stream.
        /// </summary>
        public ZipFile(Stream stream) : this(GetZipFile(stream))
        { }

        static internal Ionic.Zip.ReadOptions ReadOption = new Ionic.Zip.ReadOptions()
        {
            Encoding = UTF8SansBomEncoding.Default,
        };

        static private Ionic.Zip.ZipFile GetZipFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    return Ionic.Zip.ZipFile.Read(fileName);
                else
                    return Ionic.Zip.ZipFile.Read(fileName, ReadOption);
            }
            catch (Ionic.Zip.ZipException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Ionic.Zlib.ZlibException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Load a <see cref="ZipFile"/> from a file, or create a new if it does not exist.
        /// </summary>
        public ZipFile(string path) : this(GetZipFile(path))
        { }

        private ZipFile(Ionic.Zip.ZipFile IonicZip)
        {
            IonicZip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
            IonicZip.ExtractExistingFile = Ionic.Zip.ExtractExistingFileAction.Throw;
            IonicZip.Strategy = Ionic.Zlib.CompressionStrategy.Default;
            IonicZip.AlternateEncoding = UTF8SansBomEncoding.Default;
            IonicZip.AlternateEncodingUsage = Ionic.Zip.ZipOption.Always;
            IonicZip.MaxOutputSegmentSize = 0;

            foreach (var item in IonicZip.Entries)
            {
                item.AlternateEncoding = UTF8SansBomEncoding.Default;
                item.AlternateEncodingUsage = Ionic.Zip.ZipOption.Always;
            }

            ///////////////////
            zipFile = IonicZip;
            ///////////////////

            Delete__MACOSX(this);
        }

        /// <summary>
        /// The name of this ZIP archive, on disk.
        /// </summary>
        public string FileName
        {
            get { return zipFile.Name; }
            set
            {
                try
                {
                    zipFile.Name = value;
                }
                catch (Ionic.Zip.ZipException ex)
                {
                    throw ZipException.FromIonic(ex);
                }
                catch (Ionic.Zlib.ZlibException ex)
                {
                    throw ZipException.FromIonic(ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// A comment attached to the ZIP archive.
        /// </summary>
        public string Comment
        {
            get { return zipFile.Comment; }
            set
            {
                try
                {
                    zipFile.Comment = value;
                }
                catch (Ionic.Zip.ZipException ex)
                {
                    throw ZipException.FromIonic(ex);
                }
                catch (Ionic.Zlib.ZlibException ex)
                {
                    throw ZipException.FromIonic(ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Obtains the number of elements contained in the collection.
        /// </summary>
        public int Count { get { return Entries.Count; } }
        /// <summary>
        /// Gets the ZipEntry located at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ZipEntry this[int index] { get { return Entries[index]; } }
        /// <summary>
        /// Gets the ZipEntry located at the specified FileName.
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns>null if not found</returns>
        public ZipEntry this[string entryName] { get { return Entries[entryName]; } }
        
        ZipEntryCollection _entries;
        /// <summary>
        /// List of entries in the ZIP archive.
        /// </summary>
        public ZipEntryCollection Entries
        {
            get
            {
                if (_entries == null)
                    _entries = new ZipEntryCollection(zipFile.Entries);

                return _entries;
            }
        }
        ZipEntryCollection _entriesSorted;

        List<string> _entriesNames;
        /// <summary>
        /// List only the entries name in the ZIP archive.
        /// </summary>
        public IReadOnlyList<string> EntriesNames
        {
            get
            {
                if (true)
                     _entriesNames = new List<string>();

                _entriesNames.Clear();
                foreach (var item in Entries)
                    _entriesNames.Add(item.FileName);

                return _entriesNames;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entyName"></param>
        /// <returns></returns>
        public ZipEntry GetEntry(string entyName) { return Entries[entyName]; }

        /// <summary>
        /// Add a text entrie in the ZIP archive.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, string content)
        {
            AddEntry(entryName, content, UTF8SansBomEncoding.Default);
        }
        /// <summary>
        /// Add a text entrie with encoding in the ZIP archive.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, string content, Encoding encoding)
        {
            AddEntry(entryName, encoding.GetBytes(content));
        }
        /// <summary>
        /// Add a entrie in the ZIP archive from <see cref="byte"/>[]
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="byteContent"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, byte[] byteContent)
        {
            AddEntry(entryName, new MemoryStream(byteContent));
        }
        /// <summary>
        /// Add a entrie in the ZIP archive from <see cref="byte"/>[]
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, Stream stream)
        {
            try
            {
                zipFile.AddEntry(ToEntryFormat(entryName), stream);
            }
            catch (Ionic.Zip.ZipException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Ionic.Zlib.ZlibException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Add a entrie in the ZIP archive from file.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void AddEntryFromFile(string entryName, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                AddEntry(entryName, file.Clone());
        }
        /// <summary>
        /// Add all files of the directory in the ZIP archive.
        /// </summary>
        /// <param name="directoryPath"></param>
        public void AddEntryFromDirectory(string directoryPath)
        {
            AddEntryFromDirectory(directoryPath, "*");
        }
        /// <summary>
        /// Add all matched file of the directory in the ZIP archive.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="searchPattern"></param>
        public void AddEntryFromDirectory(string directoryPath, string searchPattern)
        {
            directoryPath = Path.GetFullPath(directoryPath);
            foreach (var file in RecursiveSearch.EnumerateFiles(directoryPath, searchPattern))
                AddEntryFromFile(file.Substring(directoryPath.Length), file);
        }

        /// <summary>
        /// Remove the entrie from the ZIP archive.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool RemoveEntry(ZipEntry entry)
        {
            return RemoveEntry(entry.FileName);
        }
        /// <summary>
        /// Remove the entrie with the given filename from the ZIP archive.
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public bool RemoveEntry(string entryName)
        {
            int index = Entries.IndexOf(entryName);
            if (index >= 0)
            {
                try
                {
                    zipFile.RemoveEntry(entryName);
                }
                catch (Ionic.Zip.ZipException ex)
                {
                    throw ZipException.FromIonic(ex);
                }
                catch (Ionic.Zlib.ZlibException ex)
                {
                    throw ZipException.FromIonic(ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Remove all entrie from the ZIP archive.
        /// </summary>
        public void RemoveAllEntry()
        {
            foreach (var item in EntriesNames.ToArray())
                RemoveEntry(item);
        }

        /// <summary>
        /// Update a text entrie in the ZIP archive
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        public void UpdateEntry(string entryName, string content)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, content);
        }
        /// <summary>
        /// Update a text entrie with encoding in the ZIP archive
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public void UpdateEntry(string entryName, string content, Encoding encoding)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, content, encoding);
        }
        /// <summary>
        /// Update entrie in the ZIP archive from <see cref="byte"/>[]
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="byteContent"></param>
        public void UpdateEntry(string entryName, byte[] byteContent)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, byteContent);
        }
        /// <summary>
        /// Update a  entrie in the ZIP archive from a stream.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="stream"></param>
        public void UpdateEntry(string entryName, Stream stream)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, stream);
        }

        /// <summary>
        /// Update a entrie in the ZIP archive from a file.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="filePath"></param>
        public void UpdateEntryFromFile(string entryName, string filePath)
        {
            RemoveEntry(entryName);
            AddEntryFromFile(entryName, filePath);
        }
        /// <summary>
        /// Update and add all files of the directory in the ZIP archive.
        /// </summary>
        /// <param name="directoryPath"></param>
        public void UpdateEntryFromDirectory(string directoryPath)
        {
            UpdateEntryFromDirectory(directoryPath, "*");
        }
        /// <summary>
        /// Update and add all matched file of the directory in the ZIP archive.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="searchPattern"></param>
        public void UpdateEntryFromDirectory(string directoryPath, string searchPattern)
        {
            directoryPath = Path.GetFullPath(directoryPath);
            foreach (var file in RecursiveSearch.EnumerateFiles(directoryPath, searchPattern))
                UpdateEntryFromFile(file.Substring(directoryPath.Length), file);
        }

        /// <summary>
        /// Saves the ZIP archive to a file, specified by the Name property.
        /// </summary>
        public void Save()
        {
            try
            {
                zipFile.Save();
            }
            catch (Ionic.Zip.ZipException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Ionic.Zlib.ZlibException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  Save the ZIP archive to a file.
        /// </summary>
        /// <param name="fileName">The name of the zip archive to save to. Existing files will be overwritten with great prejudice.</param>
        public void Save(string fileName)
        {
            try
            {
                zipFile.Save(fileName);
            }
            catch (Ionic.Zip.ZipException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Ionic.Zlib.ZlibException ex)
            {
                throw ZipException.FromIonic(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Extract all file in the specified directory
        /// </summary>
        /// <param name="path"></param>
        public void ExtractAll(string path)
        {
            ExtractAll(path, false);
        }
        /// <summary>
        /// Extract and overwrite all file in the specified directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="overwrite"></param>
        public void ExtractAll(string directoryPath, bool overwrite)
        {
            directoryPath = Path.GetFullPath(directoryPath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            foreach (var item in Entries)
                item.Extract(Path.Combine(directoryPath, item.FileName), overwrite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.IEnumerator<ZipEntry> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            zipFile.Dispose();
        }
    }
}
