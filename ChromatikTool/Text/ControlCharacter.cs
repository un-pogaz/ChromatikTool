using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{

    /// <summary>
    /// Control Character
    /// </summary>
    public static class ControlCharacter
    {
        /// <summary>
        /// U+0000 Null \0
        /// </summary>
        public static char NUL { get; } = (char)(0x00);
        /// <summary>
        /// U+0001 Start of Heading
        /// </summary>
        public static char SOH { get; } = (char)(0x01);
        /// <summary>
        /// U+0002 Start of Text
        /// </summary>
        public static char STX { get; } = (char)(0x02);
        /// <summary>
        /// U+0003 End of Text
        /// </summary>
        public static char ETX { get; } = (char)(0x03);
        /// <summary>
        /// U+0004 End of Transmission
        /// </summary>
        public static char EOT { get; } = (char)(0x04);
        /// <summary>
        /// U+0005 Enquiry
        /// </summary>
        public static char ENQ { get; } = (char)(0x05);
        /// <summary>
        /// U+0006 Acknowledge
        /// </summary>
        public static char ACK { get; } = (char)(0x06);
        /// <summary>
        /// U+0007 Bell \a
        /// </summary>
        public static char BEL { get; } = (char)(0x07);
        /// <summary>
        /// U+0008 Backspace \b
        /// </summary>
        public static char BS { get; } = (char)(0x08);
        /// <summary>
        /// U+0009 Horizontal Tabulation \t
        /// </summary>
        public static char HT { get; } = (char)(0x09);
        /// <summary>
        /// U+000A Line Feed \n (aka New Line)
        /// </summary>
        public static char LF { get; } = (char)(0x0A);
        /// <summary>
        /// U+000B Vertical Tabulation \v
        /// </summary>
        public static char VT { get; } = (char)(0x0B);
        /// <summary>
        /// U+000C Form Feed \f
        /// </summary>
        public static char FF { get; } = (char)(0x0C);
        /// <summary>
        /// U+000D Carriage Return \r
        /// </summary>
        public static char CR { get; } = (char)(0x0D);
        /// <summary>
        /// U+000E Shift Out
        /// </summary>
        public static char SO { get; } = (char)(0x0E);
        /// <summary>
        /// U+000F Shift In
        /// </summary>
        public static char SI { get; } = (char)(0x0F);

        /// <summary>
        /// U+0010 Data Link Escape
        /// </summary>
        public static char DLE { get; } = (char)(0x10);
        /// <summary>
        /// U+0011 Device Control One
        /// </summary>
        public static char DC1 { get; } = (char)(0x11);
        /// <summary>
        /// U+0012 Device Control Two
        /// </summary>
        public static char DC2 { get; } = (char)(0x12);
        /// <summary>
        /// U+0013 Device Control Three
        /// </summary>
        public static char DC3 { get; } = (char)(0x13);
        /// <summary>
        /// U+0014 Device Control Four
        /// </summary>
        public static char DC4 { get; } = (char)(0x14);
        /// <summary>
        /// U+0015 Negative Acknowledge
        /// </summary>
        public static char NAK { get; } = (char)(0x15);
        /// <summary>
        /// U+0016 Synchronous Idle
        /// </summary>
        public static char SYN { get; } = (char)(0x16);
        /// <summary>
        /// U+0017 End of Transmission Block
        /// </summary>
        public static char ETB { get; } = (char)(0x17);
        /// <summary>
        /// U+0018 Cancel
        /// </summary>
        public static char CAN { get; } = (char)(0x18);
        /// <summary>
        /// U+0019 End of Medium
        /// </summary>
        public static char EM { get; } = (char)(0x19);
        /// <summary>
        /// U+001A Substitute
        /// </summary>
        public static char SUB { get; } = (char)(0x1A);
        /// <summary>
        /// U+001B Escape
        /// </summary>
        public static char ESC { get; } = (char)(0x1B);
        /// <summary>
        /// U+001C File Separator
        /// </summary>
        public static char FS { get; } = (char)(0x1C);
        /// <summary>
        /// U+001D Group Separator
        /// </summary>
        public static char GS { get; } = (char)(0x1D);
        /// <summary>
        /// U+001E Record Separator
        /// </summary>
        public static char RS { get; } = (char)(0x1E);
        /// <summary>
        /// U+001F Unit Separator
        /// </summary>
        public static char US { get; } = (char)(0x1F);

        /// <summary>
        /// All Control Characters
        /// </summary>
        static public char[] ControlCharacters { get; } = new char[]
        {
            NUL,
            SOH,
            STX,
            ETX,
            EOT,
            ENQ,
            ACK,
            BEL,
            BS,
            HT,
            LF,
            VT,
            FF,
            CR,
            SO,
            SI,
            DLE,
            DC1,
            DC2,
            DC3,
            DC4,
            NAK,
            SYN,
            ETB,
            CAN,
            EM,
            SUB,
            ESC,
            FS,
            GS,
            RS,
            US,
        };
    }

    /// <summary>
    /// Control Character Supplement
    /// </summary>
    public static class ControlCharacterSupplement
    {
        /// <summary>
        /// U+0080 &lt;Control&gt;
        /// </summary>
        public static char XXX { get; } = (char)(0x80);
        /// <summary>
        /// U+0081 &lt;Control&gt;
        /// </summary>
        public static char XXY { get; } = (char)(0x81);
        /// <summary>
        /// U+0082 Break Permitted Here
        /// </summary>
        public static char BPH { get; } = (char)(0x82);
        /// <summary>
        /// U+0083 No Break Here
        /// </summary>
        public static char NBH { get; } = (char)(0x83);
        /// <summary>
        /// U+0084 &lt;Index&gt;
        /// </summary>
        public static char IND { get; } = (char)(0x84);
        /// <summary>
        /// U+0085 Next Line
        /// </summary>
        public static char NEL { get; } = (char)(0x85);
        /// <summary>
        /// U+0086 Start of Selected Area
        /// </summary>
        public static char SSA { get; } = (char)(0x86);
        /// <summary>
        /// U+0087 End of Selected Area
        /// </summary>
        public static char ESA { get; } = (char)(0x87);
        /// <summary>
        /// U+0088 Character Tabulation Set
        /// </summary>
        public static char HTS { get; } = (char)(0x88);
        /// <summary>
        /// U+0089 Character Tabulation with Justification
        /// </summary>
        public static char HTJ { get; } = (char)(0x89);
        /// <summary>
        /// U+008A Line Tabulation Set
        /// </summary>
        public static char VTS { get; } = (char)(0x8A);
        /// <summary>
        /// U+008B Partial Line Forward
        /// </summary>
        public static char PLD { get; } = (char)(0x8B);
        /// <summary>
        /// U+008C Partial Line Backward
        /// </summary>
        public static char PLU { get; } = (char)(0x8C);
        /// <summary>
        /// U+008D Reverse Line Feed
        /// </summary>
        public static char RI { get; } = (char)(0x8D);
        /// <summary>
        /// U+008E Single Shift Two
        /// </summary>
        public static char SS2 { get; } = (char)(0x8E);
        /// <summary>
        /// U+008F Single Shift Three
        /// </summary>
        public static char SS3 { get; } = (char)(0x8F);

        /// <summary>
        /// U+0090 Device Control String
        /// </summary>
        public static char DCS { get; } = (char)(0x90);
        /// <summary>
        /// U+0091 Private Use One
        /// </summary>
        public static char PU1 { get; } = (char)(0x91);
        /// <summary>
        /// U+0092 Private Use Two
        /// </summary>
        public static char PU2 { get; } = (char)(0x92);
        /// <summary>
        /// U+0093 Set Transmit State
        /// </summary>
        public static char STS { get; } = (char)(0x93);
        /// <summary>
        /// U+0094 Cancel Character
        /// </summary>
        public static char CCH { get; } = (char)(0x94);
        /// <summary>
        /// U+0095 Message Waiting
        /// </summary>
        public static char MW { get; } = (char)(0x95);
        /// <summary>
        /// U+0096 Start of Guarded Area
        /// </summary>
        public static char SPA { get; } = (char)(0x96);
        /// <summary>
        /// U+0097 End of Guarded Area
        /// </summary>
        public static char EPA { get; } = (char)(0x97);
        /// <summary>
        /// U+0098 Start of String
        /// </summary>
        public static char SOS { get; } = (char)(0x98);
        /// <summary>
        /// U+0099 &lt;Control&gt;
        /// </summary>
        public static char XXZ { get; } = (char)(0x99);
        /// <summary>
        /// U+009A Single Character Introducer
        /// </summary>
        public static char SCI { get; } = (char)(0x9A);
        /// <summary>
        /// U+009B Control Sequence Introducer
        /// </summary>
        public static char CSI { get; } = (char)(0x9B);
        /// <summary>
        /// U+009C String Terminator
        /// </summary>
        public static char ST { get; } = (char)(0x9C);
        /// <summary>
        /// U+009D Operating System Command
        /// </summary>
        public static char OSC { get; } = (char)(0x9D);
        /// <summary>
        /// U+009E Privacy Message
        /// </summary>
        public static char PM { get; } = (char)(0x9E);
        /// <summary>
        /// U+009F Application Program Command
        /// </summary>
        public static char APC { get; } = (char)(0x9F);


        /// <summary>
        /// All Supplement Control Characters
        /// </summary>
        static public char[] ControlCharactersSupplements { get; } = new char[]
        {
            XXX,
            XXY,
            BPH,
            NBH,
            IND,
            NEL,
            SSA,
            ESA,
            HTS,
            HTJ,
            VTS,
            PLD,
            PLU,
            RI,
            SS2,
            SS3,
            DCS,
            PU1,
            PU2,
            STS,
            CCH,
            MW,
            SPA,
            EPA,
            SOS,
            XXZ,
            SCI,
            CSI,
            ST,
            OSC,
            PM,
            APC,
        };
    }
}
