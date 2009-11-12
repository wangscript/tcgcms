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
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// ��̨����Աʵ��
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string vcAdminName { set { this._vcadminname = value; } get { return this._vcadminname; } }
        /// <summary>
        /// ����Ա�ǳ�
        /// </summary>
        public string vcNickName { set { this._vcnickname = value; } get { return this._vcnickname; } }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string vcPassword { set { this._vcpassword = value; } get { return this._vcpassword; } }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public AdminRole iRole { set { this._irole = value; } get { return this._irole; } }
        /// <summary>
        /// ����Ա�ض�Ȩ��
        /// </summary>
        public Dictionary<int, Popedom> vcPopedom { set { this._vcpopedom = value; } get { return this._vcpopedom; } }
        /// <summary>
        /// ����Ա����Ȩ��
        /// </summary>
        public string vcClassPopedom { set { this._vcclasspopedom = value; } get { return this._vcclasspopedom; } }
        /// <summary>
        /// �Ƿ����� ��ΪY��ʱ���ʾ����
        /// </summary>
        public string cLock { set { this._clock = value; } get { return this._clock; } }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime dAddDate { set { this._dadddate = value; } get { return this._dadddate; } }
        /// <summary>
        /// ���һ�θ��µ�ʱ��
        /// </summary>
        public DateTime dUpdateDate { set { this._dupdatedate = value; } get { return this._dupdatedate; } }
        /// <summary>
        /// ���һ�ε�½��ʱ��
        /// </summary>
        public DateTime dLoginDate { set { this._dlogindate = value; } get { return this._dlogindate; } }
        /// <summary>
        /// ���һ�ε�½��ʱ��
        /// </summary>
        public DateTime dLastLoginDate { set { this._dlastlogindate = value; } get { return this._dlastlogindate; } }
        /// <summary>
        /// ��½����
        /// </summary>
        public int iLoginCount { set { this._ilogincount = value; } get { return this._ilogincount; } }
        /// <summary>
        /// ���һ�ε�½��IP
        /// </summary>
        public string vcLastLoginIp { set { this._vclastloginip = value; } get { return this._vclastloginip; } }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public string cIsOnline { set { this._cisonline = value; } get { return this._cisonline; } }
        
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public string cIsDel { set { this._isdel = value; } get { return this._isdel; } }
        /// <summary>
        /// Ȩ��ID�ַ���
        /// </summary>
        public string PopedomStr { get { return this._popedomstr; } set { this._popedomstr = value; } }

        private string _cisonline = "";
        private string _vclastloginip = "";
        private int _ilogincount = 0;
        private DateTime _dlastlogindate;
        private DateTime _dlogindate;
        private DateTime _dupdatedate;
        private DateTime _dadddate;
        private string _clock = "Y";
        private string _vcclasspopedom = "";
        private Dictionary<int, Popedom> _vcpopedom = null;

        private AdminRole _irole = null;
        private string _vcpassword = "";
        private string _vcnickname = "";
        private string _vcadminname = "";
        private string _isdel = "N";
        private string _popedomstr;
    }
}
