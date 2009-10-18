using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using TCG.Utils;

using TCG.Entity;
using TCG.Pages;
using TCG.Handlers;


public partial class View : FilesMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            long iId = objectHandlers.ToLong(objectHandlers.Get("fileId"));
            this.ImageShow.Src = "attach.aspx?attach=" + iId.ToString() ;
        }
    }
}
