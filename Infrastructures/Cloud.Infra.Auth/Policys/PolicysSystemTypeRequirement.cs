using Cloud.Infra.Auth.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Infra.Auth.Policys;

public class PolicysSystemTypeRequirement : IAuthorizationRequirement
{
    public PolicysSystemTypeRequirement(PermissionsEnum permissionsEnum) =>
        PermissionsEnum = permissionsEnum;

    public PermissionsEnum? PermissionsEnum { get; set; }
}