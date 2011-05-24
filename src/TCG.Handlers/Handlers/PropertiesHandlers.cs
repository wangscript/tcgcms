using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class PropertiesHandlers
    {

        /// <summary>
        /// 根据属性分类获得属性列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetPropertiesByCId(string cid)
        {
            DataTable allcategoriesp = (DataTable)CachingService.Get(CachingService.CACHING_ALL_PROPERTIES + cid);
            if (allcategoriesp == null)
            {
                allcategoriesp = DataBaseFactory.propertiesHandlers.GetPropertiesByCIdWithOutCaching(cid);
                CachingService.Set(CachingService.CACHING_ALL_PROPERTIES + cid, allcategoriesp, null);
            }
            return allcategoriesp;
        }

        /// <summary>
        /// 根据属性分类获得属性列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetPropertiesByCIdEntity(string cid)
        {
            Dictionary<string, EntityBase> allcategories = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_PROPERTIES_ENTITY + cid);
            if (allcategories == null)
            {
                DataTable dt = GetPropertiesByCId(cid);
                if (dt == null) return null;
                allcategories = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(Properties));
                CachingService.Set(CachingService.CACHING_ALL_PROPERTIES_ENTITY + cid, allcategories, null);
            }
            return allcategories;
        }

        public int PropertiesManage(Admin admin, Properties cp)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            return DataBaseFactory.propertiesHandlers.PropertiesManage(cp);
        }

        public int PropertiesDEL(Admin admininf, int cpid)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admininf);
            if (rtn < 0) return rtn;

            return DataBaseFactory.propertiesHandlers.PropertiesDEL(cpid);
        }

        public int GetMaxProperties()
        {
            return DataBaseFactory.propertiesHandlers.GetMaxProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetPropertiesCategoriesEntityBySkinId(string skinid)
        {
            Dictionary<string, EntityBase> allcategories = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_PROPERTIES_CATEGORIES_ENTITY + skinid);
            if (allcategories == null)
            {
                DataTable dt = GetPropertiesCategoriesBySkinId(skinid);
                if (dt == null) return null;
                allcategories = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(PropertiesCategorie));
                CachingService.Set(CachingService.CACHING_ALL_PROPERTIES_ENTITY + skinid, allcategories, null);
            }
            return allcategories;
        }

        public PropertiesCategorie GetPropertiesCategoriesBySkinidAndId(string skinid, string id)
        {
            Dictionary<string, EntityBase> allcagetories = GetPropertiesCategoriesEntityBySkinId(skinid);
            if (allcagetories == null) return null;
            if (!allcagetories.ContainsKey(id)) return null;
            return (PropertiesCategorie)allcagetories[id];
        }


        public DataTable GetPropertiesCategoriesBySkinId(string skinid)
        {
            return DataBaseFactory.propertiesHandlers.GetPropertiesCategoriesBySkinId(skinid);
        }


        public int PropertiesCategoriesManage(Admin admin, PropertiesCategorie cp)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            return DataBaseFactory.propertiesHandlers.PropertiesCategoriesManage(cp);
        }

        public int GetMaxPropertiesCategrie()
        {
            return DataBaseFactory.propertiesHandlers.GetMaxPropertiesCategrie();
        }
    }
}
