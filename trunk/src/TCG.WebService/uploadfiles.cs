using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.IO.Compression;
using System.Drawing;

using TCG.Utils;
using TCG.Pages;
using TCG.Files.Utils;
using TCG.Entity;
using TCG.Handlers;
using TCG.Data;


namespace TCG.WebService
{
    /// <summary>
    ///updatefiles 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://files.xzdsw.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
    // [System.Web.Script.Services.ScriptService]
    public class uploadfiles : System.Web.Services.WebService
    {

        public uploadfiles()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string Show(string text)
        {
            return text;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="caseid">案件号</param>
        /// <param name="twoid">车编号、人编号</param>
        /// <param name="pic">压缩后二进制格式的图片</param>
        /// <param name="picmemo">图片备注</param>
        /// <param name="tablename">存储在数据库中的表名</param>
        /// <param name="filedname">单证类型</param>
        /// <param name="exifTime">EXIF信息</param>
        /// <returns></returns>
        [WebMethod]
        public string UpLoadImage(string iClassId, byte[] images, string filename, string adminname, string Key)
        {
            if (Key != "XZDSW.ADMIN")
            {
                return "";
            }

            string err = string.Empty;
            string url = string.Empty;
            FileInfos item = new FileInfos();
            item.iID = Bases.ToLong(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", ""));
            item.iClassId = Bases.ToInt(iClassId);
            MemoryStream ms = new MemoryStream(images);
            Bitmap img = new Bitmap(ms);
            Connection conn = new Connection();
            item.iSize = images.Length / 1024;
            if (item.iSize <= FilesConst.fileSize)
            {
                string ex = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                item.vcFileName = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);

                if (CheckType(ex))
                {
                    item.vcType = ex.Replace(".", "");

                    err = "attach-" + item.iID.ToString() + ex;

                    fileclasshandlers fchdl = new fileclasshandlers();

                    string filepath = fchdl.GetFilesPathByClassId(conn, item.iClassId);
                    filepath += item.iID.ToString().Substring(0, 6) + "/"
                    + item.iID.ToString().Substring(6, 2) + "/" + item.iID.ToString() + ex;
                    bool create = false;
                    try
                    {
                        filepath = Server.MapPath("~" + filepath);
                        Text.SaveFile(filepath, "");
                        img.Save(filepath);
                        create = true;
                    }
                    catch
                    {
                        create = false;
                        err = "";
                    }

                    if (create)
                    {
                        fileinfoHandlers flfh = new fileinfoHandlers();

                        int rtn = flfh.AddFileInfoByAdmin(conn, adminname, item);
                        if (rtn < 0)
                        {
                            err = "";
                            System.IO.File.Delete(filepath);
                        }
                    }
                }
                else
                {
                    err = "";
                }
            }

            return err;
        }

        private bool CheckType(string str)
        {
            string t = str.Replace(".", "");
            string text = FilesConst.alowFileType.Replace("'", "");
            string[] te = text.Split(',');
            for (int i = 0; i < te.Length; i++)
            {
                if (te[i].ToLower() == t.ToLower()) return true;
            }
            return false;
        }
    }

}