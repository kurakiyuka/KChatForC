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
            Char[] splitter = { '/' };
            String[] originalDateArray = str.Split(splitter);
            for (int i = 0; i <= 2; i++)
            {
                if (int.Parse(originalDateArray[i]) < 10)
                {
                    originalDateArray[i] = '0' + originalDateArray[i];
                }
            }
            if (int.Parse(originalDateArray[0]) < 100)
            {
                return originalDateArray[2] + '-' + originalDateArray[0] + '-' + originalDateArray[1];
            }
            else
            {
                return originalDateArray[0] + '-' + originalDateArray[1] + '-' + originalDateArray[2];
            }
        }
    }
}
