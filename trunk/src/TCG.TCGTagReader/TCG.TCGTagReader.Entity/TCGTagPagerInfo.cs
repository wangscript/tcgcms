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

namespace TCG.TCGTagReader.Entity
{
    public class TCGTagPagerInfo
    {
        public bool NeedPager { get { return this._needpager; } set { this._needpager = value; } }
        public bool Read { get { return this._read; } set { this._read = value; } }
        public int Page { get { return this._page; } set { this._page = value; } }
        public int PageCount { get { return this._pgercount; } set { this._pgercount = value; } }
        public int curPage { get { return this._curpage; } set { this._curpage = value; } }
        public int TopicCount { get { return this._topiccount; } set { this._topiccount = value; } }
        public string PageTitle { get { return this._pagetitle; } set { this._pagetitle = value; } }
        public string ScriptCss { get { return this._scriptcss; } set { this._scriptcss = value; } }
        public bool DoAllPage { get { return this._doallpage; } set { this._doallpage = value; } }

        private int _curpage = 1;
        private int _topiccount = 0;
        private int _page = 1;
        private int _pgercount = 0;
        private bool _needpager = false;
        private bool _read = true;
        private string _pagetitle = string.Empty;
        private string _scriptcss = string.Empty;
        private bool _doallpage = true; // �Ƿ���������ҳ��
    }
}
