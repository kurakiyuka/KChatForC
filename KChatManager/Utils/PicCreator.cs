using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace KChatManager.Utils
{
    class PicCreator
    {
        private String storePath;

        //path是整个KChat工程的存储目录
        public PicCreator(String path)
        {
            //图片被存放在总的存储目录下面的一个文件夹内，如果不存在，则先创建这个文件夹
            storePath = path + "Common Files\\images\\";
            if (!Directory.Exists(storePath))
            {
                Directory.CreateDirectory(storePath);
            }
        }

        public String createPic(String base64Code, String format)
        {
            try
            {
                //将图片的base64代码进行Hash，把Hash结果作为文件名，再加上后缀名
                String fileName = base64Code.GetHashCode().ToString();
                fileName = fileName + "." + format;

                //生成图片的完整路径
                String fileFullPath = storePath + "\\" + fileName + "." + format;

                //如果图片还不存在，那么创建它，最后把图片文件名返回
                if (!File.Exists(fileFullPath))
                {
                    byte[] arr = Convert.FromBase64String(base64Code);
                    MemoryStream ms = new MemoryStream(arr, 0, arr.Length);
                    ms.Write(arr, 0, arr.Length);
                    Image image = Image.FromStream(ms, true);

                    switch (format)
                    {
                        case "jpeg":
                            image.Save(fileFullPath, ImageFormat.Jpeg);
                            break;
                        case "gif":
                            image.Save(fileFullPath, ImageFormat.Gif);
                            break;
                        case "png":
                            image.Save(fileFullPath, ImageFormat.Png);
                            break;
                        case "bmp":
                            image.Save(fileFullPath, ImageFormat.Bmp);
                            break;
                        default:
                            break;
                    }
                    ms.Close();
                }

                return fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
                return "Error Happened.";
            }
        }
    }
}
