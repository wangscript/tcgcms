using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;

using TCG.Handlers;
using TCG.Entity;

public partial class Skin_skins : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();

            this.LoadSkinFiles();
            Dictionary<string, EntityBase> skins = base.handlerService.skinService.skinHandlers.GetAllSkinEntity();
            if (skins != null && skins.Count != 0)
            {
                this.ItemRepeater.DataSource = skins.Values;
                this.ItemRepeater.DataBind();
            }
        }
        else
        {
            string work = objectHandlers.Post("work");
            switch (work)
            {
                case "SetDefalutSkinId":
                    this.SetDefalutSkinId();
                    break;
                case "CreateSkinSql" :
                    this.CreateSkinSql();
                    break;
                case "UpdateSkin" :
                    this.UpdateSkin();
                    break;
            }
        }
    }

    private void LoadSkinFiles()
    {
        DirectoryInfo dinfo = new DirectoryInfo(Server.MapPath("~/skin"));

        FileSystemInfo[] fsinfos = dinfo.GetFileSystemInfos();

        foreach (FileSystemInfo fsinfo in fsinfos)
        {
            FileInfo finfo = new FileInfo(fsinfo.FullName);
            FileSystemInfo[] files = new DirectoryInfo(finfo.FullName).GetFileSystemInfos();
            foreach (FileSystemInfo file in files)
            {
                string path = file.FullName;
                string filename = file.Name;
                if (filename == "skin.config")
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(path);
                    XmlNodeList nodelist = document.GetElementsByTagName("Skin");
                    if (nodelist != null && nodelist.Count == 1)
                    {
                        Skin skin = new Skin();
                        skin.Id =  nodelist[0].SelectSingleNode("Id").InnerText.ToString();
                        skin.Info = nodelist[0].SelectSingleNode("Info").InnerText.ToString();
                        skin.Name = nodelist[0].SelectSingleNode("Name").InnerText.ToString();
                        skin.Pic = nodelist[0].SelectSingleNode("Pic").InnerText.ToString();
                        skin.Filename = finfo.Name;
                        int rtn = base.handlerService.skinService.skinHandlers.CreateSkin(skin);
                    }
                }
            }
        }
        CachingService.Remove(CachingService.CACHING_ALL_SKIN_ENTITY);
        CachingService.Remove(CachingService.CACHING_ALL_SKIN);
    }

    private void SetDefalutSkinId()
    {
        string SkinId = objectHandlers.Post("SkinId");
        try
        {
            ConfigServiceEx.UpdateConfig(ConfigServiceEx.m_skinConfig, "DefaultSkinId", "Value", SkinId);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(1, "启用模板成功", "LoginCkBack");
        return;
    }

    /// <summary>
    /// 导入皮肤操作
    /// </summary>
    private void UpdateSkin()
    {
        string SkinId = objectHandlers.Post("SkinId");

        int rtn = 0;
        try
        {
            CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
            CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
            rtn = base.handlerService.skinService.templateHandlers.UpdateTemplateFromXML(SkinId, base.adminInfo);
            rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategoriesFromXML(SkinId);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "皮肤导入成功！");
        return;
    }

    private void CreateSkinSql()
    {
        string SkinId = objectHandlers.Post("SkinId");
        int rtn = 0;

        try
        {
            rtn = base.handlerService.skinService.templateHandlers.CreateTemplateToXML(SkinId);
            rtn = base.handlerService.skinService.categoriesHandlers.CreateCategoriesToXML(SkinId);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "模板导出成功！");
        return;
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Skin skininfo = (Skin)e.Item.DataItem;
        Anchor sitename = (Anchor)e.Item.FindControl("sitename");
        Span info = (Span)e.Item.FindControl("info");
        Img pic = (Img)e.Item.FindControl("pic");
        HtmlGenericControl IsDefault = (HtmlGenericControl)e.Item.FindControl("IsDefault");
        Span sid = (Span)e.Item.FindControl("sid");
        Span vsid = (Span)e.Item.FindControl("vsid");
        Span vsid1 = (Span)e.Item.FindControl("vsid1");
        Span vsid2 = (Span)e.Item.FindControl("vsid2");
        Span sid1 = (Span)e.Item.FindControl("sid1");
        Span sid2 = (Span)e.Item.FindControl("sid2");
        

        info.Text = skininfo.Info;
        sitename.Text = skininfo.Name;
        pic.Src = skininfo.Pic;
        sitename.Href = "skinveiw.aspx?skinid=" + skininfo.Id;
        if (skininfo.Id == ConfigServiceEx.DefaultSkinId)
        {
            IsDefault.Visible = true;
        }
        else
        {
            IsDefault.Visible = false;
        }
        sid.Text = skininfo.Id;
        vsid.Text = skininfo.Id;
        vsid1.Text = skininfo.Id;
        vsid2.Text = skininfo.Id;
        sid1.Text = skininfo.Id;
        sid2.Text = skininfo.Id;
    }
}

