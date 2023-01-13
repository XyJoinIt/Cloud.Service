using Cloud.Platform.Repository.Service.Sys;
using Cloud.Platform.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Cloud.Infra.WebApi.AppCode;
using Cloud.Platform.Repository.Dto.Sys.SysUserManage;

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
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AppResult> Add([FromBody] AddSysUserDto input) => await _sysUserService.Add(input);
    }
}
