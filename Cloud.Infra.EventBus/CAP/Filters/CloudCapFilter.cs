namespace Cloud.Infra.EventBus.CAP.Filters;
/// <summary>
/// https://cap.dotnetcore.xyz/user-guide/zh/cap/filter/
/// </summary>
public class CloudCapFilter : SubscribeFilter
{
    private readonly ILogger<CloudCapFilter> _logger;
    public CloudCapFilter(ILogger<CloudCapFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 订阅方法执行前
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task OnSubscribeExecutingAsync(ExecutingContext context)
    {
        return base.OnSubscribeExecutingAsync(context);
    }

    /// <summary>
    /// 订阅方法执行后
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task OnSubscribeExecutedAsync(ExecutedContext context)
    {
       
        return base.OnSubscribeExecutedAsync(context);
    }

    /// <summary>
    /// 订阅方法执行异常
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task OnSubscribeExceptionAsync(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);
        await base.OnSubscribeExceptionAsync(context);
    }

}
