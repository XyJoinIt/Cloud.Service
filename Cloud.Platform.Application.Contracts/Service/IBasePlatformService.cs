namespace Cloud.Platform.Application.Contracts.Service;

public interface IBasePlatformService<in TAddDto,in TEditDto>
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    Task Add(TAddDto input);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <returns></returns>
    Task Edit(TEditDto input);

    /// <summary>
    /// 删除
    /// </summary>
    /// <returns></returns>
    Task Delete(long id);
}