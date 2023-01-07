using Cloud.Infra.Repository.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Repository.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// 注入仓储相关
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfraRepository(this IServiceCollection services, Action<DbContextOptionsBuilder> option)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.TryAddScoped<IUnitOfWork, UnitOfWork<DefaultDbContext>>();
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddDbContext<DefaultDbContext>(option);

        return services;
    }
}
