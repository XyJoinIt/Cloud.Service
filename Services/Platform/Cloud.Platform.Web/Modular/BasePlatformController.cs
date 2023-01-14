using Cloud.Infra.WebApi.AppCode.Base;

namespace Cloud.Platform.Web.Modular;

/// <summary>
/// platform基类控制器
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class BasePlatformController<TService,TAddDto,TEditDto> : BaseCurdController<TService,TAddDto,TEditDto> 
    where TService:ICurdRepository<TAddDto,TEditDto>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public BasePlatformController(TService service):base(service)
    {
        
    }
}