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

namespace TCG.Files.Entity
{
    public class FileInfos
    {
        public int iClassId { get { return this._iclassid; } set { this._iclassid = value; } }
        public int iSize { get { return this._isize; } set { this._isize = value; } }
        public int iDowns { get { return this._idowns; } set { this._idowns = value; } }
        public int iRequest { get { return this._irequest; } set { this._irequest = value; } }
        public DateTime dCreateDate { get { return this._dcreatedate; } set { this._dcreatedate = value; } }
        public long iID { get { return this._iid; } set { this._iid = value; } }
        public string vcType { get { return this._vctype; } set { this._vctype = value; } }
        public string vcIP { get { return this._vcip; } set { this._vcip = value; } }
        public string vcFileName { get { return this._vcfilename; } set { this._vcfilename = value; } }


        private int _iclassid;
        private int _isize;
        private int _idowns;
        private int _irequest;
        private DateTime _dcreatedate;
        private long _iid;
        private string _vctype;
        private string _vcip;
        private string _vcfilename;
    }

}
