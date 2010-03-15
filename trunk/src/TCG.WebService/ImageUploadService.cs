using System;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;

namespace TCG.WebService
{
    /// <summary>
    ///ImageUploadService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageUploadService : System.Web.Services.WebService
    {
        protected string Key = "Goodluck123!@#True852*%@";

        public ImageUploadService()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="_bytes">二进数据</param>
        /// <param name="filePath">上传路径</param>
        /// <param name="fileName">文件名称</param>
        [WebMethod]
        public string UploadFile(string key, byte[] _bytes, string filePath, string fileName)
        {
            if (key == Key)
            {
                if (!IsAllowImgExtension(fileName))
                {
                    return "0|文件格式不正确";
                }

                MemoryStream ms = new MemoryStream(_bytes);

                Image image = Image.FromStream(ms);
                Size size = ResizeImage(image.Width, image.Height, ThumpSize, ThumpSize);
                Image bitmap = new Bitmap(image, size);

                FileStream fs = null;
                try
                {
                    string _filepath = CreateFile(filePath + RtnDateToFile());
                    fs = new FileStream(_filepath + fileName, FileMode.Create);
                    ms.WriteTo(fs);

                    //生成缩略图
                    string thumPath = CreateFile("Thump/" + filePath + RtnDateToFile());
                    bitmap.Save(thumPath + fileName, GetImgFormat(fileName));

                    return "1|" + filePath + RtnDateToFile() + fileName;
                }
                catch (Exception ex)
                {
                    return "0|" + ex.Message;
                }
                finally
                {
                    ms.Close();
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    image.Dispose();
                    bitmap.Dispose();
                }
            }
            return "0|密钥错误";
        }

        private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            decimal MAX_WIDTH = (decimal)maxWidth;
            decimal MAX_HEIGHT = (decimal)maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;
            decimal originalWidth = (decimal)width;
            decimal originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
            return new Size(newWidth, newHeight);
        }

        /// <summary>
        /// 检测是否为允许的图片后缀
        /// </summary>
        /// <param name="filePath">图片路径</param>
        private static bool IsAllowImgExtension(string filePath)
        {
            if ("|.jpe|.jpeg|.jpg|.png|.tif|.tiff|.bmp|.gif|".IndexOf("|" + Path.GetExtension(filePath).ToLower() + "|") == -1)
                return false;
            return true;
        }

        /// <summary>
        /// 返回文件夹相对路径
        /// </summary>
        /// <param name="FilPath">保存路径</param>
        private string CreateFile(string FilPath)
        {
            string Path = IMGFile + "/" + FilPath;
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            return Path;
        }

        /// <summary>
        /// 根据后缀获取图片格式
        /// </summary>
        /// <param name="filePath">图片路径</param>
        public static ImageFormat GetImgFormat(string filePath)
        {
            ImageFormat format;
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".jpe":
                case ".jpeg":
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".tif":
                case ".tiff":
                    format = ImageFormat.Tiff;
                    break;
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                default:
                    format = ImageFormat.Jpeg;
                    break;
            }
            return format;
        }

        private string RtnDateToFile()
        {
            DateTime dt = DateTime.Now;

            return dt.Year + "/" + (dt.Month < 10 ? "0" + dt.Month : dt.Month.ToString()) + "/" +
                   (dt.Day < 10 ? "0" + dt.Day : dt.Day.ToString()) + "/";
        }

    }
}

