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
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;

using TCG.Data;
using TCG.Entity;
using TCG.Handlers;


public partial class skin_categorieslist : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            this.SearchInit();
        }
        else
        {
            string work = objectHandlers.Post("work");
            switch (work)
            {
                case "DEL":
                    this.NewsClassDel();
                    break;
                case "Create":
                    this.NewsClassCreate();
                    break;
            }


            string action = objectHandlers.Post("iAction");
            switch (action)
            {
                
                case "MDY":
                    this.OrderMdy();
                    break;   
            }

            base.Finish();

        }
    }

    private void SearchInit()
    {
        string iParent = objectHandlers.Get("iParentId");
        this.iClassId.Value = iParent;
        string skinid = objectHandlers.Get("skinid");
        this.iSkinId.Value = skinid;
        if (string.IsNullOrEmpty(iParent)) iParent = "0";
        Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentId(iParent, skinid);

        if (allcategories != null)
        {
            this.ItemRepeater.DataSource = allcategories.Values;
            this.ItemRepeater.DataBind();
        }
        
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Categories categorie = (Categories)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Span classname = (Span)e.Item.FindControl("classname");
        Span lname = (Span)e.Item.FindControl("lname");
        Anchor directory = (Anchor)e.Item.FindControl("directory");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span sOrder = (Span)e.Item.FindControl("sOrder");

        CheckID.Text = categorie.Id;

        sOrder.Text = categorie.iOrder.ToString();

        string text = "<a href=\"?iParentId=" + categorie.Id + "&skinid=" + objectHandlers.Get("skinid") + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
        classname.Text = text + categorie.vcClassName;
        lname.Text = "<a href='?iClassId=" + categorie.Id + "&skinid=" + objectHandlers.Get("skinid") + "' title='资讯列表'><img src='../images/icon/09.gif'></a>"
            + categorie.vcName;
        directory.Text = categorie.vcDirectory;
        updatedate.Text = categorie.dUpdateDate.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void NewsClassDel()
    {
        string tClassID = objectHandlers.Post("DelClassId");
        if (string.IsNullOrEmpty(tClassID ))
        {
            base.AjaxErch(-1,"");
            base.Finish();
            return;
        }

        int rtn = 0; 
        try
        {
            rtn = base.handlerService.skinService.categoriesHandlers.DelCategories(tClassID,base.adminInfo.vcAdminName);
            CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
            CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "分类删除成功", "refinsh()");
        base.Finish();

    }

    private void NewsClassCreate()
    {
        string tClassID = objectHandlers.Post("DelClassId");
        string text = string.Empty;
        int rtn = 0;
        try
        {
            rtn = base.handlerService.skinService.categoriesHandlers.CreateCategoriesListHtml(tClassID, base.tagService.TCGTagHandlers, ref text);
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, "<a>" + ex.Message.ToString() + "</a>", "CreateBack");
            base.Finish();
            return;
        }

        base.AjaxErch(rtn, text, "CreateBack");
    }

    private void OrderMdy()
    {
        string KeyValue = objectHandlers.Post("KeyValue");  
        string iFeildName = objectHandlers.Post("iFeildName");
        string iMdyID = objectHandlers.Post("iMdyID");

        Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(iMdyID);
        if (cif != null)
        {
            cif.iOrder = objectHandlers.ToInt(KeyValue);
            int rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategories(cif);
        }

        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
        base.AjaxErch(1, "", "NewsSMDYPostBack");
        base.Finish();
        cif = null;

    }
}