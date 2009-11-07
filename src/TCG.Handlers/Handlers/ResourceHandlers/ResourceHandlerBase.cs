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
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;
using TCG.KeyWordSplit;

namespace TCG.Handlers
{
    /// <summary>
    /// 资源操作的基本方法基类
    /// </summary>
    public class ResourceHandlerBase : ObjectHandlersBase
    {
        /// <summary>
        /// 根据分类,设置数据库连接字符串
        /// </summary>
        /// <param name="categories">分类实体</param>
        public void SetReourceHandlerDataBaseConnection(Categories categories)
        {
            if (base.configService == null) return;                             //如果配置信息为空返回
            if (base.configService.ResourceDataBaseConfig == null) return;      //如果数据库配置信息为空返回
            if (base.configService.ResourceDataBaseConfig.Count == 0) return;   //如果配置数据库为0,返回
            if (categories == null) return;

            //获得数据库配置信息
            DataBaseConnStr database = base.configService.ResourceDataBaseConfig[categories.DataBaseService];
            if (database == null) return;
            base.conn.SetConnStr = database.Value;
        }

        /// <summary>
        /// 根据数据库编号
        /// </summary>
        public void SetReourceHandlerDataBaseConnection(string categorieid)
        {
            Categories categories = base.handlerService.skinService.categoriesHandlers.GetCategoriesById(categorieid);
            if (categories == null) return;     //如果分类信息为空,则返回

            //获得数据库配置信息
            DataBaseConnStr database = base.configService.ResourceDataBaseConfig[categories.DataBaseService];
            if (database == null) return;
            base.conn.SetConnStr = database.Value;
        }
        
    }
}
