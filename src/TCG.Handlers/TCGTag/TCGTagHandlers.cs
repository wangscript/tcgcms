/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
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
    /// TempLate 的摘要说明
    /// </summary>
    public class TCGTagHandlers : TCGTagBase
    {
        public TCGTagHandlers(HandlerService handlerService)
        {

            base.handlerService = handlerService;
            this._pattern = @"<tcg:([^<>]+)\s([^<>]+)>([\S\s]*?)</tcg:\1>";
            this._tcgsystemtag = "<!--TCG:{0}-->";
            this._pagerinfo = new TCGTagPagerInfo();
        }

        public bool Replace()
        {
            if (!this._start)
            {
                this._temphtml = this._template;
                this._start = true;
            }
            this.SysteConfigReplace();
            MatchCollection mc = Regex.Matches(this._temphtml, this._pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                this._tagtemplates = new List<TCGTagAttributeHandlers>();
                foreach (Match item in mc)
                {
                    this._tagtemplate = new TCGTagAttributeHandlers(base.handlerService);

                    this._tagtemplate.Attribute = item.Result("$2");
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
                    this._temphtml = this._tagtemplates[i].Replace(this._temphtml);
                }

                this._tagtemplates = null;

                return this.Replace();
            }
            else
            {
                this.Save();
                return this._pagerinfo.Read;
            }

        }


        private void SysteConfigReplace()
        {
            if (SkinInfo == null) return;
            this._pagerinfo.SkinInfo = SkinInfo;
            this._temphtml = _temphtml.Replace("_$FileExtension$_", ConfigServiceEx.baseConfig["FileExtension"]);
            this._temphtml = _temphtml.Replace("_$WebSite$_", ConfigServiceEx.baseConfig["WebSite"]);
            this._temphtml = _temphtml.Replace("_$PageSize$_", ConfigServiceEx.baseConfig["PageSize"]);
            this._temphtml = _temphtml.Replace("_$WebTitle$_", SkinInfo.Name);
            this._temphtml = _temphtml.Replace("_$SkinId$_", SkinInfo.Id);
            this._temphtml = _temphtml.Replace("_$WebKeyWords$_", SkinInfo.WebKeyWords);
            this._temphtml = _temphtml.Replace("_$WebDescription$_", SkinInfo.WebDescription);
            this._temphtml = _temphtml.Replace("_$IndexPage$_", SkinInfo.IndexPage);
            this._temphtml = _temphtml.Replace("_$SoftCopyright$_", Versions.version);
            this._temphtml = _temphtml.Replace("_$SoftWebSite$_", Versions.WebSite);
            this._temphtml = _temphtml.Replace("_$SkinPath$_", ConfigServiceEx.baseConfig["WebSite"] + "/skin/" + SkinInfo.Filename);
            this._temphtml = _temphtml.Replace("_$SystemName$_", Versions.SystemName);
            this._temphtml = _temphtml.Replace("_$author$_", Versions.Author);
            this._temphtml = _temphtml.Replace("_$MainPage$_", ConfigServiceEx.baseConfig["MainPage"]);
            this._temphtml = _temphtml.Replace("_$ManagePath$_", ConfigServiceEx.baseConfig["ManagePath"]);
        }

        /// <summary>
        /// 更新文章标题
        /// </summary>
        private void UpdateTopicsTitle()
        {
            if (!string.IsNullOrEmpty(this._pagerinfo.PageTitle))
            {
                Match mh = Regex.Match(this._temphtml, @"(<title>)(.+?)(</title>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (mh.Success)
                {
                    this._temphtml = Regex.Replace(this._temphtml, @"(<title>)(.+?)(</title>)",
                    "$1" + this._pagerinfo.PageTitle + " - $2$3", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    this._pagerinfo.PageTitle = string.Empty;
                }

                mh = null;
            }
        }

        /// <summary>
        /// 更新CSS和脚本链接
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
            this.GetPager();

            if (NeedCreate)
            {
                string text1 = this.GetFilePath();
                this._pagerinfo.CreatePagesNotic += "<a>生成文件:" + text1 + "</a>";
                objectHandlers.SaveFile(text1, this._temphtml);
            }
        }

        private string GetFilePath()
        {
            if (this._pagerinfo.NeedPager && this._pagerinfo.PageSep!=0)
            {
                string text1 = this._filepath.Substring(0, this._filepath.LastIndexOf("."));
                string text2 = this._filepath.Substring(this._filepath.LastIndexOf("."), this._filepath.Length - this._filepath.LastIndexOf("."));
                return text1 + "_" + this._pagerinfo.Page.ToString() + text2;
            }
            else
            {
                return this._filepath;
            }
        }

        public void GetPager()
        {
            Match mh = Regex.Match(this._temphtml, @"(<TcgPager>)(.+?)(</TcgPager>)", RegexOptions.Singleline | RegexOptions.Multiline);
            string pagerhtml = string.Empty;
            if (mh.Success)
            {
                pagerhtml = mh.Result("$2");
                string pageurlt = GetPagerWebPathTemplate();

                //替换上一页
                string pevtemplate = this.GetPagerNode("Pev", pagerhtml);
                pagerhtml = Regex.Replace(pagerhtml, @"<Pev>(.+?)</Pev>", GetPevHtml(pageurlt, pevtemplate, this._pagerinfo.Page), RegexOptions.Singleline | RegexOptions.Multiline);

                string nexttemplate = this.GetPagerNode("Next", pagerhtml);  //Cur Page
                pagerhtml = Regex.Replace(pagerhtml, @"<Next>(.+?)</Next>", GetNextHtml(pageurlt, nexttemplate, this._pagerinfo.Page, this._pagerinfo.PageCount), RegexOptions.Singleline | RegexOptions.Multiline);

                string pagetemplate = this.GetPagerNode("Page", pagerhtml);
                string Curpagetemplate = this.GetPagerNode("Cur", pagerhtml);
                int showcount = objectHandlers.ToInt(this.GetPagerNode("ShowCount", pagerhtml));

                string pagestemplate = this.GetPagerNode("Pages", pagerhtml);
                pagerhtml = Regex.Replace(pagerhtml, @"<Pages>(.+?)</Pages>", GetPagesUrlHtml(pageurlt, pagetemplate, Curpagetemplate,
                    this._pagerinfo.curPage, this._pagerinfo.PageCount, showcount), RegexOptions.Singleline | RegexOptions.Multiline);

                string firsttemplate = this.GetPagerNode("First", pagerhtml);
                pagerhtml = Regex.Replace(pagerhtml, @"<First>(.+?)</First>", GetFirstUrlHtml(firsttemplate), RegexOptions.Singleline | RegexOptions.Multiline);

                string lasttemplate = this.GetPagerNode("Last", pagerhtml);
                pagerhtml = Regex.Replace(pagerhtml, @"<Last>(.+?)</Last>", GetLastUrlHtml(pageurlt, lasttemplate, this._pagerinfo.PageCount), RegexOptions.Singleline | RegexOptions.Multiline);

                pagerhtml = pagerhtml.Replace("$pagecount$", this._pagerinfo.PageCount.ToString());
                pagerhtml = pagerhtml.Replace("$topiccount$", this._pagerinfo.TopicCount.ToString());
                pagerhtml = pagerhtml.Replace("$page$", this._pagerinfo.Page.ToString());
                pagerhtml = pagerhtml.Replace("$curpage$", this._pagerinfo.Page.ToString());

                Match mh1 = Regex.Match(pagerhtml, @"(<select\s[^<>]+>)(.+?)(</select>)", RegexOptions.Singleline | RegexOptions.Multiline);
                if (mh1.Success)
                {
                    string select1 = mh1.Result("$1");
                    string select2 = mh1.Result("$2");
                    string select3 = mh1.Result("$3");

                    string selecthtml = string.Empty;


                    for (int i = 1; i <= this._pagerinfo.PageCount; i++)
                    {
                        selecthtml += string.Format(select2, i == 1?this._webpath:string.Format(pageurlt, i), i, i == this._pagerinfo.curPage ? "selected" : "");
                    }


                    selecthtml = select1 + selecthtml + select3;

                    pagerhtml = Regex.Replace(pagerhtml, @"(<select\s[^<>]+>)(.+?)(</select>)", selecthtml, RegexOptions.Singleline | RegexOptions.Multiline);
                   
                }

                pagerhtml = pagerhtml + "<input type='hidden' value='" + pageurlt + "' id='txtpageurlt'>";
  
            }

            this._temphtml = Regex.Replace(this._temphtml, @"(<TcgPager>)(.+?)(</TcgPager>)", pagerhtml, RegexOptions.Singleline | RegexOptions.Multiline);
        }


        private string GetLastUrlHtml(string pageurlt, string lasttemplate,int pagecount)
        {
            return string.Format(lasttemplate, string.Format(pageurlt, pagecount));
        }

        private string GetFirstUrlHtml(string firsttemplate)
        {
            return string.Format(firsttemplate, this._webpath);
        }

        private string GetPagesUrlHtml(string pageurl, string page, string cur, int curpage, int pagecount, int showcount)
        {
            if (pagecount == 0) return "";
            if (pagecount == 1) return string.Format(cur, string.Format(pageurl, ""), curpage);

            string str = string.Empty;

            if (curpage < showcount)
            {
                for (int i = 1; i < showcount+1; i++)
                {
                    if (i <= pagecount)
                    {
                        if (i == curpage)
                        {
                            if (i == 1)
                            {
                                str += string.Format(cur, this._webpath, i);
                            }
                            else
                            {
                                str += string.Format(cur, string.Format(pageurl, i), i);
                            }
                        }
                        else
                        {
                            if (i == 1)
                            {
                                str += string.Format(page, this._webpath, i);
                            }
                            else
                            {
                                str += string.Format(page, string.Format(pageurl, i), i);
                            }

                        }
                    }

                }
            }
            else
            {
                for (int i = curpage - showcount / 2; i <=(curpage + showcount / 2);i++)
                {
                    if (i <= pagecount)
                    {
                        if (i == curpage)
                        {
                            if (i == 1)
                            {
                                str += string.Format(cur, this._webpath, i);
                            }
                            else
                            {
                                str += string.Format(cur, string.Format(pageurl, i), i);
                            }
                        }
                        else
                        {
                            if (i == 1)
                            {
                                str += string.Format(page, this._webpath, i);
                            }
                            else
                            {
                                str += string.Format(page, string.Format(pageurl, i), i);
                            }
                        }
                    }

                }
            }

            return str;
        }

        /// <summary>
        /// 获取上一页HTML
        /// </summary>
        /// <param name="pageurl"></param>
        /// <param name="curpage"></param>
        /// <returns></returns>
        private string GetPevHtml(string pageurl, string pevhtml, int curpage)
        {
            if (curpage > 1)
            {
                return string.Format(pevhtml, string.Format(pageurl, curpage - 1));
            }
            else
            {
                return string.Format(pevhtml, string.Format(pageurl, 1));
            }
        }

        /// <summary>
        /// 获得下一页地址
        /// </summary>
        /// <param name="pageurl"></param>
        /// <param name="curpage"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        private string GetNextHtml(string pageurl, string nexthtml, int curpage, int pagecount)
        {
            if (curpage == pagecount)
            {
                return string.Format(nexthtml, string.Format(pageurl, pagecount));
            }
            else
            {
                return string.Format(nexthtml, string.Format(pageurl, curpage + 1));
            }
        }

        /// <summary>
        /// 获得分页路径模板
        /// </summary>
        /// <returns></returns>
        private string GetPagerWebPathTemplate()
        {
            if (this._webpath.IndexOf("{0}") > -1) return this._webpath;
            string text1 = this._webpath.Substring(0, this._webpath.LastIndexOf("."));
            string text2 = this._webpath.Substring(this._webpath.LastIndexOf("."), this._webpath.Length - this._webpath.LastIndexOf("."));
            return text1 + "_{0}" + text2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageshtml"></param>
        /// <returns></returns>
        private string GetPagerNode(string NodeName, string pageshtml)
        {
            Match mh = Regex.Match(pageshtml, @"(<" + NodeName + ">)(.+?)(</" + NodeName + ">)", RegexOptions.Singleline | RegexOptions.Multiline);
            if (mh.Success)
            {
                return mh.Result("$2");
            }
            return "";
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get { return this._currentpage; } set { this._currentpage = value; } }

        public string WebPath { get { return this._webpath; } set { this._webpath = value; } }
        /// <summary>
        ///设置获得模板
        /// </summary>
        public string Template { set { this._template = value; } get { return this._template; } }
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
        /// 资源所在皮肤
        /// </summary>
        public Skin SkinInfo { get { return this._skin; } set { this._skin = value; } }
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
        private int _currentpage = 0;                         /// 当前页
        private Skin _skin = null;
    }
}