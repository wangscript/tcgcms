using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Handlers;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers.Imp.AccEss
{
    public class CategoriesHandlers : ICategoriesHandlers
    {
        /// <summary>
        /// 根据皮肤编号和分类父亲ID获得分类信息
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetCategoriesEntityByParentId(string parentid, string skinid)
        {
            Dictionary<string, EntityBase> allcategories = this.GetAllCategoriesEntity();
            if (allcategories == null) return null;
            if (allcategories.Count == 0) return null;
            Dictionary<string, EntityBase> childcategories = new Dictionary<string, EntityBase>();
            foreach (KeyValuePair<string, EntityBase> entity in allcategories)
            {
                Categories tempcategories = (Categories)entity.Value;
                if (tempcategories.Parent == parentid && skinid == tempcategories.SkinId)
                {
                    childcategories.Add(tempcategories.Id, (EntityBase)tempcategories);
                }
            }
            return childcategories;
        }


        /// <summary>
        /// 获得某分类的顶级分类
        /// </summary>
        /// <param name="categoriesid"></param>
        /// <returns></returns>
        public Categories GetCategoriesParent2(string categoriesid)
        {
            Dictionary<string, EntityBase> categories = GetAllCategoriesEntity();
            if (categories == null || categories.Count == 0) return null;
            Categories cat = this.GetCategoriesById(categoriesid);
            if (cat == null) return null;

            if (cat.Parent == "0") return cat;

            return GetCategoriesParent2(cat.Parent);
        }

        /// <summary>
        /// 获得所有皮肤的实体
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllCategoriesEntity()
        {
            Dictionary<string, EntityBase> allcategories = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
            if (allcategories == null)
            {
                DataTable dt = GetAllCategories();
                if (dt == null) return null;
                allcategories = AccessFactory.GetEntitysObjectFromTable(dt, typeof(Categories));
                CachingService.Set(CachingService.CACHING_ALL_CATEGORIES_ENTITY, allcategories, null);
            }
            return allcategories;
        }

        /// <summary>
        /// 获得所有分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllCategories()
        {

            DataTable allcategories = (DataTable)CachingService.Get(CachingService.CACHING_ALL_CATEGORIES);
            if (allcategories == null)
            {
                allcategories = GetAllCategoriesWithOutCaching();
                CachingService.Set(CachingService.CACHING_ALL_CATEGORIES, allcategories, null);
            }
            return allcategories;
        }

        /// <summary>
        /// 从数据库中加载分类信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCategoriesWithOutCaching()
        {
            string Sql = "SELECT Id,vcClassName,vcName,SkinId,Parent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,DataBaseService FROM Categories order by iorder";
            return AccessFactory.conn.DataTable(Sql);
        }


        /// <summary>
        /// 获得文章的导航！~
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="classid"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public string GetResourcesCategoriesIndex(string classid, string sh)
        {
            if (string.IsNullOrEmpty(classid) || classid == "0") return "";
            Categories categorie = this.GetCategoriesById(classid);
            if (classid == null) return "";
            string url = (categorie.vcUrl.IndexOf(".") > -1) ? categorie.vcUrl : categorie.vcUrl + ConfigServiceEx.baseConfig["FileExtension"];
            string str = "<a href=\"" + url + "\" target=\"_blank\">" + categorie.vcClassName + "</a>";
            string t = this.GetResourcesCategoriesIndex(categorie.Parent, sh);
            if (!string.IsNullOrEmpty(t)) str = t + sh + str;
            return str;
        }

        /// <summary>
        /// 获得皮肤下的所有分类id，提供给查询
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public string GetAllCategoriesIndexBySkinId(string skinid)
        {
            Dictionary<string, EntityBase> allcategories = this.GetAllCategoriesEntity();
            if (allcategories == null) return "";

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, EntityBase> entity in allcategories)
            {
                Categories tempcategories = (Categories)entity.Value;
                if (tempcategories.SkinId == skinid && tempcategories.Parent == "0")
                {
                    if (sb.Length > 0) sb.Append(",");
                    sb.Append(this.GetCategoriesChild(tempcategories.Id));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获得制定皮肤下所有分类
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllCategoriesEntitySkinId(string skinid)
        {
            Dictionary<string, EntityBase> categories = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_DEFAULTSKIN_CATEGROIES_ENTITY + "_" + skinid);
            if (categories == null || categories.Count == 0)
            {
                Dictionary<string, EntityBase> allcategories = this.GetAllCategoriesEntity();
                if (allcategories == null) return null;

                categories = new Dictionary<string, EntityBase>();
                foreach (KeyValuePair<string, EntityBase> entity in allcategories)
                {
                    Categories tempcategories = (Categories)entity.Value;
                    if (tempcategories.SkinId == skinid)
                    {
                        categories.Add(tempcategories.Id, tempcategories);
                    }
                }

                CachingService.Set(CachingService.CACHING_DEFAULTSKIN_CATEGROIES_ENTITY + "_" + skinid, categories, null);
            }
            return categories.Count == 0 ? null : categories;

        }

        public string GetCategoriesChild(string categoriesid)
        {
            Dictionary<string, EntityBase> allcategories = this.GetAllCategoriesEntity();
            if (allcategories == null) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("'");
            sb.Append(categoriesid);
            sb.Append("'");

            foreach (KeyValuePair<string, EntityBase> entity in allcategories)
            {
                Categories tempcategories = (Categories)entity.Value;
                if (tempcategories.Parent == categoriesid)
                {
                    if (sb.Length > 0) sb.Append(",");
                    sb.Append("'");
                    sb.Append(tempcategories.Id);
                    sb.Append("',");
                    sb.Append(this.GetCategoriesChild(tempcategories.Id));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据ID获得分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        public Categories GetCategoriesById(string iClassID)
        {
            Dictionary<string, EntityBase> allcategories = this.GetAllCategoriesEntity();
            if (allcategories == null) return null;
            if (!allcategories.ContainsKey(iClassID)) return null;
            return allcategories.ContainsKey(iClassID) ? (Categories)allcategories[iClassID] : null;
        }

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classname"></param>
        /// <param name="lname"></param>
        /// <param name="parentid"></param>
        /// <param name="templateid"></param>
        /// <param name="ltemplateid"></param>
        /// <param name="dir"></param>
        /// <param name="url"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int CreateCategories(Admin admininfo,Categories cif)
        {

            cif.Id = Guid.NewGuid().ToString();
            return CreateCategoriesForXml(admininfo,cif);
        }

        public int CreateCategoriesForXml(Admin admininfo,Categories cif)
        {
            int rtn = AccessFactory.adminHandlers.CheckAdminPower(admininfo);
            if (rtn < 0) return rtn;

            //分类名或别名不能为空
            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                return -1000000020;
            }

            //分类所属模板不能为空
            if (string.IsNullOrEmpty(cif.SkinId))
            {
                return -1000000100;
            }

            if (cif.Parent != "0")
            {
                //模版编号不能为空
                if (string.IsNullOrEmpty(cif.ResourceListTemplate.Id))
                {
                    return -1000000029;
                }
            }

            if (cif.Parent != "0")
            {
                //列表模版编号不能为空
                if (string.IsNullOrEmpty(cif.ResourceListTemplate.Id))
                {
                    return -1000000029;
                }

                //模版编号不能为空
                if (string.IsNullOrEmpty(cif.ResourceTemplate.Id))
                {
                    return -1000000021;
                }

                //生成路径不能为空
                if (string.IsNullOrEmpty(cif.vcDirectory))
                {
                    return -1000000022;
                }

                //前台分类首页不能为空
                if (string.IsNullOrEmpty(cif.vcUrl))
                {
                    return -1000000023;
                }
            }

            AccessFactory.conn.Execute("INSERT INTO Categories(Id,vcClassName,vcName,SkinId,Parent,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,DataBaseService)"
                    + "VALUES('" + cif.Id + "','" + cif.vcClassName + "','" + cif.vcName + "','" + cif.SkinId + "','" + cif.Parent + "','" + cif.ResourceTemplate.Id + "','" 
                    + cif.ResourceListTemplate.Id + "','" + cif.vcDirectory + "','" + cif.vcUrl + "','" + cif.iOrder + "','" + cif.cVisible + "','" + cif.DataBaseService + "')");
            return 1;
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        public int UpdateCategories(Admin admininfo,Categories cif)
        {

            int rtn = AccessFactory.adminHandlers.CheckAdminPower(admininfo);
            if (rtn < 0) return rtn;

            //分类名或别名不能为空
            if (string.IsNullOrEmpty(cif.vcClassName) || string.IsNullOrEmpty(cif.vcName))
            {
                return -1000000020;
            }

            //父类ID不能为自己的ID
            if(cif.Parent==cif.Id)
            {
                return -1000000030;
            }

            if (cif.Parent != "0")
            {
                //模版编号不能为空
                if (string.IsNullOrEmpty(cif.ResourceListTemplate.Id))
                {
                    return -1000000029;
                }
            }

            if (cif.Parent != "0")
            {
                //列表模版编号不能为空
                if (string.IsNullOrEmpty(cif.ResourceListTemplate.Id))
                {
                    return -1000000029;
                }

                //模版编号不能为空
                if (string.IsNullOrEmpty(cif.ResourceTemplate.Id))
                {
                    return -1000000021;
                }

                //生成路径不能为空
                if (string.IsNullOrEmpty(cif.vcDirectory))
                {
                    return -1000000022;
                }

                //前台分类首页不能为空
                if (string.IsNullOrEmpty(cif.vcUrl))
                {
                    return -1000000023;
                }
            }

            AccessFactory.conn.Execute("UPDATE Categories SET vcClassName='" + cif.vcClassName + "',vcName='" + cif.vcName + "',Parent='" + cif.Parent + "',"
                    + "iTemplate='" + cif.ResourceTemplate.Id + "',iListTemplate='" + cif.ResourceListTemplate.Id + "',vcDirectory='" + cif.vcDirectory + "',vcUrl='"
                    + cif.vcUrl + "',iOrder=" + cif.iOrder + ",Visible = '" + cif.cVisible + "',DataBaseService='" + cif.DataBaseService + "' WHERE ID ='" + cif.Id + "'");
            return 1;
        }

        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int DelCategories(string classid, Admin admininfo)
        {

            int rtn = AccessFactory.adminHandlers.CheckAdminPower(admininfo);
            if (rtn < 0) return rtn;

            //资讯分类编号为空
            if (string.IsNullOrEmpty(classid))
            {
                return -1000000031;
            }

            int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM resources WHERE iClassID = '" + classid + "'"));
            //该分类下还存在资源，请移出后再删除
            if (ncount > 0)
            {
                return -1000000032;
            }

            ncount = 0;
            ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT  COUNT(1) FROM Categories WHERE Parent = '" + classid + "'"));

            if (ncount > 0)
            {
                return -1000000033;
            }
            AccessFactory.conn.Execute("DELETE FROM Categories WHERE ID='" + classid + "'");
            return 1;
        }

        /// <summary>
        /// 从一个XML里面更新分类
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public int UpdateCategoriesFromXML(string skinid)
        {
            //Skin skininfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(skinid);

            //if (skininfo == null)
            //{
            //    return -1000000701;
            //}

            //XmlDocument document = new XmlDocument();
            //document.Load(HttpContext.Current.Server.MapPath("~/skin/" + skininfo.Filename + "/categories.config"));
            //XmlNodeList nodelis1t = document.GetElementsByTagName("Categorie");
            //if (nodelis1t != null && nodelis1t.Count > 0)
            //{
            //    foreach (XmlElement element in nodelis1t)
            //    {
            //        Categories categories = new Categories();
            //        categories.Id = element.SelectSingleNode("Id").InnerText.ToString();
            //        categories.Parent = element.SelectSingleNode("Parent").InnerText.ToString();
            //        categories.ResourceListTemplate = new Template();
            //        categories.ResourceListTemplate.Id = element.SelectSingleNode("ResourceListTemplate").InnerText.ToString();
            //        categories.ResourceTemplate = new Template();
            //        categories.ResourceTemplate.Id = element.SelectSingleNode("ResourceTemplate").InnerText.ToString();
            //        categories.iOrder = objectHandlers.ToInt(element.SelectSingleNode("iOrder").InnerText.ToString());
            //        categories.dUpdateDate = objectHandlers.ToTime(element.SelectSingleNode("dUpdateDate").InnerText.ToString());
            //        categories.dUpdateDate = objectHandlers.ToTime(element.SelectSingleNode("dUpdateDate").InnerText.ToString());
            //        categories.vcClassName = element.SelectSingleNode("vcClassName").InnerText.ToString();
            //        categories.vcName = element.SelectSingleNode("vcName").InnerText.ToString();
            //        categories.vcDirectory = element.SelectSingleNode("vcDirectory").InnerText.ToString();
            //        categories.vcUrl = element.SelectSingleNode("vcUrl").InnerText.ToString();
            //        categories.cVisible = element.SelectSingleNode("cVisible").InnerText.ToString();
            //        categories.DataBaseService = element.SelectSingleNode("DataBaseService").InnerText.ToString();
            //        categories.SkinId = skininfo.Id;
            //        Categories t_categories = this.GetCategoriesById(categories.Id);
            //        if (t_categories == null)
            //        {
            //            this.CreateCategoriesForXml(categories);
            //        }
            //        else
            //        {
            //            this.UpdateCategories(categories);
            //        }
            //    }
            //}

            //CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES);
            //CachingService.Remove(CachingService.CACHING_ALL_CATEGORIES_ENTITY);
            return 1;
        }


        /// <summary>
        /// 创建皮肤模板文件 
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int CreateCategoriesToXML(string skinid)
        {
            //Skin skininfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(skinid);

            //if (skininfo == null)
            //{
            //    return -1000000701;
            //}

            //得到所有模板
            //StringBuilder sbcategories = new StringBuilder();
            //sbcategories.Append("<?xml version=\"1.0\"?>\r\n");
            //sbcategories.Append("<Categories>\r\n");
            //Dictionary<string, EntityBase> categories = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntity();
            //if (categories != null && categories.Count > 0)
            //{
            //    foreach (KeyValuePair<string, EntityBase> entity in categories)
            //    {
            //        Categories temp = (Categories)entity.Value;
            //        if (temp.ResourceTemplate != null)
            //        {
            //            if (temp.SkinId == skinid)
            //            {
            //                sbcategories.Append("<Categorie>\r\n");
            //                sbcategories.Append("\t<Id>" + temp.Id + "</Id>\r\n");
            //                sbcategories.Append("\t<Parent>" + temp.Parent + "</Parent>\r\n");
            //                sbcategories.Append("\t<ResourceTemplate>" + temp.ResourceTemplate.Id + "</ResourceTemplate>\r\n");
            //                sbcategories.Append("\t<ResourceListTemplate>" + temp.ResourceListTemplate.Id + "</ResourceListTemplate>\r\n");
            //                sbcategories.Append("\t<iOrder>" + temp.iOrder.ToString() + "</iOrder>\r\n");
            //                sbcategories.Append("\t<dUpdateDate>" + temp.dUpdateDate.ToString() + "</dUpdateDate>\r\n");
            //                sbcategories.Append("\t<dUpdateDate>" + temp.dUpdateDate + "</dUpdateDate>\r\n");
            //                sbcategories.Append("\t<vcClassName>" + temp.vcClassName + "</vcClassName>\r\n");
            //                sbcategories.Append("\t<vcName>" + temp.vcName + "</vcName>\r\n");
            //                sbcategories.Append("\t<vcDirectory>" + temp.vcDirectory + "</vcDirectory>\r\n");
            //                sbcategories.Append("\t<vcUrl>" + temp.vcUrl + "</vcUrl>\r\n");
            //                sbcategories.Append("\t<cVisible>" + temp.cVisible + "</cVisible>\r\n");
            //                sbcategories.Append("\t<DataBaseService>" + temp.DataBaseService + "</DataBaseService>\r\n");
            //                sbcategories.Append("\t<SkinId>" + temp.SkinId + "</SkinId>\r\n");
            //                sbcategories.Append("</Categorie>\r\n");
            //            }
            //        }
            //    }
            //}
            //sbcategories.Append("</Categories>");
            //objectHandlers.SaveFile(HttpContext.Current.Server.MapPath("~/skin/" + skininfo.Filename + "/categories.config"), sbcategories.ToString());
            return 1;
        }
      
    }
}
