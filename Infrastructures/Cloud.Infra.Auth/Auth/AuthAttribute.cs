namespace Cloud.Infra.Auth.Auth;

/// <summary>
/// 授权
/// </summary>
public class AuthAttribute:Attribute
{

    /// <summary>
    /// 构造函数
    /// </summary>
    public AuthAttribute()
    {
        
    }
    
    /// <summary>
    /// 角色
    /// </summary>
    public string[]? Roles { get; set; }

    /// <summary>
    /// 系统
    /// </summary>
    public ClaimType? ClaimType { get; set; }
}