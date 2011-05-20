﻿using System;
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

public partial class skin_categoriesmdy : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

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
            cif.ResourceTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID( objectHandlers.Post("sTemplate"));
            cif.ResourceListTemplate = base.handlerService.skinService.templateHandlers.GetTemplateByID(objectHandlers.Post("slTemplate"));
            cif.iOrder = objectHandlers.ToInt(objectHandlers.Post("iOrder"));
            cif.IsSinglePage = string.IsNullOrEmpty(objectHandlers.Post("iIsSinglePage")) ? "N" : "Y";

            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                base.AjaxErch(-1,"");
                return;
            }


            if (cif.ResourceTemplate == null || cif.ResourceListTemplate == null)
            {
                base.AjaxErch(-1,"");
                return;
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.categoriesHandlers.UpdateCategories(base.adminInfo, cif);
                if (rtn == 1)
                {
                    foreach (string key in Request.Form.AllKeys)
                    {
                        if (key.IndexOf("name_") > -1 && !string.IsNullOrEmpty(objectHandlers.Post(key)))
                        {
                            string[] keys = key.Split('_');
                            CategorieProperties cps = new CategorieProperties();
                            cps.Id = objectHandlers.Post("cpid_" + keys[1]);
                            cps.ProertieName = objectHandlers.Post(key);
                            cps.CategorieId = cif.Id;
                            cps.Type = objectHandlers.Post("type_" + keys[1]);
                            cps.Values = objectHandlers.Post("pttext_" + keys[1]);
                            cps.width = objectHandlers.ToInt(objectHandlers.Post("pwidth_" + keys[1]));
                            cps.height = objectHandlers.ToInt(objectHandlers.Post("pheight_" + keys[1]));
                            rtn = base.handlerService.skinService.categoriesHandlers.CategoriePropertiesManage(base.adminInfo, cps);
                        }
                    }

                    rtn = base.handlerService.skinService.categoriesHandlers.CreateCategoriesToXML(base.adminInfo,cif.SkinInfo.Id);
                    CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
                    CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
                    CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_PROPERTIES + cif.Id);
                    CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_PROPERTIES_ENTITY + cif.Id);
                }
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }

            base.AjaxErch(rtn, "分类添加成功！");
        }
    }

    private void Init()
    {
        string iClassId = objectHandlers.Get("iClassId");
        this.cid.Text = iClassId + "&t=" + DateTime.Now.ToString();
        string skinid = objectHandlers.Get("SkinId");
        this.iMaxPId.Value = base.handlerService.skinService.categoriesHandlers.GetMaxCategoriesProperties().ToString();
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

        Dictionary<string, EntityBase> templates = base.handlerService.skinService.templateHandlers.GetTemplatesByTemplateType(TemplateType.InfoType, skinid);
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
