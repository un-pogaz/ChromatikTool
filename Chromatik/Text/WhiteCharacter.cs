using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{
    /// <summary>
    /// Space character separator
    /// </summary>
    public static class WhiteCharacter
    {
        /// <summary>
        /// U+0020 Space
        /// </summary>
        public static char SPA { get; } = (char)(0x20);
        /// <summary>
        /// U+0020 No-Break Space
        /// </summary>
        public static char NBSP { get; } = (char)(0xA0);

        /// <summary>
        /// U+000A Line Feed \n (aka New Line)
        /// </summary>
        public static char LF { get; } = (char)(0x0A);
        /// <summary>
        /// U+000D Carriage Return \r
        /// </summary>
        public static char CR { get; } = (char)(0x0D);

        /// <summary>
        /// U+0009 Horizontal Tabulation \t
        /// </summary>
        public static char HT { get; } = (char)(0x09);
        /// <summary>
        /// U+000B Vertical Tabulation \v
        /// </summary>
        public static char VT { get; } = (char)(0x0B);

        /// <summary>
        /// U+000C Form Feed \f
        /// </summary>
        public static char FF { get; } = (char)(0x0C);

        /// <summary>
        /// Additional space separator in the Unicode standard
        /// </summary>
        public static class UnicodeSeparator
        {
            /// <summary>
            /// U+1680 Ogham Space Mark
            /// </summary>
            public static char OSM { get; } = (char)(0x1680);
            /// <summary>
            /// U+2000 En Quad (aka Demi-Cadratin)
            /// </summary>
            public static char NQSP { get; } = (char)(0x2000);
            /// <summary>
            /// U+2001  Em Quad (aka Cadratin)
            /// </summary>
            public static char MQSP { get; } = (char)(0x2001);
            /// <summary>
            /// U+2002 En Space (aka Demi-Cadratin space)
            /// </summary>
            public static char ENSP { get; } = (char)(0x2002);
            /// <summary>
            /// U+2003 Em Space (aka Cadratin space)
            /// </summary>
            public static char EMSP { get; } = (char)(0x2003);
            /// <summary>
            /// U+2004 Three-Per-Em Space (aka Thick space)
            /// </summary>
            public static char TPEM { get; } = (char)(0x2004);
            /// <summary>
            /// U+2005 Four-Per-Em Space (aka Mid space)
            /// </summary>
            public static char FPEM { get; } = (char)(0x2005);
            /// <summary>
            /// U+2006 Six-Per-Em Space
            /// </summary>
            public static char SPEM { get; } = (char)(0x2006);
            /// <summary>
            /// U+2007 Figure Space
            /// </summary>
            public static char FSP { get; } = (char)(0x2007);
            /// <summary>
            /// U+2008 Punctuation Space
            /// </summary>
            public static char PSP { get; } = (char)(0x2008);
            /// <summary>
            /// U+2009 Thin Space
            /// </summary>
            public static char THSP { get; } = (char)(0x2009);
            /// <summary>
            /// U+200A Hair Space 
            /// </summary>
            public static char HSP { get; } = (char)(0x200A);
            /// <summary>
            /// U+200B Zero Width Space
            /// </summary>
            public static char ZWSP { get; } = (char)(0x200B);
            /// <summary>
            /// U+200C Zero Width Non-Joiner
            /// </summary>
            public static char ZWNJ { get; } = (char)(0x200C);
            /// <summary>
            /// U+200D Zero Width Joiner
            /// </summary>
            public static char ZWJ { get; } = (char)(0x200D);
            /// <summary>
            /// U+2028 Line Separator
            /// </summary>
            public static char LSEP { get; } = (char)(0x2028);
            /// <summary>
            /// U+2029 Paragraph Separator
            /// </summary>
            public static char PSEP { get; } = (char)(0x2029);
            /// <summary>
            /// U+202F Narrow No-Break Space (No-Break Space)
            /// </summary>
            public static char NNBSP { get; } = (char)(0x202F);
            /// <summary>
            /// U+205F Medium Mathematical Space
            /// </summary>
            public static char MMSP { get; } = (char)(0x205F);
            /// <summary>
            /// U+3000 Ideographic Space
            /// </summary>
            public static char IDSP { get; } = (char)(0x3000);
            /// <summary>
            /// U+303F Ideographic Half Fill Space
            /// </summary>
            public static char IHSP { get; } = (char)(0x303F);
        }

        /// <summary>
        /// Mark character in the Unicode standard
        /// </summary>
        public static class UnicodeMark
        {
            /// <summary>
            /// U+200E Left-To-Right Mark
            /// </summary>
            public static char FTRM { get; } = (char)(0x200E);
            /// <summary>
            /// U+200F Right-To-Left Mark
            /// </summary>
            public static char RTLM { get; } = (char)(0x200F);
            /// <summary>
            /// U+202A Left-To-Right Embedding
            /// </summary>
            public static char LTRE { get; } = (char)(0x202A);
            /// <summary>
            /// U+202B Right-To-Left Embedding
            /// </summary>
            public static char RTLE { get; } = (char)(0x202B);
            /// <summary>
            /// U+202C Pop Directional Formatting
            /// </summary>
            public static char PDF { get; } = (char)(0x202C);
            /// <summary>
            /// U+202D Left-To-Right Override
            /// </summary>
            public static char LTRO { get; } = (char)(0x202D);
            /// <summary>
            /// U+202E Right-To-Left Override
            /// </summary>
            public static char RTLO { get; } = (char)(0x202E);
        }

        /// <summary>
        /// Default char array for <see cref="string.Trim()"/>
        /// </summary>
        static public char[] TrimChar { get; } = new char[]
        {
            SPA,
            LF,
            CR,
            HT,
            VT,
            FF,
            NBSP,

            UnicodeSeparator.OSM,
            UnicodeSeparator.NQSP,
            UnicodeSeparator.MQSP,
            UnicodeSeparator.ENSP,
            UnicodeSeparator.EMSP,
            UnicodeSeparator.TPEM,
            UnicodeSeparator.FPEM,
            UnicodeSeparator.SPEM,
            UnicodeSeparator.FSP,
            UnicodeSeparator.PSP,
            UnicodeSeparator.THSP,
            UnicodeSeparator.HSP,
            UnicodeSeparator.NNBSP,
            UnicodeSeparator.MMSP,
            UnicodeSeparator.IDSP,
            UnicodeSeparator.IHSP,

            UnicodeSeparator.LSEP,
            UnicodeSeparator.PSEP,

            //Perso
            UnicodeSeparator.ZWJ,
            UnicodeSeparator.ZWNJ,
            UnicodeSeparator.ZWSP,
        };

        /// <summary>
        /// End of line char
        /// </summary>
        static public char[] EndLineChar { get; } = new char[]
        {
            LF,
            CR,
            FF,

            UnicodeSeparator.LSEP,
            UnicodeSeparator.PSEP,
        };
    }
}
