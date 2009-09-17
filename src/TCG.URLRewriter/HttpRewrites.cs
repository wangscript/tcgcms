using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;
using System.Collections;
using System.Web.Caching;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using TCG.Utils;

namespace TCG.URLRewriter
{
    /// <summary>
    /// 获取地址重新的缓存操作
    /// </summary>
    public class HttpRewrites
    {
        /// <summary>
        /// 地址重新的缓存信息名称
        /// </summary>
        private string Key = "ReWrite";

        /// <summary>
        /// 相关的数据文件路径
        /// </summary>
        private string File = "Config/ReWrite.config";

        /// <summary>
        /// 被保存的缓存信息 DataTable
        /// </summary>
        public DataTable Table = null;

        /// <summary>
        /// 获取地址重新的缓存操作
        /// </summary>
        public HttpRewrites()
        {
            this.Table = null;
            this.Initialize();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~HttpRewrites()
        {
            this.Clear();
        }

        /// <summary>
        /// 初始化类
        /// </summary>
        private void Initialize()
        {
            Object Obj = Caching.Get(this.Key);
            if (Obj == null)
            {
                this.Set();
                return;
            }
            try
            {
                this.Table = (DataTable) Obj;
                return;
            }
            catch
            {
                this.Set();
            }
        }

        /// <summary>
        /// 获取某个地址重写的DataRow对象
        /// </summary>
        /// <param name="IndexKey">当前项唯一索引名</param>
        /// <returns>DataRow</returns>
        public DataRow Get(string IndexKey)
        {
            if (IndexKey == null) return null;
            if (IndexKey == "") return null;
            if (this.Table == null) return null;
            if (this.Table.Rows.Count <= 0)
            {
                return null;
            }
            DataRow[] Drs = this.Table.Select("IndexName = '" + IndexKey + "'");
            if (Drs.Length <= 0)
            {
                Drs = null;
                return null;
            }
            DataRow Rs = Drs[0];
            Drs = null;
            return Rs;
        }

        /// <summary>
        /// 直接指向地址重写
        /// </summary>
        /// <param name="Application">HttpApplication 对象</param>
        /// <param name="Context">HttpContext 对象</param>
        /// <returns>bool</returns>
        public bool IsRewriteUrl(HttpApplication Application, HttpContext Context)
        {
            if (this.Table == null) return false;
            int Count = this.Table.Rows.Count;
            string Url = Application.Context.Request.Path;
            for (int i = 0; i < Count; i++)
            {
                DataRow Rs = this.Table.Rows[i];
                bool IsOpen = (Rs["IsOpen"].ToString().ToLower() == "true");
                if (!IsOpen) continue;  //如果不使用地址重写,则跳到下一条记录;
                Regex Reg = new Regex("^" + Rs["FindUrl"].ToString() + "$", RegexOptions.IgnoreCase);
                if (Reg.IsMatch(Url))
                {
                    //找到匹配
                    string gUrl = Reg.Replace(Url, Rs["ToUrl"].ToString());
                    string fUrl = Application.Context.Request.ApplicationPath;
                    gUrl = this.ResolveUrl(Application.Context.Request.ApplicationPath, gUrl);
                    string mUrl  = gUrl;
                    string qUrl = "";
                    if (gUrl.IndexOf('?') > 0)
                    {
                        mUrl = gUrl.Substring(0, gUrl.IndexOf('?'));
                        qUrl = gUrl.Substring(gUrl.IndexOf('?') + 1);
                    }
                    if (fUrl == null || fUrl == "")
                    {
                        this.Table.Dispose();
                        this.Table = null;
                        return false;
                    }
                    this.Table.Dispose();
                    this.Table = null;
                    Context.RewritePath(mUrl, "", qUrl);
                    return true;
                }
            }
            this.Table.Dispose();
            this.Table = null;
            return false;
        }

        /// <summary>
        /// 获取虚拟目录
        /// </summary>
        /// <param name="AppPath">string ApplicationPath</param>
        /// <param name="Url">当前地址</param>
        /// <returns>string</returns>
        private string ResolveUrl(string AppPath, string Url)
        {
            if ((Url.Length == 0) || (Url[0] != '~')) return Url;
            if (Url.Length == 1) return AppPath;
            if ((Url[1] == '/') || (Url[1] == '\\'))
            {
                if (AppPath.Length > 1) return (AppPath + "/" + Url.Substring(2));
                return ("/" + Url.Substring(2));
            }
            if (AppPath.Length > 1) return (AppPath + "/" + Url.Substring(1));
            return (AppPath + Url.Substring(1));
        }

        /// <summary>
        /// 重新设置 地址重新的缓存
        /// </summary>
        private void Set()
        {
            this.Table = null;
            this.Table = new DataTable();
            this.Table.Columns.Add("IndexName");
            this.Table.Columns.Add("IsOpen");
            this.Table.Columns.Add("FindUrl");
            this.Table.Columns.Add("ToUrl");
            this.Table.Columns.Add("RewriteUrl");
            this.Table.Columns.Add("NoRewriteUrl");
            this.Table.Columns["IndexName"].DataType = typeof (System.String);
            this.Table.Columns["IsOpen"].DataType = typeof (System.Boolean);
            this.Table.Columns["FindUrl"].DataType = typeof (System.String);
            this.Table.Columns["ToUrl"].DataType = typeof (System.String);
            this.Table.Columns["RewriteUrl"].DataType = typeof (System.String);
            this.Table.Columns["NoRewriteUrl"].DataType = typeof (System.String);

            XmlDocument Xml = new XmlDocument();
            XmlTextReader xmlRe = new XmlTextReader(this.GetPath);
            Xml.Load(xmlRe);
            xmlRe.Close();

            foreach (XmlElement Node in Xml.GetElementsByTagName(RewriteKey.RoleItem))
            {
                string IndexName = Node.SelectSingleNode(RewriteKey.IndexName).InnerText;
                if (IndexName == null || IndexName == "") continue;
                if (this.Table.Select("IndexName = '" + IndexName + "'").Length > 0) continue;
                bool IsOpen = (Node.SelectSingleNode(RewriteKey.IsOpen).InnerText.ToLower() == "true") ? true : false;
                string FindUrl = Node.SelectSingleNode(RewriteKey.FindUrl).InnerText;
                string ToUrl = Node.SelectSingleNode(RewriteKey.ToUrl).InnerText;
                string RewriteUrl = Node.SelectSingleNode(RewriteKey.RewriteUrl).InnerText;
                string NoRewriteUrl = Node.SelectSingleNode(RewriteKey.NoRewriteUrl).InnerText;
                DataRow Rs = this.Table.NewRow();
                Rs["IndexName"] = IndexName;
                Rs["IsOpen"] = IsOpen;
                Rs["FindUrl"] = FindUrl;
                Rs["ToUrl"] = ToUrl;
                Rs["RewriteUrl"] = RewriteUrl;
                Rs["NoRewriteUrl"] = NoRewriteUrl;
                this.Table.Rows.Add(Rs);
                Rs = null;
            }
            Xml = null;
            Caching.Set(this.Key, this.Table, new CacheDependency(this.GetPath));
        }

        /// <summary>
        /// 当前程序的根目录
        /// </summary>
        private string GetPath
        {
            get { return Fetch.MapPath(this.File); }
        }

        /// <summary>
        /// 移除当前缓存
        /// </summary>
        public void Remove()
        {
            Caching.Remove(this.Key);
        }

        /// <summary>
        /// 清除资源
        /// </summary>
        public void Clear()
        {
            if (this.Table != null)
            {
                this.Table.Dispose();
                this.Table = null;
            }
        }
    } ;

