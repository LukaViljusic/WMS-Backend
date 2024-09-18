using System.ComponentModel.DataAnnotations;

namespace WorkManagerSystemBackend.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool isActive { get; set; } = true;
    }
}
