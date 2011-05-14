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
using System.Collections;
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;
using TCG.Handlers;

namespace TCG.Handlers
{
    /// <summary>
    /// 方法基类
    /// </summary>
    public class FileObjectHandlersBase : ObjectHandlersBase
    {
        /// <summary>
        /// 设置资讯数据库连接
        /// </summary>
        public void SetDataBaseConnection()
        {
            if (this.configService == null) return;
            if (this.configService.ManageDataBaseStr == null) return;
            this.conn.SetConnStr = this.configService.ManageDataBaseStr;

        }

        ///// <summary>
        ///// 从记录行中得到实体
        ///// </summary>
        ///// <param name="?"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public override EntityBase GetEntityObjectFromRow(DataRow row, Type type)
        //{
        //    if (row == null) return null;
        //    switch (type.ToString())
        //    {
               
        //        case "TCG.Entity.FileCategories":
        //            FileCategories filecagegories = new FileCategories();
        //            filecagegories.Id = row["iId"].ToString();
        //            filecagegories.iParentId = objectHandlers.ToInt(row["iParentId"]);
        //            filecagegories.dCreateDate = objectHandlers.ToTime(row["dCreateDate"].ToString());
        //            filecagegories.vcFileName = row["vcFileName"].ToString();
        //            filecagegories.vcMeno = row["vcMeno"].ToString();
        //            filecagegories.vcKey = row["vcKey"].ToString();
        //            filecagegories.MaxSpace = objectHandlers.ToLong(row["MaxSpace"]);
        //            filecagegories.Space = objectHandlers.ToLong(row["Space"]);
        //            return (EntityBase)filecagegories;
        //        case "TCG.Entity.FileResources":
        //            FileResources fileresource = new FileResources();
        //            fileresource.Id = row["iID"].ToString();
        //            fileresource.iClassId = (int)row["iClassId"];
        //            fileresource.iSize = (int)row["iSize"];
        //            fileresource.vcFileName = row["vcFileName"].ToString();
        //            fileresource.vcIP = row["vcIP"].ToString();
        //            fileresource.vcType = row["vcType"].ToString();
        //            fileresource.iRequest = (int)row["iRequest"];
        //            fileresource.iDowns = (int)row["iDowns"];
        //            fileresource.dCreateDate = (DateTime)row["dCreateDate"];
        //            return (EntityBase)fileresource;

        //    }
        //    return null;
        //}
    }
}
