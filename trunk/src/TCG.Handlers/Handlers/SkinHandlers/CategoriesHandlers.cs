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
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Data;
using System.Web.Caching;
using TCG.Utils;
using TCG.Entity;


namespace TCG.Handlers
{

    /// <summary>
    /// 分类操作方法
    /// </summary>
    public class CategoriesHandlers :ObjectHandlersBase
    {
        /// <summary>
        /// 根据皮肤编号和分类父亲ID获得分类信息
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetCategoriesEntityByParentId(string parentid,string skinid)
        {
            Dictionary<string, EntityBase> allcategories = this.GetAllCategoriesEntity();
            if (allcategories == null) return null;
            if (allcategories.Count == 0) return null;
            Dictionary<string, EntityBase> childcategories = new Dictionary<string,EntityBase>();
            foreach ( KeyValuePair<string,EntityBase> entity in allcategories)
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
                allcategories = base.GetEntitysObjectFromTable(dt, typeof(Categories));
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
            base.SetDataBaseConnection();
            string Sql = "SELECT Id,vcClassName,vcName,SkinId,Parent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,DataBaseService FROM Categories WITH (NOLOCK)";
            return conn.GetDataTable(Sql);
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
            if (string.IsNullOrEmpty(classid)) return "";
            Categories categorie = this.GetCategoriesById(classid);
            if (classid == null) return "";
            string url = (categorie.vcUrl.IndexOf(".") > -1) ? categorie.vcUrl : categorie.vcUrl + base.configService.baseConfig["FileExtension"];
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
            return (Categories)allcategories[iClassID];
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
        public int CreateCategories(Categories cif)
        {
            base.SetDataBaseConnection();
            cif.Id = Guid.NewGuid().ToString();
            SqlParameter sp2 = new SqlParameter("@vcClassName", SqlDbType.VarChar, 200); sp2.Value = cif.vcClassName;
            SqlParameter sp3 = new SqlParameter("@vcName", SqlDbType.VarChar, 50); sp3.Value = cif.vcName;
            SqlParameter sp4 = new SqlParameter("@Parent", SqlDbType.VarChar, 36); sp4.Value = cif.Parent;
            SqlParameter sp5 = new SqlParameter("@iTemplate", SqlDbType.VarChar, 36); sp5.Value = cif.ResourceTemplate.Id;
            SqlParameter sp6 = new SqlParameter("@iListTemplate", SqlDbType.VarChar, 36); sp6.Value = cif.ResourceListTemplate.Id;
            SqlParameter sp7 = new SqlParameter("@vcDirectory", SqlDbType.VarChar, 200); sp7.Value = cif.vcDirectory;
            SqlParameter sp8 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp8.Value = cif.vcUrl;
            SqlParameter sp9 = new SqlParameter("@iOrder", SqlDbType.Int, 4); sp9.Value = cif.iOrder;
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int); sp10.Direction = ParameterDirection.Output;
            SqlParameter sp11 = new SqlParameter("@cVisible", SqlDbType.Char, 1); sp11.Value = cif.cVisible;
            SqlParameter sp12 = new SqlParameter("@iClassId", SqlDbType.VarChar, 36); sp12.Value = cif.Id;
            SqlParameter sp13 = new SqlParameter("@SkinId", SqlDbType.VarChar, 36); sp13.Value = cif.SkinId;
            string[] reValues = conn.Execute("SP_Skin_categories_Manage", new SqlParameter[] { sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11,sp12,sp13}, new int[] { 8 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }


        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        public int UpdateCategories(Categories classinf)
        {
            base.SetDataBaseConnection();
            SqlParameter sp1 = new SqlParameter("@vcClassName", SqlDbType.VarChar, 200); sp1.Value = classinf.vcClassName;
            SqlParameter sp2 = new SqlParameter("@DataBaseService", SqlDbType.VarChar, 50); sp2.Value = classinf.DataBaseService;
            SqlParameter sp3 = new SqlParameter("@vcName", SqlDbType.VarChar, 50); sp3.Value = classinf.vcName;
            SqlParameter sp4 = new SqlParameter("@Parent", SqlDbType.VarChar, 36); sp4.Value = classinf.Parent;
            SqlParameter sp5 = new SqlParameter("@iTemplate", SqlDbType.VarChar, 36); sp5.Value = classinf.ResourceTemplate.Id;
            SqlParameter sp6 = new SqlParameter("@iListTemplate", SqlDbType.VarChar, 36); sp6.Value = classinf.ResourceListTemplate.Id;
            SqlParameter sp7 = new SqlParameter("@vcDirectory", SqlDbType.VarChar, 200); sp7.Value = classinf.vcDirectory;
            SqlParameter sp8 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp8.Value = classinf.vcUrl;
            SqlParameter sp9 = new SqlParameter("@iOrder", SqlDbType.Int, 4); sp9.Value = classinf.iOrder;
            SqlParameter sp10 = new SqlParameter("@action", SqlDbType.Char, 2); sp10.Value = "02";
            SqlParameter sp11 = new SqlParameter("@iClassId", SqlDbType.VarChar, 36); sp11.Value = classinf.Id;
            SqlParameter sp12 = new SqlParameter("@reValue", SqlDbType.Int); sp12.Direction = ParameterDirection.Output;
            SqlParameter sp13 = new SqlParameter("@cVisible", SqlDbType.Char, 1); sp13.Value = classinf.cVisible;
            string[] reValues = conn.Execute("SP_Skin_categories_Manage", new SqlParameter[] {sp1,sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11,sp12,sp13}, new int[] { 11 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int DelCategories(string classid)
        {
            base.SetDataBaseConnection();
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@iClassId", SqlDbType.Int, 4); sp2.Value = classid;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_DelNewsClassById", new SqlParameter[] { sp1, sp2, sp3}, new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }
    }
}
