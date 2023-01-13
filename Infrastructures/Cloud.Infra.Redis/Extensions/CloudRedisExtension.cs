using Cloud.Infra.Redis.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Cloud.Infra.Redis.Extensions;
public static class CloudRedisExtension
{
    public static IServiceCollection AddCloudRedisService(this IServiceCollection service, Action<RedisOptions> option)
    {
        RedisOptions options = new();
        option(options);

        RedisClient redisClient = new RedisClient(options.RedisStrConn!)
        {
            Serialize = obj => JsonConvert.SerializeObject(obj),
            Deserialize =(json,type)=>JsonConvert.DeserializeObject(json,type)
        };
        service.AddSingleton(redisClient);
        return service;
    }
}


