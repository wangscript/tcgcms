using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCG.Utils.Release;
using TCG.Data;
using TCG.Utils;


namespace TCG.Pages
{
    public class Origin : Page
    {
        public Origin()
        {
            this._isRobots = '?';
        }

        protected virtual void CheckSpecialTerminate()
        {
            if (Fetch.Get("staff") != "")
            {
                string text1 = "系统名称: " + Property.SystemName + "<br />";
                text1 = text1 + "系统版本: " + Property.SystemVersion + "<br />";
                text1 = text1 + "开 发 者: " + Property.Designer + "<br />";
                text1 = text1 + "电子信箱: <a href='mailto:sanyungui&#64;xzdsw&#46;com'>sanyungui&#64;xzdsw&#46;com</a><br />";
                text1 = text1 + "官方网站: <a href='" + Config.Settings["www_Site"] + "' target='_blank'>" + Config.Settings["www_Site"] + "</a>";
                this.Throw(text1, "著作签名", null, null, false);
            }
        }

        protected virtual void Alert(string message)
        {
            new Terminator().Alert(message);
        }

        protected virtual void Alert(string message, string backurl)
        {
            new Terminator().Alert(message, backurl);
        }

        protected virtual void CheckVerifyCode()
        {
            string text1 = this.Post("verification");
            if (SessionState.Get("verification") != null)
            {
                if (SessionState.Get("verification").ToString() == text1.ToUpper())
                {
                    SessionState.Remove("verification");
                }
                else
                {
                    this.Alert("\u9644\u52a0\u7801\u4e0d\u6b63\u786e\uff0c\u8bf7\u8fd4\u56de\u91cd\u8bd5\u3002");
                }
            }
        }

        protected virtual string EmailEncode(object obj)
        {
            return this.EmailEncode(obj.ToString());
        }

        protected virtual string EmailEncode(string str)
        {
            string text1 = this.TextEncode(str).Replace("@", "&#64;").Replace(".", "&#46;");
            return ("<a href='mailto:" + text1 + "'>" + text1 + "</a>");
        }

        protected virtual string Get(string name)
        {
            string text1 = HttpContext.Current.Request.QueryString[name];
            return ((text1 == null) ? string.Empty : text1.Trim());
        }

        protected virtual string Get(string name, CheckGetEnum type)
        {
            string text1 = this.Get(name);
            bool flag1 = false;
            switch (type)
            {
                case CheckGetEnum.Int:
                    flag1 = RegExp.IsNumeric(text1);
                    break;

                case CheckGetEnum.Safety:
                    flag1 = RegExp.IsSafety(text1);
                    break;

                default:
                    flag1 = true;
                    break;
            }
            if (!flag1)
            {
                this.Throw("\u5730\u5740\u680f\u4e2d\u53c2\u6570\u201c" + name + "\u201d\u7684\u503c\u4e0d\u5408\u6cd5\u6216\u5177\u6709\u6f5c\u5728\u5a01\u80c1\uff0c\u8bf7\u4e0d\u8981\u624b\u52a8\u4fee\u6539URL\u3002");
                return string.Empty;
            }
            return text1;
        }

        protected virtual DataRow GetDataRowFromRepeaterItemEventArgs(RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
            {
                return null;
            }
            return (e.Item.DataItem as DataRow);
        }

        protected virtual DataRowView GetDataRowViewFromRepeaterItemEventArgs(RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
            {
                return null;
            }
            return (e.Item.DataItem as DataRowView);
        }

        protected virtual string HtmlEncode(object obj)
        {
            return this.HtmlEncode(obj.ToString());
        }

        protected virtual string HtmlEncode(string str)
        {
            StringBuilder builder1 = new StringBuilder(str);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            builder1.Replace("\t", "&nbsp; &nbsp; ");
            builder1.Replace("\r", "");
            builder1.Replace("\n", "<br />");
            return this.ShitEncode(builder1.ToString());
        }

        protected virtual bool IsNumeric(object obj)
        {
            string text1 = @"^\-?[0-9]+$";
            return Regex.IsMatch(obj.ToString(), text1);
        }

        protected virtual bool IsNumeric(string str)
        {
            string text1 = @"^\-?[0-9]+$";
            return Regex.IsMatch(str, text1);
        }

        protected virtual string Left(object obj, int i)
        {
            return this.Left(obj.ToString(), i, true);
        }

        protected virtual string Left(string str, int i)
        {
            return this.Left(str, i, true);
        }

