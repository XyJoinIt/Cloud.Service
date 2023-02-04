using Cloud.Infra.Consul.Enum;

namespace Cloud.Infra.Consul.Configurations;

public class ConsulOptions: IRegType
{
    /// <summary>
    /// consul地址
    /// </summary>
    public string ConsulUrl { get; set; } = string.Empty;

    /// <summary>
    /// 服务名
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// 健康检查地址
    /// </summary>
    public string HealthCheckUrl { get; set; } = string.Empty;

    /// <summary>
    /// 健康检查间隔时间（秒）
    /// </summary>
    public int HealthCheckIntervalInSecond { get; set; } = default;

    /// <summary>
    /// 心跳检测超时时间
    /// </summary>
    public int Timeout { get; set; } = default;

    /// <summary>
    /// 权重
    /// </summary>
    public string[] ServerTags { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 治理类型
    /// </summary>
    public RegisteredType RegistrationType { get; set; } = RegisteredType.Direct;
}

public interface IRegType
{
    /// <summary>
    /// 治理类型
    /// </summary>
    public RegisteredType RegistrationType { get; set; }
}