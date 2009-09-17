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

using TCG.Template.Entity;
using TCG.Template.Handlers;
using TCG.TCGTagReader.Handlers;

public partial class xtucs_member : adminMain
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
                case "DEL":
                    this.DelNews();
                    break;
                case "CHECK":
                    this.CheckMember();
                    break;
                case "CHECK1":
                    this.CheckMember1();
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
        sItem.tableName = "T_XTUCS_MembersInfo";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("ID");
        arrshowfied.Add("XinMing");
        arrshowfied.Add("ZhuanYe");
        arrshowfied.Add("BiYeNianFen");
        arrshowfied.Add("GongZuoDanWei");
        arrshowfied.Add("ShouJi");
            arrshowfied.Add("cStatus");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iOrder DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = Bases.ToInt(Fetch.Get("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);

        sItem.strCondition = "ID>0";
        string Xinming = Fetch.Get("XinMing");

        if (!string.IsNullOrEmpty(Xinming))
        {
            sItem.strCondition += " AND XinMing like '%" + Xinming + "%'";
        }



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
        this.pager.SetItem("XinMing", Xinming);
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
        Span sId = (Span)e.Item.FindControl("sId");
        Span sXinMing = (Span)e.Item.FindControl("sXinMing");
        Span sZhuanYe = (Span)e.Item.FindControl("sZhuanYe");
        Span sNianJI = (Span)e.Item.FindControl("sNianJI");
        Span sDanWei = (Span)e.Item.FindControl("sDanWei");
        Span sShouJi = (Span)e.Item.FindControl("sShouJi");
        Span cCheck = (Span)e.Item.FindControl("cCheck");
        Span CheckID = (Span)e.Item.FindControl("CheckID");

        CheckID.Text = Row["ID"].ToString();
        sId.Text = Row["ID"].ToString();
        sXinMing.Text = Row["XinMing"].ToString();
        sZhuanYe.Text = Row["ZhuanYe"].ToString();
        sNianJI.Text = Row["BiYeNianFen"].ToString();
        sDanWei.Text = Row["GongZuoDanWei"].ToString();
        sShouJi.Text = Row["ShouJi"].ToString();

        if (Row["cStatus"].ToString() == "02")
        {
            cCheck.Text = "<img src='../images/icon/checked.gif' class='imginlist' />";
        }
        else
        {
            cCheck.Text = "<img src='../images/icon/falseIcon.gif' class='imginlist' />";
        }

    }

    private void DelNews()
    {
        string delids = Fetch.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch("-1000000080");
            return;
        }

        string SQL = string.Empty;
        if (delids.IndexOf(",") > -1)
        {
            SQL = "DELETE T_XTUCS_MembersInfo WHERE ID in (" + delids + ")";
        }
        else
        {
            SQL = "DELETE T_XTUCS_MembersInfo WHERE ID = " + delids;
        }

        base.conn.Dblink = DBLinkNums.News;
        base.AjaxErch(base.conn.Execute(SQL).ToString());
     
    }


    private void CheckMember()
    {
        string delids = Fetch.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch("-1000000080");
            return;
        }

        string SQL = string.Empty;
        if (delids.IndexOf(",") > -1)
        {
            SQL = "UPDATE T_XTUCS_MembersInfo SET cStatus = '02' WHERE ID in (" + delids + ")";
        }
        else
        {
            SQL = "UPDATE T_XTUCS_MembersInfo SET cStatus = '02' WHERE ID = " + delids;
        }

        base.conn.Dblink = DBLinkNums.News;
        base.AjaxErch(base.conn.Execute(SQL).ToString());
    }


    private void CheckMember1()
    {
        string delids = Fetch.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch("-1000000080");
            return;
        }

        string SQL = string.Empty;
        if (delids.IndexOf(",") > -1)
        {
            SQL = "UPDATE T_XTUCS_MembersInfo SET cStatus = '01' WHERE ID in (" + delids + ")";
        }
        else
        {
            SQL = "UPDATE T_XTUCS_MembersInfo SET cStatus = '01' WHERE ID = " + delids;
        }

        base.conn.Dblink = DBLinkNums.News;
        base.AjaxErch(base.conn.Execute(SQL).ToString());
    }
}
