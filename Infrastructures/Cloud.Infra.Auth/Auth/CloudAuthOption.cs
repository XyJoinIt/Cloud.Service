namespace Cloud.Infra.Auth.Auth
{
    public class CloudAuthOption
    {
        /// <summary>
        /// 平台
        /// </summary>
        public JwtSettings? PlatformJwt { get; set; }
    }

    public abstract class JwtSettings
    {
        public string? SecretKey { get; set; }

        public long Expire { get; set; }
    }
}
