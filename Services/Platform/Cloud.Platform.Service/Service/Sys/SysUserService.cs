using Cloud.Platform.Service.Contracts.Dto.Sys.SysUser;
using Cloud.Platform.Service.Contracts.Service.Sys;

namespace Cloud.Platform.Service.Service.Sys;

public class SysUserService : ISysUserService
{
    private readonly IEventPublisher _eventPublisher;

    public SysUserService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

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

    public async Task SendMsg(string msg)
    {
        await _eventPublisher.PublishAsync("SendMsgTest", msg);
        _eventPublisher.PublishDelayMsg(TimeSpan.FromSeconds(10), "SendDelayMsgTest", msg);
    }
}