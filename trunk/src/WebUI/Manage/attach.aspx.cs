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
using TCG.Files.Utils;
using TCG.Entity;
using TCG.Pages;
using TCG.Handlers;

public partial class attach : FilesMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            if (Fetch.IsGetFromAnotherDomain || Fetch.IsPostFromAnotherDomain)
            {
                return;
            }

            long iId = Bases.ToLong(Fetch.Get("attach"));
            if (iId == 0)
            {
                return;
            }

            FileInfoHandlers flhdl = new FileInfoHandlers();
            FileClassHandlers fcldl = new FileClassHandlers();
            FileInfos item = flhdl.GetFileInfosById(base.conn, iId);

            string filename = item.iID.ToString() + "." + item.vcType;
            string path = fcldl.GetFilesPathByClassId(base.conn, item.iClassId) + item.iID.ToString().Substring(0, 6) + "/"
                        + item.iID.ToString().Substring(6, 2) + "/" + filename;
            string str7 = "";
            switch (item.vcType)
            {
                case "gif":
                    str7 = "image/gif";
                    break;

                case "png":
                    str7 = "image/png";
                    break;

                case "bmp":
                    str7 = "image/bmp";
                    break;

                case "jpg":
                case "jpe":
                case "jpeg":
                    str7 = "image/jpeg";
                    break;

                case "swf":
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
            base.Response.WriteFile(Server.MapPath("~" + path));
        }
    }
}