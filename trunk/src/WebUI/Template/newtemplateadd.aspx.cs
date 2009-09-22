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
using TCG.Template.Entity;
using TCG.Template.Handlers;
using TCG.Template.Utils;
using TCG.Data;

public partial class Template_newtemplateadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int iSiteId = Bases.ToInt(Fetch.Get("iSiteId"));
            int Parentid = Bases.ToInt(Fetch.Get("iParentid"));
            int iSytemType = Bases.ToInt(Fetch.Get("SytemType"));

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
            item.vcTempName = Fetch.Post("vcTempName");
            item.iType = Bases.ToInt(Fetch.Post("tType"));
            item.iParentId = Bases.ToInt(Fetch.Post("iParentid"));
            item.iSystemType = Bases.ToInt(Fetch.Post("SytemType"));
            item.vcUrl = Fetch.Post("vcUrl");
            item.vcContent = Fetch.Post("vcContent");
            item.iSiteId = Bases.ToInt(Fetch.Post("iSiteId"));
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
            TemplateHandlers nthdl = new TemplateHandlers();
            int rtn = nthdl.AddTemplate(base.conn, base.admin.adminInfo.vcAdminName,item);
            Caching.Remove(TemplateConstant.CACHING_AllTemplates);
            base.AjaxErch(rtn.ToString());
            item = null;
            nthdl = null;
            base.Finish();
        }
    }
}
