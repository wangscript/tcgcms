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
    public partial class Common_AllTemplates : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.ContentType = "application/x-javascript";
                string cg = base.handlerService.GetJsEntitys(base.handlerService.skinService.templateHandlers.GetAllTemplatesEntity(), typeof(Template));
                Response.Write(cg);
                Response.End();
            }
        }
    }

}