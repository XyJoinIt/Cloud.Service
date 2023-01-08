using Cloud.Platform.Application.Contracts.Dto.Sys.SysUser;
using Cloud.Platform.Application.Contracts.Service.Sys;

namespace Cloud.Platform.Application.Service.Sys;

public class SysUserService:ISysUserService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Add(AddSysUserDto input)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Edit(EditSysUserDto input)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }
}