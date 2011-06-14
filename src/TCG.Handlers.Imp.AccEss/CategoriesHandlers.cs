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
            return AccessFactory.conn.DataTable(Sql);
        }

        public DataTable GetCategoriePropertiesByCIdWithOutCaching(string cid)
        {
            string Sql = "SELECT * FROM CategorieProperties WHERE CategorieId='" + cid + "' order by id";
            return AccessFactory.conn.DataTable(Sql);
        }

        public int CreateCategories(Categories cif)
        {

            AccessFactory.conn.Execute("INSERT INTO Categories(Id,vcClassName,vcName,SkinId,Parent,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,DataBaseService,IsSinglePage,vcPic)"
                    + "VALUES('" + cif.Id + "','" + cif.vcClassName + "','" + cif.vcName + "','" + cif.SkinInfo.Id + "','" + cif.Parent + "','" + cif.ResourceTemplate.Id + "','"
                    + cif.ResourceListTemplate.Id + "','" + cif.vcDirectory + "','" + cif.vcUrl + "','" + cif.iOrder + "','" + cif.cVisible + "','" + cif.DataBaseService + "','" 
                    + cif.IsSinglePage + "','"+cif.vcPic+"')");
            return 1;
        }

        public int CategoriePropertiesManage(Properties cp)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO CategorieProperties(CategorieId,ProertieName,[Type],[Values],width,height) VALUES("
                + "'" + cp.PropertiesCategorieId + "','" + cp.ProertieName + "','" + cp.Type + "','" + cp.Values + "'," + cp.width + "," + cp.height + ")";
            }
            else
            {
                int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM CategorieProperties WHERE id = " + cp.Id + ""));
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

            AccessFactory.conn.Execute(sql);
            return 1;
        }

        public int CategoriePropertiesDEL(int cpid)
        {
            //删除属性
            AccessFactory.conn.Execute("DELETE FROM ResourceProperties WHERE CategoriePropertieId='" + cpid + "'");
            AccessFactory.conn.Execute("DELETE FROM CategorieProperties WHERE id=" + cpid + "");

            return 1;
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

            AccessFactory.conn.Execute("UPDATE Categories SET vcClassName='" + cif.vcClassName + "',vcName='" + cif.vcName + "',Parent='" + cif.Parent + "',"
                    + "iTemplate='" + cif.ResourceTemplate.Id + "',iListTemplate='" + cif.ResourceListTemplate.Id + "',vcDirectory='" + cif.vcDirectory + "',vcUrl='"
                    + cif.vcUrl + "',iOrder=" + cif.iOrder + ",Visible = '" + cif.cVisible + "',DataBaseService='" + cif.DataBaseService + "', IsSinglePage = '"
                    + cif.IsSinglePage + "',vcPic='" + cif.vcPic + "' WHERE ID ='" + cif.Id + "'");
            return 1;
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

            int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM resources WHERE iClassID = '" + classid + "'"));
            //该分类下还存在资源，请移出后再删除
            if (ncount > 0)
            {
                return -1000000032;
            }

            ncount = 0;
            ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT  COUNT(1) FROM Categories WHERE Parent = '" + classid + "'"));

            if (ncount > 0)
            {
                return -1000000033;
            }
            AccessFactory.conn.Execute("DELETE FROM Categories WHERE ID='" + classid + "'");
            return 1;
        }

    }
}
