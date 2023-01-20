﻿using Cloud.Infra.Auth.Enum;

namespace Cloud.Infra.Auth.HttpContextUser;

public class LoginUser : ILoginUser
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get ; set; }
    
    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 访问等级
    /// </summary>
    public PermissionsEnum?  CallType { get; set; }
}
