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


using TCG.Entity;
using TCG.Handlers;
using TCG.Data;

public partial class Template_templateadd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

        if (!Page.IsPostBack)
        {
            string SkinId = objectHandlers.Get("iSkinId");
            string Parentid = objectHandlers.Get("iParentid");
            int iSytemType = objectHandlers.ToInt(objectHandlers.Get("SytemType"));

            this.iParentid.Value = Parentid;
            this.SytemType.Value = iSytemType.ToString();
            this.iSiteId.Value = SkinId;

            Template temp = base.handlerService.skinService.templateHandlers.GetTemplateByID(Parentid);
            
            if (temp != null)
            {
                string filepatch = base.handlerService.skinService.templateHandlers.GetTemplatePagePatch(Parentid);
                this.parentPath.Value = filepatch + "/";
            }
            else
            {
                this.parentPath.Value = "/";
            }


            //列表模板添加
            if (Parentid == "1")
            {
                foreach (Option option in ConfigServiceEx.templateTypes.Values)
                {
                    if (option.Value == "1")
                    {
                        this.tType.Items.Add(new ListItem(option.Text, option.Value));
                        this.tType.Value = option.Value;
                    }
                }
            }
            else if (Parentid =="0")//跟目录
            {
                foreach (Option option in ConfigServiceEx.templateTypes.Values)
                {
                    if (option.Value == "0" || option.Value == "5")
                    {
                        this.tType.Items.Add(new ListItem(option.Text, option.Value));
                    }
                }
            }
            else if (Parentid == "2")  // 详细页
            {
                foreach (Option option in ConfigServiceEx.templateTypes.Values)
                {
                    if (option.Value == "2")
                    {
                        this.tType.Items.Add(new ListItem(option.Text, option.Value));
                        this.tType.Value = option.Value;
                    }
                }
            }
            else if (Parentid == "3")  // 原件
            {
                foreach (Option option in ConfigServiceEx.templateTypes.Values)
                {
                    if (option.Value == "3")
                    {
                        this.tType.Items.Add(new ListItem(option.Text, option.Value));
                        this.tType.Value = option.Value;
                    }
                }
            }
            else
            {
                foreach (Option option in ConfigServiceEx.templateTypes.Values)
                {
                    if (option.Value == "0" || option.Value == "5")
                    {
                        this.tType.Items.Add(new ListItem(option.Text, option.Value));
                    }
                }
            }

        }
        else
        {
            Template item = new Template();
            item.vcTempName = objectHandlers.Post("vcTempName");
            item.TemplateType = base.handlerService.skinService.templateHandlers.GetTemplateType(objectHandlers.ToInt(objectHandlers.Post("tType")));
            item.iParentId = objectHandlers.Post("iParentid");
            item.iSystemType = objectHandlers.ToInt(objectHandlers.Post("SytemType"));
            item.vcUrl = objectHandlers.Post("vcUrl");
            item.Content = objectHandlers.Post("vcContent");
            item.SkinInfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId( objectHandlers.Post("iSiteId"));

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
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.templateHandlers.AddTemplate(item,base.adminInfo);
                rtn = base.handlerService.skinService.templateHandlers.CreateTemplateToXML(base.adminInfo,item.SkinInfo.Id);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES_ENTITY);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            base.AjaxErch(rtn,"模板添加成功！");
            item = null;
        }
    }
}
