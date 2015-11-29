using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace KChatManager.Utils
{
    class FontParser
    {
        private XmlDocument xml = new XmlDocument();

        public XmlNode parseFont(List<String> fontList)
        {
            XmlNode result = xml.CreateElement("font");

            //font-size:9pt;font-family:'MS Sans Serif',sans-serif;" color='505050'
            /*
            foreach (String font in fontList)
            {
                if (font != null)
                {
                    var size = getWordsBetween(font, ":", true, "pt", true);
                    var family = getWordsBetween(font, "y:", true, ";", false);
                    var color = getWordsBetween(font, "='", false, "'", false);
                    XmlElement fontStyle = xml.CreateElement("fontStyle");
                    fontStyle.SetAttribute("index", fontList.IndexOf(font).ToString());
                    XmlElement fontSize = xml.CreateElement("fontSize");
                    fontSize.InnerText = size;
                    XmlElement fontFamily = xml.CreateElement("fontFamily");
                    fontFamily.InnerText = family;
                    XmlElement fontColor = xml.CreateElement("color");
                    fontColor.InnerText = color;
                    fontStyle.AppendChild(fontSize);
                    fontStyle.AppendChild(fontFamily);
                    fontStyle.AppendChild(fontColor);
                    result.AppendChild(fontStyle);
                }
            }
             */
            return result;
        }
    }
}
