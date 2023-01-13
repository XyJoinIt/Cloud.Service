using Cloud.Platform.Repository.Dto.Sys.SysUser;

namespace Cloud.Platform.Repository.Service.Sys;

/// <summary>
/// 用户服务
/// </summary>
public interface ISysUserRepository:IBasePlatformRepository<AddSysUserDto,EditSysUserDto>
{
    public string SendMsg(string msg);
}