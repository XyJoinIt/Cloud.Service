using Cloud.Platform.Repository.Dto.Sys.SysRoleManage;
using Cloud.Platform.Repository.Service.Sys;

namespace Cloud.Platform.Web.Modular.Sys;

/// <summary>
/// 角色控制器
/// </summary>
public class SysRoleController : PlatformCurdController<ISysRoleRepository, AddSysRoleDto, EditSysRoleDto, SysRolePageParam>
{
    private readonly ISysRoleRepository _sysRoleRepository;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysRoleRepository"></param>
    public SysRoleController(ISysRoleRepository sysRoleRepository) : base(sysRoleRepository)
    {
        _sysRoleRepository = sysRoleRepository;
    }
}