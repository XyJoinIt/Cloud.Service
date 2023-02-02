using Cloud.Platform.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cloud.Platform.Repository.Dto.Sys.SysUserManage;

public class BaseSysUserDto
{
    public UserInfo? userInfo { get; set; } = default!;
}