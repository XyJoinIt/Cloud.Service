namespace Cloud.Infra.Consul.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Consul注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="consulSection"></param>
    /// <returns></returns>
    public static void RegisterConsul(this IHost app, IHostApplicationLifetime applicationLifetime, ConsulOptions consulOptions)
    {
        //留扩展 可能后面不用consul
        if (consulOptions.RegistrationType != Enum.RegisteredType.Consul)
            return;

        var consulClient = new ConsulClient(x =>
        {
            // consul 服务地址
            x.Address = new Uri(consulOptions.ConsulUrl);
        });

        var configuration = app.Services.GetService<IConfiguration>();

        var Scheme = configuration!["agree"] ?? "https"; //协议
        var ip = configuration["ip"] ?? "localhost"; //ip
        var port = int.Parse(configuration["port"] ?? "0"); //端口
        var tag = configuration["weight"] ?? "1"; //权重
        if (port == 0)
        {
            var msg = $"https://{ip}:{port}注册失败：请指定port,且不能重复";
            Console.WriteLine(msg);
            throw new Exception(msg);
        }
        if (Scheme == null)
        {
            var msg = $"注册失败：请指定Scheme(http/https)";
            Console.WriteLine(msg);
            throw new Exception(msg);
        }

        var registration = new AgentServiceRegistration()
        {
            ID = Guid.NewGuid().ToString(),
            Name = consulOptions.ServiceName,// 服务名
            Address = ip, // 服务绑定IP
            Port = port, // 服务绑定端口

            //标签参数，可以在注册的时候根据拿到tags标签来当权重，可以是属于地址参数上的tag
            //注册服务时指定权重，分配时获取权重并以此为依据分配实例
            Tags = consulOptions.ServerTags,

            Check = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(consulOptions.HealthCheckIntervalInSecond),//健康检查时间间隔
                HTTP = $"{Scheme}://{ip}:{port}{consulOptions.HealthCheckUrl}",
                Timeout = TimeSpan.FromSeconds(consulOptions.Timeout)
            }
        };

        // 服务注册
        consulClient.Agent.ServiceRegister(registration).Wait();

        Console.WriteLine($"{Scheme}://{ip}:{port}完成服务治理发现注册");

        // 应用程序终止时，服务取消注册
        applicationLifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        });
    }
}
