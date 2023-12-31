﻿using IBCL.Domain.Enums;

namespace IBCL.Domain.Entities
{
    public class Position : AuditEntity
    {
        public DateTime TransactionDate { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public PositingStatus Status { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
