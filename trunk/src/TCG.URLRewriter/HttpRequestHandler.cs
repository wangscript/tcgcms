using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;
using TCG.Handlers;


namespace TCG.URLRewriter
{
    public class HttpRequestHandler
    {
        /// <summary>
        /// 获得配置信息
        /// </summary>
        public ConfigService configService
        {
            get
            {
                if (this._configservice == null)
                {
                    this._configservice = new ConfigService();
                }
                return this._configservice;
            }
        }
        private ConfigService _configservice = null;

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
                }
                return this._conn;
            }
        }
        private Connection _conn = null;

        /// <summary>
        /// 程序开始时间
        /// </summary>
        DateTime startTime;

        /// <summary>
        /// 替换程序运行时间
        /// </summary>
        /// <param name="txt"></param>
        private void TimeReplace(ref string tag)
        {
            DateTime endTime = DateTime.Now;
            System.TimeSpan mySpan = endTime - startTime;

            tag = tag.Replace("_$SystemTime$_", mySpan.TotalSeconds.ToString());
            tag = tag.Replace("_$SystemQuery$_", this._conn.Queries.ToString());
            
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="iHttpApplication"></param>
        /// <param name="iHttpContext"></param>
        /// <param name="Response"></param>
        /// <param name="Request"></param>
        public void Do(HttpApplication iHttpApplication, HttpContext iHttpContext, HttpResponse Response, HttpRequest Request)
        {
            if (!Boolean.Parse(configService.baseConfig["IsReWrite"])) return;

            startTime = DateTime.Now;
            string OutHtml = string.Empty;

            TCGTagHandlers tcgthdl = new TCGTagHandlers();
            HandlerService handlerservice = new HandlerService(conn, configService);
            tcgthdl.handlerService = handlerservice;

            //获得页面文件名
            string pagepath = Request.Url.Segments[Request.Url.Segments.Length - 1];

            //获得所有单页模版信息

            DataSet ds = handlerservice.skinService.templateHandlers.GetTemplatesBySystemTypAndType(conn, 0, 0, false);

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    
                    //获得数据库配置URL信息
                    string texturl = configService.baseConfig["WebSite"].Trim() + Row["vcUrl"].ToString().Trim();
                    if (texturl.IndexOf(configService.baseConfig["FileExtension"]) == -1) texturl += configService.baseConfig["FileExtension"];

                    if (texturl == Request.Url.AbsoluteUri)
                    {
                        tcgthdl.Template = Row["vcContent"].ToString();
                        tcgthdl.NeedCreate = false;
                        tcgthdl.Replace(conn, configService.baseConfig);
                        OutHtml = tcgthdl.Template;


                        TimeReplace(ref OutHtml);
                        Response.Write(OutHtml);
                        Response.End();
                        tcgthdl = null;
                        return;
                    }
                }
            }

            string DpagePath = Request.Url.AbsolutePath;
            int DcurPage = 1;
            //检测文件名特性
            string pattern = @"([\s\S]*)-c(\d+)\" + configService.baseConfig["FileExtension"];
            Match match = Regex.Match(pagepath, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success)
            {
                pagepath = match.Result("$1") + configService.baseConfig["FileExtension"];
                DpagePath = DpagePath.Substring(0, DpagePath.LastIndexOf('/')) + "/" + pagepath;
                DcurPage = objectHandlers.ToInt(match.Result("$2"));
            }

            //检测分类文件
            DataTable dt = handlerservice.skinService.categoriesHandlers.GetCategoriesByCach(conn, false);
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
                        Template tlif = tlhdl.GetTemplateByID(conn, Rows[0]["iListTemplate"].ToString(), false);

                        TCGTagHandlers tcgthdl1 = new TCGTagHandlers();
                        tcgthdl1.handlerService = handlerservice;
                        tcgthdl1.Template = tlif.Content.Replace("_$ClassId$_", Rows[0]["Id"].ToString());
                        tcgthdl1.NeedCreate = false;
                        tcgthdl1.PagerInfo.DoAllPage = false;
                        tcgthdl1.PagerInfo.Page = DcurPage;
                        tcgthdl1.WebPath = Rows[0]["vcUrl"].ToString() + configService.baseConfig["FileExtension"];
                        tcgthdl1.Replace(conn, configService.baseConfig);

                        OutHtml = tcgthdl1.Template;


                        TimeReplace(ref OutHtml);
                        Response.Write(OutHtml);
                        Response.End();
                        tcgthdl1 = null;
                        return;
                    }
                }
            }

            //检测文件名特性
            pattern = @"\d{8}\/([a-z0-9]{8}\-[a-z0-9]{4}-[a-z0-9]{4}\-[a-z0-9]{4}\-[a-z0-9]{12})\" + configService.baseConfig["FileExtension"];
            match = Regex.Match(DpagePath, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success)
            {
                string topicid = match.Result("$1");
                if (!string.IsNullOrEmpty(topicid))
                {
                    //获得文章对象

                    Resources item = handlerservice.resourcsService.resourcesHandlers.GetNewsInfoById(conn, topicid);
                    if (item != null)
                    {
                        //获得分类信息

                        Template titem = handlerservice.skinService.templateHandlers.GetTemplateByID(conn, item.ClassInfo.iTemplate, false);
                       

                        TCGTagHandlers tcgth = new TCGTagHandlers();
                        tcgth.handlerService = handlerservice;
                        tcgth.Template = titem.Content.Replace("_$Id$_", item.Id.ToString());
                        titem = null;
                        tcgth.NeedCreate = false;
                        tcgth.Replace(conn, configService.baseConfig);

                        OutHtml = tcgth.Template;

                        TimeReplace(ref OutHtml);
                        Response.Write(OutHtml);
                        Response.End();
                        tcgth = null;
                        return;
                    }
   
                    return;
                }
            }

        }
    }
}
