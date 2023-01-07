using Cloud.Infra.Repository.IRepositories;
using Cloud.Platform.Model;
using Cloud.Platform.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Cloud.Platform.Web.Modular.Sys
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly IRepository<SysUser> _repository;
        public SysUserController(IRepository<SysUser> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public string getname()
        {
            var list = _repository.QueryAsNoTracking();
            return "213";
        }
    }
}
