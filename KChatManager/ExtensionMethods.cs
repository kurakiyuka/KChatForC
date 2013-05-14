using System;
using System.Linq;

namespace KChatManager
{
    public static class ExtensionMethods
    {
        public static String getWordsBetween(this String str, String startWord, bool startWordCountFromStart, String endWord, bool endWordCountFromStart)
        {
            int startIndex, endIndex, stringLength;
            if (startWordCountFromStart)
            {
                startIndex = str.IndexOf(startWord);
            }
            else
            {
                startIndex = str.LastIndexOf(startWord);
            }
            startIndex += startWord.Length;

            if (endWordCountFromStart)
            {
                endIndex = str.IndexOf(endWord);
            }
            else
            {
                endIndex = str.LastIndexOf(endWord);
            }

            stringLength = endIndex - startIndex;
            return str.Substring(startIndex, stringLength);
        }

        public static Boolean isDate(this String str)
        {
            if (str.IndexOf(">日期") != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static String formatDate(this String str)
        {
            //some version of QQ display Date as YYYY-MM-DD
            if (str.IndexOf("-") != -1)
            {
                return str;
            }

            //some version of QQ display Date as MM/DD/YYYY or YYYY/MM/DD, convert them into YYYY-MM-DD
            else
            {
                Char[] splitter = { '/' };
                String[] originalDateArray = str.Split(splitter);
                for (int i = 0; i <= 2; i++)
                {
                    // 3/15 -> 03/15
                    if (originalDateArray[i].Length < 2)
                    {
                        originalDateArray[i] = '0' + originalDateArray[i];
                    }
                }
                // 03/15/2013 -> 2013-03-15
                if (originalDateArray[0].Length < 3)
                {
                    return originalDateArray[2] + '-' + originalDateArray[0] + '-' + originalDateArray[1];
                }
                // 2013/03/15 -> 2013-03-15
                else
                {
                    return originalDateArray[0] + '-' + originalDateArray[1] + '-' + originalDateArray[2];
                }
            }
        }

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
