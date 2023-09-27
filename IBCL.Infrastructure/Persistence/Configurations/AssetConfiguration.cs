using IBCL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IBCL.Infrastructure.Persistence.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {

            builder.HasOne(x => x.Account).WithMany(x => x.Assets).HasForeignKey(x => x.AccountId);
        }
    }
}

