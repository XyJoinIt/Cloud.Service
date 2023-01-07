using Cloud.Infra.Repository.Entities.Contracts;
using Cloud.Infra.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//ͨ�÷���
builder.Services.AddCloudService(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//���ݿ�
builder.Services.AddInfraRepository(option =>
{
    var _DbOptions = builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    //�����װ�����Զ��л����ݿ�
    option.UseMySql(_DbOptions.PlatformDb!, new MySqlServerVersion(new Version()),
             sqlOptions =>
             {
                 sqlOptions.MigrationsAssembly("Cloud.Platform.Model");
                 sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 sqlOptions.EnableStringComparisonTranslations(); //MySqlҪ���� OrdinalIgnoreCase ���Ǹò����޷�ʹ��
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