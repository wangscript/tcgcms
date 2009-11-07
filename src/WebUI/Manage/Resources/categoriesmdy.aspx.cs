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
            cif.Id = objectHandlers.Get("iClassId");
            cif.vcClassName = objectHandlers.Post("iClassName");
            cif.vcName = objectHandlers.Post("iName");
            cif.vcDirectory = objectHandlers.Post("iDirectory");
            cif.vcUrl = objectHandlers.Post("iUrl");
            cif.Parent = objectHandlers.Post("iClassId");
            cif.ResourceTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID( objectHandlers.Post("sTemplate"),false);
            cif.ResourceListTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("slTemplate"),false);
            cif.iOrder = objectHandlers.ToInt(objectHandlers.Post("iOrder"));

            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            if (string.IsNullOrEmpty(cif.Parent))
            {
                if (cif.ResourceTemplate == null || cif.ResourceListTemplate == null)
                {
                    base.AjaxErch("-1");
                    base.Finish();
                    return;
                }
            }
            int rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategories(cif);
            CachingService.Remove("AllNewsClass");
            base.AjaxErch(rtn.ToString());

            base.Finish();
        }
    }

    private void Init()
    {
        string iClassId = objectHandlers.Get("iClassId");

        Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById( iClassId);
        if (cif == null)
        {
            base.Finish();
            return;
        }

        this.iClassId.Value = cif.Parent.ToString();
        this.iClassName.Value = cif.vcClassName;
        this.iName.Value = cif.vcName;
        this.iUrl.Value = cif.vcUrl;
        this.iDirectory.Value = cif.vcDirectory;
        this.iOrder.Value = cif.iOrder.ToString();
        
        DataSet ds = base.handlerService.skinService.templateHandlers.GetTemplatesBySystemTypAndType(base.conn,
            (int)TemplateType.InfoType, 0, false);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    this.sTemplate.Items.Add(new ListItem(Row["vcTempName"].ToString(), Row["Id"].ToString()));
                    if (Row["Id"].ToString() == cif.ResourceTemplate.Id)
                    {
                        this.sTemplate.SelectedIndex = i+1;
                    }
                }
            }
            ds.Clear();
        }


        ds = base.handlerService.skinService.templateHandlers.GetTemplatesBySystemTypAndType(base.conn,
            (int)TemplateType.ListType, 0, false);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    this.slTemplate.Items.Add(new ListItem(Row["vcTempName"].ToString(), Row["Id"].ToString()));
                    if (Row["Id"].ToString() == cif.ResourceListTemplate.ToString())
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
