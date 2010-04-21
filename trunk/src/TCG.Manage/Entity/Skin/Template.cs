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
    public class Template : EntityBase
    {
        /// <summary>
        /// Ĥ�������
        /// </summary>
        public string Content { get { return this._vccontent; } set { this._vccontent = value; } }
        
        /// <summary>
        /// Ƥ��ID
        /// </summary>
        public string SkinId { get { return this._isiteid; } set { this._isiteid = value; } }
        /// <summary>
        /// ģ������
        /// </summary>
        public TemplateType TemplateType { get { return this._itype; } set { this._itype = value; } }
        /// <summary>
        /// ģ�常��ID
        /// </summary>
        public string iParentId { get { return this._iparentid; } set { this._iparentid = value; } }
        public int iSystemType { get { return this._isystemtype; } set { this._isystemtype = value; } }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime dAddDate { get { return this._dadddate; } set { this._dadddate = value; } }
        /// <summary>
        /// ģ������
        /// </summary>
        public string vcTempName { get { return this._vctempname; } set { this._vctempname = value; } }
        /// <summary>
        /// ��ҳURL��ַ
        /// </summary>
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }

        private string _vccontent;
        private string _iid;
        private string _isiteid;
        private TemplateType _itype;
        private string _iparentid;
        private int _isystemtype;
        private DateTime _dupdatedate;
        private DateTime _dadddate;
        private string _vctempname;
        private string _vcurl;
    }
}
