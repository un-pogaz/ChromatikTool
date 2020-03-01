using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

class Bologue
{
    public static void NooSFereTabe(string path, int split)
    {
        XmlDocument src = XmlDocumentCreate.ParseHTML(path);
        XmlRacine rslt = new XmlRacine("table");
        rslt.SetAttribute("width", "100%");
        rslt.SetAttribute("align", "center");
        XmlElement body = rslt.AppendElement("tbody");
        
        foreach (XmlElement item in src.GetElementsByTagName("tbody")[0].GetElements("tr"))
        {
            try
            {
                XmlElement row = rslt.OwnerDocument.CreateElement("tr");

                XmlElement td = row.AppendElement("td");
                td.SetAttribute("class", "number");
                XmlText num = td.AppendText(item.GetElements("td")[0].InnerText.Trim(WhiteCharacter.WhiteCharacters.Concat('.')));
                ;
                td = row.AppendElement("td");
                td.SetAttribute("class", "bullet");
                td.AppendText("•");
                ;

                XmlElement data = item.GetElements("td")[1].GetElements("span")[0];

                td = row.AppendElement("td");
                td.SetAttribute("class", "auteur");
                td.AppendText(TitleCase(data.GetElements("a")[1].InnerText));
                ;
                td = row.AppendElement("td");
                td.AppendText(data.GetElements("a")[0].InnerText);
                ;
                XmlElement[] sub = data.GetElements("span", "class", "SousFicheNiourf");
                if (sub.Length > 0)
                {
                    td.AppendText(" ");
                    td.AppendElement("span");
                    td.LastElement().SetAttribute("class", "reedition");
                    td.LastElement().AppendText("("+ sub[0].GetElements("a").Length + " réédition)");
                }
                
                td = row.AppendElement("td");
                td.SetAttribute("class", "date");
                if (data.InnerText.RegexIsMatch(@"\d{4}"))
                    td.AppendText(data.InnerText.RegexGetMatch(@"\d{4}"));
                else
                    td.AppendText("");

                body.AppendChild(row);

                int i;
                if (split > 0 && int.TryParse(num.InnerText, out i) && i % split == 0)
                    body = rslt.AppendElement("tbody");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        XmlWriterSettings set = XmlDocumentWriter.Settings;
        set.IndentChars = "";
        set.OmitXmlDeclaration = true;
        XmlDocumentWriter.Document(path, rslt, set);
    }

    private static string TitleCase(string s)
    {
        string[] split = s.Split(' ', WhiteCharacter.NBSP);

        for (int i = 0; i < split.Length; i++)
        {
            if (split.Length == 1 || split[i] != split[i].ToLower())
            {
                string[] sub = split[i].Split('.');
                for (int ii = 0; ii < sub.Length; ii++)
                    if (sub[ii].Length > 0)
                        sub[ii] = sub[ii][0].ToString().ToUpper() + sub[ii].Substring(1).ToLower();

                split[i] = sub.ToOneString(".");
                
                sub = split[i].Split('-');
                for (int ii = 0; ii < sub.Length; ii++)
                    if (sub[ii].Length > 0)
                        sub[ii] = sub[ii][0].ToString().ToUpper() + sub[ii].Substring(1).ToLower();

                split[i] = sub.ToOneString("-");
            }

            if (split.Length != 1 && split[i].RegexIsMatch("Mc.|Mac."))
                split[i] = split[i].RegexGetMatch("Mc|Mac") + TitleCase(split[i].Regex("(Mc|Mac)(.+)", "$2"));
        }

        return split.ToOneString(" ", StringJoinOptions.SkipNullAndWhiteSpace);
    }
}