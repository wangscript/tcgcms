using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 基本物品类
    /// </summary>
    public class Item
    {


        public Item(string text, string value)
        {
            this._text = text;
            this._value = value;
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
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

        private string _text;
        private string _value;
    }
}