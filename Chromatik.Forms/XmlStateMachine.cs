using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

/// <summary>
/// https://archive.codeplex.com/?p=xmlrichtextbox
/// {
/// 	"Name": "The MIT License (MIT)",
/// 	"Text": "Copyright (c) 2015 Jeff Jones\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.",
/// 	"Id": 605595,
/// 	"ShortName": "MIT",
/// 	"StartDate": "\/Date(1450126856507-0800)\/"
/// }
/// </summary>

namespace Chromatik.Xml
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlTokenColor
    {
        public Color NodeValue { get; set; } = Color.Black;
        public Color EqualSignStart { get; set; } = Color.Black;
        public Color EqualSignEnd { get; set; } = Color.Black;
        public Color DoubleQuotationMarkStart { get; set; } = Color.Black;
        public Color DoubleQuotationMarkEnd { get; set; } = Color.Black;
        public Color SingleQuotationMarkStart { get; set; } = Color.Black;
        public Color SingleQuotationMarkEnd { get; set; } = Color.Black;

        public Color XmlDeclarationStart { get; set; } = Color.Blue;
        public Color XmlDeclarationEnd { get; set; } = Color.Blue;
        public Color NodeStart { get; set; } = Color.Blue;
        public Color NodeEnd { get; set; } = Color.Blue;
        public Color NodeEndValueStart { get; set; } = Color.Blue;
        public Color CDataStart { get; set; } = Color.Blue;
        public Color CDataEnd { get; set; } = Color.Blue;
        public Color CommentStart { get; set; } = Color.Blue;
        public Color CommentEnd { get; set; } = Color.Blue;
        public Color AttributeValue { get; set; } = Color.Blue;
        public Color DocTypeStart { get; set; } = Color.Blue;
        public Color DocTypeEnd { get; set; } = Color.Blue;
        public Color DocTypeDefStart { get; set; } = Color.Blue;
        public Color DocTypeDefEnd { get; set; } = Color.Blue;

        public Color CDataValue { get; set; } = Color.Gray;
        public Color DocTypeDefValue { get; set; } = Color.Gray;

        public Color CommentValue { get; set; } = Color.Green;

        public Color DocTypeName { get; set; } = Color.Brown;
        public Color NodeName { get; set; } = Color.Brown;

        public Color AttributeName { get; set; } = Color.Red;
        public Color DocTypeDeclaration { get; set; } = Color.Red;

        public Color Unknown { get; set; } = Color.Orange;
        public Color Default { get; set; } = Color.Orange;
    }

    public enum XmlTokenType
    {
        Whitespace,
        XmlDeclarationStart,
        XmlDeclarationEnd,
        NodeStart,
        NodeEnd,
        NodeEndValueStart,
        NodeName,
        NodeValue, 
        AttributeName,
        AttributeValue,
        EqualSignStart,
        EqualSignEnd,
        CommentStart,
        CommentValue,
        CommentEnd,
        CDataStart, 
        CDataValue,
        CDataEnd,
        DoubleQuotationMarkStart,
        DoubleQuotationMarkEnd,
        SingleQuotationMarkStart,
        SingleQuotationMarkEnd,
        DocTypeStart,
        DocTypeName,
        DocTypeDeclaration,
        DocTypeDefStart,
        DocTypeDefValue,
        DocTypeDefEnd,
        DocTypeEnd, 
        DocumentEnd,
        Unknown
    }

    public class XmlStateMachine
    {
        #region Constructor
        public XmlStateMachine() { }
        public XmlStateMachine(XmlTokenColor XmlTokenColor)
        {
            this.XmlTokenColor = XmlTokenColor;
        }

        #endregion Constructor

        #region Public Fields
        public XmlTokenType CurrentState = XmlTokenType.Unknown;
        public XmlTokenColor XmlTokenColor { get; set; } = new XmlTokenColor();
        #endregion Public Fields

        #region Private Fields
        private string subString = "";
        private string token = string.Empty;
        private Stack<XmlTokenType> PreviousStates = new Stack<XmlTokenType>();
        #endregion Private Fields

        #region Public Methods
        public string GetNextToken(string s, int location, out XmlTokenType ttype)
        {
            ttype = XmlTokenType.Unknown;

            // skip past any whitespace (token added to it at the end of method)
            string whitespace = GetWhitespace(s, location);
            if (!String.IsNullOrEmpty(whitespace))
            {
                location += whitespace.Length;
            }

            subString = s.Substring(location, s.Length - location);
            token = string.Empty;

            if (CurrentState == XmlTokenType.CDataStart)
            {
                // check for empty CDATA
                if (subString.StartsWith("]]>"))
                {
                    CurrentState = XmlTokenType.CDataEnd;
                    token = "]]>";
                }
                else
                {
                    CurrentState = XmlTokenType.CDataValue;
                    int n = subString.IndexOf("]]>");
                    token = subString.Substring(0, n);
                }
            }
            else if (CurrentState == XmlTokenType.DocTypeStart)
            {
                CurrentState = XmlTokenType.DocTypeName;
                token = "DOCTYPE";
            }
            else if (CurrentState == XmlTokenType.DocTypeName)
            {
                CurrentState = XmlTokenType.DocTypeDeclaration;
                int n = subString.IndexOf("[");
                token = subString.Substring(0, n);
            }
            else if (CurrentState == XmlTokenType.DocTypeDeclaration)
            {
                CurrentState = XmlTokenType.DocTypeDefStart;
                token = "[";
            }
            else if (CurrentState == XmlTokenType.DocTypeDefStart)
            {
                if (subString.StartsWith("]>"))
                {
                    CurrentState = XmlTokenType.DocTypeDefEnd;
                    token = "]>";
                }
                else
                {
                    CurrentState = XmlTokenType.DocTypeDefValue;
                    int n = subString.IndexOf("]>");
                    token = subString.Substring(0, n);
                }
            }
            else if (CurrentState == XmlTokenType.DocTypeDefValue)
            {
                CurrentState = XmlTokenType.DocTypeDefEnd;
                token = "]>";
            }
            else if (CurrentState == XmlTokenType.DoubleQuotationMarkStart)
            {
                // check for empty attribute value
                if (subString[0] == '\"')
                {
                    CurrentState = XmlTokenType.DoubleQuotationMarkEnd;
                    token = "\"";
                }
                else
                {
                    CurrentState = XmlTokenType.AttributeValue;
                    int n = subString.IndexOf("\"");
                    token = subString.Substring(0, n);
                }
            }
            else if (CurrentState == XmlTokenType.SingleQuotationMarkStart)
            {
                // check for empty attribute value
                if (subString[0] == '\'')
                {
                    CurrentState = XmlTokenType.SingleQuotationMarkEnd;
                    token = "\'";
                }
                else
                {
                    CurrentState = XmlTokenType.AttributeValue;
                    int n = subString.IndexOf("'");
                    token = subString.Substring(0, n);
                }
            }
            else if (CurrentState == XmlTokenType.CommentStart)
            {
                // check for empty comment
                if (subString.StartsWith("-->"))
                {
                    CurrentState = XmlTokenType.CommentEnd;
                    token = "-->";
                }
                else
                {
                    CurrentState = XmlTokenType.CommentValue;
                    token = ReadCommentValue(subString, location);
                }
            }
            else if (CurrentState == XmlTokenType.NodeStart)
            {
                CurrentState = XmlTokenType.NodeName;
                token = ReadNodeName(subString, location);
            }
            else if (CurrentState == XmlTokenType.XmlDeclarationStart)
            {
                CurrentState = XmlTokenType.NodeName;
                token = ReadNodeName(subString, location);
            }
            else if (CurrentState == XmlTokenType.NodeName)
            {
                if (subString[0] != '/' &&
                    subString[0] != '>')
                {
                    CurrentState = XmlTokenType.AttributeName;
                    token = ReadAttributeName(subString, location);
                }
                else
                {
                    HandleReservedXmlToken();
                }
            }
            else if (CurrentState == XmlTokenType.NodeEndValueStart)
            {
                if (subString[0] == '<')
                {
                    HandleReservedXmlToken();
                }
                else
                {
                    CurrentState = XmlTokenType.NodeValue;
                    token = ReadNodeValue(subString, location);
                }
            }
            else if (CurrentState == XmlTokenType.DoubleQuotationMarkEnd)
            {
                HandleAttributeEnd(location);
            }
            else if (CurrentState == XmlTokenType.SingleQuotationMarkEnd)
            {
                HandleAttributeEnd(location);
            }
            else
            {
                HandleReservedXmlToken();
            }

            if (token != string.Empty)
            {
                ttype = CurrentState;
                return whitespace + token;
            }

            return string.Empty;

        }

        public Color GetTokenColor(XmlTokenType ttype)
        {
            return GetTokenColor(ttype, XmlTokenColor);
        }
        public Color GetTokenColor(XmlTokenType ttype, XmlTokenColor XmlTokenColor)
        {
            switch (ttype)
            {
                case XmlTokenType.NodeValue:
                    return XmlTokenColor.NodeValue;

                case XmlTokenType.EqualSignStart:
                    return XmlTokenColor.EqualSignStart;

                case XmlTokenType.EqualSignEnd:
                    return XmlTokenColor.EqualSignEnd;

                case XmlTokenType.DoubleQuotationMarkStart:
                    return XmlTokenColor.DoubleQuotationMarkStart;

                case XmlTokenType.DoubleQuotationMarkEnd:
                    return XmlTokenColor.DoubleQuotationMarkEnd;

                case XmlTokenType.SingleQuotationMarkStart:
                    return XmlTokenColor.SingleQuotationMarkStart;

                case XmlTokenType.SingleQuotationMarkEnd:
                    return XmlTokenColor.SingleQuotationMarkEnd;

                case XmlTokenType.XmlDeclarationStart:
                    return XmlTokenColor.XmlDeclarationStart;

                case XmlTokenType.XmlDeclarationEnd:
                    return XmlTokenColor.XmlDeclarationEnd;

                case XmlTokenType.NodeStart:
                    return XmlTokenColor.NodeStart;

                case XmlTokenType.NodeEnd:
                    return XmlTokenColor.NodeEnd;

                case XmlTokenType.NodeEndValueStart:
                    return XmlTokenColor.NodeEndValueStart;

                case XmlTokenType.CDataStart:
                    return XmlTokenColor.CDataStart;

                case XmlTokenType.CDataEnd:
                    return XmlTokenColor.CDataEnd;

                case XmlTokenType.CommentStart:
                    return XmlTokenColor.CommentStart;

                case XmlTokenType.CommentEnd:
                    return XmlTokenColor.CommentEnd;

                case XmlTokenType.AttributeValue:
                    return XmlTokenColor.AttributeValue;

                case XmlTokenType.DocTypeStart:
                    return XmlTokenColor.DocTypeStart;

                case XmlTokenType.DocTypeEnd:
                    return XmlTokenColor.DocTypeEnd;

                case XmlTokenType.DocTypeDefStart:
                    return XmlTokenColor.DocTypeDefStart;

                case XmlTokenType.DocTypeDefEnd:
                    return XmlTokenColor.DocTypeDefEnd;

                case XmlTokenType.CDataValue:
                    return XmlTokenColor.CDataValue;

                case XmlTokenType.DocTypeDefValue:
                    return XmlTokenColor.DocTypeDefValue;

                case XmlTokenType.CommentValue:
                    return XmlTokenColor.CommentValue;

                case XmlTokenType.DocTypeName:
                    return XmlTokenColor.DocTypeName;

                case XmlTokenType.NodeName:
                    return XmlTokenColor.NodeName;

                case XmlTokenType.AttributeName:
                    return XmlTokenColor.AttributeName;

                case XmlTokenType.DocTypeDeclaration:
                    return XmlTokenColor.DocTypeDeclaration;

                case XmlTokenType.Unknown:
                    return XmlTokenColor.Unknown;


                default:
                    return XmlTokenColor.Default;
            }
        }

        public string GetXmlDeclaration(string s)
        {
            int start = s.IndexOf("<?");
            int end = s.IndexOf("?>");
            if (start > -1 &&
                end > start)
            {
                return s.Substring(start, end - start + 2);
            }
            else return string.Empty;
        }
        #endregion Public Methods

        #region Private Methods
        private void HandleAttributeEnd(int location)
        {
            if (subString.StartsWith(">"))
            {
                HandleReservedXmlToken();
            }
            else if (subString.StartsWith("/>"))
            {
                HandleReservedXmlToken();
            }
            else if (subString.StartsWith("?>"))
            {
                HandleReservedXmlToken();
            }
            else
            {
                CurrentState = XmlTokenType.AttributeName;
                token = ReadAttributeName(subString, location);
            }
        }

        private void HandleReservedXmlToken()
        {
            // check if state changer
            // <, >, =, </, />, <![CDATA[, <!--, -->

            if (subString.StartsWith("<![CDATA["))
            {
                CurrentState = XmlTokenType.CDataStart;
                token = "<![CDATA[";
            }
            else if (subString.StartsWith("<!DOCTYPE"))
            {
                CurrentState = XmlTokenType.DocTypeStart;
                token = "<!";
            }
            else if (subString.StartsWith("</"))
            {
                CurrentState = XmlTokenType.NodeStart;
                token = "</";
            }
            else if (subString.StartsWith("<!--"))
            {
                CurrentState = XmlTokenType.CommentStart;
                token = "<!--";
            }
            else if (subString.StartsWith("<?"))
            {
                CurrentState = XmlTokenType.XmlDeclarationStart;
                token = "<?";
            }
            else if (subString.StartsWith("<"))
            {
                CurrentState = XmlTokenType.NodeStart;
                token = "<";
            }
            else if (subString.StartsWith("="))
            {
                CurrentState = XmlTokenType.EqualSignStart;
                if (CurrentState == XmlTokenType.AttributeValue) CurrentState = XmlTokenType.EqualSignEnd;
                token = "=";
            }
            else if (subString.StartsWith("?>"))
            {
                CurrentState = XmlTokenType.XmlDeclarationEnd;
                token = "?>";
            }
            else if (subString.StartsWith(">"))
            {
                CurrentState = XmlTokenType.NodeEndValueStart;
                token = ">";
            }
            else if (subString.StartsWith("-->"))
            {
                CurrentState = XmlTokenType.CommentEnd;
                token = "-->";
            }
            else if (subString.StartsWith("]>"))
            {
                CurrentState = XmlTokenType.DocTypeEnd;
                token = "]>";
            }
            else if (subString.StartsWith("]]>"))
            {
                CurrentState = XmlTokenType.CDataEnd;
                token = "]]>";
            }
            else if (subString.StartsWith("/>"))
            {
                CurrentState = XmlTokenType.NodeEnd;
                token = "/>";
            }
            else if (subString.StartsWith("\""))
            {
                if (CurrentState == XmlTokenType.AttributeValue)
                {
                    CurrentState = XmlTokenType.DoubleQuotationMarkEnd;
                }
                else
                {
                    CurrentState = XmlTokenType.DoubleQuotationMarkStart;
                }
                token = "\"";
            }
            else if (subString.StartsWith("'"))
            {
                CurrentState = XmlTokenType.SingleQuotationMarkStart;
                if (CurrentState == XmlTokenType.AttributeValue)
                {
                    CurrentState = XmlTokenType.SingleQuotationMarkEnd;
                }
                token = "'";
            }
        }

        private List<string> GetAttributeTokens(string s)
        {
            List<string> list = new List<string>();
            string[] arr = s.Split(' ');
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Trim();
                if (arr[i].Length > 0) list.Add(arr[i]);
            }
            return list;
        }

        private string ReadNodeName(string s, int location)
        {
            string nodeName = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '/' ||
                    s[i] == ' ' ||
                    s[i] == '>')
                {
                    return nodeName;
                }
                else nodeName += s[i].ToString();
            }

            return nodeName;
        }

        private string ReadAttributeName(string s, int location)
        {
            string attName = "";

            int n = s.IndexOf('=');
            if (n != -1) attName = s.Substring(0, n);

            return attName;
        }

        private string ReadNodeValue(string s, int location)
        {
            string nodeValue = "";

            int n = s.IndexOf('<');
            if (n != -1) nodeValue = s.Substring(0, n);

            return nodeValue;
        }

        private string ReadCommentValue(string s, int location)
        {
            string commentValue = "";

            int n = s.IndexOf("-->");
            if (n != -1) commentValue = s.Substring(0, n);

            return commentValue;
        }



        private string GetWhitespace(string s, int location)
        {
            bool foundWhitespace = false;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; (location + i) < s.Length; i++)
            {
                char c = s[location + i];
                if (Char.IsWhiteSpace(c))
                {
                    foundWhitespace = true;
                    sb.Append(c);
                }
                else break;
            }
            if (foundWhitespace) return sb.ToString();
            return String.Empty;
        }
        #endregion Private Methods
    }
}
