using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace System.Globalization.Localization
{
    internal static class XliffStatic
    {
        static char[] invalidCharId = WhiteCharacter.WhiteCharacters.Concat(ControlCharacter.ControlCharacters, ControlCharacterSupplement.ControlCharactersSupplements);

        static internal bool XmlSpacePreserve(this XmlElement node)
        {
            return (node.GetAttribute("xml:space") == "preserve");
        }

        static internal bool IsValideID(string input)
        {
            foreach (var item in invalidCharId)
                if (input.Contains(item.ToString()))
                    return false;

            return true;
        }
        static internal bool TestValideID(this XmlElement node)
        {
            return IsValideID(node.TestAttribut("id"));
        }
        
        static internal string ConvertValideID(string input)
        {
            input = input.Trim(invalidCharId);
            if (IsValideID(input))
                return input;

            return input.Regex(invalidCharId.ToOneString("|"), "");
        }

        static public void TestName(this XmlElement element, string nodeName)
        {
            if (element.LocalName != nodeName)
                throw XliffException.InvalideNodeName(nodeName);
        }
        static public string TestAttribut(this XmlElement node, string atribut)
        {
            if (!node.HasAttribute(atribut))
                throw XliffException.NoAttributFound(node.LocalName, atribut);

            return node.GetAttribute(atribut);
        }
        static public string TestID(this XmlElement node)
        {
            return ConvertValideID(node.TestAttribut("id"));
        }
        
        static public string TestIdentified(this XmlElement node, string nodeName)
        {
            node.TestName(nodeName);
            return node.TestID();
        }



    }
}
