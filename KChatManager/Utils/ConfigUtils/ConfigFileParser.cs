using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KChatManager.Data;

namespace KChatManager.Utils.ConfigUtils
{
    class ConfigFileParser
    {
        private Config cfg;

        public Config parseConfig(String path)
        {
            cfg = new Config();
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return cfg;
            }
            cfg.Directory = xml.SelectSingleNode("//directory").InnerText;
            return cfg;
        }
    }
}
