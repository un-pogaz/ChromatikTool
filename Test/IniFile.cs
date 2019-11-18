﻿using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace System.Ini
{
    public class IniFile
    {
        /// <summary>
        /// Copies a string into the specified section of an initialization file.
        /// </summary>
        /// <returns>
        /// If the function successfully copies the string to the initialization file, the return value is nonzero.
        /// If the function fails, or if it flushes the cached version of the most recently accessed initialization file, the return value is zero. 
        /// </returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// Retrieves a string from the specified section in an initialization file.
        /// </summary>
        /// <returns>
        /// The return value is the number of characters copied to the buffer, not including the terminating null character.
        /// </returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Retrieves all the keys and values for the specified section of an initialization file.
        /// </summary>
        /// <returns>
        /// The return value is the number of characters copied to the buffer, not including the terminating null character.
        /// </returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        private string GetASCII(string s)
        {
            return Encoding.ASCII.GetString(Encoding.GetBytes(s));
        }
        private string GetEncoded(string s)
        {
            return Encoding.GetString(Encoding.ASCII.GetBytes(s));
        }



        private const int SIZE = 255;
        private const string DATETIME_MASK = "yyyy/MM/dd HH:mm:ss";
        private const string DATE_MASK = "yyyy/MM/dd";
        
        public string FileName { get; }
        public Encoding Encoding { get; }

        public IniFile(string fileName) : this(fileName, Encoding.ASCII)
        { }

        public IniFile(string fileName, Encoding encoding)
        {
            Encoding = encoding;

            FileName = fileName;
            if (!Path.IsPathRooted(FileName))
            {
                string basePath = Directory.GetCurrentDirectory();
                FileName = Path.Combine(basePath, FileName);
            }
        }

        public void DeleteKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, FileName);
        }

        public void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, FileName);
        }

        public string ReadString(string section, string key)
        {
            var temp = new StringBuilder(SIZE);
            GetPrivateProfileString(section, key, null, temp, SIZE, FileName);
            return temp.ToString();
        }

        public bool ReadBoolean(string section, string key)
        {
            string value = ReadString(section, key);
            return value.ToUpper().Equals("TRUE");
        }

        public decimal ReadDecimal(string section, string key)
        {
            string value = ReadString(section, key);
            return decimal.Parse(value.Trim(), Globalization.CultureInfo.InvariantCulture);
        }

        public double ReadDouble(string section, string key)
        {
            string value = ReadString(section, key);
            return double.Parse(value.Trim(), Globalization.CultureInfo.InvariantCulture);
        }

        public float ReadFloat(string section, string key)
        {
            string value = ReadString(section, key);
            return float.Parse(value, Globalization.CultureInfo.InvariantCulture);
        }

        public int ReadInteger(string section, string key)
        {
            string value = ReadString(section, key);
            return Convert.ToInt32(value.Trim());
        }

        public DateTime ReadDateTime(string section, string key)
        {
            string value = ReadString(section, key);
            return DateTime.ParseExact(value, DATETIME_MASK, Globalization.CultureInfo.InvariantCulture);
        }

        public DateTime ReadDate(string section, string key)
        {
            string value = ReadString(section, key);
            return DateTime.ParseExact(value, DATE_MASK, Globalization.CultureInfo.InvariantCulture);
        }

        public bool WriteString(string section, string key, string value)
        {
            long l = WritePrivateProfileString(section, key, value, FileName);
            return l > 0;
        }
        
        public bool WriteBoolean(string section, string key, bool value)
        {
            string str = value.ToString().ToUpper();
            return WriteString(section, key, str);
        }

        public bool WriteDecimal(string section, string key, decimal value)
        {
            return WriteString(section, key, value.ToString(Globalization.CultureInfo.InvariantCulture));
        }

        public bool WriteDouble(string section, string key, double value)
        {
            return WriteString(section, key, value.ToString(Globalization.CultureInfo.InvariantCulture));
        }

        public bool WriteFloat(string section, string key, float value)
        {
            return WriteString(section, key, value.ToString(Globalization.CultureInfo.InvariantCulture));
        }

        public bool WriteInteger(string section, string key, int value)
        {
            return WriteString(section, key, value.ToString());
        }

        public bool WriteDateTime(string section, string key, DateTime value)
        {
            return WriteString(section, key, value.ToString(DATETIME_MASK));
        }

        public bool WriteDate(string section, string key, DateTime value)
        {
            return WriteString(section, key, value.ToString(DATE_MASK));
        }

        public bool SectionExists(string section)
        {
            int i = GetPrivateProfileString(section, null, null, new StringBuilder(SIZE), SIZE, FileName);
            return i > 0;
        }

        public bool KeyExists(string section, string key)
        {
            int i = GetPrivateProfileString(section, key, null, new StringBuilder(SIZE), SIZE, FileName);
            return i > 0;
        }

        public IDictionary<string, string> ReadSection(string section)
        {
            var buffer = new byte[2048];
            GetPrivateProfileSection(section, buffer, 2048, FileName);
            var tmp = Encoding.ASCII.GetString(buffer).Trim('\0').Split('\0');
            var result = new Dictionary<string, string>();

            foreach (var entry in tmp)
            {
                var s = entry.Split(new string[] { "=" }, 2, StringSplitOptions.None);
                result.Add(s[0], s[1]);
            }
            return result;
        }
    }
}
