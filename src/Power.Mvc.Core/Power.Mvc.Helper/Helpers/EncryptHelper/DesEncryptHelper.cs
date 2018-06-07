using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// DES 加密實作 (不建議使用)
    /// </summary>
    public class DesEncryptHelper : ISymmetricEncryptHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="key">金鑰</param>
        /// <returns>加密後的密文</returns>
        public string Encrypt(string content, string key)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // 金鑰用UTF8格式再用MD5計算Hash
            byte[] keyArray = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));

            // 由於 DES 金鑰長度限定 8 bytes，因此需轉換
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(key, keyArray);

            // IV預設8 byte都是0
            byte[] initializationVector = new byte[8];

            // 先將內容轉為UTF8格式
            byte[] encryptionContent = Encoding.UTF8.GetBytes(content);

            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider
            {
                IV = initializationVector
            };
            desProvider.Key = rfc2898.GetBytes(desProvider.KeySize / 8);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptionContent, 0, encryptionContent.Length);
                    cryptoStream.FlushFinalBlock();

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="key">金鑰</param>
        /// <returns>解密後的明文</returns>
        public string Decrypt(string content, string key)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // 金鑰用UTF8格式再用MD5計算Hash
            byte[] keyArray = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));

            // 由於 DES 金鑰長度限定 8 bytes，因此需轉換
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(key, keyArray);

            // IV預設8 byte都是0
            byte[] initializationVector = new byte[8];

            // 密文用Base64解密
            byte[] base64DecryptionContent = Convert.FromBase64String(content);

            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider
            {
                IV = initializationVector
            };
            desProvider.Key = rfc2898.GetBytes(desProvider.KeySize / 8);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(base64DecryptionContent, 0, base64DecryptionContent.Length);
                    cryptoStream.FlushFinalBlock();

                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="key">金鑰</param>
        /// <exception cref="NotSupportedException"></exception>
        public void EncryptFile(string rawFilePath, string encryptedFilePath, string key)
        {
            throw new NotImplementedException("不支援 DES 檔案加密!");
        }

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="key">金鑰</param>
        /// <exception cref="NotSupportedException"></exception>
        public void DecryptFile(string rawFilePath, string encryptedFilePath, string key)
        {
            throw new NotImplementedException("不支援 DES 檔案解密!");
        }
    }
}