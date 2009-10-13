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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

using TCG.Data;
using TCG.Utils;
using TCG.TCGTagReader.Entity;
using TCG.News.Entity;
using TCG.News.Handlers;
using TCG.Template.Handlers;
using TCG.Template.Entity;
using TCG.Files.Handlers;

using TCG.Files.Utils;

namespace TCG.TCGTagReader.Handlers
{
    public class TCGTagAttributeHandlers
    {
        public TCGTagAttributeHandlers()
        {
            this._attpattern = @"=""([0-9A-Za-z\s,='!_\.%()$<>\/]+)""";
            this._tagstringhdl = new TCGTagStringFunHandlers();
        }

        /// <summary>
        /// ��ǩ�滻��HTML ���ݴ��ǩ �������ƥ��
        /// </summary>
        public string Tag { get { return this._tag; } set { this._tag = value; } }
        /// <summary>
        /// ��ǩ������HTML
        /// </summary>
        public string TagText { get { return this._tagtext; } set { this._tagtext = value; } }
        /// <summary>
        /// ��ǩITEM ����
        /// </summary>
        public string TagType { get { return this._tagtype; } set { this._tagtype = value; } }
        /// <summary>
        /// ��ǩ�����ַ���
        /// </summary>
        public string Attribute { get { return this._attribute; } set { this._attribute = value; } }
        /// <summary>
        /// ��ǩ��ȫ��HTML
        /// </summary>
        public string TagHtml { get { return this._taghtml; } set { this._taghtml = value; } }
        /// <summary>
        /// �Ƿ�Ϊ�б�����
        /// </summary>
        public bool Pager { get { return this._pager; } set { this._pager = value; } }

        public string Replace(string TempHtml)
        {
            return TempHtml.Replace(this._tag, this._tagtext);
        }

        public void ReplaceTagText(Connection conn,Config config, ref TCGTagPagerInfo pagerinfo)
        {
            if (this._tagtype == string.Empty || this._attribute == string.Empty || this._tag == string.Empty) return;
            string type = this.GetAttribute("type");
            if (type == "") return;
            this._conn = conn;
            this._config = config;
            switch (type)
            {
                case "newstemplate":
                    this.TagForNewsTemplate(ref pagerinfo);
                    break;
                case "newstopic":
                    this.TagForNewsTopic(ref pagerinfo);
                    break;
                case "newsclassinfo":
                    this.TagForNewsClassInfo(ref pagerinfo);
                    break;
                case "newslist":
                    this.TagForNewsList(ref pagerinfo);
                    break;
                case "newsclasslist":
                    this.TagForNewsClassList(ref pagerinfo);
                    break;
                case "scriptcss":
                    this.TagForScriptCss(ref pagerinfo);
                    break;
            }
        }

        /// <summary>
        /// ����ǩ����ñ�־����
        /// </summary>
        /// <param name="feild"></param>
        /// <returns></returns>
        public string GetAttribute(string feild)
        {
            string value = "";
            Match item = Regex.Match(this._attribute, feild + this._attpattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (item.Success)
            {
                value = item.Result("$1");
            }
            item = null;

            switch (feild)
            {
                case "condition":
                    value = this._tagstringhdl.StringConditionFun(this._conn, value);
                    break;
            }
            return value;
        }

        /// <summary>
        /// ��ñ�ǩ
        /// </summary>
        /// <param name="pagerinfo"></param>
        private void TagForScriptCss(ref TCGTagPagerInfo pagerinfo)
        {
            pagerinfo.ScriptCss = this.GetAttribute("text");
        }

        private void TagForNewsClassList(ref TCGTagPagerInfo pagerinfo)
        {
            this._conn.Dblink = DBLinkNums.News;
            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "T_News_ClassInfo";

            string fields = this.GetAttribute("fields");
            if (string.IsNullOrEmpty(fields)) return;
            string fieldC = fields.Replace("%", ",");

            string orders = this.GetAttribute("orders");
            int columns = Bases.ToInt(this.GetAttribute("columns"));
            if (columns == 0) columns = 1;

            string condition = this.GetAttribute("condition");
            if (string.IsNullOrEmpty(condition)) return;
            string SQL = "SELECT TOP " + columns + " " + fieldC + " FROM T_News_ClassInfo (NOLOCK) WHERE "
                + " iID >0 AND " + condition;

            if (!string.IsNullOrEmpty(orders))
            {
                SQL += " ORDER BY " + orders;
            }

            DataSet ds = this._conn.GetDataSet(SQL);

            if (ds == null) return;
            if (ds.Tables.Count != 0)
            {
                string tempOld = this._tagtext;
                string tempNew = string.Empty;
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    string temp = tempOld;
                    DataRow Row = ds.Tables[0].Rows[n];
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n + 1).ToString());
                    this.NewslistTagFieldsReplace(ref temp, fields, Row);
                    tempNew += temp;
                }
                this._tagtext = tempNew;
            }

