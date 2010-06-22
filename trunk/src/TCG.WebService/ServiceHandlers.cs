using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Text;

namespace TCG.WebService
{
    public class ServiceHandlers
    {

        public static TCGSoapHeader header;
        [SoapHeader("header", Direction = SoapHeaderDirection.In)]
        public static bool CheckHeader()
        {
            if (header == null)return false;

            return true;
        }
    }
}