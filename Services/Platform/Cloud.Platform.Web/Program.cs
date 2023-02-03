using Cloud.Infra.Auth.Configurations;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.WebApi.Extensions;
using Consul;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
//初始化配置
InitConfiguration(builder.Configuration);
//注入通用服务
builder.Services.AddCloudService<PlatformDbContext>(x =>
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

ConsulClient consulClient = new ConsulClient(c =>
{
    c.Address = new Uri("http://localhost:8500");
    c.Datacenter = "test_demo";
});
string ip = builder.Configuration["ip"] ?? "localhost"; //ip
int port = int.Parse(builder.Configuration["port"] ?? "6200"); //端口
string tag = builder.Configuration["weight"] ?? "1"; //权重
if (port == 0)
{
    Console.WriteLine($"https://{ip}:{port}注册失败：请指定port,且不能重复");
    return;
}
consulClient.Agent.ServiceRegister(new AgentServiceRegistration()
{
    ID = Guid.NewGuid() + "Port：" + port,
    Name = "PlatfromService",//服务名称（组）
    Address = ip,//ip地址
    Port = port,//端口
    //标签参数，可以在注册的时候根据拿到tags标签来当权重，可以是属于地址参数上的tag
    //注册服务时指定权重，分配时获取权重并以此为依据分配实例
    Tags = new string[] { tag },

    #region 配置心跳检查的
    Check = new AgentServiceCheck()
    {
        //心跳时间
        Interval = TimeSpan.FromSeconds(10),
        //心跳地址
        HTTP = $"https://{ip}:{port}/api/HealthCheck/ConsulCheck",
        //超时时间
        Timeout = TimeSpan.FromSeconds(5),
        //取消服务注册时间
        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
    }
    #endregion
}); 
Console.WriteLine($"https://{ip}:{port}完成注册");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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
    GlobalConfig.AuthOption = configuration.GetSection("AuthOption").Get<AuthOption>()!;
}