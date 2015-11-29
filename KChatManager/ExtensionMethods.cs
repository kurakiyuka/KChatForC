using System;
using System.Windows.Forms;

namespace KChatManager
{
    public static class ExtensionMethods
    {
        public static String formatSpeaker(this String str)
        {
            if(str == "&nbsp;")
			{
				return "system";
			}
            else
            {
			    return str;
            }
        }
    }
}
