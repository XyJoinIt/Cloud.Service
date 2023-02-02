using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
namespace Cloud.Platform.Repository.Service.Sys;

/// <summary>
/// 用户服务
/// </summary>
public interface ISysUserRepository:IBasePlatformRepository<AddSysUserDto,EditSysUserDto>
{
    /// <summary>
    /// 修改用户状态 (开关)
    /// </summary>
    /// <returns></returns>
    public Task<AppResult> EditUserStart();

    /// <summary>
    /// 获取用户详情信息
    /// </summary>
    /// <returns></returns>
    public Task<AppResult> GetUserInfo();
}