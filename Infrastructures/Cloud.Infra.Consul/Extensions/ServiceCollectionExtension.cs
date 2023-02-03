using Cloud.Infra.Consul.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Infra.Consul.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Consul注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="consulSection"></param>
    /// <returns></returns>


    public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime applicationLifetime,ConsulOptions consulOptions)
    {
        var consulClient = new ConsulClient(x =>
        {
            // consul 服务地址
            x.Address = new Uri(consulOptions.ConsulUrl);
        });

        //string ip = app.Configuration["ip"] ?? "localhost"; //ip
        //int port = int.Parse(builder.Configuration["port"] ?? "6200"); //端口
        //string tag = builder.Configuration["weight"] ?? "1"; //权重

        var registration = new AgentServiceRegistration()
        {
            ID = Guid.NewGuid().ToString(),
            Name = consulOptions.ServiceName,// 服务名
            Address = consulOptions.ConsulUrl, // 服务绑定IP
            Port = consulOptions.Timeout, // 服务绑定端口

            //标签参数，可以在注册的时候根据拿到tags标签来当权重，可以是属于地址参数上的tag
            //注册服务时指定权重，分配时获取权重并以此为依据分配实例
            Tags = new string[] { "1" },

            Check = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                HTTP = consulOptions.HealthCheckUrl,//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            }
        };

        // 服务注册
        consulClient.Agent.ServiceRegister(registration).Wait();

        // 应用程序终止时，服务取消注册
        applicationLifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        });

        return app;
    }

}
