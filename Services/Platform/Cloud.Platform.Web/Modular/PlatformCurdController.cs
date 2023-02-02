using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.WebApi.AppCode.Base;
using Consul;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Platform.Web.Modular
{
    /// <summary>
    /// Curd控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TAddDto"></typeparam>
    /// <typeparam name="TEditDto"></typeparam>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = nameof(PolicyType.SystemType))]
    public class PlatformCurdController<TService, TAddDto, TEditDto, TPageParam> : BaseCurdController<TService, TAddDto, TEditDto, TPageParam>
        where TService : ICurdRepository<TAddDto, TEditDto, TPageParam>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public PlatformCurdController(TService service) : base(service)
        {
        }
    }
}