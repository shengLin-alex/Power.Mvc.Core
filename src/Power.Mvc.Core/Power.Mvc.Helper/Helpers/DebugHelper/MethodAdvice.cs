using ArxOne.MrAdvice.Advice;
using Microsoft.Extensions.Logging;
using Power.Mvc.Helper.Extensions;
using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// RecordMethodCall
    /// </summary>
    public class RecordMethodCall : Attribute, IMethodAdvice
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILogger<RecordMethodCall> Logger => PackageDiResolver.Current.GetService<ILogger<RecordMethodCall>>();

        /// <summary>
        /// Implements advice logic. Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The method advice context.</param>
        public void Advise(MethodAdviceContext context)
        {
            string className = context.TargetMethod.DeclaringType?.Name;
            string methodName = context.TargetMethod.Name;
            this.Logger.EnterMethod(className, methodName);
            context.Proceed();
            this.Logger.LeaveMethod(methodName);
        }
    }
}