using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using TCG.Utils;
using TCG.Handlers;
using TCG.Pages;
using TCG.Entity;

public partial class Manage_Common_common : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "application/x-javascript";

        Response.Write("var ajaxicon = ['" + base.configService.baseConfig["ManagePath"] + "images/ajax-loader1.gif', '" + base.configService.baseConfig["ManagePath"]
            + "images/post_err.gif', '" + base.configService.baseConfig["ManagePath"] + "images/post_ok.gif'];\r\n");
        Response.End();
    }
}
