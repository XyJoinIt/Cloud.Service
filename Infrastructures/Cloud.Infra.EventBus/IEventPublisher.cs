namespace Cloud.Infra.EventBus;

public interface IEventPublisher
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">订阅名</param>
    /// <param name="obj">内容</param>
    /// <param name="headers">头</param>
    /// <returns></returns>
    public Task PublishAsync<T>(string name, T obj, IDictionary<string, string>? headers = null) where T : class;

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">订阅名</param>
    /// <param name="obj">内容</param>
    /// <param name="headers">头</param>
    /// <returns></returns>
    public void Publish<T>(string name, T obj, IDictionary<string, string>? headers = null) where T : class;

    /// <summary>
    /// 延时消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="timeSpan"></param>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public void PublishDelayMsg<T>(TimeSpan timeSpan, string name, T obj, IDictionary<string, string>? headers = null) where T : class;
}
