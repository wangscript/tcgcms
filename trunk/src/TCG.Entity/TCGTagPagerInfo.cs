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
    /// ��ǩ��ҳ���ݴ����ݣ��洢�ڱ�ǩ����е���ʱ����
    /// </summary>
    public class TCGTagPagerInfo
    {
        /// <summary>
        /// �����Ƿ���Ҫ��ҳ
        /// </summary>
        public bool NeedPager { get { return this._needpager; } set { this._needpager = value; } }
        /// <summary>
        /// ��ҳ����
        /// </summary>
        public int PageSep { get { return this._pagesep; } set { this._pagesep = value; } }
        /// <summary>
        /// �Ƿ��м�⵽��ǩ
        /// </summary>
        public bool Read { get { return this._read; } set { this._read = value; } }
        /// <summary>
        /// ��ҳʱ����ǰҳ
        /// </summary>
        public int Page { get { return this._page; } set { this._page = value; } }
        /// <summary>
        /// ��ҳʱ����ҳ��
        /// </summary>
        public int PageCount { get { return this._pgercount; } set { this._pgercount = value; } }
        /// <summary>
        /// ��ҳʱ����ǰҳ ����
        /// </summary>
        public int curPage { get { return this._curpage; } set { this._curpage = value; } }
        /// <summary>
        /// ��ҳʱ���ܼ�¼��
        /// </summary>
        public int TopicCount { get { return this._topiccount; } set { this._topiccount = value; } }
        /// <summary>
        /// ҳ��ı���
        /// </summary>
        public string PageTitle { get { return this._pagetitle; } set { this._pagetitle = value; } }
        /// <summary>
        /// ҳ��Ľű�����ʽ 
        /// </summary>
        public string ScriptCss { get { return this._scriptcss; } set { this._scriptcss = value; } }
        /// <summary>
        /// �Ƿ���������ҳ��
        /// </summary>
        public bool DoAllPage { get { return this._doallpage; } set { this._doallpage = value; } }
        /// <summary>
        /// �����ļ��б�
        /// </summary>
        public string CreatePagesNotic { get { return this._createpagesnotic; } set { this._createpagesnotic = value; } }

        private int _curpage = 1;
        private int _topiccount = 0;
        private int _page = 1;
        private int _pgercount = 0;
        private int _pagesep = 0;
        private bool _needpager = false;
        private bool _read = true;
        private string _pagetitle = string.Empty;
        private string _scriptcss = string.Empty;
        private bool _doallpage = true; // �Ƿ���������ҳ��
        private string _createpagesnotic = string.Empty;
    }
}
