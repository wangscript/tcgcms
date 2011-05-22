using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Utils;

namespace TCG.Handlers
{
    public class DataBaseFactory
    {

        public static IAdminHandlers AdminHandlers
        {
            get
            {
                if (_adminhandlers == null)
                {
                    string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
                    string className = assc + ".AdminHandlers";
                    _adminhandlers = (IAdminHandlers)Assembly.Load(assc).CreateInstance(className);
                }
                return _adminhandlers;

            }
        }
        private static IAdminHandlers _adminhandlers;

        public static ICategoriesHandlers CategoriesHandlers
        {
            get
            {
                if (_categorieshandlers == null)
                {
                    string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
                    string className = assc + ".CategoriesHandlers";
                    _categorieshandlers = (ICategoriesHandlers)Assembly.Load(assc).CreateInstance(className);
                }
                return _categorieshandlers;
            }
        }
        private static ICategoriesHandlers _categorieshandlers;

        public static IResourceHandlers ResourceHandlers
        {
            get
            {
                if (_resourcehandlers == null)
                {
                    string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
                    string className = assc + ".ResourcesHandlers";
                    _resourcehandlers = (IResourceHandlers)Assembly.Load(assc).CreateInstance(className);
                }
                return _resourcehandlers;
            }
        }
        private static IResourceHandlers _resourcehandlers;

        public static ISkinHandlers SkinHandlers
        {
            get
            {
                if (_skinhandlers == null)
                {
                    string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
                    string className = assc + ".SkinHandlers";
                    _skinhandlers = (ISkinHandlers)Assembly.Load(assc).CreateInstance(className);
                }
                return _skinhandlers;
            }
        }
        private static ISkinHandlers _skinhandlers;

        public static ITemplateHandlers TemplateHandlers
        {
            get
            {
                if (_templatehandlers == null)
                {
                    string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
                    string className = assc + ".TemplateHandlers";
                    _templatehandlers = (ITemplateHandlers)Assembly.Load(assc).CreateInstance(className);
                }
                return _templatehandlers;
            }
        }
        private static ITemplateHandlers _templatehandlers;

        public static IFileHandlers FileHandlers
        {
            get
            {
                if (_filehandlers == null)
                {
                    string assc = "TCG.Handlers.Imp." + ConfigServiceEx.baseConfig["dbtype"];
                    string className = assc + ".FileHandlers";
                    _filehandlers = (IFileHandlers)Assembly.Load(assc).CreateInstance(className);
                }
                return _filehandlers;
            }
        }
        private static IFileHandlers _filehandlers;
    }
}
