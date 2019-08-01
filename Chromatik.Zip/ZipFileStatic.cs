using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Chromatik.Zip
{
    public partial class ZipFile
    {
        /// <summary>
        /// Parse the string to valide entry name for ZIP archive.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static public string ToEntryFormat(string text)
        {
            text = text.Regex("(\\r\\n|\\r|\\n)", " ");
            text = text.Regex("(\\||\"|\\*|<|>|\\?)", "");
            text = text.Regex("(\u0000|\u0001|\u0002|\u0003|\u0004|\u0005|\u0006|\u0007|\u0008|\u0009|\u000a|\u000b|\u000c|\u000d|\u000e|\u000f)", "");
            text = text.Regex("(\u0010|\u0011|\u0012|\u0013|\u0014|\u0015|\u0016|\u0017|\u0018|\u0019|\u001a|\u001b|\u001c|\u001d|\u001e|\u001f)", "");
            text = text.Regex("(\u0080|\u0081|\u0082|\u0083|\u0084|\u0085|\u0086|\u0087|\u0088|\u0089|\u008a|\u008b|\u008c|\u008d|\u008e|\u008f)", "");
            text = text.Regex("(\u0090|\u0091|\u0092|\u0093|\u0094|\u0095|\u0096|\u0097|\u0098|\u0099|\u009a|\u009b|\u009c|\u009d|\u009e|\u009f)", "");

            string[] split = text.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 0 && split[0].Contains(":"))
                split[0] = string.Empty;

            for (int i = 0; i < split.Length; i++)
                split[i] = split[i].TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ', '.' }).Regex(":", "");

            return Path.Combine(split).Replace("\\", "/");
        }

        /// <summary>
        /// The MIME type for ZIP archive file.
        /// </summary>
        public const string Mimetype = "application/zip";

        /// <summary>
        /// Delete the "__MACOSX" folder from the ZIP archive.
        /// </summary>
        /// <param name="zipPath">ZIP target</param>
        /// <returns>Return true if the ZIP archive a was modified</returns>
        static public bool Delete__MACOSX(string zipPath)
        {
            using (ZipFile zip = new ZipFile(zipPath))
                if (Delete__MACOSX(zip))
                {
                    zip.Save();
                    return true;
                }
                else
                    return false;
        }

        /// <summary>
        /// Delete the "__MACOSX" folder from the ZIP archive.
        /// </summary>
        /// <param name="zip">ZIP target</param>
        /// <returns>Return true if the ZIP archive a was modified</returns>
        static public bool Delete__MACOSX(ZipFile zip)
        {
            List<ZipEntry> lst = new List<ZipEntry>();
            foreach (var item in zip.Entries)
                if (item.FileName.Trim().ToUpper().StartsWith("__MACOSX"))
                    lst.Add(item);
            if (lst.Count > 0)
                foreach (var item in lst)
                    zip.RemoveEntry(item);

            return (lst.Count > 0);
        }

        /// <summary>
        /// Read and load a ZIP from file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public ZipFile Read(string fileName)
        {
            try
            {
                return new ZipFile(Ionic.Zip.ZipFile.Read(fileName));
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
        /// Read and load a ZIP from stream.
        /// </summary>
        /// <param name="zipStream"></param>
        /// <returns></returns>
        static public ZipFile Read(Stream zipStream)
        {
            try
            {
                return new ZipFile(Ionic.Zip.ZipFile.Read(zipStream));
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
        /// Read and load a ZIP from <see cref="byte"/>[].
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        static public ZipFile Read(byte[] buffer)
        {
            try
            {
                return new ZipFile(Ionic.Zip.ZipFile.Read(buffer));
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
        /// Checks the ZIP file to see if its directory is consistent.
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <returns></returns>
        public static bool CheckZip(string zipFileName)
        {
            try
            {
                return Ionic.Zip.ZipFile.CheckZip(zipFileName);
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
        /// Rewrite the directory within a ZIP file.
        /// </summary>
        /// <param name="zipFileName"></param>
        public static void FixZipDirectory(string zipFileName)
        {
            try
            {
                Ionic.Zip.ZipFile.FixZipDirectory(zipFileName);
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
        /// Checks a file to see if it is a valid ZIP file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsZipFile(string fileName)
        {
            try
            {
                return Ionic.Zip.ZipFile.IsZipFile(fileName, true);
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
        /// Checks a stream to see if it contains a valid ZIP archive.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="testExtract"></param>
        /// <returns></returns>
        public static bool IsZipFile(Stream stream, bool testExtract)
        {
            try
            {
                return Ionic.Zip.ZipFile.IsZipFile(stream, testExtract);
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
}
