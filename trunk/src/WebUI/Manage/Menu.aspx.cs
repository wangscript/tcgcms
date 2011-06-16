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
using System.Text;

using TCG.Utils;
using TCG.Controls.HtmlControls;

using TCG.Entity;

namespace TCG.CMS.WebUi
{
    public partial class aMenu : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(16);

            if (!Page.IsPostBack)
            {
                this.meunInit();
                this.temp2.Text = "";
            }
        }

        private void meunInit()
        {
            string tempClass = this.temp1.Text;
            string tempSClass = this.temp2.Text;

            int ParentId = objectHandlers.ToInt(objectHandlers.Get("ParendId"));
            if (ParentId == 0) ParentId = 1;


            base.handlerService.manageService.adminHandlers.CheckAdminPop(ParentId);

            //获取管理员权限
            Dictionary<int, Popedom> cpop = base.handlerService.manageService.adminHandlers.GetChildManagePopedomEntity(ParentId);

            StringBuilder sb = new StringBuilder();
            StringBuilder script = new StringBuilder();




            script.Append("<script type=\"text/javascript\">\r\n");
            script.Append("var btNum =" + cpop.Count.ToString() + ";\r\n");

            int i = 0;

            //如果是资源管理，显示详细资源分类
            if (ParentId == 18)
            {
                //加载2级资讯分类
                Dictionary<string, EntityBase> categries = base.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentId("0", ConfigServiceEx.DefaultSkinId);
                if (categries != null && categries.Count > 0)
                {
                    foreach (KeyValuePair<string, EntityBase> keyvalue in categries)
                    {
                        Categories categorie = (Categories)keyvalue.Value;
                        if (0 == i)
                        {
                            script.Append("window.parent.main.location.href='resources/resourceslist.aspx?skinid=" + ConfigServiceEx.DefaultSkinId
                                + "&iclassid=" + categorie.Id + "';\r\n");
                        }

                        sb.Append(string.Format(tempClass, i,
                            "resources/resourceslist.aspx?skinid=" + ConfigServiceEx.DefaultSkinId + "&iclassid=" + categorie.Id
                            , categorie.vcClassName));

                        Dictionary<string, EntityBase> categries1 = base.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentId(categorie.Id, ConfigServiceEx.DefaultSkinId);
                        if (categries1 != null && categries1.Count > 0)
                        {
                            script.Append("stNums[" + i.ToString() + "]=" + categries1.Count.ToString() + ";\r\n");
                            int m = 0;
                            foreach (KeyValuePair<string, EntityBase> keyvalue1 in categries1)
                            {
                                Categories categorie12 = (Categories)keyvalue1.Value;
                                sb.Append(string.Format(tempSClass, i, m,
                                    "resources/resourceslist.aspx?skinid=" + ConfigServiceEx.DefaultSkinId + "&iclassid=" + categorie12.Id,
                                    categorie12.vcClassName, 23));

                                m++;
                            }
                        }

                        i++;
                    }
                }
            }
            else
            {

                foreach (KeyValuePair<int, Popedom> keyvalue in cpop)
                {
                    string Url = keyvalue.Value.vcUrl;

                    if (base.handlerService.manageService.adminHandlers.CheckAdminPopEx(keyvalue.Value.iID))
                    {
                        if (0 == i) script.Append("window.parent.main.location.href='" + Url + "';\r\n");
                        sb.Append(string.Format(tempClass, i, Url, keyvalue.Value.vcPopName));

                        Dictionary<int, Popedom> tpop = base.handlerService.manageService.adminHandlers.GetChildManagePopedomEntity(keyvalue.Value.iID);
                        if (tpop == null) continue;

                        script.Append("stNums[" + i.ToString() + "]=" + tpop.Count.ToString() + ";\r\n");

                        int n = 0;
                        foreach (KeyValuePair<int, Popedom> keyvalue1 in tpop)
                        {
                            if (base.handlerService.manageService.adminHandlers.CheckAdminPopEx(keyvalue1.Value.iID))
                            {
                                string Url1 = keyvalue1.Value.vcUrl;
                                sb.Append(string.Format(tempSClass, i, n, Url1, keyvalue1.Value.vcPopName, keyvalue1.Value.iID.ToString()));
                                n++;
                            }
                        }

                        i++;
                    }
                }
            }


            script.Append("</script>\r\n");
            this.temp1.Text = script.ToString();
            this.emu.Text = sb.ToString();
        }
    }
}