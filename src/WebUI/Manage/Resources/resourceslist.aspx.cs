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


public partial class resources_resourceslist : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.SearchInit();
        }
        else
        {
            string Action = objectHandlers.Post("iAction");
            switch (Action)
            {
                case "DEL":
                    this.DelNews();
                    break;
                case "CREATE":
                    this.CreateNews();
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
        sItem.tableName = "Resources";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("vcTitle");
        arrshowfied.Add("iClassID");
        arrshowfied.Add("cChecked");
        arrshowfied.Add("cCreated");
        arrshowfied.Add("dUpDateDate");
        arrshowfied.Add("vcFilePath");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = objectHandlers.ToInt(objectHandlers.Get("page"));
        sItem.pageSize = objectHandlers.ToInt(base.configService.baseConfig["PageSize"]);

        int iClassId = objectHandlers.ToInt(objectHandlers.Get("iClassId"));
        this.iClassId.Value = iClassId.ToString();

        string allchild = base.handlerService.newsClassHandlers.GetAllChildCategoriesIdByCategoriesId(base.conn, iClassId, false);
        sItem.strCondition = "iClassID in (" + allchild + ")";
        //sItem.strCondition = "iClassID = " + iClassId.ToString();

        string check = objectHandlers.Get("check");
        if (!string.IsNullOrEmpty(check))
        {
            sItem.strCondition += " AND cChecked ='" + check + "'";
        }

        string create = objectHandlers.Get("create");
        if (!string.IsNullOrEmpty(create))
        {
            sItem.strCondition += " AND cCreated ='" + create + "'";
        }

        int Speciality = objectHandlers.ToInt(objectHandlers.Get("Speciality"));
        this.iSpeciality.Value = Speciality.ToString();
        if (Speciality != 0)
        {
            sItem.strCondition += " AND dbo.IsSpeciality(vcSpeciality,'" + Speciality.ToString() + "') >0 ";
        }

        sItem.strCondition += " AND cDel ='N'";

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
        this.pager.SetItem("iClassId", iClassId);
        this.pager.SetItem("check", check);
        this.pager.SetItem("create", create);
        this.pager.SetItem("Speciality", Speciality);
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

        Span sClassName = (Span)e.Item.FindControl("sClassName");
        Span sChecked = (Span)e.Item.FindControl("sChecked");
        Span sCreated = (Span)e.Item.FindControl("sCreated");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span sTitle = (Span)e.Item.FindControl("sTitle");

        CheckID.Text = Row["iId"].ToString();

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

        string text = "<a href=\"newsadd.aspx?newsid=" + Row["iId"].ToString() + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/11.gif\" border=\"0\"></a>";
        sTitle.Text = text + "<a href='../.." + Row["vcFilePath"].ToString() + "' target='_blank'>" + Row["vcTitle"].ToString() + "</a>";
        sClassName.Text = "<script type=\"text/javascript\">ShowClassNameByClassID(" + Row["iClassID"].ToString() + ");</script>";

        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 删除文章
    /// </summary>
    private void DelNews()
    {
        string delids = objectHandlers.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch("-1000000051");
            return;
        }

        int rtn = base.handlerService.newsInfoHandlers.DelNewsInfosWithLogic(base.conn, base.adminInfo.vcAdminName, "Y", delids);
        rtn = base.handlerService.newsInfoHandlers.DelNewsInfoHtmlByIds(base.conn, base.configService.baseConfig, delids);
        base.AjaxErch(rtn.ToString());
    }

    private void CreateNews()
    {
        string id = objectHandlers.Post("DelClassId");
        if (string.IsNullOrEmpty(id))
        {
            base.AjaxErch("-1000000051");
            return;
        }

        if (base.configService.baseConfig["IsReWrite"] == "True")
        {
            base.AjaxErch("<a>系统启用URL重写，无须生成</a>");
        }

        Resources item = base.handlerService.newsInfoHandlers.GetNewsInfoById(base.conn, id);
        if (item == null) return;

        Categories cif = base.handlerService.newsClassHandlers.GetCategoriesById(base.conn, item.ClassInfo.iId, false);

        Template titem = base.handlerService.templateHandlers.GetTemplateByID(base.conn, cif.iTemplate,false);
        cif = null;

        TCGTagHandlers tcgth = base.handlerService.TCGTagHandlers;
        tcgth.Template = titem.Content.Replace("_$Id$_", id.ToString());
        tcgth.FilePath = Server.MapPath("~" + item.vcFilePath);
        tcgth.WebPath = item.vcFilePath;
        titem = null;
        if (tcgth.Replace(base.conn, base.configService.baseConfig))
        {
            base.handlerService.newsInfoHandlers.UpdateNewsInfosCreate(base.conn, base.adminInfo.vcAdminName, "Y", id.ToString());
        }

        string text1 = "";
        if (tcgth.PagerInfo.PageCount > 1)
        {

            for (int i = 1; i <= tcgth.PagerInfo.PageCount; i++)
            {
                string num = (i == 1) ? "" : i.ToString();
                text1 += "<a href='.." + item.vcFilePath + "' target='_blank'>生成成功:" + item.vcFilePath.Substring(0, item.vcFilePath.LastIndexOf(".")) + num +
                    item.vcFilePath.Substring(item.vcFilePath.LastIndexOf("."), item.vcFilePath.Length - item.vcFilePath.LastIndexOf(",")) + "...</a>";
            }
        }
        else
        {
            text1 = "<a href='.." + item.vcFilePath + "' target='_blank'>生成成功:" + item.vcFilePath + "...</a>";
        }

        base.AjaxErch(text1);
        tcgth = null;
    }
}
