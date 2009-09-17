namespace TCG.Utils
{
    using System;
    using System.Web.SessionState;
    using System.Configuration;
    using System.Web;

    public class SessionState
    {
        public static object Get(string name)
        {
            return HttpContext.Current.Session[name];
        }

        public static void Remove(string name)
        {
            if (HttpContext.Current.Session[name] != null)
            {
                HttpContext.Current.Session.Remove(name);
            }
        }

        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public static void Set(string name, object value)
        {
            HttpContext.Current.Session.Add(name, value);
        }

    }
}

