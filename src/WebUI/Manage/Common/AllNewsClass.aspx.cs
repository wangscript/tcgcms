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

using TCG.Utils;
using TCG.Entity;
using TCG.Handlers;
using TCG.Pages;

public partial class Common_AllNewsClass : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var NewsLis = new Array();\r\n");

            Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntity();
            if (allcategories != null && allcategories.Count>0)
            {
                int i = 0;
                foreach (KeyValuePair<string, EntityBase> entity in allcategories)
                {
                    Categories tempcategories = (Categories)entity.Value;
                    Response.Write("NewsLis[" + i.ToString() + "]=[\"" + tempcategories.Id
                            + "\",\"" + tempcategories.Parent + "\",\"" + tempcategories.vcClassName + "\",\"" +
                            tempcategories.vcName + "\",\"" + tempcategories.iTemplate + "\"];\r\n");
                    i++;

                }
             
            }
        }
        Response.End();
        base.Finish();
    }
}
