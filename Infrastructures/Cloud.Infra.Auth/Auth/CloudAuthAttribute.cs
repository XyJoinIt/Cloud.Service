using Cloud.Infra.Auth.Inside;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Auth
{
    /// <summary>
    /// 授权
    /// </summary>
    public class CloudAuthAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public CloudAuthAttribute(ClaimType type, string claimValue = "") : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new[] { new Claim(type.ToString(), claimValue) };
        }
    }
}
