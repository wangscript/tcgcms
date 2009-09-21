using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Release
{
    public class Versions
    {
        private static int Ver = 14;                         //系统版本号
        private static string SystemName = "TCG CMS System"; //系统名称

        public static string version
        {
            get
            {
                return SystemName + " Version " + GetVerStr(Ver);
            }
        }
        
        public static string GetVerStr(int ver)
        {

            double v1, v2, v3, v4;

            v1 = ver / 1000;
            v2 = ver / 100;
            v3 = ver / 10;
            v4 = ver / 1;


            return Math.Floor(v1).ToString() + "." + Math.Floor(v2).ToString() + "."
                + Math.Floor(v3).ToString() + "." + Math.Floor(v4).ToString();
        }

        public static string Author
        {
            get
            {
                return "≮三云鬼≯";
            }
        }

        public static string WebSite
        {
            get
            {
                return "http://www.tcgcms.cn/";
            }
        }
    }
}
