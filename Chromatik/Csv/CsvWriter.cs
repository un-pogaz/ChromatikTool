﻿using System;
using System.Collections.Generic;
using System.IO;

namespace System.Csv
{
    /// <summary>
    /// Helper class to write csv (comma separated values) data.
    /// </summary>
    public static class CsvWriter
    {
        /// <summary>
        /// Writes the lines to the writer.
        /// </summary>
        /// <param name="writer">The text writer to write the data to.</param>
        /// <param name="headers">The headers that should be used for the first line, determines the number of columns.</param>
        /// <param name="lines">The lines with data that should be written.</param>
        /// <param name="separator">The separator to use between columns (comma, semicolon, tab, ...)</param>
        /// <param name="alwaysQuotes">Forces the usesage of the double Quotes to delimit the values.</param>
        public static void Write(TextWriter writer, string[] headers, IEnumerable<string[]> lines, char separator = ',', bool alwaysQuotes = false)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            var columnCount = headers.Length;
            WriteLine(writer, headers, columnCount, separator, alwaysQuotes);
            foreach (var line in lines)
                WriteLine(writer, line, columnCount, separator, alwaysQuotes);
        }

        /// <summary>
        /// Writes the lines and return the result.
        /// </summary>
        /// <param name="headers">The headers that should be used for the first line, determines the number of columns.</param>
        /// <param name="lines">The lines with data that should be written.</param>
        /// <param name="separator">The separator to use between columns (comma, semicolon, tab, ...)</param>
        /// <param name="alwaysQuotes">Forces the usesage of the double Quotes to delimit the values.</param>
        public static string WriteToText(string[] headers, IEnumerable<string[]> lines, char separator = ',', bool alwaysQuotes = false)
        {
            using (var writer = new StringWriter())
            {
                Write(writer, headers, lines, separator, alwaysQuotes);

                return writer.ToString();
            }
        }

        /// <summary>
        /// Writes the lines and return the specified file.
        /// </summary>
        /// <param name="path">The headers that should be used for the first line, determines the number of columns.</param>
        /// <param name="headers">The headers that should be used for the first line, determines the number of columns.</param>
        /// <param name="lines">The lines with data that should be written.</param>
        /// <param name="separator">The separator to use between columns (comma, semicolon, tab, ...)</param>
        /// <param name="alwaysQuotes">Forces the usesage of the double Quotes to delimit the values.</param>
        public static void WriteToFile(string path, string[] headers, IEnumerable<string[]> lines, char separator = ',', bool alwaysQuotes = false)
        {
            using (var writer = new StreamWriter(path, false, Text.Encoding.UTF8))
            {
                Write(writer, headers, lines, separator, alwaysQuotes);
            }
        }

        private static void WriteLine(TextWriter writer, string[] data, int columnCount, char separator, bool alwaysQuotes)
        {
            var escapeChars = new[] { separator, '\'', '\n' };
            for (var i = 0; i < columnCount; i++)
            {
                if (i > 0)
                    writer.Write(separator);

                if (i < data.Length)
                {
                    var escape = alwaysQuotes;
                    var cell = data[i];

                    if (cell.Contains("\""))
                    {
                        escape = true;
                        cell = cell.Replace("\"", "\"\"");
                    }
                    else if (cell.IndexOfAny(escapeChars) >= 0)
                        escape = true;

                    if (escape)
                        writer.Write('"');
                    writer.Write(cell);
                    if (escape)
                        writer.Write('"');
                }
            }
            writer.WriteLine();
        }
    }
}