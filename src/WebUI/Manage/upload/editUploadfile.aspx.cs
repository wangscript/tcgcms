using System;
using System.IO;
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
            HttpFileCollection Fs = Request.Files;
            string text = "{Url:\"\",Err:\"\"";
            if (Fs.Count == 1)
            {
                HttpPostedFile PF = Fs[0];

                //得到上传文件的长度；   
                int upFileLength = PF.ContentLength;
                string ContentType = PF.ContentType;
                byte[] bPicture;
                bPicture = new byte[upFileLength];
                Stream filestream = PF.InputStream;
                filestream.Read(bPicture, 0, upFileLength);
                filestream.Close();

                string reUrl = string.Empty;
                
                int rtn = 0;
                try
                {
                    rtn = base.handlerService.fileService.fileInfoHandlers.UploadFile(bPicture, base.adminInfo.vcAdminName, Path.GetExtension(PF.FileName),
                        objectHandlers.ToInt(base.configService.baseConfig["NewsFileClass"]), ref reUrl);
                }
                catch (Exception ex)
                {
                    text = "{Url:'',Err:'" + ex.Message.ToString() + "'}";
                }

                if (rtn < 0)
                {
                    text = "{Url:'',Err:'" + errHandlers.GetErrTextByErrCode(rtn, base.configService.baseConfig["ManagePath"]) + "'}";
                }
                else if (rtn == 1)
                {
                    text = "{Url:'" + reUrl + "',Err:''}";
                }

            }
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
