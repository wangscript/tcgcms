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

namespace TCG.Handlers
{
    public class AdminHandlers 
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

            Admin admininfo = this.GetAdminEntityByAdminName(name);

            //用户不存在
            if (admininfo == null || admininfo.cIsDel == "Y")
            {
                return -1000000002;
            }


            if (admininfo.cIsOnline == "Y" && admininfo.vcLastLoginIp != objectHandlers.GetIP())
            {
                return -1000000005;
            }

            //用户密码错误
            if (admininfo.vcPassword != objectHandlers.MD5(pwd))
            {
                return -1000000003;
            }

            //用户已经被锁定
            if (admininfo.cLock == "Y")
            {
                return -1000000004;
            }

            admininfo.cIsOnline = "Y";
            DataBaseFactory.AdminHandlers.AdminLogin(admininfo, pwd);

            HttpCookie admincookie = Cookie.Get(ConfigServiceEx.baseConfig["AdminCookieName"]);
            if (admincookie == null)
            {
                admincookie = Cookie.Set(ConfigServiceEx.baseConfig["AdminCookieName"]);
            }
            admincookie.Values["AdminName"] = HttpContext.Current.Server.UrlEncode(name);
            Cookie.Save(admincookie);

            object TempAdmin = SessionState.Get(ConfigServiceEx.baseConfig["AdminSessionName"]);
            if (TempAdmin != null) SessionState.Remove(ConfigServiceEx.baseConfig["AdminSessionName"]);

