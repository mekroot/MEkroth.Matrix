
using Microsoft.EntityFrameworkCore;

namespace MEkroth.Matrix.Api.Database
{
    /// <summary>
    /// This is a bootstrap class to setup the dependency injection for application context.
    /// </summary>
    public static class DatabaseSetup
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlLiteApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Default")));
            services.AddScoped<IApplicationDbContext, SqlLiteApplicationDbContext>();
            return services;
        }
    }
}
