using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.App.Application.Entities
{
    public class CollectionEntity : BaseEntity
    {
        public string Title { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public List<PackageEntity> Packages { get; set; }

        public CollectionEntity()
        {
        }

        public CollectionEntity(string title, Guid userId, List<PackageEntity> packages)
            : this()
        {
            Title = title;
            UserId = userId;
            Packages = packages;
        }
    }
}
