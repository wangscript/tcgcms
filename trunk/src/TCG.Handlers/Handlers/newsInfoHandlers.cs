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
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;
using TCG.KeyWordSplit;


namespace TCG.Handlers
{
    public class NewsInfoHandlers
    {
        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int AddNewsInfo(Connection conn,string Extension,NewsInfo inf, ref int outid)
        {
            conn.Dblink = DBLinkNums.News;

            inf.dAddDate = DateTime.Now;
            inf.vcFilePath = this.CreateNewsInfoFilePath(Extension, inf);

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.Int, 4); sp0.Value = inf.ClassInfo.iId;
            SqlParameter sp1 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp1.Value = inf.vcTitle;
            SqlParameter sp2 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 200); sp2.Value = inf.vcUrl;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp3.Value = inf.vcContent;
            SqlParameter sp4 = new SqlParameter("@vcAuthor", SqlDbType.VarChar, 50); sp4.Value = inf.vcAuthor;
            SqlParameter sp5 = new SqlParameter("@iFrom", SqlDbType.Int, 4); sp5.Value = inf.FromInfo.iId;
            SqlParameter sp6 = new SqlParameter("@vcKeyWord", SqlDbType.VarChar, 100); sp6.Value = inf.vcKeyWord;
            SqlParameter sp7 = new SqlParameter("@vcEditor", SqlDbType.VarChar, 50); sp7.Value = inf.vcEditor;
            SqlParameter sp8 = new SqlParameter("@vcSmallImg", SqlDbType.VarChar, 255); sp8.Value = inf.vcSmallImg;
            SqlParameter sp9 = new SqlParameter("@vcBigImg", SqlDbType.VarChar, 255); sp9.Value = inf.vcBigImg;
            SqlParameter sp10 = new SqlParameter("@vcShortContent", SqlDbType.VarChar, 500); sp10.Value = inf.vcShortContent;
            SqlParameter sp11 = new SqlParameter("@vcSpeciality", SqlDbType.VarChar, 100); sp11.Value = inf.vcSpeciality;
            SqlParameter sp12 = new SqlParameter("@cChecked", SqlDbType.Char, 1); sp12.Value = inf.cChecked;
            SqlParameter sp13 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp13.Value = inf.vcFilePath;
            SqlParameter sp14 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp14.Direction = ParameterDirection.Output;
            SqlParameter sp15 = new SqlParameter("@iIDOut", SqlDbType.Int, 4); sp15.Direction = ParameterDirection.Output;
            SqlParameter sp16 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp16.Value = Extension;
            SqlParameter sp17 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp17.Value = inf.cCreated;
            SqlParameter sp18 = new SqlParameter("@vcTitleColor", SqlDbType.VarChar, 10); sp18.Value = inf.vcTitleColor;
            SqlParameter sp19 = new SqlParameter("@cStrong", SqlDbType.Char, 1); sp19.Value = inf.cStrong;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18,sp19}, new int[] { 14, 15 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                outid = (int)Convert.ChangeType(reValues[1], typeof(int));
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
        public int AddNewsInfoForSheif(Connection conn, string Extension, NewsInfo inf, ref int outid)
        {
            conn.Dblink = DBLinkNums.News;
            inf.dAddDate = DateTime.Now;
            inf.vcFilePath = this.CreateNewsInfoFilePath(Extension, inf);

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.Int, 4); sp0.Value = inf.ClassInfo.iId;
            SqlParameter sp1 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp1.Value = inf.vcTitle;
            SqlParameter sp2 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 200); sp2.Value = inf.vcUrl;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp3.Value = inf.vcContent;
            SqlParameter sp4 = new SqlParameter("@vcAuthor", SqlDbType.VarChar, 50); sp4.Value = inf.vcAuthor;
            SqlParameter sp5 = new SqlParameter("@iFrom", SqlDbType.Int, 4); sp5.Value = inf.FromInfo.iId;
            SqlParameter sp6 = new SqlParameter("@vcKeyWord", SqlDbType.VarChar, 100); sp6.Value = inf.vcKeyWord;
            SqlParameter sp7 = new SqlParameter("@vcEditor", SqlDbType.VarChar, 50); sp7.Value = inf.vcEditor;
            SqlParameter sp8 = new SqlParameter("@vcSmallImg", SqlDbType.VarChar, 255); sp8.Value = inf.vcSmallImg;
            SqlParameter sp9 = new SqlParameter("@vcBigImg", SqlDbType.VarChar, 255); sp9.Value = inf.vcBigImg;
            SqlParameter sp10 = new SqlParameter("@vcShortContent", SqlDbType.VarChar, 500); sp10.Value = inf.vcShortContent;
            SqlParameter sp11 = new SqlParameter("@vcSpeciality", SqlDbType.VarChar, 100); sp11.Value = inf.vcSpeciality;
            SqlParameter sp12 = new SqlParameter("@cChecked", SqlDbType.Char, 1); sp12.Value = inf.cChecked;
            SqlParameter sp13 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp13.Value = inf.vcFilePath;
            SqlParameter sp14 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp14.Direction = ParameterDirection.Output;
            SqlParameter sp15 = new SqlParameter("@iIDOut", SqlDbType.Int, 4); sp15.Direction = ParameterDirection.Output;
            SqlParameter sp16 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp16.Value = Extension;
            SqlParameter sp17 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp17.Value = inf.cCreated;
            SqlParameter sp18 = new SqlParameter("@cShief", SqlDbType.Char, 2); sp18.Value = "02";
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18}, new int[] { 14, 15 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                outid = (int)Convert.ChangeType(reValues[1], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 更改资源的保存状态
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="IsDel"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelNewsInfosWithLogic(Connection conn, string adminname, string IsDel, string ids)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp2.Value = "01";
            SqlParameter sp3 = new SqlParameter("@ids", SqlDbType.VarChar, 1000); sp3.Value = ids;
            SqlParameter sp4 = new SqlParameter("@vcKey", SqlDbType.VarChar, 30); sp4.Value = "Del";
            SqlParameter sp5 = new SqlParameter("@vcKeValue", SqlDbType.Char, 1); sp5.Value = IsDel;
            SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoStatManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 }, new int[] { 6 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int)); ;
            }
            return -19000000;
        }

        /// <summary>
        /// 更改资源的保存状态
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="IsDel"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int UpdateNewsInfosCreate(Connection conn, string adminname, string IsDel, string ids)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp2.Value = "01";
            SqlParameter sp3 = new SqlParameter("@ids", SqlDbType.VarChar, 1000); sp3.Value = ids;
            SqlParameter sp4 = new SqlParameter("@vcKey", SqlDbType.VarChar, 30); sp4.Value = "Created";
            SqlParameter sp5 = new SqlParameter("@vcKeValue", SqlDbType.Char, 1); sp5.Value = IsDel;
            SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoStatManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 }, new int[] { 6 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int)); ;
            }
            return -19000000;
        }

        /// <summary>
        /// 彻底删除资源
        /// </summary>
        /// <returns></returns>
        public int DelNewsInfosWithPhysics(Connection conn, string adminname,string ids)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp2.Value = "02";
            SqlParameter sp3 = new SqlParameter("@ids", SqlDbType.VarChar, 1000); sp3.Value = ids;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp4.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoStatManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4}, new int[] { 4 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int)); ;
            }
            return -19000000;
        }

        public int UpdateNewsInfo(Connection conn, string Extension, NewsInfo inf, ref int outid)
        {
            conn.Dblink = DBLinkNums.News;
            inf.vcFilePath = this.CreateNewsInfoFilePath(Extension, inf);

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.Int, 4); sp0.Value = inf.ClassInfo.iId;
            SqlParameter sp1 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp1.Value = inf.vcTitle;
            SqlParameter sp2 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 200); sp2.Value = inf.vcUrl;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp3.Value = inf.vcContent;
            SqlParameter sp4 = new SqlParameter("@vcAuthor", SqlDbType.VarChar, 50); sp4.Value = inf.vcAuthor;
            SqlParameter sp5 = new SqlParameter("@iFrom", SqlDbType.Int, 4); sp5.Value = inf.FromInfo.iId;
            SqlParameter sp6 = new SqlParameter("@vcKeyWord", SqlDbType.VarChar, 100); sp6.Value = inf.vcKeyWord;
            SqlParameter sp7 = new SqlParameter("@vcEditor", SqlDbType.VarChar, 50); sp7.Value = inf.vcEditor;
            SqlParameter sp8 = new SqlParameter("@vcSmallImg", SqlDbType.VarChar, 255); sp8.Value = inf.vcSmallImg;
            SqlParameter sp9 = new SqlParameter("@vcBigImg", SqlDbType.VarChar, 255); sp9.Value = inf.vcBigImg;
            SqlParameter sp10 = new SqlParameter("@vcShortContent", SqlDbType.VarChar, 500); sp10.Value = inf.vcShortContent;
            SqlParameter sp11 = new SqlParameter("@vcSpeciality", SqlDbType.VarChar, 100); sp11.Value = inf.vcSpeciality;
            SqlParameter sp12 = new SqlParameter("@cChecked", SqlDbType.Char, 1); sp12.Value = inf.cChecked;
            SqlParameter sp13 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp13.Value = inf.vcFilePath;
            SqlParameter sp14 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp14.Value = Extension;
            SqlParameter sp15 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp15.Value = inf.cCreated;
            SqlParameter sp16 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp16.Value = "02";
            SqlParameter sp17 = new SqlParameter("@iId", SqlDbType.Int, 4); sp17.Value = inf.iId;
            SqlParameter sp18 = new SqlParameter("@vcTitleColor", SqlDbType.VarChar, 10); sp18.Value = inf.vcTitleColor;
            SqlParameter sp19 = new SqlParameter("@cStrong", SqlDbType.Char, 1); sp19.Value = inf.cStrong;
            SqlParameter sp20 = new SqlParameter("@iCount", SqlDbType.Int, 4); sp20.Value = inf.iCount;
            SqlParameter sp21 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp21.Direction = ParameterDirection.Output;
            SqlParameter sp22 = new SqlParameter("@iIDOut", SqlDbType.Int, 4); sp22.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18,sp19,sp20,sp21,sp22}, new int[] { 21, 22 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                outid = (int)Convert.ChangeType(reValues[1], typeof(int));

                return rtn;
            }
            return -19000000;
        }

        public NewsInfo GetNewsInfoById(Connection conn, int id)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@iID", SqlDbType.Int, 4); sp0.Value = id;
            DataSet ds = conn.GetDataSet("SP_News_GetNewsInfoById", new SqlParameter[] { sp0 });
            if (ds == null) return null;
            NewsInfo item = new NewsInfo();
            item.iId = id;
            if (ds.Tables[0].Rows.Count == 1)
            {
                item.vcTitle = (string)ds.Tables[0].Rows[0]["vcTitle"];
                item.ClassInfo.iId = (int)ds.Tables[0].Rows[0]["iClassID"];
                item.vcUrl = (string)ds.Tables[0].Rows[0]["vcUrl"];
                item.vcContent = (string)ds.Tables[0].Rows[0]["vcContent"];
                item.vcAuthor = (string)ds.Tables[0].Rows[0]["vcAuthor"];
                item.FromInfo.iId = (int)ds.Tables[0].Rows[0]["iFrom"];
                item.iCount = (int)ds.Tables[0].Rows[0]["iCount"];
                item.vcKeyWord = (string)ds.Tables[0].Rows[0]["vcKeyWord"];
                item.vcEditor = (string)ds.Tables[0].Rows[0]["vcEditor"];
                item.cCreated = (string)ds.Tables[0].Rows[0]["cCreated"];
                item.vcSmallImg = (string)ds.Tables[0].Rows[0]["vcSmallImg"];
                item.vcBigImg = (string)ds.Tables[0].Rows[0]["vcBigImg"];
                item.vcShortContent = (string)ds.Tables[0].Rows[0]["vcShortContent"];
                item.vcSpeciality = (string)ds.Tables[0].Rows[0]["vcSpeciality"];
                item.cChecked = (string)ds.Tables[0].Rows[0]["cChecked"];
                item.cDel = (string)ds.Tables[0].Rows[0]["cDel"];
                item.cPostByUser = (string)ds.Tables[0].Rows[0]["cPostByUser"];
                item.vcFilePath = (string)ds.Tables[0].Rows[0]["vcFilePath"];
                item.dAddDate = (DateTime)ds.Tables[0].Rows[0]["dAddDate"];
                item.dUpDateDate = (DateTime)ds.Tables[0].Rows[0]["dUpDateDate"];
                item.vcTitleColor = (string)ds.Tables[0].Rows[0]["vcTitleColor"];
                item.cStrong = (string)ds.Tables[0].Rows[0]["cStrong"];
            }

            if (ds.Tables[1].Rows.Count == 1)
            {
                item.ClassInfo.vcClassName = (string)ds.Tables[1].Rows[0]["vcClassName"];
                item.ClassInfo.vcName = (string)ds.Tables[1].Rows[0]["vcName"];
                item.ClassInfo.iParent = (int)ds.Tables[1].Rows[0]["iParent"];
                item.ClassInfo.iTemplate = (int)ds.Tables[1].Rows[0]["iTemplate"];
                item.ClassInfo.iListTemplate = (int)ds.Tables[1].Rows[0]["iListTemplate"];
                item.ClassInfo.vcDirectory = (string)ds.Tables[1].Rows[0]["vcDirectory"];
                item.ClassInfo.vcUrl = (string)ds.Tables[1].Rows[0]["vcUrl"];
                item.ClassInfo.iOrder = (int)ds.Tables[1].Rows[0]["iOrder"];
            }

            if (ds.Tables[2].Rows.Count == 1)
            {
                item.FromInfo.vcTitle = (string)ds.Tables[2].Rows[0]["vcTitle"];
                item.FromInfo.vcUrl = (string)ds.Tables[2].Rows[0]["vcUrl"];
                item.FromInfo.dUpdateDate = (DateTime)ds.Tables[2].Rows[0]["dUpdateDate"];
            }

            return item;
        }

        /// <summary>
        /// 删除资源文件
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelNewsInfoHtmlById(Connection conn, Dictionary<string, string> config, int id)
        {
            NewsInfo newsitem = this.GetNewsInfoById(conn, id);
            if (newsitem == null) return -19000000;  
            string filepath = HttpContext.Current.Server.MapPath("~" + newsitem.vcFilePath);
            System.IO.File.Delete(filepath);
            return 1;
        }


        /// <summary>
        /// 批量删除资源文件
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelNewsInfoHtmlByIds(Connection conn, Dictionary<string, string> config, string ids)
        {
            if (string.IsNullOrEmpty(ids)) return -19000000;
            if (ids.IndexOf(",") != -1)
            {
                conn.Dblink = DBLinkNums.News;
                string SQL = "SELECT vcFilePath FROM T_News_NewsInfo (NOLOCK) WHERE iId in (" + ids + ")";
                DataTable dt = conn.GetDataTable(SQL);
                if (dt == null) return -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string filepath = HttpContext.Current.Server.MapPath("~" + dt.Rows[i]["vcFilePath"].ToString());
                    try
                    {
                        System.IO.File.Delete(filepath);
                    }
                    catch { }
                }
            }
            else
            {
                int t = Bases.ToInt(ids);
                if (t != 0)
                {
                    return this.DelNewsInfoHtmlById(conn, config, t);
                }
            }
            return 1;
        }

        public DataSet GetNewsInfosByClass(Connection conn, string classids, int number, int create)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcClass", SqlDbType.VarChar, 2000); sp0.Value = classids;
            SqlParameter sp1 = new SqlParameter("@iNum", SqlDbType.Int, 4); sp1.Value = number;
            SqlParameter sp2 = new SqlParameter("@iDel", SqlDbType.Int, 4); sp2.Value = create;
            return conn.GetDataSet("SP_News_GetNewsInfosForCreatHTML", new SqlParameter[] { sp0, sp1, sp2 });
        }

        public DataSet GetNewsInofsByCreateDate(Connection conn, int number, int create, DateTime stime, DateTime etime, int type)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp0.Value = "02";
            SqlParameter sp1 = new SqlParameter("@ITimeType", SqlDbType.Int,4); sp1.Value = type;
            SqlParameter sp2 = new SqlParameter("@dStartTime", SqlDbType.DateTime, 8); sp2.Value = stime;
            SqlParameter sp3 = new SqlParameter("@dEndTime", SqlDbType.DateTime, 8); sp3.Value = etime;
            SqlParameter sp4 = new SqlParameter("@iNum", SqlDbType.Int, 4); sp4.Value = number;
            SqlParameter sp5 = new SqlParameter("@iDel", SqlDbType.Int, 4); sp5.Value = create;
            return conn.GetDataSet("SP_News_GetNewsInfosForCreatHTML", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5 });
        }

        /// <summary>
        /// 检测文章是否已经抓取过
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public int CheckThiefTopic(Connection conn, int classid, string title)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp0.Value = title;
            SqlParameter sp1 = new SqlParameter("@iClassID", SqlDbType.Int, 4); sp1.Value = classid;
            SqlParameter sp2 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp2.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_CheckThiefTopic", new SqlParameter[] { sp0, sp1, sp2}, new int[] { 2 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int)); ;
            }
            return -19000000;
        }

        /// <summary>
        /// 生成文章路径
        /// </summary>
        /// <param name="extion"></param>
        /// <param name="title"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string CreateNewsInfoFilePath(string extion, NewsInfo nif)
        {
            string text = string.Empty;
            string mtitle = Text.GetTextWithoutHtml(nif.vcTitle).Replace(" ", "").Replace("　", "");    //去掉标题里面的HTML，空格
            //mtitle = KeyWordTree.FindKeyWord(mtitle, "-");    //暂时还不分词吧，分词系统太薄弱
            mtitle = Text.ReplaceBD(mtitle);    //去掉标点
            mtitle = (nif.iId == 0 ? "{0}" : nif.iId.ToString()) + "t-" + mtitle;
            text += nif.ClassInfo.vcDirectory;
            text += nif.dAddDate.Year.ToString() + "/";
            text += objectHandlers.AddZeros(nif.dAddDate.Month.ToString(), 2) + objectHandlers.AddZeros(nif.dAddDate.Day.ToString(), 2) + "/";
            text += Pinyin.GetPinyinByChineses(mtitle) + extion;
            return text;
        }

    }
}