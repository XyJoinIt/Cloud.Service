
namespace Cloud.Infrastructures.Applicatoins.Auth;

public class LoginUser : ILoginUser
{
    /// <summary>
    /// 
    /// </summary>
    public long Id { get ; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Phone { get; set; }
}
