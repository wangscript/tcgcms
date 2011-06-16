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

using TCG.Entity;

namespace TCG.CMS.WebUi
{
    public partial class Manage_Common_common : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "application/x-javascript";

            Response.Write("var ajaxicon = ['" + ConfigServiceEx.baseConfig["ManagePath"] + "images/ajax-loader1.gif', '" + ConfigServiceEx.baseConfig["ManagePath"]
                + "images/post_err.gif', '" + ConfigServiceEx.baseConfig["ManagePath"] + "images/post_ok.gif'];\r\n");
            Response.End();
        }
    }
}