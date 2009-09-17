using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace TCG.Template
{
    public class TagItem
    {
        public TagItem()
        {
            this._attpattern = @"=""([0-9A-Za-z\s\,\=]*)""";
        }

        public string Tag { get { return this._tag; } set { this._tag = value; } }
        public string TagText { get { return this._tagtext; } set { this._tagtext = value; } }
        public string TagType { get { return this._tagtype; } set { this._tagtype = value; } }
        public string Attribute { get { return this._attribute; } set { this._attribute = value; } }

        public string Replace(string TempHtml)
        {
            return TempHtml.Replace(this._tag, this._tagtext);
        }

        public void ReplaceTagText(DataTable dt)
        {
            if (this._tagtype == string.Empty || this._tagtext == string.Empty || this._attribute == string.Empty || this._tag == string.Empty) return;
            string feild = this.GetAttribute("feilds");
            if (feild == "") return;
            string strHtml = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string temp = this._tagtext;
                if (feild.IndexOf(",") > 0)
                {
                    string[] feilds = feild.Split(',');
                    for (int n = 0; n < feilds.Length; n++)
                    {
                        temp = temp.Replace("$" + feilds[n] + "$", dt.Rows[i][feilds[n]].ToString());
                    }
                }
                else
                {
                    temp = temp.Replace("$" + feild + "$", dt.Rows[i][feild].ToString());
                }

                temp = temp.Replace("_$n$_", (i + 1).ToString());
                strHtml += temp;
            }
            this._tagtext = strHtml;
        }

        public string GetAttribute(string feild)
        {
            string value = "";
            Match item = Regex.Match(this._attribute, feild + this._attpattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (item.Success)
            {
                value = item.Result("$1");
            }
            item = null;
            return value;
        }

        private string _attpattern = string.Empty;
        private string _attribute = string.Empty;
        private string _tagtype = string.Empty;
        private string _tagtext = string.Empty;
        private string _tag = string.Empty;
    }
}