using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class ResourcesHandlers 
    {

        public int GetMaxResourceId()
        {
            return DataBaseFactory.ResourceHandlers.GetMaxResourceId();
        }

        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int CreateResources(Resources inf)
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

            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            //资讯分类不存在
            if (string.IsNullOrEmpty(inf.vcFilePath))
            {
                return -1000000045;
            }

            return DataBaseFactory.ResourceHandlers.CreateResources(inf);
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

            return DataBaseFactory.ResourceHandlers.UpdateResources(inf);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceid"></param>
        /// <returns></returns>
        public Resources GetResourcesById(int resourceid)
        {
            DataTable dt =DataBaseFactory.ResourceHandlers.GetResourcesById(resourceid);
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;

            return (Resources)HandlersFactory.GetEntityObjectFromRow(dt.Rows[0], typeof(Resources));
        }

        public Dictionary<string, EntityBase> GetDelNewsInfoList(string ids)
        {
            DataTable dt = DataBaseFactory.ResourceHandlers.GetDelNewsInfoList(ids);
            
            Dictionary<string, EntityBase> res = null;
            if (dt != null && dt.Rows.Count == 1)
            {
                res = new Dictionary<string, EntityBase>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EntityBase resource = HandlersFactory.GetEntityObjectFromRow(dt.Rows[i], typeof(Resources));
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
            return HandlersFactory.GetEntitysObjectFromTable(DataBaseFactory.ResourceHandlers.GetAllResuresFromDataBase(), typeof(Resources));
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
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("SELECT ");

            if (nums > 0) sqlsb.Append(" TOP " + nums.ToString() + " ");

            sqlsb.Append(" * FROM Resources WHERE ");

            sqlsb.Append(this.GetTagResourceCondition(categories, Speciality, check, del, create, havechilecategorie));

            if (!string.IsNullOrEmpty(orders)) sqlsb.Append(" ORDER BY " + orders);

            DataTable dt = DataBaseFactory.ResourceHandlers.GetResourcesList(sqlsb.ToString());
            Dictionary<string, EntityBase> res = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                res = new Dictionary<string, EntityBase>();
                res = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(Resources));
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
            sqlsb.Append("ID>0 ");
            if (check) { sqlsb.Append(" AND cChecked='Y' "); } else { sqlsb.Append(" AND cChecked='N'"); }

            if (del) { sqlsb.Append(" AND cDel='Y' "); } else { sqlsb.Append(" AND cDel='N' "); }

            if (create) { sqlsb.Append(" AND cCreated='Y' "); } else { sqlsb.Append(" AND cCreated='N' "); }

            if (!string.IsNullOrEmpty(Speciality))
            {
                if (Speciality.IndexOf(",") > -1)
                {
                   
                    string[] aaa = Speciality.Split(',');
                    string text323 = string.Empty;
                    for (int n = 0; n < aaa.Length; n++)
                    {
                        if (!string.IsNullOrEmpty(aaa[n]))
                        {
                            string text = n == 0 ? "" : " OR ";
                            text323 += text + " vcSpeciality like '%" + aaa[n] + "%'";
                        }
                    }
                    if (!string.IsNullOrEmpty(text323))
                    {
                        sqlsb.Append(" AND (");
                        sqlsb.Append(text323);
                        sqlsb.Append(" )");
                    }
                }
                else
                {
                    sqlsb.Append(" AND vcSpeciality like '%" + Speciality + "%' ");
                }
            }


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
                                text1 += text3 + HandlersFactory.categoriesHandlers.GetCategoriesChild(cates[i].Replace("'", ""));
                            }
                        }

                        if (text1.Length >= 36)
                        {
                            sqlsb.Append(" AND iClassID in (" + text1 + ") ");
                        }
                    }
                    else
                    {
                        sqlsb.Append(" AND iClassID in (" + HandlersFactory.categoriesHandlers.GetCategoriesChild(categories.Replace("'", "")) + ") ");
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


            DataTable dt = DataBaseFactory.ResourceHandlers.GetResourcesListPager(ref curPage, ref pageCount, ref count, page, pagesize, order, strCondition);

            if (dt != null && dt.Rows.Count > 0)
            {
                res = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(Resources));
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
        /// 救回或删除资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int SaveOrDelResource(string ids, string action, Admin adminname)
        {

            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(adminname);
            if (rtn < 0) return rtn;

            //尚未选择资源
            if (string.IsNullOrEmpty(ids))
            {
                return -1000000501;
            }


            return DataBaseFactory.ResourceHandlers.SaveOrDelResource(ids,action);
        }

        /// <summary>
        /// 获取分类下最新的一篇文章
        /// </summary>
        /// <param name="ategorie"></param>
        /// <returns></returns>
        public Resources GetNewsResourcesAtCategorie(string ategorie)
        {
            DataTable dt = DataBaseFactory.ResourceHandlers.GetNewsResourcesAtCategorie(ategorie);
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;

            return (Resources)HandlersFactory.GetEntityObjectFromRow(dt.Rows[0], typeof(Resources));
        }


        public int ResourcePropertiesManage(Admin admin, ResourceProperties cp)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            return DataBaseFactory.ResourceHandlers.ResourcePropertiesManage(cp);
        }


        public Dictionary<string, EntityBase> GetResourcePropertiesByRIdEntity(string rid)
        {

            Dictionary<string, EntityBase> res = null;

            DataTable dt = DataBaseFactory.ResourceHandlers.GetResourcePropertiesByRIdEntity(rid);

            if (dt != null && dt.Rows.Count > 0)
            {
                res = HandlersFactory.GetEntitysObjectFromTable(dt, typeof(ResourceProperties));
            }

            return res;
        }

        public int DelResourcesProperties(string resid)
        {
            return DataBaseFactory.ResourceHandlers.DelResourcesProperties(resid);
        }

        public int DelResourcesPropertiesOnIds(string resid,string ids)
        {
            return DataBaseFactory.ResourceHandlers.DelResourcesPropertiesOnIds(resid, ids);
        }
        
    }
}
