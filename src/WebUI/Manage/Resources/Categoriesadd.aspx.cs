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

public partial class resources_categoriesadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Init();
        }
        else
        {
            Categories cif = new Categories();
            cif.vcClassName = objectHandlers.Post("iClassName");
            cif.vcName = objectHandlers.Post("iName");
            cif.vcDirectory = objectHandlers.Post("iDirectory");
            cif.vcUrl = objectHandlers.Post("iUrl");
            cif.iParent = objectHandlers.ToInt(objectHandlers.Post("iClassId"));
            cif.iTemplate = objectHandlers.Post("sTemplate");
            cif.iListTemplate = objectHandlers.Post("slTemplate");
            cif.iOrder = objectHandlers.ToInt(objectHandlers.Post("iOrder"));
            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            if (cif.iParent != 0)
            {
                if (string.IsNullOrEmpty(cif.iTemplate) || string.IsNullOrEmpty(cif.iListTemplate))
                {
                    base.AjaxErch("-1");
                    base.Finish();
                    return;
                }
            }

            int rtn = base.handlerService.newsClassHandlers.CreateCategories(base.conn,cif, base.adminInfo.vcAdminName);
            CachingService.Remove("AllNewsClass");
            base.AjaxErch(rtn.ToString());

            base.Finish();
        }
    }

    private void Init()
    {
        int iParent = objectHandlers.ToInt(objectHandlers.Get("iParentId"));
        this.iClassId.Value = iParent.ToString();

        DataSet ds = base.handlerService.templateHandlers.GetTemplatesBySystemTypAndType(base.conn,
            (int)TemplateType.InfoType, 0, false);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    this.sTemplate.Items.Add(new ListItem(Row["vcTempName"].ToString(), Row["Id"].ToString()));
                }
            }
            
            ds.Clear();
        }

        
        ds = base.handlerService.templateHandlers.GetTemplatesBySystemTypAndType(base.conn,
            (int)TemplateType.ListType, 0, false);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    this.slTemplate.Items.Add(new ListItem(Row["vcTempName"].ToString(), Row["Id"].ToString()));
                }
            }
        }
    }
}
