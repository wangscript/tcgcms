namespace TCG.Controls.PageControls
{
    using TCG.Utils;
    using System;
    using System.Web.UI;

    [ControlBuilder(typeof(CodeVerifierControlBuilder))]
    public class PostVerifier : Control
    {
        public PostVerifier()
        {
            this._disabled = false;
            this._filePath = "image.aspx";
            this._delimiter = "\u3000";
            this._showHelpText = false;
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj is VerifierTextBoxAttribute)
            {
                this._textBoxAttribute = ((VerifierTextBoxAttribute) obj).Attributes.ToString();
            }
            if (obj is VerifierCodeImageAttribute)
            {
                this._codeImageAttribute = ((VerifierCodeImageAttribute) obj).Attributes.ToString();
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (!base.Visible || this.Disabled)
            {
                output.Write("<i>验证码已停用</i> ");
                SessionState.Set("verification", null);
            }
            else
            {
                TCG.Controls.AttributeCollection collection1 = new TCG.Controls.AttributeCollection(this._codeImageAttribute);
                string text1 = "?";
                string[] textArray1 = new string[] { "width", "height", "fontsize", "fontfamily", "bold", "italic", "impurity" };
                foreach (string text2 in textArray1)
                {
                    string text3 = collection1.Get(text2).Trim();
                    if (text3 != string.Empty)
                    {
                        string text4 = text1;
                        text1 = text4 + text2 + "=" + text3 + "&";
                    }
                }
                output.Write("<input type='text' name='verification'" + this._textBoxAttribute + " />" + this._delimiter);
                output.Write("<script language='javascript'> ");
                output.Write("document.write(\"<img src='" + this._filePath + text1 + "time=\", new Date(), \"' alt='验证码图片, 看不清数字请刷新' " + this._codeImageAttribute + " />\"); ");
                output.Write("</script>");
                if (this._showHelpText)
                {
                    output.Write(" &nbsp;请将图片中的数字填入框中");
                }
            }
        }


        public string Delimiter
        {
            set
            {
                this._delimiter = value;
            }
        }

        public bool Disabled
        {
            get
            {
                return this._disabled;
            }
            set
            {
                this._disabled = value;
            }
        }

        public string FilePath
        {
            set
            {
                this._filePath = value;
            }
        }

        public bool ShowHelpText
        {
            set
            {
                this._showHelpText = value;
            }
        }


        private string _codeImageAttribute;
        private string _delimiter;
        private bool _disabled;
        private string _filePath;
        private bool _showHelpText;
        private string _textBoxAttribute;
    }
}

