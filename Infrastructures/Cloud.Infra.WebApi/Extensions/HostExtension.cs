using Cloud.Infra.Consul.Configurations;
using Cloud.Infra.Consul.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.WebApi.Extensions;

public static class HostExtension
{
    public static IHost UseCloud(this WebApplication host, Action<UseCloudOption> action)
    {
        if(host is null) throw new ArgumentNullException(nameof(host));

        UseCloudOption useCloudOption = new();
        action(useCloudOption);

        host.RegisterConsul(host.Services.GetService<IHostApplicationLifetime>()!, useCloudOption.consulOptions!);

        return host;
    }


}

public class UseCloudOption
{
    public ConsulOptions? consulOptions { get; set; }
}