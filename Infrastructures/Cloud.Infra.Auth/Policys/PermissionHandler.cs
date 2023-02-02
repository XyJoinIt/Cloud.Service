using System.Security.Claims;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.Auth.HttpContextUser;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Infra.Auth.Policys;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ILoginUser _loginUser;

    public PermissionHandler(ILoginUser loginUser)
    {
        _loginUser = loginUser;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        //如果是全部权限
        if (requirement.PermissionsEnum == PermissionsEnum.All)
            context.Succeed(requirement);

        var type = context.User.Claims.FirstOrDefault(x => x.Type == nameof(_loginUser.CallType).ToLower())!.Value;

        if ((PermissionsEnum)System.Enum.Parse(typeof(PermissionsEnum), type) == requirement.PermissionsEnum)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}