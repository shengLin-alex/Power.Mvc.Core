using System;
using AutoMapper;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// Execution context 執行階段上下文資訊基底類別
    /// </summary>
    public abstract class ExecutionContextBase : IExecutionContext
    {
        /// <summary>
        /// TimeZoneInfoInstance
        /// </summary>
        private TimeZoneInfo TimeZoneInfoInstance;

        /// <summary>
        /// 應用程式資料路徑
        /// </summary>
        public virtual string AppDataPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 應用程式根目錄
        /// </summary>
        public virtual string AppRootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 執行階段 系統時間
        /// </summary>
        public virtual DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 時區資訊
        /// </summary>
        public virtual TimeZoneInfo TimeZoneInfo
        {
            get
            {
                if (this.TimeZoneInfoInstance == null)
                {
                    this.TimeZoneInfoInstance = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                }

                return this.TimeZoneInfoInstance;
            }
        }

        /// <summary>
        /// AutoMapper Mapper 介面
        /// </summary>
        public abstract IMapper Mapper { get; }

        /// <summary>
        /// AutoMapper 設定
        /// </summary>
        public abstract MapperConfiguration MapperConfiguration { get; }
    }
}