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

using TCG.Utils;
using TCG.Data;
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Manage.Utils;

using TCG.Files.Utils;
using TCG.Files.Entity;
using TCG.Files.Handlers;

public partial class files_fileinfos : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int ClassId = Bases.ToInt(Fetch.Get("iClassId"));
            this.iClassId.Value = ClassId.ToString();

            fileclasshandlers fchdl = new fileclasshandlers();
            DataTable dt = fchdl.GetFilesClassInfosByParendId(base.conn, ClassId);
            if (dt != null)
            {
                this.ItemRepeater.DataSource = dt;
                this.ItemRepeater.DataBind();
            }
            this.SearchInit();
        }
        else
        {
            string Action = Fetch.Post("work");
            switch (Action)
            {
                case "AddClass":
                    this.AddClass();
                    break;
            }
            base.Finish();
            return;
        }

    }

    private void SearchInit()
    {
        base.conn.Dblink = FilesConst.FilesDbLinks[0];
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_Files_FileInfos";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("vcFileName");
        arrshowfied.Add("iSize");
        arrshowfied.Add("vcType");
        arrshowfied.Add("dCreateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("iId DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = Bases.ToInt(Fetch.Get("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);

        int tClassId = Bases.ToInt(Fetch.Get("iClassId"));
        sItem.strCondition = "iClassID = " + tClassId.ToString();

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
        this.pager.SetItem("iClassId", tClassId);
        this.pager.Total = count;
        this.pager.Calculate();

        if (ds.Tables.Count != 0)
        {
            this.ItemRepeaterFile.DataSource = ds;
            this.ItemRepeaterFile.DataBind();
        }

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Span FileClassID = (Span)e.Item.FindControl("FileClassID");
        Span sId = (Span)e.Item.FindControl("sId");
        Span sFileClassId = (Span)e.Item.FindControl("sFileClassId");
        
        Span sTitle = (Span)e.Item.FindControl("sTitle");
        Span sInfo = (Span)e.Item.FindControl("sInfo");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        FileClassID.Text = Row["iId"].ToString();
        sFileClassId.Text = Row["iId"].ToString();
        string text = "<a href=\"?iClassId=" + Row["iId"].ToString() + "\" title=\"查看子分类\">"
           + "<img src=\"" + base.config["WebSite"] + "images/icon/12.gif\" border=\"0\"></a>";
        sTitle.Text = text + Row["vcFileName"].ToString();
        sInfo.Text = Row["vcMeno"].ToString();
        updatedate.Text = Row["dCreateDate"].ToString();
    }

    protected void ItemRepeaterFile_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Span FileClassID = (Span)e.Item.FindControl("FileClassID");
        Span sId = (Span)e.Item.FindControl("sId");
        Span sFileClassId = (Span)e.Item.FindControl("sFileClassId");

        Span sTitle = (Span)e.Item.FindControl("sTitle");
        Span sInfo = (Span)e.Item.FindControl("sInfo");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span ssize = (Span)e.Item.FindControl("ssize");
        Img sIco = (Img)e.Item.FindControl("sIco");
        

       // FileClassID.Text = Row["iId"].ToString();
        //sFileClassId.Text = Row["iId"].ToString();
        sTitle.Text = Row["iId"].ToString();

        sInfo.Text = "<a href='../fileView-" + Row["iId"].ToString() + "" + base.config["FileExtension"] + "' target='_blank'>" + Row["vcFileName"].ToString() + "</a>";
        ssize.Text = Row["iSize"].ToString() + "K";
        updatedate.Text = Row["dCreateDate"].ToString();
        sIco.Src = "../images/icon/f_" + Row["vcType"].ToString() + ".gif";
    }

    private void AddClass()
    {
        FileClassInfo item = new FileClassInfo();
        item.vcFileName = Fetch.Post("inTitle");
        item.vcMeno = Fetch.Post("inInfo");
        item.iParentId = Bases.ToInt(Fetch.Post("iClassId"));
        if (string.IsNullOrEmpty(item.vcFileName))
        {
            base.AjaxErch("-1000000057");
            return;
        }

        if (string.IsNullOrEmpty(item.vcMeno))
        {
            base.AjaxErch("-1000000058");
            return;
        }

        fileclasshandlers fchd = new fileclasshandlers();
        int rtn = fchd.AddFileClass(base.conn, base.admin.adminInfo.vcAdminName, item);
        if (rtn == 1)
        {
            Caching.Remove(FilesConst.CACHING_ALL_FILECLASS);
            string text1 = fchd.GetFilesPathByClassId(base.conn, item.iParentId);
            string text2 = "~" + text1 + item.vcFileName + @"/";
            text2 = Server.MapPath(text2);
            Text.SaveFile(text2, "");
        }
        base.AjaxErch(rtn.ToString());
        fchd = null;
    }
}
