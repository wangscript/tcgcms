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
using System.Text;

namespace TCG.Entity
{
    public class NewsSpecialityInfo
    {
        public int iId { get { return this._iid; } set { this._iid = value; } }
        public int iSiteId { get { return this._isiteid; } set { this._isiteid = value; } }
        public int iParent { get { return this._iparent; } set { this._iparent = value; } }
        public DateTime dUpDateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        public string vcTitle { get { return this._vctitle; } set { this._vctitle = value; } }
        public string vcExplain { get { return this._vcexplain; } set { this._vcexplain = value; } }

        private int _iid;
        private int _isiteid;
        private int _iparent;
        private DateTime _dupdatedate;
        private string _vctitle;
        private string _vcexplain;
    }
}
