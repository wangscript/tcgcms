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
