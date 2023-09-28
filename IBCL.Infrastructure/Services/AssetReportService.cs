using Azure.Core;
using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Response.Assets;
using IBCL.Domain.Entities;
using IBCL.Domain.Enums;
using IBCL.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IBCL.Infrastructure.Services
{
    public class AssetReportService : IAssetReportService
    {
        private readonly IBCLDbContext _dbContext;

        public AssetReportService(IBCLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task GetMinutelyAssetsReport()
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync("https://www.binance.me/api/v3/ticker/price?symbols=%5B%22ETHUSDT%22,%22BTCUSDT%22,%22AVAXUSDT%22%5D ");

                if (!result.IsSuccessStatusCode)
                {
                    return;
                }

                var responseString = await result.Content.ReadAsStringAsync();

                var assetReports = JsonSerializer.Deserialize<List<AssetReportResponse>>(responseString);

                foreach (var item in assetReports)
                {
                    var asset = await _dbContext.Asset.Where(x => x.Symbol == item.symbol)
                                                       .FirstOrDefaultAsync();

                    if (asset is null)
                    {
                        var newAsset = new Asset();
                        newAsset.Price = Convert.ToDecimal(item.price);
                        newAsset.Symbol = item.symbol;
                        newAsset.RecordStatus = RecordStatus.Active;
                        newAsset.CreatedBy = Guid.NewGuid().ToString();

                        await _dbContext.Asset.AddAsync(newAsset);
                    }
                    else
                    {
                        asset.LastPrice = asset.Price;
                        asset.Symbol = item.symbol;
                        asset.Price= Convert.ToDecimal(item.price); 
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message;
                throw;
            }
  
        }
    }
}
