namespace Cloud.Infra.WebApi.Configurations;

/// <summary>
/// 全局配置
/// </summary>
public static class GlobalConfig
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public static DbConnectionOptions? DbConnectionOptions { get; set; }

    /// <summary>
    /// 队列消息配置
    /// </summary>
    public static EventBusOptions? EventBusOptions { get; set; }
}