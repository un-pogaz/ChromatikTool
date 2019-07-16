using System;
using System.Collections.Generic;
using System.Linq;

namespace System.IO
{
    sealed public class RecursiveSearch
    {
        /// <summary>
        /// Obtient de maniére sûr la liste des fichiers du dossier cible ainsi que ses sous dossiers
        /// </summary>
        /// <param name="path">Chemin</param>
        static public string[] GetFiles(string path) { return GetFiles(path, "*"); }
        /// <summary>
        /// Obtient de maniére sûr la liste des fichiers correspondent à un modèle de recherche du dossier cible ainsi que ses sous dossiers
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public string[] GetFiles(string path, string searchPattern) { return GetFiles(path, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Obtient de maniére sûr la liste des fichiers correspondent à un modèle de recherche du dossier cible ainsi que ses sous dossiers
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        /// <param name="searchOption"></param>
        static public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return EnumerateFiles(path, searchPattern, searchOption).ToArray();
        }
        /// <summary>
        /// Obtient de maniére sûr la liste des fichiers des dossiers cibles ainsi que leurs sous dossiers
        /// </summary>
        /// <param name="paths">Dossiers cibles</param>
        static public string[] GetFiles(string[] paths) { return GetFiles(paths, "*"); }
        /// <summary>
        /// Obtient de maniére sûr la liste des fichiers correspondent à un modèle de recherche des dossiers cibles ainsi que leurs sous dossiers
        /// </summary>
        /// <param name="paths">Dossiers cibles</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public string[] GetFiles(string[] paths, string searchPattern) { return GetFiles(paths, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Obtient de maniére sûr la liste des fichiers correspondent à un modèle de recherche des dossiers cibles ainsi que leurs sous dossiers
        /// </summary>
        /// <param name="paths">Dossiers cibles</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        /// <param name="searchOption"></param>
        static public string[] GetFiles(string[] paths, string searchPattern, SearchOption searchOption)
        {
            return EnumerateFiles(paths, searchPattern, searchOption).ToArray();
        }

        /// <summary>
        /// Énumére de maniére sûr tous les fichiers du dossier cible ainsi que ses sous dossiers
        /// </summary>
        /// <param name="path">Chemin</param>
        static public IEnumerable<string> EnumerateFiles(string path) { return EnumerateFiles(path, "*"); }
        /// <summary>
        /// Énumére de maniére sûr les fichiers correspondent à un modèle de recherche du dossier cible
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public IEnumerable<string> EnumerateFiles(string path, string searchPattern) { return EnumerateFiles(path, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Énumére de maniére sûr les fichiers correspondent à un modèle de recherche du dossier cible
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
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
        /// Énumére de maniére sûr tous les fichiers des dossiers cibles ainsi que leurs sous dossiers
        /// </summary>
        /// <param name="paths">Dossiers cibles</param>
        static public IEnumerable<string> EnumerateFiles(string[] paths) { return EnumerateFiles(paths, "*"); }
        /// <summary>
        /// Énumére de maniére sûr les fichiers correspondent à un modèle de recherche des dossiers cibles ainsi que leurs sous dossiers
        /// </summary>
        /// <param name="paths">Dossiers cibles</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public IEnumerable<string> EnumerateFiles(string[] paths, string searchPattern) { return EnumerateFiles(paths, "*", SearchOption.AllDirectories); }
        /// <summary>
        /// Énumére de maniére sûr les fichiers correspondent à un modèle de recherche des dossiers cibles ainsi que leurs sous dossiers
        /// </summary>
        /// <param name="paths">Dossiers cibles</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public IEnumerable<string> EnumerateFiles(string[] paths, string searchPattern, SearchOption searchOption)
        {
            foreach (string path in paths)
                foreach (var item in EnumerateFiles(path, searchPattern, searchOption))
                    yield return item;
        }

        /// <summary>
        /// Obtient de maniére sûr la liste des sous dossiers du dossier cible 
        /// </summary>
        /// <param name="path">Dossier cible</param>
        static public string[] GetDirectorys(string path) { return GetDirectorys(path, "*"); }
        /// <summary>
        /// Obtient de maniére sûr la liste des sous dossiers correspondent à un modèle de recherche du dossier cible 
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public string[] GetDirectorys(string path, string searchPattern)
        {
            return EnumerateDirectory(path, searchPattern).ToArray();
        }

        /// <summary>
        /// Énumére de maniére sûr les sous dossiers du dossier cible 
        /// </summary>
        /// <param name="path">Dossier cible</param>
        static public IEnumerable<string> EnumerateDirectory(string path) { return EnumerateDirectory(path, "*"); }
        /// <summary>
        /// Énumére de maniére sûr les sous dossiers correspondent à un modèle de recherche du dossier cible 
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
        static public IEnumerable<string> EnumerateDirectory(string path, string searchPattern) { return EnumerateDirectory(path, searchPattern, SearchOption.AllDirectories); }
        /// <summary>
        /// Énumére de maniére sûr les sous dossiers correspondent à un modèle de recherche du dossier cible 
        /// </summary>
        /// <param name="path">Dossier cible</param>
        /// <param name="searchPattern">Modèle de recherche (non Regex)</param>
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

        /// <summary>
        /// Énumére de maniére sûr les fichiers du dossier cible 
        /// </summary>
        static private IEnumerable<string> EnumerateTopFiles(string path) { return EnumerateTopFiles(path, "*"); }
        /// <summary>
        /// Énumére de maniére sûr les fichiers correspondent à un modèle de recherche du dossier cible
        /// </summary>
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
        /// <summary>
        /// Énumére de maniére sûr les dossiers du dossier cible 
        /// </summary>
        static private IEnumerable<string> EnumerateTopDirectory(string path) { return EnumerateTopDirectory(path, "*"); }
        /// <summary>
        /// Énumére de maniére sûr les dossiers correspondent à un modèle de recherche du dossier cible 
        /// </summary>
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
                Path.GetFullPath(path).ToLower().StartsWith("~/.local/share/trash/files/"))
                return true;
            else
                return false;
        }
    }
}
