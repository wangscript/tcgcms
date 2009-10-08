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

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;


namespace TCG.URLRewriter
{
    /// <summary>
    /// ��ַ��д�������
    /// </summary>
    public class LoadingRewrites
    {
        /// <summary>
        /// ��ַ��д�Ļ������
        /// </summary>
        private HttpRewrites HttpRewrite = null;
        /// <summary>
        /// ��ַ��д�������
        /// </summary>
        public LoadingRewrites() { this.HttpRewrite = new HttpRewrites(); }
        /// <summary>
        /// ��ȡָ���ĵ�ַ��д�����DataRow����
        /// </summary>
        /// <param name="Key">ָ���ĵ�ַ��д����Key</param>
        /// <returns>DataRow</returns>
        public DataRow GetRewriteDataRow(string Key) { return this.HttpRewrite.Get(Key); }
        /// <summary>
        /// ֱ��ָ���ַ��д
        /// </summary>
        /// <param name="Application">HttpApplication ����</param>
        /// <param name="Context">HttpContext ����</param>
        /// <returns>bool</returns>
        public bool IsRewriteUrl(HttpApplication Application, HttpContext Context) { return this.HttpRewrite.IsRewriteUrl(Application, Context); }
        /// <summary>
        /// ��ָ���ĵ�ַ��д�����DataRowȡ�õ�ַ��д(����д)��Url��ַ
        /// </summary>
        /// <param name="Rs">ָ���ĵ�ַ��д�����DataRow����</param>
        /// <returns>string</returns>
        public string GetUrl(DataRow Rs)
        {
            if (Rs == null) return "";
            return ((Rs["IsOpen"].ToString().ToLower() == "true") ? Rs["RewriteUrl"].ToString() : Rs["NoRewriteUrl"].ToString());
        }
        /// <summary>
        /// ��ָ���ĵ�ַ��д�����Keyȡ�õ�ַ��д(����д)��Url��ַ
        /// </summary>
        /// <param name="Key">ָ���ĵ�ַ��д����Key</param>
        /// <returns>string</returns>
        public string GetUrl(string Key) { return this.GetUrl(this.GetRewriteDataRow(Key)); }
        /// <summary>
        /// �õ���ʽ����ָ���ĵ�ַ��д����Key�ĵ�ַ��д(����д)��Url��ַ
        /// </summary>
        /// <param name="Key">ָ���ĵ�ַ��д����Key</param>
        /// <param name="strings">
        /// Ҫ��ʽ���Ķ���
        /// {0}   ��ǰ���·����ǰ��Ŀ¼,���뵱ǰҳ��ͬһĿ¼,��Ϊ��
        /// {1}   {2} .....  �������ֲ���
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(string Key, string[] strings) { return this.GetRewriteUrl(Key, "", strings); }
        /// <summary>
        /// �õ���ʽ����ָ���ĵ�ַ��д����Key�ĵ�ַ��д(����д)��Url��ַ
        /// ��Ĭ�ϵ�ַ ����д������,�򷵻�Ĭ�ϵ�ַ
        /// </summary>
        /// <param name="Key">ָ���ĵ�ַ��д����Key</param>
        /// <param name="DefaultUrl">Ĭ�ϵ�ַ ����д������,�򷵻�Ĭ�ϵ�ַ</param>
        /// <param name="strings">
        /// Ҫ��ʽ���Ķ���
        /// {0}   ��ǰ���·����ǰ��Ŀ¼,���뵱ǰҳ��ͬһĿ¼,��Ϊ��
        /// {1}   {2} .....  �������ֲ���
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(string Key, string DefaultUrl, string[] strings)
        {
            string Url = this.GetUrl(Key);
            if (string.IsNullOrEmpty(Url)) return DefaultUrl;
            return String.Format(Url, strings);
        }
        /// <summary>
        /// �õ���ʽ����ָ���ĵ�ַ��д����Key�ĵ�ַ��д(����д)��Url��ַ
        /// </summary>
        /// <param name="Rs">ָ���ĵ�ַ��д����Key</param>
        /// <param name="strings">
        /// Ҫ��ʽ���Ķ���
        /// {0}   ��ǰ���·����ǰ��Ŀ¼,���뵱ǰҳ��ͬһĿ¼,��Ϊ��
        /// {1}   {2} .....  �������ֲ���
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(DataRow Rs, string[] strings) { return this.GetRewriteUrl(Rs, "", strings); }
        /// <summary>
        /// �õ���ʽ����ָ���ĵ�ַ��д����DataRow�ĵ�ַ��д(����д)��Url��ַ
        /// ��Ĭ�ϵ�ַ ����д������,�򷵻�Ĭ�ϵ�ַ
        /// </summary>
        /// <param name="Rs">ָ���ĵ�ַ��д�����DataRow����</param>
        /// <param name="DefaultUrl">Ĭ�ϵ�ַ ����д������,�򷵻�Ĭ�ϵ�ַ</param>
        /// <param name="strings">
        /// Ҫ��ʽ���Ķ���
        /// {0}   ��ǰ���·����ǰ��Ŀ¼,���뵱ǰҳ��ͬһĿ¼,��Ϊ��
        /// {1}   {2} .....  �������ֲ���
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(DataRow Rs, string DefaultUrl, string[] strings)
        {
            string Url = this.GetUrl(Rs);
            if (Url == null || Url == "") return DefaultUrl;
            return String.Format(Url, strings);
        }
    };
}