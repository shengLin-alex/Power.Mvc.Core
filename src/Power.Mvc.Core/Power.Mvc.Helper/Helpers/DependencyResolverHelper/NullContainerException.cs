using System;

namespace Power.Mvc.Helper.Exceptions
{
    /// <summary>
    /// 未正確依賴注入容易之例外
    /// </summary>
    [Serializable]
    public class NullContainerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NullContainerException class
        /// </summary>
        public NullContainerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the NullContainerException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NullContainerException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NullContainerException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        /// Visual Basic) if no inner exception is specified.
        /// </param>
        public NullContainerException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NullContainerException class with a specified error message.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized object data
        /// about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual information
        /// about the source or destination.
        /// </param>
        protected NullContainerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}