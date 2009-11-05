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
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TCG.Data;
using TCG.Utils;

using TCG.Entity;

namespace TCG.Handlers
{
    /// <summary>
    /// 后台管理员的操作方法
    /// </summary>
    public class AdminHandlers : ManageHandlerBase
    {
        public AdminHandlers()
        {
        }

        /// <summary>
        /// 管理员登陆函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int AdminLogin(string name, string pwd)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = name;
            SqlParameter sp1 = new SqlParameter("@vcPassword", SqlDbType.VarChar, 32); sp1.Value = pwd;
            SqlParameter sp2 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp2.Value = objectHandlers.UserIp;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AdminLogin", new SqlParameter[] { sp0, sp1, sp2, sp3 }, new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                if (rtn == 1)
                {
                    HttpCookie admincookie = Cookie.Get(base.configService.baseConfig["AdminCookieName"]);
                    if (admincookie == null)
                    {
                        admincookie = Cookie.Set(base.configService.baseConfig["AdminCookieName"]);
                    }
                    admincookie.Values["AdminName"] = HttpContext.Current.Server.UrlEncode(name);
                    Cookie.Save(admincookie);

                    object TempAdmin = SessionState.Get(base.configService.baseConfig["AdminSessionName"]);
                    if (TempAdmin != null) SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
                }
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 根据管理员名获得管理员信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetAdminInfoByName(string name,string caction,ref DataSet ds)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = name;
            SqlParameter sp1 = new SqlParameter("@vcIP", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.VarChar, 15); sp2.Value = caction;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.GetDataSet("SP_Manage_GetAdminInfoByName", new SqlParameter[] { sp0, sp1, sp2, sp3 }, new int[] { 3 }, ref ds);
            if (reValues != null)
            {
               return (int)Convert.ChangeType(reValues[0], typeof(int));
            }
            return -19000000;
        }

        /// <summary>
        /// 获得所有管理角色
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLAdminRole()
        {
            base.SetManageDataBaseConnection();
            string sql = "SELECT iID,vcRoleName,vcContent,vcPopedom,vcClassPopedom,dUpdateDate FROM dbo.AdminRole (NOLOCK)";
            return base.conn.GetDataSet(sql);
        }

        /// <summary>
        ///获得指定管理员的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAdminInfoByAdminName(string adminname)
        {
            base.SetManageDataBaseConnection();
            if (string.IsNullOrEmpty(adminname)) return null;
            string sql = "SELECT vcAdminName,vcNickName,vcPassWord,iRole,clock,vcPopedom,vcClassPopedom FROM Admin (NOLOCK) WHERE vcAdminName ='" + adminname + "'";
            return base.conn.GetDataTable(sql);
        }

