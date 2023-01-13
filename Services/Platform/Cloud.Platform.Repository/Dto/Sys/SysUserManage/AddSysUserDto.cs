
using Cloud.Infra.EntityFrameworkCore.Entities;
using Cloud.Platform.Model.Entity;
namespace Cloud.Platform.Repository.Dto.Sys.SysUserManage;

[AutoMap(typeof(SysUser), ReverseMap = true)]
public class AddSysUserDto : BaseSysUserDto
{

}

[AutoMap(typeof(SysUser), ReverseMap = true)]
public class EditSysUserDto : BaseSysUserDto, IDtoId
{
    public long Id { get; set; }
}

[AutoMap(typeof(SysUser), ReverseMap = true)]
public class OutSysUserPageDto : BaseSysUserDto, IIsCreate, IDtoId
{
    public long Id { get; set; }

    public long CreateId { get; set; }

    public DateTime CreateTime { get; set; }
}