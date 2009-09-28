using System;
using System.Web;
using System.IO;
using System.Data;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using TCG.Data;
using TCG.Utils;
using TCG.TCGTagReader.Entity;
using TCG.Release;

namespace TCG.TCGTagReader.Handlers
{
    /// <summary>
    /// TempLate 的摘要说明
    /// </summary>
    public class TCGTagHandlers
    {
        public TCGTagHandlers()
        {
            this._pattern = @"<tcg:([^<>]+)\s([^<>]+)>([\S\s]*?)</tcg:\1>";
            this._tcgsystemtag = "<!--TCG:{0}-->";
            this._pagerinfo = new TCGTagPagerInfo();
        }

        public bool Replace(Connection conn,Config config)
        {
            if (!this._start)
            {
                this._temphtml = this._template;
                this._start = true;
            }
            this.SysteConfigReplace(config);
            MatchCollection mc = Regex.Matches(this._temphtml, this._pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                this._tagtemplates = new List<TCGTagAttributeHandlers>();
                foreach (Match item in mc)
                {
                    this._tagtemplate = new TCGTagAttributeHandlers();
                    this._tagtemplate.Attribute = this.ReplaceAttribute(item.Result("$2"));
                    this._tagtemplate.Tag = string.Format(this._tcgsystemtag, this._index);
                    this._tagtemplate.TagText = item.Result("$3");
                    this._tagtemplate.TagType = item.Result("$1");
                    this._tagtemplate.TagHtml = item.Value;

                    this._temphtml = this._temphtml.Replace(item.Value, this._tagtemplate.Tag);
                    this._tagtemplates.Add(this._tagtemplate);
                    this._tagtemplate = null;
                    this._index++;
                }

                for (int i = 0; i < this._tagtemplates.Count; i++)
                {
                    this._tagtemplates[i].ReplaceTagText(conn,config, ref this._pagerinfo);
                    if (this._tagtemplates[i].Pager)
                    {
                        this._listtemp = null;
                        this._listtemp = this._tagtemplates[i];
                    }
                    else
                    {
                        this._temphtml = this._tagtemplates[i].Replace(this._temphtml);
                    }
                }

                this._tagtemplates = null;
            }
            else
            {
                if (!this._pagerinfo.Read) return this._pagerinfo.Read;

                if (this._pagerinfo.NeedPager && this._pagerinfo.Page > 1)
                {
                    Match item = Regex.Match(this._listtemp.TagHtml, this._pattern,RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    if (item.Success)
                    {
                        this._tagtemplate = new TCGTagAttributeHandlers();
                        this._tagtemplate.Attribute = this.ReplaceAttribute(item.Result("$2"));
                        this._tagtemplate.Tag = string.Format(this._tcgsystemtag, this._index);
                        this._tagtemplate.TagText = item.Result("$3");
                        this._tagtemplate.TagType = item.Result("$1");
                        this._tagtemplate.TagHtml = item.Value;

                        this._tagtemplate.ReplaceTagText(conn,config, ref this._pagerinfo);
                        this._listtemp.TagText = this._tagtemplate.TagText;
                        this._temphtml = this._template;
                        this._tagtemplate = null;
                    }
                }

                if (this._listtemp != null)
                {
                    if (this._pagerinfo.NeedPager && this._pagerinfo.Page == 1) this._template = this._temphtml;
                    this._temphtml = this._listtemp.Replace(this._temphtml);
                    this._temphtml = this._temphtml.Replace("__$pager$__", this.GetPager());
                }
                this._start = false;
                this.Save();
                if (this._pagerinfo.NeedPager)
                {
                    this._pagerinfo.Page++;
                    if (this._pagerinfo.Page <= this._pagerinfo.PageCount && this._pagerinfo.PageCount != 0)
                    {
                        this.Replace(conn, config);
                    }
                }
                return this._pagerinfo.Read;
            }

            if (!this._pagerinfo.Read) return this._pagerinfo.Read;
            mc = null;
            return this.Replace(conn,config);
        }

        private string ReplaceAttribute(string str)
        {
            str = str.Replace("_@", "<");
            str = str.Replace("@_", ">");
            return str;
        }

        private void SysteConfigReplace(Config config)
        {
            this._temphtml = _temphtml.Replace("_$FileSite$_", config["FileSite"]);
            this._temphtml = _temphtml.Replace("_$FileExtension$_", config["FileExtension"]);
            this._temphtml = _temphtml.Replace("_$WebSite$_", config["WebSite"]);
            this._temphtml = _temphtml.Replace("_$ScriptsSite$_", config["ScriptsSite"]);
            this._temphtml = _temphtml.Replace("_$PageSize$_", config["PageSize"]);
            this._temphtml = _temphtml.Replace("_$WebTitle$_", config["WebTitle"]);
            this._temphtml = _temphtml.Replace("_$WebKeyWords$_", config["WebKeyWords"]);
            this._temphtml = _temphtml.Replace("_$WebDescription$_", config["WebDescription"]);
            this._temphtml = _temphtml.Replace("_$SoftCopyright$_", Versions.version);
            this._temphtml = _temphtml.Replace("_$SoftWebSite$_", Versions.WebSite);
            this._temphtml = _temphtml.Replace("_$author$_", Versions.Author);
        }

        private void Save()
        {
            if (NeedCreate)
            {
                Text.SaveFile(this.GetFilePath(), this._temphtml);
            }
        }

        private string GetFilePath()
        {
            if (this._pagerinfo.NeedPager && this._pagerinfo.Page != 1)
            {
                string text1 = this._filepath.Substring(0, this._filepath.LastIndexOf("."));
                string text2 = this._filepath.Substring(this._filepath.LastIndexOf("."), this._filepath.Length - this._filepath.LastIndexOf("."));
                return text1 + this._pagerinfo.Page.ToString() + text2;
            }
            else
            {
                return this._filepath;
            }
        }

        private string GetPager()
        {
            if (this._pagerinfo.NeedPager)
            {
                string text1 = this._webpath.Substring(0, this._webpath.LastIndexOf("."));
                string text2 = this._webpath.Substring(this._webpath.LastIndexOf("."), this._webpath.Length - this._webpath.LastIndexOf("."));
                return pager(text1 + "{0}" + text2, this._pagerinfo.Page, this._pagerinfo.PageCount, this._pagerinfo.TopicCount, this._pagerinfo.curPage, true);
            }
            return "";
        }


        private string setPage(string s, int i)
        {
            if (i == 1)
            {
                return s.Replace("{0}", "").Replace("{p}", "");
            }
            else
            {
                return s.Replace("{0}", i.ToString()).Replace("{p}", i.ToString());
            }
        }


        private string pager(string url, int page, int maxPage, int total, int per, bool countsIsVisible)
        {

            string s = string.Empty;

            for (int i = 1; i <= maxPage; i++)
            {
                if (page == i)
                {
                    s += ("<a href='" + setPage(url,i) + "' style='color:red'>" + i.ToString() + "</a> ");
                }
                else
                {
                    s += ("<a href='" + setPage(url, i) + "' >" + i.ToString() + "</a> ");
                }
            }

            return s;
        }




        public string WebPath { get { return this._webpath; } set { this._webpath = value; } }
        /// <summary>
        ///设置获得模板
        /// </summary>
        public string Template { set { this._template = value; } get { return this._temphtml; } }
        /// <summary>
        /// 标签规则
        /// </summary>
        public string Pattern { get { return this._pattern; } set { this._pattern = value; } }
        /// <summary>
        /// 系统标签
        /// </summary>
        public string SystemTag { get { return this._tcgsystemtag; } set { this._tcgsystemtag = value; } }
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FilePath { get { return this._filepath; } set { this._filepath = value; } }
        /// <summary>
        /// 需要生成文件
        /// </summary>
        public bool NeedCreate { get { return this._needcreate; } set { this._needcreate = value; } }

        public TCGTagPagerInfo PagerInfo { get { return this._pagerinfo; } set { this._pagerinfo = value; } }

        public List<TCGTagAttributeHandlers> Tagtemplates { get { return this._tagtemplates; } set { this._tagtemplates = value; } }

        private TCGTagPagerInfo _pagerinfo = null;                                //是否需要分页
        private string _template = string.Empty;                        //模板文件
        private string _pattern = string.Empty;                         //正则字符串
        private string _tcgsystemtag = string.Empty;                    //零时替换的系统标签
        private string _temphtml = string.Empty;                        //处理过程模版
        private string _filepath = string.Empty;                        //文件保存路径
        private int _index = 0;                                      //暂存标签编号
        private bool _start = false;                                     //开始标记
        private string _webpath = string.Empty;                         //页面路径
        private TCGTagAttributeHandlers _listtemp = null;             //列表缓存
        private List<TCGTagAttributeHandlers> _tagtemplates = null;
        private TCGTagAttributeHandlers _tagtemplate = null;
        private bool _needcreate = true;
    }
}