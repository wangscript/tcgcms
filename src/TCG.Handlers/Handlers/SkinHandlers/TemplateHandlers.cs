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
    public class TemplateHandlers : SkinHandlerBase
    {
        /// <summary>
        /// 获得资讯模版
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet GetAllTemplates(Connection conn, bool readdb)
        {
            DataSet ds = null;
            if (readdb)
            {
                ds = GetAllTemplatesFromDb(conn);
            }
            else
            {
                ds = (DataSet)CachingService.Get(CachingService.CACHING_AllTemplates);
                if (ds == null)
                {
                    ds = GetAllTemplatesFromDb(conn);
                    CachingService.Set(CachingService.CACHING_AllTemplates, ds, null);
                }
            }
            return ds;
        }

        public DataSet GetAllTemplatesFromDb(Connection conn)
        {
            base.SetSkinDataBaseConnection();
            string Sql = "SELECT Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM Template (NOLOCK)";
            return conn.GetDataSet(Sql);
        }

        public DataSet GetTemplatesBySystemTypeFromDb(Connection conn, int systemtype)
        {
            base.SetSkinDataBaseConnection();
            string Sql = "SELECT Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM Template (NOLOCK) WHERE "
                        +" iSystemType =" + systemtype.ToString();
            return conn.GetDataSet(Sql);
        }

        public DataSet GetTemplatesBySystemType(Connection conn, int systemtype, bool readdb)
        {
            DataSet ds = null;
            if (readdb)
            {
                ds = GetTemplatesBySystemTypeFromDb(conn, systemtype);
            }
            else
            {
                ds = (DataSet)CachingService.Get(CachingService.CACHING_All_SYSTEM_TEMPLATES + systemtype.ToString());
                if (ds == null)
                {
                    ds = GetTemplatesBySystemTypeFromDb(conn, systemtype);
                    CachingService.Set(CachingService.CACHING_All_SYSTEM_TEMPLATES + systemtype.ToString(), ds, null);
                }
            }
            return ds;
        }

        /// <summary>
        /// 根据模版类型获得模版
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetTemplatesBySystemTypAndType(Connection conn,int type,int systemtype,bool readdb)
        {
            DataSet ds = null;
            if (readdb)
            {
                base.SetSkinDataBaseConnection();
                string Sql = "SELECT Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM Template (NOLOCK) WHERE "
                            + " iSystemType =" + systemtype.ToString() + " AND TemplateType=" + type.ToString();
                ds = conn.GetDataSet(Sql);
            }
            else
            {
                DataSet systtemps = this.GetTemplatesBySystemType(conn, systemtype, false);
                DataRow[] rows = systtemps.Tables[0].Select("TemplateType=" + type.ToString());
                if (rows.Length != 0)
                {
                    ds = new DataSet();
                    ds.Merge(rows);
                }
            }
            return ds;
        }


        public int AddTemplate(Connection conn, string adminname,Template item)
        {
            base.SetSkinDataBaseConnection();
            item.Id = Guid.NewGuid().ToString();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
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

        public int DelTemplate(Connection conn, string adminname, string temps)
        {
            base.SetSkinDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
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

        public int MdyTemplate(Connection conn, string adminname, Template item)
        {
            base.SetSkinDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
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

        /// <summary>
        /// 根据ID获得模版内容
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="templateid"></param>
        /// <returns></returns>
        public DataTable GetTemplateTableByID(Connection conn, string templateid,bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                base.SetSkinDataBaseConnection();
                string sql = "SELECT Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM Template (NOLOCK) WHERE Id ='" + templateid.ToString() + "'";
                dt = conn.GetDataTable(sql);
            }
            else
            {
                DataSet ds = this.GetAllTemplates(conn, false);
                if (ds != null)
                {
                    DataRow[] rows = ds.Tables[0].Select("Id=" + templateid.ToString());
                    if (rows.Length == 1)
                    {
                        DataSet temp = new DataSet();
                        temp.Merge(rows);
                        dt = temp.Tables[0];
                    }
                }
            }
            return dt;
        }

        public Template GetTemplateBySystemAndID(int system, string templateid)
        {
            Template item = null;
            DataSet systetemps = this.GetTemplatesBySystemType(conn, system, false);
            if (systetemps != null)
            {
                DataRow[] rows = systetemps.Tables[0].Select("Id=" + templateid.ToString());
                if (rows.Length == 1)
                {
                    item = new Template();
                    item.Id = rows[0]["Id"].ToString();
                    item.SkinId = rows[0]["SkinId"].ToString();
                    item.TemplateType = objectHandlers.GetTemplateType((int)rows[0]["TemplateType"]);
                    item.iParentId = rows[0]["iParentId"].ToString();
                    item.iSystemType = (int)rows[0]["iSystemType"];
                    item.vcTempName = (string)rows[0]["vcTempName"];
                    item.Content = (string)rows[0]["vcContent"];
                    item.vcUrl = (string)rows[0]["vcUrl"];
                }
            }
            return item;
        }

        public Template GetTemplateByID(string templateid,bool readdb)
        {
            Template item = null;

            if (readdb)
            {
                DataTable dt = this.GetTemplateTableByID(conn, templateid, false);
                if (dt == null) return null;
                if (dt.Rows.Count != 1) return null;
                item = new Template();
                item.Id = dt.Rows[0]["Id"].ToString();
                item.SkinId = dt.Rows[0]["SkinId"].ToString();
                item.TemplateType = objectHandlers.GetTemplateType((int)dt.Rows[0]["TemplateType"]);
                item.iParentId = dt.Rows[0]["iParentId"].ToString();
                item.iSystemType = (int)dt.Rows[0]["iSystemType"];
                item.vcTempName = (string)dt.Rows[0]["vcTempName"];
                item.Content = (string)dt.Rows[0]["vcContent"];
                item.vcUrl = (string)dt.Rows[0]["vcUrl"];
            }
            else
            {
                DataSet ds = GetAllTemplates(conn, false);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    DataRow[] rows = ds.Tables[0].Select("Id = '" + templateid.ToString()+"'");
                    if (rows.Length == 1)
                    {
                        item = new Template();
                        item.Id = rows[0]["Id"].ToString();
                        item.SkinId = ds.Tables[0].Rows[0]["SkinId"].ToString();
                        item.TemplateType = objectHandlers.GetTemplateType((int)rows[0]["TemplateType"]);
                        item.iParentId = rows[0]["iParentId"].ToString();
                        item.iSystemType = (int)rows[0]["iSystemType"];
                        item.vcTempName = (string)rows[0]["vcTempName"];
                        item.Content = (string)rows[0]["vcContent"];
                        item.vcUrl = (string)rows[0]["vcUrl"];
                    }
                }
            }

            return item;
        }
    }
}