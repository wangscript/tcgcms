using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Data;
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Manage.Utils;

using TCG.Entity;
using TCG.Handlers;

public partial class files_ConentImgCheck : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string content = objectHandlers.Post("iContent$content");

            Response.Write(base.handlerService.fileService.fileInfoHandlers.ImgPatchInit(content, base.adminInfo.vcAdminName,
                objectHandlers.ToInt(base.configService.baseConfig["NewsFileClass"]), base.configService.baseConfig));
            base.Finish();
        }
    }
}