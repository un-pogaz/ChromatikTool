using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.IO
{
    static public class PathTool
    {
        /// <summary>
        /// Collection de caractères invalide pour les nom de fichier. Indépendant de la plateforme
        /// </summary>
        static public char[] InvalidFileNameChars { get; } = new char[] {
                '"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006',
                '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
                '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f',
                ':','*','?','\\','/'};

        /// <summary>
        /// Collection de caractères invalide pour les répertoire. Indépendant de la plateforme
        /// </summary>
        static public char[] InvalidPathChars { get; } = new char[] {
                '"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006',
                '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
                '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f'};

        /// <summary>
        /// Collection de nom invalide pour les répertoire et les fichier. Indépendant de la plateforme
        /// </summary>
        static public string[] InvalidNames { get; } = new string[] {
                "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9",
                "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9",
                "con", "nul", "prn"};

        
        /// <summary>
        /// Verifie que le nom de fichier est valide
        /// </summary>
        /// <param name="path">Nom du fichier a testé</param>
        /// <param name="exception">Ne léve aucune si false </param>
        /// <returns>True si le nom est valide</returns>
        /// <exception cref="InvalidFileNameException">Léve InvalidFileNameException si exception est a true</exception>
        static public bool IsValideFileName(string path, bool exception)
        {
            if (path.EndsWith(" ") || path.EndsWith("."))
                if (exception)
                    throw InvalidFileNameException.End;
                else
                    return false;
            
            foreach (char item in InvalidFileNameChars)
                if (path.Contains(item.ToString()))
                    if (exception)
                        throw InvalidFileNameException.Chars;
                    else
                        return false;

            foreach (string item in InvalidNames)
                if (Path.GetFileNameWithoutExtension(path).ToLowerInvariant() == item.ToLowerInvariant())
                    if (exception)
                        throw InvalidFileNameException.Name;
                    else
                        return false;

            return true;
        }
        
        /// <summary>
        /// Verifie que le chemin est valide
        /// </summary>
        /// <param name="path">Chemin a testé</param>
        /// <param name="exception">Ne léve aucune si false </param>
        /// <returns>True si le chemin est valide</returns>
        /// <exception cref="InvalidPathException">Léve InvalidPathException si exception est a true</exception>
        static public bool IsValidePath(string path, bool exception)
        {
            foreach (char item in InvalidPathChars)
                if (path.Contains(item.ToString()))
                    if (exception)
                        throw InvalidPathException.Chars;
                    else
                        return false;

            foreach (string subDir in path.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (subDir.EndsWith(" ") || subDir.EndsWith("."))
                {
                    if (exception)
                        throw InvalidPathException.End;
                    else
                        return false;
                }

                foreach (string item in InvalidNames)
                        if (subDir.ToLowerInvariant() == item.ToLowerInvariant())
                            if (exception)
                                throw InvalidPathException.Name;
                            else
                                return false;
            }

            return true;
        }

        static public string ChangeExtension(string filePath, string newExtension)
        {
            string[] split = filePath.Split(new char[] { '.' }, StringSplitOptions.None);
            if (newExtension == null)
                newExtension = string.Empty;
            newExtension = newExtension.Trim(InvalidFileNameChars.Concat(new char[] { '.', ' ' }));
            if (split.Length > 1)
            {
                int i = 1;
                if (split[0] == "")
                {
                    filePath = "." + split[1];
                    i = 2;
                }
                else
                {
                    filePath = split[0];
                    i = 1;
                }

                for (int ii = i; i < split.Length - 1; i++)
                    filePath += "." + split[i];
            }

            filePath = filePath.TrimEnd('.');
            if (!string.IsNullOrWhiteSpace(newExtension))
                return filePath + "." + newExtension;
            else
                return filePath;
        }
        /// <summary>
        /// To local Directory Separator Char
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public string ToLocal_DSC(string path)
        {
            return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }
        /// <summary>
        /// To linux Directory Separator Char
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public string ToLinux(string path)
        {
            return path.Replace("\\", "/").Trim();
        }

    }
}
