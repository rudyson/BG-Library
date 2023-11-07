using BGNet.TestAssignment.Common.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;

namespace BGNet.TestAssignment.Api
{
    public class CancelationTokenFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CancelationTokenFilter> _logger;

        public CancelationTokenFilter(ILogger<CancelationTokenFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is OperationCanceledException)
            {
                var message = String.Join(
                    " ",
                    typeof(OperationCanceledException),
                    context.Exception.Message.ToString());
                _logger.LogError(message);
                context.HttpContext.Response.StatusCode = 200;
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.WriteAsync(ResponseWrapper<CancelationTokenFilter>.Wrap(ResponseCodes.CancelationTokenHandled).ToJson());
                context.ExceptionHandled = true;
            }
            else base.OnException(context);
        }
    }
}
