using Cloud.Infra.Auth.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Cloud.Infra.Auth.Inside;

public class ClaimRequirementFilter : IAuthorizationFilter
{
    private readonly Claim _claim;
    private readonly IEnumerable<IAuthService> _authService;
    private readonly ILoginUser _loginUser;
    private readonly CloudAuthOption _cloudAuthOption;
    public ClaimRequirementFilter(ILoginUser loginUser, Claim claim, IEnumerable<IAuthService> authService, IOptions<CloudAuthOption> AdmetusSettings)
    {
        _loginUser = loginUser;
        _claim = claim;
        _authService = authService;
        _cloudAuthOption = AdmetusSettings.Value;
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ControllerActionDescriptor? controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            var skipAuthorization = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
            if (skipAuthorization)
            {
                return;
            }
        }

        ClaimType claimType = Enum.Parse<ClaimType>(_claim.Type);
        bool permission = false;

        string token = GetToken();
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (token.StartsWith("Bearer "))
        {
            token = token[7..];
        }

        switch (claimType)
        {
            case ClaimType.Platform:
                permission = _authService.First(x => x.ServiceName.Equals(nameof(PlatformAuthServiceImpl), StringComparison.OrdinalIgnoreCase)).Permission(token);
                break;
            case ClaimType.Blog:
                break;
            default:
                break;
        }

        if (!permission)
        {
            context.Result = new UnauthorizedResult();
            return;
        }


        string GetToken()
        {
            string token = context.HttpContext.Request.Headers["Authorization"]!;
            if (string.IsNullOrEmpty(token))
            {
                token = context.HttpContext.Request.Query["Authorization"]!;
            }
            if (string.IsNullOrEmpty(token))
            {
                context.HttpContext.Request.Cookies.TryGetValue("Authorization", out token);
            }
            return token;
        }
    }

}
