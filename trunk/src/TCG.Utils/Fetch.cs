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

namespace TCG.Utils
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Text;

    public class Fetch
    {
        public static string Get(string name)
        {
            string text1 = HttpContext.Current.Request.QueryString[name];
            return ((text1 == null) ? string.Empty : text1.Trim());
        }

        public static string UrlDecode(string str)
        {
            return HttpContext.Current.Server.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            return HttpContext.Current.Server.UrlEncode(str);
        }

        public static string Get(string name, CheckGetEnum chkType)
        {
            string text1 = Fetch.Get(name);
            bool flag1 = false;
            switch (chkType)
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
                new Terminator().Throw("\u5730\u5740\u680f\u4e2d\u53c2\u6570\u201c" + name + "\u201d\u7684\u503c\u4e0d\u7b26\u5408\u8981\u6c42\u6216\u5177\u6709\u6f5c\u5728\u5a01\u80c1\uff0c\u8bf7\u4e0d\u8981\u624b\u52a8\u4fee\u6539URL\u3002");
                return string.Empty;
            }
            return text1;
        }

        public static long Ip2Int(string ip)
        {
            if (!RegExp.IsIp(ip))
            {
                return (long) (-1);
            }
            string[] textArray1 = ip.Split(new char[] { '.' });
            long num1 = long.Parse(textArray1[0]) * 0x1000000;
            num1 += int.Parse(textArray1[1]) * 0x10000;
            num1 += int.Parse(textArray1[2]) * 0x100;
            return (num1 + int.Parse(textArray1[3]));
        }

        public static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath("~/" + path);
        }

        public static string Post(string name)
        {
            string text1 = HttpContext.Current.Request.Form[name];
            return ((text1 == null) ? string.Empty : text1.Trim());
        }

        public static string CurrentPath
        {
            get
            {
                string text1 = HttpContext.Current.Request.Path;
                if (text1.IndexOf(".asmx") > -1)
                {
                    text1 = text1.Substring(0, text1.LastIndexOf("/"));
                    text1 = text1.Substring(0, text1.LastIndexOf("/"));
                }
                else
                {
                    text1 = text1.Substring(0, text1.LastIndexOf("/"));
                }
                if (text1 == "/")
                {
                    return string.Empty;
                }
                return text1;
            }
        }

        public static string CurrentUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        public static bool IsGetFromAnotherDomain
        {
            get
            {
                if (HttpContext.Current.Request.HttpMethod == "POST")
                {
                    return false;
                }
                return (Fetch.Referrer.IndexOf(Fetch.ServerDomain) == -1);
            }
        }

        public static bool IsPostFromAnotherDomain
        {
            get
            {
                if (HttpContext.Current.Request.HttpMethod == "GET")
                {
                    return false;
                }
                return (Fetch.Referrer.IndexOf(Fetch.ServerDomain) == -1);
            }
        }

        public static bool IsRobots
        {
            get
            {
                return (Fetch.UserBrowser == "Unknown");
            }
        }

        public static string PathUpSeek
        {
            get
            {
                string text1 = Fetch.CurrentPath;
                string[] textArray1 = ConfigurationManager.AppSettings["PathUpSeek"].ToString().ToLower().Split(new char[] { '|' });
                foreach (string text2 in textArray1)
                {
                    if ((text2[0] == '/') && text1.ToLower().EndsWith(text2))
                    {
                        return text1.Remove(text1.Length - text2.Length, text2.Length);
                    }
                }
                return text1;
            }
        }

        public static string Referrer
        {
            get
            {
                Uri uri1 = HttpContext.Current.Request.UrlReferrer;
                if (uri1 == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(uri1);
            }
        }

        public static string ServerDomain
        {
            get
            {
                string text1 = HttpContext.Current.Request.Url.Host.ToLower();
                string[] textArray1 = text1.Split(new char[] { '.' });
                if ((textArray1.Length < 3) || RegExp.IsIp(text1))
                {
                    return text1;
                }
                string text2 = text1.Remove(0, text1.IndexOf(".") + 1);
                if ((text2.StartsWith("com.") || text2.StartsWith("net.")) || (text2.StartsWith("org.") || text2.StartsWith("gov.")))
                {
                    return text1;
                }
                return text2;
            }
        }

        public static string UserBrowser
        {
            get
            {
                string text1 = HttpContext.Current.Request.UserAgent;
                if (text1 != null)
                {
                    text1 = text1.ToLower();
                    if (text1.IndexOf("firefox") != -1)
                    {
                        return "Firefox";
                    }
                    if (text1.IndexOf("firebird") != -1)
                    {
                        return "Firebird";
                    }
                    if (text1.IndexOf("myie") != -1)
                    {
                        return "MyIE";
                    }
                    if (text1.IndexOf("opera") != -1)
                    {
                        return "Opera";
                    }
                    if (text1.IndexOf("netscape") != -1)
                    {
                        return "Netscape";
                    }
                    string[] textArray1 = text1.Split(new char[] { ';' });
                    if (textArray1[1].Trim() == "u")
                    {
                        return "Mozilla";
                    }
                    if (textArray1[1].IndexOf("msie") != -1)
                    {
                        return textArray1[1].Replace("msie", "IE").Trim();
                    }
                }
                return "Unknown";
            }
        }

        public static string UserIp
        {
            get
            {
                string text1 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                switch (text1)
                {
                    case null:
                    case "":
                        text1 = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        break;
                }
                if (!RegExp.IsIp(text1))
                {
                    return "Unknown";
                }
                return text1;
            }
        }

    }
}

