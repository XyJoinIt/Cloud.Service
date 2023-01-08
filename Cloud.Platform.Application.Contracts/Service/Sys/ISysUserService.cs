using Cloud.Platform.Application.Contracts.Dto.Sys.SysUser;

namespace Cloud.Platform.Application.Contracts.Service.Sys;

/// <summary>
/// 用户服务
/// </summary>
public interface ISysUserService:IBasePlatformService<AddSysUserDto,EditSysUserDto>
{
    
}