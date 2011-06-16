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

using TCG.Handlers;

using TCG.Entity;

namespace TCG.CMS.WebUi
{

    public partial class skin_categoriesmdy : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(25);

            if (!Page.IsPostBack)
            {
                this.skinid.Value = objectHandlers.Get("skinid");
                this.Init();
            }
            else
            {

                string skinid = objectHandlers.Get("SkinId");
                if (string.IsNullOrEmpty(skinid)) skinid = ConfigServiceEx.DefaultSkinId;
                Categories cif = new Categories();
                cif.Id = objectHandlers.Get("iClassId");
                cif.vcClassName = objectHandlers.Post("iClassName");
                cif.vcName = objectHandlers.Post("iName");
                cif.vcDirectory = objectHandlers.Post("iDirectory");
                cif.SkinInfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(skinid);
                cif.vcUrl = objectHandlers.Post("iUrl");
                cif.Parent = objectHandlers.Post("iClassId");
                cif.ResourceTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("sTemplate"));
                cif.ResourceListTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("slTemplate"));
                cif.iOrder = objectHandlers.ToInt(objectHandlers.Post("iOrder"));
                cif.IsSinglePage = string.IsNullOrEmpty(objectHandlers.Post("iIsSinglePage")) ? "N" : "Y";
                cif.vcPic = objectHandlers.Post("iPic");
                cif.cVisible = objectHandlers.Post("sVisite");

                if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
                {
                    base.AjaxErch(-1, "");
                    return;
                }


                if (cif.ResourceTemplate == null || cif.ResourceListTemplate == null)
                {
                    base.AjaxErch(-1, "");
                    return;
                }

                int rtn = 0;
                try
                {
                    rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategories(base.adminInfo, cif);
                    if (rtn == 1)
                    {
                        rtn = base.handlerService.skinService.categoriesHandlers.CreateCategoriesToXML(base.adminInfo, cif.SkinInfo.Id);
                        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
                        CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
                    }
                }
                catch (Exception ex)
                {
                    base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                    base.AjaxErch(base.ajaxdata);
                    return;
                }

                base.AjaxErch(rtn, "分类修改成功！");
            }
        }

        private void Init()
        {
            string iClassId = objectHandlers.Get("iClassId");
            string skinid = objectHandlers.Get("SkinId");
            if (string.IsNullOrEmpty(skinid)) skinid = ConfigServiceEx.DefaultSkinId;
            Categories cif = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(iClassId);
            if (cif == null)
            {
                return;
            }

            this.iClassId.Value = cif.Parent.ToString();
            this.iClassName.Value = cif.vcClassName;
            this.iName.Value = cif.vcName;
            this.iUrl.Value = cif.vcUrl;
            this.iDirectory.Value = cif.vcDirectory;
            this.iOrder.Value = cif.iOrder.ToString();
            this.iIsSinglePage.Checked = cif.IsSinglePage == "Y" ? true : false;
            this.iPic.Value = cif.vcPic;
            this.sVisite.Value = cif.cVisible == "Y" ? "Y" : "N";

            Dictionary<string, EntityBase> templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.InfoType, skinid);
            int i = 0;
            if (templates != null && templates.Count != 0)
            {
                i = 0;
                foreach (KeyValuePair<string, EntityBase> keyvalue in templates)
                {
                    Template template = (Template)keyvalue.Value;
                    this.sTemplate.Items.Add(new ListItem(template.vcTempName, template.Id));
                    if (template.Id == cif.ResourceTemplate.Id)
                    {
                        this.sTemplate.SelectedIndex = i + 1;
                    }
                    i++;
                }
            }


            templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.ListType, skinid);
            if (templates != null && templates.Count != 0)
            {
                i = 0;
                foreach (KeyValuePair<string, EntityBase> keyvalue in templates)
                {
                    Template template = (Template)keyvalue.Value;
                    this.slTemplate.Items.Add(new ListItem(template.vcTempName, template.Id));
                    if (template.Id == cif.ResourceListTemplate.Id)
                    {
                        this.slTemplate.SelectedIndex = i + 1;
                    }
                    i++;
                }
            }
        }
    }
}