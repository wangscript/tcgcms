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
    /// ��Դ��Ϣ
    /// </summary>
    public class Resources : EntityBase
    {
        /// <summary>
        /// ��Դ������Ϣ
        /// </summary>
        public Categories Categorie { get { return this._iClassID; } set { this._iClassID = value; } }
        /// <summary>
        /// ��Դ����
        /// </summary>
        public string vcTitle { get { return this._vcTitle; } set { this._vcTitle = value; } }
        /// <summary>
        /// ��Դ��ת��ַ
        /// </summary>
        public string vcUrl { get { return this._vcUrl; } set { this._vcUrl = value; } }
        /// <summary>
        /// ��Դ����
        /// </summary>
        public string vcContent { get { return this._vcContent; } set { this._vcContent = value; } }
        /// <summary>
        /// ��Դ����
        /// </summary>
        public string vcAuthor { get { return this._vcAuthor; } set { this._vcAuthor = value; } }
        /// <summary>
        /// ��Դ������
        /// </summary>
        public int iCount { get { return this._iCount; } set { this._iCount = value; } }
        /// <summary>
        /// ��Դ�ؼ���
        /// </summary>
        public string vcKeyWord { get { return this._vcKeyWord; } set { this._vcKeyWord = value; } }
        /// <summary>
        /// ��Դ�༭��
        /// </summary>
        public string vcEditor { get { return this._vcEditor; } set { this._vcEditor = value; } }
        /// <summary>
        /// ��Դ�Ƿ�����
        /// </summary>
        public string cCreated { get { return this._cCreated; } set { this._cCreated = value; } }
        /// <summary>
        /// ��Դ������
        /// </summary>
        public string cPostByUser { get { return this._cPostByUser; } set { this._cPostByUser = value; } }
        /// <summary>
        /// ��ԴСͼ���û��б�
        /// </summary>
        public string vcSmallImg { get { return this._vcSmallImg; } set { this._vcSmallImg = value; } }
        /// <summary>
        /// ��Դ��ͼ������ͼƬչʾ
        /// </summary>
        public string vcBigImg { get { return this._vcBigImg; } set { this._vcBigImg = value; } }
        /// <summary>
        /// ��Դ���
        /// </summary>
        public string vcShortContent { get { return this._vcShortContent; } set { this._vcShortContent = value; } }
        /// <summary>
        /// ��Դ����
        /// </summary>
        public string vcSpeciality { get { return this._vcSpeciality; } set { this._vcSpeciality = value; } }
        /// <summary>
        /// ��Դ���״̬
        /// </summary>
        public string cChecked { get { return this._cChecked; } set { this._cChecked = value; } }
        /// <summary>
        /// ��Դ�Ƿ�ɾ��
        /// </summary>
        public string cDel { get { return this._cDel; } set { this._cDel = value; } }
        /// <summary>
        /// ��Դ·��
        /// </summary>
        public string vcFilePath { get { return this._vcFilePath; } set { this._vcFilePath = value; } }
        /// <summary>
        /// ��Դ���ʱ��
        /// </summary>
        public DateTime dAddDate { get { return this._dadddate; } set { this._dadddate = value; } }
        /// <summary>
        /// ��Դ�޸�ʱ��
        /// </summary>
        public DateTime dUpDateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// ��Դ������ɫ
        /// </summary>
        public string vcTitleColor { get { return this._vctitlecolor; } set { this._vctitlecolor = value; } }
        /// <summary>
        /// ��Դ�����Ƿ�Ӵ�
        /// </summary>
        public string cStrong { get { return this._cstrong; } set { this._cstrong = value; } }
        /// <summary>
        /// ץȡURL
        /// </summary>
        public string SheifUrl { get { return this._sheifurl; } set { this._sheifurl = value; } }

        /// <summary>
        /// ���Է���ID
        /// </summary>
        public int PropertiesCategorieId { get; set; }

        public string GetUrl()
        {
            return string.IsNullOrEmpty(this.vcUrl) ? this.vcFilePath : this.vcUrl;
        }
         

        private Categories _iClassID = new Categories();
        private string _vcTitle = string.Empty;
        private string _vcUrl = string.Empty;
        private string _vcContent = string.Empty;
        private string _vcAuthor = string.Empty;
        private int _iCount = 0;
        private string _vcKeyWord = string.Empty;
        private string _vcEditor = string.Empty;
        private string _cCreated = string.Empty;
        private string _cPostByUser = string.Empty;
        private string _vcSmallImg = string.Empty;
        private string _vcBigImg = string.Empty;
        private string _vcShortContent = string.Empty;
        private string _vcSpeciality = string.Empty;
        private string _cChecked = string.Empty;
        private string _cDel = "N";
        private string _vcFilePath = string.Empty;
        private DateTime _dadddate = DateTime.Now;
        private DateTime _dupdatedate;
        private string _vctitlecolor;
        private string _cstrong;
        private string _sheifurl;
    }
}
