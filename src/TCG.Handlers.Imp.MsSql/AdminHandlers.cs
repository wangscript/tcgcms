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
using System.Data;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers.Imp.MsSql
{
    public class AdminHandlers : IAdminHandlers
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
        public int AdminLogin(Admin admininfo, string pwd)
        {
            
            string sql = "UPDATE admin SET cIsOnline = 'Y',vcLastLoginIp = '" + objectHandlers.GetIP()
                + "',iLoginCount = iLoginCount+1,dLastLoginDate=getdate() WHERE vcAdminName ='" + admininfo.vcAdminName + "'";

            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        
        /// <summary>
        /// 获得所有管理角色
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLAdminRole()
        {

            string sql = "SELECT iID,vcRoleName,vcContent,vcPopedom,vcClassPopedom,dUpdateDate FROM AdminRole";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        /// <summary>
        ///获得指定管理员的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAllAdmin()
        {

            string sql = "SELECT vcAdminName,vcNickName,vcPassWord,iRole,clock,vcPopedom,vcClassPopedom,cIsDel,cIsOnline,vcLastLoginIp FROM Admin";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
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
        public int AddAdmin(string admin, string vcAdminName, string nickname, string vPassWord, int iRole, string clock, string vcPopedom, string classpop)
        {

            string sql = "INSERT INTO admin (vcAdminName,vcNickName,vcPassWord,iRole,clock,vcPopedom,vcClassPopedom)"
                                + "VALUES('" + vcAdminName + "','" + nickname + "','" + vPassWord + "'," + iRole + ",'" + clock + "','" + vcPopedom + "','" + classpop + "')";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
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

            string SQL = "UPDATE admin SET ";

            if (!string.IsNullOrEmpty(vPassWord))
            {
                SQL += "vcPassWord='" + vPassWord + "',";
            }

            SQL += "vcNickName='" + nickname + "',iRole=" + iRole + ",clock='" + clock + "',vcPopedom='" + vcPopedom + "',vcClassPopedom='" + vcPopedom + "' WHERE vcAdminName='" + vcAdminName + "'";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, SQL);
        }


        /// <summary>
        /// 删除管理员(wait for mdy)
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public int DeleteAdminById(int AdminID)
        {

            if (AdminID == 0) return -19000000;
            string sql = "DELETE FROM T_Manage_Admin WHERE id=" + AdminID.ToString();
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        /// <summary>
        /// 获得所有权限选项目
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPopedom()
        {

            string sql = "SELECT iID,vcPopName,dAddTime,vcUrl,cValid,iParentId FROM Popedom";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }


        /// <summary>
        /// 更改自己的登陆信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="oldpwd"></param>
        /// <param name="npwd"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public int ChanageAdminLoginInfo(Admin admininfo, string oldpwd, string npwd, string nickname)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(npwd))
            {
                admininfo.vcNickName = nickname;
               sql = "UPDATE admin SET vcNickName = '" + nickname + "' WHERE vcAdminName='" + admininfo.vcAdminName + "'";
            }
            else
            {
                admininfo.vcNickName = nickname;
                admininfo.vcPassword = npwd;
                sql = "UPDATE admin SET vcNickName = '" + nickname + "',vcPassword = '" + npwd + "' WHERE vcAdminName='" + admininfo.vcAdminName + "'";
            }

            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        /// <summary>
        /// 获得角色组基本信息
        /// </summary>
        /// <param name="admincount"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetAdminRoleInfo(ref int admincount, ref int delcount, ref DataSet ds)
        {
            string errText = string.Empty, s_admincount = string.Empty, s_dealadmincount = string.Empty, _val = string.Empty;

            MsSqlFactory.conn.ClearSqlParams(ref errText);
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@admincount", SqlDbType.Int, ParameterDirection.Output, 4, "");
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@deladmincount", SqlDbType.Int, ParameterDirection.Output, 4, "");
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@reValue", SqlDbType.Int, ParameterDirection.Output, 4, "");

            int rtn = MsSqlFactory.conn.m_RunSPData(ref errText, "SP_Manage_GetAdminRoleInfo",ref ds);
            if (rtn == 1)
            {
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@reValue", ref _val);
                if (rtn < 0) return rtn;
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@admincount", ref s_admincount);
                if (rtn < 0) return rtn;
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@deladmincount", ref s_dealadmincount);
                if (rtn < 0) return rtn;

                admincount = objectHandlers.ToInt(s_admincount);
                delcount = objectHandlers.ToInt(s_dealadmincount);
                return objectHandlers.ToInt(_val);
            }

            return 1;
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
        public int GetAdminList(int iRoleID, ref int admincount, ref int rolecount, ref string rolename, ref DataSet ds)
        {
            string errText = string.Empty, _val = string.Empty, s_rolecount = string.Empty, s_admincount = string.Empty;

            MsSqlFactory.conn.ClearSqlParams(ref errText);
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@iRoleId", SqlDbType.Int, ParameterDirection.Input, 4, iRoleID.ToString());
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@vcRoleName", SqlDbType.VarChar, ParameterDirection.Output, 50,"");

            MsSqlFactory.conn.m_ParamAdd(ref errText, "@iRoleCount", SqlDbType.Int, ParameterDirection.Output, 4, "");
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@iAdminCount", SqlDbType.Int, ParameterDirection.Output, 4, "");
            MsSqlFactory.conn.m_ParamAdd(ref errText, "@reValue", SqlDbType.Int, ParameterDirection.Output, 4, "");

            int rtn = MsSqlFactory.conn.m_RunSPData(ref errText, "SP_Manage_GetAdminList",ref ds);

            if (rtn != 1)
            {
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@reValue", ref _val);
                if (rtn < 0) return rtn;
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@vcRoleName", ref rolename);
                if (rtn < 0) return rtn;
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@iRoleCount", ref s_rolecount);
                if (rtn < 0) return rtn;
                rtn = MsSqlFactory.conn.m_ParamGet(ref errText, "@iAdminCount", ref s_admincount);
                if (rtn < 0) return rtn;

                rolecount = objectHandlers.ToInt(s_rolecount);
                admincount = objectHandlers.ToInt(s_admincount);

                return objectHandlers.ToInt(_val); ;
            }

            return rtn;
        }

        /// <summary>
        /// 移动管理员到管理组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="irole"></param>
        /// <returns></returns>
        public int AdminChangeGroup(string admins, int irole)
        {
            string errText = string.Empty;
            string sql = string.Empty;
            if (admins.IndexOf(",") > -1)
            {
               sql = "UPDATE admin SET iRole = " + irole.ToString()
                    + " WHERE vcAdminName IN (+admins+)";
            }
            else
            {
                admins = admins.Replace("'", "");
                sql = "UPDATE admin SET iRole = " + irole.ToString()
                    + " WHERE vcAdminName = '" + admins + "'";
            }

            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
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
        public int AddAdminRole(string vcRoleName, string pop, string classpop, string content)
        {
            string errText = string.Empty;
            string sql = "INSERT INTO AdminRole (vcRoleName,vcContent,vcPopedom,vcClassPopedom) "
                    + "VALUES('" + vcRoleName + "','" + vcRoleName + "','" + vcRoleName + "','" + vcRoleName + "') ";

            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
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
        public int MdyAdminRole(string vcRoleName, string pop, string classpop, string content, int roleid)
        {
            string errText = string.Empty;
            string sql = "UPDATE AdminRole SET vcRoleName = '" + vcRoleName + "',vcContent='" + content + "',vcPopedom='" + pop + "', "
                 + "vcClassPopedom='" + pop + "' WHERE iID = " + roleid.ToString();
            int rtn = MsSqlFactory.conn.m_RunSQL(ref errText, sql);
            if (rtn < 0) return rtn;
            sql = "UPDATE admin SET cIsOnline = 'N' WHERE iRole = " + roleid.ToString();

            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        /// <summary>
        /// 删除角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public int DelAdminRole(int roleid)
        {

            string errText = string.Empty;
            string s_cont = string.Empty;
            int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM admin WHERE iRole = " + roleid, ref s_cont);

            if (rtn < 0) return rtn;

            int admincount = objectHandlers.ToInt(s_cont);
            if (admincount > 0)
            {
                return -1000000015;
            }

            return MsSqlFactory.conn.m_RunSQL(ref errText, "DELETE FROM AdminRole WHERE iID = " + roleid.ToString());
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="action">01，逻辑删除 02物理删除 03救回管理员</param>
        /// <returns></returns>
        public int DelAdmins( string admins, string action)
        {
            string errText = string.Empty;
            string sql = string.Empty;
             //尚未选择需要删除的管理员 
            if (string.IsNullOrEmpty(admins))
            {
                return -1000000016;
            }

            if (action == "01")
            {
                if (admins.IndexOf(",") > -1)
                {
                   sql = "UPDATE admin SET cIsDel = 'Y' WHERE vcAdminName IN (" + admins + ") ";
                }
                else
                {
                    admins = admins.Replace("'", "");
                    sql = "UPDATE admin SET cIsDel = 'Y' WHERE vcAdminName ='" + admins + "' ";
                }
            }
            else if(action == "02")
            {
                if (admins.IndexOf(",") > -1)
                {
                    sql = "DELETE FROM admin WHERE vcAdminName IN (" + admins + ") ";
                }
                else
                {
                    admins = admins.Replace("'", "");
                    sql = "DELETE FROM admin WHERE vcAdminName ='" + admins + "' ";
                }
            }
            else if (action == "03")
            {
                if (admins.IndexOf(",") > -1)
                {
                   sql = "UPDATE admin SET cIsDel = 'N' WHERE vcAdminName IN (" + admins + ") ";
                }
                else
                {
                    admins = admins.Replace("'", "");
                    sql = "UPDATE admin SET cIsDel = 'N' WHERE vcAdminName ='" + admins + "' ";
                }
            }

            return MsSqlFactory.conn.m_RunSQL(ref errText, sql); 
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="vcAdminname"></param>
        public void AdminLoginOut(string vcAdminname)
        {
            string errText = string.Empty;
            MsSqlFactory.conn.m_RunSQL(ref errText, "UPDATE admin SET cIsOnline = 'N' WHERE vcAdminName ='" + vcAdminname + "'");
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int CheckAdminNameForReg(string adminname)
        {
            if (string.IsNullOrEmpty(adminname)) return -1;

            string errText = string.Empty;
            string s_cont = string.Empty;
            int rtn = MsSqlFactory.conn.m_ExecuteScalar(ref errText, "SELECT COUNT(1) FROM Admin WHERE vcAdminName='" + adminname + "'", ref s_cont);

            if (rtn < 0) return rtn;

            return objectHandlers.ToInt(s_cont);
        }

       

        public void Logout(Admin admin)
        {
            string errText = string.Empty;
            string sql = "UPDATE [admin] SET cIsOnline='N' WHERE vcAdminName='" + admin.vcAdminName + "'";
            MsSqlFactory.conn.m_RunSQL(ref errText, sql); 
        }

        public void AminInfoRefash()
        {
            string errText = string.Empty;
            string sql = "UPDATE [admin] SET cIsOnline='N' WHERE DATEDIFF('n',dLastLoginDate,getdate())>30";
            MsSqlFactory.conn.m_RunSQL(ref errText, sql); 
        }

        public void UpdateAdminLastloginTime(string Adminname)
        {
            string errText = string.Empty;
            string sql = "UPDATE [admin] SET dLastLoginDate= getdate() WHERE vcAdminName ='" + Adminname + "'";
            MsSqlFactory.conn.m_RunSQL(ref errText, sql); 
        }

        private HttpCookie _admincookie = null;
        private string _name = string.Empty;
        private Admin _admin = null;
        private AdminHandlers _adminh = null;
        private string _currenturl = string.Empty;
    }
}
