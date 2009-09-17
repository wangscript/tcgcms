namespace TCG.Controls.HtmlControls
{
    using System;
    using System.Web.UI;
    using TCG.Controls;

    public class Anchor : Control, IAttributeAccessor
    {
        protected override void AddParsedSubObject(object obj)
        {
            if (obj is LiteralControl)
            {
                this._text = ((LiteralControl) obj).Text;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            if ((this._text != null) && (this._text != string.Empty))
            {
                this.Attributes.Add("href", this._href);
                if (this.Attributes.ToString() == string.Empty)
                {
                    output.Write(this._text);
                }
                else
                {
                    output.Write(string.Concat(new object[] { "<a", this.Attributes, ">", this._text, "</a>" }));
                }
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

        public string Href
        {
            get
            {
                return this._href;
            }
            set
            {
                this._href = value;
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
        private string _href;
        private string _text;
    }
}

