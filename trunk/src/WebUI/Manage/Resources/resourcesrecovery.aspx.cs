using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Entity;
using TCG.Controls.HtmlControls;


using TCG.Data;
using TCG.Handlers;

public partial class resources_resourcesrecovery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

        if (!Page.IsPostBack)
        {
            this.SearchInit();
        }
        else
        {
            string Action = objectHandlers.Post("iAction");
            switch (Action)
            {
                case "SAVE":
                    this.SaveNews();
                    break;
                case "DEL":
                    this.DelNews();
                    break;
            }
            return;
        }
    }

    private void SearchInit()
    {
        PageSearchItem sItem = new PageSearchItem();
        sItem.tableName = "Resources";


        int page = objectHandlers.ToInt(objectHandlers.Get("page"));
        int pageSize = objectHandlers.ToInt(ConfigServiceEx.baseConfig["PageSize"]);
        string strCondition = " cDel ='Y'";

        int curPage = 0;
        int pageCount = 0;
        int count = 0;

        Dictionary<string, EntityBase> res = null;
        try
        {
            res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                   page, pageSize, "iId DESC", strCondition);
        }
        catch (Exception ex)
        {

        }


        this.pager.Per = pageSize;
        this.pager.Total = count;
        this.pager.Calculate();

        if (res != null && res.Count != 0)
        {
            this.ItemRepeater.DataSource = res.Values;
            this.ItemRepeater.DataBind();
        }

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Resources res = (Resources)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Span sId = (Span)e.Item.FindControl("sId");
        Span sTitle = (Span)e.Item.FindControl("sTitle");
        Span sClassName = (Span)e.Item.FindControl("sClassName");
        Span sChecked = (Span)e.Item.FindControl("sChecked");
        Span sCreated = (Span)e.Item.FindControl("sCreated");
        Span updatedate = (Span)e.Item.FindControl("updatedate");

        CheckID.Text = res.Id;
        sId.Text = res.Id;
        string check = res.cChecked;
        string Created = res.cCreated;
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

        sTitle.Text = res.vcTitle;
        sClassName.Text = "<script type=\"text/javascript\">ShowClassNameByClassID('" + res.Categorie.Id + "');</script>";

        updatedate.Text = res.dUpDateDate.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void SaveNews()
    {
        string delids = objectHandlers.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch(-1000000051,"");
            return;
        }

        int rtn = 0;

        try
        {
            rtn = base.handlerService.resourcsService.resourcesHandlers.SaveOrDelResource(delids,"SAVE",base.adminInfo);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "", "refinsh()");

    }

    private void DelNews()
    {
        string delids = objectHandlers.Post("DelClassId");
        if (string.IsNullOrEmpty(delids))
        {
            base.AjaxErch(-1000000051, "");
            return;
        }

        int rtn = 0;

        try
        {
            rtn = base.handlerService.resourcsService.resourcesHandlers.SaveOrDelResource(delids, "DEL", base.adminInfo);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "", "refinsh()");
    }
}
