/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ι�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Manage.Utils
{
    public class ManageConst
    {
        public static string AdminCookieName = "XzdswAdmin_";
        public static string AdminSessionName = "_XzdswAdmin_";

        public static string AdminLoginName = "XAdminLName_";

        public static string[] SpecialPages = {
            "login.aspx",
            "AjaxMethod/Text_GetErrText.aspx",
            "files/ConentImgCheck.aspx",
            "version/ver.aspx"
        };

        public static string[] OnlineLoginPages = {
            "AjaxMethod/Admin_CheckAdminName.aspx",
            "AjaxMethod/Admin_logout.aspx",
            "AjaxMethod/Admin_CheckPwd.aspx",
            "AjaxMethod/Admin_DelAdminRole.aspx",
            "jsMethod/AllNewsClass.aspx",
            "jsMethod/AllPop.aspx"
        };
    }
}
