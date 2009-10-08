/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;


namespace TCG.URLRewriter
{
    /// <summary>
    /// 地址重写规则操作
    /// </summary>
    public class LoadingRewrites
    {
        /// <summary>
        /// 地址重写的缓存对象
        /// </summary>
        private HttpRewrites HttpRewrite = null;
        /// <summary>
        /// 地址重写规则操作
        /// </summary>
        public LoadingRewrites() { this.HttpRewrite = new HttpRewrites(); }
        /// <summary>
        /// 获取指定的地址重写规则的DataRow对象
        /// </summary>
        /// <param name="Key">指定的地址重写规则Key</param>
        /// <returns>DataRow</returns>
        public DataRow GetRewriteDataRow(string Key) { return this.HttpRewrite.Get(Key); }
        /// <summary>
        /// 直接指向地址重写
        /// </summary>
        /// <param name="Application">HttpApplication 对象</param>
        /// <param name="Context">HttpContext 对象</param>
        /// <returns>bool</returns>
        public bool IsRewriteUrl(HttpApplication Application, HttpContext Context) { return this.HttpRewrite.IsRewriteUrl(Application, Context); }
        /// <summary>
        /// 从指定的地址重写规则的DataRow取得地址重写(或不重写)的Url地址
        /// </summary>
        /// <param name="Rs">指定的地址重写规则的DataRow对象</param>
        /// <returns>string</returns>
        public string GetUrl(DataRow Rs)
        {
            if (Rs == null) return "";
            return ((Rs["IsOpen"].ToString().ToLower() == "true") ? Rs["RewriteUrl"].ToString() : Rs["NoRewriteUrl"].ToString());
        }
        /// <summary>
        /// 从指定的地址重写规则的Key取得地址重写(或不重写)的Url地址
        /// </summary>
        /// <param name="Key">指定的地址重写规则Key</param>
        /// <returns>string</returns>
        public string GetUrl(string Key) { return this.GetUrl(this.GetRewriteDataRow(Key)); }
        /// <summary>
        /// 得到格式化的指定的地址重写规则Key的地址重写(或不重写)的Url地址
        /// </summary>
        /// <param name="Key">指定的地址重写规则Key</param>
        /// <param name="strings">
        /// 要格式化的对象
        /// {0}   当前组合路径的前导目录,若与当前页在同一目录,则为空
        /// {1}   {2} .....  其他各种参数
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(string Key, string[] strings) { return this.GetRewriteUrl(Key, "", strings); }
        /// <summary>
        /// 得到格式化的指定的地址重写规则Key的地址重写(或不重写)的Url地址
        /// 带默认地址 若重写不存在,则返回默认地址
        /// </summary>
        /// <param name="Key">指定的地址重写规则Key</param>
        /// <param name="DefaultUrl">默认地址 若重写不存在,则返回默认地址</param>
        /// <param name="strings">
        /// 要格式化的对象
        /// {0}   当前组合路径的前导目录,若与当前页在同一目录,则为空
        /// {1}   {2} .....  其他各种参数
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(string Key, string DefaultUrl, string[] strings)
        {
            string Url = this.GetUrl(Key);
            if (string.IsNullOrEmpty(Url)) return DefaultUrl;
            return String.Format(Url, strings);
        }
        /// <summary>
        /// 得到格式化的指定的地址重写规则Key的地址重写(或不重写)的Url地址
        /// </summary>
        /// <param name="Rs">指定的地址重写规则Key</param>
        /// <param name="strings">
        /// 要格式化的对象
        /// {0}   当前组合路径的前导目录,若与当前页在同一目录,则为空
        /// {1}   {2} .....  其他各种参数
        /// </param>
        /// <returns>string</returns>
        public string GetRewriteUrl(DataRow Rs, string[] strings) { return this.GetRewriteUrl(Rs, "", strings); }
        /// <summary>
        /// 得到格式化的指定的地址重写规则DataRow的地址重写(或不重写)的Url地址
        /// 带默认地址 若重写不存在,则返回默认地址
        /// </summary>
        /// <param name="Rs">指定的地址重写规则的DataRow对象</param>
        /// <param name="DefaultUrl">默认地址 若重写不存在,则返回默认地址</param>
        /// <param name="strings">
        /// 要格式化的对象
        /// {0}   当前组合路径的前导目录,若与当前页在同一目录,则为空
        /// {1}   {2} .....  其他各种参数
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