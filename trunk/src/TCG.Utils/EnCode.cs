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
using System.IO;
using System.Text;
using System.Web;
using System.Security.Cryptography;

namespace TCG.Utils
{
    /// <summary>
    /// 提供自定义系统字符的加密和解密操作
    /// 支持指定和随机的加密方法
    /// </summary>
    public class EnCode
    {
        /// <summary>
        /// 设置初始Key
        /// </summary>
        private static string iKey = "TXZDSW";
        /// <summary>
        /// JS获取验证码名称(支持 WEB.CONFIG 文件配置 节点名 Key.EnScript)
        /// </summary>
        private static string Skey_EnScript ="EnScript";
        /// <summary>
        /// 基础的加密Key
        /// </summary>
        private static string Skey_EnScriptKeys = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// 附加的加密Key
        /// </summary>
        private static string Skey_EnScriptKey = "abcde";
        /// <summary>
        /// 获取加密主键值
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
        /// 设置获取KEY函数
        /// </summary>
        /// <param name="EnStr">输入的随机字符</param>
        /// <param name="LeftKey">左字符</param>
        /// <param name="RightKey">右字符</param>
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
        /// 加密方法 随机加密
        /// </summary>
        /// <param name="Value">要加密的字符</param>
        /// <returns></returns>
        public static string EnToCode(string Value) { return EnToCode(Value, ""); }
        /// <summary>
        /// 加密方法 指定加密
        /// </summary>
        /// <param name="Value">要加密的字符</param>
        /// <param name="CodeKey">指定的加密字符</param>
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
        /// 解密方法
        /// </summary>
        /// <param name="Value">要解密的字符</param>
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
        /// 外部对应JS加密的解密方法
        /// </summary>
        /// <param name="Value">要解密的字符</param>
        /// <param name="Key">加密的Key</param>
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