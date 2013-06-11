using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace KChatManager.Utils
{
    class ContactSaver
    {
        public void save(String path, String name)
        {
            String contactFilePath = path + "Common Files\\contact.xml";
            XmlDocument contactList = new XmlDocument();
            try
            {
                contactList.Load(contactFilePath);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "IOError");
                return;
            }

            if (!contactList.InnerText.Contains(name))
            {
                XmlNode defalutGroup = contactList.SelectSingleNode("//group[@name='default']");
                XmlElement newContact = contactList.CreateElement("contact");
                newContact.InnerText = name;
                defalutGroup.AppendChild(newContact);

                try
                {
                    contactList.Save(contactFilePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(), "IOError");
                    return;
                }
            }
        }
    }
}
