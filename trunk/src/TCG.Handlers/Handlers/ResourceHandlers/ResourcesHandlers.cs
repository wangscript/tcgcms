/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ�������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾����������(���ƹ�)�޹ء� 
  *  
  *    �����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
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
    public class ResourcesHandlers : ResourceHandlerBase
    {
        /// <summary>
        /// ������Ѷ
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int AddNewsInfo(Connection conn,string Extension,Resources inf, ref int outid)
        {
            base.SetReourceHandlerDataBaseConnection();
            inf.dAddDate = DateTime.Now;
            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp0.Value = inf.ClassInfo.Id;
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
            SqlParameter sp12 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp12.Value = inf.vcFilePath;
            SqlParameter sp13 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp13.Direction = ParameterDirection.Output;
            SqlParameter sp14 = new SqlParameter("@iId", SqlDbType.VarChar, 36); sp14.Value = inf.Id;
            SqlParameter sp15 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp15.Value = Extension;
            SqlParameter sp16 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp16.Value = inf.cCreated;
            SqlParameter sp17 = new SqlParameter("@vcTitleColor", SqlDbType.VarChar, 10); sp17.Value = inf.vcTitleColor;
            SqlParameter sp18 = new SqlParameter("@cStrong", SqlDbType.Char, 1); sp18.Value = inf.cStrong;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18}, new int[] { 13 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// ������Ѷ
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public int AddNewsInfoForSheif(Connection conn, string Extension, Resources inf)
        {
            base.SetReourceHandlerDataBaseConnection();
            inf.dAddDate = DateTime.Now;
            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp0.Value = inf.ClassInfo.Id;
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
            SqlParameter sp12 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp12.Value = inf.vcFilePath;
            SqlParameter sp13 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp13.Direction = ParameterDirection.Output;
            SqlParameter sp14 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp14.Value = Extension;
            SqlParameter sp15 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp15.Value = inf.cCreated;
            SqlParameter sp16 = new SqlParameter("@cShief", SqlDbType.Char, 2); sp16.Value = "02";
            SqlParameter sp17 = new SqlParameter("@iId", SqlDbType.VarChar, 36); sp17.Value = inf.Id;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp16,sp17}, new int[] { 13 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// ������Դ�ı���״̬
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="IsDel"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelNewsInfosWithLogic(Connection conn, string adminname, string IsDel, string ids)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
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
        /// ������Դ�ı���״̬
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="IsDel"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int UpdateNewsInfosCreate(Connection conn, string adminname, string IsDel, string ids)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
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
        /// ����ɾ����Դ
        /// </summary>
        /// <returns></returns>
        public int DelNewsInfosWithPhysics(Connection conn, string adminname,string ids)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
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

        public int UpdateNewsInfo(Resources inf)
        {
            base.SetReourceHandlerDataBaseConnection();
            inf.vcFilePath = this.CreateNewsInfoFilePath(inf);

            SqlParameter sp0 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp0.Value = inf.ClassInfo.Id;
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
            SqlParameter sp13 = new SqlParameter("@vcFilePath", SqlDbType.VarChar, 255); sp13.Value = inf.vcFilePath;
            SqlParameter sp14 = new SqlParameter("@vcExtension", SqlDbType.VarChar, 6); sp14.Value = base.configService.baseConfig["FileExtension"];
            SqlParameter sp15 = new SqlParameter("@cCreated", SqlDbType.Char, 1); sp15.Value = inf.cCreated;
            SqlParameter sp16 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp16.Value = "02";
            SqlParameter sp17 = new SqlParameter("@iId", SqlDbType.VarChar, 36); sp17.Value = inf.Id;
            SqlParameter sp18 = new SqlParameter("@vcTitleColor", SqlDbType.VarChar, 10); sp18.Value = inf.vcTitleColor;
            SqlParameter sp19 = new SqlParameter("@cStrong", SqlDbType.Char, 1); sp19.Value = inf.cStrong;
            SqlParameter sp20 = new SqlParameter("@iCount", SqlDbType.Int, 4); sp20.Value = inf.iCount;
            SqlParameter sp21 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp21.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp6,
                sp7, sp8, sp9 ,sp10,sp11, sp12, sp13 ,sp14,sp15,sp16,sp17,sp18,sp19,sp20,sp21}, new int[] { 20 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));

                return rtn;
            }
            return -19000000;
        }

        public Resources GetNewsInfoById(string categoriesid, string resourceid)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@iID", SqlDbType.VarChar, 36); sp0.Value = resourceid;
            DataSet ds = base.conn.GetDataSet("SP_News_GetNewsInfoById", new SqlParameter[] { sp0 });
            if (ds == null) return null;
            Resources item = new Resources();
            item.Id = resourceid;
            if (ds.Tables[0].Rows.Count == 1)
            {
                item.vcTitle = (string)ds.Tables[0].Rows[0]["vcTitle"];
                item.ClassInfo.Id = ds.Tables[0].Rows[0]["iClassID"].ToString();
                item.vcUrl = (string)ds.Tables[0].Rows[0]["vcUrl"];
                item.vcContent = (string)ds.Tables[0].Rows[0]["vcContent"];
                item.vcAuthor = (string)ds.Tables[0].Rows[0]["vcAuthor"];
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
                item.ClassInfo.Parent = ds.Tables[1].Rows[0]["Parent"].ToString();
                item.ClassInfo.iTemplate = ds.Tables[1].Rows[0]["iTemplate"].ToString();
                item.ClassInfo.iListTemplate = ds.Tables[1].Rows[0]["iListTemplate"].ToString();
                item.ClassInfo.vcDirectory = (string)ds.Tables[1].Rows[0]["vcDirectory"];
                item.ClassInfo.vcUrl = (string)ds.Tables[1].Rows[0]["vcUrl"];
                item.ClassInfo.iOrder = (int)ds.Tables[1].Rows[0]["iOrder"];
            }

            return string.IsNullOrEmpty(item.vcTitle) ? null : item;
        }

        /// <summary>
        /// ɾ����Դ�ļ�
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelNewsInfoHtmlById(string categoriesid, string resourceid)
        {
            Resources newsitem = this.GetNewsInfoById(categoriesid,resourceid);
            if (newsitem == null) return -19000000;  
            string filepath = HttpContext.Current.Server.MapPath("~" + newsitem.vcFilePath);
            System.IO.File.Delete(filepath);
            return 1;
        }


        /// <summary>
        /// ����ɾ����Դ�ļ�
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelNewsInfoHtmlByIds(string ids)
        {
            base.SetReourceHandlerDataBaseConnection();
            if (string.IsNullOrEmpty(ids)) return -19000000;
            if (ids.IndexOf(",") != -1)
            {

                string SQL = "SELECT vcFilePath FROM Resources (NOLOCK) WHERE iId in (" + ids + ")";
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
                string t = ids;
                if (string.IsNullOrEmpty(t))
                {
                    return this.DelNewsInfoHtmlById("",t);
                }
            }
            return 1;
        }

        public DataSet GetNewsInfosByClass(Connection conn, string classids, int number, int create)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcClass", SqlDbType.VarChar, 2000); sp0.Value = classids;
            SqlParameter sp1 = new SqlParameter("@iNum", SqlDbType.Int, 4); sp1.Value = number;
            SqlParameter sp2 = new SqlParameter("@iDel", SqlDbType.Int, 4); sp2.Value = create;
            return conn.GetDataSet("SP_News_GetNewsInfosForCreatHTML", new SqlParameter[] { sp0, sp1, sp2 });
        }

        public DataSet GetNewsInofsByCreateDate(Connection conn, int number, int create, DateTime stime, DateTime etime, int type)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp0.Value = "02";
            SqlParameter sp1 = new SqlParameter("@ITimeType", SqlDbType.Int,4); sp1.Value = type;
            SqlParameter sp2 = new SqlParameter("@dStartTime", SqlDbType.DateTime, 8); sp2.Value = stime;
            SqlParameter sp3 = new SqlParameter("@dEndTime", SqlDbType.DateTime, 8); sp3.Value = etime;
            SqlParameter sp4 = new SqlParameter("@iNum", SqlDbType.Int, 4); sp4.Value = number;
            SqlParameter sp5 = new SqlParameter("@iDel", SqlDbType.Int, 4); sp5.Value = create;
            return conn.GetDataSet("SP_News_GetNewsInfosForCreatHTML", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5 });
        }

        /// <summary>
        /// ��������Ƿ��Ѿ�ץȡ��
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public int CheckThiefTopic(Connection conn, string classid, string title)
        {
            base.SetReourceHandlerDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 100); sp0.Value = title;
            SqlParameter sp1 = new SqlParameter("@iClassID", SqlDbType.VarChar, 36); sp1.Value = classid;
            SqlParameter sp2 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp2.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_CheckThiefTopic", new SqlParameter[] { sp0, sp1, sp2}, new int[] { 2 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int)); ;
            }
            return -19000000;
        }

        /// <summary>
        /// ��������·��
        /// </summary>
        /// <param name="extion"></param>
        /// <param name="title"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string CreateNewsInfoFilePath(Resources nif)
        {
            if (string.IsNullOrEmpty(nif.Id)) nif.Id = Guid.NewGuid().ToString();
            string text = string.Empty;
            text += nif.ClassInfo.vcDirectory;
            text += nif.dAddDate.Year.ToString() + objectHandlers.AddZeros(nif.dAddDate.Month.ToString(), 2);
            text += objectHandlers.AddZeros(nif.dAddDate.Day.ToString(), 2) + "/";
            text += nif.Id + base.configService.baseConfig["FileExtension"];
            return text;
        }

    }
}