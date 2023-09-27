using IBCL.Domain.Enum;
using IBCL.Domain.Persistence;
using System.ComponentModel.DataAnnotations;

namespace IBCL.Domain.Entities
{
    public class AuditEntity : IEntity<Guid>
    {
        public Guid Id { get; set; } = SequentialGuid.NewSequentialGuid();
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }

        [MaxLength(50)]
        public string LastModifiedBy { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }
}
