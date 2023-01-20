using Cloud.Platform.Model.Entity;

namespace Cloud.Platform.Repository.Dto.Auth;

public class InputLoginDot
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? PassWord { get; set; }

    /// <summary>
    /// 验证吗ID
    /// </summary>
    public long? VeriftId { get; set; }

    /// <summary>
    /// 验证码内容
    /// </summary>
    public string? VerifText { get; set; }
}