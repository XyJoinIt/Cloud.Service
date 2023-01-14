using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cloud.Infra.Core.Helper;
using Cloud.Infra.EntityFrameworkCore;
using Cloud.Infra.EntityFrameworkCore.Extensions;
using Cloud.Infra.Mapper.Extensions;
using Cloud.Infra.WebApi.AppCode.IoCDependencyInjection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;

namespace Cloud.Infra.Applicatoins.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCloudService<TDbContext>(this IServiceCollection services, Action<AddCloudOptions> action) where TDbContext : DefaultDbContext<TDbContext>
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        AddCloudOptions options = new();
        action(options);
        var ProjectName = options.builder.Configuration.GetSection("ProjectName").Get<string>()!;

        // Add services to the container.
        services.AddControllers(option =>
        {
            option.Filters.Add<CloudExceptionFilter>();
            option.ModelValidatorProviders.Clear();//去除微软自带的参数验证

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
        options.builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>((hostBuilderContext, builder) =>
        {
            builder.RegisterModule(new DependencyAutoInjection(ProjectName));
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
        //注入数据库
        services.AddInfraRepository<TDbContext>(option =>
        {
            var dbOptions = options.builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
            option.UseMySql(dbOptions.DefaultDb!, new MySqlServerVersion(new Version()),
                     sqlOptions =>
                     {
                         sqlOptions.MigrationsAssembly($"Cloud.{ProjectName}.Model");
                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                         sqlOptions.EnableStringComparisonTranslations();
                     }
                );
        });

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
    /// <summary>
    /// 项目引用依赖
    /// </summary>
    public DependencyContext? dependencyContext { get; set; }

    /// <summary>
    /// WebApplicationBuilder
    /// </summary>
    public WebApplicationBuilder builder { get; set; } = default!;
}
