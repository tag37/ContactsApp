using ContactsApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nexus.Idc.WebApi.Infrastructure.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                _logger.LogError(context.Exception, "Unhandled error in {0} {1}",
                    context.HttpContext.Request.Method, context.HttpContext.Request.Path);
            }
            catch
            {
                // ignored
            }

            context.Result = new ErrorResult(context.Exception.Message, StatusCodes.Status500InternalServerError);
        }
    }
}