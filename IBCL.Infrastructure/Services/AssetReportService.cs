using Azure.Core;
using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Response.Accounts;
using IBCL.Application.Common.Models.Response.Assets;
using IBCL.Application.Common.Models.Response.Position;
using IBCL.Domain.Entities;
using IBCL.Domain.Enums;
using IBCL.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IBCL.Infrastructure.Services
{
    public class AssetReportService : IAssetReportService
    {
        private readonly IBCLDbContext _dbContext;
        private const string BotToken = "6407168081:AAF-Z5qzqqCUvkk3WKc4ctazFG7LRoi8K9Y";
        private const string MyChatId = "6667855809";

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
                        newAsset.CreatedBy = Guid.Empty.ToString();

                        await _dbContext.Asset.AddAsync(newAsset);
                    }
                    else
                    {

                        if (Convert.ToDecimal(item.price) != asset.Price)
                        {
                            asset.LastPrice = asset.Price;
                            asset.Symbol = item.symbol;
                            asset.Price = Convert.ToDecimal(item.price);

                            var positionList = await _dbContext.Position.AsNoTracking().Where(x => x.RecordStatus == RecordStatus.Active
                                                                     && x.Asset.Symbol == item.symbol
                                                                     && x.TransactionType == TransactionType.Purchase)
                                                                     .Include(x => x.Account)
                                                                     .Include(x => x.Asset)
                                                                     .GroupBy(x => x.AccountId)
                                                                     .Select(group => group.FirstOrDefault())
                                                                     .ToListAsync();

                            foreach (var position in positionList)
                            {
                                var positionResponse = position.Adapt<GetAllPositionResponse>();
                                await SendMessageWithTelegram(positionResponse);
                            }
                        }
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

        private async Task SendMessageWithTelegram(GetAllPositionResponse request)
        {

            var botClient = new TelegramBotClient(BotToken);

            User botUser = await botClient.GetMeAsync();

            var rate = (request.Asset.Price - request.Asset.LastPrice) / 100;

            var account = await _dbContext.Account.Where(x => x.RecordStatus == RecordStatus.Active
                                                                     && x.Id == request.Account.Id)
                                                                     .FirstOrDefaultAsync();

            if (account is not null)
            {
                account.NotificationRate = Convert.ToInt32(rate);
                await _dbContext.SaveChangesAsync();
            }

            string messageText = $"Merhaba, {request.Account?.FirstName} {request.Account?.LastName}  " +
                $"hesabın varlık portföyü yüzde {rate} oranında değişmiştir. ";

            //TODO: MyChatId alanı db'den dnamik çekilmeli; fakat fake veri sıkıntısı yasanmaması adına kendi chatId'imi const olarak tanımladım :)
            //await botClient.SendTextMessageAsync(request.Account.TelegramChatId, messageText);
            await botClient.SendTextMessageAsync(MyChatId, messageText);
        }
    }
}
