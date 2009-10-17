using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;
using TCG.Template.Handlers;
using TCG.News.Handlers;
using TCG.Entity;

using TCG.TCGTagReader.Handlers;

namespace TCG.URLRewriter
{
    public class HttpRequestHandler
    {
        /// <summary>
        /// 获得配置信息
        /// </summary>
        public Config config
        {
            get
            {
                if (this._config == null)
                {
                    this._config = new Config();
                }
                return this._config;
            }
        }
        private Config _config = null;

        /// <summary>
        /// 获得数据库链接
        /// </summary>
        public Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = (int)DBLinkNums.News;
                }
                return this._conn;
            }
        }
        private Connection _conn = null;

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="iHttpApplication"></param>
        /// <param name="iHttpContext"></param>
        /// <param name="Response"></param>
        /// <param name="Request"></param>
        public void Do(HttpApplication iHttpApplication, HttpContext iHttpContext, HttpResponse Response, HttpRequest Request)
        {
            if (!Boolean.Parse(config["IsReWrite"])) return;

            TCGTagHandlers tcgthdl = new TCGTagHandlers();

            //获得页面文件名
            string pagepath = Request.Url.Segments[Request.Url.Segments.Length - 1];

            //获得所有单页模版信息
            TemplateHandlers tplh = new TemplateHandlers();
            DataSet ds = tplh.GetTemplatesBySystemTypAndType(conn,0,0,false);

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    
                    //获得数据库配置URL信息
                    string texturl = config["WebSite"].Trim() + Row["vcUrl"].ToString().Trim();
                    if (texturl.IndexOf(config["FileExtension"]) == -1) texturl += config["FileExtension"];

                    if (texturl == Request.Url.AbsoluteUri)
                    {
                        tcgthdl.Template = Row["vcContent"].ToString();
                        tcgthdl.NeedCreate = false;
                        tcgthdl.Replace(conn, config);
                        
                        Response.Write(tcgthdl.Template);
                        Response.End();
                        tcgthdl = null;
                        return;
                    }
                }
            }

            string DpagePath = Request.Url.AbsolutePath;
            int DcurPage = 1;
            //检测文件名特性
            string pattern = @"([\s\S]*)-c(\d+)\" + config["FileExtension"];
            Match match = Regex.Match(pagepath, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success)
            {
                pagepath = match.Result("$1") + config["FileExtension"];
                DpagePath = DpagePath.Substring(0, DpagePath.LastIndexOf('/')) + "/" + pagepath;
                DcurPage = objectHandlers.ToInt(match.Result("$2"));
            }

            //检测分类文件
            classHandlers chld = new classHandlers();
            DataTable dt = chld.GetClassInfoByCach(conn,false);
            if (dt != null && dt.Rows.Count != 0)
            {
                if(!string.IsNullOrEmpty(pagepath))
                {
                    string [] p = pagepath.Split('.');
                    string exp = p[p.Length-1];
                    int num = DpagePath.LastIndexOf("." + exp);
                    string Qp = DpagePath.Substring(0, num);
                    string SQL = "vcUrl ='" + Qp + "'";
                    DataRow[] Rows = dt.Select(SQL);
                    if (Rows.Length > 0)
                    {
                        TemplateHandlers tlhdl = new TemplateHandlers();
                        TemplateInfo tlif = tlhdl.GetTemplateInfoByID(conn, objectHandlers.ToInt(Rows[0]["iListTemplate"]), false);

                        TCGTagHandlers tcgthdl1 = new TCGTagHandlers();
                        tcgthdl1.Template = tlif.vcContent.Replace("_$ClassId$_", Rows[0]["iId"].ToString());
                        tcgthdl1.NeedCreate = false;
                        tcgthdl1.PagerInfo.DoAllPage = false;
                        tcgthdl1.PagerInfo.Page = DcurPage;
                        tcgthdl1.WebPath = Rows[0]["vcUrl"].ToString() + config["FileExtension"];
                        tcgthdl1.Replace(conn, config);

                        Response.Write(tcgthdl1.Template);
                        Response.End();
                        tcgthdl1 = null;

                    }
                }
            }

            //检测文件名特性
            pattern = @"(\d+)t-([\s\S]*)\" + config["FileExtension"];
            match = Regex.Match(pagepath, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success)
            {
                int topicid = objectHandlers.ToInt(match.Result("$1"));
                if (topicid != 0)
                {
                    //获得文章对象
                    newsInfoHandlers nifhld = new newsInfoHandlers();
                    NewsInfo item = nifhld.GetNewsInfoById(conn, topicid);
                    if (item != null)
                    {
                        //获得分类信息
                       
                        TemplateHandlers ntlhdl = new TemplateHandlers();
                        TemplateInfo titem = ntlhdl.GetTemplateInfoByID(conn, item.ClassInfo.iTemplate,false);
                       

                        TCGTagHandlers tcgth = new TCGTagHandlers();
                        tcgth.Template = titem.vcContent.Replace("_$Id$_", item.iId.ToString());
                        titem = null;
                        tcgth.NeedCreate = false;
                        tcgth.Replace(conn, config);
                        
                        Response.Write(tcgth.Template);
                        Response.End();
                        tcgth = null;
                        return;
                    }
                    nifhld = null;
                    return;
                }
            }

            tplh = null;

        }
    }
}
