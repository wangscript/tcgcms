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

        public TagService tagService
        {
            get
            {
                if (this._ragservice == null)
                {
                    this._ragservice = new TagService(this.handlerService);
                    this._ragservice.configService = base.configService;
                    this._ragservice.conn = base.conn;
                }
                return this._ragservice;
            }
        }
        private TagService _ragservice = null;


        /// <summary>
        /// �ṩϵͳ���������ķ���
        /// </summary>
        protected HandlerService handlerService
        {
            get
            {
                if (this._handlerservice == null)
                {
                    this._handlerservice = new HandlerService();
                    this._handlerservice.configService = base.configService;
                    this._handlerservice.conn = base.conn;
                }
                return this._handlerservice;
            }
        }
        private HandlerService _handlerservice = null;

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

