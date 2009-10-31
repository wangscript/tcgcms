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
using TCG.Handlers;
using TCG.Pages;

using TCG.Entity;
using TCG.Handlers;

public partial class Common_Topic_click : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string topicid = objectHandlers.Get("topicid");
            bool shownum = objectHandlers.Get("shownum") == "true" ? true : false;
            if (string.IsNullOrEmpty(topicid))
            {
                if (shownum)
                {
                    Response.Write("document.write('0');");
                    Response.End();
                }
                return;
            }

            Resources nif = base.handlerService.newsInfoHandlers.GetNewsInfoById(base.conn, topicid);
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
            int rtn = base.handlerService.newsInfoHandlers.UpdateNewsInfo(base.conn, configService.baseConfig["FileExtension"], nif);
            base.conn.Close();
            Response.End();
        }
    }
}
