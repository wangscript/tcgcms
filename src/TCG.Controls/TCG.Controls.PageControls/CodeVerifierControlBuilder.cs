namespace TCG.Controls.PageControls
{
    using System;
    using System.Collections;
    using System.Web.UI;

    public class CodeVerifierControlBuilder : ControlBuilder
    {
        public override Type GetChildControlType(string tagName, IDictionary attributes)
        {
            if (tagName.ToLower() == "textboxattribute")
            {
                return typeof(VerifierTextBoxAttribute);
            }
            if (tagName.ToLower() == "codeimageattribute")
            {
                return typeof(VerifierCodeImageAttribute);
            }
            return null;
        }

    }
}

