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
                //int rtn = UploadFile(_bytes, adminname, imagetype, fileclassid, ref  imagepath);
            }
            return "0|密钥错误";
        }
    }
}

