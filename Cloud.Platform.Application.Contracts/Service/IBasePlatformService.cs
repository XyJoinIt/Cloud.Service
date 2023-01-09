using Cloud.Infra.WebApi.Dependency;

namespace Cloud.Platform.Application.Contracts.Service;

public interface IBasePlatformService<in TAddDto, in TEditDto> : IScopedDependency
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