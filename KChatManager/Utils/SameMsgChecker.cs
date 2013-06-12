using System;
using System.Xml;

namespace KChatManager.Utils
{
    public static class SameMsgChecker
    {
        public static Boolean checkSame(XmlElement msg1, XmlElement msg2)
        {
            Boolean sameContent = (msg1.InnerXml == msg2.InnerXml);
            Boolean sameSpeaker = (msg1.Attributes["speaker"].Value == msg2.Attributes["speaker"].Value);
            DateTime dt1 = Convert.ToDateTime(msg1.Attributes["time"].Value);
            DateTime dt2 = Convert.ToDateTime(msg2.Attributes["time"].Value);
            Boolean sameTime = (Math.Abs(dt1.Subtract(dt2).TotalSeconds) < 3);

            if (sameContent && sameSpeaker && sameTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