            ds.Clear();
            ds.Dispose();
        }

        /// <summary>
        /// ����ƪ��Ѷ�ı�ǩ
        /// </summary>
        private void TagForNewsTopic(ref TCGTagPagerInfo pagerinfo)
        {
            int id = Bases.ToInt(this.GetAttribute("id"));
            if (id == 0)
            {
                pagerinfo.Read = false;
                return;
            }
            newsInfoHandlers nifhd = new newsInfoHandlers();
            classHandlers chdl = new classHandlers();
            fileinfoHandlers flfhdl = new fileinfoHandlers();
            NewsInfo item = nifhd.GetNewsInfoById(this._conn, id);
            if (item != null)
            {
                pagerinfo.PageTitle = item.vcTitle;

                if (!string.IsNullOrEmpty(item.vcTitleColor)) item.vcTitle = "<font color='" + item.vcTitleColor + "'>"
                    + item.vcTitle + "</font>";
                if (item.cStrong == "Y") item.vcTitle = "<strong>" + item.vcTitle + "</strong>";
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcTitle$", item.vcTitle);

                //�滻�������ݣ����ͼƬ
                try
                {
                    string tContent = flfhdl.ImgPatchInit(this._conn, item.vcContent, "admin",
                        objectHandlers.ToInt(this._config["NewsFileClass"]), this._config);
                    if (tContent != item.vcContent)
                    {
                        item.vcContent = tContent;
                        int outid = 0;
                        string filepatch = string.Empty;
                        nifhd.UpdateNewsInfo(this._conn, this._config["FileExtension"], item, ref outid);
                    }
                }
                catch { }
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcContent$", item.vcContent);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iId$", item.iId.ToString());

                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcFilePath$", item.vcFilePath);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcKeyWord$", item.vcKeyWord);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_dUpdateDate$", item.dUpDateDate.ToString("yyyy��MM��dd��"));
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_From$",
                    "<a href=\"" + item.FromInfo.vcUrl + "\" target=\"_blank\">" + item.FromInfo.vcTitle + "</a>");
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcKeyWord$", item.vcKeyWord);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcAuthor$", item.vcAuthor);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcShortContent$", Text.GetTextWithoutHtml(item.vcShortContent));
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcClassName$", item.ClassInfo.vcClassName);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iClassId$", item.ClassInfo.iId.ToString());
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_TopicClassTitleList$",
                    chdl.GetTopicClassTitleList(this._conn, this._config, item.ClassInfo.iId, " > "));

                
            }
            else
            {
                nifhd = null;
                item = null;
                chdl = null;
                flfhdl = null;
                pagerinfo.Read = false;
                return;
            }
            nifhd = null;
            flfhdl = null;
            item = null;
            chdl = null;
        }


        private void TagForNewsClassInfo(ref TCGTagPagerInfo pagerinfo)
        {
            int id = Bases.ToInt(this.GetAttribute("id"));
            if (id == 0)
            {
                pagerinfo.Read = false;
                return;
            }
            classHandlers chdl = new classHandlers();
            ClassInfo item = chdl.GetClassInfoById(this._conn, id, false);
            if (item == null)
            {
                pagerinfo.Read = false;
                chdl = null;
                return;
            }

            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iID$", item.iId.ToString());
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcName$", item.vcName);
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcClassName$", item.vcClassName);
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iParent$", item.iParent.ToString());
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_ClassTitleList$",
                    chdl.GetTopicClassTitleList(this._conn, this._config, item.iId, " > "));

            pagerinfo.PageTitle = item.vcClassName;

            chdl = null;
            item = null;

        }

        /// <summary>
        /// ������ǩģ��ı�ǩ
        /// </summary>
        private void TagForNewsTemplate(ref TCGTagPagerInfo pagerinfo)
        {
            int id = Bases.ToInt(this.GetAttribute("id"));
            if (id == 0)
            {
                pagerinfo.Read = false;
                return;
            }
            TemplateHandlers ntlhdl = new TemplateHandlers();
            TemplateInfo item = ntlhdl.GetTemplateInfoByID(this._conn, id,false);
            if (item != null)
            {
                this._tagtext = item.vcContent.Replace("tcg:item", "tcg:itemTemp" + id.ToString());
                this._tagtext = this._tagtext.Replace("$item", "$itemTemp" + id.ToString());
            }
            else
            {
                pagerinfo.Read = false;
            }
            item = null;
            ntlhdl = null;
        }

        /// <summary>
        /// ������Ѷ�б�
        /// </summary>
        private void TagForNewsList(ref TCGTagPagerInfo pagerinfo)
        {
            bool pager = false;
            try { pager = bool.Parse(this.GetAttribute("pager"));}
            catch { pager = false; };
            if (pager)
            {
                this.TagForNewsListWithPager(ref pagerinfo);
            }
            else
            {
                this.TagForNewsListWithOutPager(ref pagerinfo);
            }
        }

        private void TagForNewsListWithOutPager(ref TCGTagPagerInfo pagerinfo)
        {

            this._conn.Dblink = DBLinkNums.News;
            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "T_News_NewsInfo";

            string fields = this.GetAttribute("fields");
            if (string.IsNullOrEmpty(fields)) return;
            string fieldC = fields.Replace("%", ",");

            fieldC += ",vcTitleColor,cStrong ";
            string orders = this.GetAttribute("orders");
            int columns = Bases.ToInt( this.GetAttribute("columns"));
            if (columns == 0) columns = 1;
            string condition = this.GetAttribute("condition");

            string SQL = "SELECT TOP " + columns + " " + fieldC + " FROM T_News_NewsInfo (NOLOCK) WHERE "
                + " iID >0 AND " + condition;
            if (!string.IsNullOrEmpty(orders))
            {
                SQL += " ORDER BY " + orders;
            }

            DataSet ds = this._conn.GetDataSet(SQL);

            if (ds == null) return;
            if (ds.Tables.Count != 0)
            {
                string tempOld = this._tagtext;
                string tempNew = string.Empty;
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    string temp = tempOld;
                    DataRow Row = ds.Tables[0].Rows[n];
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n + 1).ToString());
                    this.NewslistTagFieldsReplace(ref temp, fields, Row);
                    tempNew += temp;
                }
                this._tagtext = tempNew;
            }

            ds.Clear();
            ds.Dispose();

        }

        private void NewslistTagFieldsReplace(ref string temp, string fields, DataRow Row)
        {
            string fieldv = string.Empty;
            if (fields.IndexOf(",") == -1)
            {
                fields = fields.Replace("%", ",");
                if (fields.IndexOf(" as") == -1)
                {
                    fieldv = Row[fields].ToString();
                    if (fields == "vcTitle")
                    {
                        if (!string.IsNullOrEmpty(Row["vcTitleColor"].ToString())) fieldv = "<font color='" + Row["vcTitleColor"].ToString() + "'>" + fieldv + "</font>";
                        if (Row["cStrong"].ToString() == "Y") fieldv = "<strong>" + fieldv + "</strong>";
                    }
                    temp = temp.Replace("$" + this._tagtype + "_" + fields + "$", "<TCG>" + fieldv + "</TCG>");
                }
                else
                {

                    string field1 = fields.Split(new string[] { " as" }, StringSplitOptions.None)[1].Trim();
                    fieldv = Row[field1].ToString();
                    if (field1 == "vcTitle")
                    {
                        if (!string.IsNullOrEmpty(Row["vcTitleColor"].ToString())) fieldv = "<font color='" + Row["vcTitleColor"].ToString() + "'>" + fieldv + "</font>";
                        if (Row["cStrong"].ToString() == "Y") fieldv = "<strong>" + fieldv + "</strong>";
                    }

                    temp = temp.Replace("$" + this._tagtype + "_" + field1 + "$", "<TCG>" + fieldv + "</TCG>");
                }
            }
            else
            {
                string[] fi = fields.Split(',');
                for (int i = 0; i < fi.Length; i++)
                {
                    fields = fields.Replace("%", ",");
                    if (fi[i].IndexOf(" as") == -1)
                    {
                        fieldv = Row[fi[i]].ToString();
                        if (fi[i] == "vcTitle")
                        {
                            if (!string.IsNullOrEmpty(Row["vcTitleColor"].ToString())) fieldv = "<font color='" + Row["vcTitleColor"].ToString() + "'>" + fieldv + "</font>";
                            if (Row["cStrong"].ToString() == "Y") fieldv = "<strong>" + fieldv + "</strong>";
                        }
                        temp = temp.Replace("$" + this._tagtype + "_" + fi[i] + "$", "<TCG>" + fieldv + "</TCG>");
                    }
                    else
                    {
                        string field2 = fi[i].Split(new string[] { " as" }, StringSplitOptions.None)[1].Trim();
                        fieldv = Row[field2].ToString();
                        if (field2 == "vcTitle")
                        {
                            if (!string.IsNullOrEmpty(Row["vcTitleColor"].ToString())) fieldv = "<font color='" + Row["vcTitleColor"].ToString() + "'>" + fieldv + "</font>";
                            if (Row["cStrong"].ToString() == "Y") fieldv = "<strong>" + fieldv + "</strong>";
                        }
                        temp = temp.Replace("$" + this._tagtype + "_" + field2 + "$", "<TCG>" + fieldv + "</TCG>");
                    }
                }
            }
            temp = this._tagstringhdl.StringColoumFun(this._conn, temp, false);
        }

        /// <summary>
        /// ������Ѷ�б�����ҳ
        /// </summary>
        private void TagForNewsListWithPager(ref TCGTagPagerInfo pagerinfo)
        {
            pagerinfo.NeedPager = true;
            this._pager = true;
            this._conn.Dblink = DBLinkNums.News;
            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "T_News_NewsInfo";

            ArrayList arrshowfied = new ArrayList();

            //��ȡ�����ֶ�
            string fields = this.GetAttribute("fields");
            string fieldC = "";
            if (string.IsNullOrEmpty(fields))
            {
                pagerinfo.Read = false;
                return;
            }
            if (fields.IndexOf(",") != -1)
            {
                string[] afields = fields.Split(',');
                for (int i = 0; i < afields.Length; i++)
                {
                    if (!string.IsNullOrEmpty(afields[i]))
                    {
                        fieldC = afields[i].Replace("%", ",");
                        arrshowfied.Add(fieldC);
                    }
                }
            }
            else
            {
                fieldC = fields.Replace("%", ",");
                arrshowfied.Add(fieldC);
            }
            arrshowfied.Add("vcTitleColor");
            arrshowfied.Add("cStrong");
            sItem.arrShowField = arrshowfied;

            //��ȡ����
            ArrayList arrsortfield = new ArrayList();
            string orders = this.GetAttribute("orders");
            if (string.IsNullOrEmpty(orders)) 
            {
                pagerinfo.Read = false;
                return;
            }
            if (orders.IndexOf(",") != -1)
            {
                string[] aorders = orders.Split(',');
                for (int i = 0; i < aorders.Length; i++)
                {
                    if (!string.IsNullOrEmpty(aorders[i])) arrsortfield.Add(aorders[i]);
                }
            }
            else
            {
                arrsortfield.Add(orders);
            }
            sItem.arrSortField = arrsortfield;

            sItem.page = pagerinfo.Page;//���õ�ǰҳ��
            sItem.pageSize = Bases.ToInt(this.GetAttribute("pagesize"));
            string condition = this.GetAttribute("condition");
            if (string.IsNullOrEmpty(condition)) 
            {
                pagerinfo.Read = false;
                return;
            }
            sItem.strCondition = condition;

            int curPage = 0;
            int pageCount = 0;
            int count = 0;
            DataSet ds = new DataSet();
            int rtn = DBHandlers.GetPage(sItem,this._conn, ref curPage, ref pageCount, ref count, ref ds);

            pagerinfo.PageCount = pageCount;
            pagerinfo.curPage = curPage;
            pagerinfo.TopicCount = count;


            if (ds.Tables.Count != 0)
            {
                string tempOld = this._tagtext;
                string tempNew = string.Empty;
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    string temp = tempOld;
                    DataRow Row = ds.Tables[0].Rows[n];
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n+1).ToString());
                    this.NewslistTagFieldsReplace(ref temp, fields, Row);
                    tempNew += temp;
                }
                this._tagtext = tempNew;
            }

            ds.Clear();
            ds.Dispose();

        }


        private Connection _conn = null;
        private Config _config = null;
        private string _attpattern = string.Empty;
        private bool _pager = false;
        private string _attribute = string.Empty;
        private string _tagtype = string.Empty;
        private string _tagtext = string.Empty;
        private string _tag = string.Empty;
        private string _taghtml = string.Empty;         //��ǩ��ȫ��HTML
        private TCGTagStringFunHandlers _tagstringhdl = null;
    }
}