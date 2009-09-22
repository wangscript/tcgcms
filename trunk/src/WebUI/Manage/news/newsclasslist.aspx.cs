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
using TCG.Manage.Utils;
using TCG.Data;
using TCG.News.Entity;
using TCG.News.Handlers;

using TCG.Template.Entity;
using TCG.Template.Handlers;
using TCG.Template.Utils;

using TCG.TCGTagReader.Handlers;

public partial class news_newsclasslist : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.SearchInit();
        }
        else
        {
            string work = Fetch.Post("work");
            switch (work)
            {
                case "DEL":
                    this.NewsClassDel();
                    break;
                case "Create":
                    this.NewsClassCreate();
                    break;
            }
            base.Finish();

        }
    }

    private void SearchInit()
    {
        base.conn.Dblink = DBLinkNums.News;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_News_ClassInfo";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("vcClassName");
        arrshowfied.Add("vcName");
        arrshowfied.Add("vcDirectory");
        arrshowfied.Add("dUpdateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iOrder");
        sItem.arrSortField = arrsortfield;

        sItem.page = Bases.ToInt(Fetch.Get("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);

        int iParent = Bases.ToInt(Fetch.Get("iParentId"));
        sItem.strCondition = "iParent=" + iParent.ToString();
        this.iClassId.Value = iParent.ToString();

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
        Span classname = (Span)e.Item.FindControl("classname");
        Span lname = (Span)e.Item.FindControl("lname");
        Anchor directory = (Anchor)e.Item.FindControl("directory");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        CheckID.Text = Row["iId"].ToString();
        sId.Text = Row["iId"].ToString();
        
        string text = "<a href=\"?iParentId=" + Row["iId"].ToString() + "\" title=\"查看子分类\">" 
            + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
        classname.Text = text + Row["vcClassName"].ToString();
        lname.Text = "<a href='newslist.aspx?iClassId=" + Row["iId"].ToString() + "' title='资讯列表'><img src='../images/icon/09.gif'></a>" 
            + Row["vcName"].ToString();
        directory.Text = Row["vcDirectory"].ToString();
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void NewsClassDel()
    {
        int tClassID = Bases.ToInt(Fetch.Post("DelClassId"));
        if (tClassID == 0)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        classHandlers chdl = new classHandlers();
        int rtn = chdl.DelNewsClassByClassId(base.conn, tClassID, base.admin.adminInfo.vcAdminName);
        base.AjaxErch(rtn.ToString());
        base.Finish();
        chdl = null;
    }

    private void NewsClassCreate()
    {
        int tClassID = Bases.ToInt(Fetch.Post("DelClassId"));
        if (tClassID == 0)
        {
            base.AjaxErch("<a>生成失败,分类ID为0!</a>");
            base.Finish();
            return;
        }

        classHandlers chdl = new classHandlers();
        ClassInfo cif = chdl.GetClassInfoById(base.conn, tClassID, false);
        chdl = null;
        if (cif == null)
        {
            base.AjaxErch("<a>生成失败,分类信息不存在!</a>");
            base.Finish();
            return;
        }

        TemplateHandlers tlhdl = new TemplateHandlers();
        TemplateInfo tlif = tlhdl.GetTemplateInfoByID(base.conn, cif.iListTemplate);
        tlhdl = null;
        if (tlif == null)
        {
            base.AjaxErch("<a>生成失败,分类模版信息读取失败!</a>");
            base.Finish();
            return;
        }

        if (cif.vcUrl.IndexOf(".") > -1)
        {
            base.AjaxErch("<a>生成失败,为跳转地址，无需生成！</a>");
            base.Finish();
            return;
        }

        string filepath = "";
        try
        {
            filepath = Server.MapPath("~" + cif.vcUrl + base.config["FileExtension"]);
        }
        catch
        {
            base.AjaxErch("<a>生成失败,分类保存路径读取失败!</a>");
            base.Finish();
            return;
        }

        TCGTagHandlers tcgthdl = new TCGTagHandlers();
        tcgthdl.Template = tlif.vcContent.Replace("_$ClassId$_", tClassID.ToString());
        tcgthdl.FilePath = filepath;
        tcgthdl.WebPath = cif.vcUrl + base.config["FileExtension"];
        if (tcgthdl.Replace(base.conn, base.config))
        {   
            string text1 = "";
            if (tcgthdl.PagerInfo.PageCount > 1)
            {
                
                for (int i = 1; i <= tcgthdl.PagerInfo.PageCount; i++)
                {
                    string num = (i == 1) ? "" : i.ToString();
                    text1 += "<a href='.." + cif.vcUrl + "' target='_blank'>生成成功:" + cif.vcUrl + num + base.config["FileExtension"] + "...</a>";
                }
            }
            else
            {
                text1 = "<a href='.." + cif.vcUrl + "' target='_blank'>生成成功:" + cif.vcUrl + base.config["FileExtension"] + "...</a>";
            }
            base.AjaxErch(text1);
        }
        else
        {
            base.AjaxErch("<a>生成失败-系统错误!</a>");
        }
        tcgthdl = null;
        tlif = null;

    }
}

