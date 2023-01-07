using Cloud.Infra.Auth.Utils;
using Microsoft.Extensions.Options;
namespace Cloud.Infra.Auth.Impl
{
    public class PlatformAuthServiceImpl : IAuthService
    {

        private readonly CloudAuthOption _cloudAuthOption;
        private readonly ILoginUser _loginUser;
        public PlatformAuthServiceImpl(IOptions<CloudAuthOption> cloudAuthOption, ILoginUser loginUser)
        {
            _cloudAuthOption = cloudAuthOption.Value;
            _loginUser = loginUser;
        }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName => nameof(PlatformAuthServiceImpl);

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="token"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Permission(string token)
        {
            if (JwtUtil.CheckToken(token, _cloudAuthOption.PlatformJwt!.SecretKey!))
            {
                SetUserAsync(token);

                return true;
            }
            return false;
        }


        /// <summary>
        /// 设置用户
        /// </summary>
        /// <param name="token"></param>
        public void SetUserAsync(string token)
        {
            var payload = JwtUtil.GetPayload(token);
            _loginUser.Id = payload.Id;
            _loginUser.Name = payload.Name;
            _loginUser.UserName = payload.UserName;
            _loginUser.Phone = payload.Phone;
        }
    }
}
