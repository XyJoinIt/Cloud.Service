using Cloud.Infra.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Cloud.Infra.WebApi.Filter
{
    public class CloudExceptionFilter: IAsyncExceptionFilter
    {
        private readonly ILogger _logger;

        public CloudExceptionFilter(ILogger<CloudExceptionFilter> logger)
        {
            _logger= logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (type.Name == nameof(CloudExceptionFilter))
            {
                if (context.ExceptionHandled == false)
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.HttpContext.Response.ContentType = "text/html; charset=UTF-8";
                    if (!string.IsNullOrEmpty(context.Exception.Message))
                    {
                        context.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes(context.Exception.Data.ToJson()));
                    }
                }
                context.ExceptionHandled = true;
            }
            else if (context.Exception is BadHttpRequestException && context.Exception.Message == "Unexpected end of request content.")
            {
                // https://github.com/dotnet/aspnetcore/issues/23949
                // 此问题将在.net8解决
                context.ExceptionHandled = true;
            }

            return Task.CompletedTask;
        }
    }
}
