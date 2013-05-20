using System;
using System.Xml;
using KChatManager.Data;

namespace KChatManager.Utils
{
    class ConfigFileParser
    {
        private Config cfg;
        public Config parSerConfig(String str)
        {
            cfg = new Config();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(str);
            cfg.Directory = xml.SelectSingleNode("//directory").InnerText;
            return cfg;
        }
    }
}