        /// <summary>
        /// 添加新的管理员
        /// </summary>
        /// <param name="vcAdminName"></param>
        /// <param name="nickname"></param>
        /// <param name="vPassWord"></param>
        /// <param name="iRole"></param>
        /// <param name="slock"></param>
        /// <param name="filesysroot"></param>
        /// <returns></returns>
        public int AddAdmin(string admin,string vcAdminName, string nickname, string vPassWord, int iRole, string clock, string vcPopedom,string classpop)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcNickName", SqlDbType.VarChar, 50); sp1.Value = nickname;
            SqlParameter sp2 = new SqlParameter("@vcPassWord", SqlDbType.VarChar, 32); sp2.Value = vPassWord;
            SqlParameter sp3 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp3.Value = iRole;
            SqlParameter sp4 = new SqlParameter("@clock", SqlDbType.Char, 1); sp4.Value = clock;
            SqlParameter sp5 = new SqlParameter("@vcPopedom", SqlDbType.VarChar, 1000); sp5.Value = vcPopedom;
            SqlParameter sp6 = new SqlParameter("@vcClassPopedom", SqlDbType.VarChar, 255); sp6.Value = classpop;
            SqlParameter sp7 = new SqlParameter("@aAdminName", SqlDbType.VarChar, 50); sp7.Value = admin;
            SqlParameter sp8 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp8.Value = objectHandlers.UserIp;
            SqlParameter sp9 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp9.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AddAdmin", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9 }, new int[] { 9 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int));
            }

            return -19000000;
        }

        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="vcAdminName"></param>
        /// <param name="nickname"></param>
        /// <param name="vPassWord"></param>
        /// <param name="iRole"></param>
        /// <param name="clock"></param>
        /// <param name="vcPopedom"></param>
        /// <param name="classpop"></param>
        /// <returns></returns>
        public int UpdateAdminInfo(string admin, string vcAdminName, string nickname, string vPassWord, int iRole, string clock, string vcPopedom, string classpop)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcNickName", SqlDbType.VarChar, 50); sp1.Value = nickname;
            SqlParameter sp2 = new SqlParameter("@vcPassWord", SqlDbType.VarChar, 32); sp2.Value = vPassWord;
            SqlParameter sp3 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp3.Value = iRole;
            SqlParameter sp4 = new SqlParameter("@clock", SqlDbType.Char, 1); sp4.Value = clock;
            SqlParameter sp5 = new SqlParameter("@vcPopedom", SqlDbType.VarChar, 1000); sp5.Value = vcPopedom;
            SqlParameter sp6 = new SqlParameter("@vcClassPopedom", SqlDbType.VarChar, 255); sp6.Value = classpop;
            SqlParameter sp7 = new SqlParameter("@aAdminName", SqlDbType.VarChar, 50); sp7.Value = admin;
            SqlParameter sp8 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp8.Value = objectHandlers.UserIp;
            SqlParameter sp9 = new SqlParameter("@action", SqlDbType.Char, 2); sp9.Value = "02";
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp10.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AddAdmin", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9 ,sp10},
                new int[] { 10 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int));
            }

            return -19000000;
        }

        /// <summary>
        ///更新管理员信息
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="nickname"></param>
        /// <param name="iRole"></param>
        /// <param name="filesysroot"></param>
        /// <param name="slock"></param>
        /// <returns></returns>
        public int UpdateAdminInfo(int AdminID,string nickname,int iRole,string filesysroot,int slock)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@id", SqlDbType.Int, 4); sp0.Value = AdminID.ToString();
            SqlParameter sp1 = new SqlParameter("@nickname", SqlDbType.VarChar, 50); sp1.Value = nickname;
            SqlParameter sp2 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp2.Value = iRole.ToString();
            SqlParameter sp3 = new SqlParameter("@filesysroot", SqlDbType.VarChar, 255); sp3.Value = filesysroot;
            SqlParameter sp4 = new SqlParameter("@lock", SqlDbType.TinyInt, 1); sp4.Value = slock.ToString();
            SqlParameter sp5 = new SqlParameter("@uptime", SqlDbType.DateTime, 8); sp5.Value = DateTime.Now;
            SqlParameter sp7 = new SqlParameter("@vcIp", SqlDbType.VarBinary, 15); sp7.Value = objectHandlers.UserIp;
            SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_UpdateAdminInfo", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 ,sp7}, new int[] { 7 });
            if (reValues != null)
            {
                return (int)Convert.ChangeType(reValues[0], typeof(int));
            }

            return -19000000;
        }

        /// <summary>
        /// 删除管理员(wait for mdy)
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public int DeleteAdminById(int AdminID)
        {
            base.SetManageDataBaseConnection();
            if (AdminID == 0) return -19000000;
            string sql = "DELETE FROM T_Manage_Admin WHERE id=" + AdminID.ToString();
            return base.conn.Execute(sql);
        }

        /// <summary>
        /// 获得所有权限选项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllPopedom()
        {
            base.SetManageDataBaseConnection();
            string sql = "SELECT iID,vcPopName,vcUrl,iParentId,dAddTime FROM Popedom WITH (NOLOCK)";
            return base.conn.GetDataSet(sql);
        }

        /// <summary>
        /// 获得部分权限选项目
        /// </summary>
        /// <param name="iIds"></param>
        /// <returns></returns>
        public DataSet GetPopedomsByID(string iIds)
        {
            base.SetManageDataBaseConnection();
            string sql = "SELECT iID,vcPopName,vcUrl,iParentId,dAddTime FROM Popedom WITH (NOLOCK) WHERE iID IN (" + iIds + ")";
            return base.conn.GetDataSet(sql);
        }


        /// <summary>
        /// 更改自己的登陆信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="oldpwd"></param>
        /// <param name="npwd"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public int ChanageAdminLoginInfo(string adminname, string oldpwd, string npwd, string nickname)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@oldPwd", SqlDbType.VarChar, 32); sp1.Value = oldpwd;
            SqlParameter sp2 = new SqlParameter("@NewPwd", SqlDbType.VarChar, 32); sp2.Value = npwd;
            SqlParameter sp3 = new SqlParameter("@vcNickName", SqlDbType.VarChar, 50); sp3.Value = nickname;
            SqlParameter sp4 = new SqlParameter("@vcIP", SqlDbType.VarChar, 15); sp4.Value = objectHandlers.UserIp;
            SqlParameter sp5 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp5.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_ChanageAdminLoginInfo", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5 }, new int[] { 5 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }

            return -19000000;
        }

        /// <summary>
        /// 获得角色组基本信息
        /// </summary>
        /// <param name="admincount"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetAdminRoleInfo(ref int admincount,ref int delcount, ref DataSet ds)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@admincount", SqlDbType.Int, 4); sp0.Direction = ParameterDirection.Output;
            SqlParameter sp1 = new SqlParameter("@deladmincount", SqlDbType.Int, 4); sp1.Direction = ParameterDirection.Output;
            SqlParameter sp2 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp2.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.GetDataSet("SP_Manage_GetAdminRoleInfo", new SqlParameter[] { sp0, sp1,sp2 }, new int[] { 0,1,2 },ref ds);
            if (reValues != null)
            {
                admincount = (int)Convert.ChangeType(reValues[0], typeof(int));
                delcount = (int)Convert.ChangeType(reValues[1], typeof(int));
                int rtn = (int)Convert.ChangeType(reValues[2], typeof(int));
                return rtn;
            }

            return -19000000;
        }

        /// <summary>
        /// 获取角色下的管理员列表信息
        /// </summary>
        /// <param name="iRoleID"></param>
        /// <param name="admincount"></param>
        /// <param name="rolecount"></param>
        /// <param name="rolename"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetAdminList(int iRoleID,ref int admincount, ref int rolecount,ref string rolename,ref DataSet ds)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@iRoleId", SqlDbType.Int, 4); sp0.Value = iRoleID.ToString();
            SqlParameter sp1 = new SqlParameter("@vcRoleName", SqlDbType.VarChar, 50); sp1.Direction = ParameterDirection.Output;
            SqlParameter sp2 = new SqlParameter("@iRoleCount", SqlDbType.Int, 4); sp2.Direction = ParameterDirection.Output;
            SqlParameter sp3 = new SqlParameter("@iAdminCount", SqlDbType.Int, 4); sp3.Direction = ParameterDirection.Output;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp4.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.GetDataSet("SP_Manage_GetAdminList", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 },
                new int[] { 1, 2, 3, 4 }, ref ds);
            if (reValues != null)
            {
                rolecount = (int)Convert.ChangeType(reValues[1], typeof(int));
                admincount = (int)Convert.ChangeType(reValues[2], typeof(int));
                rolename = reValues[0];
                int rtn = (int)Convert.ChangeType(reValues[3], typeof(int));
                return rtn;
            }

            return -19000000;
        }

        /// <summary>
        /// 移动管理员到管理组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="irole"></param>
        /// <returns></returns>
        public int AdminChangeGroup(string vcAdminname,string admins, int irole)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            SqlParameter sp1 = new SqlParameter("@iRoleId", SqlDbType.Int, 4); sp1.Value = irole.ToString() ;
            SqlParameter sp2 = new SqlParameter("@vcAdmins", SqlDbType.VarChar, 1000); sp2.Value = admins;
            SqlParameter sp3 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp3.Value = objectHandlers.UserIp;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp4.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AdminChangeGroup", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 },
                new int[] { 4 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }

            return -19000000;
        }

        /// <summary>
        /// 添加新的角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="vcRoleName"></param>
        /// <param name="pop"></param>
        /// <param name="classpop"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int AddAdminRole(string vcAdminname, string vcRoleName, string pop, string classpop, string content)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcRoleName", SqlDbType.VarChar, 50); sp2.Value = vcRoleName;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.VarChar, 255); sp3.Value = content;
            SqlParameter sp4 = new SqlParameter("@vcPopedom", SqlDbType.VarChar, 1000); sp4.Value = pop;
            SqlParameter sp5 = new SqlParameter("@vcClassPopedom", SqlDbType.VarChar, 255); sp5.Value = classpop;
            SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AdminRoleInfoMdy", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 },
                new int[] { 6 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000; 
        }

        /// <summary>
        /// 修改角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="vcRoleName"></param>
        /// <param name="pop"></param>
        /// <param name="classpop"></param>
        /// <param name="content"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public int MdyAdminRole(string vcAdminname, string vcRoleName, string pop, string classpop, string content, int roleid)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcRoleName", SqlDbType.VarChar, 50); sp2.Value = vcRoleName;
            SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.VarChar, 255); sp3.Value = content;
            SqlParameter sp4 = new SqlParameter("@vcPopedom", SqlDbType.VarChar, 1000); sp4.Value = pop;
            SqlParameter sp5 = new SqlParameter("@vcClassPopedom", SqlDbType.VarChar, 255); sp5.Value = classpop;
            SqlParameter sp6 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp6.Value = "02";
            SqlParameter sp7 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp7.Value = roleid.ToString();
            SqlParameter sp8 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp8.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AdminRoleInfoMdy", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8 },
                new int[] { 8 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000; 
        }

        public DataTable GetAdminRoleInfoByRoleId(int iRoleId)
        {
            base.SetManageDataBaseConnection();
            string Sql = "SELECT iID,vcRoleName,vcContent,vcPopedom,vcClassPopedom FROM AdminRole WITH (NOLOCK) WHERE iID =" + iRoleId.ToString();
            return base.conn.GetDataTable(Sql);
        }

        /// <summary>
        /// 删除角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public int DelAdminRole(string vcAdminname, int roleid)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp2.Value = roleid.ToString();
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp3.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_AdminRoleDel", new SqlParameter[] { sp0, sp1, sp2, sp3 },
                new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000; 
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="action">01，逻辑删除 02物理删除 03救回管理员</param>
        /// <returns></returns>
        public int DelAdmins(string vcAdminname, string admins,string action)
        {
            base.SetManageDataBaseConnection();
            if (string.IsNullOrEmpty(action)) action = "01";
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcAdmins", SqlDbType.VarChar, 1000); sp2.Value = admins;
            SqlParameter sp3 = new SqlParameter("@action", SqlDbType.Char, 2); sp3.Value = action;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp4.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Manage_DelAdmins", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 },
                new int[] { 4 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000; 
        }

        public void AdminLoginOut(string vcAdminname)
        {
            base.SetManageDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            base.conn.Execute("Sp_Manage_AdminLogout", new SqlParameter[] { sp0 });
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int CheckAdminNameForReg(string adminname)
        {
            base.SetManageDataBaseConnection();
            if (string.IsNullOrEmpty(adminname)) return -1;
            return (int)base.conn.GetScalar("SELECT COUNT(1) FROM Admin (NOLOCK) WHERE vcAdminName='" + adminname + "'");
        }
    }
}