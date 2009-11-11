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
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Web.Caching;
    using System.Web;
    using System.Xml;

    public class CachingService
    {
        public static string Key = "TCG_System_";

        public static HttpContext HttpContext
        {
            get
            {
                return CachingService._ihttpcontext;
            }
            set
            {
                CachingService._ihttpcontext = value;
            }
        }
        private static HttpContext _ihttpcontext = null;

        public static object Get(string name)
        {
            if (CachingService.HttpContext != null)
            {
                return CachingService.HttpContext.Cache.Get(CachingService.Key + name);
            }
            else
            {
                return HttpContext.Current.Cache.Get(CachingService.Key + name);
            }
        }

        public static void Remove(string name)
        {
            if (CachingService.HttpContext != null)
            {
                if (CachingService.HttpContext.Cache[CachingService.Key + name] != null)
                {
                    CachingService.HttpContext.Cache.Remove(CachingService.Key + name);
                }
            }
            else
            {
                if (HttpContext.Current.Cache[CachingService.Key + name] != null)
                {
                    HttpContext.Current.Cache.Remove(CachingService.Key + name);
                }
            }
        }

        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            if (CachingService.HttpContext != null)
            {
                CachingService.HttpContext.Cache.Insert(CachingService.Key + name, value, cacheDependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
            }
            else
            {
                HttpContext.Current.Cache.Insert(CachingService.Key + name, value, cacheDependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
            }    
        }

        public static void SetCache(string name, object value, CacheDependency dependency, int expireTime)
        {
            if (CachingService.HttpContext != null)
            {
                CachingService.HttpContext.Cache.Insert(CachingService.Key + name, value, dependency, DateTime.Now.AddMinutes((double)expireTime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }
            else
            {
                HttpContext.Current.Cache.Insert(CachingService.Key + name, value, dependency, DateTime.Now.AddMinutes((double)expireTime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }    

           
        }

        #region ϵͳCaching��������
        /// <summary>
        /// �����ļ����ඨ��
        /// </summary>
        public static string CACHING_ALL_FILECLASS = "CACHING_ALL_FILECLASS";
        /// <summary>
        /// ����ģ����Ϣ����
        /// </summary>
        public static string CACHING_All_TEMPLATES = "CACHING_All_TEMPLATES";
        /// <summary>
        /// ����ģ��ʵ��
        /// </summary>
        public static string CACHING_All_TEMPLATES_ENTITY = "CACHING_All_TEMPLATES_ENTITY";
        /// <summary>
        /// ����ϵͳģ����Ϣ
        /// </summary>
        public static string CACHING_All_SYSTEM_TEMPLATES = "CACHING_All_SYSTEM_TEMPLATES";
        /// <summary>
        /// ���е���Դ������ϢDATATABle
        /// </summary>
        public static string CACHING_ALL_CATEGORIES = "CACHING_ALL_CATEGORIES";
        /// <summary>
        /// ���е���Դ������Ϣʵ��
        /// </summary>
        public static string CACHING_ALL_CATEGORIES_ENTITY = "CACHING_ALL_CATEGORIES_ENTITY";
        /// <summary>
        /// ����Ƥ����¼������
        /// </summary>
        public static string CACHING_ALL_SKIN = "CACHING_ALL_SKIN";
        /// <summary>
        /// ����Ƥ��ʵ��
        /// </summary>
        public static string CACHING_ALL_SKIN_ENTITY = "CACHING_ALL_SKIN_ENTITY";

        /// <summary>
        /// ����Ȩ����ʵ��
        /// </summary>
        public static string CACHING_ALL_POPDOM = "CACHING_ALL_POPDOM";

        /// <summary>
        /// ϵͳ���漯��
        /// </summary>
        public static Dictionary<string, string> SystemCachings
        {
            get
            {
                if (_systemcachings == null)
                {
                    _systemcachings = new Dictionary<string, string>();
                    _systemcachings.Add("�����ļ����ඨ��", CACHING_ALL_FILECLASS);
                    _systemcachings.Add("����ģ����Ϣ����", CACHING_All_TEMPLATES);
                    _systemcachings.Add("����ϵͳģ����Ϣ", CACHING_All_SYSTEM_TEMPLATES);
                    _systemcachings.Add("���е���Դ������Ϣ", CACHING_ALL_CATEGORIES);
                    _systemcachings.Add("���е���Դ������Ϣʵ��", CACHING_ALL_CATEGORIES_ENTITY);
                    _systemcachings.Add("����ģ��ʵ��", CACHING_All_TEMPLATES_ENTITY);
                    _systemcachings.Add("CACHING_ALL_POPDOM", CACHING_ALL_POPDOM);
                }
                return _systemcachings;
            }
        }
        private static Dictionary<string, string> _systemcachings = null;
        #endregion
    }
}