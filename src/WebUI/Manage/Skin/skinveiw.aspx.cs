using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;

using TCG.Handlers;
using TCG.Entity;

public partial class Manage_Skin_skinveiw : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


           
        }
    }


    private void Loadcategories()
    {
        string skinid = objectHandlers.Get("skinid");

        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
        //检测管理员登录
        base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

        StringBuilder tempashtml = new StringBuilder();

        Dictionary<string, EntityBase> cages = base.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentId("0", skinid);

        if (cages != null)
        {
            tempashtml.Append(cages.Count.ToString());
        }

        base.AjaxErch("{state:false,message:\"" + objectHandlers.JSEncode(tempashtml.ToString()) + "\"}");
    }
}