
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

using TCG.Utils;
using TCG.Data;
using TCG.Files.Utils;
using TCG.Entity;


namespace TCG.Files.Handlers
{
    public class fileinfoHandlers
    {
        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileInfoByAdmin(Connection conn, string adminname, FileInfos fif)
        {
            conn.Dblink = FilesConst.FilesDbLinks[fif.iID % FilesConst.FilesDbLinks.Length];
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iID", SqlDbType.BigInt, 8); sp2.Value = fif.iID;
            SqlParameter sp3 = new SqlParameter("@iClassId", SqlDbType.Int, 4); sp3.Value = fif.iClassId;
            SqlParameter sp4 = new SqlParameter("@vcFileName", SqlDbType.NVarChar, 100); sp4.Value = fif.vcFileName;
            SqlParameter sp5 = new SqlParameter("@iSize", SqlDbType.Int, 4); sp5.Value = fif.iSize;
            SqlParameter sp6 = new SqlParameter("@vcType", SqlDbType.VarChar, 10); sp6.Value = fif.vcType;
            SqlParameter sp7 = new SqlParameter("@reValue", SqlDbType.Int); sp7.Direction = ParameterDirection.Output;

            string[] reValues = conn.Execute("SP_Files_FileInfoManageByAdmin", new SqlParameter[] { sp0, sp1,
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
        public FileInfos GetFileInfosById(Connection conn, long id)
        {
            FileInfos item = null;
            conn.Dblink = FilesConst.FilesDbLinks[id % FilesConst.FilesDbLinks.Length];
            string SQL = "SELECT iID,iClassId,vcFileName,iSize,vcType,iDowns,iRequest,vcIP,dCreateDate FROM T_Files_FileInfos (NOLOCK) WHERE iId=" + id.ToString();
            DataTable dt = conn.GetDataTable(SQL);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    item = new FileInfos();
                    item.iID = (long)dt.Rows[0]["iID"];
                    item.iClassId = (int)dt.Rows[0]["iClassId"];
                    item.iSize = (int)dt.Rows[0]["iSize"];
                    item.vcFileName = dt.Rows[0]["vcFileName"].ToString();
                    item.vcIP = dt.Rows[0]["vcIP"].ToString();
                    item.vcType = dt.Rows[0]["vcType"].ToString();
                    item.iRequest = (int)dt.Rows[0]["iRequest"];
                    item.iDowns = (int)dt.Rows[0]["iDowns"];
                    item.dCreateDate = (DateTime)dt.Rows[0]["dCreateDate"];
                }
            }
            return item;
        }

        public string ImgPatchInit(Connection conn, string content, string adminname, int fileclassid, Config config)
        {
            string parrten = "<(img|IMG)[^>]+src=\"([^\"]+)\"[^>]*>";
            MatchCollection matchs = Regex.Matches(content, parrten, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string temp = "";
            foreach (Match item in matchs)
            {
                string text1 = item.Result("$2");
                if (text1.IndexOf(config["FileSite"]) == -1 && temp.IndexOf(text1) == -1)
                {
                    FileInfos imgfile = new FileInfos();
                    fileclasshandlers fchdl = new fileclasshandlers();

                    imgfile.iID = Bases.ToLong(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", ""));
                    imgfile.iClassId = fileclassid;

                    imgfile.vcFileName = text1.Substring(text1.LastIndexOf("/") + 1, text1.Length - text1.LastIndexOf("/") - 1);
                    imgfile.vcType = text1.Substring(text1.LastIndexOf(".") + 1, text1.Length - text1.LastIndexOf(".") - 1);

                    WebClient wc = new WebClient();
                   
                    imgfile.iSize = 100;
                    string filepath = fchdl.GetFilesPathByClassId(conn, imgfile.iClassId);
                    string filename = imgfile.iID + text1.Substring(text1.LastIndexOf("."), text1.Length - text1.LastIndexOf("."));
                    string filepatch = HttpContext.Current.Server.MapPath("~" + filepath + imgfile.iID.ToString().Substring(0, 6) + "/"
                        + imgfile.iID.ToString().Substring(6, 2) + "/" + filename);
                    try
                    {
                        Text.SaveFile(filepatch, "");
                        if (this.GetUrlError(text1) == 200)
                        {
                            wc.DownloadFile(text1, filepatch);
                            int rtn = this.AddFileInfoByAdmin(conn, adminname, imgfile);
                            if (rtn < 0)
                            {
                                System.IO.File.Delete(filepatch);
                            }
                            else
                            {
                                content = content.Replace(text1, config["FileSite"] + "/manage/attach.aspx?attach=" + imgfile.iID.ToString());
                                temp = text1 + "@@@" + temp;
                            }
                        }

                    }
                    catch
                    {
                    }
                    imgfile = null;
                    fchdl = null;
                    wc = null;
                }
            }
            return content;
        }

        public string ImgPatchInit(Connection conn, string content,string url, string adminname, int fileclassid, Config config)
        {
            string parrten = "<(img|IMG)[^>]+src=\"([^\"]+)\"[^>]*>";
            MatchCollection matchs = Regex.Matches(content, parrten, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string temp = "";
            foreach (Match item in matchs)
            {
                string text1 = item.Result("$2");
                if (text1.IndexOf(config["FileSite"]) == -1 && temp.IndexOf(text1) == -1)
                {
                    FileInfos imgfile = new FileInfos();
                    fileclasshandlers fchdl = new fileclasshandlers();

                    imgfile.iID = Bases.ToLong(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", ""));
                    imgfile.iClassId = fileclassid;

                    imgfile.vcFileName = text1.Substring(text1.LastIndexOf("/") + 1, text1.Length - text1.LastIndexOf("/") - 1);
                    imgfile.vcType = text1.Substring(text1.LastIndexOf(".") + 1, text1.Length - text1.LastIndexOf(".") - 1);

                    WebClient wc = new WebClient();
                    imgfile.iSize = 100;
                    string filepath = fchdl.GetFilesPathByClassId(conn, imgfile.iClassId);
                    string filename = imgfile.iID + text1.Substring(text1.LastIndexOf("."), text1.Length - text1.LastIndexOf("."));
                    string filepatch = HttpContext.Current.Server.MapPath("~" + filepath + imgfile.iID.ToString().Substring(0, 6) + "/"
                        + imgfile.iID.ToString().Substring(6, 2) + "/" + filename);
                    try
                    {
                        Text.SaveFile(filepatch, "");
                        string imgurlpath = TxtReader.GetFileWebPath(url, text1);
                        if (this.GetUrlError(imgurlpath) == 200)
                        {
                            wc.DownloadFile(imgurlpath, filepatch);

                            int rtn = this.AddFileInfoByAdmin(conn, adminname, imgfile);
                            if (rtn < 0)
                            {
                                System.IO.File.Delete(filepatch);
                            }
                            else
                            {
                                content = content.Replace(text1, config["FileSite"] + "/manage/attach.aspx?attach=" + imgfile.iID.ToString());
                                temp = text1 + "@@@" + temp;
                            }
                        }
                    }
                    catch
                    {
                    }
                    imgfile = null;
                    fchdl = null;
                    wc = null;
                }
            }
            return content;
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
    }
}
