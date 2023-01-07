using Cloud.Infra.Repository.Entities.Contracts;
using Cloud.Infra.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//通用服务
builder.Services.AddCloudService(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//数据库
builder.Services.AddInfraRepository(option =>
{
    var _DbOptions = builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    //后面封装可以自动切换数据库
    option.UseMySql(_DbOptions.PlatformDb!, new MySqlServerVersion(new Version()),
             sqlOptions =>
             {
                 sqlOptions.MigrationsAssembly("Cloud.Platform.Model");
                 sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 sqlOptions.EnableStringComparisonTranslations(); //MySql要开启 OrdinalIgnoreCase 不是该参数无法使用
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