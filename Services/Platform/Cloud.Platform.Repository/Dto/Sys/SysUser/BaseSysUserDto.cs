using Cloud.Infra.WebApi.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Cloud.Platform.Repository.Dto.Sys.SysUser;

public class BaseSysUserDto
{
    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; } = default!;

    /// <summary>
    /// 密码（默认HMACSHA256加密）
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 性别-男_1、女_2
    /// </summary>
    public Gender Sex { get; set; } = Gender.保密;

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// 管理员类型-超级管理员_1、管理员_2、普通账号_3
    /// </summary>
    public AccessType AdminType { get; set; } = AccessType.普通账号;

    /// <summary>
    /// 状态-正常_0、停用_1、删除_2
    /// </summary>
    public CommonStatus Status { get; set; } = CommonStatus.正常;
}