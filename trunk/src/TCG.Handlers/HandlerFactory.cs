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
            string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
            string className = assc + ".AdminHandlers";
            return (IAdminHandlers)Assembly.Load(assc).CreateInstance(className);
        }
    }
}
