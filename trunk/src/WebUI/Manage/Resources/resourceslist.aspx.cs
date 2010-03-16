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
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

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
        base.handlerService.resourcsService.resourcesHandlers.SetDataBaseConnection();

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

        string iClassId = objectHandlers.Get("iClassId");
        if (string.IsNullOrEmpty(iClassId)) iClassId = "0";
        this.iClassId.Value = iClassId.ToString();

        string skinId = objectHandlers.Get("SkinId");
        if (string.IsNullOrEmpty(skinId))
        {
            skinId = base.configService.DefaultSkinId;
        }

        this.iSkinId.Value = skinId;

        string allchild = string.Empty;

        if (iClassId == "0")
        {
            allchild = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesIndexBySkinId(skinId);
        }
        else
        {
            allchild = base.handlerService.skinService.categoriesHandlers.GetCategoriesChild(iClassId);
        }

        sItem.strCondition = "iClassID in (" + allchild + ")";

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

        string text = "<a href=\"resourceshandlers.aspx?newsid=" + Row["iId"].ToString() + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/11.gif\" border=\"0\"></a>";
        sTitle.Text = text + "<a href='../.." + Row["vcFilePath"].ToString() + "' target='_blank'>" + Row["vcTitle"].ToString() + "</a>";
        sClassName.Text = "<script type=\"text/javascript\">ShowClassNameByClassID('" + Row["iClassID"].ToString() + "');</script>";

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

        int rtn = 0;

        try
        {
            rtn = base.handlerService.resourcsService.resourcesHandlers.DelNewsInfoHtmlByIds(delids);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "文章修改成功！", "refinsh()");
    }

    private void CreateNews()
    {
        string resourceid = objectHandlers.Post("DelClassId");
        if (string.IsNullOrEmpty(resourceid))
        {
            base.AjaxErch("-1000000051");
            return;
        }


        Resources item = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(resourceid);
        if (item == null) return;

        TCGTagHandlers tcgth = base.tagService.TCGTagHandlers;
        tcgth.Template = item.Categorie.ResourceTemplate.Content.Replace("_$Id$_", resourceid);
        tcgth.FilePath = Server.MapPath("~" + item.vcFilePath);
        tcgth.WebPath = item.vcFilePath;
        tcgth.configService = base.configService;
        tcgth.conn = base.conn;

        string text1 = string.Empty;
        try
        {
            tcgth.Replace();
            if (item.cChecked != "Y")
            {
                item.cChecked = "Y";
                base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
            }
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, objectHandlers.JSEncode(ex.Message.ToString()), "CreateBack");
            return;
        }

        text1 = "<a href=\"" + item.vcFilePath + "\" target=\"_blank\">生成成功:" + item.vcFilePath + "</a>";
        base.AjaxErch(1, text1, "CreateBack");
    }
}
