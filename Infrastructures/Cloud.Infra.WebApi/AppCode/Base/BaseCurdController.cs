using Microsoft.AspNetCore.Mvc;

namespace Cloud.Infra.WebApi.AppCode.Base;

/// <summary>
/// Curd基类控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TAddDto"></typeparam>
/// <typeparam name="TEditDto"></typeparam>
public class BaseCurdController<TService, TAddDto, TEditDto, TPageParam> : ControllerBase where TService : ICurdRepository<TAddDto, TEditDto,TPageParam>
{
    private readonly TService _service;
    /// <summary>
    /// 构造函数
    /// </summary>
    public BaseCurdController(TService service)
    {
        _service = service;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<AppResult> Add([FromBody] TAddDto input) => await _service.Add(input);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    public virtual async Task<AppResult> Delete([FromRoute] long id) => await _service.Delete(id);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public virtual async Task<AppResult> Edit([FromBody] TEditDto input) => await _service.Edit(input);

    /// <summary>
    /// 分页
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<AppResult> Page([FromQuery] TPageParam input) => await _service.Page(input);
}