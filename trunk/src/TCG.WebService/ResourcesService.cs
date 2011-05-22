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
using System.Web.Services.Protocols;

using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;

namespace TCG.WebService
{

    /// <summary>
    /// Summary description for ResourcesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ResourcesService : ServiceMain
    {

        public TCGSoapHeader header;

        public ResourcesService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public int CreateResources(Resources inf)
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return -1000000307;
            }

            //return base.handlerService.resourcsService.resourcesHandlers.CreateResourcesForSheif(inf);
            return 1;
        }

    }

}