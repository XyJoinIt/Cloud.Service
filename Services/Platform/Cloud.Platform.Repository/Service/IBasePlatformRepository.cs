namespace Cloud.Platform.Repository.Service;

public interface IBasePlatformRepository<in TAddDto, in TEditDto>
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    abstract Task<AppResult> Add(TAddDto input);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <returns></returns>
    abstract Task<AppResult> Edit(TEditDto input);

    /// <summary>
    /// 删除
    /// </summary>
    /// <returns></returns>
    abstract Task<AppResult> Delete(long id);
}