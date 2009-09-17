using System;
using System.IO;
using System.Web.UI;
using System.Web.Caching;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.News.Kernel;
using TCG.TCGTagReader.Handlers;

namespace TCG.News.Kernel
{
    public class NewsMain : Origin
    {
        public NewsMain()
        {

        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            StringWriter html = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(html);
            base.Render(tw);
            string outhtml = html.ToString();

            string CacheString = string.Empty;
            TCGTagHandlers tcgthl = new TCGTagHandlers();
            tcgthl.Template = outhtml;
            tcgthl.NeedCreate = false;
            tcgthl.Replace(base.conn, base.config);
            CacheString = tcgthl.Template;
            writer.Write(CacheString);
        }
    }
}
