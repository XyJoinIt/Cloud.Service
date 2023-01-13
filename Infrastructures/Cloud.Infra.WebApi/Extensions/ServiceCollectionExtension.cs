using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cloud.Infra.Core.Helper;
using Cloud.Infra.Mapper.Extensions;
using Cloud.Infra.WebApi.AppCode.IoCDependencyInjection;
using FluentValidation;
using Microsoft.Extensions.DependencyModel;

namespace Cloud.Infra.Applicatoins.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCloudService(this IServiceCollection services, Action<AddCloudOptions> action)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        AddCloudOptions options = new();
        action(options);

        // Add services to the container.
        services.AddControllers(option =>
        {
            option.Filters.Add<CloudExceptionFilter>();
            option.ModelValidatorProviders.Clear();

        }).AddNewtonsoftJson(option =>
        {
            option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            option.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
        //redis
        services.AddCloudRedisService(x => x.RedisStrConn = options.builder.Configuration.GetConnectionString("RedisDb")!);
        //登录用户
        services.AddScoped<ILoginUser, LoginUser>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //鉴权服务
        services.AddScoped<IAuthService, PlatformAuthServiceImpl>();
        //Swagger
        services.AddSwaggerSetup(options.builder);
        //注入Cap消息
        services.AddCapEventBus(options.builder);
        //控制反转
        var str = options.builder.Configuration.GetSection("ProjectName").Get<string>()!;
        options.builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>((hostBuilderContext, builder) =>
        {
            builder.RegisterModule(new DependencyAutoInjection(str));
        });
        //验证服务
        if (options.dependencyContext != null)
        {
            AssemblyHelper.Init(options.dependencyContext);
            var assemblies = AssemblyHelper.FindTypes(o => o.IsBaseOn(typeof(AbstractValidator<>)) && o.IsClass == true && !o.IsAbstract);
            Array.ForEach(assemblies, a =>
            {
                options.builder.Services.AddValidatorsFromAssemblyContaining(a);
            });

            services.AddAdncInfraAutoMapper(AssemblyHelper.AllTypes);
        }

        //雪花Id
        var redis = services.BuildServiceProvider().GetService<RedisClient>();
        var workid = redis!.Get<int>("SnowflakeWorkId");
        if (workid == 62) workid = 1;
        workid += 1;
        var Idoption = new IdGeneratorOptions((ushort)workid);
        YitIdHelper.SetIdGenerator(options: Idoption);
        redis.Set("SnowflakeWorkId", workid);

        return services;
    }
}

public class AddCloudOptions
{
    public DependencyContext? dependencyContext { get; set; }

    public WebApplicationBuilder builder { get; set; } = default!;
}
