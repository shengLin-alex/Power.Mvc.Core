using System;

namespace Power.Mvc.Helper.Models
{
    /// <summary>
    /// 編輯欄位介面
    /// </summary>
    public interface IEditColumn
    {
        /// <summary>
        /// 建立於
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// 建立者
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// 更新於
        /// </summary>
        DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        string UpdatedBy { get; set; }
    }
}