using  Cloud.Infra.WebApi.AppCode.Base;
namespace Cloud.Platform.Repository.Service;
/// <summary>
/// platform 基础服务接口
/// </summary>
/// <typeparam name="TAddDto"></typeparam>
/// <typeparam name="TEditDto"></typeparam>
/// <typeparam name="TPageParam"></typeparam>
public interface IBasePlatformRepository<in TAddDto, in TEditDto,in TPageParam> :ICurdRepository<TAddDto, TEditDto, TPageParam>
{
    
}
