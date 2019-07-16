using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace System.Ini
{
    /// <summary>
    /// Class for read and modify a INI file
    /// </summary>
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
                                                             string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                                                          string key, string def, StringBuilder retVal,
                                                          int size, string filePath);

        /// <summary>
        /// Create a instance associated with a INI file 
        /// </summary>
        /// <param name="ini"></param>
        public IniFile(string ini)
        {
            IniPath = ini;
        }
        /// <summary> 
        /// Path of the INI file associated
        /// </summary>
        public string IniPath { get; set; }
        /// <summary>
        /// Read the value in the section of the INI file
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadValue(string section, string key)
        {
            return ReadValue(section, key, string.Empty);
        }
        /// <summary>
        /// Read the value in the section of the INI file
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def">Default value if the value is empty</param>
        /// <returns></returns>
        public string ReadValue(string section, string key, string def)
        {
            SectionKey(ref section, ref key);
            if (def == null)
                def = string.Empty;
            var temp = new StringBuilder(short.MaxValue);
            GetPrivateProfileString(section, key, def, temp, temp.Capacity, IniPath);
            return temp.ToString().Trim();
        }
        /// <summary>
        /// Write a value in the section of the INI file
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteValue(string section, string key, string value)
        {
            SectionKey(ref section, ref key);
            if (string.IsNullOrWhiteSpace(value))
                value = string.Empty;
            value = value.Trim();
            WritePrivateProfileString(section, key, value, IniPath);
        }

        static private void SectionKey(ref string section, ref string key)
        {
            if (string.IsNullOrWhiteSpace(section))
                section = string.Empty;
            section = section.Trim();
            if (string.IsNullOrWhiteSpace(key))
                key = string.Empty;
            key = key.Trim();
        }
    }
}
