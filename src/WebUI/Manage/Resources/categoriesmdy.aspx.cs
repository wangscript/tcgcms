using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
            cif.ResourceTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID( objectHandlers.Post("sTemplate"));
            cif.ResourceListTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("slTemplate"));
            cif.iOrder = objectHandlers.ToInt(objectHandlers.Post("iOrder"));

            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }


            if (cif.ResourceTemplate == null || cif.ResourceListTemplate == null)
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
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

        Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(iClassId);
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

        Dictionary<string, EntityBase> templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.InfoType);
        int i = 0;
        if (templates != null && templates.Count!=0)
        {
            i = 0;
            foreach(KeyValuePair<string,EntityBase> keyvalue in templates)
            {
                Template template = (Template)keyvalue.Value;
                this.sTemplate.Items.Add(new ListItem(template.vcTempName, template.Id));
                if (template.Id == cif.ResourceTemplate.Id)
                {
                    this.sTemplate.SelectedIndex = i+1;
                }
                i++;
            }
        }


        templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.ListType);
        if (templates != null && templates.Count != 0)
        {
            i = 0;
            foreach (KeyValuePair<string, EntityBase> keyvalue in templates)
            {
                Template template = (Template)keyvalue.Value;
                this.slTemplate.Items.Add(new ListItem(template.vcTempName, template.Id));
                if (template.Id == cif.ResourceTemplate.Id)
                {
                    this.slTemplate.SelectedIndex = i + 1;
                }
                i++;
            }
        }

        base.Finish();
    }
}
