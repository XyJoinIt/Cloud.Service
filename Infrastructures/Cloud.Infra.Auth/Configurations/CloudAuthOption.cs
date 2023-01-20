using Cloud.Infra.Auth.Enum;

namespace Cloud.Infra.Auth.Configurations
{
    /// <summary>
    /// 授权参数
    /// </summary>
    public class AuthOption
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
        /// 过期时间 分钟
        /// </summary>
        public int Exp { get; set; }
        
        /// <summary>
        /// 授权策略
        /// </summary>
        public PermissionsEnum? PermissionsEnum { get; set; }
    }
}
