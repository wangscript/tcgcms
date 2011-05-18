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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using TCG.Utils;
using TCG.Entity;
using System.Net;

namespace TCG.Handlers.Imp.AccEss
{
    public class FileHandlers : IFileHandlers 
    {
        /// <summary>
        /// 从数据库获取所遇分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllFilesClassFromDb()
        {
            return AccessFactory.conn.DataTable("SELECT * FROM filecategories");
        }

        /// <summary>
        /// 获得所有文章分类实体
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllFileCategoriesEntity()
        {
            Dictionary<string, EntityBase> allfilecategories = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_FILECATEGORIES_ENTITY);
            if (allfilecategories == null)
            {
                DataTable dt = GetAllFilesClassFromDb();
                if (dt == null) return null;
                allfilecategories = AccessFactory.GetEntitysObjectFromTable(dt, typeof(FileCategories));
                CachingService.Set(CachingService.CACHING_ALL_FILECATEGORIES_ENTITY, allfilecategories, null);
            }
            return allfilecategories;
        }

        /// <summary>
        /// 根据ID获得文章分类信息
        /// </summary>
        /// <param name="filecateorieid"></param>
        /// <returns></returns>
        public FileCategories GetFileCategories(string filecateorieid)
        {
            Dictionary<string, EntityBase> allfilecategories = this.GetAllFileCategoriesEntity();
            if (allfilecategories == null) return null;
            return allfilecategories.ContainsKey(filecateorieid) ? (FileCategories)allfilecategories[filecateorieid] : null;
        }

