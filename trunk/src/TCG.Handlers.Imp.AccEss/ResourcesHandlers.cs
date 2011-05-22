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

            inf.dAddDate = DateTime.Now;

            //资讯标题不能为空
            if (string.IsNullOrEmpty(inf.vcTitle))
            {
                return -1000000039;
            }

            if (string.IsNullOrEmpty(inf.vcAuthor)) inf.vcAuthor = inf.vcEditor;

            //资讯编辑者不能为空
            if (string.IsNullOrEmpty(inf.vcEditor))
            {
                return -1000000041;
            }

            //资讯分类不能为空
            if (string.IsNullOrEmpty(inf.Categorie.Id))
            {
                return -1000000056;
            }

            //资讯关键字不能为空
            if (string.IsNullOrEmpty(inf.vcKeyWord))
            {
                return -1000000043;
            }

            inf.Id = objectHandlers.ToString
                (objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT max(iid) FROM resources")) + 1);

            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            //资讯分类不存在
            if (string.IsNullOrEmpty(inf.vcFilePath))
            {
                return -1000000045;
            }

            AccessFactory.conn.Execute("INSERT INTO resources (iid,iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iCount,vcKeyWord,"
        + "vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,cDel,cPostByUser,"
        + "vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong,SheifUrl) VALUES(" + inf.Id + ",'" + inf.Categorie.Id + "','" + inf.vcTitle + "','"
        + inf.vcUrl + "','" + inf.vcContent.Replace("'", "''") + "','" + inf.vcAuthor + "','" + inf.iCount + "','" + inf.vcKeyWord + "','" + inf.vcEditor + "','" + inf.cCreated + "','"
        + inf.vcSmallImg + "','" + inf.vcBigImg + "','" + inf.vcShortContent.Replace("'", "''") + "','" + inf.vcSpeciality + "','" + inf.cChecked + "','"
        + inf.cDel + "','" + inf.cPostByUser + "','" + inf.vcFilePath + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','"
        + inf.vcTitleColor + "','" + inf.cStrong + "','" + inf.SheifUrl + "')");
            return 1;

        }

        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int CreateResourcesForSheif(Resources inf)
        {

            inf.dAddDate = DateTime.Now;

            //资讯标题不能为空
            if (string.IsNullOrEmpty(inf.vcTitle))
            {
                return -1000000039;
            }

            if (string.IsNullOrEmpty(inf.vcAuthor)) inf.vcAuthor = inf.vcEditor;

            //资讯编辑者不能为空
            if (string.IsNullOrEmpty(inf.vcEditor))
            {
                return -1000000041;
            }

            //资讯分类不能为空
            if (string.IsNullOrEmpty(inf.Categorie.Id))
            {
                return -1000000056;
            }

            //资讯关键字不能为空
            if (string.IsNullOrEmpty(inf.vcKeyWord))
            {
                return -1000000043;
            }

            //修改文章的ID不能为0
            if(string.IsNullOrEmpty(inf.Id)||inf.Id.Trim()=="0")
            {
                return -1000000046;
            }

            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            //资讯分类不存在
            if (string.IsNullOrEmpty(inf.vcFilePath))
            {
                return -1000000045;
            }

            AccessFactory.conn.Execute("UPDATE resources SET iClassID='" + inf.Categorie.Id + "',vcTitle='" + inf.vcTitle + "',vcUrl='"
                + inf.vcUrl + "',vcContent='" + inf.vcContent.Replace("'", "''") + "',vcAuthor='" + inf.vcAuthor + "',iCount='"
                + inf.iCount + "',vcKeyWord='" + inf.vcKeyWord + "',vcEditor='" + inf.vcEditor + "',cCreated='"
                + inf.cCreated + "',vcSmallImg='" + inf.vcSmallImg + "',vcBigImg='" + inf.vcBigImg + "',vcShortContent='"
                + inf.vcShortContent.Replace("'", "''") + "',vcSpeciality='" + inf.vcSpeciality + "',cChecked='" + inf.cChecked + "',cDel='"
                + inf.cDel + "',cPostByUser='" + inf.cPostByUser + "',vcFilePath='" + inf.vcFilePath
                + "',dUpDateDate=now(),vcTitleColor = '" + inf.vcTitleColor + "',cStrong = '"
                + inf.cStrong + " WHERE iId = " + inf.Id);


            return 1;
        }

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int UpdateResources(Resources inf)
        {

            //资讯标题不能为空
            if (string.IsNullOrEmpty(inf.vcTitle))
            {
                return -1000000039;
            }

            if (string.IsNullOrEmpty(inf.vcAuthor)) inf.vcAuthor = inf.vcEditor;

            //资讯编辑者不能为空
            if (string.IsNullOrEmpty(inf.vcEditor))
            {
                return -1000000041;
            }

            //资讯分类不能为空
            if (string.IsNullOrEmpty(inf.Categorie.Id))
            {
                return -1000000056;
            }

            //资讯关键字不能为空
            if (string.IsNullOrEmpty(inf.vcKeyWord))
            {
                return -1000000043;
            }

            //修改文章的ID不能为0
            if (string.IsNullOrEmpty(inf.Id) || inf.Id.Trim() == "0")
            {
                return -1000000046;
            }

            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            //资讯分类不存在
            if (string.IsNullOrEmpty(inf.vcFilePath))
            {
                return -1000000045;
            }

            string sql = "UPDATE resources SET iClassID='" + inf.Categorie.Id + "',vcTitle='" + inf.vcTitle + "',vcUrl='"
                + inf.vcUrl + "',vcContent='" + inf.vcContent.Replace("'", "''") + "',vcAuthor='" + inf.vcAuthor + "',iCount='"
                + inf.iCount + "',vcKeyWord='" + inf.vcKeyWord + "',vcEditor='" + inf.vcEditor + "',cCreated='"
                + inf.cCreated + "',vcSmallImg='" + inf.vcSmallImg + "',vcBigImg='" + inf.vcBigImg + "',vcShortContent='"
                + inf.vcShortContent.Replace("'", "''") + "',vcSpeciality='" + inf.vcSpeciality + "',cChecked='" + inf.cChecked + "',cDel='"
                + inf.cDel + "',cPostByUser='" + inf.cPostByUser + "',vcFilePath='" + inf.vcFilePath
                + "',dUpDateDate=now(),vcTitleColor = '" + inf.vcTitleColor + "',cStrong = '"
                + inf.cStrong + "' WHERE iId = " + inf.Id;
            AccessFactory.conn.Execute(sql);

            return 1;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceid"></param>
        /// <returns></returns>
        public Resources GetResourcesById(int resourceid)
        {

            DataTable dt = AccessFactory.conn.DataTable("SELECT * FROM Resources WHERE iID = " + resourceid.ToString().Trim() + "");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;

            return (Resources)AccessFactory.GetEntityObjectFromRow(dt.Rows[0], typeof(Resources));
        }



        public Dictionary<string, EntityBase> GetDelNewsInfoList(string ids)
        {
            AccessFactory.conn.Execute("UPDATE resources SET cDel = 'Y',cCreated = 'N' WHERE iId IN (" + ids + ")");

            DataSet ds = AccessFactory.conn.DataSet("SELECT * FROM resources WHERE iId IN (" + ids + ")");
            Dictionary<string, EntityBase> res = null;
            if (ds != null && ds.Tables.Count == 1)
            {
                res = new Dictionary<string, EntityBase>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EntityBase resource = AccessFactory.GetEntityObjectFromRow(ds.Tables[0].Rows[i], typeof(Resources));
                    res.Add(resource.Id, resource);
                }
            }
            return res;
        }


        /// <summary>
        /// 批量删除资源文件
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelNewsInfoHtmlByIds(string ids)
        {

            if (string.IsNullOrEmpty(ids)) return -19000000;

            Dictionary<string, EntityBase> res = GetDelNewsInfoList(ids);

            if (res == null) return -19000000;
            foreach (KeyValuePair<string, EntityBase> entity in res)
            {
                Resources restemp = (Resources)entity.Value;
                string filepath = HttpContext.Current.Server.MapPath("~" + restemp.vcFilePath);
                try
                {
                    System.IO.File.Delete(filepath);
                }
                catch { }
            }
            return 1;
        }


        /// <summary>
        /// 获取所有的文章咨询,并放入内存中,不要轻易调用,将消耗大量的时间
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllResurces()
        {

            Dictionary<string, EntityBase> resurceses = GetAllResuresFromDataBase();
            return resurceses;
        }

        public Dictionary<string, EntityBase> GetAllResuresFromDataBase()
        {

            DataTable dt = AccessFactory.conn.DataTable("SELECT * FROM Resources ");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            return AccessFactory.GetEntitysObjectFromTable(dt, typeof(Resources));
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
        public Dictionary<string, EntityBase> GetResourcesList(int nums, string categories, string Speciality, string orders, bool check, bool del, bool create, bool havechilecategorie)
        {
            Dictionary<string, EntityBase> res = null;

            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("SELECT ");

            if (nums > 0) sqlsb.Append(" TOP " + nums.ToString() + " ");

            sqlsb.Append(" * FROM Resources WHERE ");

            sqlsb.Append(this.GetTagResourceCondition(categories, Speciality, check, del, create, havechilecategorie));

            if (!string.IsNullOrEmpty(orders)) sqlsb.Append(" ORDER BY " + orders);

            DataTable dt = AccessFactory.conn.DataTable(sqlsb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                res = AccessFactory.GetEntitysObjectFromTable(dt, typeof(Resources));
            }

            return res;
        }

        /// <summary>
        /// 获得标签中文章的搜索条件
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="Speciality"></param>
        /// <param name="check"></param>
        /// <param name="del"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public string GetTagResourceCondition(string categories, string Speciality, bool check, bool del, bool create, bool havechilecategorie)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("iID>0 ");
            if (check) { sqlsb.Append(" AND cChecked='Y' "); } else { sqlsb.Append(" AND cChecked='N'"); }

            if (del) { sqlsb.Append(" AND cDel='Y' "); } else { sqlsb.Append(" AND cDel='N' "); }

            if (create) { sqlsb.Append(" AND cCreated='Y' "); } else { sqlsb.Append(" AND cCreated='N' "); }

            if (!string.IsNullOrEmpty(categories))
            {
                if (havechilecategorie)
                {
                    if (categories.IndexOf(',') > -1)
                    {
                        string[] cates = categories.Split(',');

                        string text1 = string.Empty;
                        for (int i = 0; i < cates.Length; i++)
                        {
                            if (cates[i].Trim().Length == 36)
                            {
                                string text3 = text1.Length == 0 ? "" : ",";
                                text1 += text3 + AccessFactory.categoriesHandlers.GetCategoriesChild(cates[i].Replace("'", ""));
                            }
                        }

                        if (text1.Length >= 36)
                        {
                            sqlsb.Append(" AND iClassID in (" + text1 + ") ");
                        }
                    }
                    else
                    {
                        sqlsb.Append(" AND iClassID in (" + AccessFactory.categoriesHandlers.GetCategoriesChild(categories.Replace("'", "")) + ") ");
                    }
                }
                else
                {
                    sqlsb.Append(" AND iClassID =  '" + categories + "'");
                }
            }

            return sqlsb.ToString();
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
        public Dictionary<string, EntityBase> GetResourcesListPager(ref int curPage, ref int pageCount, ref int count, int page, int pagesize, string order, string strCondition)
        {
            Dictionary<string, EntityBase> res = null;


            DataTable dt = AccessFactory.conn.ExecutePager(curPage, pagesize, " * ", "Resources", strCondition, order, out pageCount, out count);

            if (dt != null && dt.Rows.Count >0)
            {
                res = AccessFactory.GetEntitysObjectFromTable(dt, typeof(Resources));
            }

            return res;
        }

        /// <summary>
        /// 生成文章路径
        /// </summary>
        /// <param name="extion"></param>
        /// <param name="title"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string CreateNewsInfoFilePath(Resources nif)
        {
            if (string.IsNullOrEmpty(nif.Id)) nif.Id = Guid.NewGuid().ToString();
            string text = string.Empty;
            text += nif.Categorie.vcDirectory;
            text += nif.dAddDate.Year.ToString() + objectHandlers.AddZeros(nif.dAddDate.Month.ToString(), 2);
            text += objectHandlers.AddZeros(nif.dAddDate.Day.ToString(), 2) + "/";
            text += nif.Id + ConfigServiceEx.baseConfig["FileExtension"];
            return text;
        }


        /// <summary>
        /// 就会或删除资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int SaveOrDelResource(string ids, string action, Admin adminname)
        {

            int rtn = AccessFactory.adminHandlers.CheckAdminPower(adminname);
            if (rtn < 0) return rtn;

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
        public Resources GetNewsResourcesAtCategorie(string ategorie)
        {
            DataTable dt = AccessFactory.conn.DataTable("SELECT TOP 1 * FROM Resources WHERE iClassID = '" + ategorie + "'");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;

            return (Resources)AccessFactory.GetEntityObjectFromRow(dt.Rows[0], typeof(Resources));
        }


        public int ResourcePropertiesManage(Admin admin, ResourceProperties cp)
        {
            int rtn = AccessFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            string sql = string.Empty;
            if (string.IsNullOrEmpty(cp.Id))
            {
                sql = "INSERT INTO ResourceProperties(ResourceId,PropertieName,PropertieValue,CategoriePropertieId) VALUES("
                + "'" + cp.ResourceId + "','" + cp.PropertieName + "','" + cp.PropertieValue + "','" + cp.CategoriePropertieId + "')";
            }
            else
            {
                int ncount = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM ResourceProperties WHERE ResourceId = '"
                    + cp.ResourceId + "' AND CategoriePropertieId='" + cp.CategoriePropertieId + "'"));
                if (ncount > 0)
                {
                    sql = "UPDATE ResourceProperties SET ResourceId='" + cp.ResourceId + "',PropertieName='" + cp.PropertieName + "',PropertieValue='" + cp.PropertieValue
                        + "',CategoriePropertieId='" + cp.CategoriePropertieId + "' WHERE id=" + cp.Id;
                }
                else
                {
                    sql = "INSERT INTO ResourceProperties(ResourceId,PropertieName,PropertieValue,CategoriePropertieId) VALUES("
                 + "'" + cp.ResourceId + "','" + cp.PropertieName + "','" + cp.PropertieValue + "','" + cp.CategoriePropertieId + "')";
                }
            }

            AccessFactory.conn.Execute(sql);
            return 1;
        }


        public Dictionary<string, EntityBase> GetResourcePropertiesByRIdEntity(string rid)
        {
            Dictionary<string, EntityBase> res = null;


            DataTable dt = AccessFactory.conn.DataTable("SELECT * FROM ResourceProperties WHERE ResourceId='" + rid + "'");

            if (dt != null && dt.Rows.Count > 0)
            {
                res = AccessFactory.GetEntitysObjectFromTable(dt, typeof(ResourceProperties));
            }

            return res;
        }
        
    }
}
