using System.Security.Claims;
using Cloud.Infra.Auth.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Infra.Auth.Policys;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var dateOfBirthClaim = context.User.FindFirst(
            c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == "http://contoso.com");

        if (dateOfBirthClaim is null)
        {
            return Task.CompletedTask;
        }

        var a = PermissionsEnum.Platform;
        if (requirement.PermissionsEnum == a)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}