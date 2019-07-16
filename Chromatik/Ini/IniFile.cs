using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace System.Ini
{
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
                                                             string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                                                          string key, string def, StringBuilder retVal,
                                                          int size, string filePath);

        public IniFile(string ini)
        {
            IniPath = ini;
        }
        public string IniPath { get; set; }
        public string ReadValue(string section, string key)
        {
            return ReadValue(section, key, string.Empty);
        }
        public string ReadValue(string section, string key, string def)
        {
            SectionKey(ref section, ref key);
            var temp = new StringBuilder(short.MaxValue);
            GetPrivateProfileString(section, key, def, temp, temp.Capacity, IniPath);
            return temp.ToString().Trim();
        }
        public void WriteValue(string section, string key, string value)
        {
            SectionKey(ref section, ref key);
            if (string.IsNullOrWhiteSpace(value))
                value = string.Empty;
            value = value.Trim();
            WritePrivateProfileString(section, key, value, IniPath);
        }

        private void SectionKey(ref string section, ref string key)
        {
            if (string.IsNullOrWhiteSpace(section))
                section = string.Empty;
            section = section.Trim();
            if (string.IsNullOrWhiteSpace(key))
                key = string.Empty;
            key = key.Trim();
        }

        private class _IniFile
        {
            public _IniFile(string ini)
            {
                IniPath = ini;
            }

            public string IniPath { get; set; }

            public string[] ReadAll()
            {
                try
                {
                    if (File.Exists(IniPath))
                    {
                        string[] temp = File.ReadAllLines(IniPath);
                        foreach (var item in temp)
                            if (item.Contains("\0"))
                                return new string[0];

                        return temp;

                    }
                    else
                        return new string[0];
                }
                catch (Exception)
                {
                    return new string[0];
                }
            }

            public string[] ReadRegion(string setionName)
            {
                string[] all = ReadAll();
                if (string.IsNullOrWhiteSpace(setionName) || setionName.Contains("[") || setionName.Contains("]"))
                    setionName = string.Empty;
                setionName = setionName.Trim();

                List<string> rslt = new List<string>();

                int index = 0;
                bool foud = false;
                for (int i = 0; i < all.Length; index++)
                {
                    string line = all[index].Trim(' ');
                    if (line.StartsWith("[") && line.EndsWith("]"))
                        if (line.Substring(1, line.Length - 2).Trim(' ') == setionName)
                        {
                            foud = true;
                            break;
                        }
                }

                if (foud)
                {
                    index++;
                    string dd = all[index];
                    for (int i = index; i < all.Length; index++)
                        if (!string.IsNullOrWhiteSpace(all[index]))
                        {
                            string line = all[index].Trim(' ');
                            if (line.StartsWith("["))
                                break;
                            if (!line.StartsWith(";") && line.Contains("="))
                                rslt.Add(line);
                        }
                }

                return rslt.ToArray();
            }
            public string ReadValue(string regionName, string valueName)
            {
                return ReadValue(ReadRegion(regionName), valueName);
            }
            protected string ReadValue(string[] region, string valueName)
            {
                if (region == null || region.Length == 0)
                    return string.Empty;

                string rslt = string.Empty;

                if (string.IsNullOrWhiteSpace(valueName))
                    valueName = string.Empty;
                valueName = valueName.Trim();

                foreach (var item in region)
                {
                    int index = item.IndexOf("=");
                    string key = item.Substring(0, index).Trim(' ');
                    if (key == valueName)
                        rslt = item.Substring(index + 1).Trim(' ');
                }
                return rslt;
            }
        }

    }
}
