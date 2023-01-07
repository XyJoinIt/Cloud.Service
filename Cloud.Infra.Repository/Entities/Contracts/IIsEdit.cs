namespace Cloud.Infra.Repository.Entities;

public interface IIsEdit
{
    /// <summary>
    /// 修改人
    /// </summary>
    public long? EditId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? EditTime { get; set; }
}
