using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Handlers;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers.Imp.AccEss
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

            AccessFactory.conn.Execute("INSERT INTO resources (iid,iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iCount,vcKeyWord,"
        + "vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,cDel,cPostByUser,"
        + "vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong,SheifUrl,PropertiesCategorieId) VALUES(" + inf.Id + ",'" + inf.Categorie.Id + "','" + inf.vcTitle + "','"
        + inf.vcUrl + "','" + inf.vcContent.Replace("'", "''") + "','" + inf.vcAuthor + "','" + inf.iCount + "','" + inf.vcKeyWord + "','" + inf.vcEditor + "','" + inf.cCreated + "','"
        + inf.vcSmallImg + "','" + inf.vcBigImg + "','" + inf.vcShortContent.Replace("'", "''") + "','" + inf.vcSpeciality + "','" + inf.cChecked + "','"
        + inf.cDel + "','" + inf.cPostByUser + "','" + inf.vcFilePath + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','"
        + inf.vcTitleColor + "','" + inf.cStrong + "','" + inf.SheifUrl + "','" + inf.PropertiesCategorieId + "')");
            return 1;

        }

        public int GetMaxResourceId()
        {
            return objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT max(iid) FROM resources"));
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
                + "',dUpDateDate=now(),vcTitleColor = '" + inf.vcTitleColor + "',cStrong = '"
                + inf.cStrong + "',PropertiesCategorieId='" + inf.PropertiesCategorieId + "' WHERE iId = " + inf.Id;
            AccessFactory.conn.Execute(sql);

            return 1;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceid"></param>
        /// <returns></returns>
        public DataTable GetResourcesById(int resourceid)
        {

            DataTable dt = AccessFactory.conn.DataTable("SELECT * FROM Resources WHERE iID = " + resourceid.ToString().Trim() + "");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;

            return dt;
        }



        public DataTable GetDelNewsInfoList(string ids)
        {
            AccessFactory.conn.Execute("UPDATE resources SET cDel = 'Y',cCreated = 'N' WHERE iId IN (" + ids + ")");

            DataSet ds = AccessFactory.conn.DataSet("SELECT * FROM resources WHERE iId IN (" + ids + ")");

            if (ds != null && ds.Tables.Count == 1)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetAllResuresFromDataBase()
        {

            DataTable dt = AccessFactory.conn.DataTable("SELECT * FROM Resources ");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            return dt;
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
            return AccessFactory.conn.DataTable(sqlsb);
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
           return AccessFactory.conn.ExecutePager(curPage, pagesize, " * ", "Resources", strCondition, order, out pageCount, out count);
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

            if (action == "SAVE")
            {
                AccessFactory.conn.Execute("UPDATE resources SET cDel = ''N'' WHERE iId IN (" + ids + ")");
            }

            if (action == "DEL")
            {
                AccessFactory.conn.Execute("DELETE FROM resources WHERE iId IN (" + ids + ")");
            }

            return 1;
        }

        /// <summary>
        /// 获取分类下最新的一篇文章
        /// </summary>
        /// <param name="ategorie"></param>
        /// <returns></returns>
        public DataTable GetNewsResourcesAtCategorie(string ategorie)
        {
            return AccessFactory.conn.DataTable("SELECT TOP 1 * FROM Resources WHERE iClassID = '" + ategorie + "'");
        }


        public int ResourcePropertiesManage(ResourceProperties cp)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO ResourceProperties(ResourceId,PropertieName,PropertieValue,PropertieId,iOrder) VALUES("
                + "'" + cp.ResourceId + "','" + cp.PropertieName + "','" + cp.PropertieValue + "','" + cp.PropertieId + "'," + cp.iOrder + ")";
            }
            else
            {
                int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM ResourceProperties WHERE ResourceId = '"
                    + cp.ResourceId + "' AND PropertieId='" + cp.PropertieId + "'"));
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

            AccessFactory.conn.Execute(sql);
            return 1;
        }


        public DataTable GetResourcePropertiesByRIdEntity(string rid)
        {
            return AccessFactory.conn.DataTable("SELECT * FROM ResourceProperties WHERE ResourceId='" + rid + "' order by iOrder");
        }

        public int DelResourcesProperties(string resid)
        {
             AccessFactory.conn.DataTable("DELETE FROM ResourceProperties WHERE ResourceId='" + resid + "'");
             return 1;
        }

        public int DelResourcesPropertiesOnIds(string resid, string ids)
        {
            AccessFactory.conn.DataTable("DELETE FROM ResourceProperties WHERE ResourceId='" + resid + "' and id not in(" + ids + ")");
            return 1;
        }
    }
}
