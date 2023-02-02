using Cloud.Infra.Auth.Enum;
using Cloud.Platform.Repository.Service.Sys;
using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Platform.Web.Modular.Sys;
/// <summary>
/// 用户控制器
/// </summary>
public class SysUserController : PlatformCurdController<ISysUserRepository, AddSysUserDto, EditSysUserDto,SysUserPageParam>
{
    private readonly ISysUserRepository _sysUserService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysUserService"></param>
    public SysUserController(ISysUserRepository sysUserService) : base(sysUserService)
    {
        _sysUserService = sysUserService;
    }
}
