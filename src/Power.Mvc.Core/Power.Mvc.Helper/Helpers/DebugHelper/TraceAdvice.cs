using ArxOne.MrAdvice.Advice;
using Microsoft.Extensions.Logging;
using Power.Mvc.Helper.Extensions;
using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 程式執行流程追蹤
    /// </summary>
    public class TraceAdvice : Attribute, IMethodAdvice
    {
        /// <summary>
        /// Implements advice logic. Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The method advice context.</param>
        public void Advise(MethodAdviceContext context)
        {
            ILogger<TraceAdvice> logger = PackageDiResolver.Current.GetService<ILogger<TraceAdvice>>();

            try
            {
                if (context.TargetMethod.DeclaringType != null)
                {
                    string className = context.TargetMethod.DeclaringType.Name;
                    string methodName = context.TargetMethod.Name;
                    logger.EnterMethod(className, methodName);
                }

                context.Proceed();
            }
            catch (Exception e)
            {
                logger.LogCritical(e.ToString());
                throw;
            }
            finally
            {
                if (context.TargetMethod.DeclaringType != null)
                {
                    logger.LeaveMethod(context.TargetMethod.Name);
                }
            }
        }
    }
}