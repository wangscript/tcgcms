/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ι�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Text;
using System.Web.Security;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;

namespace TCG.Utils
{
    /// <summary>
    /// ����������
    /// </summary>
	public class Bases
    {
        #region �����б���
        /// <summary>
        /// �����Ƿ�ǿ�
        /// Ϊ�շ��� false
        /// ��Ϊ�շ��� true
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <returns>boolֵ</returns>
        public static bool NotNull(object Object) { return !IsNull(Object, false); }
        /// <summary>
        /// �����Ƿ�ǿ�
        /// Ϊ�շ��� false
        /// ��Ϊ�շ��� true
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsRemoveSpace">�Ƿ�ȥ���ո�</param>
        /// <returns>boolֵ</returns>
        public static bool NotNull(object Object, bool IsRemoveSpace) { return !IsNull(Object, IsRemoveSpace); }
        /// <summary>
        /// �����Ƿ�Ϊ��
        /// Ϊ�շ��� false
        /// ��Ϊ�շ��� true
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <returns>boolֵ</returns>
        public static bool IsNull(object Object) { return IsNull(Object, false); }
        /// <summary>
        /// �����Ƿ�Ϊ��
        /// Ϊ�շ��� false
        /// ��Ϊ�շ��� true
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsRemoveSpace">�Ƿ�ȥ���ո�</param>
        /// <returns>boolֵ</returns>
        public static bool IsNull(object Object, bool IsRemoveSpace)
        {
            if (Object == null) return true;
            string Objects = Object.ToString();
            if (Objects == "") return true;
            if (IsRemoveSpace)
            {
                if (Objects.Replace(" ", "") == "") return true;
                if (Objects.Replace("��", "") == "") return true;
            }
            return false;
        }
        /// <summary>
        /// �����Ƿ�Ϊboolֵ
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <returns>boolֵ</returns>
        public static bool IsBool(object Object) { return IsBool(Object, false); }
        /// <summary>
        /// �ж��Ƿ�Ϊboolֵ
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="Default">Ĭ��boolֵ</param>
        /// <returns>boolֵ</returns>
        public static bool IsBool(object Object, bool Default)
        {
            if (IsNull(Object)) return Default;
            try { return bool.Parse(Object.ToString()); }
            catch { return Default; }
        }
        /// <summary>
        /// �Ƿ��ʼ���ַ
        /// </summary>
        /// <param name="Mail">�ȴ���֤���ʼ���ַ</param>
        /// <returns>bool</returns>
        public static bool IsMail(string Mail) { return Regex.IsMatch(Mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); }
        /// <summary>
        /// �Ƿ�URL��ַ
        /// </summary>
        /// <param name="HttpUrl">�ȴ���֤��Url��ַ</param>
        /// <returns>bool</returns>
        public static bool IsHttp(string HttpUrl) { return Regex.IsMatch(HttpUrl, @"^(http|https):\/\/[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]{2,}[A-Za-z0-9\.\/=\?%\-&_~`@[\]:+!;]*$"); }
        /// <summary>
        /// ȡ�ַ�����
        /// </summary>
        /// <param name="Object">Ҫ������ string  ����</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string Left(object Object, int MaxLength)
        {
            if (IsNull(Object)) return "";
            return Object.ToString().Substring(0, Math.Min(Object.ToString().Length, MaxLength));
        }
        /// <summary>
        /// ȡ�ַ��м亯��
        /// </summary>
        /// <param name="Object">Ҫ������ string  ����</param>
        /// <param name="StarIndex">��ʼ��λ������</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string Mid(string Object, int StarIndex, int MaxLength)
        {
            if (IsNull(Object)) return "";
            if (StarIndex >= Object.Length) return "";
            return Object.Substring(StarIndex, MaxLength);
        }
        /// <summary>
        /// ȡ�ַ��Һ���
        /// </summary>
        /// <param name="Object">Ҫ������ string  ����</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string Right(object Object, int MaxLength)
        {
            if (IsNull(Object)) return "";
            int i = Object.ToString().Length;
            if (i < MaxLength) { MaxLength = i; i = 0; } else { i = i - MaxLength; }
            return Object.ToString().Substring(i, MaxLength);
        }
        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="Object">Ҫ������ string  ����</param>
        /// <returns>string</returns>
        public static string MD5(string Object)
        {
            if (IsNull(Object)) return "";
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Object, "MD5");
        }
        /// <summary>
        /// �ж��Ƿ����Httpͷ�ĵ�ַ
        /// </summary>
        /// <param name="Object">Ҫ�����Ķ���</param>
        /// <returns>bool</returns>
        public static bool IsHttpUrl(string Object)
        {
            if (IsNull(Object)) return false;
            if (Left(Object, 7).ToLower() == "http://") return true;
            return false;
        }
        /// <summary>
        /// �ؼ�����ɫ����
        /// </summary>
        /// <param name="Object">Ҫ������ string  ����</param>
        /// <param name="Keys">�ؼ���</param>
        /// <param name="Style">��ʽ����</param>
        /// <returns>string</returns>
        public static string AddColors(string Object, string Keys, string Style)
        {
            StringBuilder Builders = new StringBuilder(Object);
            Builders.Replace(Keys, "<span class=\"" + Style + "\">" + Keys + "</span>");
            return Builders.ToString();
        }
        /// <summary>
        /// ����ǰ����0
        /// </summary>
        /// <param name="Int">Ҫ������ int  ����</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string AddZeros(int Int, int MaxLength) { return AddZeros(Int.ToString(), MaxLength); }
        /// <summary>
        /// ����ǰ����0
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="MaxLength">Ĭ�ϳ��Ȳ���0</param>
        /// <returns>�ַ�</returns>
        public static string AddZeros(string Object, int MaxLength)
        {
            int iLength = Object.Length;
            if (iLength < MaxLength)
            {
                for (int i = 1; i <= (MaxLength - iLength); i++) Object = "0" + Object;
            }
            return Object;
        }
        /// <summary>
        /// ���»�ȡ����ID����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>�ַ�</returns>
        public static string ToLoadID(string Object)
        {
            if (IsNull(Object)) return "";
            StringBuilder Builder = new StringBuilder("");
            string[] Atemp = Object.Split(',');
            for (int i = 0; i < Atemp.Length; i++)
            {
                bool IsTrue = false; int Int = IsInt(Atemp[i].ToString(), out IsTrue);
                if (IsTrue) Builder.Append(((i != 0) ? "," : "") + Int.ToString());
            }
            return Builder.ToString();
        }
        #endregion
        #region �ַ�ת������
        /// <summary>
        /// �ַ� int  ת��Ϊ char
        /// </summary>
        /// <param name="Int">�ַ�[int]</param>
        /// <returns>char</returns>
        public static char IntToChar(int Int) { return (char)Int; }
        /// <summary>
        /// �ַ� int  ת��Ϊ�ַ� string
        /// </summary>
        /// <param name="Int">�ַ� int</param>
        /// <returns>�ַ� string</returns>
        public static string IntToString(int Int) { return IntToChar(Int).ToString(); }
        /// <summary>
        /// �ַ� string  ת��Ϊ�ַ� int
        /// </summary>
        /// <param name="Strings">�ַ� string</param>
        /// <returns>�ַ� int</returns>
        public static int StringToInt(string Strings)
        {
            if (IsNull(Strings)) return -100; char[] chars = Strings.ToCharArray(); return (int)chars[0];
        }
        /// <summary>
        /// �ַ� string  ת��Ϊ char
        /// </summary>
        /// <param name="Strings">�ַ� string</param>
        /// <returns>char</returns>
        public static char StringToChar(string Strings) { return IntToChar(StringToInt(Strings)); }
        #endregion
        #region ���� int  ����
        /// <summary>
        /// �����Ƿ�Ϊ int  ��������
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsTrue">�����Ƿ�ת���ɹ�</param>
        /// <returns>intֵ</returns>
        private static int IsInt(object Object, out bool IsTrue)
        {
            try { IsTrue = true; return int.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// ת����Ϊ int  ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>int ����</returns>
        public static int ToInt(object Object) { return ToInt(Object, 0); }
        /// <summary>
        /// ת����Ϊ int  ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <returns>int ����</returns>
        public static int ToInt(object Object, int Default) { return ToInt(Object, Default, 0, 999999999); }
        /// <summary>
        /// ת����Ϊ int  ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinInt"> �½��޶�����Сֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <returns>int ����</returns>
        public static int ToInt(object Object, int Default, int MinInt) { return ToInt(Object, Default, MinInt, 999999999); }
        /// <summary>
        /// ת����Ϊ int  ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinInt"> �½��޶�����Сֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <param name="MaxInt">�Ͻ��޶������ֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <returns>int ����</returns>
        public static int ToInt(object Object, int Default, int MinInt, int MaxInt)
        {
            bool IsTrue = false;
            int Int = IsInt(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Int < MinInt || Int > MaxInt) return Default;
            return Int;
        }
        #endregion
        #region ���� long  ����
        /// <summary>
        /// �����Ƿ�Ϊ long  ��������
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsTrue">�����Ƿ�ת���ɹ�</param>
        /// <returns>longֵ</returns>
        private static long IsLong(object Object, out bool IsTrue)
        {
            try { IsTrue = true; return long.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// ת����Ϊ Long ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>Long ����</returns>
        public static long ToLong(object Object) { return ToLong(Object, 0); }
        /// <summary>
        /// ת����Ϊ Long ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <returns>Long ����</returns>
        public static long ToLong(object Object, long Default) { return ToLong(Object, Default, -9223372036854775808, 9223372036854775807); }
        /// <summary>
        /// ת����Ϊ long ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">ת�����ɹ����ص�Ĭ��ֵ</param>
        /// <param name="MinLong">�½��޶�����Сֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <returns>long ����</returns>
        public static long ToLong(object Object, long Default, long MinLong) { return ToLong(Object, Default, MinLong, 9223372036854775807); }
        /// <summary>
        /// ת����Ϊ long ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinLong">�½��޶�����Сֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <param name="MaxLong">�Ͻ��޶������ֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <returns>long ����</returns>
        public static long ToLong(object Object, long Default, long MinLong, long MaxLong)
        {
            bool IsTrue = false;
            long Long = IsLong(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Long < MinLong || Long > MaxLong) return Default;
            return Long;
        }
        #endregion
        #region ���� float  ����
        /// <summary>
        /// �����Ƿ�Ϊ float  ��������
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsTrue">�����Ƿ�ת���ɹ�</param>
        /// <returns>floatֵ</returns>
        private static float IsFloat(object Object, out bool IsTrue)
        {
            try { IsTrue = true; return float.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// ת����Ϊ float ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>float ����</returns>
        public static float ToFloat(object Object) { return ToFloat(Object, 0); }
        /// <summary>
        /// ת����Ϊ float ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <returns>float ����</returns>
        public static float ToFloat(object Object, float Default) { return ToFloat(Object, Default, 0, 999999999); }
        /// <summary>
        /// ת����Ϊ float ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinFloat"> С�ڵ��� ת���ɹ���,�½��޶�����Сֵ,��������Χ �򷵻� Ĭ��ֵ</param>
        /// <returns>float ����</returns>
        public static float ToFloat(object Object, float Default, float MinFloat) { return ToFloat(Object, Default, MinFloat, 999999999); }
        /// <summary>
        /// ת����Ϊ float ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinFloat"> �½��޶�����Сֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <param name="MaxFloat"> �Ͻ��޶������ֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <returns>float ����</returns>
        public static float ToFloat(object Object, float Default, float MinFloat, float MaxFloat)
        {
            bool IsTrue = false;
            float Float = IsFloat(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Float < MinFloat || Float > MaxFloat) return Default;
            return Float;
        }
        #endregion
        #region ���� decimal  ����
        /// <summary>
        /// �����Ƿ�Ϊ decimal  ��������
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsTrue">�����Ƿ�ת���ɹ�</param>
        /// <returns>decimalֵ</returns>
        private static decimal IsDecimal(object Object, out bool IsTrue)
        {
            try { IsTrue = true; return decimal.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// ת����Ϊ decimal ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>decimal ����</returns>
        public static decimal ToDecimal(object Object) { return ToDecimal(Object, 0); }
        /// <summary>
        /// ת����Ϊ decimal ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <returns>decimal ����</returns>
        public static decimal ToDecimal(object Object, decimal Default) { return ToDecimal(Object, Default, 0, 999999999); }

        /// <summary>
        /// ת����Ϊ decimal ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinFloat"> С�ڵ��� ת���ɹ���,�½��޶�����Сֵ,��������Χ �򷵻� Ĭ��ֵ</param>
        /// <returns>decimal ����</returns>
        public static decimal ToDecimal(object Object, decimal Default, decimal MinFloat) { return ToDecimal(Object, Default, MinFloat, 999999999); }
        /// <summary>
        /// ת����Ϊ decimal ����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="Default">Ĭ��ֵ</param>
        /// <param name="MinDecimal"> �½��޶�����Сֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <param name="MaxDecimal"> �Ͻ��޶������ֵ , ��������Χ , �򷵻� Ĭ��ֵ</param>
        /// <returns>decimal ����</returns>
        public static decimal ToDecimal(object Object, decimal Default, decimal MinDecimal, decimal MaxDecimal)
        {
            bool IsTrue = false;
            decimal Decimal = IsDecimal(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Decimal < MinDecimal || Decimal > MaxDecimal) return Default;
            return Decimal;
        }
        #endregion
        #region ���� DateTime ����
        /// <summary>
        /// �Ƿ�Ϊʱ���ʽ
        /// </summary>
        /// <param name="Object">Ҫ�жϵĶ���</param>
        /// <param name="IsTrue">�����Ƿ�ת���ɹ�</param>
        /// <returns>DateTime</returns>
        public static DateTime IsTime(object Object,out bool IsTrue)
        {
            IsTrue = false;
            if (IsNull(Object)) return DateTime.Now;
            try { IsTrue = true; return DateTime.Parse(Object.ToString()); }
            catch { IsTrue = false; return DateTime.Now; }
        }
        /// <summary>
        /// ���� DateTime  ����
        /// </summary>
        /// <param name="Object">Ҫ�������ַ�</param>
        /// <returns>DateTime</returns>
        public static DateTime ToTime(string Object) { return ToTime(Object, DateTime.Now); }
        /// <summary>
        /// �ַ���ת��Ϊʱ�亯��
        /// </summary>
        /// <param name="Object">Ҫ�������ַ�</param>
        /// <param name="Default">Ĭ��ʱ��</param>
        /// <returns>DateTime</returns>
        public static DateTime ToTime(string Object, DateTime Default)
        {
            if (IsNull(Object)) return Default;
             bool IsTrue = false;
             DateTime Time = IsTime(Object, out IsTrue);
             if (!IsTrue) return Default;
             return Time;
        }
        /// <summary>
        /// ��õ�ǰʱ��
        /// </summary>
        /// <param name="Style">ʱ����ʽ</param>
        /// <returns>string</returns>
        public static string ToNow(string Style) { return DateTime.Now.ToString(Style); }
        /// <summary>
        /// ת���ַ���Ϊ��ʽ��ʱ���ַ���
        /// </summary>
        /// <param name="Object">Ҫ�������ַ�</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object) { return ToTimes(Object, "yyyy-MM-dd HH:mm:ss"); }
        /// <summary>
        /// ת���ַ���Ϊ��ʽ��ʱ���ַ���
        /// </summary>
        /// <param name="Object">Ҫ�������ַ�</param>
        /// <param name="Style">��ʽ����ʽ</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object, string Style) { return ToTimes(Object, DateTime.Now, Style); }
        /// <summary>
        /// ת���ַ���Ϊ��ʽ��ʱ���ַ���
        /// </summary>
        /// <param name="Object">Ҫ�������ַ�</param>
        /// <param name="Default">Ĭ��ʱ��</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object, DateTime Default) { return ToTimes(Object, Default, "yyyy-MM-dd HH:mm:ss"); }
        /// <summary>
        /// ת���ַ���Ϊ��ʽ��ʱ���ַ���
        /// </summary>
        /// <param name="Object">Ҫ�������ַ�</param>
        /// <param name="Default">Ĭ��ʱ��</param>
        /// <param name="Style">��ʽ����ʽ</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object, DateTime Default, string Style)
        {
            if (IsNull(Object)) return Default.ToString(Style);
            bool IsTrue = false;
            DateTime Time = IsTime(Object, out IsTrue);
            if (!IsTrue) return Default.ToString(Style);
            return Time.ToString(Style);
        }
        #endregion
        #region ���� Ȩ�� ����
        /// <summary>
        /// �ж�Ȩ�� ���ص�ǰ������Ȩ�޵�����
        /// </summary>
        /// <param name="PowerChars">Ȩ������</param>
        /// <param name="Index">��ǰȨ��������ĳ��Ȩ�޵�����</param>
        /// <returns>int</returns>
        public static int ToPowerValue(char[] PowerChars, int Index)
        {
            if (Index < 0) return 1;
            if (PowerChars == null) return 0;
            if (Index >= PowerChars.Length) return 0;
            return (PowerChars[Index] == '0') ? 0 : 1;
        }
        /// <summary>
        /// ��ȡ��ǰȨ��������ĳ��������Ȩ��
        /// </summary>
        /// <param name="PowerChars">Ȩ������</param>
        /// <param name="Index">��ǰȨ��������ĳ��Ȩ�޵�����</param>
        /// <returns>bool</returns>
        public static bool IsPower(char[] PowerChars, int Index) { return ((ToPowerValue(PowerChars, Index) == 0) ? false : true); }
        /// <summary>
        /// ��ȡ��ǰȨ���ַ�����ĳ��������Ȩ��
        /// </summary>
        /// <param name="Powers">Ȩ���ַ��� 101110000����ʽ</param>
        /// <param name="Index">��ǰȨ��������ĳ��Ȩ�޵�����</param>
        /// <returns>bool</returns>
        public static bool IsPower(string Powers, int Index)
        {
            if (IsNull(Powers)) return false;
            return IsPower(Powers.ToCharArray(), Index);
        }
        /// <summary>
        /// ��Ȩ�������л���Ȩ���ַ���
        /// </summary>
        /// <param name="PowerChars">PowerChars Ȩ������</param>
        /// <returns>string</returns>
        public static string GetPower(char[] PowerChars) { return new string(PowerChars); }
        /// <summary>
        /// ��Ȩ�������и���ĳ��������Ȩ��
        /// ������Ȩ������
        /// </summary>
        /// <param name="PowerChars">PowerChars Ȩ������</param>
        /// <param name="Index">��ǰȨ��������ĳ��Ȩ�޵�����</param>
        /// <param name="PowerValue">Ҫ���ĵ�ֵ</param>
        /// <returns>�µ�char[]Ȩ������</returns>
        public static char[] GetPower(char[] PowerChars, int Index, string PowerValue)
        {
            if (PowerChars == null) return null;
            if (Index < 0) return PowerChars;
            if (Index >= PowerChars.Length) return PowerChars;
            PowerChars[Index] = StringToChar(PowerValue);
            return PowerChars;
        }
        /// <summary>
        /// ��Ȩ�������и���ĳ��������Ȩ��
        /// ������Ȩ���ַ���
        /// </summary>
        /// <param name="PowerChars">PowerChars Ȩ������</param>
        /// <param name="Index">��ǰȨ��������ĳ��Ȩ�޵�����</param>
        /// <param name="PowerValue">Ҫ���ĵ�ֵ</param>
        /// <returns>Ȩ���ַ���</returns>
        public static string GetPowers(char[] PowerChars, int Index, string PowerValue)
        {
            char[] Powers = GetPower(PowerChars, Index, PowerValue);
            if (Powers == null) return "";
            return new string(Powers);
        }
        /// <summary>
        /// ��Ȩ���ַ����и���ĳ��������Ȩ��
        /// ������Ȩ���ַ���
        /// </summary>
        /// <param name="Powers">Ȩ���ַ���</param>
        /// <param name="Index">��ǰȨ��������ĳ��Ȩ�޵�����</param>
        /// <param name="PowerValue">Ҫ���ĵ�ֵ</param>
        /// <returns>Ȩ���ַ���</returns>
        public static string GetPowers(string Powers, int Index, string PowerValue)
        {
            if (IsNull(Powers)) return "";
            return GetPowers(Powers.ToCharArray(), Index, PowerValue);
        }
        #endregion
        #region ���� �Ӽ��˳� ����
        /// <summary>
        /// �ж���һ���ź͵�ǰ���� ����������ֵ
        /// ���Բ�����ͨ�ļӼ��˳�
        /// </summary>
        /// <param name="Operator1">string</param>
        /// <param name="Operator2">string</param>
        /// <returns></returns>
        private static string Precede(string Operator1, string Operator2)
        {
            switch (Operator1)
            {
                case "+":
                case "-": return ("*/(".IndexOf(Operator2) != -1) ? "<" : ">";
                case "*":
                case "/": return ((Operator2 == "(") ? "<" : ">");
                case "(": return ((Operator2 == ")") ? "=" : "<");
                case ")": return ((Operator2 == "(") ? "?" : ">");
                case "#": return ((Operator2 == "#") ? "=" : "<");
            }
            return "?";
        }
        /// <summary>
        /// ���㵱ǰ2��ֵ�Ľ��
        /// </summary>
        /// <param name="Operator">�������</param>
        /// <param name="Value1">ֵ1</param>
        /// <param name="Value2">ֵ2</param>
        /// <returns>Double</returns>
        private static Double Result(char Operator, Double Value1, Double Value2)
        {
            if (Operator == '+') return (Value1 + Value2);
            if (Operator == '-') return (Value1 - Value2);
            if (Operator == '*') return (Value1 * Value2);
            if (Operator == '/') return (Value1 / Value2);
            return 0;
        }
        /// <summary>
        /// VC��eval�㷨
        /// </summary>
        /// <param name="Expression">Ҫ����ı��ʽ</param>
        /// <returns>Object</returns>
        public static Object VcEval(string Expression)
        {
            //������ķ�������
            Stack OperatorArr = new Stack();
            //����ֵ�ķ�������
            Stack ValueArr = new Stack();
            //Ҫ����ı��ʽ�ĳ��� ����
            int i = 0;
            //���ڵ�Ҫ�����2������
            Double Value1 = 0;
            Double Value2 = 0;
            //���ַ�
            string Text = "";
            //�����
            char Operator;
            //��ȡ�����������ֵ����
            MatchCollection ExpArray = Regex.Matches(Expression.Replace(" ", "") + "#", @"(((?<=(^|\())-)?\d+(\.\d+)?|\D)");

            OperatorArr.Push('#');
            Text = System.Convert.ToString(ExpArray[i++]);
            while (!(Text == "#" && System.Convert.ToString(OperatorArr.Peek()) == "#"))
            {
                if ("+-*/()#".IndexOf(Text) != -1)
                {
                    switch (Precede(OperatorArr.Peek().ToString(), Text))
                    {
                        case "<":
                            OperatorArr.Push(Text);
                            Text = System.Convert.ToString(ExpArray[i++]);
                            break;
                        case "=":
                            OperatorArr.Pop();
                            Text = System.Convert.ToString(ExpArray[i++]);
                            break;
                        case ">":
                            Operator = System.Convert.ToChar(OperatorArr.Pop());
                            Value2 = System.Convert.ToDouble(ValueArr.Pop());
                            Value1 = System.Convert.ToDouble(ValueArr.Pop());
                            ValueArr.Push(Result(Operator, Value1, Value2));
                            break;
                        default:
                            return "Error";
                    }
                }
                else
                {
                    ValueArr.Push(Text);
                    Text = System.Convert.ToString(ExpArray[i++]);
                }
            }
            return ValueArr.Pop();
        }
        #endregion
        #region ����  �����  ����
        /// <summary>
        /// ��ȡ���������ö�ٶ�Ӧ������
        /// </summary>
        /// <param name="Enum">�����������ö������</param>
        /// <returns>int</returns>
        private static int RandInt(RandEnum Enum) { return (int)Enum; }
        /// <summary>
        /// ������������͵�ö��
        /// </summary>
        public enum RandEnum : int
        {
            /// <summary>
            /// ����
            /// </summary>
            Numeric = 0,
            /// <summary>
            /// ��ĸ
            /// </summary>
            Letter = 1,
            /// <summary>
            /// ������ĸ���
            /// </summary>
            Blend = 2,
            /// <summary>
            /// ����
            /// </summary>
            Chinese = 3
        }
        /// <summary>
        /// ��ȡȫ��ΨһGUID ֵ
        /// </summary>
        /// <returns>string</returns>
        public static string GetGuid() { return GetGuid(100, true); }
        /// <summary>
        /// ��ȡ��ǰʱ��Ŀ̶�ֵ
        /// </summary>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>int</returns>
        public static int GetGuid(int MaxLength) { return ToInt(Right(DateTime.Now.Ticks.ToString(), 9)); }
        /// <summary>
        /// ��ȡȫ��ΨһGUID ֵ
        /// </summary>
        /// <param name="MaxLength">��󳤶�</param>
        /// <param name="IsRemove">�Ƿ�ȥ�������ַ�</param>
        /// <returns>string</returns>
        public static string GetGuid(int MaxLength, bool IsRemove)
        {
            string Guids = Guid.NewGuid().ToString();
            if (IsRemove) { Guids = Guids.Replace("{", ""); Guids = Guids.Replace("}", ""); Guids = Guids.Replace("-", ""); }
            Guids = Left(Guids, MaxLength);
            return Guids;
        }
        /// <summary>
        /// ��ȡָ�����͵������
        /// </summary>
        /// <param name="Enum">�����������ö������</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string GetGuid(RandEnum Enum, int MaxLength)
        {
            return GetGuid(Enum, "GB2312", MaxLength);
        }
        /// <summary>
        /// ��ȡָ�����͵������
        /// </summary>
        /// <param name="Enum">�����������ö������</param>
        /// <param name="Encode">����������ĵı���</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string GetGuid(RandEnum Enum, string Encode, int MaxLength)
        {
            if (Enum == RandEnum.Chinese) return GetGuid(Encode, MaxLength);
            string Guids = "";
            for (int i = 1; i <= MaxLength; i++)
            {
                int MinInt, MaxInt, Num;
                Random Rand = new Random(GetGuid(9) + i * 1000);
                Num = (Enum == RandEnum.Blend) ? ((Rand.Next(0, 10) <= 4) ? (int)RandEnum.Numeric : (int)RandEnum.Letter) : (int)Enum;
                MinInt = Num == 0 ? 48 : 97;
                MaxInt = Num == 0 ? 57 : 122;
                Guids += IntToString(Rand.Next(MinInt, MaxInt));
                Rand = null;
            }
            return Guids;
        }
        /// <summary>
        /// ��ȡ�������������
        /// </summary>
        /// <param name="Encode">����������ĵı���</param>
        /// <param name="MaxLength">��󳤶�</param>
        /// <returns>string</returns>
        public static string GetGuid(string Encode, int MaxLength)
        {
            //���巵�ص��ַ���
            string Chinese = "";
            //�������ı���
            Encoding Ecode = Encoding.GetEncoding(Encode);
            Random Rnd = null;// new Random(GetIntRand(4));
            //int Rint = Rnd.Next(1, 100);
            //����λ�롢����ķ�Χ��
            int Wint, Qint;
            for (int i = 1; i <= MaxLength; i++)
            {
                int Rint = 0;
                //��ȡ������λ
                Rnd = new Random(GetGuid(9) + i * 1000);
                Rint = Rnd.Next(16, 56);//ֻ��ȡ���ú��� 16-55֮��
                Wint = Rint;
                //55��ֻ89������ ���� 94������
                Rint = (Wint == 55) ? 90 : 95;
                //����������
                Rnd = new Random(GetGuid(9) + i * 3000);
                Rint = Rnd.Next(1, Rint);
                Qint = Rint;
                //�����ֽڱ����洢���������������λ��
                byte Fbyte = System.Convert.ToByte((Wint + 160).ToString("x"), 16);
                byte Lbyte = System.Convert.ToByte((Qint + 160).ToString("x"), 16);
                //�������ֽڱ����洢���ֽ�������
                byte[] Nbytes = new byte[] { Fbyte, Lbyte };
                Chinese += Ecode.GetString(Nbytes);
                Rnd = null;
            }
            Ecode = null;
            return Chinese;
        }
        #endregion
        #region ��� �ַ���(��������)    ����
        /// <summary>
        /// �Ƿ���
        /// </summary>
        /// <param name="Object">�����ַ�</param>
        /// <returns>bool</returns>
        private static bool IsChinese(string Object)
        {
            int Int = StringToInt(Object);
            if (Int >= 19968 && Int <= 40869) return true;
            return false;
        }
        /// <summary>
        /// �Ƿ���
        /// </summary>
        /// <param name="Object">�����ַ� char</param>
        /// <returns>bool</returns>
        private static bool IsChinese(char Object)
        {
            int Int = (int)Object;
            if (Int >= 19968 && Int <= 40869) return true;
            return false;
        }
        /// <summary>
        /// ��ȡ�ַ����ĳ���
        /// </summary>
        /// <param name="Object">Ҫ���Ķ���</param>
        /// <param name="IsChecked">�Ƿ��жϺ���</param>
        /// <returns>int</returns>
        public static int GetLength(string Object, bool IsChecked)
        {
            if (IsNull(Object)) return 0;
            if (!IsChecked) return Object.Length;
            int iLen = 0;
            foreach (char c in Object)
            {
                iLen++;
                if (IsChinese(c)) iLen++;
            }
            return iLen;
        }
        /// <summary>
        /// ��ⳤ��
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <param name="MinInt">С�ڵ��� ��С����</param>
        /// <param name="MaxInt">���ڵ��� ��󳤶�</param>
        /// <returns>����?true:false</returns>
        public static bool CheckLength(string Object, int MinInt, int MaxInt)
        { return CheckLength(Object, MinInt, MaxInt, false); }
        /// <summary>
        /// ��ⳤ��
        /// </summary>
        /// <param name="Object">Ҫ���Ķ���</param>
        /// <param name="MinInt">С�ڵ��� ��С����</param>
        /// <param name="MaxInt">���ڵ��� ��󳤶�</param>
        /// <param name="IsChecked">�Ƿ��жϺ���</param>
        /// <returns>����?true:false</returns>
        public static bool CheckLength(string Object, int MinInt, int MaxInt, bool IsChecked)
        {
            int Int = GetLength(Object, IsChecked);
            if (Int < MinInt || Int > MaxInt) return false;
            return true;
        }
        #endregion
        #region ���� HTML �ı� ����
        /// <summary>
        /// �ı����� ִ��HTML����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>�ַ�</returns>
        public static string HtmlDecode(string Object)
        {
            if (IsNull(Object)) return "";
            StringBuilder Builder = new StringBuilder(Object);
            Builder.Replace("&amp;", "&");
            Builder.Replace("&lt;", "<");
            Builder.Replace("&gt;", ">");
            Builder.Replace("&quot;", "\"");
            Builder.Replace("&#39;", "'");
            Builder.Replace("&nbsp; &nbsp; ", "\t");
            Builder.Replace("<p></p>", "\r\n\r\n");
            Builder.Replace("<br />", "\r\n");
            Builder.Replace("<br>", "\r\n");
            return Builder.ToString();
        }
        /// <summary>
        /// �ı����� ִ��HTML����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>�ַ�</returns>
        public static string HtmlEncode(string Object)
        {
            if (IsNull(Object)) return "";
            StringBuilder Builder = new StringBuilder(Object);
            Builder.Replace("&", "&amp;");
            Builder.Replace("<", "&lt;");
            Builder.Replace(">", "&gt;");
            Builder.Replace("\"", "&quot;");
            Builder.Replace("'", "&#39;");
            Builder.Replace("\t", "&nbsp; &nbsp; ");
            Builder.Replace("\r\n\r\n", "<p></p>");
            Builder.Replace("\r\n", "<br />");
            return Builder.ToString();
        }
        /// <summary>
        /// �ı����� ִ��TEXT����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>�ַ�</returns>
        public static string TextDecode(string Object)
        {
            if (IsNull(Object)) return "";
            StringBuilder Builder = new StringBuilder(Object);
            Builder.Replace("&amp;", "&");
            Builder.Replace("&lt;", "<");
            Builder.Replace("&gt;", ">");
            Builder.Replace("&quot;", "\"");
            Builder.Replace("&#39;", "'");
            return Builder.ToString();
        }
        /// <summary>
        /// �ı����� ִ��TEXT����
        /// </summary>
        /// <param name="Object">Ҫת���Ķ���</param>
        /// <returns>�ַ�</returns>
        public static string TextEncode(string Object)
        {
            if (IsNull(Object)) return "";
            StringBuilder Builder = new StringBuilder(Object);
            Builder.Replace("&", "&amp;");
            Builder.Replace("<", "&lt;");
            Builder.Replace(">", "&gt;");
            Builder.Replace("\"", "&quot;");
            Builder.Replace("'", "&#39;");
            return Builder.ToString();
        }
        #endregion

        /// <summary>
        /// ��ȡ��ǰ�����ַ� ����Ӧ�Ĺ涨��ģ��ֵ
        /// ��ĸռ26�����ݱ�
        /// �����ַ�(��������)ռ24�����ݱ�
        /// һ��50���û��ܱ�
        /// </summary>
        /// <param name="Object">�����ַ�</param>
        /// <returns>int</returns>
        public static int GetMod(string Object)
        {
            //����Ϊ1
            return 1;
            //
            if (Object.Length > 1) Object = Left(Object, 1);
            int Int = StringToInt(Object.ToLower());
            //0~9
            if (Int >= 48 && Int <= 57) return (1 + Int % 48);
            //a~z
            if (Int >= 97 && Int <= 122) return (11 + Int % 97);
            //����
            return 37 + Int % 14;
        }
        /// <summary>
        /// ��ȡ2��ʱ��֮���ʱ���
        /// </summary>
        /// <param name="StarTime">��һ��ʱ��</param>
        /// <param name="EndTime">�ڶ���ʱ��</param>
        /// <returns>double</returns>
        public static double DateDiff(DateTime StarTime, DateTime EndTime)
        {
            try
            {
                TimeSpan StarTimeSpan = new TimeSpan(StarTime.Ticks);
                TimeSpan EndTimeSpan = new TimeSpan(EndTime.Ticks);
                TimeSpan TotalTimeSpan = StarTimeSpan.Subtract(EndTimeSpan).Duration();
                return TotalTimeSpan.TotalMilliseconds;
            }
            catch { return -1; }
        }
        /// <summary>
        /// ����һ��ʱ���뵱ǰ�������ں�ʱ���ʱ����,���ص���ʱ���������ڲ�ľ���ֵ.
        /// </summary>
        /// <param name="StarTime">һ�����ں�ʱ��</param>
        /// <returns>double</returns>
        private static double DateDiff(DateTime StarTime) { return DateDiff(StarTime, DateTime.Now); }
    };
}