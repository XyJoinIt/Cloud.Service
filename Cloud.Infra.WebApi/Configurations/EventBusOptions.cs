namespace Cloud.Infra.WebApi.Configurations;

public class EventBusOptions
{
    public EventBusType Type { get; set; }

    public RabbitMqOptions? RabbitMqOptions { get; set; }

    public RedisOptions? redisOptions { get; set; }
}

public enum EventBusType
{
    RabbitMq
}
