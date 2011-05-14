/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ƹ�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
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
    /// ��̨����Ա�Ĳ�������
    /// </summary>
    public interface  IAdminHandlers 
    {

        /// <summary>
        /// ����Ա��½����
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        int AdminLogin(string name, string pwd);

        /// <summary>
        /// ������й�����ʵ��
        /// </summary>
        /// <returns></returns>
        Dictionary<int, AdminRole> GetAllAdminRoleEntity();

        /// <summary>
        /// ����Ȩ����ID��ȡȨ������Ϣ
        /// </summary>
        /// <param name="iRoleId"></param>
        /// <returns></returns>
        AdminRole GetAdminRoleInfoByRoleId(int iRoleId);


        /// <summary>
        /// �������Ȩ��ʵ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        Dictionary<int, Popedom> GetPopedomsEntityFromDataTable(DataTable dt);

        /// <summary>
        /// ���ݹ���Ա������ù���Ա��Ϣ
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        Admin GetAdminEntityByAdminName(string adminname);

        /// <summary>
        /// �ӹ���Ա��¼���еõ�����Աʵ��
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        Admin GetAdminEntityFromDataRow(DataRow Row);

        /// <summary>
        /// ������й���Աʵ��
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Admin> GetAllAdminEntity();


        /// <summary>
        /// ����µĹ���Ա
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
        /// ���¹���Ա��Ϣ
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
        /// ɾ������Ա(wait for mdy)
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        int DeleteAdminById(int AdminID);

        /// <summary>
        /// �������Ȩ����
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Popedom> GetAllPopedomEntity();

        /// <summary>
        /// ��ú�̨����Ĳ˵���Ŀ
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Popedom> GetManagePopedomEntity();

        /// <summary>
        /// �����Ȩ��
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Dictionary<int, Popedom> GetChildManagePopedomEntity(int pid);

        /// <summary>
        /// ��ò���Ȩ��ѡ��Ŀ
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
        /// �����Լ��ĵ�½��Ϣ
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="oldpwd"></param>
        /// <param name="npwd"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        int ChanageAdminLoginInfo(string adminname, string oldpwd, string npwd, string nickname);

        /// <summary>
        /// ��ý�ɫ�������Ϣ
        /// </summary>
        /// <param name="admincount"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        int GetAdminRoleInfo(ref int admincount,ref int delcount, ref DataSet ds);

        /// <summary>
        /// ��ȡ��ɫ�µĹ���Ա�б���Ϣ
        /// </summary>
        /// <param name="iRoleID"></param>
        /// <param name="admincount"></param>
        /// <param name="rolecount"></param>
        /// <param name="rolename"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        int GetAdminList(int iRoleID, ref int admincount, ref int rolecount, ref string rolename, ref DataSet ds);

        /// <summary>
        /// �ƶ�����Ա��������
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="irole"></param>
        /// <returns></returns>
        int AdminChangeGroup(string vcAdminname,string admins, int irole);

        /// <summary>
        /// ����µĽ�ɫ��
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="vcRoleName"></param>
        /// <param name="pop"></param>
        /// <param name="classpop"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        int AddAdminRole(string vcAdminname, string vcRoleName, string pop, string classpop, string content);

        /// <summary>
        /// �޸Ľ�ɫ��
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
        /// ɾ����ɫ��
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        int DelAdminRole(string vcAdminname, int roleid);

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="vcAdminname"></param>
        /// <param name="admins"></param>
        /// <param name="action">01���߼�ɾ�� 02����ɾ�� 03�Ȼع���Ա</param>
        /// <returns></returns>
        int DelAdmins(string vcAdminname, string admins,string action);

        /// <summary>
        /// �û��˳�
        /// </summary>
        /// <param name="vcAdminname"></param>
        void AdminLoginOut(string vcAdminname);

        /// <summary>
        /// ����û����Ƿ����
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        int CheckAdminNameForReg(string adminname);

        /// <summary>
        /// ���Ȩ�޲�����Ŀ
        /// </summary>
        /// <param name="pid">��������</param>
        void CheckAdminPop(int pid);

        /// <summary>
        /// ������Ա��¼
        /// </summary>
        void CheckAdminLogin();

        void Logout();

        Admin GetAdminInfo();

        int CheckAdminPower(Admin admininfo);
    }
}