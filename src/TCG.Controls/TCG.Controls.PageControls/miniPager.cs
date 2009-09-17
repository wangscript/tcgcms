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

