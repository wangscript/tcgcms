using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers.Imp.AccEss
{
    public class PropertiesHandlers : IPropertiesHandlers
    {
        public DataTable GetPropertiesByCIdWithOutCaching(string cid)
        {
            string Sql = "SELECT * FROM Properties WHERE PropertiesCategorieId='" + cid + "' order by id";
            return AccessFactory.conn.DataTable(Sql);
        }

        public int PropertiesDEL(int cpid)
        {
            //删除属性
            AccessFactory.conn.Execute("DELETE FROM ResourceProperties WHERE PropertieId='" + cpid + "'");
            AccessFactory.conn.Execute("DELETE FROM Properties WHERE id=" + cpid + "");

            return 1;
        }


        public int PropertiesCategoriesManage(PropertiesCategorie cp)
        {
            string sql = string.Empty;

            int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM PropertiesCategorie WHERE id = " + cp.Id + ""));
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

            AccessFactory.conn.Execute(sql);
            return 1;
        }


        public int PropertiesManage(Properties cp)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO Properties(PropertiesCategorieId,ProertieName,[Type],[Values],width,height,iOrder) VALUES("
                + "'" + cp.PropertiesCategorieId + "','" + cp.ProertieName + "','" + cp.Type + "','" + cp.Values + "'," + cp.width + ","
                + cp.height + ",iOrder=" + cp.iOrder + ")";
            }
            else
            {
                int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM Properties WHERE id = " + cp.Id + ""));
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

            AccessFactory.conn.Execute(sql);
            return 1;
        }

        public int GetMaxPropertiesCategrie()
        {
            return objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT Max(id) FROM PropertiesCategorie"));
        }

        public int GetMaxProperties()
        {
            return objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT Max(id) FROM Properties"));
        }

        public DataTable GetPropertiesCategoriesBySkinId(string skinid)
        {
            string Sql = "SELECT * FROM PropertiesCategorie WHERE SkinId='" + skinid + "' order by id";
            return AccessFactory.conn.DataTable(Sql);
        }
    }
}
