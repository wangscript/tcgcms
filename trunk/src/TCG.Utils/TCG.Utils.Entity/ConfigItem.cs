namespace TCG.Utils.Entity
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

