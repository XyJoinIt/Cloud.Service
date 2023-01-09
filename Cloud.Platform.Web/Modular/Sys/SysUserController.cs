using Cloud.Platform.Application.Contracts.Service.Sys;
using Cloud.Platform.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Platform.Web.Modular.Sys
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class SysUserController : BasePlatformController
    {
        private readonly ISysUserService _sysUserService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserService"></param>
        public SysUserController(ISysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public Task SendMsg([FromForm] string msg)
        {
            _sysUserService.SendMsg(msg);
            return Task.CompletedTask;
        }
    }
}
