namespace Cloud.Infra.Auth;

public interface ILoginUser
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    public string? Phone { get; set; }
}
