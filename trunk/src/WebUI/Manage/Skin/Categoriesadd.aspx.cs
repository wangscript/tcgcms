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

public partial class skin_categoriesadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            this.Init();
        }
        else
        {
            Categories cif = new Categories();
            cif.vcClassName = objectHandlers.Post("iClassName");
            cif.vcName = objectHandlers.Post("iName");
            cif.vcDirectory = objectHandlers.Post("iDirectory");
            cif.vcUrl = objectHandlers.Post("iUrl");
            cif.Parent = objectHandlers.Post("iClassId");
            cif.ResourceTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("sTemplate"));
            cif.ResourceListTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("slTemplate"));
            cif.iOrder = objectHandlers.ToInt(objectHandlers.Post("iOrder"));
            cif.SkinId = objectHandlers.Post("iSkinId");
            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            if (string.IsNullOrEmpty(cif.Parent))
            {
                if (cif.ResourceTemplate == null || cif.ResourceListTemplate==null)
                {
                    base.AjaxErch("-1");
                    base.Finish();
                    return;
                }
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.categoriesHandlers.CreateCategories(cif);
                CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
                CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            base.AjaxErch(rtn, "分类添加成功！");
            base.Finish();
        }
    }

    private void Init()
    {
        string iParent = string.IsNullOrEmpty(objectHandlers.Get("iParentId")) ? "0" : objectHandlers.Get("iParentId");
        this.iClassId.Value = iParent.ToString();
        this.iSkinId.Value = objectHandlers.Get("SkinId");

        Dictionary<string, EntityBase> templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.InfoType);
        if (templates != null && templates.Count != 0)
        {
            foreach (KeyValuePair<string, EntityBase> keyvalue in templates)
            {
                Template template = (Template)keyvalue.Value;
                this.sTemplate.Items.Add(new ListItem(template.vcTempName, template.Id));
            }
        }


        templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.ListType);
        if (templates != null && templates.Count != 0)
        {
            foreach (KeyValuePair<string, EntityBase> keyvalue in templates)
            {
                Template template = (Template)keyvalue.Value;
                this.slTemplate.Items.Add(new ListItem(template.vcTempName, template.Id));
            }
        }
    }
}
