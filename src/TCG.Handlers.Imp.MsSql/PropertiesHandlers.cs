using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers.Imp.MsSql
{
    public class PropertiesHandlers : IPropertiesHandlers
    {
        public DataTable GetPropertiesByCIdWithOutCaching(string cid)
        {
            string Sql = "SELECT * FROM Properties WHERE PropertiesCategorieId='" + cid + "' order by id";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText,Sql,ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        public int PropertiesDEL(int cpid)
        {
            string errText = string.Empty;
            int rtn = MsSqlFactory.conn.m_RunSQL(ref errText,"DELETE FROM ResourceProperties WHERE PropertieId='" + cpid + "'");
            rtn = MsSqlFactory.conn.m_RunSQL(ref errText, "DELETE FROM Properties WHERE id=" + cpid + "");
            return rtn;
        }


        public int PropertiesCategoriesManage(PropertiesCategorie cp)
        {
            string sql = string.Empty;

            string errText = string.Empty;
            string s_cont = string.Empty;
            int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM PropertiesCategorie WHERE id = " + cp.Id + "", ref s_cont);

            if (rtn < 0) return rtn;

            int ncount = objectHandlers.ToInt(s_cont);
            if (ncount > 0)
            {
                sql = "UPDATE PropertiesCategorie SET CategoriePropertiesName='" + cp.CategoriePropertiesName + "',Visible='" + cp.Visible + "',[SkinId]='" + cp.SkinId
                    + "' WHERE id=" + cp.Id;
            }
            else
            {
                sql = "INSERT INTO PropertiesCategorie(id,CategoriePropertiesName,Visible,[SkinId]) VALUES("
                      + "" + cp.Id + ",'" + cp.CategoriePropertiesName + "','" + cp.Visible + "','" + cp.SkinId + "')";
            }

            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }


        public int PropertiesManage(Properties cp)
        {
            string errText = string.Empty;
            string sql = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO Properties(PropertiesCategorieId,ProertieName,[Type],[Values],width,height,iOrder) VALUES("
                + "'" + cp.PropertiesCategorieId + "','" + cp.ProertieName + "','" + cp.Type + "','" + cp.Values + "'," + cp.width + ","
                + cp.height + ",iOrder=" + cp.iOrder + ")";
            }
            else
            {
                string s_cont = string.Empty;
                int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM Properties WHERE id = " + cp.Id + "", ref s_cont);

                if (rtn < 0) return rtn;

                int ncount = objectHandlers.ToInt(s_cont);
                if (ncount > 0)
                {
                    sql = "UPDATE Properties SET PropertiesCategorieId='" + cp.PropertiesCategorieId + "',ProertieName='" + cp.ProertieName + "',[Type]='" + cp.Type
                        + "',[Values]='" + cp.Values + "',width=" + cp.width + ",height=" + cp.height + ",iOrder=" + cp.iOrder + " WHERE id=" + cp.Id;
                }
                else
                {
                    sql = "INSERT INTO Properties(PropertiesCategorieId,ProertieName,[Type],[Values],width,height,iOrder) VALUES("
            + "'" + cp.PropertiesCategorieId + "','" + cp.ProertieName + "','" + cp.Type + "','" + cp.Values + "'," + cp.width + ","
            + cp.height + "," + cp.iOrder + ")";
                }
            }

            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
            
        }

        public int GetMaxPropertiesCategrie()
        {
            string errText = string.Empty;
            string s_cont = string.Empty;
            int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT Max(id) FROM PropertiesCategorie", ref s_cont);

            if (rtn < 0) return rtn;

            return objectHandlers.ToInt(s_cont);
        }

        public int GetMaxProperties()
        {
            string errText = string.Empty;
            string s_cont = string.Empty;
            int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT Max(id) FROM Properties", ref s_cont);

            if (rtn < 0) return rtn;

            return objectHandlers.ToInt(s_cont);
        }

        public DataTable GetPropertiesCategoriesBySkinId(string skinid)
        {
            string Sql = "SELECT * FROM PropertiesCategorie WHERE SkinId='" + skinid + "' order by id";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, Sql, ref ds);

            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }
    }
}