        protected virtual string Left(object obj, int i, bool encode)
        {
            return this.Left(obj.ToString(), i, encode);
        }

        protected virtual string Left(string str, int need, bool encode)
        {
            char ch1;
            if ((str == null) || (str == string.Empty))
            {
                return string.Empty;
            }
            int num1 = str.Length;
            if (num1 < (need / 2))
            {
                return (encode ? this.TextEncode(str) : str);
            }
            int num4 = 0;
            int num2 = 0;
            while (num2 < num1)
            {
                ch1 = str[num2];
                num4 += RegExp.IsUnicode(ch1.ToString()) ? 2 : 1;
                if (num4 >= need)
                {
                    break;
                }
                num2++;
            }
            string text1 = str.Substring(0, num2);
            if (num1 > num2)
            {
                int num3 = 0;
                while (num3 < 5)
                {
                    ch1 = str[num2 - num3];
                    num4 -= RegExp.IsUnicode(ch1.ToString()) ? 2 : 1;
                    if (num4 <= need)
                    {
                        break;
                    }
                    num3++;
                }
                text1 = str.Substring(0, num2 - num3) + "...";
            }
            return (encode ? this.TextEncode(text1) : text1);
        }

        protected virtual int Len(string str)
        {
            return Encoding.GetEncoding(0x3a8).GetByteCount(str);
        }

        protected virtual string Post(string name)
        {
            string text1 = HttpContext.Current.Request.Form[name];
            return ((text1 == null) ? string.Empty : text1.Trim());
        }

        protected virtual string ShitEncode(string str)
        {
            string text1 = ConfigurationManager.ConnectionStrings["BadWords"].ToString();
            switch (text1)
            {
                case null:
                case "":
                    text1 = "妈的|你妈|他妈|妈b|妈比|我日|我操|法轮|fuck|shit";
                    break;

                default:
                    text1 = Regex.Replace(text1, @"\|{2,}", "|");
                    text1 = Regex.Replace(text1, @"(^\|)|(\|$)", "");
                    break;
            }
            return Regex.Replace(str, text1, "**", RegexOptions.IgnoreCase);
        }

        protected virtual string SqlEncode(string str)
        {
            return str.Trim().Replace("'", "''");
        }

        protected virtual string TextEncode(object obj)
        {
            return this.TextEncode(obj.ToString());
        }

        protected virtual string TextEncode(string str)
        {
            StringBuilder builder1 = new StringBuilder(str);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            return this.ShitEncode(builder1.ToString());
        }

        protected virtual void Throw(string message)
        {
            new Terminator().Throw(message, null, null, null, true);
        }

        protected virtual void Throw(string message, string title, string links, string autojump, bool showback)
        {
            new Terminator().Throw(message, title, links, autojump, showback);
        }

        protected virtual void Throw(int errvalue, string autojump, bool showbak)
        {
            new Terminator().Throw(errvalue, autojump, showbak);
        }

        public void trace(object o)
        {
            base.Response.Write(o);
        }

        protected virtual string UrlEncode(object obj)
        {
            return this.UrlEncode(obj.ToString());
        }

        protected virtual string UrlEncode(string str)
        {
            return HttpContext.Current.Server.UrlEncode(str);
        }

        protected virtual string XLeft(object s, int need)
        {
            return this.XLeft(s.ToString(), need);
        }

        protected virtual string XLeft(string s, int need)
        {
            if ((s == null) || (s == ""))
            {
                return string.Empty;
            }
            if (s.Length < need)
            {
                return s;
            }
            string text1 = s.Substring(0, need);
            return (text1 + "...");
        }


        protected Config config
        {
            get
            {
                if (this._config == null)
                {
                    this._config = new Config();
                }
                return this._config;
            }
        }

        protected virtual string currentPath
        {
            get
            {
                string text1 = HttpContext.Current.Request.Path;
                text1 = text1.Substring(0, text1.LastIndexOf("/"));
                if (text1 == "/")
                {
                    return string.Empty;
                }
                return text1;
            }
        }

        protected virtual bool IsRobots
        {
            get
            {
                if (this._isRobots == '?')
                {
                    this._isRobots = Fetch.IsRobots ? 'Y' : 'N';
                }
                return (this._isRobots == 'Y');
            }
        }

        protected Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = DBLinkNums.Manage;
                }
                return this._conn;
            }
        }

        private Config _config;
        private char _isRobots;
        private Connection _conn;
    }
}
