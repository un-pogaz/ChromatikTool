using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace System
{
    public static class Settings
    {
        static Settings()
        {

        }


        /// <summary>
        /// Path.DirectorySeparatorChar
        /// </summary>
        static private char DSC { get; } = Path.DirectorySeparatorChar;

        static public string[] Args
        {
            get
            {
                string[] rslt = Environment.GetCommandLineArgs();
                if (rslt.Length > 0)
                    return rslt.SubArray(1);
                else
                    return new string[0];
            }
        }

        static public string TempFolderName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_tempFolderName))
                    _tempFolderName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
                return _tempFolderName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Trim();
                    try
                    {
                        Path.Combine(Path.GetTempPath(), value);
                        _tempFolderName = value;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        static string _tempFolderName;


        static public string TempFolder
        {
            get
            {
                string folder = Path.Combine(Path.GetTempPath(), TempFolderName) + DSC;

                Directory.CreateDirectory(folder);

                DirectoryInfo dir = new DirectoryInfo(folder);

                if (TempFolderfirstAcces)
                {
                    DateTime dt = DateTime.Now.AddDays(-1);

                    foreach (var item in dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
                        if (item.LastAccessTime < dt)
                        {
                            try
                            {
                                item.Delete(true);
                            }
                            catch (Exception)
                            {
                            }
                        }

                    foreach (var item in dir.EnumerateFiles("*", SearchOption.TopDirectoryOnly))
                        if (item.LastAccessTime < dt)
                        {
                            try
                            {
                                item.Delete();
                            }
                            catch (Exception)
                            {
                            }
                        }


                    TempFolderfirstAcces = false;
                }

                return folder;
            }
        }
        static private bool TempFolderfirstAcces = true;

        static public string CreateNewTempWorkFolder()
        {
            string rslt;

            Random ran = new Random();

            do
            {
                rslt = TempFolder + TempFolderName + "_" + DateTime.Now.ToString("HH-mm-ss") + " (" + ran.Next(0, 1000).ToString("D3") + ")" + Path.DirectorySeparatorChar;
            }
            while (Directory.Exists(rslt));

            Directory.CreateDirectory(rslt);

            return rslt;
        }

        static public string ApplicationPath
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath) + DSC; }
        }
    }
}
