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

namespace TCG.Pages
{
    using TCG.Entity;
    using TCG.Handlers;
    using TCG.Utils;
    using TCG.Data;
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;

    public class adminMain : Origin
    {

        public adminMain()
        {
           
        }

        protected Admin adminInfo
        {
            get
            {
                if (this._admininfo == null)
                {
                    this._admininfo = this.handlerService.manageService.adminLoginHandlers.adminInfo;
                }
                return this._admininfo;
            }
        }
        private Admin _admininfo = null;
    }
}

