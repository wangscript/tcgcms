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

using TCG.Template.Utils;
using TCG.Template.Entity;
using TCG.Utils;

namespace TCG.Template.Handlers
{
    public class TemplateHandlers
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
                ds = (DataSet)Caching.Get(TemplateConstant.CACHING_AllTemplates);
                if (ds == null)
                {
                    ds = GetAllTemplatesFromDb(conn);
                    Caching.Set(TemplateConstant.CACHING_AllTemplates, ds, null);
                }
            }
            return ds;
        }

        public DataSet GetAllTemplatesFromDb(Connection conn)
        {
            conn.Dblink = DBLinkNums.Template;
            string Sql = "SELECT iId,iSiteId,iType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM T_Template_TemplateInfo (NOLOCK)";
            return conn.GetDataSet(Sql);
        }

        public DataSet GetTemplatesBySystemTypeFromDb(Connection conn, int systemtype)
        {
            conn.Dblink = DBLinkNums.Template;
            string Sql = "SELECT iId,iSiteId,iType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM T_Template_TemplateInfo (NOLOCK) WHERE "
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
                ds = (DataSet)Caching.Get(TemplateConstant.CACHING_AllTemplates_System + systemtype.ToString());
                if (ds == null)
                {
                    ds = GetTemplatesBySystemTypeFromDb(conn, systemtype);
                    Caching.Set(TemplateConstant.CACHING_AllTemplates_System + systemtype.ToString(), ds, null);
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
                conn.Dblink = DBLinkNums.Template;
                string Sql = "SELECT iId,iSiteId,iType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM T_Template_TemplateInfo (NOLOCK) WHERE "
                            + " iSystemType =" + systemtype.ToString() + " AND iType=" + type.ToString();
                ds = conn.GetDataSet(Sql);
            }
            else
            {
                DataSet systtemps = this.GetTemplatesBySystemType(conn, systemtype, false);
                DataRow[] rows = systtemps.Tables[0].Select("iType=" + type.ToString());
                if (rows.Length != 0)
                {
                    ds = new DataSet();
                    ds.Merge(rows);
                }
            }
            return ds;
        }


        public int AddTemplate(Connection conn, string adminname,TemplateInfo item)
        {
            conn.Dblink = DBLinkNums.Template;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iSiteId", SqlDbType.Int, 4); sp2.Value = item.iSiteId;
            SqlParameter sp3 = new SqlParameter("@iType", SqlDbType.Int, 4); sp3.Value = item.iType;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = item.vcTempName;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = item.vcContent;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = item.vcUrl;
            SqlParameter sp7 = new SqlParameter("@iParentId", SqlDbType.Int, 4); sp7.Value = item.iParentId;
            SqlParameter sp8 = new SqlParameter("@iSystemType", SqlDbType.Int, 4); sp8.Value = item.iSystemType;
            SqlParameter sp9 = new SqlParameter("@reValue", SqlDbType.Int); sp9.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_ManageTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3, 
                sp4, sp5, sp6, sp7 , sp8 , sp9}, new int[] { 9 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        public int DelTemplate(Connection conn, string adminname, string temps)
        {
            conn.Dblink = DBLinkNums.Template;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
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

        public int MdyTemplate(Connection conn, string adminname, TemplateInfo item)
        {
            conn.Dblink = DBLinkNums.Template;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iSiteId", SqlDbType.Int, 4); sp2.Value = item.iSiteId;
            SqlParameter sp3 = new SqlParameter("@iType", SqlDbType.Int, 4); sp3.Value = item.iType;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = item.vcTempName;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = item.vcContent;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = item.vcUrl;
            SqlParameter sp7 = new SqlParameter("@action", SqlDbType.Char, 2); sp7.Value = "02";
            SqlParameter sp8 = new SqlParameter("@iID", SqlDbType.Int, 255); sp8.Value = item.iId;
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
        public DataTable GetTemplateByID(Connection conn, int templateid,bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                conn.Dblink = DBLinkNums.Template;
                string sql = "SELECT iId,iSiteId,iType,iParentId,iSystemType,vcTempName,vcContent,vcUrl FROM T_Template_TemplateInfo (NOLOCK) WHERE iId =" + templateid.ToString();
                dt = conn.GetDataTable(sql);
            }
            else
            {
                DataSet ds = this.GetAllTemplates(conn, false);
                if (ds != null)
                {
                    DataRow[] rows = ds.Tables[0].Select("iId=" + templateid.ToString());
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

        public TemplateInfo GetTemplateInfoBySystemAndID(Connection conn, int system, int templateid)
        {
            TemplateInfo item = null;
            DataSet systetemps = this.GetTemplatesBySystemType(conn, system, false);
            if (systetemps != null)
            {
                DataRow[] rows = systetemps.Tables[0].Select("iId=" + templateid.ToString());
                if (rows.Length == 1)
                {
                    item = new TemplateInfo();
                    item.iId = (int)rows[0]["iId"];
                    item.iSiteId = (int)rows[0]["iSiteId"];
                    item.iType = (int)rows[0]["iType"];
                    item.iParentId = (int)rows[0]["iParentId"];
                    item.iSystemType = (int)rows[0]["iSystemType"];
                    item.vcTempName = (string)rows[0]["vcTempName"];
                    item.vcContent = (string)rows[0]["vcContent"];
                    item.vcUrl = (string)rows[0]["vcUrl"];
                }
            }
            return item;
        }

        public TemplateInfo GetTemplateInfoByID(Connection conn, int templateid)
        {
            DataTable dt = this.GetTemplateByID(conn, templateid,false);
            if (dt == null) return null;
            if (dt.Rows.Count != 1) return null;
            TemplateInfo item = new TemplateInfo();
            item.iId = (int)dt.Rows[0]["iId"];
            item.iSiteId = (int)dt.Rows[0]["iSiteId"];
            item.iType = (int)dt.Rows[0]["iType"];
            item.iParentId = (int)dt.Rows[0]["iParentId"];
            item.iSystemType = (int)dt.Rows[0]["iSystemType"];
            item.vcTempName = (string)dt.Rows[0]["vcTempName"];
            item.vcContent = (string)dt.Rows[0]["vcContent"];
            item.vcUrl = (string)dt.Rows[0]["vcUrl"];
            return item;
        }
    }
}