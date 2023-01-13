using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cloud.Infra.Repository.IRepositories;
using Cloud.Infra.WebApi.AppCode.IoCDependencyInjection;
using Cloud.Infra.WebApi.Configurations;
using Cloud.Platform.Model;
using Microsoft.Extensions.DependencyModel;

var builder = WebApplication.CreateBuilder(args);
//初始化配置
InitConfiguration(builder.Configuration);
//注入通用服务
builder.Services.AddCloudService(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//注入数据库
builder.Services.AddInfraRepository<PlatformDbContext>(option =>
{
    var dbOptions = builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    option.UseMySql(dbOptions.PlatformDb!, new MySqlServerVersion(new Version()),
             sqlOptions =>
             {
                 sqlOptions.MigrationsAssembly("Cloud.Platform.Model");
                 sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 sqlOptions.EnableStringComparisonTranslations();
             }
        );
});

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule(new DependencyAutoInjection("Platform"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

//初始化配置
void InitConfiguration(IConfiguration configuration)
{
    GlobalConfig.DbConnectionOptions = configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    GlobalConfig.EventBusOptions = configuration.GetSection("EventBusOptions").Get<EventBusOptions>()!;
}