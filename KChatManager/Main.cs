using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace KChatManager
{
    public partial class Main : Form
    {
        private String kChatFileFolderPath;

        public Main()
        {
            InitializeComponent();
            checkConfigFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile openFileForm = new OpenFile();
            openFileForm.Show();
        }

        private void checkConfigFile()
        {
            FileInfo configFile = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KChatManager\\config.xml");
            if (configFile.Exists)
            {
                readConfigFile();
            }
            else
            {
                FirstUse firstUseForm = new FirstUse();
                firstUseForm.ShowDialog();
            }
        }

        private void readConfigFile()
        {
            String configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KChatManager\\config.xml";
            try
            {
                XmlReaderSettings xmlSets = new XmlReaderSettings();
                xmlSets.ConformanceLevel = ConformanceLevel.Fragment;
                xmlSets.IgnoreWhitespace = true;
                xmlSets.IgnoreComments = true;

                XmlReader configReader = XmlReader.Create(configFilePath, xmlSets);
                configReader.ReadStartElement("Config");
                configReader.ReadStartElement("Directory");
                kChatFileFolderPath = configReader.ReadString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }
        }
    }
}
