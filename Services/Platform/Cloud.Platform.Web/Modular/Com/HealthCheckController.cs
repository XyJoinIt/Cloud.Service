using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Platform.Web.Modular.Com
{
    public class HealthCheckController : PlatformController
    {
        public HealthCheckController() { }

        [HttpGet]
        public IActionResult ConsulCheck()
        {
            return Ok();
        }

        [HttpGet]
        public string Check()
        {
            string str = (Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + Request.HttpContext.Connection.LocalPort);
            return $"Platform:---{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}----" +
                $"{str}";
        }
    }
}
