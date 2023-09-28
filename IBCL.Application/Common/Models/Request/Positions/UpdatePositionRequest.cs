using IBCL.Domain.Enums;

namespace IBCL.Application.Common.Models.Request.Positions
{
    public class UpdatePositionRequest 
    {
        public Guid Id { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public PositingStatus Status { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
