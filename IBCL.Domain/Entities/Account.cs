

namespace IBCL.Domain.Entities
{
    public class Account : AuditEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public decimal NotificationRate { get; set; }
    }
}
