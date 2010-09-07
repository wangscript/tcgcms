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
using System.Web;
using System.Web.Services;


using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;

namespace TCG.WebService
{
    /// <summary>
    ///SheifSource 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SheifSourceService : System.Web.Services.WebService
    {

        public SheifSourceService()
        {
            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        public int AddSheifSource(SheifSourceInfo source)
        {

            SheifHandlers sheifhld = new SheifHandlers();
            sheifhld.conn = new TCG.Data.Connection();
            sheifhld.configService = new ConfigService();

            return sheifhld.AddShiefSource(source);
        }

        [WebMethod]
        public List<SheifSourceInfo> GetAllSheifSourceInfos()
        {
            SheifHandlers sheifhld = new SheifHandlers();
            sheifhld.conn = new TCG.Data.Connection();
            sheifhld.configService = new ConfigService();
            List<SheifSourceInfo> sources = null;
            int rtn = sheifhld.GetAllShieSourceInfo(ref sources);
            if (rtn < 0)
            {
                return null;
            }

            return sources;
        }
    }
}

