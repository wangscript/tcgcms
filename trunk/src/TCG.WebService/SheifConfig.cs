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
using System.Xml.Linq;
using System.Web.Services;


using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;
using System.Web.Services.Protocols;

namespace TCG.WebService
{
    /// <summary>
    /// Summary description for SheifConfig
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SheifConfig : ServiceMain
    {
        public TCGSoapHeader header;

        public SheifConfig()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public int CreateSheifCategorieConfig(SheifCategorieConfig sheifcategorieconfig)
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return -1000000307;
            }

            return base.handlerService.sheifService.sheifHandlers.CreateSheifCategorieConfig(sheifcategorieconfig);
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public int UpdateSheifCategorieConfig(SheifCategorieConfig sheifcategorieconfig)
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return -1000000307;
            }

            return base.handlerService.sheifService.sheifHandlers.UpdateSheifCategorieConfig(sheifcategorieconfig);
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public SheifCategorieConfig GetSheifCategorieConfigById(string sheifcategorieconfigid)
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return null;
            }

            Dictionary<string, SheifCategorieConfig> sources = null;
            int rtn = base.handlerService.sheifService.sheifHandlers.GeSheifcategorieconfigs(ref sources);
            if (rtn < 0)
            {
                return null;
            }

            if (sources != null && sources.Count > 0 && sources.ContainsKey(sheifcategorieconfigid)) return sources[sheifcategorieconfigid];
            return null;
        }


        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public List<SheifCategorieConfig> GetSheifCategorieConfigs()
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return null;
            }

            Dictionary<string,SheifCategorieConfig> sources = null;
            int rtn = base.handlerService.sheifService.sheifHandlers.GeSheifcategorieconfigs(ref sources);
            if (rtn < 0)
            {
                return null;
            }

            if (sources != null && sources.Count > 0)
            {
                List<SheifCategorieConfig> alist = new List<SheifCategorieConfig>();
                foreach (KeyValuePair<string, SheifCategorieConfig> entity in sources)
                {
                    SheifCategorieConfig tempcategories = (SheifCategorieConfig)entity.Value;
                    alist.Add(tempcategories);
                }
                return alist;
            }

            return null;
        }
    }
}