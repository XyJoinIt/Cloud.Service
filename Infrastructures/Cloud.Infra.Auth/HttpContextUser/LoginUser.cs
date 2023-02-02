using Cloud.Infra.Auth.Enum;
using Microsoft.AspNetCore.Http;
using System;

namespace Cloud.Infra.Auth.HttpContextUser;

public class LoginUser : AuthUser, ILoginUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public LoginUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Id
    /// </summary>
    public override long Id { get => long.Parse(_httpContextAccessor.HttpContext!.User!.FindFirst(nameof(this.Id))!.Value); set { this.Id = value; } }

    /// <summary>
    /// 用户名
    /// </summary>
    public override string? UserName { get => _httpContextAccessor.HttpContext!.User.FindFirst(nameof(this.UserName))?.Value!; set { this.UserName = value; } }

    /// <summary>
    /// 姓名
    /// </summary>
    public override string? Name { get => _httpContextAccessor.HttpContext!.User.FindFirst(nameof(this.Name))?.Value!; set { this.Name = value; } }

    /// <summary>
    /// 电话
    /// </summary>
    public override string? Phone { get => _httpContextAccessor.HttpContext!.User.FindFirst(nameof(this.Phone))?.Value!; set { this.Phone = value; } }

    /// <summary>
    /// 访问等级
    /// </summary>
    public override PermissionsEnum? CallType
    {
        get
        {
            var type = _httpContextAccessor.HttpContext!.User.FindFirst(nameof(this.CallType))?.Value!;
            return (PermissionsEnum)System.Enum.Parse(typeof(PermissionsEnum), type);
        }
        set
        {
            this.CallType = value;
        }
    }
}

public class AuthUser
{

    /// <summary>
    /// Id
    /// </summary>
    public virtual long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public virtual string? UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public virtual string? Phone { get; set; }

    /// <summary>
    /// 访问等级
    /// </summary>
    public virtual PermissionsEnum? CallType { get; set; }
}