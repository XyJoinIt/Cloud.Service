using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Cloud.Infra.Auth.Policys;

public class ApiResponseHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ApiResponseHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.ContentType = "application/json";
        var resObj = new
        {
            Message ="很抱歉,您无权访问该接口。",
            Succeeded = false,
            Code = 401
        };

        await Response.WriteAsync(JsonConvert.SerializeObject(resObj));
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.ContentType = "application/json";
        var resObj = new
        {
            Message ="很抱歉,您访问权限等级不够。",
            Succeeded = false,
            Code = 401
        };

        await Response.WriteAsync(JsonConvert.SerializeObject(resObj));
    }
}