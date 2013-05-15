using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace KChatManager.Utils
{
    class ContactListParser
    {
        public void parSerContactList(String str)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(str);
        }
    }
}
