using ArxOne.MrAdvice.Advice;
using Microsoft.Extensions.Logging;
using Power.Mvc.Helper.Extensions;
using System;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// ParameterAdvice
    /// </summary>
    public class ParameterAdvice : Attribute, IParameterAdvice
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILogger<ParameterAdvice> Logger => PackageDiResolver.Current.GetService<ILogger<ParameterAdvice>>();

        /// <summary>
        /// Implements advice logic. Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The parameter advice context.</param>
        public void Advise(ParameterAdviceContext context)
        {
            if (context.IsIn)
            {
                this.DumpInParamete(context);
                context.SetValue(context.Value);
            }

            context.Proceed();

            if (context.IsOut && !context.IsIn)
            {
                this.DumpOutParamete(context);
                context.SetValue(context.Value);
            }
        }

        /// <summary>
        /// DumpInParamete
        /// </summary>
        /// <param name="context">The parameter advice context.</param>
        public void DumpInParamete(ParameterAdviceContext context)
        {
            string message = "->(" + context.TargetParameter.Name + ")";

            if (context.Value == null)
            {
                message += " NULL";
            }
            else
            {
                message += context.Value;
            }

            this.Logger.LogInformation(message);
        }

        /// <summary>
        /// DumpOutParamete
        /// </summary>
        /// <param name="context">The parameter advice context.</param>
        public void DumpOutParamete(ParameterAdviceContext context)
        {
            string message = "<-(";

            if (context.Value == null)
            {
                message += " NULL ";
            }
            else
            {
                message += context.Value;
            }

            this.Logger.LogInformation(message + ")");
        }
    }
}