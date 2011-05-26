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
using TCG.Controls.HtmlControls;
using TCG.Data;
using TCG.Handlers;
using TCG.Entity;

public partial class Manage_feedback : BasePage
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
            string work = objectHandlers.Post("work");
            switch (work)
            {
                case "DEL":
                    this.DelFeedBack();
                    break;
            }
            return;
        }
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        FeedBack feedBack = (FeedBack)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Span ipid = (Span)e.Item.FindControl("ipid");
        Span cid = (Span)e.Item.FindControl("cid");
        Span lname = (Span)e.Item.FindControl("lname");
        Span sqq = (Span)e.Item.FindControl("sqq");
        Span stel = (Span)e.Item.FindControl("stel");
        Span sadddate = (Span)e.Item.FindControl("sadddate");
        Span scontent = (Span)e.Item.FindControl("scontent");
        scontent.Text = objectHandlers.HtmlEncode(feedBack.Content);

        CheckID.Text = feedBack.Id;
        cid.Text = feedBack.Id;
        ipid.Text = feedBack.Id;

        string text = "<a href=\"javascript:GoTo();\" title=\"查看留言内容\" onclick=\"ShowContent("+feedBack.Id+")\">"
            + "<img src=\"../images/icon/magnifier.png\" border=\"0\"></a>";

        lname.Text = text + feedBack.UserName;
        sqq.Text = feedBack.QQ;
        stel.Text = feedBack.Tel;
        sadddate.Text = feedBack.AddDate.ToString();

    }

    private void DelFeedBack()
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
            rtn = base.handlerService.feedBackHandlers.DelFeedBackById(base.adminInfo, delids);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.AjaxErch(rtn, "", "refinsh()");
    }

    private void SearchInit()
    {
        this.iSkinId.Value = ConfigServiceEx.DefaultSkinId;

        int page = objectHandlers.ToInt(objectHandlers.Get("page"));
        int pageSize = objectHandlers.ToInt(ConfigServiceEx.baseConfig["PageSize"]);

        int curPage = 0;
        int pageCount = 0;
        int count = 0;


        Dictionary<string, EntityBase> res = null;
        try
        {
            res = base.handlerService.feedBackHandlers.GeFeedBackListPager(ref curPage, ref pageCount, ref count,
                   page, pageSize, "Id DESC", " skinid='" + ConfigServiceEx.DefaultSkinId + "'");
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
}