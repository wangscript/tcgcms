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

namespace TCG.Utils
{
    using System;
    using System.Configuration;
    using System.Web;

    public class Cookie
    {
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[text1 + name];
        }

        public static void Remove(string name)
        {
            Cookie.Remove(Cookie.Get(name));
        }

        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = new DateTime(0x7bf, 5, 0x15);
                Cookie.Save(cookie);
            }
        }

        public static void Save(HttpCookie cookie)
        {
            string text1 = Fetch.ServerDomain;
            string text2 = HttpContext.Current.Request.Url.Host.ToLower();
            if (text1 != text2)
            {
                cookie.Domain = text1;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static HttpCookie Set(string name)
        {
            return new HttpCookie(text1 + name);
        }

        private static string text1 = "TCG_FOR_XZDSW_";
    }
}

