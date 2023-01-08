using Cloud.Infra.Repository.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Cloud.Infra.Repository.Extensions;
public static class ServiceCollectionExtension
{
    /// <summary>
    /// 注入仓储相关
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfraRepository<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> option) where TDbContext : DefaultDbContext
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.TryAddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddDbContext<TDbContext>(option);

        return services;
    }
}
