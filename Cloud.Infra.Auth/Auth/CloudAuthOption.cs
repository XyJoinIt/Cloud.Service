using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Auth
{
    public class CloudAuthOption
    {
        /// <summary>
        /// 平台
        /// </summary>
        public JwtSettings? PlatformJwt { get; set; }
    }

    public class JwtSettings
    {
        public string? SecretKey { get; set; }

        public long Expire { get; set; }
    }
}
