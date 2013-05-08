using System;
using System.Linq;

namespace KChatManager
{
    public static class ExtensionMethods
    {
        public static String getWordsBetween(this String str, String startWord, bool startWordCountFromStart, String endWord, bool endWordCountFromStart)
        {
            int startIndex, endIndex, stringLength;
            if(startWordCountFromStart)
            {
               startIndex  = str.IndexOf(startWord);
            }
            else
            {
                startIndex  = str.LastIndexOf(startWord);
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
    }
}
