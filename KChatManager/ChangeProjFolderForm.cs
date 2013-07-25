using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using KChatManager.Utils.ConfigUtils;

namespace KChatManager
{
    public partial class ChangeProjFolderForm : Form
    {
        private String projectFolderPath;
        private const String PROJECTNAME = "KChatManager";

        public delegate void ChangedEventHandler(Object sender, ChangedEventArgs e);
        public event ChangedEventHandler Changed;

        public class ChangedEventArgs : EventArgs
        {
            public readonly String projectFolderPath;
            public ChangedEventArgs(String path)
            {
                this.projectFolderPath = path;
            }
        }

        protected virtual void OnChanged(ChangedEventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public ChangeProjFolderForm(String path)
        {
            InitializeComponent();
            projectFolderPath = projectFolder_txt.Text = path;
        }

        private void browseKChatFileFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlgSelectKChatFolder = new FolderBrowserDialog();
            dlgSelectKChatFolder.Description = "Select The Folder For Storing KChat Files";
            if (dlgSelectKChatFolder.ShowDialog() == DialogResult.OK)
            {
                projectFolder_txt.Text = dlgSelectKChatFolder.SelectedPath + "KChatManager\\";
                projectFolderPath = projectFolder_txt.Text;
            }
        }

        private void saveKChatFileFolder_Click(object sender, EventArgs e)
        {
            if (projectFolderPath == "" || projectFolderPath == null)
            {
                MessageBox.Show("Please Select the Folder For Storing KChat Files", "Alert");
            }
            else
            {
                String configFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + PROJECTNAME;
                String configFilePath = configFileFolderPath + "\\config.xml";

                //in a general way, this won't happen, unless somebody delete the config file while the program running
                if (!File.Exists(configFilePath))
                {
                    String[] eleArray = { "directory", projectFolderPath };
                    //Write Config File
                    new ConfigFileCreator().CreateConfigFile(configFileFolderPath, configFilePath, "config", eleArray);
                    this.Close();
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    try
                    {
                        xml.Load(configFilePath);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(), "IOError");
                    }
                    XmlElement xmle = xml.SelectSingleNode("//directory") as XmlElement;
                    xmle.InnerText = projectFolderPath;
                    try
                    {
                        xml.Save(configFilePath);
                        ChangedEventArgs ev = new ChangedEventArgs(projectFolderPath);
                        OnChanged(ev);
                        this.Close();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(), "IOError");
                        return;
                    }
                }
            }
        }//end function saveKChatFileFolder_Click
    }//end class
}
