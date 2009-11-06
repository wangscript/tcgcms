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
using TCG.Entity;

using TCG.Handlers;
using TCG.Data;

public partial class Template_templatelist : adminMain
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
            string work = objectHandlers.Post("work");
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
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "Template";

        ArrayList arrshowfied = new ArrayList();
        arrshowfied.Add("Id");
        arrshowfied.Add("TemplateType");
        arrshowfied.Add("vcTempName");
        arrshowfied.Add("dUpdateDate");
        sItem.arrShowField = arrshowfied;

        ArrayList arrsortfield = new ArrayList();
        arrsortfield.Add("dUpdateDate DESC");
        sItem.arrSortField = arrsortfield;

        sItem.page = objectHandlers.ToInt(objectHandlers.Get("page"));
        sItem.pageSize = objectHandlers.ToInt(base.configService.baseConfig["PageSize"]);

        int SkinId = objectHandlers.ToInt(objectHandlers.Get("SkinId"));
        sItem.strCondition = "SkinId ='" + SkinId.ToString() + "' AND iSystemType =" + 0;
        this.iSiteId.Value = SkinId.ToString();
        iSite = SkinId;

        string iParentid = objectHandlers.Get("iParentid");
        if (iParentid.Length != 36) iParentid = "0";
        sItem.strCondition += " AND iParentid='" + iParentid + "'";
        this.iParentid.Value = iParentid.ToString();


        string tt = objectHandlers.Get("iType");
        int iType = -1;
        if (!string.IsNullOrEmpty(tt))
        {
            iType = objectHandlers.ToInt(objectHandlers.Get("iType"));
            sItem.strCondition += " AND iType=" + iType.ToString();
        }

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
        this.pager.SetItem("SkinId", SkinId);
        this.pager.SetItem("iParentid", iParentid);
        this.pager.SetItem("iType", iType);
        this.pager.Total = count;
        this.pager.Calculate();

        if (ds.Tables.Count != 0)
        {
            this.ItemRepeater.DataSource = ds;
            this.ItemRepeater.DataBind();
        }

        foreach (Option option in base.configService.templateTypes.Values)
        {
            this.sType.Items.Add(new ListItem(option.Text, option.Value));
            int i = objectHandlers.ToInt(option.Value);
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

        CheckID.Text = Row["Id"].ToString();
        sId.Text = Row["Id"].ToString();

        string text = "<a href=\"?iParentid=" + Row["Id"].ToString() + "&SkinId=" + iSite.ToString() + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/12.gif\" border=\"0\"></a>";
        text += "<a href=\"templatemdy.aspx?templateid=" + Row["Id"].ToString() + "\" title=\"修改模版\">"
            + "<img src=\"../images/icon/11.gif\" border=\"0\"></a>";
        classname.Text = text + Row["vcTempName"].ToString();
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void TemplateDel()
    {
        string temps = objectHandlers.Post("temps");
        if (string.IsNullOrEmpty(temps))
        {
            base.ajaxdata = "{state:false,message:\"需要删除的记录编号不能为空！\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        int rtn = 0;
        try
        {
            rtn = base.handlerService.skinService.templateHandlers.DelTemplate(base.conn, base.adminInfo.vcAdminName, temps);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" +objectHandlers.JSEncode(  ex.Message.ToString() )+ "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch("{state:true,message:'模板删除成功！'}");
        base.Finish();
    }

    private void TemplateCreate()
    {
        string iTemplate = objectHandlers.Post("iTemplateId");
        if (string.IsNullOrEmpty(iTemplate))
        {
            base.AjaxErch("{state:false,message:'<a>生成失败-模版ID不能为0！</a>'}");
            return;
        }

        Template tlif = base.handlerService.skinService.templateHandlers.GetTemplateByID( iTemplate,false);

        if (tlif == null)
        {
            base.AjaxErch("{state:false,message:'<a>生成失败-编号：" + iTemplate.ToString() + "的模版不存在！</a>'}");
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
                filepath = Server.MapPath("~" + tlif.vcUrl + base.configService.baseConfig["FileExtension"]);
            }
        }
        catch
        {
            base.AjaxErch("{state:false,message:'<a>生成失败-模版生成文件路径不存在!</a>'}");
            return;
        }

        if (needTCG)
        {
            TCGTagHandlers tcgthdl = base.tagService.TCGTagHandlers;
            tcgthdl.Template = tlif.Content;
            tcgthdl.FilePath = filepath;
            if (tcgthdl.Replace(base.conn, base.configService.baseConfig))
            {
                base.AjaxErch("{state:true,message:'<a>生成成功:" + objectHandlers.JSEncode( filepath) + "...</a>'}");
            }
            else
            {
                base.AjaxErch("{state:false,message:<a>生成失败-系统错误!</a>'}");
            }
            tcgthdl = null;
        }
        else
        {
            try
            {
                objectHandlers.SaveFile(filepath, tlif.Content);
                base.AjaxErch("{state:true,message:<a>生成成功:" + objectHandlers.JSEncode( filepath )+ "...</a>'}");
            }
            catch
            {
                base.AjaxErch("{state:false,message:<a>生成失败-系统错误!</a>'}");

            }

        }

        tlif = null;
    }
}
