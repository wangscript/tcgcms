using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace TCG.WebService
{
    public class TCGSoapHeader : SoapHeader
    {
        string _name;
        string _passWord;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }
    }
}

