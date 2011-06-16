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

using TCG.Entity;

using TCG.Handlers;
using TCG.Data;


namespace TCG.CMS.WebUi
{
    public partial class Template_templatelist : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(27);

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
                        this.TemplateDel();
                        break;
                    case "Create":
                        this.TemplateCreate();
                        break;
                }
            }
        }

        private void SearchInit()
        {


            string iParentid = objectHandlers.Get("iParentid");
            iParentid = iParentid.Trim().Length == 0 ? "0" : iParentid;

            this.iParentid.Value = iParentid;

            string SkinId = objectHandlers.Get("SkinId");
            if (string.IsNullOrEmpty(SkinId))
            {
                SkinId = ConfigServiceEx.DefaultSkinId;
            }

            this.iSkinId.Value = SkinId;
            Skin skinentity = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(SkinId);
            this.classTitle.InnerHtml = "<a href='?SkinId=" + SkinId + "&iParentid=0'>" + skinentity.Name + "</a>";

            if (iParentid == "0")
            {
                this.systempagetemplate.Text = "<a href=\"?iParentid=1&skinid=" + SkinId + "\" title=\"详细页模板\">"
                    + "<img src=\"../images/icon/24.gif\" border=\"0\"></a>  详细页模板";

                this.systemlisttemplate.Text = "<a href=\"?iParentid=2&skinid=" + SkinId + "\" title=\"列表模板\">"
                    + "<img src=\"../images/icon/24.gif\" border=\"0\"></a> 列表模板";

                this.systemctrltemplte.Text = "<a href=\"?iParentid=3&skinid=" + SkinId + "\" title=\"原件模板\">"
                    + "<img src=\"../images/icon/24.gif\" border=\"0\"></a> 原件模板";
            }
            else
            {
                this.systemlisttemplateDiv.Visible = false;
                this.systempagetemplateDiv.Visible = false;
                this.systemctrltemplteDiv.Visible = false;
            }

            //文件夹
            string type = objectHandlers.Get("iType");
            int iType = objectHandlers.ToInt(objectHandlers.Get("iType"));
            if (type.Trim().Length == 0) iType = -1;

            Dictionary<string, EntityBase> filetemplates = base.handlerService.skinService.templateHandlers.GetTemplates(SkinId, iParentid, new int[] { (int)TemplateType.Folider });

            if (filetemplates != null && filetemplates.Count != 0)
            {
                this.ItemRepeater1.DataSource = filetemplates.Values;
                this.ItemRepeater1.DataBind();
            }


            //单页
            Dictionary<string, EntityBase> pagetemplates = base.handlerService.skinService.templateHandlers.GetTemplates(SkinId, iParentid, new int[] { (int)TemplateType.SinglePageType,
            (int)TemplateType.ListType,(int)TemplateType.InfoType,(int)TemplateType.OriginalType });

            if (pagetemplates != null && pagetemplates.Count != 0)
            {
                this.ItemRepeater.DataSource = pagetemplates.Values;
                this.ItemRepeater.DataBind();
            }

        }

        protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Template template = (Template)e.Item.DataItem;
            Span CheckID = (Span)e.Item.FindControl("CheckID");
            Span sId = (Span)e.Item.FindControl("sId");
            Span classname = (Span)e.Item.FindControl("classname");
            Span updatedate = (Span)e.Item.FindControl("updatedate");


            string SkinId = objectHandlers.Get("SkinId");
            if (string.IsNullOrEmpty(SkinId))
            {
                SkinId = ConfigServiceEx.DefaultSkinId;
            }
            CheckID.Text = template.Id;
            sId.Text = template.Id;

            string text = "<a href=\"templatemdy.aspx?templateid=" + template.Id + "\" title=\"修改" + template.vcTempName + "\">"
                + "<img src=\"../images/icon/f_shtml.gif\" border=\"0\"></a>";
            classname.Text = text + template.vcTempName;
            updatedate.Text = (template.dUpdateDate).ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected void ItemRepeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Template template = (Template)e.Item.DataItem;
            Span CheckID = (Span)e.Item.FindControl("CheckID");
            Span sId = (Span)e.Item.FindControl("sId");
            Span classname = (Span)e.Item.FindControl("classname");
            Span updatedate = (Span)e.Item.FindControl("updatedate");


            string SkinId = objectHandlers.Get("SkinId");
            if (string.IsNullOrEmpty(SkinId))
            {
                SkinId = ConfigServiceEx.DefaultSkinId;
            }

            CheckID.Text = template.Id;
            sId.Text = template.Id;

            string text = "<a href=\"?iParentid=" + template.Id + "&skinid=" + SkinId + "\" title=\"" + template.vcTempName + "\">"
                    + "<img src=\"../images/icon/24.gif\" border=\"0\"></a>";
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
                rtn = base.handlerService.skinService.templateHandlers.DelTemplate(temps, base.adminInfo);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES_ENTITY);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            base.AjaxErch("{state:true,message:'模板删除成功！'}");
        }

        private void TemplateCreate()
        {
            string iTemplate = objectHandlers.Post("iTemplateId");
            string filepath = string.Empty;
            string text = string.Empty;
            int rtn = 0;
            try
            {
                rtn = base.handlerService.tagService.CreateSingeTemplateToHtml(iTemplate, ref filepath);
            }
            catch (Exception ex)
            {
                base.AjaxErch(1, "<a><font color='red'>" + ex.Message.ToString() + "</font></a>", "CreateBack");
                return;
            }

            if (rtn == 1)
            {
                text = "<a>生成成功：" + filepath + "</a>";
            }
            else
            {
                text = "<a>生成失败：" + errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]) + "</a>";
            }

            base.AjaxErch(rtn, text, "CreateBack");
        }
    }
}