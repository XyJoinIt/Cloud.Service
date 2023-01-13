
var builder = WebApplication.CreateBuilder(args);
//初始化配置
InitConfiguration(builder.Configuration);
//注入通用服务
builder.Services.AddCloudService(x =>
{
    x.builder = builder;
    x.dependencyContext = DependencyContext.Default!;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//注入数据库
builder.Services.AddInfraRepository<BlogDbContext>(option =>
{
    var dbOptions = builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    option.UseMySql(dbOptions.PlatformDb!, new MySqlServerVersion(new Version()),
             sqlOptions =>
             {
                 sqlOptions.MigrationsAssembly("Cloud.Blog.Model");
                 sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 sqlOptions.EnableStringComparisonTranslations();
             }
        );
});

var app = builder.Build();

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