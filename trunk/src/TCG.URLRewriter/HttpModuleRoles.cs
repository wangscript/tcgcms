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

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;


namespace TCG.URLRewriter
{
    /// <summary>
    /// ������д���õĲ�����
    /// </summary>
    public class HttpModuleRoles
    {
        /// <summary>
        /// �������л���
        /// </summary>
        //public static void LoadHttpRoles() { LoadingAllCaches.Loading(Based.Skey_LoadingCache); }
        /// <summary>
        /// ֱ��ָ���ַ��д
        /// </summary>
        /// <param name="Application">HttpApplication ����</param>
        /// <param name="Context">HttpContext ����</param>
        /// <returns>bool</returns>
        public static bool OverrideUrl(HttpApplication Application, HttpContext Context)
        {
            LoadingRewrites LoadingRewrite = new LoadingRewrites();
            return LoadingRewrite.IsRewriteUrl(Application, Context);
        }
    }
}
