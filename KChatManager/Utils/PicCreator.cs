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

        public PicCreator(String path)
        {
            storePath = path + "Common Files\\images\\";
            if (!Directory.Exists(storePath))
            {
                Directory.CreateDirectory(storePath);
            }
        }

        public void createPic(String base64Code, String format)
        {
            try
            {
                String fileName = base64Code.GetHashCode().ToString();
                fileName = storePath + "\\" + fileName + "." + format;
                if (!File.Exists(fileName))
                {
                    byte[] arr = Convert.FromBase64String(base64Code);
                    MemoryStream ms = new MemoryStream(arr, 0, arr.Length);
                    ms.Write(arr, 0, arr.Length);
                    Image image = Image.FromStream(ms, true);

                    switch (format)
                    {
                        case "jpeg":
                            image.Save(fileName, ImageFormat.Jpeg);
                            break;
                        case "gif":
                            image.Save(fileName, ImageFormat.Gif);
                            break;
                        case "png":
                            image.Save(fileName, ImageFormat.Png);
                            break;
                        case "bmp":
                            image.Save(fileName, ImageFormat.Bmp);
                            break;
                        default:
                            break;
                    }
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
                return;
            }
        }
    }
}
