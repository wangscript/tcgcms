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
    using System.Collections;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Web.Caching;
    using System.Web;
    using System.Xml;

    public class Caching
    {
        private static string Key = "TCG_System_";

        public static object Get(string name)
        {
            return HttpContext.Current.Cache.Get(Caching.Key + name);
        }

        public static void Remove(string name)
        {
            if (HttpContext.Current.Cache[Caching.Key + name] != null)
            {
                HttpContext.Current.Cache.Remove(Caching.Key + name);
            }
        }

        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            HttpContext.Current.Cache.Insert(Caching.Key + name, value, cacheDependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

        public static void SetCache(string name, object value, CacheDependency dependency, int expireTime)
        {
            //Code信息保存在缓存中
            HttpContext.Current.Cache.Insert(Caching.Key + name, value, dependency, DateTime.Now.AddMinutes((double)expireTime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }
    }
}