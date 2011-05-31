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


public partial class resources_resourceslist : BasePage
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
                case "DEL":
                    this.DelNews();
                    break;
                case "CREATE":
                    this.CreateNews();
                    break;
                case "CHECK" :
                    this.CheckNews();
                    break;
            }
            return;
        }
    }

    private void SearchInit()
    {
       

        int page = objectHandlers.ToInt(objectHandlers.Get("page"));
        int pageSize = objectHandlers.ToInt(ConfigServiceEx.baseConfig["PageSize"]);

        string iClassId = objectHandlers.Get("iClassId");
        if (string.IsNullOrEmpty(iClassId)) iClassId = "0";
        this.iClassId.Value = iClassId.ToString();

        string skinId = objectHandlers.Get("SkinId");
        if (string.IsNullOrEmpty(skinId))
        {
            skinId = ConfigServiceEx.DefaultSkinId;
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

        string strCondition = "iClassID in (" + allchild + ")";

        string check = objectHandlers.Get("check");
        if (!string.IsNullOrEmpty(check))
        {
            strCondition += " AND cChecked ='" + check + "'";
        }

        string create = objectHandlers.Get("create");
        if (!string.IsNullOrEmpty(create))
        {
            strCondition += " AND cCreated ='" + create + "'";
        }

        int Speciality = objectHandlers.ToInt(objectHandlers.Get("Speciality"));
        this.iSpeciality.Value = Speciality.ToString();
        //if (Speciality != 0)
        //{
        //    strCondition += " AND dbo.IsSpeciality(vcSpeciality,'" + Speciality.ToString() + "') >0 ";
        //}

        strCondition += " AND cDel ='N'";

        int curPage = 0;
        int pageCount = 0;
        int count = 0;

        
        Dictionary<string, EntityBase> res = null;
        try
        {
            res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                   page, pageSize, "Id DESC", strCondition);
        }
        catch (Exception ex)
        {

        }


        this.pager.Per = pageSize;
        this.pager.SetItem("iClassId", iClassId);
        this.pager.SetItem("check", check);
        this.pager.SetItem("create", create);
        this.pager.SetItem("Speciality", Speciality);
        this.pager.SetItem("skinid", skinId);
        this.pager.Total = count;
        this.pager.Calculate();

        if (res!=null&&res.Count != 0)
        {
            this.ItemRepeater.DataSource = res.Values;
            this.ItemRepeater.DataBind();
        }

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Resources res = (Resources)e.Item.DataItem;
        Span CheckID = (Span)e.Item.FindControl("CheckID");

        Span sClassName = (Span)e.Item.FindControl("sClassName");
        Span sChecked = (Span)e.Item.FindControl("sChecked");
        Span sCreated = (Span)e.Item.FindControl("sCreated");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span sTitle = (Span)e.Item.FindControl("sTitle");
        Span sId = (Span)e.Item.FindControl("sId");
        

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

        string text = "<a href=\"resourceshandlers.aspx?newsid=" + res.Id + "\" title=\"查看子分类\">"
            + "<img src=\"../images/icon/11.gif\" border=\"0\"></a>";
        string tstyle = string.Empty;
        if (!string.IsNullOrEmpty(res.vcTitleColor))
            tstyle = "style=' color:" + res.vcTitleColor + ";'";
        sTitle.Text = text + "<a href='" + res.GetUrl() + "' " + tstyle 
            + " target='_blank'>" + res.vcTitle + "</a>";
        sClassName.Text = "<script type=\"text/javascript\">ShowClassNameByClassID('" + res.Categorie.Id + "');</script>";

        updatedate.Text = res.dUpDateDate.ToString("yyyy-MM-dd HH:mm:ss");
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
        int resourceid =objectHandlers.ToInt(objectHandlers.Post("DelClassId"));
        if (resourceid<=0)
        {
            base.AjaxErch("-1000000051");
            return;
        }
        int rtn = 0;
        string errText = string.Empty;
        try
        {
            rtn = base.handlerService.tagService.CreateResourcHtmlById(ref errText, resourceid);
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, objectHandlers.JSEncode(ex.Message.ToString()), "CreateBack");
            return;
        }

        base.AjaxErch(rtn, "<a>" + errText.Replace("\\", "/") + "</a>", "CreateBack");
    }

    private void CheckNews()
    {
        int resourceid = objectHandlers.ToInt(objectHandlers.Post("DelClassId"));
        if (resourceid <= 0)
        {
            base.AjaxErch("-1000000051");
            return;
        }
        Resources res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesById(resourceid);
        if (res == null)
        {
            base.AjaxErch("-1000000051");
            return;
        }

        int rtn = 0;
        string errText = string.Empty;
        try
        {
            if (res.cChecked == "Y")
            {
                res.cChecked = "N";
                res.cCreated = "N";
                errText = "审核文章[" + res.vcTitle + "]状态为:<font color='red'>审核不通过</font> 并删除静态文件";
                rtn = base.handlerService.resourcsService.resourcesHandlers.DelNewsInfoHtmlByIds(res.Id);
            }
            else
            {
                res.cChecked = "Y";
                res.cCreated = "Y";
                rtn = base.handlerService.tagService.CreateResourcHtmlById(ref errText, resourceid);
                errText = "审核文章[" + res.vcTitle + "]状态为:<font color='green'>审核通过</font> 并生成静态文件";
            }

            rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(res);
            
        }
        catch (Exception ex)
        {
            base.AjaxErch(1, objectHandlers.JSEncode(ex.Message.ToString()), "CreateBack");
            return;
        }

        base.AjaxErch(rtn, "<a>" + errText.Replace("\\", "/") + "</a>", "CreateBack");
    }
}
