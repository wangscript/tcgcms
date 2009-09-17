namespace TCG.Controls
{
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web.UI;

    public class AttributeCollection
    {
        public AttributeCollection()
        {
            this.m_attrColl = string.Empty;
        }

        public AttributeCollection(string attributes)
        {
            if (attributes == null)
            {
                this.m_attrColl = string.Empty;
            }
            else
            {
                this.m_attrColl = attributes;
            }
        }

        public void Add(string name, string value)
        {
            if ((value != null) && (value != string.Empty))
            {
                string text1 = " " + name.ToLower() + "='";
                string text2 = value.Replace("'", "\"");
                if (this.m_attrColl.IndexOf(text1) != -1)
                {
                    switch (text1)
                    {
                        case " style='":
                            this.m_attrColl = this.m_attrColl.Replace(text1, text1 + text2 + "; ");
                            return;

                        case " class='":
                            this.m_attrColl = this.m_attrColl.Replace(text1, text1 + text2 + " ");
                            return;
                    }
                    this.Remove(name);
                }
                this.m_attrColl = this.m_attrColl + (text1 + text2 + "'");
            }
        }

        public void Clear()
        {
            this.m_attrColl = string.Empty;
        }

        private Regex CreateRegex(string name)
        {
            string text1 = "(^.*) " + name + "='([^']*)'(.*$)";
            return new Regex(text1, RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        public string Get(string name)
        {
            Regex regex1 = this.CreateRegex(name);
            if (!regex1.IsMatch(this.m_attrColl))
            {
                return string.Empty;
            }
            return regex1.Replace(this.m_attrColl, "$2");
        }

        public void Remove(string name)
        {
            Regex regex1 = this.CreateRegex(name);
            if (regex1.IsMatch(this.m_attrColl))
            {
                this.m_attrColl = regex1.Replace(this.m_attrColl, "$1$3");
            }
        }

        public virtual void Render(HtmlTextWriter output)
        {
            output.Write(this.m_attrColl);
        }

        public override string ToString()
        {
            return this.m_attrColl;
        }


        public string this[string index]
        {
            get
            {
                return this.Get(index);
            }
            set
            {
                this.Add(index, value);
            }
        }


        private string m_attrColl;
    }
}

