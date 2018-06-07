using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Power.Mvc.Helper.Utils
{
    /// <summary>
    /// 鍵值對擴充版
    /// </summary>
    /// <typeparam name="TKey">鍵型別</typeparam>
    /// <typeparam name="TValue">值型別</typeparam>
    public class KeyValuePair<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// 物件內之私有集合
        /// </summary>
        private readonly List<KeyValuePair<TKey, TValue>> List = new List<KeyValuePair<TKey, TValue>>();

        /// <summary>
        /// 鍵
        /// </summary>
        private readonly TKey KeyField;

        /// <summary>
        /// 值
        /// </summary>
        private readonly TValue ValueField;

        /// <summary>
        /// 預設建構子
        /// </summary>
        public KeyValuePair()
        {
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="key">鍵</param>
        /// <param name="value">值</param>
        private KeyValuePair(TKey key, TValue value)
        {
            this.KeyField = key;
            this.ValueField = value;
        }

        /// <summary>
        /// 鍵屬性
        /// </summary>
        public TKey Key => this.KeyField;

        /// <summary>
        /// 值屬性
        /// </summary>
        public TValue Value => this.ValueField;

        /// <summary>
        /// 使用陣列索引子存取
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>鍵值對</returns>
        public KeyValuePair<TKey, TValue> this[int index]
        {
            get => this.List[index];
            set => this.List.Insert(index, value);
        }

        /// <summary>
        /// 產生鍵值對
        /// </summary>
        /// <returns>鍵值對</returns>
        public static KeyValuePair<TKey, TValue> Create()
        {
            return new KeyValuePair<TKey, TValue>();
        }

        /// <summary>
        /// 產生鍵值對多載
        /// </summary>
        /// <param name="key">鍵</param>
        /// <param name="value">值</param>
        /// <returns>鍵值對</returns>
        public static KeyValuePair<TKey, TValue> Create(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="key">鍵</param>
        /// <param name="value">值</param>
        public void Add(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
            this.List.Add(keyValuePair);
        }

        /// <summary>
        /// GetEnumerator 簡單實作
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        /// <summary>
        /// GetEnumerator 嚴格實作
        /// </summary>
        /// <returns>IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// 鍵值對轉為字串
        /// </summary>
        /// <returns>"[Key, Value]"</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            if (this.KeyField != null)
            {
                sb.Append(this.KeyField);
            }

            sb.Append(", ");
            if (this.ValueField != null)
            {
                sb.Append(this.ValueField);
            }

            sb.Append(']');

            return sb.ToString();
        }
    }
}