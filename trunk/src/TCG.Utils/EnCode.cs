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
using System.IO;
using System.Text;
using System.Web;
using System.Security.Cryptography;

namespace TCG.Utils
{
    /// <summary>
    /// �ṩ�Զ���ϵͳ�ַ��ļ��ܺͽ��ܲ���
    /// ֧��ָ��������ļ��ܷ���
    /// </summary>
    public class EnCode
    {
        /// <summary>
        /// ���ó�ʼKey
        /// </summary>
        private static string iKey = "TXZDSW";
        /// <summary>
        /// JS��ȡ��֤������(֧�� WEB.CONFIG �ļ����� �ڵ��� Key.EnScript)
        /// </summary>
        private static string Skey_EnScript ="EnScript";
        /// <summary>
        /// �����ļ���Key
        /// </summary>
        private static string Skey_EnScriptKeys = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// ���ӵļ���Key
        /// </summary>
        private static string Skey_EnScriptKey = "abcde";
        /// <summary>
        /// ��ȡ��������ֵ
        /// </summary>
        /// <returns>string</returns>
        public static string GetKey()
        {
            SessionState.Remove(Skey_EnScript);
            string Key = Skey_EnScriptKeys + Bases.GetGuid(Bases.RandEnum.Letter, 5).Replace("z", "h");
            string reKey = "0";
            for (int i = 1; i < 41; i++)
            {
                int Int = new Random(Bases.GetGuid(9)).Next(0, reKey.Length + 1);
                reKey = reKey.Insert(Int, Key.Substring(i, 1));
            }
            SessionState.Set(Skey_EnScript, reKey);
            return reKey;
        }
        /// <summary>
        /// ���û�ȡKEY����
        /// </summary>
        /// <param name="EnStr">���������ַ�</param>
        /// <param name="LeftKey">���ַ�</param>
        /// <param name="RightKey">���ַ�</param>
        /// <returns>string</returns>
        private static string GetKey(string EnStr, out string LeftKey, out string RightKey)
        {
            LeftKey = "";
            RightKey = "";
            if (Bases.NotNull(EnStr))
            {
                EnStr = EnStr.ToUpper();
                LeftKey = Bases.Left(EnStr, 1);
                RightKey = Bases.Right(EnStr, 1);
            }
            else
            {
                LeftKey = Bases.GetGuid().ToUpper();
                RightKey = Bases.Right(LeftKey, 1);
                LeftKey = Bases.Left(LeftKey, 1);
            }
            return (LeftKey + iKey + RightKey);
        }
        /// <summary>
        /// ���ܷ��� �������
        /// </summary>
        /// <param name="Value">Ҫ���ܵ��ַ�</param>
        /// <returns></returns>
        public static string EnToCode(string Value) { return EnToCode(Value, ""); }
        /// <summary>
        /// ���ܷ��� ָ������
        /// </summary>
        /// <param name="Value">Ҫ���ܵ��ַ�</param>
        /// <param name="CodeKey">ָ���ļ����ַ�</param>
        /// <returns>string</returns>
        public static string EnToCode(string Value, string CodeKey)
        {
            string LeftKey, RightKey;
            string Key = GetKey(CodeKey, out LeftKey, out RightKey);
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                byte[] Byte = Encoding.Default.GetBytes(Value);
                DES.Key = ASCIIEncoding.ASCII.GetBytes(Key);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(Key);
                MemoryStream MStream = new MemoryStream();
                CryptoStream CStream = new CryptoStream(
                    MStream,
                    DES.CreateEncryptor(),
                    CryptoStreamMode.Write
                    );
                CStream.Write(Byte, 0, Byte.Length);
                CStream.FlushFinalBlock();
                StringBuilder SBuilder = new StringBuilder();
                foreach (byte mByte in MStream.ToArray())
                {
                    SBuilder.AppendFormat("{0:X2}", mByte);
                }
                SBuilder.ToString();
                return (LeftKey + SBuilder.ToString() + RightKey);
            }
            catch { return Value; }
        }
        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="Value">Ҫ���ܵ��ַ�</param>
        /// <returns>string</returns>
        public static string DeToCode(string Value)
        {
            string LeftKey, RightKey;
            string Key = GetKey(Value, out LeftKey, out RightKey);
            try
            {
                string tValue = Value;
                tValue = tValue.Substring(1, tValue.Length - 2);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                byte[] Byte = new byte[tValue.Length / 2];
                for (int X = 0; X < tValue.Length / 2; X++)
                {
                    int i = (Convert.ToInt32(tValue.Substring(X * 2, 2), 16));
                    Byte[X] = (byte)i;
                }
                DES.Key = ASCIIEncoding.ASCII.GetBytes(Key);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(Key);
                MemoryStream MStream = new MemoryStream();
                CryptoStream CStream = new CryptoStream(MStream, DES.CreateDecryptor(), CryptoStreamMode.Write);
                CStream.Write(Byte, 0, Byte.Length);
                CStream.FlushFinalBlock();
                StringBuilder SBuilder = new StringBuilder();
                return System.Text.Encoding.Default.GetString(MStream.ToArray());
            }
            catch { return Value; }
        }
        /// <summary>
        /// �ⲿ��ӦJS���ܵĽ��ܷ���
        /// </summary>
        /// <param name="Value">Ҫ���ܵ��ַ�</param>
        /// <param name="Key">���ܵ�Key</param>
        /// <returns>string</returns>
        public static string DeToCode(string Value, string Key)
        {
            string reValue = "";
            if (Bases.IsNull(Value)) return reValue;
            int ArrLen, Index1, Index2, Index3, Index, KeyLen, Int;
            bool Flag = Value.Substring(0, 1) == "z";
            ArrLen = Flag ? ((Value.Length - 1) / 2) : (Value.Length / 3);
            Index = 0;
            KeyLen = Key.Length;
            try
            {
                for (int i = 0; i < ArrLen; i++)
                {
                    Index1 = Flag ? 0 : Key.IndexOf(Value.Substring(Index, 1));
                    Index++;
                    Index2 = Key.IndexOf(Value.Substring(Index, 1));
                    Index++;
                    Index3 = Key.IndexOf(Value.Substring(Index, 1));
                    if (!Flag) Index++;
                    Int = Index1 * 1681 + Index2 * KeyLen + Index3;
                    reValue += Bases.IntToString(Int);
                }
            }
            catch { }
            return reValue;
        }
    };
}