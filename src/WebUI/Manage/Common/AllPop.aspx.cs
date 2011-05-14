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

using TCG.Handlers;
using TCG.Entity;

public partial class Common_AllPop : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("var _Popedom=[");
            Dictionary<int, Popedom> allpop = base.handlerService.manageService.adminHandlers.GetAllPopedomEntity();

            if (allpop != null && allpop.Count != 0)
            {
                int i = 0;
                foreach (KeyValuePair<int, Popedom> keyvalue in allpop)
                {
                    sb.Append("{");
                    sb.Append("Id:" + keyvalue.Value.iID + ",");
                    sb.Append("Name:'" + keyvalue.Value.vcPopName + "',");
                    sb.Append("Addtime:'" + keyvalue.Value.dAddtime.ToString() + "',");
                    sb.Append("ParentId:" + keyvalue.Value.iParentId.ToString() + "");
                    sb.Append("}");
                    i++;
                    if (i != allpop.Count) sb.Append(",");
                }
            }

            sb.Append("];");
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}
