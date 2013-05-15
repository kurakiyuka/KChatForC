using System;
using System.Collections.Generic;

namespace KChatManager.Data
{
    class ContactList
    {
        private List<String> contactlist;
        public List<String> Contactlist
        {
            get { return contactlist; }
            set { contactlist = value; }
        }

        public void addContact(String contactName)
        {
            contactlist.Add(contactName);
        }
    }
}
