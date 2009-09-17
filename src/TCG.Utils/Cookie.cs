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

