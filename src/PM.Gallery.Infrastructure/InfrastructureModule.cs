using Microsoft.EntityFrameworkCore;
using PM.Gallery.Domain.IRepository;
using PM.Gallery.Infrastructure.EntityFrameworkCore;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Repositories;

namespace PM.Gallery.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext with MySQL connection string
        // Use "LazyLoading" for better performance
        services.AddDbContext<AppDbContext>(options => options.UseMySql(configuration.GetConnectionString("MySQL"),
            MySqlServerVersion.LatestSupportedServerVersion)
        );

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}