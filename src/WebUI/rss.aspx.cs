using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;


using TCG.Entity;
using TCG.Utils;
using System.Collections;


namespace TCG.CMS.WebUi
{
    public partial class rss : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.ContentType = "text/XML";
                StringBuilder sb = new StringBuilder();

                sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sb.Append("<rss version=\"2.0\">");
                sb.Append("<channel>");
                sb.Append("<title>" + ConfigServiceEx.baseConfig["WebTitle"] + "</title>");
                sb.Append("<link>" + ConfigServiceEx.baseConfig["WebSite"] + "</link>");
                sb.Append("<description>" + ConfigServiceEx.baseConfig["WebDescription"] + "</description>");
                sb.Append("<language>zh-cn</language>");
                sb.Append("<generator></generator>");
                sb.Append("<webmaster>sanyungui@tcgcms.cn</webmaster>");


                Dictionary<string, EntityBase> res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesList(158, "0", "", "dAddDate DESC", true, false, true, true);
                if (res != null && res.Count != 0)
                {
                    foreach (KeyValuePair<string, EntityBase> entity in res)
                    {
                        Resources tempres = (Resources)entity.Value;
                        sb.Append("<item>");
                        string text1 = (string.IsNullOrEmpty(tempres.vcUrl)) ? ConfigServiceEx.baseConfig["WebSite"] + tempres.vcFilePath : tempres.vcUrl;
                        sb.Append("<link>" + text1 + "</link>");
                        sb.Append("<title><![CDATA[" + tempres.vcTitle + "]]></title>");
                        sb.Append("<author></author>");
                        sb.Append("<category>" + tempres.Categorie.vcClassName + "</category>");
                        sb.Append("<pubDate>" + tempres.dAddDate.ToString() + "</pubDate>");
                        sb.Append("<guid>" + text1 + "</guid>");
                        sb.Append("<description>" + tempres.vcShortContent + "</description>");
                        sb.Append("</item>");

                    }
                }

                sb.Append("</channel>");
                sb.Append("</rss>");
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }
}