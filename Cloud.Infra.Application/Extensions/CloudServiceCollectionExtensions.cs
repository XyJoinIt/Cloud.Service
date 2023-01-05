using Cloud.Infrastructures.Applicatoins.Auth;
using Cloud.Infrastructures.Applicatoins.Auth.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Infrastructures.Applicatoins.Extensions;

public static class CloudServiceCollectionExtensions
{
    public static IServiceCollection AddCloudService(this IServiceCollection services, IConfiguration configuration = null)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        //登录用户
        services.AddScoped<ILoginUser, LoginUser>();
        //鉴权服务
        services.AddScoped<IAuthService, PlatformAuthServiceImpl>();
        return services;
    }
}
