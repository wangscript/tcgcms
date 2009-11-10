
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
    /// 皮肤操作方法
    /// </summary>
    public class SkinHandlers : SkinHandlerBase
    {
        /// <summary>
        /// 获得所有皮肤实体
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllSkinEntity()
        {
            Dictionary<string, EntityBase> allskin = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_SKIN_ENTITY);
            if (allskin == null)
            {
                DataTable dt = GetAllSkin();
                if (dt == null) return null;
                allskin = base.GetEntitysObjectFromTable(dt, typeof(Skin));
                CachingService.Set(CachingService.CACHING_ALL_SKIN_ENTITY, allskin, null);
            }
            return allskin;
        }

        /// <summary>
        /// 从缓存获得所有皮肤记录集
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSkin()
        {
            DataTable allskin = (DataTable)CachingService.Get(CachingService.CACHING_ALL_SKIN);
            if (allskin == null)
            {
                allskin = GetAllSkinWithOutCaching();
                CachingService.Set(CachingService.CACHING_ALL_SKIN, allskin, null);
            }
            return allskin;
        }

        /// <summary>
        /// 从数据库中获得皮肤记录集
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSkinWithOutCaching()
        {
            base.SetSkinDataBaseConnection();
            string Sql = "SELECT * FROM Skin WITH (NOLOCK)";
            return conn.GetDataTable(Sql);
        }

        /// <summary>
        /// 创建皮肤
        /// </summary>
        /// <param name="skin">皮肤实体</param>
        /// <returns></returns>
        public int CreateSkin(Skin skin)
        {
            return -19000000;
        }

        /// <summary>
        /// 修改皮肤
        /// </summary>
        /// <param name="skin">皮肤实体</param>
        /// <returns></returns>
        public int UpdateSkin(Skin skin)
        {
            return -19000000;
        }
    }
}
