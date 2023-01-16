using Cloud.Infra.Mapper.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Infra.Mapper.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCloudInfraAutoMapper(this IServiceCollection services, params Type[] profileAssemblyMarkerTypes)
    {
        if (services==null) throw new ArgumentNullException(nameof(services));
        services.AddAutoMapper(profileAssemblyMarkerTypes);
        services.AddSingleton<IObjectMapper, AutoMapperObject>();
        return services;
    }

    public static IServiceCollection AddCloudInfraAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        services.AddAutoMapper(assemblies);
        services.AddSingleton<IObjectMapper, AutoMapperObject>();
        return services;
    }
}
