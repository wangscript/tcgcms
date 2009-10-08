/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

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
