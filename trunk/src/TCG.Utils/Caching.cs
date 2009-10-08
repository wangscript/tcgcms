/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ƹ�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
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
            //Code��Ϣ�����ڻ�����
            HttpContext.Current.Cache.Insert(Caching.Key + name, value, dependency, DateTime.Now.AddMinutes((double)expireTime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }
    }
}