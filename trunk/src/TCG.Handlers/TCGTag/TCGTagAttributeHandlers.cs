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


using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    public class TCGTagAttributeHandlers : TCGTagBase
    {
        public TCGTagAttributeHandlers(HandlerService handlerservice)
        {
            base.handlerService = handlerservice;
            this._attpattern = @"=""([\S\s]*?)""";
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
                case "Template": //<tcg:id1 type="Template" id=''>dfddfd</tcg:id1>
                    this.TagForNewsTemplate(ref pagerinfo);
                    break;
                case "Resource": //<tcg:id1 type="Resource" id='topic1'> </tcg:id1>
                    this.TagForNewsTopic(ref pagerinfo);
                    break;
                case "Categories": //<tcg:id1 type="Categories" id='Categoriesid'>dfddfd</tcg:id1>
                    this.TagForNewsClassInfo(ref pagerinfo);
                    break;
                case "Resourcelist":
                    this.TagForNewsList(ref pagerinfo);
                    break;
                case "CategoriesList":
                    this.TagForNewsClassList(ref pagerinfo);
                    break;
                case "scriptcss": // <tcg:id1 type="scriptcss" text='/sdfsdf.js|ggg.js'>dfddfd</tcg:id1>
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
        /// 跟标签名获得标志属性
        /// </summary>
        /// <param name="feild"></param>
        /// <returns></returns>
        public string GetAttribute(string attributes,string feild)
        {
            string value = "";
            Match item = Regex.Match(attributes, feild + this._attpattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (item.Success)
            {
                value = item.Result("$1");
            }
            item = null;

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
            string Speciality = this.GetAttribute("speciality");

            Dictionary<string, EntityBase> categories = this.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentIdForIndex(parentid, skinid, Speciality);

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
                    this.CategorieslistTagFieldsRelace(ref temp,ref pagerinfo ,tempres);
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
            int resourceid = objectHandlers.ToInt(this.GetAttribute("id"));
            if (resourceid==0)
            {
                pagerinfo.Read = false;
                return;
            }

            Resources item = this.handlerService.resourcsService.resourcesHandlers.GetResourcesById(resourceid);
            if (item != null)
            {
                string temp = this._tagtext;
                this.NewslistTagFieldsReplace(ref temp, ref pagerinfo, item);
                this._tagtext = temp;

            }
            else
            {
                throw new Exception("编号为:[" + resourceid + "]的文章找不到！");
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

            this.CategorieslistTagFieldsRelace(ref this._tagtext, ref pagerinfo,item);

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
            bool pager = objectHandlers.ToBoolen(this.GetAttribute("pager"),false);
            
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
            bool havechilecategorie = this.GetAttribute("havechilecategorie") == "Y" ? true : false;

            Dictionary<string, EntityBase> res = this.handlerService.resourcsService.resourcesHandlers.GetResourcesList(
                nums, categories, Speciality, orders, check, del, create, havechilecategorie);

            string tempNew = string.Empty;
            if (res == null)
            {
                this._tagtext = tempNew;
                return;
            }

            if (res.Count != 0)
            {
                string tempOld = this._tagtext;
                

                int n = 0;
                foreach (KeyValuePair<string, EntityBase> entity in res)
                {
                    Resources tempres = (Resources)entity.Value;
                    string temp = tempOld;
                    temp = temp.Replace("$" + this._tagtype + "_Index$", (n + 1).ToString());
                    this.NewslistTagFieldsReplace(ref temp,ref pagerinfo, tempres);
                    tempNew += temp;
                    n++;
                }

            }
            this._tagtext = tempNew;
        }

        private void NewslistTagFieldsReplace(ref string temp, ref TCGTagPagerInfo pagerinfo, Resources item)
        {
            bool isintitle = objectHandlers.ToBoolen(this.GetAttribute("intitle"), false);
            if (isintitle) pagerinfo.PageTitle += (string.IsNullOrEmpty(pagerinfo.PageTitle) ? "" : " - ") + item.vcTitle;

            int maxtitlenum = objectHandlers.ToInt(this.GetAttribute("maxtitlenum"));
            string title = item.vcTitle;
            if (maxtitlenum != 0)
            {
                 string text = (title.Length > maxtitlenum) ? "..." : "";
                 title = objectHandlers.Left(title, maxtitlenum) + text;
            }

            if (!string.IsNullOrEmpty(item.vcTitleColor)) title = "<font color='" + item.vcTitleColor + "'>"
                + title + "</font>";
            if (item.cStrong == "Y") title = "<strong>" + "<TCG>" + title + "</strong>";
            temp = temp.Replace("$" + this._tagtype + "_vcTitle$", "<TCG>" + title + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcTitle1$", "<TCG>" + item.vcTitle + "</TCG>");

            //替换正文内容，检查图片
            //try
            //{
            //    string tContent = this.handlerService.fileService.fileHandlers.ImgPatchInit(item, "", adminInfo,
            //        objectHandlers.ToInt(ConfigServiceEx.baseConfig["NewsFileClass"]));
            //    if (tContent != item.vcContent)
            //    {
            //        item.vcContent = tContent;
            //        int outid = 0;
            //        string filepatch = string.Empty;
            //        this.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message.ToString());
            //}

            temp = temp.Replace("$" + this._tagtype + "_vcContent$", "<TCG>" + item.vcContent + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iId$", "<TCG>" + item.Id.ToString() + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcKeyWord$", "<TCG>" + item.vcKeyWord + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_dUpdateDate$", "<TCG>" + item.dUpDateDate.ToString("yyyy年MM月dd日") + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_dAddDate$", "<TCG>" + item.dAddDate.ToString("yyyy年MM月dd日") + "</TCG>");

            temp = temp.Replace("$" + this._tagtype + "_vcKeyWord$", "<TCG>" + item.vcKeyWord + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcClassPic$", "<TCG>" + "/skin/"
                + pagerinfo.SkinInfo.Filename + item.Categorie.vcPic + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcAuthor$", "<TCG>" + item.vcAuthor + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcShortContent$", "<TCG>" + objectHandlers.HtmlEncode(item.vcShortContent) + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcClassName$", "<TCG>" + item.Categorie.vcClassName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iClassId$", "<TCG>" + item.Categorie.Id + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iClassParentId$", "<TCG>" + item.Categorie.Parent + "</TCG>");
            Categories cinfo = this.handlerService.skinService.categoriesHandlers.GetCategoriesParent2(item.Categorie.Id);
            temp = temp.Replace("$" + this._tagtype + "_iClassParentId2$", "<TCG>" + cinfo.Id + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iClassParentvcClassName2$", "<TCG>" + cinfo.vcClassName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iClassParentvcName2$", "<TCG>" + cinfo.vcName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_TopicClassTitleList$",
                "<TCG>" + this.handlerService.skinService.categoriesHandlers.GetResourcesCategoriesIndex(item.Categorie.Id, " > ") + "</TCG>");

            string url = item.vcUrl.Trim().Length == 0 ? item.vcFilePath : item.vcUrl;
            temp = temp.Replace("$" + this._tagtype + "_vcFilePath$", "<TCG>" + url + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcUrl$", "<TCG>" + url + "</TCG>");

            temp = temp.Replace("$" + this._tagtype + "_vcSmallImg$", "<TCG>" + item.vcSmallImg + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcSpeciality$", "<TCG>" + item.vcSpeciality + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcBigImg$", "<TCG>" + item.vcBigImg + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_SheifUrl$", "<TCG>" + item.SheifUrl + "</TCG>");

            temp = temp.Replace("$" + this._tagtype + "_iCount$", "<TCG><script type='text/javascript' src='"
                + ConfigServiceEx.baseConfig["WebSite"] + "/interface/aspx/resources.aspx?w=getresourcecount&rid=" + item.Id + "'></script></TCG>");

            string sssid = this._tagtype + "_nicon_" + item.Id;
            if (item.dAddDate.AddDays(5) >= DateTime.Now)
            {
                temp = temp.Replace("$" + this._tagtype + "_nIcon$", "<TCG><img src='/images/newicon.jpg' style='vertical-align: middle;'/></TCG>");
            }
            else
            {
                temp = temp.Replace("$" + this._tagtype + "_nIcon$", "");
            }

            bool isshowrp = objectHandlers.ToBoolen(this.GetAttribute("inpropertie"), false);
            if (isshowrp)
            {

                
                Dictionary<string, EntityBase> ress = base.handlerService.resourcsService.resourcesHandlers.GetResourcePropertiesByRIdEntity(item.Id);

                if (ress != null && ress.Count != 0)
                {
                    Match mh = Regex.Match(temp, @"<ResourcePropertiesList([^<>]+)?>([\S\s]*?)</ResourcePropertiesList>", RegexOptions.Singleline | RegexOptions.Multiline);
                    string rplist = string.Empty;
                    if (mh.Success)
                    {
                        string temp1 = mh.Result("$2");
                        string attrs = mh.Result("$1");

                        string hide = this.GetAttribute(attrs, "hide");

                        int num = objectHandlers.ToInt(this.GetAttribute(attrs, "num"));
                        if (num == 0) num = ress.Count;

                        int i = 0;
                        foreach (KeyValuePair<string, EntityBase> entity in ress)
                        {
                            ResourceProperties resourceProperties = (ResourceProperties)entity.Value;
                            //在制定行数是内，并且不在排除字段中
                            if (i <= num - 1 && hide.IndexOf(resourceProperties.PropertieName) == -1)
                            {
                                int maxtlength = objectHandlers.ToInt(this.GetAttribute(attrs, "maxlength"));
                                string temp33 = maxtlength == 0 ? resourceProperties.PropertieValue : objectHandlers.Left(resourceProperties.PropertieValue, maxtlength);
                                temp33 = objectHandlers.HtmlEncode(temp33);
                                string temp2 = temp1;
                                temp2 = temp2.Replace("$PropertieName$", resourceProperties.PropertieName);
                                temp2 = temp2.Replace("$PropertieValue$", temp33);
                                rplist += temp2;
                            }
                            i++;
                        }
                        temp = Regex.Replace(temp, @"<ResourcePropertiesList([^<>]+)?>([\S\s]*?)</ResourcePropertiesList>", rplist, RegexOptions.Singleline | RegexOptions.Multiline);
                    }

                    MatchCollection mhs = Regex.Matches(temp, @"<RPItem>([\S\s]*?)</RPItem>", RegexOptions.Singleline | RegexOptions.Multiline);
                    if (mhs.Count > 0)
                    {
                        foreach (Match m in mhs)
                        {
                            string temp22 = m.Result("$1");
                            foreach (KeyValuePair<string, EntityBase> entity in ress)
                            {
                                ResourceProperties resourceProperties = (ResourceProperties)entity.Value;
                                if (temp22.IndexOf("$" + resourceProperties.PropertieName + "$") > -1)
                                {
                                    temp22 = temp22.Replace("$" + resourceProperties.PropertieName + "$", objectHandlers.HtmlEncode(resourceProperties.PropertieValue));
                                }
                            }

                            if (temp22 == m.Result("$1"))
                            {
                                temp = temp.Replace(@"<RPItem>" + m.Result("$1") + "</RPItem>", @"");
                            }
                            else
                            {
                                temp = temp.Replace( @"<RPItem>" + m.Result("$1") + "</RPItem>", temp22);
                            }
                        }
                    }
                }
               
            }

            temp = base.tcgTagStringFunHandlers.StringColoumFun(temp, false);
        }

        private void CategorieslistTagFieldsRelace(ref string temp, ref TCGTagPagerInfo pagerinfo,Categories categorie)
        {

            string url = "#";
            if (!string.IsNullOrEmpty(categorie.vcUrl))
            {
                url = (categorie.vcUrl.IndexOf(".") > -1) ? categorie.vcUrl : categorie.vcUrl + ConfigServiceEx.baseConfig["FileExtension"];
            }
            else
            {
                if (categorie.IsSinglePage == "Y")
                {
                    Resources res = base.handlerService.resourcsService.resourcesHandlers.GetNewsResourcesAtCategorie(categorie.Id);
                    if (res != null && categorie.IsSinglePage == "Y" && !string.IsNullOrEmpty(res.vcFilePath))
                    {
                        url = res.vcFilePath;
                    }
                }
                else
                {
                   Dictionary<string,EntityBase> chiledcategorys = base.handlerService.skinService.categoriesHandlers.GetCategoriesEntityByParentId(categorie.Id, categorie.SkinInfo.Id);
                   foreach (KeyValuePair<string, EntityBase> keyvalue in chiledcategorys)
                   {
                       Categories ctgr = keyvalue.Value as Categories;
                       if (ctgr != null)
                       {
                           if (!string.IsNullOrEmpty(ctgr.vcUrl))
                           {
                               url = (ctgr.vcUrl.IndexOf(".") > -1) ? ctgr.vcUrl : ctgr.vcUrl + ConfigServiceEx.baseConfig["FileExtension"];
                           }
                           else
                           {
                               if (ctgr.IsSinglePage == "Y")
                               {
                                   Resources res1 = base.handlerService.resourcsService.resourcesHandlers.GetNewsResourcesAtCategorie(ctgr.Id);
                                   if (res1 != null && ctgr.IsSinglePage == "Y" && !string.IsNullOrEmpty(res1.vcFilePath))
                                   {
                                       url = res1.vcFilePath;
                                   }
                               }
                           }
                       }
                       if (!string.IsNullOrEmpty(url)) break;
                   }
                }
            }

            temp = temp.Replace("$" + this._tagtype + "_vcUrl$", "<TCG>" + url + "</TCG>");

            temp = temp.Replace("$" + this._tagtype + "_iParent$", "<TCG>" + categorie.Parent.ToString() + "</TCG>");
            Categories cinfo = this.handlerService.skinService.categoriesHandlers.GetCategoriesParent2(categorie.Id);
            temp = temp.Replace("$" + this._tagtype + "_iParentId2$", "<TCG>" + cinfo.Id + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iParentvcClassName2$", "<TCG>" + cinfo.vcClassName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_iParentvcName2$", "<TCG>" + cinfo.vcName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_ClassTitleList$",
                    "<TCG>" + this.handlerService.skinService.categoriesHandlers.GetResourcesCategoriesIndex(categorie.Id, " > ") + "</TCG>");

            bool isintitle = objectHandlers.ToBoolen(this.GetAttribute("intitle"), false);
            if (isintitle) pagerinfo.PageTitle += (string.IsNullOrEmpty(pagerinfo.PageTitle) ? "" : " - ") + categorie.vcClassName;

            temp = temp.Replace("$" + this._tagtype + "_Id$", "<TCG>" + categorie.Id + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcClassName$", "<TCG>" + categorie.vcClassName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcName$", "<TCG>" + categorie.vcName + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_vcPic$", "<TCG>/skin/" 
                + pagerinfo.SkinInfo.Filename + categorie.vcPic + "</TCG>");
            temp = temp.Replace("$" + this._tagtype + "_allchildid$", "<TCG>" + base.handlerService.skinService.categoriesHandlers.GetCategoriesChild(categorie.Id) + "</TCG>");

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
            bool havechilecategorie = this.GetAttribute("havechilecategorie") != "Y" ? false : true;

            int curPage = pagerinfo.Page;
            int pageCount = 0;
            int count = 0;

            string strCondition = base.handlerService.resourcsService.resourcesHandlers.GetTagResourceCondition(categories, Speciality, check, del, create, havechilecategorie);
            Dictionary<string, EntityBase> res = null;

            if (ConfigServiceEx.baseConfig["IndexListType"] == "01")
            {
                orders = pagerinfo.Page == 0 ? "dAddDate DESC" : "dAddDate";
            }
            else
            {
                if (pagerinfo.Page == 0) pagerinfo.Page = 1;
            }

            try
            {
                res = base.handlerService.resourcsService.resourcesHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count,
                       pagerinfo.Page, nums, orders, strCondition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
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
                    this.NewslistTagFieldsReplace(ref temp,ref pagerinfo, tempres);
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