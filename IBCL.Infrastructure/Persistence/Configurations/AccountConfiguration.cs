using IBCL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IBCL.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.IdentityNumber).HasMaxLength(11).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();


            builder.HasMany(x => x.Assets).WithOne(x => x.Account).HasForeignKey(x=>x.AccountId);
        }
    }
}
