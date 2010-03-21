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
using System.Text;

using TCG.Utils;
using TCG.Entity;
using TCG.Pages;
using TCG.Handlers;

public partial class attach : NewsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            if (objectHandlers.IsGetFromAnotherDomain || objectHandlers.IsPostFromAnotherDomain)
            {
                return;
            }

            string iId = objectHandlers.Get("attach");
            if (string.IsNullOrEmpty(iId))
            {
                return;
            }

            
            FileResources item = base.handlerService.fileService.fileInfoHandlers.GetFileInfosById( iId);

            string filename = item.Id + item.vcType;
            string path = base.handlerService.fileService.fileInfoHandlers.GetFilePath(item.vcFileName,item.iClassId);
            string str7 = "";
            switch (item.vcType)
            {
                case ".gif":
                    str7 = "image/gif";
                    break;

                case ".png":
                    str7 = "image/png";
                    break;

                case ".bmp":
                    str7 = "image/bmp";
                    break;

                case ".jpg":
                    str7 = "image/jpeg";
                    break;
                case ".jpe":
                    str7 = "image/jpeg";
                    break;
                case ".jpeg":
                    str7 = "image/jpeg";
                    break;

                case ".swf":
                    str7 = "application/x-shockwave-flash";
                    break;

                default:
                    str7 = "APPLICATION/OCTET-STREAM";
                    break;
            }
            base.Finish();
            base.Response.ContentType = str7;
            if ((item.vcType != "swf"))
            {
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, Encoding.UTF8));
            }
            base.Response.WriteFile(path);
        }
    }
}