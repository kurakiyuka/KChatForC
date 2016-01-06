using KChatManager.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace KChatManager.Data
{
    class QQMhtFile : ChatFile
    {
        private String allWordsContent;
        private String allPicsContent;

        public QQMhtFile(String content, String fileFolderPath)
            : base(content, fileFolderPath)
        {
            this.allWordsContent = getWordsBetween(content, "<body>", true, "</html>", true);
            this.allPicsContent = getWordsBetween(content, "</html>", true, "------=", false);
        }

        public String getContactName()
        {
            contactName = GetWordsBetween(allWordsContent, "消息对象:", "</div>");
            return contactName;
        }

        public XmlDocument formatContent()
        {
            String[] allWordsArray = Regex.Split(allWordsContent, "</tr>", RegexOptions.IgnoreCase);
            String[] allPicsArray = Regex.Split(allPicsContent, "------=", RegexOptions.IgnoreCase);

            XmlDocument resultXML = new XmlDocument();
            XmlElement root = resultXML.CreateElement("content");

            /*
             * the first 4 elements in this Array is determined to be useless, so we start loop from 5th element
             * From: <Save by Tencent MsgMgr>...
             * <tr><td><div style=padding-left:10px;>group:music</div></td>
             * <tr><td><div style=padding-left:10px;>contact:kuraki</div></td>
             * <tr><td><div style=padding-left:10px;>&nbsp;</div></td>
             */
            for (int i = 4; i < allWordsArray.Length - 1; i++)
            {
                String date;

                /*
                 * there are two types of elements: Date and Chat Log
                 * each Chat Log element must begin with a Date element that indicates the following elements' happening time
                 */
                if (isDate(allWordsArray[i]))
                {
                    //convert date into YYYY-MM-DD format
                    date = formatDate(getWordsBetween(allWordsArray[i], "日期: ", false, "</td>", false));

                    String selector = "//day[@day='" + date + "']";
                    if (root.SelectSingleNode(selector) == null)
                    {
                        /*
                         * <day day="YYYY-MM-DD">
                         *   <msg type="day">YYYY-MM-DD</msg>
                         * </day>
                         */
                        XmlElement dateEle = resultXML.CreateElement("day");
                        dateEle.SetAttribute("day", date);
                        root.AppendChild(dateEle);

                        XmlElement msgEle = resultXML.CreateElement("msg");
                        msgEle.SetAttribute("type", "day");
                        msgEle.InnerText = date;
                        dateEle.AppendChild(msgEle);
                    }
                }

                /*
                 * Chat Log elements
                 * <tr><td><div style=...><div style=...>kurakiyuka</div>13:35:31</div><div style=...><font style=...>hello</font></div></td>
                 * split by </div> into following 4 new items
                 * <td><div style=...><div style=...>kurakiyuka
                 * 13:35:31
                 * <div style=...><font style=...>hello</font>
                 * </td>
                 */
                else
                {
                    String[] msgEleArray = Regex.Split(allWordsArray[i], "</div>", RegexOptions.IgnoreCase);
                    
                    //get speaker's name from the first item of the array, if blank, means this is a system info
                    String speaker = msgEleArray[0].Substring(msgEleArray[0].LastIndexOf(">") + 1).formatSpeaker();

                    //get speak time from the second item of the array
                    String time = msgEleArray[1];

                    XmlElement msgEle = resultXML.CreateElement("msg");
                    msgEle.SetAttribute("type", "msg");
                    msgEle.SetAttribute("time", time);
                    msgEle.SetAttribute("speaker", speaker);
                    msgEle.SetAttribute("from", "1");

                    //get speak content and font from the third item of the array
                    String[] contentArr = Regex.Split(msgEleArray[2], "><");

                    foreach (String s in contentArr)
                    {
                        if (s.Contains("IMG"))
                        {
                            String src = getWordsBetween(s, "{", true, "}", false);
                            XmlElement imgEle = resultXML.CreateElement("img");
                            imgEle.SetAttribute("src", src);
                            msgEle.AppendChild(imgEle);
                        }
                        /*
                         * <font style="font-size:9pt;font-family:'MS Sans Serif',sans-serif;" color='505050'><br></font>
                         * will be cutted into 3 elements we all don't need
                         * font style="font-size:9pt;font-family:'MS Sans Serif',sans-serif;" color='505050' ; br ; /font
                         *
                        else if (s.StartsWith("font") && !s.EndsWith("50'"))
                        {
                            String p = getWordsBetween(s, "'>", false, "<", false);
                            font = getWordsBetween(s, "style=\"", true, ">", true);
                            XmlElement pEle = resultXML.CreateElement("p");
                            pEle.InnerText = p;
                            msgEle.AppendChild(pEle);
                        }
                         */
                    }
                    
                    if (true)
                    {
                        root.LastChild.AppendChild(msgEle);
                    }
                }//end else
            }//end for

            PicCreator picCreator = new PicCreator(fileFolderPath);
            List<String> picMappingList = new List<String>();
            //first element is blank, so loop from j = 1
            for (int j = 1; j < allPicsArray.Length; j++)
            {
                String format = getWordsBetween(allPicsArray[j], "image/", true, "Content-Transfer", true).TrimEnd();
                String location = getWordsBetween(allPicsArray[j], "n:{", true, "}.d", true);
                String picCode = allPicsArray[j].Substring(allPicsArray[j].IndexOf(".dat") + 4).Trim();
                String picName = picCreator.createPic(picCode, format);
                picMappingList.Add(location);
                picMappingList.Add(picName);
            }
            String[] picMappingArr = picMappingList.ToArray();
            for (int k = 0; k < picMappingArr.Length / 2; k++)
            {
                XmlNodeList pic = null;
                String selector = "//img[@src='" + picMappingArr[2 * k] + "']";
                pic = root.SelectNodes(selector);
                foreach (XmlElement picEle in pic)
                {
                    picEle.SetAttribute("src", picMappingArr[2 * k + 1]);
                }
            }

            //root.AppendChild(root.OwnerDocument.ImportNode(new FontParser().parseFont(fontList), true));
            return resultXML;
        }
    }
}
