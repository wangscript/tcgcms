/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */
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
            news.Add("单页");
            news.Add("资讯内页");
            news.Add("资讯列表");
            news.Add("原件模版");
            return news;
        }
    }
}
