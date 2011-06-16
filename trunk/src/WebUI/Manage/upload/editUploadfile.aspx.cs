using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using LitJson;

using TCG.Utils;
using TCG.Entity;
using TCG.Handlers;

namespace TCG.CMS.WebUi
{

    public partial class Manage_upload_editUploadfile : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();


            HttpFileCollection Fs = Request.Files;
            string text = "{url:\"\",error:\"\"";
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
                    rtn = base.handlerService.fileService.fileHandlers.UploadFile(bPicture, base.adminInfo, Path.GetExtension(PF.FileName),
                        objectHandlers.ToInt(ConfigServiceEx.baseConfig["NewsFileClass"]), ref reUrl);
                }
                catch (Exception ex)
                {
                    text = "{url:'',error:'" + ex.Message.ToString() + "'}";
                }

                if (rtn < 0)
                {
                    text = "{url:'',error:'" + errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]) + "'}";
                }
                else if (rtn == 1)
                {
                    Hashtable hash = new Hashtable();
                    hash["error"] = 0;
                    hash["url"] = reUrl;

                    text = JsonMapper.ToJson(hash);
                }

            }


            base.AjaxErch(text);

        }

        private bool CheckType(string str)
        {
            string t = str.Replace(".", "");
            string text = ConfigServiceEx.baseConfig["alowFileType"].Replace("'", "");
            string[] te = text.Split(',');
            for (int i = 0; i < te.Length; i++)
            {
                if (te[i].ToLower() == t.ToLower()) return true;
            }
            return false;
        }
    }
}