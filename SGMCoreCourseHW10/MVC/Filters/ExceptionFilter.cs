using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace MVC.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger logger;

        public ExceptionFilter(
            ILogger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            this.logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
