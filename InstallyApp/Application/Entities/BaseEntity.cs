using System.ComponentModel.DataAnnotations;

namespace InstallyApp.Application.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
