using ArxOne.MrAdvice.Advice;
using System;
using System.Transactions;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 資料庫交易隔離
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UseTransactionScope : Attribute, IMethodAdvice
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isolationLevel">指定交易的隔離等級 (Isolation Level)</param>
        /// <param name="transactionScopeOption">提供建立交易範圍的其他選項</param>
        public UseTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required)
        {
            this.IsolationLevel = isolationLevel;
            this.TransactionScopeOption = transactionScopeOption;
        }

        /// <summary>
        /// 指定交易的隔離等級 (Isolation Level)。
        /// </summary>
        public IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// 提供建立交易範圍的其他選項
        /// </summary>
        public TransactionScopeOption TransactionScopeOption { get; }

        /// <summary>
        /// Implements advice logic. Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">Method advice context, passed to method advisors</param>
        public void Advise(MethodAdviceContext context)
        {
            TransactionOptions options = new TransactionOptions
            {
                IsolationLevel = this.IsolationLevel,
                Timeout = new TimeSpan(0, 2, 0)
            };

            using (TransactionScope scope = new TransactionScope(this.TransactionScopeOption, options))
            {
                context.Proceed();

                scope.Complete();
            }
        }
    }
}