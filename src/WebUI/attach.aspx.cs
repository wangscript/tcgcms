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
using TCG.Handlers;


public partial class attach : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string filename = "nopic.jpg";
            string path = Server.MapPath("~/images/nopic.jpg");
            string str7 = ".jpg";

            if (!(objectHandlers.IsGetFromAnotherDomain || objectHandlers.IsPostFromAnotherDomain))
            {
                string iId = objectHandlers.Get("id");
                if (!string.IsNullOrEmpty(iId))
                {
                    FileResources item = base.handlerService.fileService.fileInfoHandlers.GetFileInfosById(iId);

                    if (item != null)
                    {
                        filename = item.Id + item.vcType;
                        path = base.handlerService.fileService.fileInfoHandlers.GetFilePath(filename, item.iClassId);

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
                    }
                }
            }

            base.Finish();
            base.Response.ContentType = str7;
            if ((str7 != ".swf"))
            {
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, Encoding.UTF8));
            }
            base.Response.WriteFile(path);
        }
    }
}