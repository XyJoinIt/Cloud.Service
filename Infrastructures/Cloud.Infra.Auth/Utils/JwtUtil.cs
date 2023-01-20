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
    private static string CreateToken(IEnumerable<Claim> claims, string securityKey)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        var reds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var securityToken = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddMonths(1),
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
    public static string GenerateToken(LoginUser loginUser, string securityKey)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Typ, "JWT"),
            //new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
            //new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(60).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64), //过期时间
            new Claim(nameof(loginUser.Name).ToLower(), loginUser.Name ?? ""),
            new Claim(nameof(loginUser.UserName).ToLower(), loginUser.UserName ?? ""),
            new Claim(nameof(loginUser.CallType).ToLower(), loginUser.CallType.ToString()!),
            new Claim(nameof(loginUser.Phone).ToLower(), loginUser.Phone ?? ""),
            new Claim(nameof(loginUser.Id).ToLower(), loginUser.Id.ToString())
        };
        return CreateToken(claims, securityKey);
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


    /// <summary>
    /// 获取Token中的用户信息
    /// </summary>
    /// <param name="jwt">token</param>
    /// <returns></returns>
    public static JwtPayload GetPayload(string jwt)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken securityToken = jwtHandler.ReadJwtToken(jwt);

        JwtPayload jwtPayload = new();

        var id = securityToken.Payload.GetValueOrDefault(nameof(LoginUser.Id).ToLower());
        if (id != null)
            jwtPayload.Id = long.Parse(id.ToString()!);

        var userName = securityToken.Payload.GetValueOrDefault(nameof(LoginUser.UserName).ToLower());
        if (userName != null)
            jwtPayload.UserName = userName.ToString();

        var phone = securityToken.Payload.GetValueOrDefault(nameof(LoginUser.Phone).ToLower());
        if (phone != null)
            jwtPayload.Phone = phone.ToString();

        var name = securityToken.Payload.GetValueOrDefault(nameof(LoginUser.Name).ToLower());
        if (name != null)
            jwtPayload.Name = name.ToString();

        jwtPayload.Exp = DateTimeOffset
            .FromUnixTimeSeconds(long.Parse(securityToken.Payload[JwtRegisteredClaimNames.Exp].ToString()!))
            .ToLocalTime().DateTime;
        jwtPayload.Iat = securityToken.Payload[JwtRegisteredClaimNames.Iat]?.ToString()!;

        return jwtPayload;
    }

    /// <summary>
    /// Jwt载荷信息
    /// </summary>
    public class JwtPayload : LoginUser
    {
        public string? Iat { get; set; }

        public DateTime Exp { get; set; }
    }
}