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
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Entity;

using TCG.Handlers;
using TCG.Data;

public partial class Template_templatelist : adminMain
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
                    this.TemplateDel();
                    break;
                case "Create":
                    this.TemplateCreate();
                    break;
            }
            base.Finish();
        }
    }

    private void SearchInit()
    {
        
        string iParentid = objectHandlers.Get("iParentid");
        iParentid = iParentid.Trim().Length == 0 ? "0" : iParentid;

        this.iParentid.Value = iParentid;

        string SkinId = objectHandlers.Get("SkinId");
        SkinId = SkinId.Trim().Length == 0 ? "0" : SkinId;

        this.iSkinId.Value = SkinId;
        Skin skinentity = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(SkinId);
        this.classTitle.InnerHtml = "<a href='?SkinId=" + SkinId + "&iParentid=0'>" + skinentity.Name + "</a>";


        string type = objectHandlers.Get("iType");
        int iType = objectHandlers.ToInt(objectHandlers.Get("iType"));
        if (type.Trim().Length == 0) iType = -1;

        Dictionary<string, EntityBase> templates = base.handlerService.skinService.templateHandlers.GetTemplates(SkinId, iParentid, iType);

        if (templates!=null&&templates.Count != 0)
        {
            this.ItemRepeater.DataSource = templates.Values;
            this.ItemRepeater.DataBind();
        }

        foreach (Option option in base.configService.templateTypes.Values)
        {
            this.sType.Items.Add(new ListItem(option.Text, option.Value));
            int i = objectHandlers.ToInt(option.Value);
            if (iType == i)
            {
                this.sType.SelectedIndex = i + 1;
            }
        }

        base.Finish();

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Template template = (Template)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Span sId = (Span)e.Item.FindControl("sId");
        Span classname = (Span)e.Item.FindControl("classname");
        Span updatedate = (Span)e.Item.FindControl("updatedate");


        string SkinId = objectHandlers.Get("SkinId");

        CheckID.Text = template.Id;
        sId.Text = template.Id;

        string text = "<a href=\"?iParentid=" + template.Id + "&SkinId=" + SkinId + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
        text += "<a href=\"templatemdy.aspx?templateid=" + template.Id + "\" title=\"修改模版\">"
            + "<img src=\"../images/icon/11.gif\" border=\"0\"></a>";
        classname.Text = text + template.vcTempName;
        updatedate.Text = (template.dUpdateDate).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void TemplateDel()
    {
        string temps = objectHandlers.Post("temps");
        if (string.IsNullOrEmpty(temps))
        {
            base.ajaxdata = "{state:false,message:\"需要删除的记录编号不能为空！\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        int rtn = 0;
        try
        {
            rtn = base.handlerService.skinService.templateHandlers.DelTemplate(temps,base.adminInfo);
            CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
            CachingService.Remove(CachingService.CACHING_All_TEMPLATES_ENTITY);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" +objectHandlers.JSEncode(  ex.Message.ToString() )+ "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch("{state:true,message:'模板删除成功！'}");
        base.Finish();
    }

    private void TemplateCreate()
    {
        string iTemplate = objectHandlers.Post("iTemplateId");
        string text = string.Empty;
        int rtn = 0;
        try
        {
            rtn = base.handlerService.skinService.templateHandlers.CreateSingeTemplateToHtml(iTemplate, base.tagService.TCGTagHandlers, ref text);
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, "<a><font color='red'>" + ex.Message.ToString() + "</font></a>", "CreateBack");
            base.Finish();
            return;
        }

        base.AjaxErch(rtn, text, "CreateBack");
    }
}
