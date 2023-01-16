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
        Action<CloudAuthOption> option)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        CloudAuthOption cloudAuthOption = new();
        option(cloudAuthOption);


        serviceCollection.AddTransient<IAuthorizationHandler, PermissionHandler>();
        //策略授权
        serviceCollection.AddAuthorization(x =>
        {
            x.AddPolicy("policys", policys => { policys.AddRequirements(new PermissionRequirement()); });
        });

        serviceCollection.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = nameof(ApiResponseHandler);
            x.DefaultChallengeScheme = nameof(ApiResponseHandler);
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
        }).AddScheme<AuthenticationSchemeOptions,ApiResponseHandler>(nameof(ApiResponseHandler),p=>{});

        
        serviceCollection.AddScoped<ILoginUser, LoginUser>();
        serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return serviceCollection;
    }
}