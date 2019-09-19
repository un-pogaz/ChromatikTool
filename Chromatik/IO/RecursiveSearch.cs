using System;
using System.Collections.Generic;
using System.Linq;

namespace System.IO
{
    /// <summary>
    /// Static class for a safely search in a folder
    /// </summary>
    sealed public class RecursiveSearch
    {
        /// <summary>
        /// Obtains safely all file in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        static public string[] GetFiles(string path) { return GetFiles(path, "*"); }
        /// <summary>
        /// Obtains safely file correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        static public string[] GetFiles(string path, string searchPattern) { return GetFiles(path, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Obtains safely file correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        /// <param name="searchOption"></param>
        static public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return EnumerateFiles(path, searchPattern, searchOption).ToArray();
        }

        /// <summary>
        /// Enumerable safely all files in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        static public IEnumerable<string> EnumerateFiles(string path) { return EnumerateFiles(path, "*"); }
        /// <summary>
        /// Enumerable safely files correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        static public IEnumerable<string> EnumerateFiles(string path, string searchPattern) { return EnumerateFiles(path, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Enumerable safely files correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        /// <param name="searchOption"></param>
        static public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*";

            path = Path.GetFullPath(path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));

            if (Directory.Exists(path))
            {
                foreach (string item in EnumerateTopFiles(path, searchPattern))
                    yield return item;

                if (searchOption == SearchOption.AllDirectories)
                    foreach (string dir in EnumerateTopDirectory(path))
                        foreach (string item in EnumerateFiles(dir, searchPattern, SearchOption.AllDirectories))
                            yield return item;
            }
        }

        /// <summary>
        /// Obtains safely all directory in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        static public string[] GetDirectorys(string path) { return GetDirectorys(path, "*"); }
        /// <summary>
        /// Obtains safely directory correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        static public string[] GetDirectorys(string path, string searchPattern)
        {
            return EnumerateDirectory(path, searchPattern).ToArray();
        }
        /// <summary>
        /// Obtains safely directory correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        /// <param name="searchOption"></param>
        static public string[] GetDirectorys(string path, string searchPattern, SearchOption searchOption)
        {
            return EnumerateDirectory(path, searchPattern, searchOption).ToArray();
        }

        /// <summary>
        /// Enumerable safely all directory in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        static public IEnumerable<string> EnumerateDirectory(string path) { return EnumerateDirectory(path, "*"); }
        /// <summary>
        /// Enumerable safely directory correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        static public IEnumerable<string> EnumerateDirectory(string path, string searchPattern) { return EnumerateDirectory(path, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Enumerable safely directory correspond to the research pattern in the target folder.
        /// </summary>
        /// <param name="path">Target folder</param>
        /// <param name="searchPattern">Search string to find among the file names in path. This parameter can contain a combination of literal and generic characters * and ? (see Remarks), but does not support regular expressions.</param>
        /// <param name="searchOption"></param>
        static public IEnumerable<string> EnumerateDirectory(string path, string searchPattern, SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*";

            path = Path.GetFullPath(path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));

            if (Directory.Exists(path))
            {
                foreach (var item in EnumerateTopDirectory(path, searchPattern))
                    yield return item;

                if (searchOption == SearchOption.AllDirectories)
                    foreach (var dir in EnumerateTopDirectory(path))
                        foreach (var item in EnumerateDirectory(dir, searchPattern, SearchOption.AllDirectories))
                            yield return item;
            }
        }


        static private IEnumerable<string> EnumerateTopFiles(string path) { return EnumerateTopFiles(path, "*"); }

        static private IEnumerable<string> EnumerateTopFiles(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*";

            path = Path.GetFullPath(path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));

            if (IsRECYCLE_BIN(path))
                yield break;

            IEnumerable<string> Ienum = null;
            try
            {
                Ienum = Directory.EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException) { yield break; }
            catch (Exception)
            {
                yield break;
            }

            if (Ienum != null)
                foreach (var item in Ienum)
                    yield return item;

            yield break;
        }


        static private IEnumerable<string> EnumerateTopDirectory(string path) { return EnumerateTopDirectory(path, "*"); }

        static private IEnumerable<string> EnumerateTopDirectory(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*";

            path = Path.GetFullPath(path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));

            if (IsRECYCLE_BIN(path))
                yield break;

            IEnumerable<string> Ienum = null;
            try
            {
                Ienum = Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException) { yield break; }
            catch (Exception)
            {
                yield break;
            }

            if (Ienum != null)
                foreach (var item in Ienum)
                    yield return item;

            yield break;
        }

        static private bool IsRECYCLE_BIN(string path)
        {
            if (Path.GetFullPath(path).ToUpper().Substring(3).StartsWith("$RECYCLE.BIN") ||
                Path.GetFullPath(path).ToLower().StartsWith("~/.local/share/trash/files"))
                return true;
            else
                return false;
        }
    }
}
