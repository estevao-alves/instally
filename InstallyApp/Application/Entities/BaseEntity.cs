using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
