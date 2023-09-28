using FluentValidation.AspNetCore;
using Hangfire;
using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Request.Account;
using IBCL.Domain.Entities;
using IBCL.Infrastructure.Persistence;
using IBCL.Infrastructure.Services;
using IBCL.Infrastructure.Validator.Account;
using IBCL.Infrastructure.Validator.Position;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            ConfigureHangfire(services, configuration);
            AuthenticationConfiguration(services);
            ValidatorConfiguration(services);
            return services;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
          
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IAssetReportService, AssetReportService>();

            services.AddIdentity<Account, IdentityRole<Guid>>()
                   .AddEntityFrameworkStores<IBCLDbContext>()
                   .AddDefaultTokenProviders(); ;
        }

        private static void ConfigureHangfire(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HangfireConnectionString");
            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();
        }

        public static void AuthenticationConfiguration(IServiceCollection services) {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecretsecretsecretsecretsecret")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });

        }

        public static void ValidatorConfiguration(IServiceCollection services)
        {
            services.AddControllers()
                      .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AccountLoginValidator>());

            services.AddControllers()
                     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterValidator>());

            services.AddControllers()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SavePositionValidator>());


            services.AddControllers()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdatePositionValidator>());

            services.AddControllers()
                   .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetAllPositionValidator>());

        }
    }
}
