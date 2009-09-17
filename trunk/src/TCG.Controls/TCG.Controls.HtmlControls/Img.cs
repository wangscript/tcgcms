namespace TCG.Controls.HtmlControls
{
    using System;
    using System.Web.UI;

    public class Img : Control, IAttributeAccessor
    {
        protected override void Render(HtmlTextWriter output)
        {
            if ((this._src != null) && (this._src != string.Empty))
            {
                if (this._href != null)
                {
                    output.Write("<a href='" + this._href + "'");
                    if (this._target != null)
                    {
                        output.Write(" target='" + this._target + "'");
                    }
                    output.Write(">");
                }
                output.Write("<img src='" + this._src + "'");
                if (this._alpha != null)
                {
                    string[] textArray1 = this._alpha.Split(new char[] { ',' });
                    if (textArray1.Length == 3)
                    {
                        output.Write(" style='filter:alpha(opacity=" + textArray1[0] + ")'");
                        output.Write(" onmouseover='this.filters.Alpha.opacity=\"" + textArray1[1] + "\"'");
                        output.Write(" onmouseout='this.filters.Alpha.opacity=\"" + textArray1[2] + "\"'");
                    }
                }
                output.Write(this.Attributes.ToString() + " border='0' />");
                if (this._href != null)
                {
                    output.Write("</a>");
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


        public string Alpha
        {
            set
            {
                this._alpha = value;
            }
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

        public string Src
        {
            get
            {
                return this._src;
            }
            set
            {
                this._src = value;
            }
        }

        public string Target
        {
            get
            {
                return this._target;
            }
            set
            {
                this._target = value;
            }
        }


        private string _alpha;
        private TCG.Controls.AttributeCollection _attrColl;
        private string _href;
        private string _src;
        private string _target;
    }
}

