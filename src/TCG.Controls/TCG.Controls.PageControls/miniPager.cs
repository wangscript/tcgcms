
/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

namespace TCG.Controls.PageControls
{
    using System;
    using System.Text;
    using System.Web.UI;

    public class miniPager : Control
    {
        public miniPager()
        {
            this.topicId = 0;
            this.total = 0;
            this.perPage = 15;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder builder1 = new StringBuilder();
            if (this.total <= this.perPage)
            {
                return "";
            }
            int num1 = 1;
            if ((this.total % this.perPage) == 0)
            {
                num1 = this.total / this.perPage;
            }
            else
            {
                num1 = (this.total / this.perPage) + 1;
            }
            if (num1 <= 1)
            {
                return "";
            }
            builder1.Append("(\u9875<span style='font-family:Courier New;font-size:12px'>");
            string text1 = " ";
            for (int num2 = 2; num2 <= num1; num2++)
            {
                if (num2 > 3)
                {
                    if (num1 == num2)
                    {
                        builder1.Append(text1);
                    }
                    else
                    {
                        builder1.Append("...");
                    }
                    builder1.Append(string.Concat(new object[] { "<a href='topic.aspx?topicid=", this.topicId, "&page=", num1, "' name='topiclink'>", num1, "</a>" }));
                    break;
                }
                builder1.Append(string.Concat(new object[] { text1, "<a href='topic.aspx?topicid=", this.topicId, "&page=", num2, "' name='topiclink'>", num2, "</a>" }));
            }
            builder1.Append("</span>)");
            return builder1.ToString();
        }


        public int TopicId
        {
            get
            {
                return this.topicId;
            }
            set
            {
                this.topicId = value;
            }
        }

        public int Total
        {
            get
            {
                return this.total;
            }
            set
            {
                this.total = value;
            }
        }


        private int perPage;
        private int topicId;
        private int total;
    }
}

