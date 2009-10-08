/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ι�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.News.Entity
{
    public class NewsFromInfo
    {
        public int iId { get { return this._iId; } set { this._iId = value; } }
        public DateTime dUpdateDate { get { return this._dUpdateDate; } set { this._dUpdateDate = value; } }
        public string vcTitle { get { return this._vcTitle; } set { this._vcTitle = value; } }
        public string vcUrl { get { return this._vcUrl; } set { this._vcUrl = value; } }

        private int _iId = 0;
        private DateTime _dUpdateDate;
        private string _vcTitle = string.Empty;
        private string _vcUrl = string.Empty;
    }
}
