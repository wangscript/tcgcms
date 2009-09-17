namespace TCG.Utils
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public class UbbCode
    {
        public UbbCode()
        {
        }

        public UbbCode(string content, int usingTag, bool enableFormat)
        {
            this._content = content;
            this._usingTag = usingTag;
            this._enableFormat = enableFormat;
        }

        private string FindUrl(string s)
        {
            string[] textArray1 = new string[7];
            string[] textArray2 = new string[7];
            textArray1[0] = @"\[url=([^\[]{5,})\]([^\[]+)\[\/url\]";
            textArray2[0] = "<a href='<~>$1' target='_blank'>$2</a>";
            textArray1[1] = @"\[url\]([^\[]{5,})\[\/url\]";
            textArray2[1] = "<a href='<~>$1' target='_blank'>$1</a>";
            textArray1[2] = @"\[email\]([^\s@]+@[^\[\.]+\.[^\[]+)\[\/email\]";
            textArray2[2] = "<a href='mailto:$1'>$1</a>";
            textArray1[3] = @"\[email=([^\s@]+@[^\[\.]+\.[^\[]+)\]([^\[]+)\[\/email\]";
            textArray2[3] = "<a href='mailto:$1'>$2</a>";
            textArray1[4] = @"([^>=\]])((http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*)";
            textArray2[4] = "$1<a href='$2' target='_blank'>$2</a>";
            textArray1[5] = @"(^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*)";
            textArray2[5] = "<a href='$1' target='_blank'>$1</a>";
            textArray1[6] = @"(^|\n)(www\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]+)";
            textArray2[6] = "<a href='http://$2' target='_blank'>$2</a>";
            return this.RegexReplace(s, textArray1, textArray2, RegexOptions.IgnoreCase);
        }

        private string FixRelativePath(string content)
        {
            RegexOptions options1 = RegexOptions.IgnoreCase;
            string text1 = Regex.Replace(content, @" (href|src)=\'(javascript|vbscript|script):([^\']*)\'", " $1='$3'", options1);
            text1 = Regex.Replace(text1, @" (href|src)=\'<~>(\/|[a-zA-Z]+:)([^\']*)\'", " $1='$2$3'", options1);
            return Regex.Replace(text1, @" (href|src)=\'<~>([^\']*)\'", " $1='" + Fetch.PathUpSeek + "/$2'", options1);
        }

        public string GetClientSideContent()
        {
            return this.GetClientSideContent(this._content, this._usingTag, this._enableFormat);
        }

        public string GetClientSideContent(string content, int usingTag, bool enableFormat)
        {
            StringBuilder builder1 = new StringBuilder(this.HideAttachImageAddress(content.Trim()));
            builder1.Replace(@"\", @"\\");
            builder1.Replace("\n", @"\n");
            builder1.Replace("\r", @"\r");
            char ch1 = '"';
            ch1 = '"';
            builder1.Replace(ch1.ToString(), @"\" + ch1.ToString());
            builder1.Replace("<!--", "<!\" + \"--");
            builder1.Replace("-->", "--\" + \">");
            builder1.Insert(0, "<script language='javascript'>\r\n<!--\n\toutput = \"");
            builder1.AppendFormat("\";\r\n\tubb.print(output, '{0}', {1}, {2});\r\n//-->\r\n</script>", Fetch.PathUpSeek, usingTag, enableFormat ? 1 : 0);
            return builder1.ToString();
        }

        public string GetServerSideSimpleUbbContent(string content)
        {
            string text1 = this.HideAttachImageAddress(content);
            text1 = this.RemoveExtraLineBreaks(text1);
            text1 = Text.HtmlEncode(text1);
            text1 = this.SetFontStyle(text1);
            text1 = this.FindUrl(text1);
            return this.FixRelativePath(text1);
        }

        private string HideAttachImageAddress(string content)
        {
            RegexOptions options1 = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            string text1 = @"(\[uploadimage)(=left|=right|)(\][0-9]+,)[0-9a-zA-Z_\.\/\\]+(\[\/uploadimage\])";
            return Regex.Replace(content, text1, "$1$2$3Protected$4", options1);
        }

        private string RegexReplace(string input, string[] pattern, string[] replacement, RegexOptions ro)
        {
            string text1 = input;
            for (int num1 = 0; num1 < pattern.Length; num1++)
            {
                text1 = Regex.Replace(text1, pattern[num1], replacement[num1], ro);
            }
            return text1;
        }

        private string RemoveExtraLineBreaks(string s)
        {
            string[] textArray1 = new string[3];
            string[] textArray2 = new string[3];
            textArray1[0] = @"\r";
            textArray2[0] = "";
            textArray1[1] = @"[\n]{2,}";
            textArray2[1] = "\n\n";
            textArray1[2] = @"\n";
            textArray2[2] = "\r\n";
            return this.RegexReplace(s, textArray1, textArray2, RegexOptions.IgnoreCase);
        }

        private string SetFontStyle(string s)
        {
            string[] textArray1 = new string[5];
            string[] textArray2 = new string[5];
            textArray1[0] = @"\[((\/?b)|(\/?i)|(\/?u)|(\/?h[1-6])|(\/?sub)|(\/?sup)|(\/?center))\]";
            textArray2[0] = "<$1>";
            textArray1[1] = @"\[color=(#[A-Fa-f0-9]{3}|#[A-Fa-f0-9]{6}|[a-zA-Z]{3,12})\]";
            textArray2[1] = "<span style='color:$1'>";
            textArray1[2] = @"\[size=(1[1-9])\]";
            textArray2[2] = "<span style='font-size:$1px'>";
            textArray1[3] = @"\[face=([^\]]+)\]";
            textArray2[3] = "<span style='font-family:$1'>";
            textArray1[4] = @"\[\/(color|size|face)\]";
            textArray2[4] = "</span>";
            return this.RegexReplace(s, textArray1, textArray2, RegexOptions.IgnoreCase);
        }


        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public bool EnableFormat
        {
            get
            {
                return this._enableFormat;
            }
            set
            {
                this._enableFormat = value;
            }
        }

        public int UsingTag
        {
            get
            {
                return this._usingTag;
            }
            set
            {
                this._usingTag = value;
            }
        }


        private string _content;
        private bool _enableFormat;
        private int _usingTag;
    }
}

