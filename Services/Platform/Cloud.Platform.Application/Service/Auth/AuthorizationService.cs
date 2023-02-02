using Cloud.Infra.Auth.Enum;
using Cloud.Infra.Auth.HttpContextUser;
using Cloud.Infra.Auth.Utils;
using Cloud.Infra.WebApi.Configurations;
using Cloud.Platform.Repository.Dto.Auth;
using Cloud.Platform.Repository.Service.Auth;
using Cloud.Platform.Repository.Service.Sys;

namespace Cloud.Platform.Service.Service.Auth;

/// <summary>
/// 授权服务
/// </summary>
public class AuthorizationService : IAuthorizationRepository
{
    private readonly IValidator<InputLoginDot> _inputLoginValidator;
    private readonly IRepository<SysUser> _sysUserRepository;
    private readonly IEncryptionRepository _encryption;


    public AuthorizationService(IRepository<SysUser> sysUserRepository, IValidator<InputLoginDot> inputLoginValidator, IEncryptionRepository encryption)
    {
        _sysUserRepository = sysUserRepository;
        _inputLoginValidator = inputLoginValidator;
        _encryption = encryption;
    }

    /// <summary>
    /// 用户名登陆
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<AppResult> Login(InputLoginDot input)
    {
        input.NotNull(nameof(input));
        var validator = await _inputLoginValidator.ValidateAsync(input);
        if (!validator.IsValid)
            return AppResult.Error(validator);
        var user = await _sysUserRepository.FirstOrDefaultAsync(x => x.userInfo!.Account == input.Account);
        if (user == null) throw new CloudException("用户不存在。");
        else
        {
            if (input.PassWord != null && !_encryption.CheckPasswordAsync(passwordHash: user.userInfo!.Password,
                    securityStamp: user.userInfo.SecurityStamp,
                    password: input.PassWord))
                throw new CloudException("密码错误");
        }

        var token = JwtUtil.GenerateToken(new LoginUser()
        {
            Id = user.Id,
            UserName = user.userInfo?.Account,
            Name = user.userInfo?.Name,
            Phone = user.userInfo?.Phone,
            CallType = PermissionsEnum.All,//能访问的权限 这个权限需要后期逻辑判断 待更新
        }, x =>
        {
            x.Issuer = GlobalConfig.AuthOption!.Issuer;
            x.Audience = GlobalConfig.AuthOption.Audience;
            x.SecurityKey = GlobalConfig.AuthOption.SecurityKey;
            x.Exp = GlobalConfig.AuthOption.Exp;
        });
        return AppResult.Success(new { token = token });
    }
}