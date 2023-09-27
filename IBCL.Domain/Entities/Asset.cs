

namespace IBCL.Domain.Entities
{
    public class Asset : AuditEntity
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
    }
}
