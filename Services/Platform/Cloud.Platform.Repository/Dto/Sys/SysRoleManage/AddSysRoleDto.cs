using Cloud.Infra.EntityFrameworkCore.Entities;
using Cloud.Platform.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Cloud.Platform.Repository.Dto.Sys.SysRoleManage;

public class BaseSysRoleDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Comment("名称")]
    [Required, MaxLength(20)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// 编码
    /// </summary>
    [Comment("编码")]
    [Required, MaxLength(50)]
    public string Code { get; set; } = default!;

    /// <summary>
    /// 排序
    /// </summary>
    [Comment("排序")]
    public int Sort { get; set; } = 100;

    /// <summary>
    /// 备注
    /// </summary>
    [Comment("备注")]
    [MaxLength(100)]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（字典 0正常 1停用 2删除）
    /// </summary>
    [Comment("状态")]
    public CommonStatus Status { get; set; } = CommonStatus.正常;
}

[AutoMap(typeof(SysRole), ReverseMap = true)]
public class AddSysRoleDto: BaseSysRoleDto
{

}

[AutoMap(typeof(SysRole), ReverseMap = true)]
public class EditSysRoleDto : BaseSysRoleDto, IDtoId
{
    public long Id { get; set; }
}

[AutoMap(typeof(SysRole), ReverseMap = true)]
public class OutSysRolePageDto : BaseSysRoleDto
{

}

public class SysRolePageParam : BasePage
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public CommonStatus? Status { get; set; }
}

