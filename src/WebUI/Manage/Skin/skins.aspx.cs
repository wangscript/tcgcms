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

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;

using TCG.Handlers;
using TCG.Entity;

public partial class Skin_skins : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

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
                if (filename == "skin.txt")
                {
                    string skintext = TCG.Utils.TxtReader.ReadW(path);
                    if (!string.IsNullOrEmpty(skintext))
                    {
                        string[] skininfo = skintext.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        if (skininfo.Length >= 4)
                        {
                            Skin skin = new Skin();
                            skin.Id = skininfo[0];
                            skin.Info = skininfo[3];
                            skin.Name = skininfo[1];
                            skin.Pic = skininfo[2];
                            skin.Filename = finfo.Name;
                            int rtn = base.handlerService.skinService.skinHandlers.CreateSkin(skin);
                        }
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
            base.configService.UpdateConfig(base.configService.m_skinConfig, "DefaultSkinId", "Value", SkinId);
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

    private void CreateSkinSql()
    {
        string SkinId = objectHandlers.Post("SkinId");

        Skin skininfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(SkinId);

        if (skininfo == null)
        {
            base.AjaxErch(-1000000701, "");
            return;
        }

        //得到所有模板
        StringBuilder sbtemplate = new StringBuilder();
        sbtemplate.Append("<?xml version=\"1.0\"?>\r\n");
        Dictionary<string, EntityBase> templates = base.handlerService.skinService.templateHandlers.GetAllTemplatesEntity();
        if (templates != null && templates.Count > 0)
        {
            foreach (KeyValuePair<string, EntityBase> entity in templates)
            {
                Template temp = (Template)entity.Value;
                if (temp.SkinId == SkinId)
                {
                    sbtemplate.Append("<Template>\r\n");
                    sbtemplate.Append("\t<ID>" + temp.Id + "</ID>\r\n");
                    sbtemplate.Append("\t<Content>" + temp.Content + "</Content>\r\n");
                    sbtemplate.Append("\t<SkinId>" + temp.SkinId + "</SkinId>\r\n");
                    sbtemplate.Append("\t<TemplateType>" + ((int)temp.TemplateType).ToString() + "</TemplateType>\r\n");
                    sbtemplate.Append("\t<iParentId>" + temp.iParentId + "</iParentId>\r\n");
                    sbtemplate.Append("\t<iSystemType>" + temp.iSystemType + "</iSystemType>\r\n");
                    sbtemplate.Append("\t<dUpdateDate>" + temp.dUpdateDate + "</dUpdateDate>\r\n");
                    sbtemplate.Append("\t<dAddDate>" + temp.dAddDate + "</dAddDate>\r\n");
                    sbtemplate.Append("\t<vcTempName>" + temp.vcTempName + "</vcTempName>\r\n");
                    sbtemplate.Append("\t<vcUrl>" + temp.vcUrl + "</vcUrl>\r\n");
                    sbtemplate.Append("</Template>\r\n");
                }
            }
        }

        //得到所有模板
        StringBuilder sbcategories = new StringBuilder();
        sbcategories.Append("<?xml version=\"1.0\"?>\r\n");
        Dictionary<string, EntityBase> categories = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntity();
        if (templates != null && templates.Count > 0)
        {
            foreach (KeyValuePair<string, EntityBase> entity in categories)
            {
                Categories temp = (Categories)entity.Value;
                if (temp.SkinId == SkinId)
                {
                    sbcategories.Append("<Categorie>\r\n");
                    sbcategories.Append("\t<ID>" + temp.Id + "</ID>\r\n");
                    sbcategories.Append("\t<Parent>" + temp.Parent + "</Parent>\r\n");
                    sbcategories.Append("\t<ResourceTemplate>" + temp.ResourceTemplate.Id + "</ResourceTemplate>\r\n");
                    sbcategories.Append("\t<ResourceListTemplate>" + temp.ResourceListTemplate.Id + "</ResourceListTemplate>\r\n");
                    sbcategories.Append("\t<iOrder>" + temp.iOrder.ToString() + "</iOrder>\r\n");
                    sbcategories.Append("\t<dUpdateDate>" + temp.dUpdateDate.ToString() + "</dUpdateDate>\r\n");
                    sbcategories.Append("\t<dUpdateDate>" + temp.dUpdateDate + "</dUpdateDate>\r\n");
                    sbcategories.Append("\t<vcClassName>" + temp.vcClassName + "</vcClassName>\r\n");
                    sbcategories.Append("\t<vcName>" + temp.vcName + "</vcName>\r\n");
                    sbcategories.Append("\t<vcDirectory>" + temp.vcDirectory + "</vcDirectory>\r\n");
                    sbcategories.Append("\t<vcUrl>" + temp.vcUrl + "</vcUrl>\r\n");
                    sbcategories.Append("\t<cVisible>" + temp.cVisible + "</cVisible>\r\n");
                    sbcategories.Append("\t<DataBaseService>" + temp.DataBaseService + "</DataBaseService>\r\n");
                    sbcategories.Append("\t<SkinId>" + temp.SkinId + "</SkinId>\r\n");
                    sbcategories.Append("</Categorie>\r\n");
                }
            }
        }

        try
        {
            objectHandlers.SaveFile(Server.MapPath("~/skin/" + skininfo.Filename + "/template.config"), sbtemplate.ToString());
            objectHandlers.SaveFile(Server.MapPath("~/skin/" + skininfo.Filename + "/categories.config"), sbcategories.ToString());
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(1, "模板导出成功！");
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

        info.Text = skininfo.Info;
        sitename.Text = skininfo.Name;
        pic.Src = skininfo.Pic;
        sitename.Href = "skinveiw.aspx?skinid=" + skininfo.Id;
        if (skininfo.Id == base.configService.DefaultSkinId)
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
    }
}

