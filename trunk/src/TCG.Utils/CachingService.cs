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
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Web.Caching;
    using System.Web;
    using System.Xml;

    public class CachingService
    {
        private static string Key = "TCG_System_";

        public static object Get(string name)
        {
            return HttpContext.Current.Cache.Get(CachingService.Key + name);
        }

        public static void Remove(string name)
        {
            if (HttpContext.Current.Cache[CachingService.Key + name] != null)
            {
                HttpContext.Current.Cache.Remove(CachingService.Key + name);
            }
        }

        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            HttpContext.Current.Cache.Insert(CachingService.Key + name, value, cacheDependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

        public static void SetCache(string name, object value, CacheDependency dependency, int expireTime)
        {
            //Code信息保存在缓存中
            HttpContext.Current.Cache.Insert(CachingService.Key + name, value, dependency, DateTime.Now.AddMinutes((double)expireTime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        #region 系统Caching参数定义
        /// <summary>
        /// 所有文件分类定义
        /// </summary>
        public static string CACHING_ALL_FILECLASS = "CACHING_ALL_FILECLASS";
        /// <summary>
        /// 所有模版信息缓存
        /// </summary>
        public static string CACHING_All_TEMPLATES = "CACHING_All_TEMPLATES";
        /// <summary>
        /// 所有系统模版信息
        /// </summary>
        public static string CACHING_All_SYSTEM_TEMPLATES = "CACHING_All_SYSTEM_TEMPLATES";
        /// <summary>
        /// 所有的资源分类信息
        /// </summary>
        public static string CACHING_ALL_CATEGORIES = "CACHING_ALL_CATEGORIES";

        /// <summary>
        /// 系统缓存集合
        /// </summary>
        public static Dictionary<string, string> SystemCachings
        {
            get
            {
                if (_systemcachings == null)
                {
                    _systemcachings = new Dictionary<string, string>();
                    _systemcachings.Add("所有文件分类定义", CACHING_ALL_FILECLASS);
                    _systemcachings.Add("所有模版信息缓存", CACHING_All_TEMPLATES);
                    _systemcachings.Add("所有系统模版信息", CACHING_All_SYSTEM_TEMPLATES);
                    _systemcachings.Add("所有的资源分类信息", CACHING_ALL_CATEGORIES);
                }
                return _systemcachings;
            }
        }
        private static Dictionary<string, string> _systemcachings = null;
        #endregion
    }
}