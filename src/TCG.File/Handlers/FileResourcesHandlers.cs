
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
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;


namespace TCG.Handlers
{
    public class FileResourcesHandlers : FileResourcesHandlerBase
    {

        public FileResourcesHandlers()
        {
        }

        /// <summary>
        /// 提供文件分类操作的方法
        /// </summary>
        public FileCategoriesHandlers fileClassHandlers
        {
            set
            {
                this._fileclasshandlers = value;
            }
        }
        private FileCategoriesHandlers _fileclasshandlers;
        

        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileInfoByAdmin(string adminname, FileResources fif)
        {
            if (!base.SetFileDatabase(fif.Id)) return -19000000;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@iID", SqlDbType.BigInt, 8); sp2.Value = fif.Id;
            SqlParameter sp3 = new SqlParameter("@iClassId", SqlDbType.Int, 4); sp3.Value = fif.iClassId;
            SqlParameter sp4 = new SqlParameter("@vcFileName", SqlDbType.NVarChar, 100); sp4.Value = fif.vcFileName;
            SqlParameter sp5 = new SqlParameter("@iSize", SqlDbType.Int, 4); sp5.Value = fif.iSize;
            SqlParameter sp6 = new SqlParameter("@vcType", SqlDbType.VarChar, 10); sp6.Value = fif.vcType;
            SqlParameter sp7 = new SqlParameter("@reValue", SqlDbType.Int); sp7.Direction = ParameterDirection.Output;

            string[] reValues = base.conn.Execute("SP_Files_FileInfoManageByAdmin", new SqlParameter[] { sp0, sp1,
                sp2, sp3, sp4, sp5, sp6, sp7 }, new int[] { 7 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }


        /// <summary>
        ///根据ID获得文件信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResources GetFileInfosById(string id)
        {
            FileResources fileresource = (FileResources)CachingService.Get(CachingService.CACHING_FILECATEGORIES_ENTITY + id);

            if (fileresource == null)
            {
                if (!base.SetFileDatabase(id)) return null;
                string SQL = "SELECT iID,iClassId,vcFileName,iSize,vcType,iDowns,iRequest,vcIP,dCreateDate FROM fileresources (NOLOCK) WHERE iId=" + id.ToString();
                DataTable dt = base.conn.GetDataTable(SQL);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        fileresource = (FileResources)base.GetEntityObjectFromRow(dt.Rows[0], typeof(FileResources));
                        CachingService.Set(CachingService.CACHING_FILECATEGORIES_ENTITY + id, fileresource, null);
                    }
                }
            }
            return fileresource;
        }

        public string UrlCheck(string url, Resources resource)
        {
            if (url.Substring(0, 7).ToLower() == "http://") return url;
            if (url.Substring(0, 1).ToLower() == "/")
            {
                string text = resource.SheifUrl.Substring(7, resource.SheifUrl.Length - 7);
                string www = text.Substring(0, text.IndexOf("/"));
                return "http://" + www + url;
            }
            else
            {
                return resource.SheifUrl.Substring(0, resource.SheifUrl.LastIndexOf("/") + 1) + url;
            }
        }

