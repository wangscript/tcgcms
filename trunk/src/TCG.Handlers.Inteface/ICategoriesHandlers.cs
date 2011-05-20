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
using System.Linq;
using System.Text;
using TCG.Entity;

namespace TCG.Handlers
{
    public interface ICategoriesHandlers
    {
        /// <summary>
        /// 根据皮肤编号和分类父亲ID获得分类信息
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="skinid"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetCategoriesEntityByParentId(string parentid, string skinid);


        /// <summary>
        /// 获得某分类的顶级分类
        /// </summary>
        /// <param name="categoriesid"></param>
        /// <returns></returns>
        Categories GetCategoriesParent2(string categoriesid);

        /// <summary>
        /// 获得所有皮肤的实体
        /// </summary>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetAllCategoriesEntity();


        /// <summary>
        /// 获得文章的导航！~
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="classid"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        string GetResourcesCategoriesIndex(string classid, string sh);

        /// <summary>
        /// 获得皮肤下的所有分类id，提供给查询
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        string GetAllCategoriesIndexBySkinId(string skinid);

        /// <summary>
        /// 获得制定皮肤下所有分类
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetAllCategoriesEntitySkinId(string skinid);

        string GetCategoriesChild(string categoriesid);

        /// <summary>
        /// 根据ID获得分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        Categories GetCategoriesById(string iClassID);

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
        int CreateCategories(Admin admininfo, Categories cif);


        int CreateCategoriesForXml(Admin admininfo, Categories cif);


        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        int UpdateCategories(Admin admin,Categories classinf);

        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        int DelCategories(string classid, Admin adminname);

        /// <summary>
        /// 从一个XML里面更新分类
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        int UpdateCategoriesFromXML(Admin admininfo,string skinid);

        /// <summary>
        /// 创建皮肤模板文件 
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int CreateCategoriesToXML(Admin admininfo,string skinid);

        /// <summary>
        /// 分类属性管理
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="cp"></param>
        /// <returns></returns>
        int CategoriePropertiesManage(Admin admin, CategorieProperties cp);

        /// <summary>
        /// 所有的资源分类属性信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetCategoriePropertiesByCIdEntity(string cid);

        int GetMaxCategoriesProperties();

        /// <summary>
        /// 删除分类属性
        /// </summary>
        /// <param name="admininf"></param>
        /// <param name="cpid"></param>
        /// <returns></returns>
        int CategoriePropertiesDEL(Admin admininf, int cpid);
        
    }
}
