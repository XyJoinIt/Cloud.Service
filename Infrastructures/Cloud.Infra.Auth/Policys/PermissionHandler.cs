using Microsoft.AspNetCore.Authorization;
using Cloud.Infra.Auth.Enum;
using FreeRedis;
using Microsoft.AspNetCore.Http;

namespace Cloud.Infra.Auth.Policys;

public abstract class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IRedisClient _redisClient;
    private readonly IHttpContextAccessor _contextAccessor;

    protected PermissionHandler(IRedisClient redisClient, IHttpContextAccessor contextAccessor)
    {
        _redisClient = redisClient;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// 策略逻辑
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (!context.User.Claims.Any())
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var httpContext = _contextAccessor.HttpContext;
        //请求url
        if (httpContext != null)
        {
            //策略判断
            var permissions = context.User.Claims.First(x => x.Type == nameof(Permissions)).Value;
            //从redis中获取的系统凭证是否和当前凭证相同,如果相同那么放行。
            //var str = _redisClient.GetSet("")
            if (permissions != "")
            {
                
            }
        }

        context.Succeed(requirement);

        throw new NotImplementedException();
    }
}