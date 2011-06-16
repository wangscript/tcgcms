using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Utils;
using TCG.Entity;
using TCG.Handlers;

namespace TCG.CMS.WebUi
{
    public partial class Manage_Common_ResourcesProperties : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rid = objectHandlers.Get("nid");
            Response.ContentType = "application/x-javascript";
            string cg = base.handlerService.GetJsEntitys(base.handlerService.resourcsService.resourcesHandlers.GetResourcePropertiesByRIdEntity(rid), typeof(ResourceProperties));
            Response.Write(cg);
            Response.End();
        }
    }
}