using Cloud.Infra.WebApi.Dependency;
using Microsoft.Extensions.DependencyModel;

namespace Cloud.Infra.Applicatoins.Extensions;

public static class ServiceCollectionExtension
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
        //注入Cap消息
        services.AddCapEventBus(builder);

        var redis = services.BuildServiceProvider().GetService<RedisClient>();
        var workid = redis!.Get<ushort>("SnowflakeWorkId");
        workid = (ushort)(++workid & 62);
        var Idoption = new IdGeneratorOptions(workid);
        YitIdHelper.SetIdGenerator(Idoption);
        redis.Set("SnowflakeWorkId", workid);

        return services;
    }
}
