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
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;

using System.Text;
using System.Text.RegularExpressions;


using TCG.Data;
using TCG.Utils;
using TCG.Entity;


namespace TCG.Handlers
{
    public class ResourcesHandlers : ManageObjectHandlersBase
    {
        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int CreateResources(Resources inf)
        {
            base.SetDataBaseConnection();
            inf.dAddDate = DateTime.Now;

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp0.Value = inf.Categorie.Id;
            SqlParameter sp1 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp1.Value = inf.vcTitle;
            SqlParameter sp2 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 200); sp2.Value = inf.vcUrl;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp3.Value = inf.vcContent;
            SqlParameter sp4 = new SqlParameter("@vcAuthor", SqlDbType.VarChar, 50); sp4.Value = inf.vcAuthor;
            SqlParameter sp5 = new SqlParameter("@vcKeyWord", SqlDbType.VarChar, 100); sp5.Value = inf.vcKeyWord;
            SqlParameter sp6 = new SqlParameter("@vcEditor", SqlDbType.VarChar, 50); sp6.Value = inf.vcEditor;
            SqlParameter sp7 = new SqlParameter("@vcSmallImg", SqlDbType.VarChar, 255); sp7.Value = inf.vcSmallImg;
            SqlParameter sp8 = new SqlParameter("@vcBigImg", SqlDbType.VarChar, 255); sp8.Value = inf.vcBigImg;
            SqlParameter sp9 = new SqlParameter("@vcShortContent", SqlDbType.VarChar, 500); sp9.Value = inf.vcShortContent;
            SqlParameter sp10 = new SqlParameter("@vcSpeciality", SqlDbType.VarChar, 100); sp10.Value = inf.vcSpeciality;
            SqlParameter sp11 = new SqlParameter("@cChecked", SqlDbType.Char, 1); sp11.Value = inf.cChecked;
            SqlParameter sp12 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp12.Direction = ParameterDirection.Output;;
            SqlParameter sp13 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp13.Direction = ParameterDirection.Output;
            SqlParameter sp14 = new SqlParameter("@iIDOut", SqlDbType.Int, 4); sp14.Direction = ParameterDirection.Output;
            SqlParameter sp15 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp15.Value = base.configService.baseConfig["FileExtension"];
            SqlParameter sp16 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp16.Value = inf.cCreated;
            SqlParameter sp17 = new SqlParameter("@vcTitleColor", SqlDbType.VarChar, 10); sp17.Value = inf.vcTitleColor;
            SqlParameter sp18 = new SqlParameter("@cStrong", SqlDbType.Char, 1); sp18.Value = inf.cStrong;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18}, new int[] {12, 13 ,14});
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[1], typeof(int));
                inf.Id = reValues[2].Trim();
                inf.vcFilePath = reValues[0];
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int CreateResourcesForSheif(Resources inf)
        {
            base.SetDataBaseConnection();
            inf.dAddDate = DateTime.Now;

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp0.Value = inf.Categorie.Id;
            SqlParameter sp1 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp1.Value = inf.vcTitle;
            SqlParameter sp2 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 200); sp2.Value = inf.vcUrl;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp3.Value = inf.vcContent;
            SqlParameter sp4 = new SqlParameter("@vcAuthor", SqlDbType.VarChar, 50); sp4.Value = inf.vcAuthor;
            SqlParameter sp5 = new SqlParameter("@vcKeyWord", SqlDbType.VarChar, 100); sp5.Value = inf.vcKeyWord;
            SqlParameter sp6 = new SqlParameter("@vcEditor", SqlDbType.VarChar, 50); sp6.Value = inf.vcEditor;
            SqlParameter sp7 = new SqlParameter("@vcSmallImg", SqlDbType.VarChar, 255); sp7.Value = inf.vcSmallImg;
            SqlParameter sp8 = new SqlParameter("@vcBigImg", SqlDbType.VarChar, 255); sp8.Value = inf.vcBigImg;
            SqlParameter sp9 = new SqlParameter("@vcShortContent", SqlDbType.VarChar, 500); sp9.Value = inf.vcShortContent;
            SqlParameter sp10 = new SqlParameter("@vcSpeciality", SqlDbType.VarChar, 100); sp10.Value = inf.vcSpeciality;
            SqlParameter sp11 = new SqlParameter("@cChecked", SqlDbType.Char, 1); sp11.Value = inf.cChecked;
            SqlParameter sp12 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp12.Direction = ParameterDirection.Output;
            SqlParameter sp13 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp13.Direction = ParameterDirection.Output;
            SqlParameter sp14 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp14.Value = base.configService.baseConfig["FileExtension"];
            SqlParameter sp15 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp15.Value = inf.cCreated;
            SqlParameter sp16 = new SqlParameter("@cShief", SqlDbType.Char, 2); sp16.Value = "02";
            SqlParameter sp17 = new SqlParameter("@iIDOut", SqlDbType.Int, 4); sp17.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp16,sp17}, new int[] { 12,13 ,17});
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[1], typeof(int));
                inf.Id = reValues[2].Trim();
                inf.vcFilePath = reValues[0];
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int UpdateResources(Resources inf)
        {
            base.SetDataBaseConnection();

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp0.Value = inf.Categorie.Id;
            SqlParameter sp1 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp1.Value = inf.vcTitle;
            SqlParameter sp2 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 200); sp2.Value = inf.vcUrl;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp3.Value = inf.vcContent;
            SqlParameter sp4 = new SqlParameter("@vcAuthor", SqlDbType.VarChar, 50); sp4.Value = inf.vcAuthor;
            SqlParameter sp6 = new SqlParameter("@vcKeyWord", SqlDbType.VarChar, 100); sp6.Value = inf.vcKeyWord;
            SqlParameter sp7 = new SqlParameter("@vcEditor", SqlDbType.VarChar, 50); sp7.Value = inf.vcEditor;
            SqlParameter sp8 = new SqlParameter("@vcSmallImg", SqlDbType.VarChar, 255); sp8.Value = inf.vcSmallImg;
            SqlParameter sp9 = new SqlParameter("@vcBigImg", SqlDbType.VarChar, 255); sp9.Value = inf.vcBigImg;
            SqlParameter sp10 = new SqlParameter("@vcShortContent", SqlDbType.VarChar, 500); sp10.Value = inf.vcShortContent;
            SqlParameter sp11 = new SqlParameter("@vcSpeciality", SqlDbType.VarChar, 100); sp11.Value = inf.vcSpeciality;
            SqlParameter sp12 = new SqlParameter("@cChecked", SqlDbType.Char, 1); sp12.Value = inf.cChecked;
            SqlParameter sp13 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp13.Direction = ParameterDirection.Output;
            SqlParameter sp14 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp14.Value = base.configService.baseConfig["FileExtension"];
            SqlParameter sp15 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp15.Value = inf.cCreated;
            SqlParameter sp16 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp16.Value = "02";
            SqlParameter sp17 = new SqlParameter("@iId", SqlDbType.VarChar, 36); sp17.Value = inf.Id;
            SqlParameter sp18 = new SqlParameter("@vcTitleColor", SqlDbType.VarChar, 10); sp18.Value = inf.vcTitleColor;
            SqlParameter sp19 = new SqlParameter("@cStrong", SqlDbType.Char, 1); sp19.Value = inf.cStrong;
            SqlParameter sp20 = new SqlParameter("@iCount", SqlDbType.Int, 4); sp20.Value = inf.iCount;
            SqlParameter sp21 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp21.Direction = ParameterDirection.Output;
            SqlParameter sp22 = new SqlParameter("@iIDOut", SqlDbType.Int, 4); sp22.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18,sp19,sp20,sp21,sp22}, new int[] { 20 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));

                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceid"></param>
        /// <returns></returns>
        public Resources GetResourcesById(int resourceid)
        {
            base.SetDataBaseConnection();
            DataTable dt = base.conn.GetDataTable("SELECT * FROM Resources (NOLOCK) WHERE iID = " + resourceid.ToString().Trim() + "");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;

            return (Resources)base.GetEntityObjectFromRow(dt.Rows[0], typeof(Resources));
        }

         

        public Dictionary<string, EntityBase> GetDelNewsInfoList(string ids)
        {
            SqlParameter sp1 = new SqlParameter("@ids", SqlDbType.NVarChar, 100); sp1.Value = ids;
            SqlParameter sp2 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp2.Direction = ParameterDirection.Output;

            DataSet ds = base.conn.GetDataSet("SP_News_DelResourcesByIdS", new SqlParameter[] { sp1, sp2 });
            Dictionary<string, EntityBase> res = null;
            if (ds != null && ds.Tables.Count == 1)
            {
                res = new Dictionary<string, EntityBase>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EntityBase resource = base.GetEntityObjectFromRow(ds.Tables[0].Rows[i], typeof(Resources));
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
            base.SetDataBaseConnection();
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
            base.SetDataBaseConnection();
            Dictionary<string, EntityBase> resurceses = GetAllResuresFromDataBase();         
            return resurceses;
        }

        public Dictionary<string, EntityBase> GetAllResuresFromDataBase()
        {
            base.SetDataBaseConnection();
            DataTable dt = base.conn.GetDataTable("SELECT * FROM Resources (NOLOCK) ");
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            return base.GetEntitysObjectFromTable(dt, typeof(Resources));
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
        public Dictionary<string, EntityBase> GetResourcesList(int nums, string categories, string Speciality, string orders, bool check, bool del, bool create,bool havechilecategorie)
        {
            Dictionary<string, EntityBase> res = null;
            base.SetDataBaseConnection();
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("SELECT ");

            if (nums > 0) sqlsb.Append(" TOP " + nums.ToString() + " ");

            sqlsb.Append(" * FROM Resources (NOLOCK) WHERE ");

            sqlsb.Append(this.GetTagResourceCondition(categories, Speciality, check, del, create, havechilecategorie));

            if (!string.IsNullOrEmpty(orders)) sqlsb.Append(" ORDER BY " + orders);

            DataTable  dt = base.conn.GetDataTable(sqlsb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                res = base.GetEntitysObjectFromTable(dt, typeof(Resources));
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
                                text1 += text3 + base.handlerService.skinService.categoriesHandlers.GetCategoriesChild(cates[i].Replace("'", ""));
                            }
                        }

                        if (text1.Length >= 36)
                        {
                            sqlsb.Append(" AND iClassID in (" + text1 + ") ");
                        }
                    }
                    else
                    {
                        sqlsb.Append(" AND iClassID in (" + base.handlerService.skinService.categoriesHandlers.GetCategoriesChild(categories.Replace("'", "")) + ") ");
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
        public Dictionary<string, EntityBase> GetResourcesListPager(ref int curPage, ref int pageCount,ref int count, int page, int pagesize, string order, string strCondition)
        {
            Dictionary<string, EntityBase> res = null;
            base.SetDataBaseConnection();

            PageSearchItem sItem = new PageSearchItem();
            sItem.tableName = "Resources";

            ArrayList arrshowfied = new ArrayList();
            arrshowfied.Add("*");
            sItem.arrShowField = arrshowfied;

            ArrayList arrsortfield = new ArrayList();
            arrsortfield.Add(order);
            sItem.arrSortField = arrsortfield;

            sItem.page = page;
            sItem.pageSize = pagesize;

            sItem.strCondition = strCondition;

            DataSet ds = new DataSet();
            int rtn = DBHandlers.GetPage(sItem, base.conn, ref curPage, ref pageCount, ref count, ref ds);

            if (rtn == 1 && ds != null && ds.Tables.Count == 1)
            {
                res = base.GetEntitysObjectFromTable(ds.Tables[0], typeof(Resources));
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
            text += nif.Id + base.configService.baseConfig["FileExtension"];
            return text;
        }


        /// <summary>
        /// 就会或删除资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int SaveOrDelResource(string ids,string action,string adminname)
        {
            base.SetDataBaseConnection();
            SqlParameter sp1 = new SqlParameter("@ids", SqlDbType.NVarChar, 100); sp1.Value = ids;
            SqlParameter sp2 = new SqlParameter("@Action", SqlDbType.NVarChar, 10); sp2.Value = action;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp3.Direction = ParameterDirection.Output;

            SqlParameter sp4 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp4.Value = adminname;
            SqlParameter sp5 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp5.Value = objectHandlers.GetIP();

            string[] reValues = conn.Execute("SP_News_SaveOrDelResource", new SqlParameter[] { sp1, sp2, sp3 , sp4 , sp5}, new int[] { 2 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));

                return rtn;
            }

            return -19000000;
        }


        public int CreateResourcHtmlById(ref string errText, int id, TCGTagHandlers tcgth)
        {
            if (id <= 0) return -1000000308;

            Resources item = this.GetResourcesById(id);
            if (item == null) return -1000000309;

            if (item.vcUrl.IndexOf(".") > -1) return -1000000310;

            int rtn = 0;
            tcgth.Template = item.Categorie.ResourceTemplate.Content.Replace("_$Id$_", id.ToString());
            tcgth.FilePath =  HttpContext.Current.Server.MapPath("~" + item.vcFilePath);
            tcgth.WebPath = item.vcFilePath;
            tcgth.configService = base.configService;
            tcgth.conn = base.conn;

            string text1 = string.Empty;

            if (tcgth.Replace())
            {
                if (item.cChecked != "Y")
                {
                    item.cChecked = "Y";
                    rtn = base.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
                }
            }
            else
            {
                rtn = -1000000311;
            }

            return rtn;
        }
    }
}