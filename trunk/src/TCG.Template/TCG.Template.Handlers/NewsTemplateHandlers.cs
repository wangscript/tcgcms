using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using TCG.Data;

using TCG.Template.Utils;
using TCG.Utils;

namespace TCG.Template.Handlers
{
    public class NewsTemplateHandlers
    {
        /// <summary>
        /// 获得资讯模版
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet GetNewsTemplates(Connection conn)
        {
            conn.Dblink = DBLinkNums.Template;
            string Sql = "SELECT iId,iSiteId,iType,vcTempName,vcContent,vcUrl FROM T_Template_NewsTemplateInfo (NOLOCK)";
            return conn.GetDataSet(Sql);
        }

        /// <summary>
        /// 根据模版类型获得模版
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetNewsTemplatesByType(Connection conn,int type)
        {
            DataSet ds = this.GetNewsTemplates(conn);
            if (ds != null)
            {
                DataRow[] ts = ds.Tables[0].Select("iType=" + type.ToString());
                if (ts.Length != 0)
                {
                    DataSet nt = new DataSet();
                    nt.Merge(ts);
                    return nt;
                }
            }
            return null;
        }


        public int AddNewsTemplate(Connection conn, string adminname, string tempname, int siteid, int type, string content,string url)
        {
            conn.Dblink = DBLinkNums.Template;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iSiteId", SqlDbType.Int, 4); sp2.Value = siteid;
            SqlParameter sp3 = new SqlParameter("@iType", SqlDbType.Int, 4); sp3.Value = type;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = tempname;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = content;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = url;
            SqlParameter sp7 = new SqlParameter("@reValue", SqlDbType.Int); sp7.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_ManageNewsTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7 }, new int[] { 7 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        public int DelNewsTemplate(Connection conn, string adminname, string temps)
        {
            conn.Dblink = DBLinkNums.Template;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@vctemps", SqlDbType.VarChar, 1000); sp2.Value = temps;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_DelNewsTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3 }, new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        public int MdyNewsTemplate(Connection conn, string adminname, string tempname, int siteid, int type, string content, string url,int iid)
        {
            conn.Dblink = DBLinkNums.Template;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iSiteId", SqlDbType.Int, 4); sp2.Value = siteid;
            SqlParameter sp3 = new SqlParameter("@iType", SqlDbType.Int, 4); sp3.Value = type;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = tempname;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = content;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = url;
            SqlParameter sp7 = new SqlParameter("@action", SqlDbType.Char, 2); sp7.Value = "02";
            SqlParameter sp8 = new SqlParameter("@iID", SqlDbType.Int, 255); sp8.Value = iid;
            SqlParameter sp9 = new SqlParameter("@reValue", SqlDbType.Int); sp9.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_ManageNewsTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9 }, new int[] { 9 });
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
        public DataTable GetTemplateByID(Connection conn, int templateid)
        {
            conn.Dblink = DBLinkNums.Template;
            string sql = "SELECT iId,iSiteId,iType,vcTempName,vcContent,vcUrl FROM T_Template_NewsTemplateInfo (NOLOCK) WHERE iId =" + templateid.ToString();
            return conn.GetDataTable(sql);
        }
    }
}