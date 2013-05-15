using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using KChatManager.Utils;

namespace KChatManager
{
    public partial class Main : Form
    {
        private String kChatFileFolderPath;
        private String configFilePath;

        public Main()
        {
            InitializeComponent();
            checkConfigFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileForm frmOpenFile = new OpenFileForm(kChatFileFolderPath);
            frmOpenFile.Show();
        }

        private void checkConfigFile()
        {
            configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KChatManager\\config.xml";
            if (!File.Exists(configFilePath))
            {
                FirstUseForm frmFisrtUse = new FirstUseForm();
                frmFisrtUse.ShowDialog(); 
            }

            readConfigFile();
        }

        private void readConfigFile()
        {
            try
            {
                StreamReader fs = new StreamReader(configFilePath, Encoding.Default);
                String config = fs.ReadToEnd();
                fs.Close();

                kChatFileFolderPath = new ConfigFileParser().parSerConfig(config).Directory;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }
        }
    }
}