            CachingService.Remove(CachingService.CACHING_ALL_ADMIN_ENTITY);
            return 1;
        }

        /// <summary>
        /// 根据管理员名获得管理员信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetAdminInfoByName(string name, string caction, ref DataSet ds)
        {

            //SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = name;
            //SqlParameter sp1 = new SqlParameter("@vcIP", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            //SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.VarChar, 15); sp2.Value = caction;
            //SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            //string[] reValues = base.conn.GetDataSet("SP_Manage_GetAdminInfoByName", new SqlParameter[] { sp0, sp1, sp2, sp3 }, new int[] { 3 }, ref ds);
            //if (reValues != null)
            //{
            //    return (int)Convert.ChangeType(reValues[0], typeof(int));
            //}
            return -19000000;
        }

        /// <summary>
        /// 获得所有管理组实体
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, AdminRole> GetAllAdminRoleEntity()
        {
            Dictionary<int, AdminRole> alladminrole = (Dictionary<int, AdminRole>)CachingService.Get(CachingService.CACHING_ALL_ADMINROLE_ENTITY);
            if (alladminrole == null)
            {
                DataTable dt = GetALLAdminRole();
                if (dt == null) return null;
                if (dt.Rows.Count == 0) return null;
                alladminrole = new Dictionary<int, AdminRole>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow Row = dt.Rows[i];
                    AdminRole role = new AdminRole();

                    role.iID = (int)Row["iId"];
                    role.vcRoleName = Row["vcRoleName"].ToString();
                    role.vcContent = Row["vcContent"].ToString();
                    role.vcPopedom = this.GetPopedomsByIDs(Row["vcPopedom"].ToString());
                    role.PopedomStr = Row["vcPopedom"].ToString();
                    role.vcClassPopedom = Row["vcClassPopedom"].ToString();
                    role.dUpdateDate = objectHandlers.ToTime(Row["dUpdateDate"].ToString());

                    alladminrole.Add(role.iID, role);
                }

                CachingService.Set(CachingService.CACHING_ALL_ADMINROLE_ENTITY, alladminrole, null);
            }
            return alladminrole;
        }

        /// <summary>
        /// 根据权限组ID获取权限组信息
        /// </summary>
        /// <param name="iRoleId"></param>
        /// <returns></returns>
        public AdminRole GetAdminRoleInfoByRoleId(int iRoleId)
        {
            Dictionary<int, AdminRole> allRole = this.GetAllAdminRoleEntity();
            if (allRole == null) return null;
            if (allRole.Count == 0) return null;
            if (!allRole.ContainsKey(iRoleId)) return null;
            return allRole[iRoleId];
        }

        /// <summary>
        /// 获得所有管理角色
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLAdminRole()
        {
            return DataBaseFactory.AdminHandlers.GetALLAdminRole();
        }

        /// <summary>
        /// 获得所有权限实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Dictionary<int, Popedom> GetPopedomsEntityFromDataTable(DataTable dt)
        {
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            Dictionary<int, Popedom> allpopedom = new Dictionary<int, Popedom>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow Row = dt.Rows[i];
                Popedom pop = new Popedom();

                pop.iID = (int)Row["iId"];
                pop.dAddtime = (DateTime)Row["dAddtime"];
                pop.vcPopName = Row["vcPopName"].ToString();
                pop.cValid = Row["cValid"].ToString();
                pop.iParentId = (int)Row["iParentId"];
                pop.vcUrl = Row["vcUrl"].ToString();

                allpopedom.Add(pop.iID, pop);
            }
            return allpopedom;
        }

        /// <summary>
        /// 根据管理员姓名获得管理员信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public Admin GetAdminEntityByAdminName(string adminname)
        {
            Dictionary<string, Admin> allamdin = this.GetAllAdminEntity();
            if (allamdin == null) return null;
            if (allamdin.ContainsKey(adminname))
            {
                return allamdin[adminname];
            }
            return null;
        }

        /// <summary>
        /// 从管理员记录行中得到管理员实体
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public Admin GetAdminEntityFromDataRow(DataRow Row)
        {
            if (Row == null) return null;
            Admin admininfo = new Admin();
            admininfo.vcAdminName = Row["vcAdminName"].ToString();
            admininfo.vcNickName = Row["vcNickName"].ToString();
            admininfo.vcPassword = Row["vcPassword"].ToString();
            admininfo.iRole = this.GetAdminRoleInfoByRoleId((int)Row["iRole"]);
            admininfo.vcPopedom = this.GetAdminPopedomsByID(admininfo.iRole, Row["vcPopedom"].ToString());
            admininfo.PopedomStr = Row["vcPopedom"].ToString();
            admininfo.cLock = Row["clock"].ToString();
            admininfo.vcClassPopedom = Row["vcClassPopedom"].ToString();
            admininfo.cIsDel = Row["cIsDel"].ToString();
            admininfo.cIsOnline = Row["cIsOnline"].ToString();
            admininfo.vcLastLoginIp = Row["vcLastLoginIp"].ToString();
            return admininfo;
        }

        /// <summary>
        /// 获得所有管理员实体
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Admin> GetAllAdminEntity()
        {
            Dictionary<string, Admin> alladmin = (Dictionary<string, Admin>)CachingService.Get(CachingService.CACHING_ALL_ADMIN_ENTITY);
            if (alladmin == null)
            {
                DataTable dt = this.GetAllAdmin();
                if (dt == null) return null;
                if (dt.Rows.Count == 0) return null;
                alladmin = new Dictionary<string, Admin>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Admin admin = this.GetAdminEntityFromDataRow(dt.Rows[i]);
                    alladmin.Add(admin.vcAdminName, admin);
                }
                CachingService.Set(CachingService.CACHING_ALL_ADMIN_ENTITY, alladmin, null);
            }
            return alladmin;
        }

        /// <summary>
        ///获得指定管理员的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAllAdmin()
        {
            return DataBaseFactory.AdminHandlers.GetAllAdmin();
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

            Admin admininfos = this.GetAdminEntityByAdminName(admin);

            int rtn = this.CheckAdminPower(admininfos);
            if (rtn < 0) return rtn;

            DataBaseFactory.AdminHandlers.AddAdmin(admin, vcAdminName, nickname, vPassWord, iRole, clock, vcPopedom, classpop);

            this.AminInfoRefash();
            return 1;
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
            Admin admininfos = this.GetAdminEntityByAdminName(admin);

            int rtn = this.CheckAdminPower(admininfos);
            if (rtn < 0) return rtn;


            DataBaseFactory.AdminHandlers.UpdateAdminInfo( admin,  vcAdminName,  nickname,  vPassWord,  iRole,  clock,  vcPopedom,  classpop);

            this.AminInfoRefash();
            return 1;
        }


        public void AminInfoRefash()
        {
            CachingService.Remove(CachingService.CACHING_ALL_ADMIN_ENTITY);
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
        public int UpdateAdminInfo(int AdminID, string nickname, int iRole, string filesysroot, int slock)
        {

            //SqlParameter sp0 = new SqlParameter("@id", SqlDbType.Int, 4); sp0.Value = AdminID.ToString();
            //SqlParameter sp1 = new SqlParameter("@nickname", SqlDbType.VarChar, 50); sp1.Value = nickname;
            //SqlParameter sp2 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp2.Value = iRole.ToString();
            //SqlParameter sp3 = new SqlParameter("@filesysroot", SqlDbType.VarChar, 255); sp3.Value = filesysroot;
            //SqlParameter sp4 = new SqlParameter("@lock", SqlDbType.TinyInt, 1); sp4.Value = slock.ToString();
            //SqlParameter sp5 = new SqlParameter("@uptime", SqlDbType.DateTime, 8); sp5.Value = DateTime.Now;
            //SqlParameter sp7 = new SqlParameter("@vcIp", SqlDbType.VarBinary, 15); sp7.Value = objectHandlers.UserIp;
            //SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            //string[] reValues = base.conn.Execute("SP_Manage_UpdateAdminInfo", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7 }, new int[] { 7 });
            //if (reValues != null)
            //{
            //    return (int)Convert.ChangeType(reValues[0], typeof(int));
            //}

            return -19000000;
        }

        /// <summary>
        /// 删除管理员(wait for mdy)
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public int DeleteAdminById(int AdminID)
        {
            return DataBaseFactory.AdminHandlers.DeleteAdminById(AdminID);
        }

        /// <summary>
        /// 获得所有权限项
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Popedom> GetAllPopedomEntity()
        {
            Dictionary<int, Popedom> allpop = (Dictionary<int, Popedom>)CachingService.Get(CachingService.CACHING_ALL_POPDOM);
            if (allpop == null)
            {
                DataTable dt = GetAllPopedom();
                if (dt == null) return null;
                allpop = this.GetPopedomsEntityFromDataTable(dt);
                CachingService.Set(CachingService.CACHING_ALL_POPDOM, allpop, null);
            }
            return allpop;
        }

        /// <summary>
        /// 获得后台管理的菜单项目
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Popedom> GetManagePopedomEntity()
        {
            Dictionary<int, Popedom> allpop = this.GetAllPopedomEntity();
            if (allpop == null) return null;
            if (allpop.Count == 0) return null;
            Dictionary<int, Popedom> managepop = new Dictionary<int, Popedom>();
            foreach (KeyValuePair<int, Popedom> keyvalue in allpop)
            {
                if (keyvalue.Value.cValid == "Y")
                {
                    managepop.Add(keyvalue.Key, keyvalue.Value);
                }
            }
            return managepop;
        }

        /// <summary>
        /// 获得子权限
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public Dictionary<int, Popedom> GetChildManagePopedomEntity(int pid)
        {
            Dictionary<int, Popedom> managepop = this.GetManagePopedomEntity();
            if (managepop == null) return null;
            Dictionary<int, Popedom> cmpop = new Dictionary<int, Popedom>();
            foreach (KeyValuePair<int, Popedom> keyvalue in managepop)
            {
                if (keyvalue.Value.iParentId == pid)
                {
                    cmpop.Add(keyvalue.Key, keyvalue.Value);
                }
            }
            return cmpop;
        }

        /// <summary>
        /// 获得所有权限选项目
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPopedom()
        {
            return DataBaseFactory.AdminHandlers.GetAllPopedom();
        }

        /// <summary>
        /// 获得部分权限选项目
        /// </summary>
        /// <param name="iIds"></param>
        /// <returns></returns>
        public Dictionary<int, Popedom> GetPopedomsByIDs(string iIds)
        {
            Dictionary<int, Popedom> allpop = this.GetAllPopedomEntity();
            if (allpop == null) return null;
            if (allpop.Count == 0) return null;
            Dictionary<int, Popedom> pops = new Dictionary<int, Popedom>();
            if (iIds.IndexOf(",") > -1)
            {
                string[] ids = iIds.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    int tid = objectHandlers.ToInt(ids[i]);
                    if (allpop.ContainsKey(tid))
                    {
                        pops.Add(tid, allpop[tid]);
                    }
                }
            }
            else
            {
                int id = objectHandlers.ToInt(iIds);
                if (allpop.ContainsKey(id))
                {
                    pops.Add(id, allpop[id]);
                }
            }

            return (pops.Count == 0) ? null : pops;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="iIds"></param>
        /// <returns></returns>
        public Dictionary<int, Popedom> GetAdminPopedomsByID(AdminRole adminrole, string iIds)
        {
            Dictionary<int, Popedom> allpop = this.GetAllPopedomEntity();
            if (allpop == null) return null;
            if (allpop.Count == 0) return null;
            Dictionary<int, Popedom> pops = new Dictionary<int, Popedom>();
            if (iIds.IndexOf(",") > -1)
            {
                string[] ids = iIds.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    int tid = objectHandlers.ToInt(ids[i]);
                    if (allpop.ContainsKey(tid))
                    {
                        pops.Add(tid, allpop[tid]);
                    }
                }
            }
            else
            {
                int id = objectHandlers.ToInt(iIds);
                if (allpop.ContainsKey(id))
                {
                    pops.Add(id, allpop[id]);
                }
            }

            if (adminrole.vcPopedom != null && adminrole.vcPopedom.Count != 0)
            {
                foreach (KeyValuePair<int, Popedom> keyvalue in adminrole.vcPopedom)
                {
                    if (!pops.ContainsKey(keyvalue.Key))
                    {
                        pops.Add(keyvalue.Key, keyvalue.Value);
                    }
                }
            }

            return (pops.Count == 0) ? null : pops;
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

            Admin admininfo = this.GetAdminEntityByAdminName(adminname);

            int rtn = this.CheckAdminPower(admininfo);
            if (rtn < 0) return rtn;

            //输入原始密码不正确
            if(oldpwd!=admininfo.vcPassword)
            {
                return  -1000000009;
            }
     
            //您的帐号已经锁定，不能修改登陆信息
            if(admininfo.cLock=="Y")
            {
                return -1000000010;
            }



            return DataBaseFactory.AdminHandlers.ChanageAdminLoginInfo(admininfo, oldpwd, npwd, nickname);
        }

        public int CheckAdminPower(Admin admininfo)
        {

            //-操作员为空，您是否尚未登陆？
            if (string.IsNullOrEmpty(admininfo.vcAdminName))
            {
                return -1000000012;
            }

            //您不并不在线，您是否尚未登陆？
            if (admininfo.cIsOnline != "Y")
            {
                return -1000000017;
            }
            return 1;
        }

        /// <summary>
        /// 获得角色组基本信息
        /// </summary>
        /// <param name="admincount"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetAdminRoleInfo(ref int admincount, ref int delcount, ref DataSet ds)
        {
            return DataBaseFactory.AdminHandlers.GetAdminRoleInfo(ref admincount, ref  delcount, ref ds);
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
            return DataBaseFactory.AdminHandlers.GetAdminList( iRoleID, ref  admincount, ref rolecount, ref rolename, ref ds);
        }

        /// <summary>
        /// 移动管理员到管理组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="irole"></param>
        /// <returns></returns>
        public int AdminChangeGroup(string vcAdminname, string admins, int irole)
        {

            Admin admininfos = this.GetAdminEntityByAdminName(vcAdminname);

            int rtn = this.CheckAdminPower(admininfos);
            if (rtn < 0) return rtn;

            //组编号不正确
            if (irole == 0)
            {
                return -1000000011;
            }

            return DataBaseFactory.AdminHandlers.AdminChangeGroup(admins, irole);
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

            //SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            //SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            //SqlParameter sp2 = new SqlParameter("@vcRoleName", SqlDbType.VarChar, 50); sp2.Value = vcRoleName;
            //SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.VarChar, 255); sp3.Value = content;
            //SqlParameter sp4 = new SqlParameter("@vcPopedom", SqlDbType.VarChar, 1000); sp4.Value = pop;
            //SqlParameter sp5 = new SqlParameter("@vcClassPopedom", SqlDbType.VarChar, 255); sp5.Value = classpop;
            //SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            //string[] reValues = base.conn.Execute("SP_Manage_AdminRoleInfoMdy", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 },
            //    new int[] { 6 });
            //if (reValues != null)
            //{
            //    int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
            //    return rtn;
            //}
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

            //SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            //SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            //SqlParameter sp2 = new SqlParameter("@vcRoleName", SqlDbType.VarChar, 50); sp2.Value = vcRoleName;
            //SqlParameter sp3 = new SqlParameter("@vcContent", SqlDbType.VarChar, 255); sp3.Value = content;
            //SqlParameter sp4 = new SqlParameter("@vcPopedom", SqlDbType.VarChar, 1000); sp4.Value = pop;
            //SqlParameter sp5 = new SqlParameter("@vcClassPopedom", SqlDbType.VarChar, 255); sp5.Value = classpop;
            //SqlParameter sp6 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp6.Value = "02";
            //SqlParameter sp7 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp7.Value = roleid.ToString();
            //SqlParameter sp8 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp8.Direction = ParameterDirection.Output;
            //string[] reValues = base.conn.Execute("SP_Manage_AdminRoleInfoMdy", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8 },
            //    new int[] { 8 });
            //if (reValues != null)
            //{
            //    int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
            //    return rtn;
            //}
            return -19000000;
        }

        /// <summary>
        /// 删除角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public int DelAdminRole(string vcAdminname, int roleid)
        {

            //SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = vcAdminname;
            //SqlParameter sp1 = new SqlParameter("@vcIp", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            //SqlParameter sp2 = new SqlParameter("@iRole", SqlDbType.Int, 4); sp2.Value = roleid.ToString();
            //SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp3.Direction = ParameterDirection.Output;
            //string[] reValues = base.conn.Execute("SP_Manage_AdminRoleDel", new SqlParameter[] { sp0, sp1, sp2, sp3 },
            //    new int[] { 3 });
            //if (reValues != null)
            //{
            //    int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
            //    return rtn;
            //}
            return -19000000;
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="action">01，逻辑删除 02物理删除 03救回管理员</param>
        /// <returns></returns>
        public int DelAdmins(string vcAdminname, string admins, string action)
        {

            Admin admin = this.GetAdminEntityByAdminName(vcAdminname);
            if (admin == null) return -19000000;
            return DataBaseFactory.AdminHandlers.DelAdmins(admins, action);
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="vcAdminname"></param>
        public void AdminLoginOut(string vcAdminname)
        {
            DataBaseFactory.AdminHandlers.AdminLoginOut(vcAdminname);
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int CheckAdminNameForReg(string adminname)
        {
            return DataBaseFactory.AdminHandlers.CheckAdminNameForReg(adminname);
        }

        private void Initialization()
        {
            if (this._admincookie == null)
            {
                this._admincookie = Cookie.Get(ConfigServiceEx.baseConfig["AdminCookieName"]);
                if (this._admincookie != null)
                {
                    if (this._admincookie.Values.Count != 1) return;
                    this._name = objectHandlers.UrlDecode(this._admincookie.Values["AdminName"].ToString());
                }
            }
            this._currenturl = this.RemoveA(objectHandlers.CurrentUrl);
        }

        private void AdminInit()
        {
            this.Initialization();
            if (this._admin != null) return;
            object TempAdmin = null;
            if (string.IsNullOrEmpty(this._name))
            {
                TempAdmin = SessionState.Get(ConfigServiceEx.baseConfig["AdminSessionName"]);
                if (TempAdmin != null) SessionState.Remove(ConfigServiceEx.baseConfig["AdminSessionName"]);
                this._admin = null;
                return;
            }
            this._admin = this.GetAdminEntityByAdminName(this._name);
            TempAdmin = SessionState.Get(ConfigServiceEx.baseConfig["AdminSessionName"]);
            if (TempAdmin == null)
            {
                if (this._admin != null && this._admin.cIsOnline == "Y" && this._admin.vcLastLoginIp == objectHandlers.UserIp && this._admin.cIsDel != "Y")
                {
                    SessionState.Set(ConfigServiceEx.baseConfig["AdminSessionName"], this._admin);
                }
                else
                {
                    SessionState.Remove(ConfigServiceEx.baseConfig["AdminSessionName"]);
                }
            }
        }

        /// <summary>
        /// 检测权限操作项目
        /// </summary>
        /// <param name="pid">操作项编号</param>
        public void CheckAdminPop(int pid)
        {
            if (!(this._admin != null && this._admin.vcPopedom != null && this._admin.vcPopedom.Count != 0 && this._admin.vcPopedom.ContainsKey(pid)))
            {
                new Terminator().Throw("您无权限访问改页面!", "", "," + ConfigServiceEx.baseConfig["WebSite"]
                         + ConfigServiceEx.baseConfig["ManagePath"]
                         + "login.aspx", ConfigServiceEx.baseConfig["WebSite"] +
                         ConfigServiceEx.baseConfig["ManagePath"] + "login.aspx", false);
                return;
            }
        }

        /// <summary>
        /// 检测管理员登录
        /// </summary>
        public void CheckAdminLogin()
        {
            this.AdminInit();

            if (this._admin == null)
            {
                new Terminator().Throw("您还未登录后台!", "登录后台", "返回首页," + ConfigServiceEx.baseConfig["WebSite"]
                         + ConfigServiceEx.baseConfig["ManagePath"]
                         + "login.aspx", ConfigServiceEx.baseConfig["WebSite"] +
                         ConfigServiceEx.baseConfig["ManagePath"] + "login.aspx", false);
                return;
            }
        }


        private string RemoveA(string str)
        {
            if (str.IndexOf("?") > 0)
            {
                str = str.Substring(0, str.IndexOf("?"));
            }
            return str;
        }

        public void Logout()
        {
            this.AdminInit();
            this.AdminLoginOut(this._name);
            if (this._admincookie != null)
            {
                Cookie.Remove(this._admincookie);
            }
            if (this._admin != null)
            {
                DataBaseFactory.AdminHandlers.Logout(this._admin);
                SessionState.Remove(ConfigServiceEx.baseConfig["AdminSessionName"]);
            }
        }


        public Admin adminInfo
        {
            get
            {
                this.AdminInit();
                if (this._admin == null) return new Admin();
                return this._admin;
            }
        }

        public Admin GetAdminInfo()
        {
            this.AdminInit();
            return this._admin;
        }


        private HttpCookie _admincookie = null;
        private string _name = string.Empty;
        private Admin _admin = null;
        private AdminHandlers _adminh = null;
        private string _currenturl = string.Empty;
    }
}
