

namespace Cloud.Infra.EventBus.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCloudCap(this IServiceCollection services,Action<CapOptions> option)
    {
        if(services == null)
            throw new ArgumentNullException(nameof(services));

        CapOptions options = new();
        option(options);


        return services;
    }
}
