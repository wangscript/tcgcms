/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ι�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
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
            builder1.Append("\t\t<img src=\"images/ajax-loader1.gif\" id=\"ajaximg\" /><span id=\"ajaxText\"> ���ڷ�������...");
            builder1.Append("</span><a href=\"javascript:GoTo();\" class=\"ajaxclose\" onclick=\"ajaxClose();\" title=\"�ر�\"></a>\r\n");
            builder1.Append("</div>");
            return builder1.ToString();
        }
    }
}
