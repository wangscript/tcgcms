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
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCG.Entity;

namespace TCG.Handlers
{
    public interface ICategoriesHandlers
    {
        /// <summary>
        /// 从数据库中加载分类信息
        /// </summary>
        /// <returns></returns>
        DataTable GetAllCategoriesWithOutCaching();


        DataTable GetCategoriePropertiesByCIdWithOutCaching(string cid);

        int CreateCategories(Categories cif);


        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        int UpdateCategories(Categories classinf);

        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        int DelCategories(string classid);

        /// <summary>
        /// 分类属性管理
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="cp"></param>
        /// <returns></returns>
        int CategoriePropertiesManage(CategorieProperties cp);

        int GetMaxCategoriesProperties();

        /// <summary>
        /// 删除分类属性
        /// </summary>
        /// <param name="admininf"></param>
        /// <param name="cpid"></param>
        /// <returns></returns>
        int CategoriePropertiesDEL(int cpid);
    }
}
