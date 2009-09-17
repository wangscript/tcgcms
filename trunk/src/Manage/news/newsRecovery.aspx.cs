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

public partial class news_newsRecovery : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.SearchInit();
        }
        else
        {
            string Action = Fetch.Post("iAction");
            switch (Action)
            {
                case "SAVE":
                    this.SaveNews();
                    break;
                case "DEL":
                    this.DelNews();
                    break;
            }
            base.Finish();
            return;
        }
    }

    private void SearchInit()
    {
        base.conn.Dblink = DBLinkNums.News;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_News_NewsInfo";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("vcTitle");
        arrshowfied.Add("iClassID");
        arrshowfied.Add("cChecked");
        arrshowfied.Add("cCreated");
        arrshowfied.Add("dUpDateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = Bases.ToInt(Fetch.Get("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);
        sItem.strCondition += " cDel ='Y'";

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
        Span sClassName = (Span)e.Item.FindControl("sClassName");
        Span sChecked = (Span)e.Item.FindControl("sChecked");
        Span sCreated = (Span)e.Item.FindControl("sCreated");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        CheckID.Text = Row["iId"].ToString();
        sId.Text = Row["iId"].ToString();
        string check = Row["cChecked"].ToString();
        string Created = Row["cCreated"].ToString();
        if (check == "Y")
        {
            sChecked.Text = "<img src='../images/icon/checked.gif' class='imginlist' />";
        }
        else
        {
            sChecked.Text = "<img src='../images/icon/falseIcon.gif' class='imginlist' />";
        }

        if (Created == "Y")
        {
            sCreated.Text = "<img src='../images/icon/deled.gif' class='imginlist' />";
        }
        else
        {
            sCreated.Text = "<img src='../images/icon/falseIcon.gif' class='imginlist' />";
        }

        sTitle.Text = Row["vcTitle"].ToString();
        sClassName.Text = "<script type=\"text/javascript\">ShowClassNameByClassID(" + Row["iClassID"].ToString() + ");</script>";

        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void SaveNews()
    {
        string delids = Fetch.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch("-1000000051");
            return;
        }

        newsInfoHandlers nihdl = new newsInfoHandlers();

        int rtn = nihdl.DelNewsInfosWithLogic(base.conn, base.admin.adminInfo.vcAdminName, "N", delids);
        base.AjaxErch(rtn.ToString());
    }

    private void DelNews()
    {
        string delids = Fetch.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch("-1000000051");
            return;
        }

        newsInfoHandlers nihdl = new newsInfoHandlers();

        int rtn = nihdl.DelNewsInfosWithPhysics(base.conn, base.admin.adminInfo.vcAdminName, delids);
        base.AjaxErch(rtn.ToString());
    }
}
