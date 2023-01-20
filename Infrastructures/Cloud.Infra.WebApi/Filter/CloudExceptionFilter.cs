using Cloud.Infra.WebApi.AppCode;

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
            if (type.Name == nameof(CloudException))
            {
                if (context.ExceptionHandled == false)
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.HttpContext.Response.ContentType = "text/html; charset=UTF-8";
                    if (!string.IsNullOrEmpty(context.Exception.Message))
                    {
                        context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(context.Exception.Data.ToJson()));
                    }
                }
                context.ExceptionHandled = true;
            }
            return Task.CompletedTask;
        }
    }
}
