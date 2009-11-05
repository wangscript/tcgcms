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
    public class ResourceHandlerBase : ObjectHandlersBase
    {
        public void SetReourceHandlerDataBaseConnection(string resourceid)
        {
            if (base.configService == null) return;
            if (base.configService.ResourceDataBaseConfig == null) return;
            if (base.configService.ResourceDataBaseConfig.Count == 0) return;
            //获得文章编号转ASCII码 取模的值
            int index = objectHandlers.ToInt(objectHandlers.GetStringAscSum(resourceid) % base.configService.fileDataBaseConfig.Count);
            DataBaseConnStr database = base.configService.ResourceDataBaseConfig[index];
            base.conn.SetConnStr = database.Value;
        }

        public void SetReourceHandlerDataBaseConnection()
        {
            base.conn.SetConnStr = base.configService.ManageDataBaseStr; ;
        }
    }
}
