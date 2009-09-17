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
    /// ��ȡ��ַ���µĻ������
    /// </summary>
    public class HttpRewrites
    {
        /// <summary>
        /// ��ַ���µĻ�����Ϣ����
        /// </summary>
        private string Key = "ReWrite";

        /// <summary>
        /// ��ص������ļ�·��
        /// </summary>
        private string File = "Config/ReWrite.config";

        /// <summary>
        /// ������Ļ�����Ϣ DataTable
        /// </summary>
        public DataTable Table = null;

        /// <summary>
        /// ��ȡ��ַ���µĻ������
        /// </summary>
        public HttpRewrites()
        {
            this.Table = null;
            this.Initialize();
        }

        /// <summary>
        /// ��������
        /// </summary>
        ~HttpRewrites()
        {
            this.Clear();
        }

        /// <summary>
        /// ��ʼ����
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
        /// ��ȡĳ����ַ��д��DataRow����
        /// </summary>
        /// <param name="IndexKey">��ǰ��Ψһ������</param>
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
        /// ֱ��ָ���ַ��д
        /// </summary>
        /// <param name="Application">HttpApplication ����</param>
        /// <param name="Context">HttpContext ����</param>
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
                if (!IsOpen) continue;  //�����ʹ�õ�ַ��д,��������һ����¼;
                Regex Reg = new Regex("^" + Rs["FindUrl"].ToString() + "$", RegexOptions.IgnoreCase);
                if (Reg.IsMatch(Url))
                {
                    //�ҵ�ƥ��
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
        /// ��ȡ����Ŀ¼
        /// </summary>
        /// <param name="AppPath">string ApplicationPath</param>
        /// <param name="Url">��ǰ��ַ</param>
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
        /// �������� ��ַ���µĻ���
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
        /// ��ǰ����ĸ�Ŀ¼
        /// </summary>
        private string GetPath
        {
            get { return Fetch.MapPath(this.File); }
        }

        /// <summary>
        /// �Ƴ���ǰ����
        /// </summary>
        public void Remove()
        {
            Caching.Remove(this.Key);
        }

        /// <summary>
        /// �����Դ
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
    /// Rewrite.config �ļ���ָ��Ŀ¼�ṹ
    /// </summary>
    public class RewriteKey
    {
        /// <summary>
        /// Item ��
        /// </summary>
        public static string RoleItem = "RoleItem";

        /// <summary>
        /// Ψһ��������
        /// </summary>
        public static string IndexName = "IndexName";

        /// <summary>
        /// �Ƿ�����ǰʹ����
        /// </summary>
        public static string IsOpen = "IsOpen";

        /// <summary>
        /// ��ַ��д�������Url
        /// </summary>
        public static string FindUrl = "FindUrl";

        /// <summary>
        /// ��ת����ҳ���ַ
        /// </summary>
        public static string ToUrl = "ToUrl";

        /// <summary>
        /// ʹ�õ�ַ��д��Url
        /// </summary>
        public static string RewriteUrl = "RewriteUrl";

        /// <summary>
        /// ��ʹ�õ�ַ��д��Url
        /// </summary>
        public static string NoRewriteUrl = "NoRewriteUrl";
    } ;

    /// <summary>
    /// ��Rewrite.config�ĵ����п��õĸ�ʽ��
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RewriteFileItem
    {
        /// <summary>
        /// Ψһ��������
        /// </summary>
        public string IndexName;

        /// <summary>
        /// �Ƿ�����ǰʹ����
        /// </summary>
        public bool IsOpen;

        /// <summary>
        /// ��ַ��д�������Url
        /// </summary>
        public string FindUrl;

        /// <summary>
        /// ��ת����ҳ���ַ
        /// </summary>
        public string ToUrl;

        /// <summary>
        /// ʹ�õ�ַ��д��Url
        /// </summary>
        public string RewriteUrl;

        /// <summary>
        /// ��ʹ�õ�ַ��д��Url
        /// </summary>
        public string NoRewriteUrl;
    } ;
}