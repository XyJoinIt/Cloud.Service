using Cloud.Platform.Repository.Dto.Sys.SysUser;
using Cloud.Platform.Repository.Service.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Platform.Service.Service.Sys
{
    public class SysUserService : ISysUserRepository
    {
        public Task Add(AddSysUserDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(EditSysUserDto input)
        {
            throw new NotImplementedException();
        }

        public string SendMsg(string msg)
        {
            return msg;
        }
    }
}
