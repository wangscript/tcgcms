using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Handlers;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers.Imp.AccEss
{
    public class CategoriesHandlers : ICategoriesHandlers
    {

        /// <summary>
        /// 从数据库中加载分类信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCategoriesWithOutCaching()
        {
            string Sql = "SELECT Id,vcClassName,vcName,SkinId,Parent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,DataBaseService,IsSinglePage,vcPic FROM Categories order by iorder";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = AccessFactory.conn.m_RunSQLData(ref errText, Sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        public DataTable GetCategoriePropertiesByCIdWithOutCaching(string cid)
        {
            string Sql = "SELECT * FROM CategorieProperties WHERE CategorieId='" + cid + "' order by id";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = AccessFactory.conn.m_RunSQLData(ref errText, Sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        public int CreateCategories(Categories cif)
        {

            string Sql = "INSERT INTO Categories(Id,vcClassName,vcName,SkinId,Parent,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,DataBaseService,IsSinglePage,vcPic)"
                    + "VALUES('" + cif.Id + "','" + cif.vcClassName + "','" + cif.vcName + "','" + cif.SkinInfo.Id + "','" + cif.Parent + "','" + cif.ResourceTemplate.Id + "','"
                    + cif.ResourceListTemplate.Id + "','" + cif.vcDirectory + "','" + cif.vcUrl + "','" + cif.iOrder + "','" + cif.cVisible + "','" + cif.DataBaseService + "','" 
                    + cif.IsSinglePage + "','"+cif.vcPic+"')";
            string errText = string.Empty;
            return AccessFactory.conn.m_RunSQL(ref errText, Sql);
        }

        public int CategoriePropertiesManage(Properties cp)
        {
            string sql = string.Empty;
            string errText = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO CategorieProperties(CategorieId,ProertieName,[Type],[Values],width,height) VALUES("
                + "'" + cp.PropertiesCategorieId + "','" + cp.ProertieName + "','" + cp.Type + "','" + cp.Values + "'," + cp.width + "," + cp.height + ")";
            }
            else
            {
                string s_cont = string.Empty;
                int rtn = AccessFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM CategorieProperties WHERE id = " + cp.Id + "", ref s_cont);

                if (rtn < 0) return rtn;

                int ncount = objectHandlers.ToInt(s_cont);

                if (ncount > 0)
                {
                    sql = "UPDATE CategorieProperties SET CategorieId='" + cp.PropertiesCategorieId + "',ProertieName='" + cp.ProertieName + "',[Type]='" + cp.Type
                        + "',[Values]='" + cp.Values + "',width=" + cp.width + ",height=" + cp.height + " WHERE id=" + cp.Id;
                }
                else
                {
                    sql = "INSERT INTO CategorieProperties(CategorieId,ProertieName,[Type],[Values],width,height) VALUES("
            + "'" + cp.PropertiesCategorieId + "','" + cp.ProertieName + "','" + cp.Type + "','" + cp.Values + "'," + cp.width + "," + cp.height + ")";
                }
            }

            return AccessFactory.conn.m_RunSQL(ref errText, sql);
        }

        public int CategoriePropertiesDEL(int cpid)
        {
            string errText = string.Empty;
            int rtn = AccessFactory.conn.m_RunSQL(ref errText, "DELETE FROM ResourceProperties WHERE CategoriePropertieId='" + cpid + "''");
            rtn = AccessFactory.conn.m_RunSQL(ref errText, "DELETE FROM CategorieProperties WHERE id=" + cpid + "");
            return rtn;
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        public int UpdateCategories(Categories cif)
        {

           string Sql = "UPDATE Categories SET vcClassName='" + cif.vcClassName + "',vcName='" + cif.vcName + "',Parent='" + cif.Parent + "',"
                    + "iTemplate='" + cif.ResourceTemplate.Id + "',iListTemplate='" + cif.ResourceListTemplate.Id + "',vcDirectory='" + cif.vcDirectory + "',vcUrl='"
                    + cif.vcUrl + "',iOrder=" + cif.iOrder + ",Visible = '" + cif.cVisible + "',DataBaseService='" + cif.DataBaseService + "', IsSinglePage = '"
                    + cif.IsSinglePage + "',vcPic='" + cif.vcPic + "' WHERE ID ='" + cif.Id + "'";
            string errText = string.Empty;
            return AccessFactory.conn.m_RunSQL(ref errText, Sql);
        }

        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int DelCategories(string classid)
        {
            string s_cont = string.Empty;
            string errText = string.Empty;
            int rtn = AccessFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM resources WHERE iClassID = '" + classid + "'", ref s_cont);

            if (rtn < 0) return rtn;

            int ncount = objectHandlers.ToInt(s_cont);
            //该分类下还存在资源，请移出后再删除
            if (ncount > 0)
            {
                return -1000000032;
            }

            rtn = AccessFactory.conn.m_ExecuteScalar(ref errText, "SELECT  COUNT(1) FROM Categories WHERE Parent = '" + classid + "'", ref s_cont);
            if (rtn < 0) return rtn;
            ncount = objectHandlers.ToInt(s_cont);

            if (ncount > 0)
            {
                return -1000000033;
            }
            string Sql = "DELETE FROM Categories WHERE ID='" + classid + "'";
            return AccessFactory.conn.m_RunSQL(ref errText, Sql);
        }

    }
}
