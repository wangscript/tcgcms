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
    public class FileCategories : EntityBase
    {
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
        /// <summary>
        /// ��Կ
        /// </summary>
        public string Key { get { return this._key; } set { this._key = value; } }
        /// <summary>
        /// ����ʹ�õ����ռ�
        /// </summary>
        public long MaxSpace { get { return this._maxspace; } set { this._maxspace = value; } }
        /// <summary>
        /// ʣ��ռ�
        /// </summary>
        public long Space { get { return this._space; } set { this._space = value; } }
        
      
        private int _iid;
        private int _iparentid;
        private DateTime _dcreatedate;
        private string _vcfilename;
        private string _vcmeno;
        private string _key;
        private long _maxspace;
        private long _space;
    }
}
