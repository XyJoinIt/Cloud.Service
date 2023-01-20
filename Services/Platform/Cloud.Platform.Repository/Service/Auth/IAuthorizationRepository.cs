using Cloud.Platform.Repository.Dto.Auth;

namespace Cloud.Platform.Repository.Service.Auth;

public interface IAuthorizationRepository
{
    /// <summary>
    /// 用户名登陆
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<AppResult> Login(InputLoginDot input);
}