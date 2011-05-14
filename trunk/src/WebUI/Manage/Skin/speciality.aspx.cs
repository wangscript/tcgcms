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


using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Controls.HtmlControls;


using TCG.Data;
using TCG.Handlers;
using TCG.Entity;


public partial class skin_speciality : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            this.SearchInit();
        }
        else
        {
            string action = objectHandlers.Post("iAction");
            switch (action)
            {
                case "ADD" :
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
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "Speciality";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("iParent");
        arrshowfied.Add("iSiteId");
        arrshowfied.Add("vcTitle");
        arrshowfied.Add("vcExplain");
        arrshowfied.Add("dUpDateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId");
        sItem.arrSortField = arrsortfield;

        sItem.page = objectHandlers.ToInt(objectHandlers.Get("page"));
        sItem.pageSize = objectHandlers.ToInt(ConfigServiceEx.baseConfig["PageSize"]);

        int iParent = objectHandlers.ToInt(objectHandlers.Get("iParentID"));
        sItem.strCondition = "iParent=" + iParent.ToString();
        this.iParentID.Value = iParent.ToString();
        this.inParentId.Value = iParent.ToString();

        int iSiteId = objectHandlers.ToInt(objectHandlers.Get("iSiteId"));
        sItem.strCondition += " AND iSiteId=" + iSiteId.ToString();
        this.iSiteId.Value = iSiteId.ToString();

        int curPage = 0;
        int pageCount = 0;
        int count = 0;
        DataSet ds = new DataSet();
        int rtn = DBHandlers.GetPage(sItem, base.conn, ref curPage, ref pageCount, ref count, ref ds);
        if (rtn < 0)
        {
            return;
        }
        this.pager.Per = sItem.pageSize;
        this.pager.SetItem("iParentId", iParent);
        this.pager.SetItem("iSiteId", iSiteId);
        this.pager.Total = count;
        this.pager.Calculate();

        if (ds.Tables.Count != 0)
        {
            this.ItemRepeater.DataSource = ds;
            this.ItemRepeater.DataBind();
        }

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Span sId = (Span)e.Item.FindControl("sId");
        Span sTitle = (Span)e.Item.FindControl("sTitle");
        Span sExplain = (Span)e.Item.FindControl("sExplain");
        Span sParent = (Span)e.Item.FindControl("sParent");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        CheckID.Text = Row["iId"].ToString();
        sId.Text = Row["iId"].ToString();
        string text = "<a href=\"?iParentId=" + Row["iId"].ToString() + "&iSiteId=" 
            + Row["iSiteId"].ToString() +"\" title=\"查看子分类\">" +"<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
        sTitle.Text = text + "<span class=\"l_classname\" style=\"width:170px;\" onclick=\"MdyFeild(this,'Title')\">" 
            + Row["vcTitle"].ToString() +"</span>";
        sExplain.Text = Row["vcExplain"].ToString();
        sParent.Text = Row["iParent"].ToString();
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void SpecialityADD()
    {
        Speciality  item = new Speciality();
        item.vcTitle = objectHandlers.Post("inTitle");
        item.vcExplain = objectHandlers.Post("inExplain");
        item.iParent = objectHandlers.ToInt(objectHandlers.Post("inParentId"));
        item.iSiteId = objectHandlers.ToInt(objectHandlers.Post("iSiteId"));
        if (string.IsNullOrEmpty(item.vcTitle))
        {
            base.AjaxErch("-1000000035");
            base.Finish();
            return;
        }
        //if (item.iSiteId == 0)
        //{
        //    base.AjaxErch("-1000000034");
        //    base.Finish();
        //    return;
        //}

        int rtn = base.handlerService.skinService.specialityHandlers.NewsSpecialityAdd(base.conn, base.adminInfo.vcAdminName, item);
        CachingService.Remove("AllNewsSpeciality");
        base.AjaxErch(rtn.ToString());
        base.Finish();

        item = null;
    }

    private void SpecialityMDY()
    {
        string KeyValue = objectHandlers.Post("KeyValue");
        if (string.IsNullOrEmpty(KeyValue))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
        string iFeildName = objectHandlers.Post("iFeildName");
        if (string.IsNullOrEmpty(iFeildName))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        int iMdyID = objectHandlers.ToInt(objectHandlers.Post("iMdyID"));
        if (iMdyID == 0)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
         

        Speciality item = base.handlerService.skinService.specialityHandlers.GetNewsSpecialityInfoById(base.conn, iMdyID);
        bool ismdy = true;
        if (item == null)
        {
            base.AjaxErch("-1");
            base.Finish();
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
                if (item.iId == iKeyValue)
                {
                    ismdy = false;
                    base.AjaxErch("-1000000036");
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
            int rtn = base.handlerService.skinService.specialityHandlers.NewsSpecialityMdy(base.conn, base.adminInfo.vcAdminName, item);
            CachingService.Remove("AllNewsSpeciality");
            base.AjaxErch(rtn.ToString());
        }

        base.Finish();
        item = null;
    }

    private void SpecialityDEL()
    {
        string Ids = objectHandlers.Post("iIds");
        if (string.IsNullOrEmpty(Ids))
        {
            base.AjaxErch("-1000000038");
            base.Finish();
            return;
        }

        int rtn = base.handlerService.skinService.specialityHandlers.NewSpecialityDel(base.conn, base.adminInfo.vcAdminName, Ids);
        CachingService.Remove("AllNewsSpeciality");
        base.AjaxErch(rtn.ToString());
        base.Finish();
    }
}
