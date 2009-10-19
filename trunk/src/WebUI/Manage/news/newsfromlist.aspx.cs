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
using TCG.Pages;

using TCG.Data;
using TCG.Handlers;
using TCG.Entity;


public partial class news_newsfromlist : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    this.NewsFromADD();
                    break;
                case "MDY":
                    this.NewsFromMDY();
                    break;
                case "DEL":
                    this.NewsFromDEL();
                    break;
            }
        }
    }

    private void SearchInit()
    {
        base.conn.Dblink = DBLinkNums.News;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_News_NewsFromInfo";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("vcTitle");
        arrshowfied.Add("vcUrl");
        arrshowfied.Add("dUpDateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId");
        sItem.arrSortField = arrsortfield;

        sItem.page = objectHandlers.ToInt(objectHandlers.Get("page"));
        sItem.pageSize = objectHandlers.ToInt(base.configService.baseConfig["PageSize"]);
        sItem.strCondition = "iID>0";

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
        Span sUrl = (Span)e.Item.FindControl("sUrl");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        CheckID.Text = Row["iId"].ToString();
        sId.Text = Row["iId"].ToString();
        sTitle.Text = Row["vcTitle"].ToString();
        sUrl.Text = Row["vcUrl"].ToString();
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void NewsFromADD()
    {
        NewsFromInfo item = new NewsFromInfo();
        item.vcTitle = objectHandlers.Post("inTitle");
        item.vcUrl = objectHandlers.Post("inUrl");
        if (string.IsNullOrEmpty(item.vcTitle))
        {
            base.AjaxErch("-1000000052");
            base.Finish();
            return;
        }
        if (string.IsNullOrEmpty(item.vcUrl))
        {
            base.AjaxErch("-1000000053");
            base.Finish();
            return;
        }

        int rtn = base.handlerService.newsFromHandlers.AddNewsFromInfo(base.conn, base.adminInfo.vcAdminName, item);
        base.AjaxErch(rtn.ToString());
        base.Finish();
        item = null;
    }

    private void NewsFromMDY()
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


        NewsFromInfo item = base.handlerService.newsFromHandlers.GetNewsFromInfoById(base.conn, iMdyID);
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
            case "Url":
                item.vcUrl = KeyValue;
                break;
            default:
                base.AjaxErch("-1");
                base.Finish();
                item = null;
                break;
        }

        int rtn = base.handlerService.newsFromHandlers.UpdateNewsFromInfo(base.conn, base.adminInfo.vcAdminName, item);
        base.AjaxErch(rtn.ToString());
        base.Finish();
        item = null;
    }

    private void NewsFromDEL()
    {
        string Ids = objectHandlers.Post("iIds");
        if (string.IsNullOrEmpty(Ids))
        {
            base.AjaxErch("-1000000038");
            base.Finish();
            return;
        }

        int rtn = base.handlerService.newsFromHandlers.DeleteNewsFromInfos(base.conn, base.adminInfo.vcAdminName, Ids);
        base.AjaxErch(rtn.ToString());
        base.Finish();

    }
}
