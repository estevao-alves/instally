using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Instally.App.Application.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; protected set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
