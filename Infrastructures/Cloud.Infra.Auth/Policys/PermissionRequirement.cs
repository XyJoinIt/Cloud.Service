using Cloud.Infra.Auth.HttpContextUser;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Infra.Auth.Policys;

public class PermissionRequirement:LoginUser,IAuthorizationRequirement
{
    
}