using System;

namespace Power.Mvc.Helper.Exceptions
{
    /// <summary>
    /// 未指定排序的欄位
    /// </summary>
    [Serializable]
    public class OrderbyClauseNotSpecifiedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the OrderbyClauseNotSpecifiedException class
        /// </summary>
        public OrderbyClauseNotSpecifiedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the OrderbyClauseNotSpecifiedException class with a
        /// specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OrderbyClauseNotSpecifiedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OrderbyClauseNotSpecifiedException class with a
        /// specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        /// Visual Basic) if no inner exception is specified.
        /// </param>
        public OrderbyClauseNotSpecifiedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OrderbyClauseNotSpecifiedException class with a
        /// specified error message.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized object data
        /// about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual information
        /// about the source or destination.
        /// </param>
        protected OrderbyClauseNotSpecifiedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}