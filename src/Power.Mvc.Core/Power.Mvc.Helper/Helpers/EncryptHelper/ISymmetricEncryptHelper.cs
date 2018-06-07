namespace Power.Mvc.Helper
{
    /// <summary>
    /// 對稱式加解密
    /// </summary>
    public interface ISymmetricEncryptHelper
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="key">金鑰</param>
        /// <returns>解密後的明文</returns>
        string Decrypt(string content, string key);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="key">金鑰</param>
        /// <returns>加密後的密文</returns>
        string Encrypt(string content, string key);

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="key">金鑰</param>
        void DecryptFile(string rawFilePath, string encryptedFilePath, string key);

        /// <summary>
        /// 解密檔案
        /// </summary>
        /// <param name="rawFilePath">原始檔案路徑</param>
        /// <param name="encryptedFilePath">加密後檔案路徑</param>
        /// <param name="key">金鑰</param>
        void EncryptFile(string rawFilePath, string encryptedFilePath, string key);
    }
}