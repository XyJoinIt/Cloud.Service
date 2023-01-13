namespace Cloud.Infra.Repository.Entities;

public interface Ikey<T>
{
    /// <summary>
    /// 主键
    /// </summary>
    T Id { get; set; }
}
