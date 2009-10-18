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
using TCG.Manage.Utils;
using TCG.Entity;
using TCG.Handlers;
using TCG.Template.Utils;
using TCG.Data;

public partial class Template_newtemplateadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int iSiteId = objectHandlers.ToInt(objectHandlers.Get("iSiteId"));
            int Parentid = objectHandlers.ToInt(objectHandlers.Get("iParentid"));
            int iSytemType = objectHandlers.ToInt(objectHandlers.Get("SytemType"));

            this.iParentid.Value = Parentid.ToString();
            this.SytemType.Value = iSytemType.ToString();
            this.iSiteId.Value = iSiteId.ToString();

            for (int i = 0; i < TemplateConstant.TypeNames().Count; i++)
            {
                this.tType.Items.Add(new ListItem(TemplateConstant.TypeNames()[i].ToString(), i.ToString()));
            }
        }
        else
        {
            TemplateInfo item = new TemplateInfo();
            item.vcTempName = objectHandlers.Post("vcTempName");
            item.iType = objectHandlers.ToInt(objectHandlers.Post("tType"));
            item.iParentId = objectHandlers.ToInt(objectHandlers.Post("iParentid"));
            item.iSystemType = objectHandlers.ToInt(objectHandlers.Post("SytemType"));
            item.vcUrl = objectHandlers.Post("vcUrl");
            item.vcContent = objectHandlers.Post("vcContent");
            item.iSiteId = objectHandlers.ToInt(objectHandlers.Post("iSiteId"));
            if (string.IsNullOrEmpty(item.vcTempName) || string.IsNullOrEmpty(item.vcContent))
            {
                base.AjaxErch("-1");
                base.Finish();
            }
            if (item.iType == 0 && string.IsNullOrEmpty(item.vcUrl))
            {
                base.AjaxErch("-1000000024");
                base.Finish();
            }

            int rtn = base.handlerService.templateHandlers.AddTemplate(base.conn, base.adminInfo.vcAdminName,item);
            CachingService.Remove(TemplateConstant.CACHING_AllTemplates);
            base.AjaxErch(rtn.ToString());
            item = null;
            base.Finish();
        }
    }
}
