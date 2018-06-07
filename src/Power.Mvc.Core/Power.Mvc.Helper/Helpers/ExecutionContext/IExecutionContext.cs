using System;
using AutoMapper;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// Execution context 執行階段上下文資訊
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// 應用程式資料路徑
        /// </summary>
        string AppDataPath { get; }

        /// <summary>
        /// 應用程式根目錄
        /// </summary>
        string AppRootPath { get; }

        /// <summary>
        /// 執行階段 系統時間
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// 時區資訊
        /// </summary>
        TimeZoneInfo TimeZoneInfo { get; }

        /// <summary>
        /// AutoMapper Mapper 介面
        /// </summary>
        IMapper Mapper { get; }

        /// <summary>
        /// AutoMapper 設定
        /// </summary>
        MapperConfiguration MapperConfiguration { get; }
    }
}