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
    public partial class Manage_Skin_propertieshandlers : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(46);

            if (!Page.IsPostBack)
            {
                this.Init();
            }
            else
            {
                PropertiesCategorie pc = new PropertiesCategorie();
                pc.Id = objectHandlers.Get("id");
                if (objectHandlers.ToInt(pc.Id) == 0)
                {
                    pc.Id = (base.handlerService.skinService.propertiesHandlers.GetMaxPropertiesCategrie() + 1).ToString();
                }

                pc.CategoriePropertiesName = objectHandlers.Post("iClassName");
                pc.Visible = objectHandlers.Post("iVisible");
                pc.SkinId = objectHandlers.Post("iSkinId"); ;

                int rtn = 0;
                try
                {
                    rtn = base.handlerService.skinService.propertiesHandlers.PropertiesCategoriesManage(base.adminInfo, pc);
                    if (rtn == 1)
                    {
                        foreach (string key in Request.Form.AllKeys)
                        {
                            if (key.IndexOf("name_") > -1 && !string.IsNullOrEmpty(objectHandlers.Post(key)))
                            {
                                string[] keys = key.Split('_');
                                Properties cps = new Properties();
                                cps.Id = objectHandlers.Post("cpid_" + keys[1]);
                                cps.ProertieName = objectHandlers.Post(key);
                                cps.PropertiesCategorieId = pc.Id;
                                cps.Type = objectHandlers.Post("type_" + keys[1]);
                                cps.Values = objectHandlers.Post("pttext_" + keys[1]);
                                cps.width = objectHandlers.ToInt(objectHandlers.Post("pwidth_" + keys[1]));
                                cps.height = objectHandlers.ToInt(objectHandlers.Post("pheight_" + keys[1]));
                                cps.iOrder = objectHandlers.ToInt(objectHandlers.Post("porder_" + keys[1]));
                                rtn = base.handlerService.skinService.propertiesHandlers.PropertiesManage(base.adminInfo, cps);
                            }
                        }

                        CachingService.Remove(CachingService.CACHING_ALL_PROPERTIES + pc.Id);
                        CachingService.Remove(CachingService.CACHING_ALL_PROPERTIES_ENTITY + pc.Id);
                        CachingService.Remove(CachingService.CACHING_ALL_PROPERTIES_CATEGORIES_ENTITY + pc.SkinId);
                    }
                }
                catch (Exception ex)
                {
                    base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                    base.AjaxErch(base.ajaxdata);
                    return;
                }

                base.AjaxErch(rtn, "属性分类维护成功！");


            }
        }

        private void Init()
        {
            string skinid = string.IsNullOrEmpty(objectHandlers.Get("skinid")) ? ConfigServiceEx.DefaultSkinId : objectHandlers.Get("skinid");
            this.iSkinId.Value = skinid;
            string pcid = objectHandlers.Get("id");
            this.PropertiesCategorieId.Value = pcid;

            this.iMaxPId.Value = base.handlerService.skinService.propertiesHandlers.GetMaxProperties().ToString();

            if (objectHandlers.ToInt(pcid) > 0)
            {
                PropertiesCategorie pc = base.handlerService.skinService.propertiesHandlers.GetPropertiesCategoriesBySkinidAndId(skinid, pcid);
                if (pc != null)
                {
                    this.iClassName.Value = pc.CategoriePropertiesName;
                    this.iVisible.Value = pc.Visible;
                    this.cid.Text = pc.Id;
                }
            }
        }
    }
}