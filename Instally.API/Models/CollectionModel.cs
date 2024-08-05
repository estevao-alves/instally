using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.API.Models
{
    public class CollectionModel : BaseModel
    {
        public string Title { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public List<PackageModel> Packages { get; set; }

        public CollectionModel()
        {
        }

        public CollectionModel(string title, Guid userId, List<PackageModel> packages)
            : this()
        {
            Title = title;
            UserId = userId;
            Packages = packages;
        }
    }
}
