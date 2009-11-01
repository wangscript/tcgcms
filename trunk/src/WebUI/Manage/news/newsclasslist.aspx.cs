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
using TCG.Entity;
using TCG.Handlers;


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
            string work = objectHandlers.Post("work");
            switch (work)
            {
                case "DEL":
                    this.NewsClassDel();
                    break;
                case "Create":
                    this.NewsClassCreate();
                    break;
            }


            string action = objectHandlers.Post("iAction");
            switch (action)
            {
                
                case "MDY":
                    this.OrderMdy();
                    break;   
            }

            base.Finish();

        }
    }

    private void SearchInit()
    {
        base.conn.Dblink = DBLinkNums.News;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "Categories";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("vcClassName");
        arrshowfied.Add("vcName");
        arrshowfied.Add("vcDirectory");
        arrshowfied.Add("dUpdateDate");
        arrshowfied.Add("iOrder");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iOrder");
        sItem.arrSortField = arrsortfield;

        sItem.page = objectHandlers.ToInt(objectHandlers.Get("page"));
        sItem.pageSize = objectHandlers.ToInt(base.configService.baseConfig["PageSize"]);

        int iParent = objectHandlers.ToInt(objectHandlers.Get("iParentId"));
        sItem.strCondition = "iParent=" + iParent.ToString();
        this.iClassId.Value = iParent.ToString();

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
        Span classname = (Span)e.Item.FindControl("classname");
        Span lname = (Span)e.Item.FindControl("lname");
        Anchor directory = (Anchor)e.Item.FindControl("directory");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span sOrder = (Span)e.Item.FindControl("sOrder");

        CheckID.Text = Row["iId"].ToString();

        sOrder.Text = Row["iOrder"].ToString();
        
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
        int tClassID = objectHandlers.ToInt(objectHandlers.Post("DelClassId"));
        if (tClassID == 0)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        int rtn = base.handlerService.newsClassHandlers.DelCategories(base.conn, tClassID, base.adminInfo.vcAdminName);
        base.AjaxErch(rtn.ToString());
        base.Finish();

    }

    private void NewsClassCreate()
    {
        int tClassID = objectHandlers.ToInt(objectHandlers.Post("DelClassId"));
        if (tClassID == 0)
        {
            base.AjaxErch("<a>生成失败,分类ID为0!</a>");
            base.Finish();
            return;
        }

        Categories cif = base.handlerService.newsClassHandlers.GetCategoriesById(base.conn, tClassID, false);

        if (cif == null)
        {
            base.AjaxErch("<a>生成失败,分类信息不存在!</a>");
            base.Finish();
            return;
        }


        Template tlif = base.handlerService.templateHandlers.GetTemplateByID(base.conn, cif.iListTemplate,false);
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
            filepath = Server.MapPath("~" + cif.vcUrl + base.configService.baseConfig["FileExtension"]);
        }
        catch
        {
            base.AjaxErch("<a>生成失败,分类保存路径读取失败!</a>");
            base.Finish();
            return;
        }

        TCGTagHandlers tcgthdl = base.handlerService.TCGTagHandlers;
        tcgthdl.Template = tlif.Content.Replace("_$ClassId$_", tClassID.ToString());
        tcgthdl.FilePath = filepath;
        tcgthdl.WebPath = cif.vcUrl + base.configService.baseConfig["FileExtension"];
        if (tcgthdl.Replace(base.conn, base.configService.baseConfig))
        {   
            string text1 = "";
            if (tcgthdl.PagerInfo.PageCount > 1)
            {
                
                for (int i = 1; i <= tcgthdl.PagerInfo.PageCount; i++)
                {
                    string num = (i == 1) ? "" : i.ToString();
                    text1 += "<a href='.." + cif.vcUrl + "' target='_blank'>生成成功:" + cif.vcUrl + "-c" + num + base.configService.baseConfig["FileExtension"] + "...</a>";
                }
            }
            else
            {
                text1 = "<a href='.." + cif.vcUrl + "' target='_blank'>生成成功:" + cif.vcUrl + base.configService.baseConfig["FileExtension"] + "...</a>";
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

    private void OrderMdy()
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

        Categories cif = base.handlerService.newsClassHandlers.GetCategoriesById(base.conn, iMdyID,false);
        if (cif == null)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }

        cif.iOrder = objectHandlers.ToInt(KeyValue);

        int rtn = base.handlerService.newsClassHandlers.UpdateCategories(base.conn, base.adminInfo.vcAdminName, cif);
        if (rtn < 0)
        {
            base.AjaxErch("-1");
            base.Finish();
            return;
        }
    
        base.Finish();
        cif = null;

    }
}

