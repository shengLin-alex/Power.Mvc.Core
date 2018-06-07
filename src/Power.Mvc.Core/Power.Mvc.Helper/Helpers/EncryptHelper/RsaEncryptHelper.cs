using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 非對稱式 RSA 加解密實作
    /// </summary>
    public class RsaEncryptHelper : IAsymmetricEncryptHelper
    {
        /// <summary>
        /// 金鑰長度
        /// </summary>
        protected virtual int KeySize { get; set; } = 2048;

        /// <summary>
        /// 取得公鑰與私鑰
        /// </summary>
        /// <returns>公鑰與私鑰</returns>
        public Tuple<string, string> GenerateKey()
        {
            // 可加密的資料內容大小估算公式為 (KeySize / 8) - 11
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(this.KeySize);

            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            return Tuple.Create(publicKey, privateKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="publicKey">加密用公鑰</param>
        /// <returns>加密後的密文</returns>
        public string Encrypt(string content, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(this.KeySize);
            rsa.FromXmlString(publicKey);

            return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(content), false));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="privateKey">解密用私鑰</param>
        /// <returns>解密後的明文</returns>
        public string Decrypt(string content, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(this.KeySize);
            rsa.FromXmlString(privateKey);

            return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(content), false));
        }

        /// <summary>
        /// 加密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="publicKey">公鑰</param>
        public void EncryptFile(string rawFilePath, string encryptedFilePath, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(this.KeySize);
            rsa.FromXmlString(publicKey);

            using (FileStream readStream = File.OpenRead(rawFilePath))
            {
                using (FileStream writeStream = File.OpenWrite(encryptedFilePath))
                {
                    byte[] contentBytes = new byte[readStream.Length];
                    readStream.Read(contentBytes, 0, contentBytes.Length);

                    byte[] encryptedContentBytes = rsa.Encrypt(contentBytes, false);
                    writeStream.Write(encryptedContentBytes, 0, encryptedContentBytes.Length);
                }
            }
        }

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="privateKey">私鑰</param>
        public void DecryptFile(string rawFilePath, string encryptedFilePath, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(this.KeySize);
            rsa.FromXmlString(privateKey);

            using (FileStream readStream = File.OpenRead(encryptedFilePath))
            {
                using (FileStream writeStream = File.OpenWrite(rawFilePath))
                {
                    byte[] contentBytes = new byte[readStream.Length];
                    readStream.Read(contentBytes, 0, contentBytes.Length);

                    byte[] decryptedContentBytes = rsa.Decrypt(contentBytes, false);
                    writeStream.Write(decryptedContentBytes, 0, decryptedContentBytes.Length);
                }
            }
        }
    }
}