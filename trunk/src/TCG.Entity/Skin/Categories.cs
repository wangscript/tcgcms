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
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    public class Categories : EntityBase
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public string Parent { get { return this._iparent; } set { this._iparent = value; } }
        /// <summary>
        /// ����ģ��
        /// </summary>
        public Template ResourceTemplate { get { return this._itemplate; } set { this._itemplate = value; } }
        /// <summary>
        /// �б�ģ��
        /// </summary>
        public Template ResourceListTemplate { get { return this._ilisttemplate; } set { this._ilisttemplate = value; } }
        /// <summary>
        /// ����
        /// </summary>
        public int iOrder { get { return this._iorder; } set { this._iorder = value; } }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// ��������
        /// </summary>
        public string vcClassName { get { return this._vcclassname; } set { this._vcclassname = value; } }
        /// <summary>
        /// ����
        /// </summary>
        public string vcName { get { return this._vcname; } set { this._vcname = value; } }
        /// <summary>
        /// ����·��
        /// </summary>
        public string vcDirectory { get { return this._vcdirectory; } set { this._vcdirectory = value; } }
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public string cVisible { get { return this._cvisible; } set { this._cvisible = value; } }
        /// <summary>
        /// ָ�����ݿ�����
        /// </summary>
        public string DataBaseService { get { return this._dbService; } set { this._dbService = value; } }

        
        private string _iparent;
        private Template _itemplate;
        private Template _ilisttemplate;
        private int _iorder;
        private DateTime _dupdatedate;
        private string _vcclassname;
        private string _vcname;
        private string _vcdirectory;
        private string _vcurl;
        private string _cvisible;
        private string _dbService = null;
    }
}