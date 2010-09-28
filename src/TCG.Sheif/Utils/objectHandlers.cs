using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TCG.Sheif
{
    public class objectHandlers
    {

        /// <summary>
        /// 操作 DateTime  数据
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <returns>DateTime</returns>
        public static DateTime ToTime(string Object) { return ToTime(Object, DateTime.Now); }
        /// <summary>
        /// 字符串转换为时间函数
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <param name="Default">默认时间</param>
        /// <returns>DateTime</returns>
        public static DateTime ToTime(string Object, DateTime Default)
        {
            if (string.IsNullOrEmpty(Object)) return Default;
            bool IsTrue = false;
            DateTime Time = IsTime(Object, out IsTrue);
            if (!IsTrue) return Default;
            return Time;
        }

        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsTrue">返回是否转换成功</param>
        /// <returns>DateTime</returns>
        public static DateTime IsTime(object Object, out bool IsTrue)
        {
            IsTrue = false;
            if (Object==null) return DateTime.Now;
            try { IsTrue = true; return DateTime.Parse(Object.ToString()); }
            catch { IsTrue = false; return DateTime.Now; }
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
                return StrToBool(expression, defValue);

            return defValue;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                    return true;
                else if (string.Compare(expression, "false", true) == 0)
                    return false;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型,转换失败返回0
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str)
        {
            return StrToInt(str, 0);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(str, defValue));
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object strValue)
        {
            return ObjectToFloat(strValue.ToString(), 0);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue)
        {
            if ((strValue == null))
                return 0;

            return StrToFloat(strValue.ToString(), 0);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
                return defValue;

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        public static string NoHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");

            return Htmlstring;
        }

        /// <summary>
        /// 取字符左函数
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string Left(object Object, int MaxLength)
        {
            if (IsNull(Object)) return "";
            return Object.ToString().Substring(0, Math.Min(Object.ToString().Length, MaxLength));
        }
        /// <summary>
        /// 取字符中间函数
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="StarIndex">开始的位置索引</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string Mid(string Object, int StarIndex, int MaxLength)
        {
            if (IsNull(Object)) return "";
            if (StarIndex >= Object.Length) return "";
            return Object.Substring(StarIndex, MaxLength);
        }
        /// <summary>
        /// 取字符右函数
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string Right(object Object, int MaxLength)
        {
            if (IsNull(Object)) return "";
            int i = Object.ToString().Length;
            if (i < MaxLength) { MaxLength = i; i = 0; } else { i = i - MaxLength; }
            return Object.ToString().Substring(i, MaxLength);
        }

        /// <summary>
        /// 对象是否为空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <returns>bool值</returns>
        public static bool IsNull(object Object) { return IsNull(Object, false); }
        /// <summary>
        /// 对象是否为空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsRemoveSpace">是否去除空格</param>
        /// <returns>bool值</returns>
        public static bool IsNull(object Object, bool IsRemoveSpace)
        {
            if (Object == null) return true;
            string Objects = Object.ToString();
            if (Objects == "") return true;
            if (IsRemoveSpace)
            {
                if (Objects.Replace(" ", "") == "") return true;
                if (Objects.Replace("　", "") == "") return true;
            }
            return false;
        }



    }
}
