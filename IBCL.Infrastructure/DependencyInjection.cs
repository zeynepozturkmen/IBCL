using IBCL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IBCL.Infrastructure
{
    public static class DependencyInjection
    {
        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<IBCLDbContext>(options => options.UseSqlServer(connectionString, optionBuilder =>
                                                                                 optionBuilder.MigrationsAssembly("IBCL.Infrastructure")),
                                                                                 ServiceLifetime.Scoped);

        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            return services;
        }
    }
}
