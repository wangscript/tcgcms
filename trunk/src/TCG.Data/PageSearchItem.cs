using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TCG.Data
{
    public class PageSearchItem
    {
        /// <summary>
        /// ����
        /// </summary>
        public string tableName { get { return this._tablename; } set { this._tablename = value; } }
        /// <summary>
        /// ��Ҫ������ʾ���ֶμ�
        /// </summary>
        public ArrayList arrShowField { get { return this._arrshowfield; } set { this._arrshowfield = value; } }
        /// <summary>
        /// ��ѯ�����ֶμ�
        /// </summary>
        public string strCondition { get { return this._strcondition; } set { this._strcondition = value; } }
        /// <summary>
        /// �����ֶμ�
        /// </summary>
        public ArrayList arrSortField { get { return this._arrsortfield; } set { this._arrsortfield = value; } }
        /// <summary>
        /// ÿҳ��ʾ�ļ�¼����
        /// </summary>
        public int pageSize { get { return this._pagesize; } set { this._pagesize = value; } }
        /// <summary>
        /// Ҫ��ʾ��ҳ����
        /// </summary>
        public int page { get { return this._page; } set { this._page = value; } }

        private string _tablename;
        private ArrayList _arrshowfield;
        private string _strcondition;
        private ArrayList _arrsortfield;
        private int _pagesize;
        private int _page;
    }
}
