using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace TCG.Manage.Controls
{
    public class AjaxDiv : Control
    {
        protected override void Render(HtmlTextWriter output)
        {
            output.Write(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("<div class=\"ajaxdiv hid\" id=\"ajaxdiv\">\r\n");
            builder1.Append("\t\t<img src=\"images/ajax-loader1.gif\" id=\"ajaximg\" /><span id=\"ajaxText\"> 正在发送请求...");
            builder1.Append("</span><a href=\"javascript:GoTo();\" class=\"ajaxclose\" onclick=\"ajaxClose();\" title=\"关闭\"></a>\r\n");
            builder1.Append("</div>");
            return builder1.ToString();
        }
    }
}
