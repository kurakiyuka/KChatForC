using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace KChatManager
{
    public partial class FirstUse : Form
    {
        private String kChatFileFolderPath;

        public FirstUse()
        {
            InitializeComponent();
        }

        private void browseKChatFileFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectKChatFileFolder = new FolderBrowserDialog();
            selectKChatFileFolder.Description = "Select The Folder For Storing KChat Files";
            if (selectKChatFileFolder.ShowDialog() == DialogResult.OK)
            {
                kChatFileFolder_txt.Text = selectKChatFileFolder.SelectedPath + "KChatManager\\";
                kChatFileFolderPath = kChatFileFolder_txt.Text;
            }
        }

        private void saveKChatFileFolder_Click(object sender, EventArgs e)
        {
            if (kChatFileFolderPath == "" || kChatFileFolderPath == null)
            {
                MessageBox.Show("Please Select the Folder For Storing KChat Files", "Alert");
            }
            else
            {
                //Create Config File Folder
                String configFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KChatManager";
                if (!Directory.Exists(configFileFolderPath))
                {
                    Directory.CreateDirectory(configFileFolderPath);
                }

                //Write Config File
                try
                {
                    XmlWriterSettings xmlSets = new XmlWriterSettings();
                    xmlSets.Indent = true;
                    xmlSets.IndentChars = ("    ");

                    XmlWriter configFileWriter = XmlWriter.Create(configFileFolderPath + "\\config.xml", xmlSets);
                    configFileWriter.WriteStartElement("Config");
                    configFileWriter.WriteElementString("Directory", kChatFileFolderPath);
                    configFileWriter.WriteEndElement();
                    configFileWriter.Flush();

                    this.Close();
                }

                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(), "IOError");
                    return;
                }
            }
        }
    }
}
