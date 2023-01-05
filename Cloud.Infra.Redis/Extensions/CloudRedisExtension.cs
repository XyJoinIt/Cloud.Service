using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Infra.Redis.Extensions;

public static class CloudRedisExtension
{
    public static IServiceCollection AddCloudRedisService(this IServiceCollection service, Action<RedisOptions> option)
    {

        return service;
    }
}

public class RedisOptions
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public string RedisStrConn { get; set; }
}

