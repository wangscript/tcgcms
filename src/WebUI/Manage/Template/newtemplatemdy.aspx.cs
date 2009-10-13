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
using TCG.Template.Handlers;
using TCG.Template.Entity;
using TCG.Template.Utils;
using TCG.Data;

public partial class Template_newtemplatemdy : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TemplateHandlers nthdl = new TemplateHandlers();
        int templateid = Bases.ToInt(Fetch.Get("templateid"));
        
        if (!Page.IsPostBack)
        {
            TemplateInfo item = nthdl.GetTemplateInfoByID(base.conn, templateid,false);
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
            for (int i = 0; i < TemplateConstant.TypeNames().Count; i++)
            {
                this.tType.Items.Add(new ListItem(TemplateConstant.TypeNames()[i].ToString(), i.ToString()));
                if (i == item.iType)
                {
                    this.tType.SelectedIndex = i;
                }
            }
            item = null;
            nthdl = null;
            base.Finish();
        }
        else
        {
            TemplateInfo item = new TemplateInfo();
            item.vcTempName = Fetch.Post("vcTempName");
            item.iType = Bases.ToInt(Fetch.Post("tType"));
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
            item.iId = templateid;
            int rtn = nthdl.MdyTemplate(base.conn, base.admin.adminInfo.vcAdminName,item);
            Caching.Remove(TemplateConstant.CACHING_AllTemplates);
            base.AjaxErch(rtn.ToString());
            nthdl = null;
            base.Finish();
        }
    }
}
