using IBCL.Domain.Enums;

namespace IBCL.Domain.Entities
{
    public class Position : AuditEntity
    {
        public DateTime TransactionDate { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public PositingStatus Status { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
