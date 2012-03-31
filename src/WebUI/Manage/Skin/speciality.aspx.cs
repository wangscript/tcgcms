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


using TCG.Entity;
using TCG.Handlers;


namespace TCG.CMS.WebUi
{
    public partial class skin_speciality : BasePage
    {

        private string SkinId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(31);

            SkinId = objectHandlers.Get("skinid");
            if (string.IsNullOrEmpty(SkinId)) SkinId = ConfigServiceEx.DefaultSkinId;
            this.iSkinId.Value = SkinId;
            if (!Page.IsPostBack)
            {
                this.SearchInit();
            }
            else
            {
                string action = objectHandlers.Post("iAction");
                switch (action)
                {
                    case "ADD":
                        this.SpecialityADD();
                        break;
                    case "MDY":
                        this.SpecialityMDY();
                        break;
                    case "DEL":
                        this.SpecialityDEL();
                        break;
                }
            }
        }

        private void SearchInit()
        {
            int iParent = objectHandlers.ToInt(objectHandlers.Get("iParentId"));
            this.iParentID.Value = iParent.ToString();
            string skinid = objectHandlers.Get("skinid");
            if (string.IsNullOrEmpty(skinid))
            {
                skinid = ConfigServiceEx.DefaultSkinId;
            }
            this.iSkinId.Value = skinid;

            Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.specialityHandlers.GetAllNewsSpecialityEntityBySkinIdAndParentid(skinid, iParent);

            if (allcategories != null)
            {
                this.ItemRepeater.DataSource = allcategories.Values;
                this.ItemRepeater.DataBind();
            }

        }

        protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Speciality seciality = (Speciality)e.Item.DataItem;
            Span CheckID = (Span)e.Item.FindControl("CheckID");
            Span sId = (Span)e.Item.FindControl("sId");
            Span sTitle = (Span)e.Item.FindControl("sTitle");
            Span sExplain = (Span)e.Item.FindControl("sExplain");
            Span sParent = (Span)e.Item.FindControl("sParent");
            Span updatedate = (Span)e.Item.FindControl("updatedate");

            CheckID.Text = seciality.Id;
            sId.Text = seciality.Id;
            string text = "<a href=\"?iParentId=" + seciality.Id + "&skinid="
                + seciality.SkinId + "\" title=\"查看子分类\">" + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
            sTitle.Text = text + "<span class=\"l_classname\" style=\"width:170px;\" onclick=\"MdyFeild(this,'Title')\">"
                + seciality.vcTitle + "</span>";
            sExplain.Text = seciality.vcExplain;
            sParent.Text = seciality.iParent.ToString();
            updatedate.Text = seciality.dUpDateDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void SpecialityADD()
        {
            Speciality item = new Speciality();
            item.vcTitle = objectHandlers.Post("inTitle");
            item.vcExplain = objectHandlers.Post("inExplain");
            item.iParent = objectHandlers.ToInt(objectHandlers.Post("inParentId"));
            item.SkinId = objectHandlers.Post("iSkinId");
            if (string.IsNullOrEmpty(item.vcTitle))
            {
                base.AjaxErch(-1000000035, "");
                return;
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.skinService.specialityHandlers.NewsSpecialityAdd(base.adminInfo, item);
                CachingService.Remove(CachingService.CACHING_ALL_SPECIALITY_ENTITY + "_" + item.SkinId);
            }
            catch (Exception ex)
            {
                base.AjaxErch(-1, ex.Message.ToString());
                return;
            }

            base.AjaxErch(1, "成功添加资讯特性", "refinsh()");
        }

        private void SpecialityMDY()
        {
            string KeyValue = objectHandlers.Post("KeyValue");
            if (string.IsNullOrEmpty(KeyValue))
            {
                base.AjaxErch(-1, "");
                return;
            }
            string iFeildName = objectHandlers.Post("iFeildName");
            if (string.IsNullOrEmpty(iFeildName))
            {
                base.AjaxErch(-1, "");
                return;
            }

            int iMdyID = objectHandlers.ToInt(objectHandlers.Post("iMdyID"));
            if (iMdyID == 0)
            {
                base.AjaxErch(-1, "");
                return;
            }


            Speciality item = base.handlerService.skinService.specialityHandlers.GetSpecialityById(SkinId, iMdyID.ToString());
            bool ismdy = true;
            if (item == null)
            {
                base.AjaxErch(-1, "");
                return;
            }

            switch (iFeildName)
            {
                case "Title":
                    item.vcTitle = KeyValue;
                    break;
                case "Explain":
                    item.vcExplain = KeyValue;
                    break;
                case "Parent":
                    int iKeyValue = objectHandlers.ToInt(KeyValue);
                    if (item.Id == iKeyValue.ToString())
                    {
                        ismdy = false;
                        base.AjaxErch(-1000000036, "");
                        return;
                    }
                    else
                    {
                        item.iParent = iKeyValue;
                    }
                    break;
                default:
                    ismdy = false;
                    break;
            }

            if (ismdy)
            {
                int rtn = 0;
                try
                {
                    rtn = base.handlerService.skinService.specialityHandlers.NewsSpecialityMdy(base.adminInfo, item);
                    CachingService.Remove(CachingService.CACHING_ALL_SPECIALITY_ENTITY + "_" + SkinId);
                }
                catch (Exception ex)
                {
                    base.AjaxErch(-1, ex.Message.ToString());
                    return;
                }
            }

            base.AjaxErch(1, "成功编辑资讯特性", "NewsSMDYPostBack()");
        }

        private void SpecialityDEL()
        {
            string Ids = objectHandlers.Post("iIds");
            if (string.IsNullOrEmpty(Ids))
            {
                base.AjaxErch(-1000000038, "");
                return;
            }

            try
            {
                int rtn = base.handlerService.skinService.specialityHandlers.NewSpecialityDel(base.adminInfo, Ids);
                CachingService.Remove(CachingService.CACHING_ALL_SPECIALITY_ENTITY + "_" + SkinId);
            }
            catch (Exception ex)
            {
                base.AjaxErch(-1, ex.Message.ToString());
                return;
            }

            base.AjaxErch(1, "成功删除资讯特性", "refinsh()");
        }
    }
}