        /// <summary>
        /// 获取文章中间外部网站的图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <param name="adminname"></param>
        /// <param name="fileclassid"></param>
        /// <returns></returns>
        public string ImgPatchInit(Resources resource, string url, string adminname, int fileclassid)
        {
            string parrten = "<(img|IMG)[^>]+src=\"([^\"]+)\"[^>]*>";
            MatchCollection matchs = Regex.Matches(resource.vcContent, parrten, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string temp = "";
            foreach (Match item in matchs)
            {
                string text1 = item.Result("$2");
                if (text1.IndexOf("/attach.aspx?id=") != 0 && text1.IndexOf(ConfigServiceEx.baseConfig["WebSite"]) != 0)
                {
                    FileResources imgfile = new FileResources();

                    imgfile.Id = this.GetFlieName();
                    imgfile.iClassId = fileclassid;
                    string text2 = UrlCheck(text1, resource);
                    bool isload = false;
                    try
                    {
                        WebRequest myre = WebRequest.Create(text2);
                        isload = true;
                    }
                    catch (Exception ex)
                    {
                        
                    }

                    if (isload)
                    {
                        WebClient wc =  new WebClient();
                        byte[] b = wc.DownloadData(text2);
                        Stream s = new MemoryStream(b);

                        System.Drawing.Image loadimage = null;
                        try
                        {
                            loadimage = System.Drawing.Image.FromStream(s);
                        }
                        catch{}

                        if (loadimage != null)
                        {
                            if (loadimage.RawFormat.Equals(ImageFormat.Jpeg))
                            {
                                imgfile.vcType = ".jpg";
                            }
                            else if (loadimage.RawFormat.Equals(ImageFormat.Png))
                            {
                                imgfile.vcType = ".png";
                            }
                            else if (loadimage.RawFormat.Equals(ImageFormat.Tiff))
                            {
                                imgfile.vcType = ".tiff";
                            }
                            else if (loadimage.RawFormat.Equals(ImageFormat.Bmp))
                            {
                                imgfile.vcType = ".bmp";
                            }
                            else if (loadimage.RawFormat.Equals(ImageFormat.Gif))
                            {
                                imgfile.vcType = ".gif";
                            }
                            else
                            {
                                imgfile.vcType = ".jpg";
                            }

                            imgfile.iSize = b.Length;
                            string filename = imgfile.Id + imgfile.vcType;

                            string filepatch = this.GetFilePath(filename, fileclassid);
                            try
                            {
                                objectHandlers.SaveFile(filepatch, "");

                                loadimage.Save(filepatch);
                                int rtn = this.AddFileInfoByAdmin(adminname, imgfile);
                                if (rtn < 0)
                                {
                                    System.IO.File.Delete(filepatch);
                                }
                                else
                                {
                                    resource.vcContent = resource.vcContent.Replace(text1, "/attach.aspx?id=" + imgfile.Id);
                                }
                            }
                            catch
                            {
                            }
                        }
                       
                        imgfile = null;
                        wc = null;
                    }
                }
            }
            return resource.vcContent;
        }

        /// <summary>
        /// 判断远程文件是否存在
        /// </summary>
        /// <param name="curl"></param>
        /// <returns></returns>
        public int GetUrlError(string curl)
        {
            int num = 200;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(curl));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    return 401;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            return num;
        }

        /// <summary>
        /// 检测是否为允许的图片后缀
        /// </summary>
        /// <param name="filePath">图片路径</param>
        private bool IsAllowImgExtension(string filePath)
        {
            if (ConfigServiceEx.baseConfig["alowFileType"].IndexOf("'" + Path.GetExtension(filePath).ToLower() + "'") == -1)
                return false;
            return true;
        }

        public string GetFilePath(string filename, int fileclassid)
        {
            return HttpContext.Current.Server.MapPath("~" + this._fileclasshandlers.GetFilesPathByClassId(fileclassid)
                + filename.Substring(0, 6) + "/" + filename.Substring(6, 2) + "/" + filename);
        }

        private string GetFlieName()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", "");
        }

        public int UploadFile(byte[] _bytes, string adminname, string imagetype, int fileclassid, ref string imagepath)
        {
            if (!IsAllowImgExtension(imagetype))
            {
                return -1000000401;
            }

            MemoryStream ms = new MemoryStream(_bytes);

            Image image = Image.FromStream(ms);

            FileResources imgfile = new FileResources();

            imgfile.Id = this.GetFlieName();
            imgfile.iClassId = fileclassid;

            imgfile.vcFileName = imgfile.Id + imagetype;
            imgfile.vcType = imagetype;

            imgfile.iSize = 100;

            string filepatch = this.GetFilePath(imgfile.vcFileName, fileclassid);
            FileStream fs = null;
            try
            {
                objectHandlers.SaveFile(filepatch, "");
                fs = new FileStream(filepatch, FileMode.Create);
                ms.WriteTo(fs);


                int rtn = 0;
                if (!string.IsNullOrEmpty(adminname))
                {
                    rtn = this.AddFileInfoByAdmin(adminname, imgfile);
                }

                if (rtn < 0)
                {
                    System.IO.File.Delete(filepatch);
                }
                else
                {
                    imagepath = "/attach.aspx?id=" + imgfile.Id;
                }

                return 1;
            }
            catch (Exception ex)
            {
                return -1000000402;
            }
            finally
            {
                ms.Close();
                if (fs != null)
                {
                    fs.Close();
                }
                image.Dispose();
            }

        }

        /// <summary>
        /// 生成缩略图算法
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
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

        
    }
}