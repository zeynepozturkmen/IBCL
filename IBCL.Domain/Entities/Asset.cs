

namespace IBCL.Domain.Entities
{
    public class Asset : AuditEntity
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
