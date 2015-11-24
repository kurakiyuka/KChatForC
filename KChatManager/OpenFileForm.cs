using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using KChatManager.Utils;

namespace KChatManager
{
    public partial class OpenFileForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private String kChatFileFolderPath;
        /// <summary>
        /// 
        /// </summary>
        private String chatFilePath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public OpenFileForm(String path)
        {
            //create contact.xml while opening the OpenFile Dialog first time
            kChatFileFolderPath = path;
            if (!Directory.Exists(kChatFileFolderPath + "Common Files"))
            {
                Directory.CreateDirectory(kChatFileFolderPath + "Common Files");
            }
            if (!File.Exists(kChatFileFolderPath + "Common Files\\contact.xml"))
            {
                try
                {
                    XmlDocument contact = new XmlDocument();
                    XmlDeclaration dec = contact.CreateXmlDeclaration("1.0", "UTF-8", null);
                    contact.AppendChild(dec);
                    XmlElement root = contact.CreateElement("contactlist");
                    contact.AppendChild(root);
                    XmlElement group = contact.CreateElement("group");
                    group.SetAttribute("name", "default");
                    root.AppendChild(group);
                    contact.Save(kChatFileFolderPath + "Common Files\\contact.xml");
                }

                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(), "IOError");
                    return;
                }
            }
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseToChooseChatFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectChatFile = new OpenFileDialog();
            selectChatFile.Title = "Select Chat File";
            selectChatFile.Filter = "Supported Files (*.mht;*.txt;*.xml)|*.mht;*.txt;*.xml|All Files(*.*)|*.*";
            if (selectChatFile.ShowDialog() == DialogResult.OK)
            {
                chatFileDirectory_txt.Text = selectChatFile.FileName;
                chatFilePath = chatFileDirectory_txt.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createKChatFile_Click(object sender, EventArgs e)
        {
            String allContent = String.Empty;
            List<String> fontList = new List<String>();
            XmlDocument resultXML = new XmlDocument();
            XmlElement root;

            try
            {
                StreamReader fileStream = new StreamReader(chatFilePath, Encoding.Default);
                allContent = fileStream.ReadToEnd();
                fileStream.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }            

            String allWordsContent = allContent.getWordsBetween("<body>", true, "</html>", true);
            String allPicsContent = allContent.getWordsBetween("</html>", true, "------=", false);
            String[] allWordsArray = Regex.Split(allWordsContent, "</tr>", RegexOptions.IgnoreCase);
            String[] allPicsArray = Regex.Split(allPicsContent, "------=", RegexOptions.IgnoreCase);

            //get and save contact
            String contact = allWordsArray[2].getWordsBetween("消息对象:", true, "</div>", false);
            new ContactSaver().save(kChatFileFolderPath, contact);

            String savePath = kChatFileFolderPath + contact + ".kchat";
            if (File.Exists(savePath))
            {
                try
                {
                    resultXML.Load(savePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(), "IOError");
                    return;
                }

                root = resultXML.SelectSingleNode("//kchat") as XmlElement;
            }
            else
            {
                XmlDeclaration dec = resultXML.CreateXmlDeclaration("1.0", "UTF-8", null);
                resultXML.AppendChild(dec);
                root = resultXML.CreateElement("kchat");
                //version number used for upgrade
                root.SetAttribute("ver", "0.1");               
                root.SetAttribute("contact", contact);
                resultXML.AppendChild(root);
            }

            /*
             * the first 4 elements in this Array is determined to be useless, so we start loop from 5th element
             * From: <Save by Tencent MsgMgr>...
             * <tr><td><div style=padding-left:10px;>group:music</div></td>
             * <tr><td><div style=padding-left:10px;>contact:kuraki</div></td>
             * <tr><td><div style=padding-left:10px;>&nbsp;</div></td>
             */
            for (int i = 4; i < allWordsArray.Length - 1; i++)
            {
                String date;

                /*
                 * there are two types of elements: Date and Chat Log
                 * each Chat Log element must begin with a Date element that indicates the following elements' happening time
                 */
                if (allWordsArray[i].isDate())
                {
                    //convert date into YYYY-MM-DD format
                    date = allWordsArray[i].getWordsBetween("日期: ", false, "</td>", false).formatDate();

                    String selector = "//day[@day='" + date + "']";
                    if (root.SelectSingleNode(selector) == null)
                    {
                        /*
                         * <day day="2012-03-17">
                         *   <msg type="day">2012-03-17</msg>
                         * </day>
                         */
                        XmlElement dateEle = resultXML.CreateElement("day");
                        dateEle.SetAttribute("day", date);
                        root.AppendChild(dateEle);

                        XmlElement msgEle = resultXML.CreateElement("msg");
                        msgEle.SetAttribute("type", "day");
                        msgEle.InnerText = date;
                        dateEle.AppendChild(msgEle);
                    }
                }

                /*
                 * Chat Log elements
                 * <tr><td><div style=...><div style=...>kurakiyuka</div>13:35:31</div><div style=...><font style=...>hello</font></div></td>
                 * split by </div> into following 4 new items
                 * <td><div style=...><div style=...>kurakiyuka
                 * 13:35:31
                 * <div style=...><font style=...>hello</font>
                 * </td>
                 */
                else
                {
                    String[] msgEleArray = Regex.Split(allWordsArray[i], "</div>", RegexOptions.IgnoreCase);                   
                    String font = null;

                    //get speaker's name from the first item of the array, if blank, means this is a system info
                    String speaker = msgEleArray[0].Substring(msgEleArray[0].LastIndexOf(">") + 1).formatSpeaker();

                    //get speak time from the second item of the array
                    String time = msgEleArray[1];

                    XmlElement msgEle = resultXML.CreateElement("msg");
                    msgEle.SetAttribute("type", "msg");
                    msgEle.SetAttribute("time", time);
                    msgEle.SetAttribute("speaker", speaker);
                    msgEle.SetAttribute("from", "1");

                    //get speak content and font from the third item of the array
                    String[] contentArr = Regex.Split(msgEleArray[2], "><");

                    foreach (String s in contentArr)
                    {
                        if (s.Contains("IMG"))
                        {
                            String src = s.getWordsBetween("{", true, "}", false);
                            XmlElement imgEle = resultXML.CreateElement("img");
                            imgEle.SetAttribute("src", src);
                            msgEle.AppendChild(imgEle);
                        }
                        /*
                         * <font style="font-size:9pt;font-family:'MS Sans Serif',sans-serif;" color='505050'><br></font>
                         * will be cutted into 3 elements we all don't need
                         * font style="font-size:9pt;font-family:'MS Sans Serif',sans-serif;" color='505050' ; br ; /font
                         */
                        else if (s.StartsWith("font") && !s.EndsWith("50'"))
                        {
                            String p = s.getWordsBetween("'>", false, "<", false);
                            font = s.getWordsBetween("style=\"", true, ">", true);                            
                            XmlElement pEle = resultXML.CreateElement("p");
                            pEle.InnerText = p;
                            msgEle.AppendChild(pEle);
                        }
                        else
                        {
                        }

                        if(!fontList.Contains(font))
                        {
                            fontList.Add(font);
                        }
                    }
                    msgEle.SetAttribute("fontStyle", fontList.IndexOf(font).ToString());

                    if (true)
                    {
                        root.LastChild.AppendChild(msgEle);
                    }
                }//end else
            }//end for

            PicCreator picCreator = new PicCreator(kChatFileFolderPath);
            List<string> picMappingList = new List<string>();
            //first element is blank, so loop from j = 1
            for (int j = 1; j < allPicsArray.Length; j++)
            {
                String format = allPicsArray[j].getWordsBetween("image/", true, "Content-Transfer", true).TrimEnd();
                String location = allPicsArray[j].getWordsBetween("n:{", true, "}.d", true);
                String picCode = allPicsArray[j].Substring(allPicsArray[j].IndexOf(".dat") + 4).Trim();
                String picName = picCreator.createPic(picCode, format);
                picMappingList.Add(location);
                picMappingList.Add(picName);
            }
            String[] picMappingArr = picMappingList.ToArray();
            for (int k = 0; k < picMappingArr.Length / 2; k++)
            {
                XmlNodeList pic = null;
                String selector = "//img[@src='" + picMappingArr[2 * k] + "']";
                pic = root.SelectNodes(selector);
                foreach (XmlElement picEle in pic)
                {
                    picEle.SetAttribute("src", picMappingArr[2 * k + 1]);
                }
            }
            
            root.AppendChild(root.OwnerDocument.ImportNode(new FontParser().parseFont(fontList), true));

            try
            {
                resultXML.Save(savePath);
                MessageBox.Show("done");
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }
        }//end function createKChatFile_Click
    }//end class OpenFile
}
