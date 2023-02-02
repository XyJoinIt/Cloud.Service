
using Cloud.Infra.Auth.Configurations;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
//��ʼ������
InitConfiguration(builder.Configuration);
//ע��ͨ�÷���
builder.Services.AddCloudService<BlogDbContext>(x =>
{
    x.Builder = builder;
    x.DependencyContext = DependencyContext.Default!;
    x.AuthOption = new AuthOption()
    {
        Audience = GlobalConfig.AuthOption!.Audience,
        Issuer = GlobalConfig.AuthOption!.Issuer,
        Exp = GlobalConfig.AuthOption!.Exp,
        PermissionsEnum = PermissionsEnum.Platform,
        SecurityKey = GlobalConfig.AuthOption.SecurityKey
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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

//��ʼ������
void InitConfiguration(IConfiguration configuration)
{
    GlobalConfig.DbConnectionOptions = configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    GlobalConfig.EventBusOptions = configuration.GetSection("EventBusOptions").Get<EventBusOptions>()!;
    GlobalConfig.AuthOption = configuration.GetSection("AuthOption").Get<AuthOption>()!;
}