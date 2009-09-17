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
using TCG.Manage.Kernel;
using TCG.Manage.Utils;
using TCG.Data;
using TCG.News.Handlers;
using TCG.News.Entity;


public partial class news_NewsSpecialityList : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.SearchInit();
        }
        else
        {
            string action = Fetch.Post("iAction");
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
        base.conn.Dblink = DBLinkNums.News;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_News_Speciality";

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

        sItem.page = Bases.ToInt(Fetch.Get("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);

        int iParent = Bases.ToInt(Fetch.Get("iParentID"));
        sItem.strCondition = "iParent=" + iParent.ToString();
        this.iParentID.Value = iParent.ToString();
        this.inParentId.Value = iParent.ToString();

        int iSiteId = Bases.ToInt(Fetch.Get("iSiteId"));
        sItem.strCondition += " AND iSiteId=" + iSiteId.ToString();
        this.iSiteId.Value = iSiteId.ToString();

        int curPage = 0;
        int pageCount = 0;
        int count = 0;
        DataSet ds = new DataSet();
        int rtn = DBHandlers.GetPage(sItem, base.conn, ref curPage, ref pageCount, ref count, ref ds);
        if (rtn < 0)
        {
            this.Throw(rtn, null, true);
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
        NewsSpecialityInfo  item = new NewsSpecialityInfo();
        item.vcTitle = Fetch.Post("inTitle");
        item.vcExplain = Fetch.Post("inExplain");
        item.iParent = Bases.ToInt(Fetch.Post("inParentId"));
        item.iSiteId = Bases.ToInt(Fetch.Post("iSiteId"));
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

        NewsSpecialityHandlers nshdl = new NewsSpecialityHandlers();
        int rtn = nshdl.NewsSpecialityAdd(base.conn, base.admin.adminInfo.vcAdminName, item);
        Caching.Remove("AllNewsSpeciality");
        base.AjaxErch(rtn.ToString());
        base.Finish();
        nshdl = null;
        item = null;
    }

    private void SpecialityMDY()
    {
        string KeyValue = Fetch.Post("KeyValue");
        if (string.IsNullOrEmpty(KeyValue))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
        string iFeildName = Fetch.Post("iFeildName");
        if (string.IsNullOrEmpty(iFeildName))
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        int iMdyID = Bases.ToInt(Fetch.Post("iMdyID"));
        if (iMdyID == 0)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
         
        NewsSpecialityHandlers nshdl = new NewsSpecialityHandlers();
        NewsSpecialityInfo item = nshdl.GetNewsSpecialityInfoById(base.conn, iMdyID);
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
                int iKeyValue = Bases.ToInt(KeyValue);
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
            int rtn = nshdl.NewsSpecialityMdy(base.conn, base.admin.adminInfo.vcAdminName, item);
            Caching.Remove("AllNewsSpeciality");
            base.AjaxErch(rtn.ToString());
        }

        base.Finish();
        nshdl = null;
        item = null;
    }

    private void SpecialityDEL()
    {
        string Ids = Fetch.Post("iIds");
        if (string.IsNullOrEmpty(Ids))
        {
            base.AjaxErch("-1000000038");
            base.Finish();
            return;
        }
        NewsSpecialityHandlers nshdl = new NewsSpecialityHandlers();
        int rtn = nshdl.NewSpecialityDel(base.conn, base.admin.adminInfo.vcAdminName, Ids);
        Caching.Remove("AllNewsSpeciality");
        base.AjaxErch(rtn.ToString());
        base.Finish();
        nshdl = null;
    }
}
