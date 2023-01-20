using System.Text;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.Auth.HttpContextUser;
using Cloud.Infra.Auth.Policys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Cloud.Infra.Auth.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// 注入授权服务
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="option"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static IServiceCollection AddAuthorizationSetup(this IServiceCollection serviceCollection,
        Action<AuthOption> option)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        AuthOption cloudAuthOption = new();
        option(cloudAuthOption);


        //策略授权 
        serviceCollection.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(PolicyType.SystemType), policy =>
                policy.Requirements.Add(new PermissionRequirement(cloudAuthOption.PermissionsEnum ?? PermissionsEnum.All)));
        });

        serviceCollection.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true, //是否验证issuer
                ValidateAudience = true, //是否验证audience
                ValidateIssuerSigningKey = true, //是否验证securitykey
                ValidateLifetime = true, //是否验证失效时间
                ValidIssuer = cloudAuthOption.Issuer, //发行人
                ValidAudience = cloudAuthOption.Audience, //订阅人
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cloudAuthOption.SecurityKey)), //securityKey
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        serviceCollection.AddScoped<ILoginUser, LoginUser>();
        serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return serviceCollection;
    }
}