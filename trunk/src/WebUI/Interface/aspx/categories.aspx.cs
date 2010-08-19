using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TCG.Pages;
using TCG.Utils;
using TCG.Entity;

public partial class Interface_aspx_categories : Origin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string work = objectHandlers.Get("w");
            switch (work)
            {
                case "getallcategories" :
                    this.GetAllCategories();
                    break;
            }
        }
    }

    private void GetAllCategories()
    {
        Response.ContentType = "application/x-javascript";
        string cg = base.handlerService.GetJsEntitys(base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntitySkinId(base.configService.DefaultSkinId), typeof(Categories));
        Response.Write(cg);
        Response.End();
    }
}