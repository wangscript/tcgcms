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

using TCG.Handlers;
using TCG.Entity;
using TCG.Data;

public partial class Template_newtemplatemdy : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int templateid = objectHandlers.ToInt(objectHandlers.Get("templateid"));
        
        if (!Page.IsPostBack)
        {
            TemplateInfo item = base.handlerService.templateHandlers.GetTemplateInfoByID(base.conn, templateid,false);
            if (item == null)
            {
                base.Finish();
                return;
            }

            this.vcTempName.Value = item.vcTempName;
            this.vcContent.Value = item.vcContent;
            this.vcUrl.Value = item.vcUrl;
            this.iSiteId.Value = item.iSiteId.ToString();
            this.iParentid.Value = item.iParentId.ToString();
            this.SytemType.Value = item.iSystemType.ToString();

            foreach (Option option in base.configService.templateTypes.Values)            
            {
                this.tType.Items.Add(new ListItem(option.Text, option.Value));
                int i = objectHandlers.ToInt(option.Value);
                if (i == item.iType)
                {
                    this.tType.SelectedIndex = i;
                }
            }
            item = null;
            base.Finish();
        }
        else
        {
            TemplateInfo item = new TemplateInfo();
            item.vcTempName = objectHandlers.Post("vcTempName");
            item.iType = objectHandlers.ToInt(objectHandlers.Post("tType"));
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
            item.iId = templateid;
            int rtn = base.handlerService.templateHandlers.MdyTemplate(base.conn, base.adminInfo.vcAdminName,item);
            CachingService.Remove(CachingService.CACHING_AllTemplates);
            base.AjaxErch(rtn.ToString());
            base.Finish();
        }
    }
}
