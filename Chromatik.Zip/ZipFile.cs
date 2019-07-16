using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Chromatik.Zip
{
    public partial class ZipFile : IEnumerable<ZipEntry>, IEnumerable, IDisposable
    {
        protected Ionic.Zip.ZipFile zipFile;

        /// <summary>
        /// Create a new empty <see cref="ZipFile"/>
        /// </summary>
        public ZipFile() : this(new Ionic.Zip.ZipFile(Encoding.UTF8))
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
        /// Load a <see cref="ZipFile"/> from a  <see cref="Stream"/>
        /// </summary>
        public ZipFile(Stream stream) : this(GetZipFile(stream))
        { }

        static private Ionic.Zip.ZipFile GetZipFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    return Ionic.Zip.ZipFile.Read(fileName);
                else
                    return Ionic.Zip.ZipFile.Read(fileName, Encoding.UTF8);
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
        /// Load a <see cref="ZipFile"/> from a file, or create a new if it does not exist
        /// </summary>
        public ZipFile(string path) : this(GetZipFile(path))
        { }

        private ZipFile(Ionic.Zip.ZipFile IonicZip)
        {
            IonicZip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
            IonicZip.ExtractExistingFile = Ionic.Zip.ExtractExistingFileAction.Throw;
            IonicZip.Strategy = Ionic.Zlib.CompressionStrategy.Default;
            IonicZip.ProvisionalAlternateEncoding = Encoding.UTF8;
            IonicZip.UseUnicodeAsNecessary = true;
            IonicZip.MaxOutputSegmentSize = 0;

            foreach (var item in IonicZip.Entries)
            {
                item.ProvisionalAlternateEncoding = Encoding.UTF8;
                item.UseUnicodeAsNecessary = true;
            }

            ///////////////////
            zipFile = IonicZip;
            ///////////////////

            Delete__MACOSX(this);
            DelDirectory();
        }

        private void DelDirectory()
        {
            try
            {
                List<string> lstDir = new List<string>();
                foreach (var item in zipFile.Entries)
                    if (item.IsDirectory)
                        lstDir.Add(item.FileName);

                foreach (var item in lstDir)
                    zipFile.RemoveEntry(item);
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
        /// The name of the ZipFile, on disk.
        /// </summary>
        public string ZipFileName {
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
        /// A comment attached to the zip archive.
        /// </summary>
        public string Comment {
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
        /// Obtains the number of elements contained in the collection
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

        protected virtual IList<ZipEntry> CreateEntries()
        {
            try
            {
                IList<ZipEntry> rslt = new List<ZipEntry>();
                foreach (var item in zipFile)
                    rslt.Add(new ZipEntry(item));

                return rslt;
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
        /// 
        /// </summary>
        public ZipEntryCollection Entries { get { return new ZipEntryCollection(CreateEntries()); } }

        public ZipEntry GetEntry(string entyName) { return Entries[entyName]; }

        protected virtual IList<ZipEntry> CreateEntriesSorted()
        {
            try
            {
                IDictionary<string, ZipEntry> rslt = new SortedList<string, ZipEntry>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var item in zipFile)
                {
                    ZipEntry entry = new ZipEntry(item);
                    rslt.Add(entry.FileName, entry);
                }
                return rslt.Values.ToList();
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
        /// 
        /// </summary>
        public ZipEntryCollection EntriesSorted { get { return new ZipEntryCollection(CreateEntriesSorted()); } }
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<string> EntriesNames
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (var item in Entries)
                    lst.Add(item.FileName);
                return lst;
            }
        }

        /// <summary>
        /// Adds a named entry into the zip archive, taking content for the entry from a
        /// string.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, string content)
        {
            AddEntry(entryName, "", Encoding.UTF8);
        }
        /// <summary>
        /// Adds a named entry into the zip archive, taking content for the entry from a
        /// string, and using the specified text encoding.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, string content, Encoding encoding)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms, encoding);
            sw.Write(content);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            AddEntry(entryName, ms);
        }
        /// <summary>
        /// Add an entry into the zip archive using the given filename and directory path
        /// within the archive, and the given content for the file. No file is created in
        /// the filesystem.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="byteContent"></param>
        /// <returns></returns>
        public void AddEntry(string entryName, byte[] byteContent)
        {
            AddEntry(entryName, new MemoryStream(byteContent));
        }
        /// <summary>
        /// Create an entry in the ZipFile using the given Stream as input. The entry will have the given filename.
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
        /// Create an entry in the ZipFile using the specified file.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void AddEntryFromFile(string entryName, string filePath)
        {
            AddEntry(entryName, new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        public void AddEntryFromDirectory(string directoryPath)
        {
            AddEntryFromDirectory(directoryPath, "*");
        }
        /// <summary>
        /// 
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
        /// Removes the ZipEntry from the zip archive.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool RemoveEntry(ZipEntry entry)
        {
            return RemoveEntry(entry.FileName);
        }
        /// <summary>
        /// Removes the ZipEntry with the given filename from the zip archive.
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
        /// Remove all entry from the zip archive.
        /// </summary>
        public void RemoveAllEntry()
        {
            foreach (var item in EntriesNames.ToArray())
                RemoveEntry(item);
        }

        public void UpdateEntry(string entryName, string content)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, content);
        }
        public void UpdateEntry(string entryName, string content, Encoding encoding)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, content, encoding);
        }
        public void UpdateEntry(string entryName, byte[] byteContent)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, byteContent);
        }
        public void UpdateEntry(string entryName, Stream stream)
        {
            RemoveEntry(entryName);
            AddEntry(entryName, stream);
        }

        public void UpdateEntryFromFile(string entryName, string filePath)
        {
            RemoveEntry(entryName);
            AddEntryFromFile(entryName, filePath);
        }
        public void UpdateEntryFromDirectory(string directoryPath) { UpdateEntryFromDirectory(directoryPath, "*"); }
        public void UpdateEntryFromDirectory(string directoryPath, string searchPattern)
        {
            directoryPath = Path.GetFullPath(directoryPath);
            foreach (var file in RecursiveSearch.EnumerateFiles(directoryPath, searchPattern))
                UpdateEntryFromFile(file.Substring(directoryPath.Length), file);
        }

        /// <summary>
        /// Saves the Zip archive to a file, specified by the Name property of the ZipFile.
        /// </summary>
        public void Save() { Save(ZipFileName); }
        /// <summary>
        ///  Save the file to a new zipfile, with the given name.
        /// </summary>
        /// <param name="fileName">The name of the zip archive to save to. Existing files will be overwritten with great prejudice.</param>
        public void Save(string fileName)
        {
            DelDirectory();
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

        public void ExtractAll(string path)
        {
            ExtractAll(path, false);
        }
        public void ExtractAll(string directoryPath, bool overwrite)
        {
            directoryPath = Path.GetFullPath(directoryPath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            foreach (var item in Entries)
                item.Extract(Path.Combine(directoryPath, item.FileName), overwrite);
        }




        public System.Collections.Generic.IEnumerator<ZipEntry> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        public void Dispose()
        {
            zipFile.Dispose();
        }
    }
}
