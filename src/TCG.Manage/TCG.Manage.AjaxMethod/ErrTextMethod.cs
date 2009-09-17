using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

using TCG.Utils;

namespace TCG.Manage.AjaxMethod
{
    public class ErrTextMethod
    {
        /// <summary>
        /// 根据错误代码获得错误文字
        /// </summary>
        /// <param name="ErrCode"></param>
        /// <returns></returns>
        [Ajax.AjaxMethod]
        public string GetErrTextByErrCode(int ErrCode)
        {
            string ErrTexts = TxtReader.Read(ConfigurationManager.ConnectionStrings["ErrTxtPath"].ToString());
            string errText = "";
            if (!string.IsNullOrEmpty(ErrTexts))
            {
                string patten = ErrCode.ToString() + @"@@@([A-Z_a-z]+)@@@([^-\r\n]+)";
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
