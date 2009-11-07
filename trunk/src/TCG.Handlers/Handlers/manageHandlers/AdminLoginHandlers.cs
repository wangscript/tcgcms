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
using System.Web.SessionState;
using System.Data;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;

using TCG.Handlers;

namespace TCG.Handlers
{
    /// <summary>
    /// ��̨�����½״̬�Ĳ�������
    /// </summary>
    public class AdminLoginHandlers : ManageHandlerBase
    {

        public AdminLoginHandlers(Connection conn, ConfigService configservice,HandlerService handlersevice, AdminHandlers adminhandlers)
        {
            base.conn = conn;
            base.configService = configservice;
            base.handlerService = handlersevice;
            this._adminh = adminhandlers;
            this.Initialization();
        }

        private void Initialization()
        {
            if (this._admincookie == null)
            {
                this._admincookie = Cookie.Get(base.configService.baseConfig["AdminCookieName"]);
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
            if (this._admin != null) return;
            object TempAdmin = null;
            if (string.IsNullOrEmpty(this._name))
            {
                TempAdmin = SessionState.Get(base.configService.baseConfig["AdminSessionName"]);
                if (TempAdmin != null) SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
                this._admin = null;
                return;
            }
            TempAdmin = SessionState.Get(base.configService.baseConfig["AdminSessionName"]);
            if (TempAdmin == null)
            {
                DataSet ds =  new DataSet();
                int rtn = this._adminh.GetAdminInfoByName(this._name, "01", ref ds);
                if (rtn < 0)
                {
                    SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
                    this._admin = null;
                    return;
                }
                this._admin = new Admin();
                if (ds != null)
                {
                    
                    this._admin.PopedomUrls = ds.Tables[0];
                    this._admin.vcAdminName = ds.Tables[1].Rows[0]["vcAdminName"].ToString();
                    this._admin.vcNickName = ds.Tables[1].Rows[0]["vcNickName"].ToString();
                    this._admin.vcPassword = ds.Tables[1].Rows[0]["vcPassword"].ToString();
                    this._admin.vcRoleName = ds.Tables[1].Rows[0]["vcRoleName"].ToString();
                }

                SessionState.Set(base.configService.baseConfig["AdminSessionName"], this._admin);
                return;
            }
            this._admin = (Admin)TempAdmin;
            int n = this._admin.PopedomUrls.Rows.Count;
        }

        /// <summary>
        /// ������ԱȨ��
        /// </summary>
        /// <param name="popnum"></param>
        public void CheckAdminPop()
        {
            this.AdminInit();
            if (!IsSpage(this._currenturl))
            {
                if (this._admin == null)
                {
                    new Terminator().Throw("��δ��½��̨�����ܷ��ʴ�ҳ�棡", "����û��½��", "��½��̨," + base.configService.baseConfig["WebSite"]
                        + base.configService.baseConfig["ManagePath"]
                        + "login.aspx", base.configService.baseConfig["WebSite"] + 
                        base.configService.baseConfig["ManagePath"] + "login.aspx", false);
                    return;
                }
                if (string.IsNullOrEmpty(this._admin.vcAdminName))
                {
                    new Terminator().Throw("��δ��½��̨�����ܷ��ʴ�ҳ�棡", "����û��½��", "��½��̨,"
                        + base.configService.baseConfig["WebSite"] + base.configService.baseConfig["ManagePath"]
                        + "login.aspx", base.configService.baseConfig["WebSite"] + base.configService.baseConfig["ManagePath"] 
                        + "login.aspx", false);
                    return;
                }

                if (IsOnlyLoginPage(this._currenturl)) return;
                if (!HavePower(this._currenturl))
                {
                    new Terminator().Throw("���Ѿ���½��̨�����ǲ��߱��ò�����Ȩ�ޣ�", "��û�иò���Ȩ�ޣ�",null,null, false);
                    return;
                }
            }
        }

        public bool CheckAdminPop(string page)
        {
            if (!IsSpage(page)) return true;
            if (IsOnlyLoginPage(page)) return true;
            if (HavePower(page)) return true;
            return false;
        }

        private bool IsSpage(string pages)
        {
            List<Option> specialpages = base.configService.manageOutpages["specialpages"];
            bool rtn = false;
            for (int i = 0; i < specialpages.Count; i++)
            {
                string text2 = base.configService.baseConfig["ManagePath"] + specialpages[i].Value;
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        private bool IsOnlyLoginPage(string pages)
        {
            List<Option> onlineloginpages = base.configService.manageOutpages["onlineloginpages"];
            bool rtn = false;
            for (int i = 0; i < onlineloginpages.Count; i++)
            {
                string text2 = base.configService.baseConfig["ManagePath"] + onlineloginpages[i].Value;
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        /// <summary>
        /// �����ж��Ƿ��и�URL�ķ���Ȩ��
        /// </summary>
        /// <param name="config"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        private bool HavePower(string pages)
        {
            if (this._admin.PopedomUrls.Rows.Count == 0) return false;
            //��õ�ǰURL·��
            string text = this._currenturl.ToLower().Trim();

            ///ѭ��URLȨ�ޣ������ķ�����ȷ
            for (int i = 0; i < this._admin.PopedomUrls.Rows.Count; i++)
            {
                if (text.IndexOf(this._admin.PopedomUrls.Rows[i]["vcUrl"].ToString()) > -1)
                {
                    return true;
                }
            }
          
            
            return false;
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
            this._adminh.AdminLoginOut(this._name);
            if (this._admincookie != null)
            {
                Cookie.Remove(this._admincookie);
            }
            if (this._admin != null)
            {
                SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
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

        
        private HttpCookie _admincookie = null;
        private string _name = string.Empty;
        private Admin _admin = null;
        private AdminHandlers _adminh = null;
        private string _currenturl = string.Empty;
    }
}