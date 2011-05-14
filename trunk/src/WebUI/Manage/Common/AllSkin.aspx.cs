using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Utils;
using TCG.Handlers;

using TCG.Entity;

public partial class Manage_Common_AllSkin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.ContentType = "application/x-javascript";
            string cg = base.handlerService.GetJsEntitys(base.handlerService.skinService.skinHandlers.GetAllSkinEntity(), typeof(Skin));
            Response.Write(cg);
            Response.End();
        }
    }
}
