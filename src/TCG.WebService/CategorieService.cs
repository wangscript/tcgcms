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

using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;

using System;
using System.IO;
using System.Web.Services;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Web.Services.Protocols;
using System.Reflection;
using System.Linq;

namespace TCG.WebService
{

    /// <summary>
    /// Summary description for CategorieService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CategorieService : ServiceMain
    {
        public TCGSoapHeader header;

        public CategorieService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public List<Categories> GetAllCategorieEntity()
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return null;
            }

            Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntity();
            if (allcategories != null && allcategories.Count > 0)
            {
                List<Categories> alist = new List<Categories>();
                foreach (KeyValuePair<string, EntityBase> entity in allcategories)
                {
                    Categories tempcategories = (Categories)entity.Value;
                    alist.Add(tempcategories);
                }
                return alist;
            }
            return null;
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public Categories GetCategorieById(string categorieid)
        {
            if (!base.serviceHandlers.CheckHeader(header))
            {
                return null;
            }

            Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntitySkinId(base.configService.DefaultSkinId);
            if (allcategories != null && allcategories.Count > 0)
            {
                if (allcategories.ContainsKey(categorieid))
                {
                    return (Categories)allcategories[categorieid];
                }
            }
            return null;
        }

        [WebMethod]
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public List<EntityBase> GetDefaultCategories()
        {
            Dictionary<string, EntityBase> allcategories = base.handlerService.skinService.categoriesHandlers.GetAllCategoriesEntitySkinId(base.configService.DefaultSkinId);
            if (allcategories == null && allcategories.Count == 0)
            {
                return null;
            }
            return allcategories.Values.ToList();
        }
    }
}
