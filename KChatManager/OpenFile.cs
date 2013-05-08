using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KChatManager
{
    public partial class OpenFile : Form
    {
        private String _chatFilePath;
        private String _allContent;
        private String _contact;

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
                fileStream.Close();               
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }

            int indexOfBody = _allContent.IndexOf("<body>");
            int indexOfBodyEnd = _allContent.IndexOf("</html>");

            String _allWordsContent = _allContent.Substring(indexOfBody, indexOfBodyEnd - indexOfBody);
            String[] _allWordsArray = Regex.Split(_allWordsContent, "</tr>", RegexOptions.IgnoreCase);
            _contact = _allWordsArray[2].getWordsBetween("消息对象:", true, "</div>", false);
        }
    }
}
