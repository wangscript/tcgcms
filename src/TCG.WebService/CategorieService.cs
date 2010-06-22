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
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

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

        public CategorieService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public List<Categories> GetAllCategorieEntity()
        {
            //if (!ServiceHandlers.CheckHeader()) return null;

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
    }
}
