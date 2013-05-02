using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KChatManager
{
    public partial class OpenFile : Form
    {
        private String _chatFilePath;
        private String _allContent;

        public OpenFile()
        {
            InitializeComponent();
        }

        private void browseToChooseChatFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectChatFile = new OpenFileDialog();
            selectChatFile.Title = "Select Chat File";          
            selectChatFile.Filter = "Supported Files (*.mht;*.txt;*.xml)|*.mht;*.txt;*.xml|All Files(*.*)|*.*";
            if (selectChatFile.ShowDialog() == DialogResult.OK)
            {
                chatFileDirectory_txt.Text = selectChatFile.FileName;
                _chatFilePath = chatFileDirectory_txt.Text;
            }
        }

        private void createKChatFile_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader fileStream = new StreamReader(_chatFilePath, Encoding.Default);
                _allContent = fileStream.ReadToEnd();
                MessageBox.Show(_allContent);
                fileStream.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }
        }
    }
}
