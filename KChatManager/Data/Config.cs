using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KChatManager.Data
{
    class Config
    {
        private String kChatFileFolderPath;
        public String KChatFileFolderPath
        {
            get { return kChatFileFolderPath; }
            set { kChatFileFolderPath = value; }
        }
    }
}
