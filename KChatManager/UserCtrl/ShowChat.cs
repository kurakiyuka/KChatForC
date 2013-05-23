using System.Windows.Forms;
using System.Xml;

namespace KChatManager.UserCtrl
{
    public partial class ShowChat : UserControl
    {
        private XmlNode msg;

        public ShowChat(XmlNode node)
        {
            InitializeComponent();
            msg = node;
            InitializeMsg();
        }

        private void InitializeMsg()
        {
            if (msg.Attributes["type"].Value == "day")
            {
                lbName.Text = "时间";
                lbTime.Text = msg.InnerText;
                this.Controls.Remove(lbContent);
                this.Height = lbTime.Height + 14;
            }
            else
            {
                lbName.Text = msg.Attributes["speaker"].Value;
                lbTime.Text = msg.Attributes["time"].Value;
                lbContent.Text = msg.InnerText;
                this.Height = lbTime.Height + lbContent.Height + 14;
            }
        }
    }
}
