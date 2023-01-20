using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud.Platform.Repository.Dto.Auth;
using Cloud.Platform.Repository.Service.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Platform.Web.Modular.Auth
{
    /// <summary>
    /// 鉴权控制器
    /// </summary>
    public class AuthorizationController:PlatformController
    {
        private readonly IAuthorizationRepository _authorizationRepository;
         
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorizationController(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<AppResult> Login([FromBody]InputLoginDot input) => _authorizationRepository.Login(input);
    }
}
