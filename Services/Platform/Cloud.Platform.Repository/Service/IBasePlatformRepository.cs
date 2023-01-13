namespace Cloud.Platform.Repository.Service;

public interface IBasePlatformRepository<in TAddDto, in TEditDto>
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    abstract Task Add(TAddDto input);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <returns></returns>
    abstract Task Edit(TEditDto input);

    /// <summary>
    /// 删除
    /// </summary>
    /// <returns></returns>
    abstract Task Delete(long id);
}