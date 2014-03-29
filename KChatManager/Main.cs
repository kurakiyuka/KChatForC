using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KChatManager.UserCtrl;
using KChatManager.Utils.ConfigUtils;

namespace KChatManager
{
    public partial class Main : Form
    {
        private String projectFolderPath;
        private String contactFilePath;
        private String configFileFolderPath;
        private String configFilePath;
        private String picFolderPath;
        private int counter = 0;

        private const String PROJECTNAME = "KChatManager";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            checkConfigFile();

            TreeNode node = new TreeNode("联系人");

            contactFilePath = projectFolderPath + "Common Files\\contact.xml";
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

        private void checkConfigFile()
        {
            configFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + PROJECTNAME;
            configFilePath = configFileFolderPath + "\\config.xml";

            if (!File.Exists(configFilePath))
            {
                projectFolderPath = "C:\\" + PROJECTNAME + "\\";
              
                String[] eleArray = { "directory", projectFolderPath };
                //Write Config File
                new ConfigFileCreator().CreateConfigFile(configFileFolderPath, configFilePath, "config", eleArray);
            }
            else
            {
                readConfigFile();
            }
        }

        private void readConfigFile()
        {
            projectFolderPath = new ConfigFileParser().parseConfig(configFilePath).Directory;
            picFolderPath = projectFolderPath + "Common Files\\images\\";
        }

        private void trvContactList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvContactList.SelectedNode.Level == 1)
            {
                panelChatLog.Controls.Clear();
                counter = 0;
                int totalHeight = 0;
                String path = projectFolderPath + trvContactList.SelectedNode.Text + ".kchat";
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileForm frmOpenFile = new OpenFileForm(projectFolderPath);
            frmOpenFile.Show();
        }

        private void projectFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeProjFolderForm frmChangeProjFolder = new ChangeProjFolderForm(projectFolderPath);
            frmChangeProjFolder.Changed += ProjFolder_Changed;
            frmChangeProjFolder.Show();
        }

        private void ProjFolder_Changed(Object sender, ChangeProjFolderForm.ChangedEventArgs e)
        {
            projectFolderPath = e.projectFolderPath;
        }
    }
}
