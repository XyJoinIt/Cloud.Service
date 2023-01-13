namespace Cloud.Platform.Model.Entity;

/// <summary>
/// 机构表
/// </summary>
[Table("SysOrg")]
[Comment("机构表")]
public class SysOrg:FullEntity
{
    /// <summary>
    /// 父Id
    /// </summary>
    public long Pid { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required, MaxLength(64)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; set; } = 100;

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(128)]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public CommonStatus Status { get; set; } = CommonStatus.正常;
}