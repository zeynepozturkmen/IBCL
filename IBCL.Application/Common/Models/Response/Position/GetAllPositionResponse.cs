using IBCL.Application.Common.Models.Response.Accounts;
using IBCL.Application.Common.Models.Response.Assets;

namespace IBCL.Application.Common.Models.Response.Position
{
    public class GetAllPositionResponse
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string TransactionType { get; set; }
        public AssetDto Asset { get; set; }
        public AccountDto Account { get; set; }
    }
}
