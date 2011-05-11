using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Utils;

namespace TCG.Handlers
{
    public class HandlerFactory
    {

        public static IAdminHandlers CreateAdminHandlers()
        {
            string className = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"] + ".AdminHandlers";
            return (IAdminHandlers)Assembly.Load("TCG.Handlers.Imp").CreateInstance(className);
        }
    }
}
