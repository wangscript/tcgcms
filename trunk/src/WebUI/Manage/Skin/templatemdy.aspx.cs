using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Controls.HtmlControls;


using TCG.Handlers;
using TCG.Entity;
using TCG.Data;

public partial class Template_templatemdy : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string templateid = objectHandlers.Get("templateid");

        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

        if (!Page.IsPostBack)
        {
            Template item = base.handlerService.skinService.templateHandlers.GetTemplateByID(templateid);
            if (item == null)
            {
                return;
            }

            string filepatch = base.handlerService.skinService.templateHandlers.GetTemplatePagePatch(templateid);
            this.parentPath.Value =  filepatch.Substring(0, filepatch.LastIndexOf(item.vcTempName));

            this.vcTempName.Value = item.vcTempName;
            this.vcContent.Value = item.Content.Replace("+", "&#43;");
            this.vcUrl.Value =  filepatch;

            this.iSiteId.Value = item.SkinInfo.Id;
            if (item.TemplateType == TemplateType.Folider)
            {
                this.iParentid.Value = item.iParentId.ToString();
            }
            else
            {
                this.parentdiv.Visible = false;
            }
            
            this.SytemType.Value = ((int)item.TemplateType).ToString();

            //无法修改父类
            if (item.TemplateType != TemplateType.Folider && item.TemplateType != TemplateType.SinglePageType)
            {
                this.iParentid.Disabled = true;
            }
            else
            {
                this.iParentid.Disabled = false;
            }

            foreach (Option option in ConfigServiceEx.templateTypes.Values)            
            {
                this.tType.Items.Add(new ListItem(option.Text, option.Value));
                int i = objectHandlers.ToInt(option.Value);
                if (i == (int)item.TemplateType)
                {
                    this.tType.SelectedIndex = i;
                }
            }
            item = null;
        }
        else
        {

            Template item = base.handlerService.skinService.templateHandlers.GetTemplateByID(templateid);
            item.vcTempName = objectHandlers.Post("vcTempName");

            item.TemplateType = base.handlerService.skinService.templateHandlers.GetTemplateType(objectHandlers.ToInt(this.tType.Value));
            item.vcUrl = objectHandlers.Post("vcUrl");
            item.Content = objectHandlers.Post("vcContent").Replace("&#43;","+");
            item.SkinInfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(objectHandlers.Post("iSiteId"));
            if (item.SkinInfo == null)
            {
                base.ajaxdata = "{state:false,message:\"模版所属皮肤未找到！\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            string parentid = objectHandlers.Post("iParentId");
            if (!string.IsNullOrEmpty(parentid))
            {
                item.iParentId = objectHandlers.Post("iParentId");
            }

            if (string.IsNullOrEmpty(item.iParentId)) item.iParentId = "0";

            if (string.IsNullOrEmpty(item.vcTempName) || string.IsNullOrEmpty(item.Content))
            {
                base.ajaxdata = "{state:false,message:\"模板内容和名称不能为空！\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            if ((int)item.TemplateType == 0 && string.IsNullOrEmpty(item.vcUrl))
            {
                base.ajaxdata = "{state:false,message:\"" + errHandlers.GetErrTextByErrCode(-1000000024, ConfigServiceEx.baseConfig["ManagePath"]) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;

            }
            item.Id = templateid;

            ///执行修改操作
            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.templateHandlers.MdyTemplate(item,base.adminInfo);
                rtn = base.handlerService.skinService.templateHandlers.CreateTemplateToXML(base.adminInfo,item.SkinInfo.Id);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES_ENTITY);
                CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
                CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }
            if (rtn < 0)
            {
                base.AjaxErch("{state:false,message:'" + errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]) + "'}");
            }
            else
            {
                bool create = false;
                if (item.TemplateType == TemplateType.SinglePageType)
                {

                    try
                    {
                        string path = string.Empty;
                        rtn = base.handlerService.tagService.CreateSingeTemplateToHtml(item.Id, ref path);
                    }
                    catch (Exception ex)
                    {
                        base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                        base.AjaxErch(base.ajaxdata);
                        return;
                    }

                    create = rtn == 1 ? true : false;

                }
                base.AjaxErch("{state:true,message:'" + ((create) ? "模板修改成功" : "模板修改失败") + "!'}");
            }
            

        }
    }
}
