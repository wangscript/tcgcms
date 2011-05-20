using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Utils;
using TCG.Entity;
using TCG.Handlers;

public partial class Manage_Common_CategorieProperties : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string categorieid = objectHandlers.Get("cid");
        Response.ContentType = "application/x-javascript";
        string cg = base.handlerService.GetJsEntitys(base.handlerService.skinService.categoriesHandlers.GetCategoriePropertiesByCIdEntity(categorieid), typeof(CategorieProperties));
        Response.Write(cg);
        Response.End();
    }
}