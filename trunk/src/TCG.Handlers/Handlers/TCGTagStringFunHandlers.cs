
/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ƹ�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    public class TCGTagStringFunHandlers
    {
        /// <summary>
        /// ������������ID
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public string StringConditionFun(Connection conn, string str)
        {
            string pattern = @"\$TCG.GetAllClassID\(([0-9]+)\)";
            MatchCollection matchs = this.GetMatchs(pattern, str);
            if (matchs.Count > 0)
            {
                CategoriesHandlers clhds = new CategoriesHandlers();

                foreach (Match item in matchs)
                {
                    str = str.Replace(item.Value, clhds.GetAllChildCategoriesIdByCategoriesId(conn, objectHandlers.ToInt(item.Result("$1")),false));
                }
                clhds = null;
            }
            matchs = null;
            return str;
        }

        public string StringColoumFun(Connection conn, string str, bool findtcgstringfunByF)
        {
            string pattern = @"\$TCG.CutStrLeft\(<TCG>([\S\s]*?)</TCG>\,([0-9]+)\)";
            string text1 = "";
            bool findtcgstringfun = false;
            MatchCollection matchs = this.GetMatchs(pattern, str);
            if (matchs.Count > 0)
            {
                findtcgstringfun = true;
                foreach (Match item in matchs)
                {
                    text1 = this.StringColoumFun(conn, item.Result("$1"), findtcgstringfun);
                    try
                    {
                        str = str.Replace(item.Value, objectHandlers.Left(text1,objectHandlers.ToInt(item.Result("$2"))));
                    }
                    catch { }
                }
            }
            matchs = null;

            pattern = @"\$TCG.GetTextWithoutHtml\(<TCG>([\S\s]*?)</TCG>\)";
            matchs = this.GetMatchs(pattern, str);
            if (matchs.Count > 0)
            {
                findtcgstringfun = true;
                foreach (Match item in matchs)
                {
                    text1 = this.StringColoumFun(conn, item.Result("$1"), findtcgstringfun);
                    str = str.Replace(item.Value, objectHandlers.GetTextWithoutHtml(text1));
                }
            }
            matchs = null;

            pattern = @"\$TCG.TimeFormat\(<TCG>([\S\s]*?)</TCG>\,""(([\S\s]*?))""\)";
            matchs = this.GetMatchs(pattern, str);
            if (matchs.Count > 0)
            {
                findtcgstringfun = true;
                foreach (Match item in matchs)
                {
                    try
                    {
                        str = str.Replace(item.Value, objectHandlers.ToTime(item.Result("$1")).ToString(item.Result("$2")));
                    }
                    catch { }
                }
            }
            matchs = null;

            if (!findtcgstringfunByF)
            {
                pattern = @"<TCG>([\S\s]*?)</TCG>";
                matchs = this.GetMatchs(pattern, str);
                if (matchs.Count > 0)
                {
                    foreach (Match item in matchs)
                    {
                        str = str.Replace(item.Value, item.Result("$1"));
                    }
                }
            }
            matchs = null;
            return str;
        }

        private MatchCollection GetMatchs(string pattern, string str)
        {
            return Regex.Matches(str,pattern , RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
    }
}
