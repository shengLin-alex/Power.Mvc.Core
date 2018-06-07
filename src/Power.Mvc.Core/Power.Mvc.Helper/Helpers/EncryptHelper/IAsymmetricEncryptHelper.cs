using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 非對稱式加解密介面
    /// </summary>
    public interface IAsymmetricEncryptHelper
    {
        /// <summary>
        /// 取得公鑰與私鑰
        /// </summary>
        /// <returns>公鑰與私鑰</returns>
        Tuple<string, string> GenerateKey();

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="privateKey">解密用私鑰</param>
        /// <returns>解密後的明文</returns>
        string Decrypt(string content, string privateKey);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="publicKey">加密用公鑰</param>
        /// <returns>加密後的密文</returns>
        string Encrypt(string content, string publicKey);

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="privateKey">私鑰</param>
        void DecryptFile(string rawFilePath, string encryptedFilePath, string privateKey);

        /// <summary>
        /// 加密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="publicKey">公鑰</param>
        void EncryptFile(string rawFilePath, string encryptedFilePath, string publicKey);
    }
}