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
using TCG.Data;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class TemplateHandlers : ObjectHandlersBase
    {

        public DataTable GetAllTemplates()
        {
            DataTable dt = (DataTable)CachingService.Get(CachingService.CACHING_All_TEMPLATES);
            if (dt == null)
            {
                dt = GetAllTemplatesWithOutCaching();
                CachingService.Set(CachingService.CACHING_All_TEMPLATES, dt, null);
            }
            return dt;
        }

        public Dictionary<string, EntityBase> GetAllTemplatesEntity()
        {
            Dictionary<string, EntityBase> templates = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_All_TEMPLATES_ENTITY);
            if (templates == null)
            {
                templates = base.GetEntitysObjectFromTable(this.GetAllTemplates(), typeof(Template));
                CachingService.Set(CachingService.CACHING_All_TEMPLATES_ENTITY, templates, null);
            }
            return templates;
        }

        public Template GetTemplateByID(string templateid)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            return (Template)templates[templateid];
        }

        public DataTable GetAllTemplatesWithOutCaching()
        {
            base.SetDataBaseConnection();
            string Sql = "SELECT Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM Template (NOLOCK)";
            return conn.GetDataTable(Sql);
        }

        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetTemplatesByTemplateType(TemplateType templatetype)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            if (templates.Count == 0) return null;
            Dictionary<string, EntityBase> childtemplates = new Dictionary<string, EntityBase>();
            foreach (KeyValuePair<string, EntityBase> entity in templates)
            {
                Template temp = (Template)entity.Value;
                if (temp.TemplateType == templatetype)
                {
                    childtemplates.Add(temp.Id, (EntityBase)temp);
                }
            }
            return childtemplates;
        }

        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetTemplates(string skinid, string parentid, int templatetype)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            if (templates.Count == 0) return null;
            Dictionary<string, EntityBase> childtemplates = new Dictionary<string, EntityBase>();
            foreach (KeyValuePair<string, EntityBase> entity in templates)
            {
                Template temp = (Template)entity.Value;
                if (templatetype == -1)
                {
                    if (skinid == temp.SkinId && parentid == temp.iParentId)
                    {
                        childtemplates.Add(temp.Id, (EntityBase)temp);
                    }
                }
                else
                {
                    if ((int)temp.TemplateType == templatetype && skinid == temp.SkinId && parentid == temp.iParentId)
                    {
                        childtemplates.Add(temp.Id, (EntityBase)temp);
                    }
                }
            }
            return childtemplates;
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddTemplate(Template item,Admin admin)
        {
            base.SetDataBaseConnection();
            item.Id = Guid.NewGuid().ToString();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = admin.vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@SkinId", SqlDbType.VarChar, 36); sp2.Value = item.SkinId;
            SqlParameter sp3 = new SqlParameter("@TemplateType", SqlDbType.Int, 4); sp3.Value = (int)item.TemplateType;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = item.vcTempName;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = item.Content;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = item.vcUrl;
            SqlParameter sp7 = new SqlParameter("@iParentId", SqlDbType.Int, 4); sp7.Value = item.iParentId;
            SqlParameter sp8 = new SqlParameter("@iSystemType", SqlDbType.Int, 4); sp8.Value = item.iSystemType;
            SqlParameter sp9 = new SqlParameter("@Id", SqlDbType.VarChar, 36); sp9.Value = item.Id;
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int); sp10.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_ManageTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3, 
                sp4, sp5, sp6, sp7 , sp8 , sp9,sp10}, new int[] { 10 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int DelTemplate(string temps,Admin admin)
        {
            base.SetDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = admin.vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vctemps", SqlDbType.VarChar, 1000); sp2.Value = temps;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_DelTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3 }, new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 修改模板信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int MdyTemplate(Template item,Admin admin)
        {
            base.SetDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = admin.vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@SkinId", SqlDbType.VarChar, 36); sp2.Value = item.SkinId;
            SqlParameter sp3 = new SqlParameter("@TemplateType", SqlDbType.Int, 4); sp3.Value = (int)item.TemplateType;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = item.vcTempName;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = item.Content;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = item.vcUrl;
            SqlParameter sp7 = new SqlParameter("@action", SqlDbType.Char, 2); sp7.Value = "02";
            SqlParameter sp8 = new SqlParameter("@Id", SqlDbType.VarChar, 36); sp8.Value = item.Id;
            SqlParameter sp9 = new SqlParameter("@reValue", SqlDbType.Int); sp9.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_ManageTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3,
                sp4, sp5, sp6, sp7, sp8, sp9 }, new int[] { 9 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

    }
}