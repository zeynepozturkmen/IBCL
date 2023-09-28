

namespace IBCL.Domain.Entities
{
    public class Asset : AuditEntity
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal LastPrice { get; set; }
        public List<Position> Positions { get; set; }
    }
}
