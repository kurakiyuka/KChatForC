using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace KChatManager.Utils.ConfigUtils
{
    class ConfigFileCreator
    {
        public void CreateConfigFile(String folderPath, String filePath, String root, String[] eleArray)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(), "IOError");
                    return;
                }
            }

            XmlDocument config = new XmlDocument();
            XmlDeclaration dec = config.CreateXmlDeclaration("1.0", "UTF-8", null);
            config.AppendChild(dec);

            XmlElement rootEle = config.CreateElement(root);
            config.AppendChild(rootEle);

            int leng = eleArray.Length;

            for (int i = 0; i < leng; )
            {
                XmlElement ele = config.CreateElement(eleArray[i]);
                ele.InnerText = eleArray[i + 1];
                rootEle.AppendChild(ele);
                i += 2;
            }

            try
            {
                config.Save(filePath);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }
        }
    }
}
