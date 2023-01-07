using Cloud.Infra.Redis.Extensions;
using Cloud.Infra.WebApi.Extensions;
using Cloud.Infrastructures.Applicatoins.Auth;
using Cloud.Infrastructures.Applicatoins.Auth.Impl;
using FreeRedis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yitter.IdGenerator;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Cloud.Infra.WebApi.Filter;
using Microsoft.AspNetCore.Builder;

namespace Cloud.Infrastructures.Applicatoins.Extensions;

public static class CloudServiceCollectionExtensions
{
    public static IServiceCollection AddCloudService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        // Add services to the container.
        services.AddControllers(option =>
        {
            option.Filters.Add<CloudExceptionFilter>();
        }).AddNewtonsoftJson(option =>
        {
            option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            option.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
        //redis
        services.AddCloudRedisService(x => x.RedisStrConn = builder.Configuration.GetConnectionString("RedisDb")!);
        //登录用户
        services.AddScoped<ILoginUser, LoginUser>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //鉴权服务
        services.AddScoped<IAuthService, PlatformAuthServiceImpl>();

        //Swagger
        services.AddSwaggerSetup(builder);

        var redis = services.BuildServiceProvider().GetService<RedisClient>();
        var workid = redis!.Get<ushort>("SnowflakeWorkId");
        workid = (ushort)(++workid & 62);
        var Idoption = new IdGeneratorOptions(workid);
        YitIdHelper.SetIdGenerator(Idoption);
        redis.Set("SnowflakeWorkId", workid);

        return services;
    }
}
