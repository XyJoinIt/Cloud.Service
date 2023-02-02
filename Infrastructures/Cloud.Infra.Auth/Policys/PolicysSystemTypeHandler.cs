using System.Security.Claims;
using Cloud.Infra.Auth.Enum;
using Cloud.Infra.Auth.HttpContextUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Cloud.Infra.Auth.Policys;

public class PolicysSystemTypeHandler : AuthorizationHandler<PolicysSystemTypeRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PolicysSystemTypeHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PolicysSystemTypeRequirement requirement)
    {

        AuthUser authUser = new AuthUser();
        var dateOfBirthClaim = context.User.FindFirst(c => c.Type == nameof(authUser.CallType).ToLower());

        if(dateOfBirthClaim==null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if ((PermissionsEnum)System.Enum.Parse(typeof(PermissionsEnum), dateOfBirthClaim!.Value) == requirement.PermissionsEnum)
        {
            context.Succeed(requirement);
        }

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}