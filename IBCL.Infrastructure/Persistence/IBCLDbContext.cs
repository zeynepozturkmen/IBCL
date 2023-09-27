using System.Reflection;
using IBCL.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IBCL.Infrastructure.Persistence
{
    public class IBCLDbContext : BaseDbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Asset> Asset { get; set; }

        public IBCLDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Account>().ToTable("Account");
            builder.Entity<Asset>().ToTable("Asset");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
