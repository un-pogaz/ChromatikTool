using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// A writable <see cref="Stream"/> for <see cref="string"/> class
    /// </summary>
    public class StreamString : MemoryStream
    {
        /// <summary>
        /// The <see cref="Text.Encoding"/> used by the instance.
        /// </summary>
        public Encoding Encoding { get; }
        /// <summary>
        /// Gets or sets the line termination string used for the current instance.
        /// </summary>
        string NewLine {
            get { return _newLine; }
            set {
                if (value == null)
                    value = "\n";

                _newLine = value;
            }
        }
        string _newLine = "\n";

        /// <summary>
        /// Initializes a new instance with the specified text.
        /// </summary>
        /// <param name="text"></param>
        public StreamString(string text) : this(text, UTF8SansBomEncoding.UTF8SansBom)
        { }
        /// <summary>
        /// Initializes a new instance with the specified text and encoding.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        public StreamString(string text, Encoding encoding) : base(byte.MaxValue)
        {
            Encoding = encoding;
            foreach (var item in Encoding.GetPreamble())
                WriteByte(item);

            Write(text);
        }
        /// <summary>
        /// Writer a text to the actual position.
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            byte[] add = Encoding.GetBytes(text.Regex("\r\n|\r|\n", NewLine));
            if (Capacity < add.Length + (int)Position)
                Capacity = add.Length + (int)Position;

            foreach (var item in add)
                WriteByte(item);
        }

        /// <summary>
        /// Append a empty line at the end of the stream.
        /// </summary>
        public void AppendLine()
        {
            AppendText(null);
        }
        /// <summary>
        /// Append a text at the end of the stream.
        /// </summary>
        /// <param name="text"></param>
        public void AppendLine(string text)
        {
            if (text == null)
                text = string.Empty;

            AppendText(text + NewLine);
        }

        /// <summary>
        /// Append a text at the end of the stream.
        /// </summary>
        /// <param name="text"></param>
        public void AppendText(string text)
        {
            Position = Length - 1;
            Write(text);
        }


        /// <summary>
        /// Get the content of the instance into a string.
        /// </summary>
        public void GetString()
        {
            Encoding.GetString(ToArray());
        }
    }
}
