using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

class Bologue
{
    public static void NooSFereTabe(string path)
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
                td.AppendText(item.GetElements("td")[0].InnerText.Trim(WhiteCharacter.WhiteCharacters.Concat('.')));
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
                    XmlElement br = td.AppendElement("br");
                    br.SetAttribute("class", "reedition");
                    td.AppendText(sub[0].GetElements("a").Length + " réédition");
                }
                
                td = row.AppendElement("td");
                td.SetAttribute("class", "date");
                if (data.InnerText.RegexIsMatch(@"\d+"))
                    td.AppendText(data.InnerText.RegexGetMatch(@"\d+"));
                else
                    td.AppendText("");

                body.AppendChild(row);
            }
            catch (Exception ex)
            {
            }
        }

        XmlDocumentWriter.Document(path, rslt);
    }

    private static string TitleCase(string s)
    {
        string[] split = s.Split(' ', WhiteCharacter.NBSP);

        for (int i = 0; i < split.Length; i++)
        {
            if (split[i].Length > 0 && (split.Length == 1 || split[i] != split[i].ToLower()))
                split[i] = split[i][0].ToString().ToUpper() + split[i].Substring(1).ToLower();

            if (split.Length != 1 && split[i].RegexIsMatch("Mc.|Mac."))
                split[i] = split[i].RegexGetMatch("Mc|Mac") + TitleCase(split[i].Regex("(Mc|Mac)(.+)", "$2"));
        }

        return split.ToOneString(" ", StringJoinOptions.SkipNullAndWhiteSpace);
    }
}