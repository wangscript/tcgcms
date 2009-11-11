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

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;

using TCG.Handlers;
using TCG.Entity;

public partial class Skin_skins : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Dictionary<string, EntityBase> skins = base.handlerService.skinService.skinHandlers.GetAllSkinEntity();
            if (skins != null && skins.Count != 0)
            {
                this.ItemRepeater.DataSource = skins.Values;
                this.ItemRepeater.DataBind();
            }
        }
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Skin skininfo = (Skin)e.Item.DataItem;
        Anchor sitename = (Anchor)e.Item.FindControl("sitename");
        Span info = (Span)e.Item.FindControl("info");
        Img pic = (Img)e.Item.FindControl("pic");
        HtmlGenericControl IsDefault = (HtmlGenericControl)e.Item.FindControl("IsDefault");
        Span sid = (Span)e.Item.FindControl("sid");

        info.Text = skininfo.Info;
        sitename.Text = skininfo.Name;
        pic.Src = skininfo.Pic;
        sitename.Href = "skinveiw.aspx?skinid=" + skininfo.Id;
        if (skininfo.Id == base.configService.DefaultSkinId)
        {
            IsDefault.Visible = true;
        }
        else
        {
            IsDefault.Visible = false;
        }
        sid.Text = skininfo.Id;
    }
}

