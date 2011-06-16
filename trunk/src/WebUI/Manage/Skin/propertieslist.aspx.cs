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
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Controls.HtmlControls;


using TCG.Data;
using TCG.Entity;
using TCG.Handlers;

namespace TCG.CMS.WebUi
{
    public partial class Manage_Skin_propertieslist : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(46);

            if (!Page.IsPostBack)
            {
                this.SearchInit();
            }
            else
            {
                string work = objectHandlers.Post("work");
                switch (work)
                {

                }

            }

        }

        private void SearchInit()
        {
            string iParent = objectHandlers.Get("iParentId");
            this.iClassId.Value = iParent;
            string skinid = objectHandlers.Get("skinid");
            if (string.IsNullOrEmpty(skinid))
            {
                skinid = ConfigServiceEx.DefaultSkinId;
            }
            this.iSkinId.Value = skinid;
            if (string.IsNullOrEmpty(iParent)) iParent = "0";
            Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.propertiesHandlers.GetPropertiesCategoriesEntityBySkinId(skinid);

            if (allcategories != null)
            {
                this.ItemRepeater.DataSource = allcategories.Values;
                this.ItemRepeater.DataBind();
            }

        }

        protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            PropertiesCategorie categorie = (PropertiesCategorie)e.Item.DataItem;
            Span CheckID = (Span)e.Item.FindControl("CheckID");
            Span ipid = (Span)e.Item.FindControl("ipid");
            Span lname = (Span)e.Item.FindControl("lname");
            Span sVisible = (Span)e.Item.FindControl("sVisible");


            string skinid = objectHandlers.Get("skinid");
            if (string.IsNullOrEmpty(skinid))
            {
                skinid = ConfigServiceEx.DefaultSkinId;
            }

            CheckID.Text = categorie.Id;

            sVisible.Text = categorie.Visible.ToString();

            string text = "<a href=\"?iParentId=" + categorie.Id + "&skinid=" + skinid + "\" title=\"查看子分类\">"
                + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
            ipid.Text = categorie.Id;
            lname.Text = "<a href='propertieshandlers.aspx?Id=" + categorie.Id + "&skinid=" + skinid + "' title='属性维护'><img src='../images/icon/11.gif'></a>"
                + categorie.CategoriePropertiesName;

        }
    }
}