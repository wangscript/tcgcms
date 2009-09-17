using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TCG.Template.Utils
{
    public struct TemplateConstant
    {
        public static int SinglePageType = 0;
        public static int InfoType = 1;
        public static int ListType = 2;
        public static int OriginalType = 3;

        public static string CACHING_AllTemplates = "AllTemplates";
        public static string CACHING_AllTemplates_System = "AllTemplates_System";


        public static int SystemType_News = 0; //

        public static ArrayList TypeNames()
        {
            ArrayList news = new ArrayList();
            news.Add("��ҳ");
            news.Add("��Ѷ��ҳ");
            news.Add("��Ѷ�б�");
            news.Add("ԭ��ģ��");
            return news;
        }
    }
}
