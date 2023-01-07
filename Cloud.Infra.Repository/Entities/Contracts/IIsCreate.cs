namespace Cloud.Infra.Repository.Entities;

public interface IIsCreate
{
    /// <summary>
    /// 新增人
    /// </summary>
    public long CreateId { get; set; }

    /// <summary>
    /// 新增时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