    /// <summary>
    /// Rewrite.config 文件的指定目录结构
    /// </summary>
    public class RewriteKey
    {
        /// <summary>
        /// Item 项
        /// </summary>
        public static string RoleItem = "RoleItem";

        /// <summary>
        /// 唯一索引名称
        /// </summary>
        public static string IndexName = "IndexName";

        /// <summary>
        /// 是否开启当前使用项
        /// </summary>
        public static string IsOpen = "IsOpen";

        /// <summary>
        /// 地址重写后产生的Url
        /// </summary>
        public static string FindUrl = "FindUrl";

        /// <summary>
        /// 跳转到的页面地址
        /// </summary>
        public static string ToUrl = "ToUrl";

        /// <summary>
        /// 使用地址重写的Url
        /// </summary>
        public static string RewriteUrl = "RewriteUrl";

        /// <summary>
        /// 不使用地址重写的Url
        /// </summary>
        public static string NoRewriteUrl = "NoRewriteUrl";
    } ;

    /// <summary>
    /// 对Rewrite.config文档进行可用的格式化
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RewriteFileItem
    {
        /// <summary>
        /// 唯一索引名称
        /// </summary>
        public string IndexName;

        /// <summary>
        /// 是否开启当前使用项
        /// </summary>
        public bool IsOpen;

        /// <summary>
        /// 地址重写后产生的Url
        /// </summary>
        public string FindUrl;

        /// <summary>
        /// 跳转到的页面地址
        /// </summary>
        public string ToUrl;

        /// <summary>
        /// 使用地址重写的Url
        /// </summary>
        public string RewriteUrl;

        /// <summary>
        /// 不使用地址重写的Url
        /// </summary>
        public string NoRewriteUrl;
    } ;
}