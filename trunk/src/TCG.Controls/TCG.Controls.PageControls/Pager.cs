namespace TCG.Controls.PageControls
{
    using TCG.Utils;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;

    public class Pager : Control
    {
        public Pager()
        {
            this._currentPage = 1;
            this._maxpage = 1;
            this._per = 0x19;
            this._total = 0;
            this._showCounts = true;
            this._alternate = false;
        }

        public void Calculate()
        {
            string text1 = Fetch.Get("page");
            this._currentPage = RegExp.IsNumeric(text1) ? int.Parse(text1) : 1;
            this._maxpage = this._total / this._per;
            if ((this._total % this._per) != 0)
            {
                this._maxpage++;
            }
            this._maxpage = Math.Max(this._maxpage, 1);
            this._currentPage = Math.Max(Math.Min(this._currentPage, this._maxpage), 1);
            this.SetItem("total", this._total);
        }

        public bool GetTotal()
        {
            string text1 = Fetch.Get("total");
            if (RegExp.IsNumeric(text1) && (this.Context.Request.HttpMethod == "GET"))
            {
                this._total = int.Parse(text1);
                return true;
            }
            return false;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(this.ToString());
        }

        public void SetItem(string name, object value)
        {
            this.Items.Add(name, this.Context.Server.UrlEncode(value.ToString()));
        }

        public void SetItem(string name, string value)
        {
            this.Items.Add(name, this.Context.Server.UrlEncode(value));
        }

        public override string ToString()
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("<script language='javascript' type='text/javascript'>\r\n<!--\r\n");
            builder1.Append(string.Concat(new object[] { "\tvar strPager = pager('$BaseURL', ", this._currentPage, ", ", this._maxpage, ", ", this._total, ", ", this._per, ", ", this._showCounts ? 1 : 0, ");\r\n" }));
            builder1.Append("\tdocument.write(strPager);\r\n");
            builder1.Append("//-->\r\n</script>");
            if (this._alternate)
            {
                builder1.Replace("$BaseURL", Regex.Replace(this.Context.Request.RawUrl, @"(^.*[,_\/]{1})\d+(\/index)?([,_\/]{1}\d+)?\.aspx$", "$1{p}.aspx", RegexOptions.IgnoreCase));
            }
            else
            {
                builder1.Replace("$BaseURL", this.Context.Request.FilePath + this.QueryString + "page={p}");
            }
            return builder1.ToString();
        }


        private bool AlternateUrl
        {
            get
            {
                return this._alternate;
            }
            set
            {
                this._alternate = value;
            }
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        private NameValueCollection Items
        {
            get
            {
                if (this.m_itemColl == null)
                {
                    this.m_itemColl = new NameValueCollection();
                }
                return this.m_itemColl;
            }
        }

        public int MaxPage
        {
            get
            {
                return this._maxpage;
            }
            set
            {
                this._maxpage = value;
            }
        }

        public int Per
        {
            get
            {
                return this._per;
            }
            set
            {
                this._per = value;
            }
        }

        public int PreviousRecords
        {
            get
            {
                return (this._per * (this._currentPage - 1));
            }
        }

        public string QueryString
        {
            get
            {
                string text1 = "?";
                if (this.m_itemColl != null)
                {
                    foreach (string text2 in this.Items.AllKeys)
                    {
                        string text4 = text1;
                        text1 = text4 + text2 + "=" + this.Items[text2] + "&";
                    }
                }
                return text1;
            }
        }

        public bool ShowCounts
        {
            get
            {
                return this._showCounts;
            }
            set
            {
                this._showCounts = value;
            }
        }

        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }


        private bool _alternate;
        private int _currentPage;
        private int _maxpage;
        private int _per;
        private bool _showCounts;
        private int _total;
        private NameValueCollection m_itemColl;
    }
}

