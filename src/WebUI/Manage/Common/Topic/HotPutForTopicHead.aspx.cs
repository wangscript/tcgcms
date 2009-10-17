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

using TCG.Handlers;

public partial class Common_Topic_HotPutForTopicHead : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fileinfoHandlers flih = new fileinfoHandlers();
        int i =  flih.GetUrlError("http://www1.91wang.com/images/gamelogo/tg129m.gif");
    }
}
