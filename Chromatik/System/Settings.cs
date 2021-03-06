﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace System
{
    /// <summary>
    /// Static class for various setings
    /// </summary>
    public static partial class Settings
    {
        static Settings()
        { }

        /// <summary>
        /// Path.DirectorySeparatorChar
        /// </summary>
        static private char DSC { get; } = Path.DirectorySeparatorChar;

        /// <summary>
        /// Name used for the temporary work folder; see <see cref="CreateTempWorkFolder()"/>
        /// </summary>
        /// <remarks>By default, is the file name of the application</remarks>
        static public string WorkFolderName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_workFolderName))
                    _workFolderName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
                return _workFolderName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Trim();
                    Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), value));
                    _workFolderName = value;
                }
            }
        }
        static string _workFolderName;

        /// <summary>
        /// Temporary folder specific to the application
        /// </summary>
        static public string TempFolder
        {
            get
            {
                string folder = Path.Combine(Path.GetTempPath(), WorkFolderName) + DSC;

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
                            catch
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
                            catch
                            {
                            }
                        }


                    TempFolderfirstAcces = false;
                }

                return folder;
            }
        }
        static private bool TempFolderfirstAcces = true;

        /// <summary>
        /// Create a new subfolder (aka work folder) in the temporary folder
        /// </summary>
        /// <returns>Path of the new folder</returns>
        /// <remarks><see cref="TempFolder"/>/<see cref="WorkFolderName"/>_HH-mm-ss (xxx)</remarks>
        static public string CreateTempWorkFolder()
        {
            string rslt;
            Random ran = new Random();
            do
            {
                rslt = TempFolder + WorkFolderName + "_" + DateTime.Now.ToString("HH-mm-ss") + " (" + ran.Next(0, 1000).ToString("D3") + ")" + Path.DirectorySeparatorChar;
            }
            while (Directory.Exists(rslt));

            Directory.CreateDirectory(rslt);

            return rslt;
        }


        /// <summary>
        /// Application folder
        /// </summary>
        static public string ApplicationFolder { get; } = Path.GetDirectoryName(Application.ExecutablePath) + DSC;

        /// <summary>
        /// Default ApplicationData (%AppData%) folder
        /// </summary>
        static public string ApplicationData_Default { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + DSC;

        /// <summary>
        /// ApplicationData (%AppData%) folder of the application
        /// </summary>
        static public string ApplicationData { get; } = Path.Combine(ApplicationData_Default, WorkFolderName) + DSC;

        /// <summary>
        /// Default ApplicationData (%AppData%) folder
        /// </summary>
        static public string LocalApplicationData_Default { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + DSC;
        /// <summary>
        /// LocalApplicationData folder of the application
        /// </summary>
        static public string LocalApplicationData { get; } = Path.Combine(LocalApplicationData_Default, WorkFolderName) + DSC;


        /// <summary>
        /// ProgramData folder
        /// </summary>
        static public string ProgramData_Default { get; } = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + DSC;
        /// <summary>
        /// ProgramData folder of the application
        /// </summary>
        static public string ProgramData { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), WorkFolderName) + DSC;


        /// <summary>
        /// Version info
        /// </summary>
        static public VersionClass Version { get; } = new VersionClass(Application.ProductVersion);


        /// <summary>
        /// Command line arguments of the application
        /// </summary>
        static public CommandLineArgs Args { get; } = new CommandLineArgs();

        [System.Diagnostics.DebuggerDisplay("{CommandLine}")]
        public class CommandLineArgs : Collections.ObjectModel.ReadOnlyCollection<string>
        {
            static private IList<string> Init()
            {
                string[] rslt = Environment.GetCommandLineArgs();
                if (rslt.Length > 0)
                    return rslt.SubArray(1);
                else
                    return new string[0];
            }

            public CommandLineArgs() : base(Init())
            {
                string cmd = Environment.CommandLine;
                CommandLine = cmd.Substring(cmd.IndexOf('"', 1)+1).Trim();
            }

            public string CommandLine { get; }

            new public bool Contains(string value)
            {
                return (IndexOf(value) >= 0);
            }
            public bool Contains(string value, bool caseSensitive)
            {
                return (IndexOf(value, caseSensitive) >= 0);
            }
            public bool Contains(string value, StringComparison comparisonType)
            {
                return (IndexOf(value, comparisonType) >= 0);
            }

            new public int IndexOf(string value)
            {
                return IndexOf(value, true);
            }
            public int IndexOf(string value, bool caseSensitive)
            {
                if (caseSensitive)
                    return IndexOf(value, StringComparison.InvariantCulture);
                else
                    return IndexOf(value, StringComparison.InvariantCultureIgnoreCase);
            }
            public int IndexOf(string value, StringComparison comparisonType)
            {
                for (int i = 0; i < Items.Count; i++)
                    if (Items[i].Equals(value, comparisonType))
                        return i;

                return -1;
            }

            public string GetNextArg(string value)
            {
                return GetNextArg(value, true);
            }
            public string GetNextArg(string value, bool caseSensitive)
            {
                if (caseSensitive)
                    return GetNextArg(value, StringComparison.InvariantCulture);
                else
                    return GetNextArg(value, StringComparison.InvariantCultureIgnoreCase);
            }
            public string GetNextArg(string value, StringComparison comparisonType)
            {
                int index = IndexOf(value, comparisonType);
                if (index >= 0 && index+1 < Count)
                    return Items[index+1];

                return null;
            }
        }
    }
}
