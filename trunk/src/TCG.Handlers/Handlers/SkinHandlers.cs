using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class SkinHandlers 
    {
        /// <summary>
        /// 根据皮肤ID得到皮肤信息
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public Skin GetSkinEntityBySkinId(string skinid)
        {
            Dictionary<string, EntityBase> skins = this.GetAllSkinEntity();
            if (skins == null) return null;
            if (skins.ContainsKey(skinid))
            {
                return (Skin)skins[skinid];
            }
            return null;
        }

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
                allskin = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(Skin));
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
            return DataBaseFactory.SkinHandlers.GetAllSkinWithOutCaching();
        }

        /// <summary>
        /// 创建皮肤
        /// </summary>
        /// <param name="skin">皮肤实体</param>
        /// <returns></returns>
        public int CreateSkin(Skin skin)
        {
            return DataBaseFactory.SkinHandlers.CreateSkin(skin);
        }

        /// <summary>
        /// 修改皮肤
        /// </summary>
        /// <param name="skin">皮肤实体</param>
        /// <returns></returns>
        public int UpdateSkin(Skin skin)
        {
            return DataBaseFactory.SkinHandlers.UpdateSkin(skin);
        }
    }
}
