using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infrastructures.Applicatoins.Auth;

public interface IAuthService
{
    /// <summary>
    /// 服务名
    /// </summary>
    public string ServiceName { get;}

    /// <summary>
    /// 权限判断
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    bool Permission(string token);
}
