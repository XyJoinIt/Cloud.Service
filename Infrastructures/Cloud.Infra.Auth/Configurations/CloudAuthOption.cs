using Cloud.Infra.Auth.Enum;

namespace Cloud.Infra.Auth.Configurations
{
    /// <summary>
    /// 授权参数
    /// </summary>
    public class CloudAuthOption
    {
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; } = default!;
        
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }= default!;
        
        /// <summary>
        /// SecurityKey
        /// </summary>
        public string SecurityKey { get; set; }= default!;

        /// <summary>
        /// 授权策略
        /// </summary>
        public Permissions Permissions { get; set; }
    }
}
