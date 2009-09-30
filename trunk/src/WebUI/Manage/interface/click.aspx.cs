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
using TCG.Template.Handlers;
using TCG.Pages;

using TCG.News.Entity;
using TCG.News.Handlers;

public partial class Manage_interface_click : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Fetch.IsGetFromAnotherDomain || Fetch.IsPostFromAnotherDomain)
            {
                return;
            }

            int topicid = Bases.ToInt(Fetch.Get("topicid"));
            bool shownum = Fetch.Get("shownum") == "true" ? true : false;
            if (topicid == 0)
            {
                if (shownum)
                {
                    Response.Write("document.write('0');");
                    Response.End();
                }
                return;
            }
            newsInfoHandlers nifh = new newsInfoHandlers();
            NewsInfo nif = nifh.GetNewsInfoById(base.conn, topicid);
            if (nif == null)
            {
                if (shownum)
                {
                    Response.Write("document.write('0');");
                    Response.End();
                }
                return;
            }

            if (shownum) Response.Write("document.write('" + nif.iCount.ToString() + "');");

            nif.iCount = nif.iCount + 1;
            int outid = 0;
            int rtn = nifh.UpdateNewsInfo(base.conn, config["FileExtension"], nif, ref outid);
            base.conn.Close();
            Response.End();
        }
    }
}
