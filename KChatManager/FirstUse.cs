using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml;

namespace KChatManager
{
    public partial class FirstUse : Form
    {
        byte[] byData;
        char[] charData;

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
                kChatFileFolderDirectory.Text = selectKChatFileFolder.SelectedPath + "KChatManager\\";
            }
        }

        private void saveKChatFileFolder_Click(object sender, EventArgs e)
        {
            //Format Config File Content XML
            String config = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Config><Directory>" + kChatFileFolderDirectory.Text + "</Directory></Config>";

            //Create Config File Folder
            String configFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KChatManager";
            if (!Directory.Exists(configFileFolderPath))
            {
                Directory.CreateDirectory(configFileFolderPath);
            }

            //Write Config File
            try
            {
                FileStream configFileStream = new FileStream(configFileFolderPath + "\\config.xml", FileMode.Create);
                charData = config.ToCharArray();
                byData = new byte[charData.Length];
                Encoder en = Encoding.UTF8.GetEncoder();
                en.GetBytes(charData, 0, charData.Length, byData, 0, true);

                configFileStream.Seek(0, SeekOrigin.Begin);
                configFileStream.Write(byData, 0, byData.Length);

                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
    }
}
