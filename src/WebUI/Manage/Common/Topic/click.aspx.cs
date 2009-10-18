﻿using System;
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
            int topicid = objectHandlers.ToInt(objectHandlers.Get("topicid"));
            bool shownum = objectHandlers.Get("shownum") == "true" ? true : false;
            if (topicid == 0)
            {
                if (shownum)
                {
                    Response.Write("document.write('0');");
                    Response.End();
                }
                return;
            }
            NewsInfoHandlers nifh = new NewsInfoHandlers();
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
            int rtn = nifh.UpdateNewsInfo(base.conn, configService.baseConfig["FileExtension"], nif, ref outid);
            base.conn.Close();
            Response.End();
        }
    }
}
