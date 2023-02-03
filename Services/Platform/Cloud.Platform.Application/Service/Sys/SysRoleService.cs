using Cloud.Infra.WebApi.Extensions;
using Cloud.Platform.Repository.Dto.Sys.SysRoleManage;
using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
using Cloud.Platform.Repository.Service.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Platform.Service.Service.Sys;

/// <summary>
/// 角色
/// </summary>
public class SysRoleService : BasePlatformRepository<SysRole, AddSysRoleDto, EditSysRoleDto, SysRolePageParam>, ISysRoleRepository
{
    private readonly IRepository<SysRole> _repository;
    private readonly IObjectMapper _objectMapper;

    public SysRoleService(IRepository<SysRole> repository,
                          IValidator<AddSysRoleDto> addValidator,
                          IValidator<EditSysRoleDto> editValidator,
                          IObjectMapper objectMapper) : base(repository, addValidator, editValidator, objectMapper)
    {
        _repository = repository;
        _objectMapper = objectMapper;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public override async Task<AppResult> Page(SysRolePageParam param)
    {
        var list = await _repository.QueryAsNoTracking()
                .Where(x => x.Status != Infra.WebApi.Enum.CommonStatus.停用)
                .WhereIf(!param.Name.IsNullOrEmpty(), x => x!.Name == param.Name)
                .WhereIf(!param.Code.IsNullOrEmpty(), x => x!.Code == param.Code)
                .WhereIf(param.Status != null, x => x!.Status == param.Status)
                .OrderBy(x => x.Sort)
                .ToPageAsync<SysRole, OutSysRolePageDto>(param, _objectMapper);
        return AppResult.Success(list);
    }
}
