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

public partial class resources_categoriesmdy : adminMain
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
            cif.iId = objectHandlers.ToInt(objectHandlers.Get("iClassId"));
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
                if (string.IsNullOrEmpty(cif.iTemplate) || string.IsNullOrEmpty(cif.iListTemplate ))
                {
                    base.AjaxErch("-1");
                    base.Finish();
                    return;
                }
            }
            int rtn = base.handlerService.newsClassHandlers.UpdateCategories(base.conn, base.adminInfo.vcAdminName,cif);
            CachingService.Remove("AllNewsClass");
            base.AjaxErch(rtn.ToString());

            base.Finish();
        }
    }

    private void Init()
    {
        int iClassId = objectHandlers.ToInt(objectHandlers.Get("iClassId"));

        Categories cif = base.handlerService.newsClassHandlers.GetCategoriesById(base.conn, iClassId,false);
        if (cif == null)
        {
            base.Finish();
            return;
        }

        this.iClassId.Value = cif.iParent.ToString();
        this.iClassName.Value = cif.vcClassName;
        this.iName.Value = cif.vcName;
        this.iUrl.Value = cif.vcUrl;
        this.iDirectory.Value = cif.vcDirectory;
        this.iOrder.Value = cif.iOrder.ToString();
        
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
                    if (Row["Id"].ToString() == cif.iTemplate.ToString())
                    {
                        this.sTemplate.SelectedIndex = i+1;
                    }
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
                    if (Row["Id"].ToString() == cif.iListTemplate.ToString())
                    {
                        this.slTemplate.SelectedIndex = i + 1;
                    }
                }
            }

            ds.Clear();
            ds.Dispose();
        }

        
        cif = null;

        base.Finish();
    }
}
