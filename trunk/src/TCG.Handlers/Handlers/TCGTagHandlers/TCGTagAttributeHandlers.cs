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
                case "Template":
                    this.TagForNewsTemplate(ref pagerinfo);
                    break;
                case "Resource":
                    this.TagForNewsTopic(ref pagerinfo);
                    break;
                case "Categories":
                    this.TagForNewsClassInfo(ref pagerinfo);
                    break;
                case "Resourcelist":
                    this.TagForNewsList(ref pagerinfo);
                    break;
                case "CategoriesList":
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

            string tempNew = string.Empty;
            string parentid = this.GetAttribute("parent");
            string skinid = this.GetAttribute("skin");

            Dictionary<string, EntityBase> categories =  this.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentId(parentid, skinid);

            if (categories == null)return;
    
            if (categories.Count != 0)
            {
                string tempOld = this._tagtext;
                
                int n = 0;
                foreach (KeyValuePair<string, EntityBase> entity in categories)
                {
                    Categories tempres = (Categories)entity.Value;
                    string temp = tempOld;
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n + 1).ToString());
                    this.CategorieslistTagFieldsRelace(ref temp, tempres);
                    tempNew += temp;
                    n++;
                }

                this._tagtext = tempNew;
            }

            this._tagtext = tempNew;

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



            Resources item = this.handlerService.resourcsService.resourcesHandlers.GetResourcesById(resourceid);
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
                    string tContent = this.handlerService.fileService.fileInfoHandlers.ImgPatchInit(item.vcContent,"", "admin",
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
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_dAddDate$", item.dAddDate.ToString("yyyy年MM月dd日"));
               
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcKeyWord$", item.vcKeyWord);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcAuthor$", item.vcAuthor);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcShortContent$", objectHandlers.GetTextWithoutHtml(item.vcShortContent));
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_vcClassName$", item.Categorie.vcClassName);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iClassId$", item.Categorie.Id);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iClassParentId$", item.Categorie.Parent);
                this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iClassParentId2$", this.handlerService.skinService.categoriesHandlers.GetCategoriesParent2(item.Categorie.Id).Id);
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
            this._tagtext = this._tagtext.Replace("$" + this._tagtype + "_iParent2$",this.handlerService.skinService.categoriesHandlers.GetCategoriesParent2(item.Id).Id);
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
                this._tagtext = item.Content.Replace("tcg:" + this.TagType, "tcg:" + this.TagType + "Temp" + id.ToString());
                this._tagtext = this._tagtext.Replace("$" + this.TagType, "$" + this.TagType + "Temp" + id.ToString());
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


            int nums = objectHandlers.ToInt(this.GetAttribute("num"));
            string categories = this.GetAttribute("categories");
            string Speciality = this.GetAttribute("speciality");
            string orders = this.GetAttribute("orders");
            bool check = this.GetAttribute("check") != "Y" ? false : true;
            bool del = this.GetAttribute("del") != "Y" ? false : true;
            bool create = this.GetAttribute("create") != "Y" ? false : true;

            Dictionary<string, EntityBase> res = this.handlerService.resourcsService.resourcesHandlers.GetResourcesList(
                nums, categories, Speciality, orders, check, del, create);


            if (res == null) return;
            if (res.Count != 0)
            {
                string tempOld = this._tagtext;
                string tempNew = string.Empty;

                int n = 0;
                foreach (KeyValuePair<string, EntityBase> entity in res)
                {
                    Resources tempres = (Resources)entity.Value;
                    string temp = tempOld;
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n + 1).ToString());
                    this.NewslistTagFieldsReplace(ref temp, tempres);
                    tempNew += temp;
                    n++;
                }

                this._tagtext = tempNew;
            }
        }

        private void NewslistTagFieldsReplace(ref string temp, Resources res)
        {
            string Title = res.vcTitle;
            if (!string.IsNullOrEmpty(res.vcTitleColor)) Title = "<font color='" + res.vcTitleColor + "'>" + Title + "</font>";
            if (res.cStrong == "Y") Title = "<strong>" + Title + "</strong>";

            temp = temp.Replace("$" + this._tagtype + "_vcTitle$", "<TCG>" + Title + "</TCG>");

            string url = res.vcUrl.Trim().Length == 0 ? res.vcFilePath : res.vcUrl;
            temp = temp.Replace("$" + this._tagtype + "_vcFilePath$", "<TCG>" + url + "</TCG>");

            temp = temp.Replace("$" + this._tagtype + "_vcSmallImg$", "<TCG>" + res.vcSmallImg + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcBigImg$", "<TCG>" + res.vcBigImg + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcShortContent$", "<TCG>" + res.vcShortContent + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_dAddDate$", "<TCG>" + res.dAddDate.ToString("yyyy-MM-dd") + "</TCG>");

            temp = base.tcgTagStringFunHandlers.StringColoumFun(temp, false);
        }

        private void CategorieslistTagFieldsRelace(ref string temp, Categories categorie)
        {

            temp = temp.Replace("$" + this._tagtype + "_Id$", "<TCG>" + categorie.Id + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcClassName$", "<TCG>" + categorie.vcClassName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcName$", "<TCG>" + categorie.vcName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcUrl$", "<TCG>" 
                + (categorie.vcUrl.IndexOf('.') > -1 ? categorie.vcUrl : categorie.vcUrl + base.configService.baseConfig["FileExtension"])+ "</TCG>");

            temp = base.tcgTagStringFunHandlers.StringColoumFun(temp, false);
        }


        /// <summary>
        /// 处理资讯列表，带分页
        /// </summary>
        private void TagForNewsListWithPager(ref TCGTagPagerInfo pagerinfo)
        {
            pagerinfo.NeedPager = true;
            this._pager = true;


            int nums = objectHandlers.ToInt(this.GetAttribute("num"));
            string categories = this.GetAttribute("categories");
            string Speciality = this.GetAttribute("speciality");
            string orders = this.GetAttribute("orders");
            bool check = this.GetAttribute("check") != "Y" ? false : true;
            bool del = this.GetAttribute("del") != "Y" ? false : true;
            bool create = this.GetAttribute("create") != "Y" ? false : true;


            int curPage = 0;
            int pageCount = 0;
            int count = 0;

            string strCondition = base.handlerService.resourcsService.resourcesHandlers.GetTagResourceCondition(categories, Speciality, check, del, create);
            Dictionary<string, EntityBase> res = null;
            try
            {
                res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                       pagerinfo.Page, nums, "iId DESC", strCondition);
            }
            catch (Exception ex)
            {
                pagerinfo.Read = false;

            }

            pagerinfo.PageCount = pageCount;
            pagerinfo.curPage = curPage;
            pagerinfo.TopicCount = count;

            if (res != null && res.Count != 0)
            {
                string tempOld = this._tagtext;
                string tempNew = string.Empty;
                int n = 0;
                foreach (KeyValuePair<string, EntityBase> entity in res)
                {
                    Resources tempres = (Resources)entity.Value;
                    string temp = tempOld;
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n + 1).ToString());
                    this.NewslistTagFieldsReplace(ref temp, tempres);
                    tempNew += temp;
                    n++;
                }

                this._tagtext = tempNew;
            }

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