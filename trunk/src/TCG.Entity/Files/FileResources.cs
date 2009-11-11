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
    /// <summary>
    /// �ļ���Դʵ��
    /// </summary>
    public class FileResources
    {
        /// <summary>
        /// �ļ�����ID
        /// </summary>
        public int iClassId { get { return this._iclassid; } set { this._iclassid = value; } }
        /// <summary>
        /// �ļ���С
        /// </summary>
        public int iSize { get { return this._isize; } set { this._isize = value; } }
        /// <summary>
        /// �ļ����ش���
        /// </summary>
        public int iDowns { get { return this._idowns; } set { this._idowns = value; } }
        /// <summary>
        /// �ļ����ʴ���
        /// </summary>
        public int iRequest { get { return this._irequest; } set { this._irequest = value; } }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime dCreateDate { get { return this._dcreatedate; } set { this._dcreatedate = value; } }
        /// <summary>
        /// �ļ��ļ�¼���
        /// </summary>
        public long iID { get { return this._iid; } set { this._iid = value; } }
        /// <summary>
        /// �ļ�����
        /// </summary>
        public string vcType { get { return this._vctype; } set { this._vctype = value; } }
        /// <summary>
        /// �ϴ�ʱ��IP
        /// </summary>
        public string vcIP { get { return this._vcip; } set { this._vcip = value; } }
        /// <summary>
        /// �ļ���
        /// </summary>
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
