namespace Cloud.Platform.Model.Entity;

/// <summary>
/// 角色表
/// </summary>
[Table("SysRole")]
[Comment("角色表")]
public class SysRole : FullEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [Comment("名称")]
    [Required, MaxLength(20)]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Comment("编码")]
    [Required, MaxLength(50)]
    public string Code { get; set; }

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

