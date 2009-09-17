using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using TCG.Data;

namespace TCG.Template
{
    /// <summary>
    /// TempLate 的摘要说明
    /// </summary>
    public class Template
    {
        public Template()
        {
            this._pattern = @"<tcg:([^<>]+)\s([^<>]+)>([\S\s]*?)</tcg:\1>";
            this._tcgsystemtag = "<!--TCG:{0}-->";
        }

        public void Replace(Connection conn)
        {
            MatchCollection mc = Regex.Matches(this._template, this._pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                this._tagtemplates = new List<TagItem>();
                foreach (Match item in mc)
                {
                    this._tagtemplate = new TagItem();
                    this._tagtemplate.Attribute = item.Result("$2");
                    this._tagtemplate.Tag = string.Format(this._tcgsystemtag, item.Index);
                    this._tagtemplate.TagText = item.Result("$3");
                    this._tagtemplate.TagType = item.Result("$1");

                    this._template = this._template.Replace(item.Value, this._tagtemplate.Tag);
                    this._tagtemplates.Add(this._tagtemplate);
                    this._tagtemplate = null;
                }

                string sql = "";
                if (this._tagtemplates.Count > 0)
                {
                    for (int i = 0; i < this._tagtemplates.Count; i++)
                    {
                        string text1 = this._tagtemplates[i].GetAttribute("dbexc");
                        if (!string.IsNullOrEmpty(text1))
                        {
                            sql += text1 + ";";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sql))
                {
                    DataSet ds = conn.GetDataSet(sql);
                    if (ds.Tables.Count == this._tagtemplates.Count)
                    {
                        for (int i = 0; i < this._tagtemplates.Count; i++)
                        {
                            this._tagtemplates[i].ReplaceTagText(ds.Tables[i]);
                            this._template = this._tagtemplates[i].Replace(this._template);
                        }
                    }
                }
            }
            else
            {
                return;
            }

            mc = null;
            this.Replace(conn);
        }

        /// <summary>
        ///设置获得模板
        /// </summary>
        public string TempHtml { get { return this._template; } set { this._template = value; } }
        /// <summary>
        /// 标签规则
        /// </summary>
        public string Pattern { get { return this._pattern; } set { this._pattern = value; } }
        /// <summary>
        /// 系统标签
        /// </summary>
        public string SystemTag { get { return this._tcgsystemtag; } set { this._tcgsystemtag = value; } }

        public List<TagItem> Tagtemplates { get { return this._tagtemplates; } set { this._tagtemplates = value; } }

        private string _template = string.Empty;    //模板文件
        private string _pattern = string.Empty;     //正则字符串
        private string _tcgsystemtag = string.Empty;//零时替换的系统标签
        private List<TagItem> _tagtemplates = null;
        private TagItem _tagtemplate = null;
    }
}