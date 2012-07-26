using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Handlers;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers.Imp.MsSql
{
    public class ResourcesHandlers :IResourceHandlers
    {
        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int CreateResources(Resources inf)
        {

            string Sql = "INSERT INTO resources (id,iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iCount,vcKeyWord,"
        + "vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,cDel,cPostByUser,"
        + "vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong,SheifUrl,PropertiesCategorieId,CCCategories) VALUES(" + inf.Id + ",'" + inf.Categorie.Id + "','" + inf.vcTitle + "','"
        + inf.vcUrl + "','" + inf.vcContent.Replace("'", "''") + "','" + inf.vcAuthor + "','" + inf.iCount + "','" + inf.vcKeyWord + "','" + inf.vcEditor + "','" + inf.cCreated + "','"
        + inf.vcSmallImg + "','" + inf.vcBigImg + "','" + inf.vcShortContent.Replace("'", "''") + "','" + inf.vcSpeciality + "','" + inf.cChecked + "','"
        + inf.cDel + "','" + inf.cPostByUser + "','" + inf.vcFilePath + "','" + inf.dAddDate + "','" + inf.dUpDateDate + "','"
        + inf.vcTitleColor + "','" + inf.cStrong + "','" + inf.SheifUrl + "','" + inf.PropertiesCategorieId + "','" + inf.CCCategories + "')";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, Sql);

        }

        public int GetMaxResourceId()
        {
            string errText = string.Empty;
            string s_cont = string.Empty;
            int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT max(id) FROM resources", ref s_cont);

            if (rtn < 0) return rtn;

            return objectHandlers.ToInt(s_cont);

        }

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int UpdateResources(Resources inf)
        {

            string sql = "UPDATE resources SET iClassID='" + inf.Categorie.Id + "',vcTitle='" + inf.vcTitle + "',vcUrl='"
                + inf.vcUrl + "',vcContent='" + inf.vcContent.Replace("'", "''") + "',vcAuthor='" + inf.vcAuthor + "',iCount='"
                + inf.iCount + "',vcKeyWord='" + inf.vcKeyWord + "',vcEditor='" + inf.vcEditor + "',cCreated='"
                + inf.cCreated + "',vcSmallImg='" + inf.vcSmallImg + "',vcBigImg='" + inf.vcBigImg + "',vcShortContent='"
                + inf.vcShortContent.Replace("'", "''") + "',vcSpeciality='" + inf.vcSpeciality + "',cChecked='" + inf.cChecked + "',cDel='"
                + inf.cDel + "',cPostByUser='" + inf.cPostByUser + "',vcFilePath='" + inf.vcFilePath
                + "',dUpDateDate='" + inf.dUpDateDate + "',vcTitleColor = '" + inf.vcTitleColor + "',cStrong = '"
                + inf.cStrong + "',PropertiesCategorieId='" + inf.PropertiesCategorieId + "',CCCategories='" + inf.CCCategories + "' WHERE Id = " + inf.Id;
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceid"></param>
        /// <returns></returns>
        public DataTable GetResourcesById(int resourceid)
        {
            string Sql = "SELECT * FROM Resources WHERE ID = " + resourceid.ToString().Trim() + "";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, Sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
          
        }



        public DataTable GetDelNewsInfoList(string ids)
        {
            string sql = "UPDATE resources SET cDel = 'Y',cCreated = 'N' WHERE Id IN (" + ids + ")";

            string errText = string.Empty;
            int rtn = MsSqlFactory.conn.m_RunSQL(ref errText, sql);

            if (rtn < 0) return null;

            sql = "SELECT * FROM resources WHERE Id IN (" + ids + ")";

            DataSet ds = null;
            rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        public DataTable GetAllResuresFromDataBase()
        {

            string sql = "SELECT * FROM Resources ";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="categories"></param>
        /// <param name="Speciality"></param>
        /// <param name="orders"></param>
        /// <param name="check"></param>
        /// <param name="del"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public DataTable GetResourcesList(string sqlsb)
        {
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sqlsb, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }


        /// <summary>
        /// 获取指定页数
        /// </summary>
        /// <param name="curPage"></param>
        /// <param name="pageCount"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="order"></param>
        /// <param name="strCondition"></param>
        /// <returns></returns>
        public DataTable GetResourcesListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition)
        {
            curPage = page;
            return MsSqlFactory.conn.ExecutePager(page, pagesize, " * ", "Resources", strCondition, order, out pageCount, out count);
        }

        /// <summary>
        /// 就会或删除资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int SaveOrDelResource(string ids, string action)
        {

            //尚未选择资源
            if (string.IsNullOrEmpty(ids))
            {
                return -1000000501;
            }

            string errText = string.Empty;
            string sql = string.Empty;
            int rtn = 1;
            if (action == "SAVE")
            {
                sql = "UPDATE resources SET cDel = ''N'' WHERE Id IN (" + ids + ")";
                rtn = MsSqlFactory.conn.m_RunSQL(ref errText, sql);
            }

            if (action == "DEL")
            {
                sql = "DELETE FROM resources WHERE Id IN (" + ids + ")";
                rtn = MsSqlFactory.conn.m_RunSQL(ref errText, sql);
            }

            return rtn;
        }

        /// <summary>
        /// 获取分类下最新的一篇文章
        /// </summary>
        /// <param name="ategorie"></param>
        /// <returns></returns>
        public DataTable GetNewsResourcesAtCategorie(string ategorie)
        {
            string sqlsb = "SELECT TOP 1 * FROM Resources WHERE iClassID = '" + ategorie + "'";

            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sqlsb, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }


        public int ResourcePropertiesManage(ResourceProperties cp)
        {
            string sql = string.Empty;
            string errText = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO ResourceProperties(ResourceId,PropertieName,PropertieValue,PropertieId,iOrder) VALUES("
                + "'" + cp.ResourceId + "','" + cp.PropertieName + "','" + cp.PropertieValue + "','" + cp.PropertieId + "'," + cp.iOrder + ")";
            }
            else
            {
            
                string s_cont = string.Empty;
                int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM ResourceProperties WHERE ResourceId = '"
                    + cp.ResourceId + "' AND PropertieId='" + cp.PropertieId + "'", ref s_cont);

                if (rtn < 0) return rtn;

                int ncount = objectHandlers.ToInt(s_cont);
                if (ncount > 0)
                {
                    sql = "UPDATE ResourceProperties SET ResourceId='" + cp.ResourceId + "',PropertieName='" + cp.PropertieName + "',PropertieValue='" + cp.PropertieValue
                        + "',PropertieId='" + cp.PropertieId + "',iOrder=" + cp.iOrder + " WHERE id=" + cp.Id;
                }
                else
                {
                    sql = "INSERT INTO ResourceProperties(ResourceId,PropertieName,PropertieValue,PropertieId,iOrder) VALUES("
                 + "'" + cp.ResourceId + "','" + cp.PropertieName + "','" + cp.PropertieValue + "','" + cp.PropertieId + "'," + cp.iOrder + ")";
                }
            }

          
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }


        public DataTable GetResourcePropertiesByRIdEntity(string rid)
        {
            string sqlsb = "SELECT * FROM ResourceProperties WHERE ResourceId='" + rid + "' order by iOrder";

            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sqlsb, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        public int DelResourcesProperties(string resid)
        {
             string sql = "DELETE FROM ResourceProperties WHERE ResourceId='" + resid + "'";
             string errText = string.Empty;
             return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        public int DelResourcesPropertiesOnIds(string resid, string ids)
        {
            string sql = "DELETE FROM ResourceProperties WHERE ResourceId='" + resid + "' and id in(" + ids + ")";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }
    }
}