        /// <summary>
        /// 获取所有分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="readdb"></param>
        /// <returns></returns>
        public DataTable GetAllFileClass(bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                dt = this.GetAllFilesClassFromDb();
            }
            else
            {
                dt = (DataTable)CachingService.Get(CachingService.CACHING_ALL_FILECLASS);
                if (dt == null)
                {
                    dt = this.GetAllFilesClassFromDb();
                    CachingService.Set(CachingService.CACHING_ALL_FILECLASS, dt, null);
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据父亲获得文件夹
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="prarentid"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetFileCategories(int prarentid)
        {
            Dictionary<string, EntityBase> allfilecategories = this.GetAllFileCategoriesEntity();
            if (allfilecategories == null || allfilecategories.Count == 0) return null;

            Dictionary<string, EntityBase> categories = new Dictionary<string, EntityBase>();

            foreach (KeyValuePair<string, EntityBase> entity in allfilecategories)
            {
                FileCategories tempfilecategories = (FileCategories)entity.Value;
                if (tempfilecategories.iParentId == prarentid)
                {
                    categories.Add(tempfilecategories.Id, tempfilecategories);
                }
            }

            return categories.Count == 0 ? null : categories;
        }

        public string GetFilesPathByClassId(int classid)
        {
            string text1 = this.GetFilesPathByClassIdW(classid);
            return ConfigServiceEx.baseConfig["filePatch"] + text1 + @"/";
        }

        private string GetFilesPathByClassIdW(int classid)
        {
            DataTable dt = this.GetAllFileClass(false);
            if (dt != null)
            {
                DataRow[] Rows = dt.Select("iId =" + classid.ToString());
                if (Rows.Length == 1)
                {
                    string text1 = this.GetFilesPathByClassIdW(objectHandlers.ToInt(Rows[0]["iParentId"]));
                    string text2 = (text1 == "") ? "" : "/";
                    return text1 + text2 + Rows[0]["vcFileName"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileClass(Admin adminf, FileCategories fcif)
        {
            int rtn = AccessFactory.adminHandlers.CheckAdminPower(adminf);
            if (rtn < 0) return rtn;

            //文件名不能为空
            if (string.IsNullOrEmpty(fcif.vcFileName))
            {
                return -1000000057;
            }

            //简单说明不能为空
            if (string.IsNullOrEmpty(fcif.vcMeno))
            {
               return -1000000058;
            }

            AccessFactory.conn.Execute("INSERT INTO filecategories (vcFileName,iParentId,vcMeno) VALUES('" + fcif.vcFileName + "','" + fcif.iParentId + "','" + fcif.vcMeno + "')");

            return 1;
        }



        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileInfoByAdmin(Admin adminf, FileResources fif)
        {
            int rtn = AccessFactory.adminHandlers.CheckAdminPower(adminf);
            if (rtn < 0) return rtn;

            //文件所在的文件夹不能为空
            if (fif.iClassId == 0)
            {
                return -1000000060;
            }

            //文件名称不能为空
            if (string.IsNullOrEmpty(fif.vcFileName))
            {
                return -1000000061;
            }

            //文件类型不能为空
            if (string.IsNullOrEmpty(fif.vcType))
            {
                return -1000000062;
            }

            //文件类型不能为空
            if (fif.iSize==0)
            {
                return -1000000063;
            }

            AccessFactory.conn.Execute("INSERT INTO fileresources (iID,iClassId,vcFileName,iSize,vcType,dCreateDate,vcIP)"
                                        + "VALUES('" + fif.Id + "'," + fif.iClassId + ",'" + fif.vcFileName + "'," + fif.iSize + ",'" 
                                        + fif.vcType + "',now(),'" + fif.vcIP + "')");
            
            return 1;
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
                string SQL = "SELECT iID,iClassId,vcFileName,iSize,vcType,iDowns,iRequest,vcIP,dCreateDate FROM fileresources WHERE iId=" + id.ToString();
                DataTable dt = AccessFactory.conn.DataTable(SQL);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        fileresource = (FileResources)AccessFactory.GetEntityObjectFromRow(dt.Rows[0], typeof(FileResources));
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
        public string ImgPatchInit(Resources resource, string url, Admin adminname, int fileclassid)
        {
            string parrten = "<(img|IMG)[^>]+src=\"([^\"]+)\"[^>]*>";
            MatchCollection matchs = Regex.Matches(resource.vcContent, parrten, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string temp = "";
            foreach (Match item in matchs)
            {
                string text1 = item.Result("$2");
                string domainstr = objectHandlers.GetDomainName(text1);
                string domainstr1 = objectHandlers.GetDomainName(ConfigServiceEx.baseConfig["WebSite"]);
                if (!string.IsNullOrEmpty(domainstr) && domainstr != domainstr1)
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
                        WebClient wc = new WebClient();
                        byte[] b = wc.DownloadData(text2);
                        Stream s = new MemoryStream(b);

                        System.Drawing.Image loadimage = null;
                        try
                        {
                            loadimage = System.Drawing.Image.FromStream(s);
                        }
                        catch { }

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

                            string imagepath1 = string.Empty;

                            string filepatch = this.GetFilePath(filename, fileclassid, ref imagepath1);
                            try
                            {
                                objectHandlers.SaveFile(filepatch, "");

                                loadimage.Save(filepatch);
                                //int rtn = this.AddFileInfoByAdmin(adminname, imgfile);
                                //if (rtn < 0)
                                //{
                                //    System.IO.File.Delete(filepatch);
                                //}
                                //else
                                //{
                                //    resource.vcContent = resource.vcContent.Replace(text1, imagepath1);
                                //}

                                resource.vcContent = resource.vcContent.Replace(text1, imagepath1);
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

        public string GetFilePath(string filename, int fileclassid, ref string imagepath)
        {
            imagepath = this.GetFilesPathByClassId(fileclassid)
                + filename.Substring(0, 6) + "/" + filename.Substring(6, 2) + "/" + filename;
            return HttpContext.Current.Server.MapPath("~" + imagepath);
        }

        private string GetFlieName()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", "");
        }

        public int UploadFile(byte[] _bytes, Admin adminname, string imagetype, int fileclassid, ref string imagepath)
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

            string filepatch = this.GetFilePath(imgfile.vcFileName, fileclassid, ref imagepath);
            FileStream fs = null;
            try
            {
                objectHandlers.SaveFile(filepatch, "");
                fs = new FileStream(filepatch, FileMode.Create);
                ms.WriteTo(fs);


                //int rtn = 0;
                //if (adminname != null)
                //{
                //    rtn = this.AddFileInfoByAdmin(adminname, imgfile);
                //}

                //if (rtn < 0)
                //{
                //    System.IO.File.Delete(filepatch);
                //}
                //else
                //{
                //    imagepath = "/attach.aspx?id=" + imgfile.Id;
                //}

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
        public ImageFormat GetImgFormat(string filePath)
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