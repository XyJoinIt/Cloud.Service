

using Cloud.Infra.EventBus.CAP;
using Cloud.Infra.EventBus.CAP.Filters;

namespace Cloud.Infra.EventBus.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCloudCap(this IServiceCollection services,Action<CapOptions> option)
    {
        if(services == null)
            throw new ArgumentNullException(nameof(services));

        services.AddSingleton<IEventPublisher, CapPublisher>()
            .AddCap(option)
            .AddSubscribeFilter<CloudCapFilter>();

        return services;
    }
}
