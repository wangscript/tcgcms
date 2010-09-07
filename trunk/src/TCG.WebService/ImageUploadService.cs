/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

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

