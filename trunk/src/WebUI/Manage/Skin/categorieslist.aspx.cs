﻿using System;
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
        if (string.IsNullOrEmpty(tClassID))
        {
            base.AjaxErch(1, "<a>生成失败,分类ID为0!</a>", "CreateBack");
            base.Finish();
            return;
        }

        Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(tClassID);

        if (cif == null)
        {
            base.AjaxErch(1, "<a>生成失败,分类信息不存在!</a>", "CreateBack");
            base.Finish();
            return;
        }

        if (cif.ResourceListTemplate == null)
        {
            base.AjaxErch(1, "<a>生成失败,分类模版信息读取失败!</a>", "CreateBack");
            base.Finish();
            return;
        }

        if (cif.vcUrl.IndexOf(".") > -1)
        {
            base.AjaxErch(1, "<a>生成失败,为跳转地址，无需生成！</a>", "CreateBack");
            base.Finish();
            return;
        }

        string filepath = "";
        try
        {
            filepath = Server.MapPath("~" + cif.vcUrl + base.configService.baseConfig["FileExtension"]);
        }
        catch
        {
            base.AjaxErch(1, "<a>生成失败,分类保存路径读取失败!</a>", "CreateBack");
            base.Finish();
            return;
        }

        TCGTagHandlers tcgthdl = base.tagService.TCGTagHandlers;
        tcgthdl.Template = cif.ResourceListTemplate.Content.Replace("_$ClassId$_", tClassID.ToString());
        tcgthdl.FilePath = filepath;
        tcgthdl.WebPath = cif.vcUrl + base.configService.baseConfig["FileExtension"];
        tcgthdl.configService = base.configService;
        tcgthdl.conn = base.conn;


        string text1 = "";
        try
        {
            tcgthdl.Replace();
            if (tcgthdl.PagerInfo.PageCount > 1)
            {

                for (int i = 1; i <= tcgthdl.PagerInfo.PageCount; i++)
                {
                    string num = (i == 1) ? "" : i.ToString();
                    text1 += "<a>生成成功:" + cif.vcUrl + "-c" + num + base.configService.baseConfig["FileExtension"] + "...</a>";
                }
            }
            else
            {
                text1 = "<a>生成成功:" + cif.vcUrl + base.configService.baseConfig["FileExtension"] + "...</a>";
            }
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, objectHandlers.JSEncode(ex.Message.ToString()), "CreateBack");
        }

        base.AjaxErch(1, text1, "CreateBack");
    }

    private void OrderMdy()
    {
        string KeyValue = objectHandlers.Post("KeyValue");
        if (string.IsNullOrEmpty(KeyValue))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
        string iFeildName = objectHandlers.Post("iFeildName");
        if (string.IsNullOrEmpty(iFeildName))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        string iMdyID = objectHandlers.Post("iMdyID");
        if (string.IsNullOrEmpty(iMdyID))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(iMdyID);
        if (cif == null)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        cif.iOrder = objectHandlers.ToInt(KeyValue);

        int rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategories(cif);
        if (rtn < 0)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
    
        base.Finish();
        cif = null;

    }
}