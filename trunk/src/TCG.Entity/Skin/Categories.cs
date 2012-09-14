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
        /// ��������ϸҳ���ɵ�ַ
        /// </summary>
        public string vcDirectory { get { return this._vcdirectory; } set { this._vcdirectory = value; } }
        /// <summary>
        /// �����б��ַ
        /// </summary>
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public string cVisible { get { return this._cvisible; } set { this._cvisible = value; } }
        /// <summary>
        /// ָ�����ݿ�����
        /// </summary>
        public string DataBaseService { get { return this._dbService; } set { this._dbService = value; } }
        /// <summary>
        ///  ����Ƥ����ID
        /// </summary>
        public Skin SkinInfo { get { return this._skin; } set { this._skin = value; } }
        /// <summary>
        /// �Ƿ�Ϊ��ҳ
        /// </summary>
        public string IsSinglePage { get { return this._sssinglepage; } set { this._sssinglepage = value; } }
        /// <summary>
        /// ����չʾͼƬ
        /// </summary>
        public string vcPic { get { return this._pic; } set { this._pic = value; } }
        /// <summary>
        /// ��Դ����
        /// </summary>
        public string vcSpeciality { get { return this._vcSpeciality; } set { this._vcSpeciality = value; } }

        private string _iparent = string.Empty;
        private Template _itemplate;
        private Template _ilisttemplate;
        private int _iorder;
        private DateTime _dupdatedate;
        private string _vcclassname = string.Empty;
        private string _vcname = string.Empty;
        private string _vcdirectory = string.Empty;
        private string _vcurl = string.Empty;
        private string _cvisible = string.Empty;
        private string _dbService = null;
        private string _skinid = string.Empty;
        private string _sssinglepage = string.Empty;
        private Skin _skin;
        private string _pic = string.Empty;
        private string _vcSpeciality = string.Empty;
    }
}