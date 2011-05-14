
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
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;

namespace TCG.Handlers
{
    public class FileResourcesHandlerBase : FileObjectHandlersBase
    {
        /// <summary>
        /// 设置数据库链接
        /// </summary>
        /// <returns></returns>
        protected bool SetFileDatabase(string fid)
        {
            //if (ConfigServiceEx == null) return false;
            //if (ConfigServiceEx.fileDataBaseConfig == null) return false;
            //if (ConfigServiceEx.fileDataBaseConfig.Count == 0) return false;
            //int index = objectHandlers.ToInt(objectHandlers.ToLong(fid) % ConfigServiceEx.fileDataBaseConfig.Count);
            //DataBaseConnStr filedatabase = ConfigServiceEx.fileDataBaseConfig[index];
            //base.conn.SetConnStr = filedatabase.Value;
            return true;
        }

        protected void SetMainFileDataBase()
        {
            if (this.MianDatabase == null) return;
            base.conn.SetConnStr = this.MianDatabase;
        }
        /// <summary>
        /// 获得主文件库
        /// </summary>
        /// <returns></returns>
        private string GetMainFileDatabaseStr()
        {
            //if (ConfigServiceEx == null) return null;
            //if (ConfigServiceEx.fileDataBaseConfig == null) return null;
            //if (ConfigServiceEx.fileDataBaseConfig.Count == 0) return null;

            //foreach (DataBaseConnStr database in ConfigServiceEx.fileDataBaseConfig)
            //{
            //    if (database.IsBaseDataBase)
            //    {
            //        return database.Value;

            //    }
            //}
            return null;
        }

        protected string MianDatabase
        {
            get
            {
                if (this._maindatabase == null)
                {
                    this._maindatabase = this.GetMainFileDatabaseStr();
                }
                return this._maindatabase;
            }
        }
        private string _maindatabase = null;
    }
}
