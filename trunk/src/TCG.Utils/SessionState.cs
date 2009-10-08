/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ι�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

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

