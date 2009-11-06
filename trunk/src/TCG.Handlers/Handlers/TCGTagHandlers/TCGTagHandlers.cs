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
using System.Web;
using System.IO;
using System.Data;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;
using TCG.Release;

namespace TCG.Handlers
{
    /// <summary>
    /// TempLate ��ժҪ˵��
    /// </summary>
    public class TCGTagHandlers : TCGTagBase
    {
        public TCGTagHandlers(Connection conn, ConfigService configservice,HandlerService handlerService)
        {
            base.conn = conn;
            base.handlerService = handlerService;
            base.configService = configservice;

            this._pattern = @"<tcg:([^<>]+)\s([^<>]+)>([\S\s]*?)</tcg:\1>";
            this._tcgsystemtag = "<!--TCG:{0}-->";
            this._pagerinfo = new TCGTagPagerInfo();
        }

        public bool Replace(Connection conn, Dictionary<string, string> config)
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
                    this._tagtemplate = new TCGTagAttributeHandlers(base.conn,base.configService,base.handlerService);
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
                    this._tagtemplates[i].ReplaceTagText(ref this._pagerinfo);
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

                if (this._pagerinfo.NeedPager && this._pagerinfo.DoAllPage &&this._pagerinfo.Page > 1)
                {
                    Match item = Regex.Match(this._listtemp.TagHtml, this._pattern,RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    if (item.Success)
                    {
                        this._tagtemplate = new TCGTagAttributeHandlers(base.conn, base.configService, base.handlerService);
                        this._tagtemplate.Attribute = this.ReplaceAttribute(item.Result("$2"));
                        this._tagtemplate.Tag = string.Format(this._tcgsystemtag, this._index);
                        this._tagtemplate.TagText = item.Result("$3");
                        this._tagtemplate.TagType = item.Result("$1");
                        this._tagtemplate.TagHtml = item.Value;

                        this._tagtemplate.ReplaceTagText(ref this._pagerinfo);
                        this._listtemp.TagText = this._tagtemplate.TagText;
                        this._temphtml = this._template;
                        this._tagtemplate = null;
                    }
                }

                if (this._listtemp != null)
                {
                    if (this._pagerinfo.NeedPager)
                    {
                        if (this._pagerinfo.Page == 1 || !this._pagerinfo.DoAllPage)
                        {
                            this._template = this._temphtml;
                        }
                    }
                    this._temphtml = this._listtemp.Replace(this._temphtml);
                    this._temphtml = this._temphtml.Replace("__$pager$__", this.GetPager());
                }
                this._start = false;
                this.Save();
                if (this._pagerinfo.NeedPager&&this._pagerinfo.DoAllPage)
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

        private void SysteConfigReplace(Dictionary<string, string> config)
        {
            this._temphtml = _temphtml.Replace("_$FileSite$_", config["FileSite"]);
            this._temphtml = _temphtml.Replace("_$FileExtension$_", config["FileExtension"]);
            this._temphtml = _temphtml.Replace("_$WebSite$_", config["WebSite"]);
            this._temphtml = _temphtml.Replace("_$PageSize$_", config["PageSize"]);
            this._temphtml = _temphtml.Replace("_$WebTitle$_", config["WebTitle"]);
            this._temphtml = _temphtml.Replace("_$WebKeyWords$_", config["WebKeyWords"]);
            this._temphtml = _temphtml.Replace("_$WebDescription$_", config["WebDescription"]);
            this._temphtml = _temphtml.Replace("_$SoftCopyright$_", Versions.version);
            this._temphtml = _temphtml.Replace("_$SoftWebSite$_", Versions.WebSite);
            this._temphtml = _temphtml.Replace("_$SystemName$_", Versions.SystemName);
            this._temphtml = _temphtml.Replace("_$author$_", Versions.Author);
            this._temphtml = _temphtml.Replace("_$ManagePath$_", config["ManagePath"]);
        }

        /// <summary>
        /// �������±���
        /// </summary>
        private void UpdateTopicsTitle()
        {
            if (!string.IsNullOrEmpty(this._pagerinfo.PageTitle))
            {
                Match mh = Regex.Match(this._temphtml, @"(<title>)(.*?)(</title>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (mh.Success)
                {
                    this._temphtml = Regex.Replace(this._temphtml, @"(<title>)(.*?)(</title>)",
                    "$1" + this._pagerinfo.PageTitle + " - $2$3", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    this._pagerinfo.PageTitle = string.Empty;
                }

                mh = null;
            }
        }

        /// <summary>
        /// ����CSS�ͽű�����
        /// </summary>
        private void UpdateScriptCss()
        {
            if (!string.IsNullOrEmpty(this._pagerinfo.ScriptCss))
            {
                string text = string.Empty;
                if (this._pagerinfo.ScriptCss.IndexOf("|") > -1)
                {
                    string[] txt = this._pagerinfo.ScriptCss.Split('|');
                    for (int i = 0; i < txt.Length; i++)
                    {
                        string text2 = (i == txt.Length - 1) ? "" : "\r\n"; 
                        if (txt[i].IndexOf(".css") > -1)
                        {
                            text += "<link href=\"" + txt[i] + "\" rel=\"stylesheet\" type=\"text/css\" />" + text2;
                        }
                        else
                        {
                            text += "<script language=\"javascript\" src=\"" + txt[i] + "\"></script>" + text2;
                        }
                    }
                }
                else
                {
                    if (this._pagerinfo.ScriptCss.IndexOf(".css") > -1)
                    {
                        text += "<link href=\"" + this._pagerinfo.ScriptCss + "\" rel=\"stylesheet\" type=\"text/css\" />";
                    }
                    else
                    {
                        text += "<script language=\"javascript\" src=\"" + this._pagerinfo.ScriptCss + "\"></script>";
                    }
                }

                Match head = Regex.Match(this._temphtml, @"(<head>)([\s\S]*)(</head>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (head.Success)
                {
                    this._temphtml = Regex.Replace(this._temphtml, @"(<head>)([\s\S]*)(</head>)",
                    "$1$2" + text + "\r\n$3", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    this._pagerinfo.ScriptCss = string.Empty;
                }

                head = null;
            }

        }

        private void Save()
        {
            this.UpdateTopicsTitle();
            this.UpdateScriptCss();

            if (NeedCreate)
            {
                objectHandlers.SaveFile(this.GetFilePath(), this._temphtml);
            }
        }

        private string GetFilePath()
        {
            if (this._pagerinfo.NeedPager && this._pagerinfo.Page != 1)
            {
                string text1 = this._filepath.Substring(0, this._filepath.LastIndexOf("."));
                string text2 = this._filepath.Substring(this._filepath.LastIndexOf("."), this._filepath.Length - this._filepath.LastIndexOf("."));
                return text1 + "-c" + this._pagerinfo.Page.ToString() + text2;
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
                return pager(text1 + "-c{0}" + text2, this._pagerinfo.Page, this._pagerinfo.PageCount, this._pagerinfo.TopicCount, this._pagerinfo.curPage, true);
            }
            return "";
        }


        private string setPage(string s, int i)
        {
            if (i == 1)
            {
                return s.Replace("-c{0}", "").Replace("{p}", "");
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

        /// <summary>
        /// ��ǰҳ
        /// </summary>
        public int CurrentPage { get { return this._currentpage; } set { this._currentpage = value; } }

        public string WebPath { get { return this._webpath; } set { this._webpath = value; } }
        /// <summary>
        ///���û��ģ��
        /// </summary>
        public string Template { set { this._template = value; } get { return this._temphtml; } }
        /// <summary>
        /// ��ǩ����
        /// </summary>
        public string Pattern { get { return this._pattern; } set { this._pattern = value; } }
        /// <summary>
        /// ϵͳ��ǩ
        /// </summary>
        public string SystemTag { get { return this._tcgsystemtag; } set { this._tcgsystemtag = value; } }
        /// <summary>
        /// �ļ�����·��
        /// </summary>
        public string FilePath { get { return this._filepath; } set { this._filepath = value; } }
        /// <summary>
        /// ��Ҫ�����ļ�
        /// </summary>
        public bool NeedCreate { get { return this._needcreate; } set { this._needcreate = value; } }

        public TCGTagPagerInfo PagerInfo { get { return this._pagerinfo; } set { this._pagerinfo = value; } }

        public List<TCGTagAttributeHandlers> Tagtemplates { get { return this._tagtemplates; } set { this._tagtemplates = value; } }

        private TCGTagPagerInfo _pagerinfo = null;                                //�Ƿ���Ҫ��ҳ
        private string _template = string.Empty;                        //ģ���ļ�
        private string _pattern = string.Empty;                         //�����ַ���
        private string _tcgsystemtag = string.Empty;                    //��ʱ�滻��ϵͳ��ǩ
        private string _temphtml = string.Empty;                        //�������ģ��
        private string _filepath = string.Empty;                        //�ļ�����·��
        private int _index = 0;                                      //�ݴ��ǩ���
        private bool _start = false;                                     //��ʼ���
        private string _webpath = string.Empty;                         //ҳ��·��
        private TCGTagAttributeHandlers _listtemp = null;             //�б���
        private List<TCGTagAttributeHandlers> _tagtemplates = null;
        private TCGTagAttributeHandlers _tagtemplate = null;
        private bool _needcreate = true;
        private int _currentpage = 0;                         /// ��ǰҳ
                                                              /// 
    }
}