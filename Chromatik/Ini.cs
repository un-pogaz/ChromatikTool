using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace System.Configuration
{
    /// <summary>
    /// Represent a INI file on the system and provide the methods to interact with it.
    /// </summary>
    public class Ini
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
        
        private const int SIZE = 255;
        private const string DATETIME_MASK = "yyyy/MM/dd HH:mm:ss";
        private const string DATE_MASK = "yyyy/MM/dd";

        /// <summary>
        /// Path of the file read associated on this <see cref="Ini"/>
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Create a <see cref="Ini"/> associated with a file on the system.
        /// </summary>
        /// <param name="fileName"></param>
        public Ini(string fileName)
        {
            FileName = fileName;
            if (!Path.IsPathRooted(FileName))
            {
                string basePath = Directory.GetCurrentDirectory();
                FileName = Path.Combine(basePath, FileName);
            }
        }
        /// <summary></summary>
        public override string ToString()
        {
            return Path.GetFileName(FileName) +" | INI \""+ FileName+ "\""; 
        }

        /// <summary>
        /// Delete a section in the INI file
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, FileName);
        }
        /// <summary>
        /// Delete a key in the INI file
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public void DeleteKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, FileName);
        }

        /// <summary>
        /// Read a <see cref="string"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadString(string section, string key)
        {
            var temp = new StringBuilder(SIZE);
            GetPrivateProfileString(section, key, null, temp, SIZE, FileName);
            return temp.ToString();
        }
        /// <summary>
        /// Read a <see cref="bool"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ReadBoolean(string section, string key)
        {
            string value = ReadString(section, key);
            return value.ToUpper().Equals("TRUE");
        }
        /// <summary>
        /// Read a <see cref="decimal"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal ReadDecimal(string section, string key)
        {
            string value = ReadString(section, key);
            return decimal.Parse(value.Trim(), Globalization.CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Read a <see cref="double"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public double ReadDouble(string section, string key)
        {
            string value = ReadString(section, key);
            return double.Parse(value.Trim(), Globalization.CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Read a <see cref="float"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public float ReadFloat(string section, string key)
        {
            string value = ReadString(section, key);
            return float.Parse(value, Globalization.CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Read a <see cref="int"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int ReadInteger(string section, string key)
        {
            string value = ReadString(section, key);
            return Convert.ToInt32(value.Trim());
        }
        /// <summary>
        /// Read a <see cref="DateTime"/> value (yyyy/MM/dd HH:mm:ss)
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DateTime ReadDateTime(string section, string key)
        {
            string value = ReadString(section, key);
            return DateTime.ParseExact(value, DATETIME_MASK, Globalization.CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Read a Date value (yyyy/MM/dd)
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DateTime ReadDate(string section, string key)
        {
            string value = ReadString(section, key);
            return DateTime.ParseExact(value, DATE_MASK, Globalization.CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Write a <see cref="string"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteString(string section, string key, string value)
        {
            long l = WritePrivateProfileString(section, key, value, FileName);
            return l > 0;
        }
        /// <summary>
        /// Write a <see cref="bool"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteBoolean(string section, string key, bool value)
        {
            string str = value.ToString().ToUpper();
            return WriteString(section, key, str);
        }
        /// <summary>
        /// Write a <see cref="decimal"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteDecimal(string section, string key, decimal value)
        {
            return WriteString(section, key, value.ToString(Globalization.CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Write a <see cref="double"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteDouble(string section, string key, double value)
        {
            return WriteString(section, key, value.ToString(Globalization.CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Write a <see cref="float"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteFloat(string section, string key, float value)
        {
            return WriteString(section, key, value.ToString(Globalization.CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Write a <see cref="int"/> value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteInteger(string section, string key, int value)
        {
            return WriteString(section, key, value.ToString());
        }
        /// <summary>
        /// Write a <see cref="DateTime"/> value (yyyy/MM/dd HH:mm:ss)
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteDateTime(string section, string key, DateTime value)
        {
            return WriteString(section, key, value.ToString(DATETIME_MASK));
        }
        /// <summary>
        /// Write a Date value (yyyy/MM/dd)
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteDate(string section, string key, DateTime value)
        {
            return WriteString(section, key, value.ToString(DATE_MASK));
        }

        /// <summary>
        /// Test if the section exist
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool SectionExists(string section)
        {
            int i = GetPrivateProfileString(section, null, null, new StringBuilder(SIZE), SIZE, FileName);
            return i > 0;
        }
        /// <summary>
        /// Test if the key exist
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string section, string key)
        {
            int i = GetPrivateProfileString(section, key, null, new StringBuilder(SIZE), SIZE, FileName);
            return i > 0;
        }

        /// <summary>
        /// Read all values of the section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
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
