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

namespace Cloud.Infra.WebApi.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// 注入通用服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddCloudService<TDbContext>(this IServiceCollection services, Action<AddCloudOptions> action) where TDbContext : DefaultDbContext<TDbContext>
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        AddCloudOptions options = new();
        action(options);
        var projectName = options.Builder.Configuration.GetSection("ProjectName").Get<string>()!;

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
        services.AddCloudRedisService(x => x.RedisStrConn = options.Builder.Configuration.GetConnectionString("RedisDb")!);
        //登录用户
        services.AddScoped<ILoginUser, LoginUser>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //鉴权服务
        services.AddScoped<IAuthService, PlatformAuthServiceImpl>();
        //Swagger
        services.AddSwaggerSetup(options.Builder);
        //注入Cap消息
        services.AddCapEventBus(options.Builder);
        //控制反转
        options.Builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>((hostBuilderContext, builder) =>
        {
            builder.RegisterModule(new DependencyAutoInjection(projectName));
        });
        //验证服务
        if (options.DependencyContext != null)
        {
            AssemblyHelper.Init(options.DependencyContext);
            var assemblies = AssemblyHelper.FindTypes(o => o.IsBaseOn(typeof(AbstractValidator<>)) && o.IsClass == true && !o.IsAbstract);
            Array.ForEach(assemblies, a =>
            {
                options.Builder.Services.AddValidatorsFromAssemblyContaining(a);
            });

            services.AddAdncInfraAutoMapper(AssemblyHelper.AllTypes);
        }
        //注入数据库
        services.AddInfraRepository<TDbContext>(option =>
        {
            var dbOptions = options.Builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
            option.UseMySql(dbOptions.DefaultDb!, new MySqlServerVersion(new Version()),
                     sqlOptions =>
                     {
                         sqlOptions.MigrationsAssembly($"Cloud.{projectName}.Model");
                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                         sqlOptions.EnableStringComparisonTranslations();
                     }
                );
        });

        //雪花Id
        var redis = services.BuildServiceProvider().GetService<RedisClient>();
        var worked = redis!.Get<int>("SnowflakeWorkId");
        if (worked == 62) worked = 1;
        worked += 1;
        var adoption = new IdGeneratorOptions((ushort)worked);
        YitIdHelper.SetIdGenerator(options: adoption);
        redis.Set("SnowflakeWorkId", worked);

        return services;
    }
}

public class AddCloudOptions
{
    /// <summary>
    /// 项目引用依赖
    /// </summary>
    public DependencyContext? DependencyContext { get; set; }

    /// <summary>
    /// WebApplicationBuilder
    /// </summary>
    public WebApplicationBuilder Builder { get; set; } = default!;
}
