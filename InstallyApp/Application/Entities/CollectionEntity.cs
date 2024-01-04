using InstallyApp.Application.Queries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Entities
{
    public class CollectionEntity : BaseEntity
    {
        public string Title { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public UserEntity User { get; set; }

        public CollectionEntity(string title)
        {
            Title = title;
        }
    }
}
