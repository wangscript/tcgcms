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


using TCG.Data;
using TCG.Entity;
using TCG.Handlers;


public partial class skin_categorieslist : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

        if (!Page.IsPostBack)
        {
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

        }
    }

    private void SearchInit()
    {
        string iParent = objectHandlers.Get("iParentId");
        this.iClassId.Value = iParent;
        string skinid = objectHandlers.Get("skinid");
        if (string.IsNullOrEmpty(skinid))
        {
            skinid = ConfigServiceEx.DefaultSkinId;
        }
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
            return;
        }

        int rtn = 0; 
        try
        {
            rtn = base.handlerService.skinService.categoriesHandlers.DelCategories(tClassID,base.adminInfo);
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

    }

    private void NewsClassCreate()
    {
        string tClassID = objectHandlers.Post("DelClassId");
        string classbackstr = "CreateBack";
        string text = string.Empty;
        int page = objectHandlers.ToInt(objectHandlers.Post("iPage"));
        int rtn = 0;
        try
        {

            if (string.IsNullOrEmpty(tClassID))
            {
                rtn = -1000000801;
            }
            else
            {
                Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(tClassID);

                if (cif == null)
                {
                    rtn = -1000000802;
                }
                else
                {
                    if (cif.ResourceListTemplate == null)
                    {
                        rtn = -1000000803;
                    }
                    else
                    {
                        if (cif.vcUrl.IndexOf(".") > -1)
                        {
                            rtn = -1000000804;
                        }
                        else
                        {
                            string filepath = "";
                            filepath = HttpContext.Current.Server.MapPath("~" + cif.vcUrl + ConfigServiceEx.baseConfig["FileExtension"]);

                            TCGTagHandlers tcgthdl = base.handlerService.tagService.tcgTagHandlers;
                            tcgthdl.Template = cif.ResourceListTemplate.Content.Replace("_$ClassId$_", tClassID.ToString());
                            tcgthdl.FilePath = filepath;
                            tcgthdl.WebPath = cif.vcUrl + ConfigServiceEx.baseConfig["FileExtension"];
                            tcgthdl.PagerInfo.DoAllPage = false;
                            tcgthdl.PagerInfo.Page = page;
                            tcgthdl.PagerInfo.PageSep = page <= 0 ? 0 : 1;

                            if (tcgthdl.Replace())
                            {
                                rtn = 1;
                                text = tcgthdl.PagerInfo.CreatePagesNotic.Replace("\\", "/");

                                if (tcgthdl.PagerInfo.PageCount > page)
                                {
                                    classbackstr = "CreateBack1";
                                }
                            }
                            else
                            {
                                rtn = -1000000805;
                            }

                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, "<a>" + ex.Message.ToString() + "</a>", classbackstr);
            return;
        }

        base.AjaxErch(1, text, classbackstr);
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
            int rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategories(base.adminInfo, cif);
        }

        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
        base.AjaxErch(1, "", "NewsSMDYPostBack");
        cif = null;

    }
}