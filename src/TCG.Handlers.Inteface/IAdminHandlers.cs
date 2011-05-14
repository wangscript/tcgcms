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
using TCG.Entity;

namespace TCG.Handlers
{
    /// <summary>
    /// 后台管理员的操作方法
    /// </summary>
    public interface  IAdminHandlers 
    {

        /// <summary>
        /// 管理员登陆函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        int AdminLogin(string name, string pwd);

        /// <summary>
        /// 获得所有管理组实体
        /// </summary>
        /// <returns></returns>
        Dictionary<int, AdminRole> GetAllAdminRoleEntity();

        /// <summary>
        /// 根据权限组ID获取权限组信息
        /// </summary>
        /// <param name="iRoleId"></param>
        /// <returns></returns>
        AdminRole GetAdminRoleInfoByRoleId(int iRoleId);


        /// <summary>
        /// 获得所有权限实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        Dictionary<int, Popedom> GetPopedomsEntityFromDataTable(DataTable dt);

        /// <summary>
        /// 根据管理员姓名获得管理员信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        Admin GetAdminEntityByAdminName(string adminname);

        /// <summary>
        /// 从管理员记录行中得到管理员实体
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        Admin GetAdminEntityFromDataRow(DataRow Row);

        /// <summary>
        /// 获得所有管理员实体
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Admin> GetAllAdminEntity();


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
        int AddAdmin(string admin, string vcAdminName, string nickname, string vPassWord, int iRole, string clock, string vcPopedom, string classpop);
        
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
        int UpdateAdminInfo(string admin, string vcAdminName, string nickname, string vPassWord, int iRole, string clock, string vcPopedom, string classpop);



        int UpdateAdminInfo(int AdminID, string nickname, int iRole, string filesysroot, int slock);

        /// <summary>
        /// 删除管理员(wait for mdy)
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        int DeleteAdminById(int AdminID);

        /// <summary>
        /// 获得所有权限项
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Popedom> GetAllPopedomEntity();

        /// <summary>
        /// 获得后台管理的菜单项目
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Popedom> GetManagePopedomEntity();

        /// <summary>
        /// 获得子权限
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Dictionary<int, Popedom> GetChildManagePopedomEntity(int pid);

        /// <summary>
        /// 获得部分权限选项目
        /// </summary>
        /// <param name="iIds"></param>
        /// <returns></returns>
        Dictionary<int, Popedom> GetPopedomsByIDs(string iIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="iIds"></param>
        /// <returns></returns>
        Dictionary<int, Popedom> GetAdminPopedomsByID(AdminRole adminrole, string iIds);


        /// <summary>
        /// 更改自己的登陆信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="oldpwd"></param>
        /// <param name="npwd"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        int ChanageAdminLoginInfo(string adminname, string oldpwd, string npwd, string nickname);

        /// <summary>
        /// 获得角色组基本信息
        /// </summary>
        /// <param name="admincount"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        int GetAdminRoleInfo(ref int admincount,ref int delcount, ref DataSet ds);

        /// <summary>
        /// 获取角色下的管理员列表信息
        /// </summary>
        /// <param name="iRoleID"></param>
        /// <param name="admincount"></param>
        /// <param name="rolecount"></param>
        /// <param name="rolename"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        int GetAdminList(int iRoleID, ref int admincount, ref int rolecount, ref string rolename, ref DataSet ds);

        /// <summary>
        /// 移动管理员到管理组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="irole"></param>
        /// <returns></returns>
        int AdminChangeGroup(string vcAdminname,string admins, int irole);

        /// <summary>
        /// 添加新的角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="vcRoleName"></param>
        /// <param name="pop"></param>
        /// <param name="classpop"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        int AddAdminRole(string vcAdminname, string vcRoleName, string pop, string classpop, string content);

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
        int MdyAdminRole(string vcAdminname, string vcRoleName, string pop, string classpop, string content, int roleid);

        /// <summary>
        /// 删除角色组
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        int DelAdminRole(string vcAdminname, int roleid);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="action">01，逻辑删除 02物理删除 03救回管理员</param>
        /// <returns></returns>
        int DelAdmins(string vcAdminname, string admins,string action);

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="vcAdminname"></param>
        void AdminLoginOut(string vcAdminname);

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        int CheckAdminNameForReg(string adminname);

        /// <summary>
        /// 检测权限操作项目
        /// </summary>
        /// <param name="pid">操作项编号</param>
        void CheckAdminPop(int pid);

        /// <summary>
        /// 检测管理员登录
        /// </summary>
        void CheckAdminLogin();

        void Logout();

        Admin GetAdminInfo();

        int CheckAdminPower(Admin admininfo);
    }
}