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

namespace TCG.Entity
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ConfigItem
    {
        private string _name;
        private string _explain;
        private string _mode;
        private string _value;
        private string _pattern;
        private string _range;
        private string _rangeHint;
        private bool _isRequired;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public string Explain
        {
            get
            {
                return this._explain;
            }
            set
            {
                this._explain = value;
            }
        }
        public string Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
            }
        }
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
        public string Pattern
        {
            get
            {
                return this._pattern;
            }
            set
            {
                this._pattern = value;
            }
        }
        public string Range
        {
            get
            {
                return this._range;
            }
            set
            {
                this._range = value;
            }
        }
        public string RangeHint
        {
            get
            {
                return this._rangeHint;
            }
            set
            {
                this._rangeHint = value;
            }
        }
        public bool IsRequired
        {
            get
            {
                return this._isRequired;
            }
            set
            {
                this._isRequired = value;
            }
        }
    }
}

