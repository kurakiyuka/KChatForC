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

        //path is the directory of kchat project
        public PicCreator(String path)
        {
            //all images will be stored in an given folder path， user can't change it
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
                //use standard 3 letter suffix
                if (format == "jpeg")
                {
                    format = "jpg";
                }

                //get hash code of this specific image as its file name, ensuring duplicate image won't be created
                String fileName = base64Code.GetHashCode().ToString();
                fileName = fileName + "." + format;

                //generate full path of the image
                String fileFullPath = storePath + "\\" + fileName;

                //if image doesn't exist， create it
                if (!File.Exists(fileFullPath))
                {
                    byte[] arr = Convert.FromBase64String(base64Code);
                    MemoryStream ms = new MemoryStream(arr, 0, arr.Length);
                    ms.Write(arr, 0, arr.Length);
                    Image image = Image.FromStream(ms, true);

                    switch (format)
                    {
                        case "jpg":
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

                //return file name for replacing the original "src" in _resultXML
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
