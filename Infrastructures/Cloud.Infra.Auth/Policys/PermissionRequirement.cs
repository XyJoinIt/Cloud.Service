using Cloud.Infra.Auth.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Infra.Auth.Policys;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(PermissionsEnum permissionsEnum) =>
        PermissionsEnum = permissionsEnum;

    public PermissionsEnum? PermissionsEnum { get; set; }
}