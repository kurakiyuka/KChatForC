using System;
using System.Windows.Forms;
using System.IO;

namespace KChatManager
{
    public partial class Main : Form
    {
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
            
        }
    }
}
