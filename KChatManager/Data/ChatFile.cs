using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KChatManager.Data
{
    class ChatFile
    {
        protected String fileFolderPath;
        protected String content;
        protected String contactName;

        public ChatFile(String content, String fileFolderPath)
        {
            this.content = content;
            this.fileFolderPath = fileFolderPath;
        }

        /// <summary>
        /// 使用正则表达式截取字符串中开始和结束字符串中间的字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        protected String GetWordsBetween(string source, string startStr, string endStr)
        {
            Regex rg = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(source).Value;
        }

        /// <summary>
        /// 对于一个给定的字符串，查找其中是否包含2个特定的子字符串，并返回它们之间的部分
        /// </summary>
        /// <param name="originalStr"></param>
        /// <param name="startWord"></param>
        /// <param name="startWordCountFromStart"></param>
        /// <param name="endWord"></param>
        /// <param name="endWordCountFromStart"></param>
        /// <returns></returns>
        protected String getWordsBetween(String originalStr, String startWord, Boolean startWordCountFromStart, String endWord, Boolean endWordCountFromStart)
        {
            Int32 startIndex, endIndex;
            String subStr;

            if (originalStr.Contains(startWord))
            {
                if (startWordCountFromStart)
                {
                    startIndex = originalStr.IndexOf(startWord);
                }
                else
                {
                    startIndex = originalStr.LastIndexOf(startWord);
                }
                startIndex += startWord.Length;
                subStr = originalStr.Substring(startIndex);
            }
            else
            {
                MessageBox.Show("Error in function getWordsBetween: can't find start word");
                return "Error";
            }

            if (subStr.Contains(endWord))
            {
                if (endWordCountFromStart)
                {
                    endIndex = subStr.IndexOf(endWord);
                }
                else
                {
                    endIndex = subStr.LastIndexOf(endWord);
                }
            }
            else
            {
                MessageBox.Show("Error in function getWordsBetween: can't find end word");
                return "Error";
            }

            return subStr.Substring(0, endIndex);
        }

        protected Boolean isDate(String str)
        {
            if (str.Contains(">日期"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected String formatDate(String str)
        {
            //some version of QQ display Date as YYYY-MM-DD
            if (str.Contains("-"))
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
    }
}
