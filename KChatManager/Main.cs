using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using KChatManager.UserCtrl;
using KChatManager.Utils;

namespace KChatManager
{
    public partial class Main : Form
    {
        private String kChatFileFolderPath;
        private String contactFilePath;
        private String configFilePath;
        private String picFolderPath;
        private int counter = 0;

        public Main()
        {
            InitializeComponent();            
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            checkConfigFile();

            TreeNode node = new TreeNode("联系人");

            contactFilePath = kChatFileFolderPath + "Common Files\\contact.xml";
            if (File.Exists(contactFilePath))
            {
                XmlDocument contact = new XmlDocument();
                contact.Load(contactFilePath);
                foreach (XmlElement el in contact.SelectNodes("//contact"))
                {
                    node.Nodes.Add(new TreeNode(el.InnerText));
                }
            }

            trvContactList.Nodes.Add(node);
            trvContactList.ExpandAll();
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
            kChatFileFolderPath = new ConfigFileParser().parseConfig(configFilePath).Directory;
            picFolderPath = kChatFileFolderPath + "Common Files\\images\\";
        }

        private void trvContactList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvContactList.SelectedNode.Level == 1)
            {
                panelChatLog.Controls.Clear();
                counter = 0;
                int totalHeight = 0;
                String path = kChatFileFolderPath + trvContactList.SelectedNode.Text + ".kchat";
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.Load(path);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(), "IOError");
                    return;
                }
                
                int msgNum = xmlDoc.SelectNodes("//msg").Count;
                ps.update(msgNum);

                foreach(XmlNode msgNode in xmlDoc.SelectNodes("//msg"))
                {
                    ShowChat sc = new ShowChat(msgNode, picFolderPath);
                    sc.Location = new Point(10, totalHeight + 20 * (counter + 1));
                    panelChatLog.Controls.Add(sc);
                    counter++;
                    totalHeight += sc.Height;
                }
            }
        }
    }
}
