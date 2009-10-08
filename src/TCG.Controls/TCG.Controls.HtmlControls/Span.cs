
/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

namespace TCG.Controls.HtmlControls
{
    using System;
    using System.Web.UI;

    public class Span : Control, IAttributeAccessor
    {
        protected override void AddParsedSubObject(object obj)
        {
            if (obj is LiteralControl)
            {
                this._text = ((LiteralControl) obj).Text;
            }
            base.AddParsedSubObject(obj);
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (this.Attributes.ToString() == string.Empty)
            {
                output.Write(this._text);
            }
            else
            {
                output.Write(string.Concat(new object[] { "<span", this.Attributes, ">", this._text, "</span>" }));
            }
        }

        string IAttributeAccessor.GetAttribute(string name)
        {
            return this.Attributes[name];
        }

        void IAttributeAccessor.SetAttribute(string name, string value)
        {
            this.Attributes[name] = value;
        }


        public TCG.Controls.AttributeCollection Attributes
        {
            get
            {
                if (this._attrColl == null)
                {
                    this._attrColl = new TCG.Controls.AttributeCollection();
                }
                return this._attrColl;
            }
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


        private TCG.Controls.AttributeCollection _attrColl;
        private string _text;
    }
}

