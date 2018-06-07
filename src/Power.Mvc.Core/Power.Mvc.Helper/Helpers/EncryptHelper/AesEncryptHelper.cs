using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// AES 加解密實作
    /// </summary>
    public class AesEncryptHelper : ISymmetricEncryptHelper
    {
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

            // IV預設16 byte都是0
            byte[] initializationVector = new byte[16];

            // 密文用Base64解密
            byte[] base64DecryptionContent = Convert.FromBase64String(content);

            // RijndaelManaged：.Net的常用AE加S解密模組
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = keyArray, //// 設定金鑰
                IV = initializationVector, //// 設定IV
                Mode = CipherMode.CBC, //// 使用CBC模式
                Padding = PaddingMode.PKCS7 //// Padding用PKCS7,註Java是PKCS5
            };

            // 使用RijndaelManaged的解密模組
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
            byte[] resultArray = cryptoTransform.TransformFinalBlock(base64DecryptionContent, 0, base64DecryptionContent.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

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

            // IV預設16 byte都是0
            byte[] initializationVector = new byte[16];

            // 先將內容轉為UTF8格式
            byte[] encryptionContent = Encoding.UTF8.GetBytes(content);

            // RijndaelManaged：.Net的常用AES加解密模組
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = keyArray, //// 設定金鑰
                IV = initializationVector, //// 設定IV
                Mode = CipherMode.CBC, //// 使用CBC模式
                Padding = PaddingMode.PKCS7 //// Padding用PKCS7,註Java是PKCS5
            };

            // 使用RijndaelManaged的加密模組
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
            byte[] resultArray = cryptoTransform.TransformFinalBlock(encryptionContent, 0, encryptionContent.Length);

            // 最後用Base64加密
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="key">金鑰</param>
        public void DecryptFile(string rawFilePath, string encryptedFilePath, string key)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // 金鑰用UTF8格式再用MD5計算Hash
            byte[] keyArray = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));

            // IV預設16 byte都是0
            byte[] initializationVector = new byte[16];

            using (FileStream readStream = File.OpenRead(encryptedFilePath))
            {
                using (FileStream writeStream = File.OpenWrite(rawFilePath))
                {
                    byte[] contentBytes = new byte[readStream.Length];
                    readStream.Read(contentBytes, 0, contentBytes.Length);

                    // RijndaelManaged：.Net的常用AES加解密模組
                    RijndaelManaged rijndaelManaged = new RijndaelManaged
                    {
                        Key = keyArray, //// 設定金鑰
                        IV = initializationVector, //// 設定IV
                        Mode = CipherMode.CBC, //// 使用CBC模式
                        Padding = PaddingMode.PKCS7 //// Padding用PKCS7,註Java是PKCS5
                    };

                    // 使用RijndaelManaged的解密模組
                    ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                    byte[] resultArray = cryptoTransform.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                    writeStream.Write(resultArray, 0, resultArray.Length);
                }
            }
        }

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="key">金鑰</param>
        public void EncryptFile(string rawFilePath, string encryptedFilePath, string key)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // 金鑰用UTF8格式再用MD5計算Hash
            byte[] keyArray = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));

            // IV預設16 byte都是0
            byte[] initializationVector = new byte[16];

            using (FileStream readStream = File.OpenRead(rawFilePath))
            {
                using (FileStream writeStream = File.OpenWrite(encryptedFilePath))
                {
                    byte[] contentBytes = new byte[readStream.Length];
                    readStream.Read(contentBytes, 0, contentBytes.Length);

                    // RijndaelManaged：.Net的常用AES加解密模組
                    RijndaelManaged rijndaelManaged = new RijndaelManaged
                    {
                        Key = keyArray, //// 設定金鑰
                        IV = initializationVector, //// 設定IV
                        Mode = CipherMode.CBC, //// 使用CBC模式
                        Padding = PaddingMode.PKCS7 //// Padding用PKCS7,註Java是PKCS5
                    };

                    // 使用RijndaelManaged的加密模組
                    ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                    byte[] resultArray = cryptoTransform.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                    writeStream.Write(resultArray, 0, resultArray.Length);
                }
            }
        }
    }
}