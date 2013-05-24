using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace KChatManager.UserCtrl
{
    public partial class ShowChat : UserControl
    {
        private XmlNode msg;
        private String picFolderPath;

        public ShowChat(XmlNode node, String str)
        {
            InitializeComponent();
            msg = node;
            picFolderPath = str;
            InitializeMsg();
        }

        private void InitializeMsg()
        {
            if (msg.Attributes["type"].Value == "day")
            {
                lbName.Text = "时间";
                lbTime.Text = msg.InnerText;
                this.Controls.Remove(rtbContent);
                this.Height = lbTime.Height + 14;
            }
            else
            {
                lbName.Text = msg.Attributes["speaker"].Value;
                lbTime.Text = msg.Attributes["time"].Value;

                foreach (XmlNode msgItem in msg.ChildNodes)
                {
                    if (msgItem.Name == "p")
                    {
                        rtbContent.AppendText(msgItem.InnerText);
                    }
                    else
                    {
                        String picPath = picFolderPath + msgItem.Attributes["src"].Value;
                        try
                        {
                            Bitmap img = new Bitmap(picPath);
                            Clipboard.SetDataObject(img);
                            DataFormats.Format dataFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (rtbContent.CanPaste(dataFormat))
                            {
                                rtbContent.Paste(dataFormat);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }  
                    }
                }
                rtbContent.ReadOnly = true;
                rtbContent.ScrollBars = RichTextBoxScrollBars.None;
                this.Height = lbTime.Height + rtbContent.Height + 14;
            }
        }

        private void rtbContent_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            this.Height = e.NewRectangle.Height;
        }
    }
}
