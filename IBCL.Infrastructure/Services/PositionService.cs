using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Request.Positions;
using IBCL.Application.Common.Models.Response.Assets;
using IBCL.Application.Common.Models.Response.Position;
using IBCL.Domain.Entities;
using IBCL.Domain.Enums;
using IBCL.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace IBCL.Infrastructure.Services
{
    public class PositionService : IPositionService
    {
        private readonly IBCLDbContext _dbContext;

        public PositionService(IBCLDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task SavePositionAsync(SavePositionRequest request)
        {
            var account = await _dbContext.Account.Where(x => x.Id == request.AccountId
                                                         && x.RecordStatus == RecordStatus.Active)
                                                   .FirstOrDefaultAsync();

            var asset = await _dbContext.Asset.Where(x => x.Id == request.AssetId
                                                         && x.RecordStatus == RecordStatus.Active)
                                                   .FirstOrDefaultAsync();

            if (account is null || asset is null)
            {
                throw new Exception("Account or asset Notfound");
            }

            var position = request.Adapt<Position>();
            position.RecordStatus = RecordStatus.Active;
            position.CreatedBy = Guid.NewGuid().ToString();
            position.TotalPrice = request.Amount * asset.Price;

            await _dbContext.Position.AddAsync(position);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePositionAsync(UpdatePositionRequest request)
        {
            var position = await _dbContext.Position.Where(x => x.Id == request.Id)
                                           .FirstOrDefaultAsync();

            if (position is null)
            {
                throw new Exception("Notfound");
            }


            var asset = await _dbContext.Asset.Where(x => x.Id == position.AssetId
                                                        && x.RecordStatus == RecordStatus.Active)
                                                  .FirstOrDefaultAsync();

            if (asset is null)
            {
                throw new Exception("Account or asset Notfound");
            }

            request.Adapt(position);
            position.TotalPrice = request.Amount * asset.Price;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<GetAllPositionResponse>> GetAllAsync(GetAllPositionRequest request)
        {
            var query = await _dbContext.Position.AsNoTracking().Where(x => x.RecordStatus == RecordStatus.Active
                                                                 && x.AccountId == request.AccountId)
                                                                 .Include(x=>x.Account)
                                                                 .Include(x=>x.Asset)
                                                                 .ToListAsync();

            return query.Adapt<List<GetAllPositionResponse>>();
        }
    }
}
