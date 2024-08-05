using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Instally.API.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; protected set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; protected set; }

        protected BaseModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
