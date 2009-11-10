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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    public class TCGTagAttributeHandlers : TCGTagBase
    {
        public TCGTagAttributeHandlers(HandlerService handlerservice)
        {
            base.handlerService = handlerservice;
            this._attpattern = @"=""([0-9A-Za-z\s,='!_\-\.%\|()$<>\/]+)""";
        }

        /// <summary>
        /// 标签替换后HTML 的暂存标签 避免二次匹配
        /// </summary>
        public string Tag { get { return this._tag; } set { this._tag = value; } }
        /// <summary>
        /// 标签包含的HTML
        /// </summary>
        public string TagText { get { return this._tagtext; } set { this._tagtext = value; } }
        /// <summary>
        /// 标签ITEM 数字
        /// </summary>
        public string TagType { get { return this._tagtype; } set { this._tagtype = value; } }
        /// <summary>
        /// 标签属性字符串
        /// </summary>
        public string Attribute { get { return this._attribute; } set { this._attribute = value; } }
        /// <summary>
        /// 标签的全部HTML
        /// </summary>
        public string TagHtml { get { return this._taghtml; } set { this._taghtml = value; } }
        /// <summary>
        /// 是否为列表属性
        /// </summary>
        public bool Pager { get { return this._pager; } set { this._pager = value; } }

        public string Replace(string TempHtml)
        {
            return TempHtml.Replace(this._tag, this._tagtext);
        }

        public void ReplaceTagText(ref TCGTagPagerInfo pagerinfo)
        {
            if (this._tagtype == string.Empty || this._attribute == string.Empty || this._tag == string.Empty) return;
            string type = this.GetAttribute("type");
            if (type == "") return;
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
        /// 跟标签名获得标志属性
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
                    
                    break;
            }
            return value;
        }

        /// <summary>
        /// 获得标签
        /// </summary>
        /// <param name="pagerinfo"></param>
        private void TagForScriptCss(ref TCGTagPagerInfo pagerinfo)
        {
            pagerinfo.ScriptCss = this.GetAttribute("text");
        }

        private void TagForNewsClassList(ref TCGTagPagerInfo pagerinfo)
        {

            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "Categories";

            string fields = this.GetAttribute("fields");
            if (string.IsNullOrEmpty(fields)) return;
            string fieldC = fields.Replace("%", ",");

            string orders = this.GetAttribute("orders");
            int columns = objectHandlers.ToInt(this.GetAttribute("columns"));
            if (columns == 0) columns = 1;

            string condition = this.GetAttribute("condition");
            if (string.IsNullOrEmpty(condition)) return;
            string SQL = "SELECT TOP " + columns + " " + fieldC + " FROM Categories (NOLOCK) WHERE "
                + " ID !='' AND " + condition;

            if (!string.IsNullOrEmpty(orders))
            {
                SQL += " ORDER BY " + orders;
            }

            DataSet ds = base.conn.GetDataSet(SQL);

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
        /// 处理单篇资讯的标签
        /// </summary>
        private void TagForNewsTopic(ref TCGTagPagerInfo pagerinfo)
        {
            string resourceid = this.GetAttribute("id");
            if (string.IsNullOrEmpty(resourceid))
            {
                pagerinfo.Read = false;
                return;
            }

            string categoriesid = this.GetAttribute("cid");
            if (string.IsNullOrEmpty(categoriesid))
            {
                pagerinfo.Read = false;
                return;
            }



            Resources item = this.handlerService.resourcsService.resourcesHandlers.GetResourcesByIdAndCategorieId(categoriesid, resourceid);
            if (item != null)
            {
                pagerinfo.PageTitle = item.vcTitle;

                if (!string.IsNullOrEmpty(item.vcTitleColor)) item.vcTitle = "<font color='" + item.vcTitleColor + "'>"
                    + item.vcTitle + "</font>";
                if (item.cStrong == "Y") item.vcTitle = "<strong>" + item.vcTitle + "</strong>";
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcTitle$", item.vcTitle);

                //替换正文内容，检查图片
                try
                {
                    string tContent = this.handlerService.fileService.fileInfoHandlers.ImgPatchInit(item.vcContent, "admin",
                        objectHandlers.ToInt(base.configService.baseConfig["NewsFileClass"]));
                    if (tContent != item.vcContent)
                    {
                        item.vcContent = tContent;
                        int outid = 0;
                        string filepatch = string.Empty;
                        this.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
                    }
                }
                catch { }
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcContent$", item.vcContent);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iId$", item.Id.ToString());

                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcFilePath$", item.vcFilePath);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcKeyWord$", item.vcKeyWord);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_dUpdateDate$", item.dUpDateDate.ToString("yyyy年MM月dd日"));
               
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcKeyWord$", item.vcKeyWord);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcAuthor$", item.vcAuthor);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcShortContent$", objectHandlers.GetTextWithoutHtml(item.vcShortContent));
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcClassName$", item.Categorie.vcClassName);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iClassId$", item.Categorie.Id.ToString());
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_TopicClassTitleList$",
                    this.handlerService.skinService.categoriesHandlers.GetResourcesCategoriesIndex(item.Categorie.Id, " > "));

                
            }
            else
            {

                item = null;

                pagerinfo.Read = false;
                return;
            }

            item = null;

        }


        private void TagForNewsClassInfo(ref TCGTagPagerInfo pagerinfo)
        {
            string id = this.GetAttribute("id");
            if (string.IsNullOrEmpty(id))
            {
                pagerinfo.Read = false;
                return;
            }

            Categories item = this.handlerService.skinService.categoriesHandlers.GetCategoriesById(id);
            if (item == null)
            {
                pagerinfo.Read = false;
                return;
            }

            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iID$", item.Id.ToString());
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcName$", item.vcName);
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcClassName$", item.vcClassName);
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iParent$", item.Parent.ToString());
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_ClassTitleList$",
                    this.handlerService.skinService.categoriesHandlers.GetResourcesCategoriesIndex(item.Id, " > "));

            pagerinfo.PageTitle = item.vcClassName;

            item = null;

        }

        /// <summary>
        /// 处理内签模版的标签
        /// </summary>
        private void TagForNewsTemplate(ref TCGTagPagerInfo pagerinfo)
        {
            string id = this.GetAttribute("id");
            if (string.IsNullOrEmpty(id))
            {
                pagerinfo.Read = false;
                return;
            }

            Template item = this.handlerService.skinService.templateHandlers.GetTemplateByID(id);
            if (item != null)
            {
                this._tagtext = item.Content.Replace("tcg:item", "tcg:itemTemp" + id.ToString());
                this._tagtext = this._tagtext.Replace("$item", "$itemTemp" + id.ToString());
            }
            else
            {
                pagerinfo.Read = false;
            }
            item = null;

        }

        /// <summary>
        /// 处理资讯列表
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

            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "Resources";

            string fields = this.GetAttribute("fields");
            if (string.IsNullOrEmpty(fields)) return;
            string fieldC = fields.Replace("%", ",");

            fieldC += ",vcTitleColor,cStrong ";
            string orders = this.GetAttribute("orders");
            int columns = objectHandlers.ToInt( this.GetAttribute("columns"));
            if (columns == 0) columns = 1;
            string condition = this.GetAttribute("condition");

            string SQL = "SELECT TOP " + columns + " " + fieldC + " FROM Resources (NOLOCK) WHERE "
                + " iID != '' AND " + condition;
            if (!string.IsNullOrEmpty(orders))
            {
                SQL += " ORDER BY " + orders;
            }

            DataSet ds = base.conn.GetDataSet(SQL);

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
            temp = base.tcgTagStringFunHandlers.StringColoumFun(temp, false);
        }

        /// <summary>
        /// 处理资讯列表，带分页
        /// </summary>
        private void TagForNewsListWithPager(ref TCGTagPagerInfo pagerinfo)
        {
            pagerinfo.NeedPager = true;
            this._pager = true;
            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "Resources";

            ArrayList arrshowfied = new ArrayList();

            //读取返回字段
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

            //读取排序
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

            sItem.page = pagerinfo.Page;//设置当前页数
            sItem.pageSize = objectHandlers.ToInt(this.GetAttribute("pagesize"));
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
            int rtn = DBHandlers.GetPage(sItem,base.conn, ref curPage, ref pageCount, ref count, ref ds);

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

        private string _attpattern = string.Empty;
        private bool _pager = false;
        private string _attribute = string.Empty;
        private string _tagtype = string.Empty;
        private string _tagtext = string.Empty;
        private string _tag = string.Empty;
        private string _taghtml = string.Empty;         //标签的全部HTML

    }
}