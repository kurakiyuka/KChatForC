using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using KChatManager.Data;
using KChatManager.Utils;

namespace KChatManager
{
    public partial class OpenFileForm : Form
    {
        private String kChatFileFolderPath;
        
        private String chatFilePath;

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

            QQMhtFile qqMhtFile = new QQMhtFile(allContent, kChatFileFolderPath);

            //get and save contact
            String contact = qqMhtFile.getContactName();
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
