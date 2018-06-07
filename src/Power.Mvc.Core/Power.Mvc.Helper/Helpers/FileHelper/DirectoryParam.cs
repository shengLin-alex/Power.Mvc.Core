namespace Power.Mvc.Helper
{
    /// <summary>
    /// 檔案參數
    /// </summary>
    public class DirectoryParam
    {
        /// <summary>
        /// 目錄路徑清單
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 檔案過濾條件
        /// </summary>
        public string SearchPattern { get; set; } = "*.*";

        /// <summary>
        /// 使否遞迴搜尋子目錄
        /// </summary>
        public bool RecursivelySearch { get; set; } = true;
    }
}