namespace Cloud.Infra.EntityFrameworkCore.Entities;

public interface IIsDelete
{
    /// <summary>
    /// 删除人
    /// </summary>
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeleteTime { get; set; }

    /// <summary>
    /// 是否软删除
    /// </summary>
    public bool IsDelete { get; set; }
}
