using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace KChatManager
{
    public partial class OpenFile : Form
    {
        private String _chatFilePath;
        private String _allContent;
        private String _contact;
        private XmlDocument _resultXML;

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

            _resultXML = new XmlDocument();
            XmlElement root = _resultXML.CreateElement("kchat");
            _resultXML.AppendChild(root);

            String _allWordsContent = _allContent.getWordsBetween("<body>", true, "</html>", true);
            String[] _allWordsArray = Regex.Split(_allWordsContent, "</tr>", RegexOptions.IgnoreCase);
            _contact = _allWordsArray[2].getWordsBetween("消息对象:", true, "</div>", false);
            root.SetAttribute("contact", _contact);

            //the first 4 elements in this Array is determined to be useless, so we start loop from 5th element
            for (int i = 4; i < _allWordsArray.Length; i++)
            {
                /*
			    * there are two types of elements: Date and Chat Log
			    * each Chat Log element must begin with a Date element that indicates the following elements' happening time
			    */
                if (_allWordsArray[i].isDate())
                {
                    //convert date into YYYY-MM-DD format
                    String date = _allWordsArray[i].getWordsBetween("日期: ", false, "</td>", false).formatDate();
                    XmlElement dateEle = _resultXML.CreateElement("day");
                    dateEle.SetAttribute("day", date);
                    root.AppendChild(dateEle);

                    XmlElement msgEle = _resultXML.CreateElement("msg");
                    msgEle.SetAttribute("type", "day");
                    msgEle.InnerText = date;
                    dateEle.AppendChild(msgEle);
                }

                /*
                * Chat Log elements
                * <tr><td><div style=...><div style=...>kurakiyuka</div>13:35:31</div><div style=...><font style=...>hello</font></div></td>
                * split by </div> into following 4 new items
                * <td><div style=...><div style=...>kurakiyuka
                * 13:35:31
                * <div style=...><font style=...>hello</font>
                * </td></tr>
                */
                else
                {
                    String[] msgEleArray = Regex.Split(_allWordsArray[i], "</div>", RegexOptions.IgnoreCase);

                    //get speaker's name from the first item of the array, if blank, means this is a system info
                    String speaker = msgEleArray[0].Substring(msgEleArray[0].LastIndexOf(">") + 1).formatSpeaker();
                }
            }
        }
    }
}
