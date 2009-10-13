using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Data;
using TCG.Template.Entity;
using TCG.Template.Handlers;
using TCG.News.Handlers;
using TCG.News.Entity;
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

            //检测文件名特性
            string pattern = @"(\d+)t-([\s\S]*)\" + config["FileExtension"];
            Match match = Regex.Match(pagepath, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
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
                        TemplateInfo titem = ntlhdl.GetTemplateInfoByID(conn, item.ClassInfo.iTemplate);
                       

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
