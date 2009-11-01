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
    /// �ļ�����ʵ�� 
    /// <log>by :sanyungui@yahoo.com.cn date: 2009-10-23 info:</log>
    /// </summary>
    public class FileCategories
    {
        /// <summary>
        /// �ļ�����ID
        /// </summary>
        public int iId { get { return this._iid; } set { this._iid = value; } }
        /// <summary>
        /// �ļ����ุ��
        /// </summary>
        public int iParentId { get { return this._iparentid; } set { this._iparentid = value; } }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime dCreateDate { get { return this._dcreatedate; } set { this._dcreatedate = value; } }
        /// <summary>
        /// ��������
        /// </summary>
        public string vcFileName { get { return this._vcfilename; } set { this._vcfilename = value; } }
        /// <summary>
        /// ˵��
        /// </summary>
        public string vcMeno { get { return this._vcmeno; } set { this._vcmeno = value; } }
      
        private int _iid;
        private int _iparentid;
        private DateTime _dcreatedate;
        private string _vcfilename;
        private string _vcmeno;
    }
}
