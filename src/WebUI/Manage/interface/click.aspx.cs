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
using TCG.Handlers;
using TCG.Pages;

using TCG.Entity;

public partial class Manage_interface_click : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (objectHandlers.IsGetFromAnotherDomain || objectHandlers.IsPostFromAnotherDomain)
            {
                return;
            }

            string topicid = objectHandlers.Get("rid");
            string categoriesid = objectHandlers.Get("cid");
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

            if (string.IsNullOrEmpty(categoriesid))
            {
                if (shownum)
                {
                    Response.Write("document.write('0');");
                    Response.End();
                }
                return;
            }

            Resources nif = base.handlerService.resourcsService.resourcesHandlers.GetNewsInfoById(categoriesid, topicid);
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
            int rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateNewsInfo(nif);
            base.conn.Close();
            Response.End();
        }
    }
}
