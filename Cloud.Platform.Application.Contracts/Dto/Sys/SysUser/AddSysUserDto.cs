namespace Cloud.Platform.Application.Contracts.Dto.Sys.SysUser;

public class AddSysUserDto:BaseSysUserDto
{
    
}

public class EditSysUserDto:BaseSysUserDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }
}