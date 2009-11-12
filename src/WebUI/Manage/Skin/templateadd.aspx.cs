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
using TCG.Pages;

using TCG.Entity;
using TCG.Handlers;
using TCG.Data;

public partial class Template_templateadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            int iSiteId = objectHandlers.ToInt(objectHandlers.Get("iSiteId"));
            int Parentid = objectHandlers.ToInt(objectHandlers.Get("iParentid"));
            int iSytemType = objectHandlers.ToInt(objectHandlers.Get("SytemType"));

            this.iParentid.Value = Parentid.ToString();
            this.SytemType.Value = iSytemType.ToString();
            this.iSiteId.Value = iSiteId.ToString();

            foreach (Option option in base.configService.templateTypes.Values)
            {
                this.tType.Items.Add(new ListItem(option.Text, option.Value));
            }

        }
        else
        {
            Template item = new Template();
            item.vcTempName = objectHandlers.Post("vcTempName");
            item.TemplateType = objectHandlers.GetTemplateType(objectHandlers.ToInt(objectHandlers.Post("tType")));
            item.iParentId = objectHandlers.Post("iParentid");
            item.iSystemType = objectHandlers.ToInt(objectHandlers.Post("SytemType"));
            item.vcUrl = objectHandlers.Post("vcUrl");
            item.Content = objectHandlers.Post("vcContent");
            item.SkinId = objectHandlers.Post("iSiteId");

            if (string.IsNullOrEmpty(item.vcTempName) || string.IsNullOrEmpty(item.Content))
            {
                base.ajaxdata = "{state:false,message:\"模板内容和名称不能为空！\"}";
                base.AjaxErch(base.ajaxdata);
                base.Finish();
                return;
            }
            if ((int)item.TemplateType == 0 && string.IsNullOrEmpty(item.vcUrl))
            {
                base.ajaxdata = "{state:false,message:\"" + errHandlers.GetErrTextByErrCode(-1000000024, base.configService.baseConfig["ManagePath"]) + "\"}";
                base.AjaxErch(base.ajaxdata);
                base.Finish();
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.templateHandlers.AddTemplate(item);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            base.AjaxErch("{state:true,message:'模板添加成功！'}");
            item = null;
            base.Finish();
        }
    }
}
