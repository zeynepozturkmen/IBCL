using IBCL.Application.Common.Interfaces;
using IBCL.Domain.Entities;
using IBCL.Infrastructure.Persistence;
using IBCL.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
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
                                                                                 ServiceLifetime.Singleton);

        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            ConfigureServices(services);
            return services;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
          
            services.AddScoped<IAccountService, AccountService>();

            services.AddIdentity<Account, IdentityRole<Guid>>()
                   .AddEntityFrameworkStores<IBCLDbContext>()
                   .AddDefaultTokenProviders(); ;
        }
    }
}
