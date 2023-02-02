using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.Auth.HttpContextUser;
using Cloud.Infra.Auth.Policys;

namespace Cloud.Infra.Auth.Utils;

public static class JwtUtil
{
    /// <summary>
    /// 生成Token
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    private static string CreateToken(IEnumerable<Claim> claims, AuthOption authOption)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOption.SecurityKey));
        var reds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var now = DateTime.Now;

        var securityToken = new JwtSecurityToken(
            issuer: authOption.Issuer,
            audience: authOption.Audience,
            claims: claims,
            expires: now.AddDays(authOption.Exp),
            signingCredentials: reds);
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }

    /// <summary>
    /// 生成Jwt
    /// </summary>
    /// <param name="loginUser"></param>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public static string GenerateToken(AuthUser loginUser, AuthOption authOption)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Typ, "JWT"),
            new Claim(nameof(loginUser.Name).ToLower(), loginUser.Name ?? ""),
            new Claim(nameof(loginUser.UserName).ToLower(), loginUser.UserName ?? ""),
            new Claim(nameof(loginUser.CallType).ToLower(), loginUser.CallType.ToString()!),
            new Claim(nameof(loginUser.Phone).ToLower(), loginUser.Phone ?? ""),
            new Claim(nameof(loginUser.Id).ToLower(), loginUser.Id.ToString()),
        };
        return CreateToken(claims, authOption);
    }

    /// <summary>
    /// 校验Token
    /// </summary>
    /// <param name="token">token</param>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public static bool CheckToken(string token, string securityKey)
    {
        var principal = GetPrincipal(token, securityKey);
        return principal is not null;
    }

    /// <summary>
    /// 从Token中获取用户身份
    /// </summary>
    /// <param name="token"></param>
    /// <param name="securityKey">securityKey明文,Java加密使用的是Base64</param>
    /// <returns></returns>
    private static ClaimsPrincipal? GetPrincipal(string token, string securityKey)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                ValidateLifetime = true
            };
            return handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}