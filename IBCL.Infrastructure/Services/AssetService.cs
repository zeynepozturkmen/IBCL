using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Response.Assets;
using IBCL.Domain.Enums;
using IBCL.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace IBCL.Infrastructure.Services
{
    public class AssetService : IAssetService
    {
        private readonly IBCLDbContext _dbContext;

        public AssetService(IBCLDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<AssetDto>> GetAllAsync()
        {
            var query = await _dbContext.Asset.AsNoTracking().Where(x => x.RecordStatus == RecordStatus.Active)
                                                          .ToListAsync();

            return query.Adapt<List<AssetDto>>();
        }
    }
}
