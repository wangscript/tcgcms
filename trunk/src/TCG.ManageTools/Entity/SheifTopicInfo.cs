using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Sheif
{
    public class SheifTopicInfo
    {
        private string _title;
        private string _url;
        private string _content;

        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
    }
}
