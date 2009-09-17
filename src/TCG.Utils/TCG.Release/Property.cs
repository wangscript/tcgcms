namespace TCG.Utils.Release
{
    using TCG.Utils;
    using System;
    using System.Configuration;

    public class Property
    {
        public static string AppPrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["AppPrefix"].ToString();
            }
        }

        public static string WebICP
        {
            get
            {
                return Config.Settings["WebICP"];
            }
        }

        public static string SystemName
        {
            get
            {
                return Config.Settings["SystemName"];
            }
        }

        public static string SystemVersion
        {
            get
            {
                return ("TCG System " + new Version(0,0,0,10) + "");
            }
        }

        public static string Designer
        {
            get
            {
                return Config.Settings["Designer"];
            }
        }

        public static bool IsProgramRegistered
        {
            get
            {
                return new Authorization("XZDSW").IsRegistered;
            }
        }

    }
}

