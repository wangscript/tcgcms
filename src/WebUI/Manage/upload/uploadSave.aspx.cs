using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;


using TCG.Utils;
using TCG.Pages;
using TCG.Files.Utils;
using TCG.Files.Entity;
using TCG.Files.Handlers;

public partial class upload_uploadSave : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int ifid = Bases.ToInt(Fetch.Get("ifid"));
            this.iId.Value = ifid.ToString();

            int ClassId = Bases.ToInt(Fetch.Get("ClassId"));
            this.iClassId.Value = ClassId.ToString();
        }
        else
        {
            Thread.Sleep(20);
            FileInfos item = new FileInfos();
            item.iID = Bases.ToLong(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", ""));
            item.iClassId = Bases.ToInt(Fetch.Post("iClassId"));

            HttpFileCollection Fs = Request.Files;

            string err = "";
            string url = "";
            if (Fs.Count == 1)
            {
                HttpPostedFile PF = Fs[0];
                string filename = PF.FileName;
                item.iSize = PF.ContentLength / 1024;
                if (item.iSize <= FilesConst.fileSize)
                {
                    string ex = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    item.vcFileName = filename.Substring(filename.LastIndexOf("\\")+1, filename.Length - filename.LastIndexOf("\\")-1);

                    if (CheckType(ex))
                    {
                        item.vcType = ex.Replace(".", "");
                        url = base.config["FileSite"] + "attach-" + item.iID.ToString() + ex;
                        fileclasshandlers fchdl = new fileclasshandlers();

                        string filepath = fchdl.GetFilesPathByClassId(base.conn, item.iClassId);
                        filepath += item.iID.ToString().Substring(0, 6) + "/"
                        + item.iID.ToString().Substring(7, 2) + "/" + item.iID.ToString() + ex;
                        bool create = false;
                        try
                        {
                            filepath = Server.MapPath("~" + filepath);
                            PF.SaveAs(filepath);
                            create = true;
                        }
                        catch
                        {
                            create = false;
                            err = "保存路径错误";
                        }

                        if (create)
                        {
                            fileinfoHandlers flfh = new fileinfoHandlers();
                            int rtn = flfh.AddFileInfoByAdmin(base.conn, base.admin.adminInfo.vcAdminName, item);
                            if (rtn < 0)
                            {
                                err = "数据库保存错误";
                                System.IO.File.Delete(filepath);
                            }
                        }
                    }
                    else
                    {
                        err = "类型错误";
                    }
                }
                else
                {
                    err = "超过" + FilesConst.fileSize.ToString() + "K";
                }
            }

            int ifid = Bases.ToInt(Fetch.Post("iId"));
            string text = "<script type=\"text/javascript\">\r\n"
                + "window.parent.UpdateBack(" + ifid.ToString() + ",\"Url:'" + url + "',Err:'" + err + "'\");\r\n"
                + "</script>\r\n";
            base.AjaxErch(text);
        }
    }

    private bool CheckType(string str)
    {
        string t =str.Replace(".","");
        string text = FilesConst.alowFileType.Replace("'", "");
        string[] te = text.Split(',');
        for (int i = 0; i < te.Length; i++)
        {
            if (te[i].ToLower() == t.ToLower()) return true;
        }
        return false;
    }
}
