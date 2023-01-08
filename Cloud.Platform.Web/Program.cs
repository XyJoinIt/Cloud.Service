using Cloud.Infra.Repository.IRepositories;
using Cloud.Infra.WebApi.Configurations;
using Cloud.Platform.Model;

var builder = WebApplication.CreateBuilder(args);

//注入通用服务
builder.Services.AddCloudService(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//注入仓储

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