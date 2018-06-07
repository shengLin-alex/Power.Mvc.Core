using Microsoft.Extensions.Logging;
using Power.Mvc.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 檔案處理相關功能
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILogger<FileHelper> Logger = PackageDiResolver.Current.GetService<ILogger<FileHelper>>();

        /// <summary>
        /// 建構子
        /// </summary>
        protected FileHelper()
        {
        }

        /// <summary>
        /// 清除目錄下的檔案與子目錄
        /// </summary>
        /// <param name="directoryParam">目錄參數</param>
        /// <returns>是否成功</returns>
        public static bool CleanDirectory(DirectoryParam directoryParam)
        {
            try
            {
                // 取得目錄資訊
                DirectoryInfo dirInfo = new DirectoryInfo(directoryParam.Path);

                // 刪除檔案
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }

                // 刪除目錄
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    dir.Delete(true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 建立目錄
        /// </summary>
        /// <param name="directoryParam">目錄路徑參數</param>
        /// <returns>成功回傳 true, 如果建立失敗則傳回 false</returns>
        public static bool CreateDirectory(DirectoryParam directoryParam)
        {
            Logger.LogInformation("path => " + directoryParam.Path);

            try
            {
                if (!Directory.Exists(directoryParam.Path))
                {
                    Directory.CreateDirectory(directoryParam.Path);
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.ToString());

                return false;
            }
        }

        /// <summary>
        /// 搜尋目錄
        /// </summary>
        /// <param name="directoryParam">目錄路徑參數</param>
        /// <returns>檔案路徑清單</returns>
        public static List<string> GetDirectories(DirectoryParam directoryParam)
        {
            List<string> directories = new List<string>();
            try
            {
                directories.AddRange(Directory.GetDirectories(directoryParam.Path, directoryParam.SearchPattern));
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.ToString());
            }

            return directories;
        }

        /// <summary>
        /// 搜尋目錄下的檔案
        /// </summary>
        /// <param name="directoryParam">檔案參數</param>
        /// <returns>檔案路徑清單</returns>
        public static List<string> GetFiles(DirectoryParam directoryParam)
        {
            List<string> files = new List<string>();
            try
            {
                // 回傳起始目錄下的檔案
                files.AddRange(Directory.GetFiles(directoryParam.Path, directoryParam.SearchPattern));
                if (!directoryParam.RecursivelySearch)
                {
                    return files;
                }

                // 如果包含子目錄，遞迴搜尋子目錄
                string[] subDirectories = Directory.GetDirectories(directoryParam.Path);
                foreach (string directory in subDirectories)
                {
                    GetFilesRecursively(files, directory, directoryParam.SearchPattern);
                }

                return files;
            }
            catch
            {
                return files;
            }
        }

        /// <summary>
        /// 建立檔案並寫入文字內容
        /// </summary>
        /// <param name="filePath">檔案名稱(含路徑)</param>
        /// <param name="contents">文字內容</param>
        /// <param name="encoding">編碼(預設UTF8)</param>
        public static void WriteAllText(string filePath, string contents, System.Text.Encoding encoding = null)
        {
            using (StreamWriter file = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write), encoding ?? System.Text.Encoding.UTF8))
            {
                file.WriteLine(contents);
            }
        }

        /// <summary>
        /// 遞迴搜尋目錄下的檔案
        /// </summary>
        /// <param name="files">用來存放檔案名稱的陣列</param>
        /// <param name="startDirectory">起始目錄</param>
        /// <param name="searchPattern">過濾條件</param>
        private static void GetFilesRecursively(List<string> files, string startDirectory, string searchPattern)
        {
            files.AddRange(Directory.GetFiles(startDirectory, searchPattern));
            string[] subDirectories = Directory.GetDirectories(startDirectory);
            foreach (string directory in subDirectories)
            {
                GetFilesRecursively(files, directory, searchPattern);
            }
        }
    }
}