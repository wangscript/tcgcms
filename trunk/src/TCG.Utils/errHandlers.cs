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
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TCG.Utils
{
    public class errHandlers
    {
        /// <summary>
        /// 根据错误ID获得错误信息
        /// </summary>
        /// <param name="errCode"></param>
        public static string GetErrTextByErrCode(int errCode)
        {
            string ErrTexts = TxtReader.Read(ConfigurationManager.ConnectionStrings["ErrTxtPath"].ToString());
            string errText = "";
            if (!string.IsNullOrEmpty(ErrTexts))
            {
                string patten = errCode.ToString() + @"@@@([A-Z_a-z|]+)@@@([^-\r\n]+)";
                Match mt = Regex.Match(ErrTexts, patten, RegexOptions.Singleline);
                if (mt.Success)
                {
                    errText = mt.Result("$2");
                }
                mt = null;
            }
            return errText;
        }
    }
}
