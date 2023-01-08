namespace Cloud.Platform.Model.Entity;

/// <summary>
/// 角色表
/// </summary>
[Table("SysRole")]
[Comment("角色表")]
public class SysRole : FullEntity
{
	/// <summary>
	/// 角色名
	/// </summary>
	public string Name { get; set; } = default!;

	/// <summary>
	/// 排序
	/// </summary>
	public int Sort { get; set; } = 100;

	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }

	/// <summary>
	/// 状态
	/// </summary>
	public CommonStatus Status { get; set; } = CommonStatus.正常;
}

