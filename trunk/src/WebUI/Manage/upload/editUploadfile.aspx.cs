using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TCG.Utils;
using TCG.Pages;

using TCG.Entity;
using TCG.Handlers;

public partial class Manage_upload_editUploadfile : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

        if (Page.IsPostBack)
        {
            FileResources item = new FileResources();
            item.iID = objectHandlers.ToLong(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff").Replace("-", "").Replace(":", "").Replace(" ", ""));
            item.iClassId = objectHandlers.ToInt(base.configService.baseConfig["NewsFileClass"]);

            HttpFileCollection Fs = Request.Files;

            string err = "";
            string url = "";
            if (Fs.Count == 1)
            {
                HttpPostedFile PF = Fs[0];
                string filename = PF.FileName;
                item.iSize = PF.ContentLength / 1024;
                if (item.iSize <= objectHandlers.ToInt(base.configService.baseConfig["fileSize"]))
                {
                    string ex = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    item.vcFileName = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);

                    if (CheckType(ex))
                    {
                        item.vcType = ex.Replace(".", "");
                        url = base.configService.baseConfig["FileSite"] + base.configService.baseConfig["ManagePath"] + "attach.aspx?attach=" + item.iID.ToString();
                       

                        string filepath = base.handlerService.fileService.fileClassHandlers.GetFilesPathByClassId( item.iClassId);
                        filepath += item.iID.ToString().Substring(0, 6) + "/"
                        + item.iID.ToString().Substring(6, 2) + "/" + item.iID.ToString() + ex;
                        bool create = false;
                        try
                        {
                            filepath = Server.MapPath("~" + filepath);
                            objectHandlers.SaveFile(filepath, "");
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
                            
                            int rtn = base.handlerService.fileService.fileInfoHandlers.AddFileInfoByAdmin( base.adminInfo.vcAdminName, item);
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
                    err = "超过" + base.configService.baseConfig["fileSize"] + "K";
                }
            }

            string text = "{Url:'" + url + "',Err:'" + err + "'}";
            base.AjaxErch(text);
        }
    }

    private bool CheckType(string str)
    {
        string t = str.Replace(".", "");
        string text = base.configService.baseConfig["alowFileType"].Replace("'", "");
        string[] te = text.Split(',');
        for (int i = 0; i < te.Length; i++)
        {
            if (te[i].ToLower() == t.ToLower()) return true;
        }
        return false;
    }
}
