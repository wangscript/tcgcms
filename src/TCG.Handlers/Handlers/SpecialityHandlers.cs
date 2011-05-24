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
    public class SpecialityHandlers
    {
        /// <summary>
        /// 添加新的资讯类型
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        public int NewsSpecialityAdd(Admin adminname, Speciality nif)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(adminname);
            if (rtn < 0) return rtn;

            return DataBaseFactory.specialityHandlers.NewsSpecialityAdd(nif);
        }

        /// <summary>
        /// 修改资讯类型
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        public int NewsSpecialityMdy(Admin adminname, Speciality nif)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(adminname);
            if (rtn < 0) return rtn;
            return DataBaseFactory.specialityHandlers.NewsSpecialityMdy(nif);
        }


        /// <summary>
        /// 删除特性
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int NewSpecialityDel(Admin adminname, string ids)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(adminname);
            if (rtn < 0) return rtn;
            return DataBaseFactory.specialityHandlers.NewSpecialityDel(ids);
        }

   

        /// <summary>
        /// 获得所有资源特性
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllNewsSpecialityInfoBySkinId(string skinid)
        {
            return DataBaseFactory.specialityHandlers.GetAllNewsSpecialityInfo(skinid);
        }

        /// <summary>
        /// 获得制定皮肤下所有分类
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllNewsSpecialityEntityBySkinId(string skinid)
        {
            Dictionary<string, EntityBase> specialits = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_SPECIALITY_ENTITY + "_" + skinid);
            if (specialits == null || specialits.Count == 0)
            {
                DataTable dt = this.GetAllNewsSpecialityInfoBySkinId(skinid);
                if (dt == null) return null;
                specialits = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(Speciality));
                CachingService.Set(CachingService.CACHING_ALL_SPECIALITY_ENTITY + "_" + skinid, specialits, null);
            }

            return specialits;
        }

        public Dictionary<string, EntityBase> GetAllNewsSpecialityEntityBySkinIdAndParentid(string skinid, int iparent)
        {
            Dictionary<string, EntityBase> categories = this.GetAllNewsSpecialityEntityBySkinId(skinid);
            Dictionary<string, EntityBase> categories1 = new Dictionary<string, EntityBase>();
            if (categories != null)
            {
                foreach (KeyValuePair<string, EntityBase> entity in categories)
                {
                    Speciality slty = (Speciality)entity.Value;
                    if (slty.iParent == iparent)
                    {
                        categories1.Add(slty.Id, slty);
                    }
                }
            }

            return (categories1 == null || categories1.Count == 0) ? null : categories1;
        }

        public Speciality GetSpecialityById(string skinid, string id)
        {
            Dictionary<string, EntityBase> categories = this.GetAllNewsSpecialityEntityBySkinId(skinid);
            if (categories != null)
            {
                foreach (KeyValuePair<string, EntityBase> entity in categories)
                {
                    Speciality slty = (Speciality)entity.Value;
                    if (slty.Id == id) return slty;
                }
            }
            return null;
        }
       
    }
}