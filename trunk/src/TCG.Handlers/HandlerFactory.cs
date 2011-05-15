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

        public static ICategoriesHandlers CreateCategoriesHandlers()
        {
            string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
            string className = assc + ".CategoriesHandlers";
            return (ICategoriesHandlers)Assembly.Load(assc).CreateInstance(className);
        }

        public static IResourceHandlers CreateResourceHandlers()
        {
            string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
            string className = assc + ".ResourcesHandlers";
            return (IResourceHandlers)Assembly.Load(assc).CreateInstance(className);
        }

        public static ISkinHandlers CreateSkinHandlers()
        {
            string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
            string className = assc + ".SkinHandlers";
            return (ISkinHandlers)Assembly.Load(assc).CreateInstance(className);
        }

        public static ITemplateHandlers CreateTemplateHandlers()
        {
            string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
            string className = assc + ".TemplateHandlers";
            return (ITemplateHandlers)Assembly.Load(assc).CreateInstance(className);
        }

        public static IFileHandlers CreateFileHandlers()
        {
            string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
            string className = assc + ".FileHandlers";
            return (IFileHandlers)Assembly.Load(assc).CreateInstance(className);
        }
    }
}
