namespace TCG.Controls.PageControls
{
    using System;
    using System.Web.UI;
    using TCG.Controls;

    public class VerifierTextBoxAttribute : Control, IAttributeAccessor
    {
        protected override void Render(HtmlTextWriter output)
        {
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


        private TCG.Controls.AttributeCollection _attrColl;
    }
}

