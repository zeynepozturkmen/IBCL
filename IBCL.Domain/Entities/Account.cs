using IBCL.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IBCL.Domain.Entities
{
    public class Account : IdentityUser<Guid>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }

        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Balance { get; set; }
        public int NotificationRate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }

        public RecordStatus RecordStatus { get; set; }
        public List<Position> Positions { get; set; }
    }
}
