namespace Cloud.Infra.WebApi.AppCode.Base;

/// <summary>
/// curd基础接口
/// </summary>
/// <typeparam name="TAddDto"></typeparam>
/// <typeparam name="TEditDto"></typeparam>
public interface ICurdRepository<in TAddDto, in TEditDto,in PageParam>
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

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    abstract Task<AppResult> Page(PageParam param);
}