using System;
using System.IO;
using System.Xml;

namespace KChatManager.Utils
{
    class ContactSaver
    {
        public void save(String path, String name)
        {
            String contactFilePath = path + "Common Files\\contact.xml";
            XmlDocument contactList = new XmlDocument();
            contactList.Load(contactFilePath);

            if (!contactList.InnerText.Contains(name))
            {
                XmlNode defalutGroup = contactList.SelectSingleNode("//group[@name='default']");
                XmlElement newContact = contactList.CreateElement("contact");
                newContact.InnerText = name;
                defalutGroup.AppendChild(newContact);

                contactList.Save(contactFilePath);
            }
        }
    }
}
