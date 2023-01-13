using Cloud.Platform.Repository.Service.Sys;
using Cloud.Platform.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Platform.Web.Modular.Sys
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class SysUserController : BasePlatformController
    {
        private readonly ISysUserRepository _sysUserService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserService"></param>
        public SysUserController(ISysUserRepository sysUserService)
        {
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public string SendMsg([FromForm] string msg)
        {
            return _sysUserService.SendMsg(msg);
        }
    }
}
