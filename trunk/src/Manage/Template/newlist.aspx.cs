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
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Template.Entity;
using TCG.Manage.Utils;
using TCG.Template.Handlers;
using TCG.Template.Utils;
using TCG.Data;

using TCG.TCGTagReader.Handlers;

public partial class Template_newlist : adminMain
{
    public int iSite = 0;
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
                    this.TemplateDel();
                    break;
                case "Create":
                    this.TemplateCreate();
                    break;
            }
            base.Finish();
        }
    }

    private void SearchInit()
    {
        base.conn.Dblink = DBLinkNums.Template;
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "T_Template_TemplateInfo";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("iId");
        arrshowfied.Add("iType");
        arrshowfied.Add("vcTempName");
        arrshowfied.Add("dUpdateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("dUpdateDate DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = Bases.ToInt(Fetch.Get("page"));
        sItem.pageSize = Bases.ToInt(base.config["PageSize"]);

        int iSiteId = Bases.ToInt(Fetch.Get("iSiteId"));
        sItem.strCondition = "iSiteId=" + iSiteId.ToString() + " AND iSystemType =" + TemplateConstant.SystemType_News;
        this.iSiteId.Value = iSiteId.ToString();
        iSite = iSiteId;

        int iParentid = Bases.ToInt(Fetch.Get("iParentid"));
        sItem.strCondition += " AND iParentid=" + iParentid.ToString();
        this.iParentid.Value = iParentid.ToString();

        string tt = Fetch.Get("iType");
        int iType = -1;
        if (!string.IsNullOrEmpty(tt))
        {
            iType = Bases.ToInt(Fetch.Get("iType"));
            sItem.strCondition += " AND iType=" + iType.ToString();
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
        this.pager.SetItem("iSiteId", iSiteId);
        this.pager.SetItem("iParentid", iParentid);
        this.pager.SetItem("iType", iType);
        this.pager.Total = count;
        this.pager.Calculate();

        if (ds.Tables.Count != 0)
        {
            this.ItemRepeater.DataSource = ds;
            this.ItemRepeater.DataBind();
        }

        for (int i = 0; i < TemplateConstant.TypeNames().Count; i++)
        {
            this.sType.Items.Add(new ListItem(TemplateConstant.TypeNames()[i].ToString(), i.ToString()));
            if (iType == i)
            {
                this.sType.SelectedIndex = i + 1;
            }
        }

        base.Finish();

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Span sId = (Span)e.Item.FindControl("sId");
        Span classname = (Span)e.Item.FindControl("classname");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        CheckID.Text = Row["iId"].ToString();
        sId.Text = Row["iId"].ToString();

        string text = "<a href=\"?iParentid=" + Row["iId"].ToString() + "&iSiteId=" + iSite.ToString() + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
        text += "<a href=\"newtemplatemdy.aspx?templateid=" + Row["iId"].ToString() + "\" title=\"修改模版\">"
            + "<img src=\"../images/icon/11.gif\" border=\"0\"></a>";
        classname.Text = text + Row["vcTempName"].ToString();
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void TemplateDel()
    {
        string temps = Fetch.Post("temps");
        if (string.IsNullOrEmpty(temps))
        {
            base.AjaxErch("-1");
            base.Finish();
        }
        TemplateHandlers nthdl = new TemplateHandlers();

        int rtn = nthdl.DelTemplate(base.conn, base.admin.adminInfo.vcAdminName, temps);
        base.AjaxErch(rtn.ToString());
        nthdl = null;
        base.Finish();
    }

    private void TemplateCreate()
    {
        int iTemplate = Bases.ToInt(Fetch.Post("iTemplateId"));
        if (iTemplate == 0)
        {
            base.AjaxErch("<a>生成失败-模版ID不能为0！</a>");
            return;
        }
        TemplateHandlers tlhdl = new TemplateHandlers();
        TemplateInfo tlif = tlhdl.GetTemplateInfoByID(base.conn, iTemplate);
        tlhdl = null;
        if (tlif == null)
        {
            base.AjaxErch("<a>生成失败-编号：" + iTemplate.ToString() + "的模版不存在！</a>");
            return;
        }
        string filepath = string.Empty;
        bool needTCG = true;
        try
        {
            if (tlif.vcUrl.IndexOf(".") > -1)
            {
                filepath = Server.MapPath("~" + tlif.vcUrl);
                if (tlif.vcUrl.Substring(tlif.vcUrl.LastIndexOf(".") + 1, tlif.vcUrl.Length - tlif.vcUrl.LastIndexOf(".") - 1) == "aspx")
                {
                    needTCG = false;
                }
            }
            else
            {
                filepath = Server.MapPath("~" + tlif.vcUrl + base.config["FileExtension"]);
            }
        }
        catch
        {
            base.AjaxErch("<a>生成失败-模版生成文件路径不存在!</a>");
            return;
        }

        if (needTCG)
        {
            TCGTagHandlers tcgthdl = new TCGTagHandlers();
            tcgthdl.Template = tlif.vcContent;
            tcgthdl.FilePath = filepath;
            if (tcgthdl.Replace(base.conn, base.config))
            {
                base.AjaxErch("<a>生成成功:" + filepath + "...</a>");
            }
            else
            {
                base.AjaxErch("<a>生成失败-系统错误!</a>");
            }
            tcgthdl = null;
        }
        else
        {
            try
            {
                Text.SaveFile(filepath, tlif.vcContent);
                base.AjaxErch("<a>生成成功:" + filepath + "...</a>");
            }
            catch
            {
                base.AjaxErch("<a>生成失败-系统错误!</a>");

            }

        }

        tlif = null;
    }
}
