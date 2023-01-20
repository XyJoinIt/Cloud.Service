using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud.Infra.WebApi.AppCode.Base;
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
    public class PlatformCurdController<TService, TAddDto, TEditDto> : BaseCurdController<TService, TAddDto, TEditDto>
        where TService : ICurdRepository<TAddDto, TEditDto>
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