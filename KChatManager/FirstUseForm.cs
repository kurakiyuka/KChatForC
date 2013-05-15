using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace KChatManager
{
    public partial class FirstUseForm : Form
    {
        private String kChatFileFolderPath;

        public FirstUseForm()
        {
            InitializeComponent();
        }

        private void browseKChatFileFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlgSelectKChatFolder = new FolderBrowserDialog();
            dlgSelectKChatFolder.Description = "Select The Folder For Storing KChat Files";
            if (dlgSelectKChatFolder.ShowDialog() == DialogResult.OK)
            {
                kChatFileFolder_txt.Text = dlgSelectKChatFolder.SelectedPath + "KChatManager\\";
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
                    XmlDocument config = new XmlDocument();
                    XmlDeclaration dec = config.CreateXmlDeclaration("1.0", "UTF-8", null);
                    config.AppendChild(dec);
                    XmlElement root = config.CreateElement("config");
                    config.AppendChild(root);
                    XmlElement dir = config.CreateElement("directory");
                    dir.InnerText = kChatFileFolderPath;
                    root.AppendChild(dir);
                    config.Save(configFileFolderPath + "\\config.xml");

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
